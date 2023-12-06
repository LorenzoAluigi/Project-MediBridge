import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, map, tap } from 'rxjs';
import { IRegister } from './interfaces/iregister';
import { IAccessData } from './interfaces/iaccess-data';
import { User } from './classes/user';
import { AccessData } from './classes/access-data';
import { ILogin } from './interfaces/ilogin';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private jwtHelper: JwtHelperService = new JwtHelperService();
  private authSubject = new BehaviorSubject<null | AccessData>(null);
  user$ = this.authSubject.asObservable();
  isLoggedIn$ = this.user$.pipe(map((user) => !!user));
  userUID!: string;

  private userDataSubject = new BehaviorSubject<User | null>(null);
  userData$ = this.userDataSubject.asObservable();

  autoLogoutTimer: any;

  apiUrl: string = 'https://localhost:44327';
  registerUrl: string = this.apiUrl + '/api/register';
  loginUrl: string = this.apiUrl + '/api/login';
  getUserByIdUrl: string = this.apiUrl + '/api/Users/';

  constructor(private http: HttpClient, private router: Router) {
    this.restoreUser();
  }

  signUp(data: IRegister) {
    return this.http.post<IRegister>(this.registerUrl, data);
  }

  login(data: ILogin) {
    return this.http.post<IAccessData>(this.loginUrl, data).pipe(
      tap((data) => {
        const token = JSON.stringify(data);

        const tokenPayload = this.jwtHelper.decodeToken(token);

        localStorage.setItem('accessData', token); //salvo lo user per poterlo recuperare se si ricarica la pagina
        const user = new AccessData(
          tokenPayload.UID,
          data.token,
          tokenPayload.role
        );
        console.log('user in auth loign', user);

        this.userUID = tokenPayload.UID;

        this.authSubject.next(user); //invio lo user al subject
        console.log(this.user$);
        this.getUserData(this.userUID);
        const expDate = this.jwtHelper.getTokenExpirationDate(token) as Date;

        this.autoLogout(expDate); //un metodo per il logout automatico
        if (user.role?.includes('Doctor')) {
          console.log("sono nell'if");

          this.router.navigate(['/doctorDashboard']);
        } else {
          this.router.navigate(['/userDashboard']);
        }
      })
    );
  }

  logout() {
    this.authSubject.next(null); //comunico al behaviorsubject che il valore da propagare è null
    this.userDataSubject.next(null); //comunico al behaviorsubject che il
    localStorage.removeItem('accessData'); //elimino i dati salvati in localstorage
    this.router.navigate(['/login']); //redirect al login
  }

  autoLogout(expDate: Date) {
    const expMs = expDate.getTime() - new Date().getTime(); //sottraggo i ms della data attuale da quelli della data del jwt
    this.autoLogoutTimer = setTimeout(() => {
      //avvio un timer che fa logout allo scadere del tempo
      this.logout();
    }, expMs);
  }

  //metodo che controlla al reload di pagina se l'utente è loggato e se il jwt è scaduto
  restoreUser() {
    const userJson: string | null = localStorage.getItem('accessData'); //recupero i dati di accesso

    if (!userJson) return; //se i dati non ci sono blocco la funzione

    const accessData: any = JSON.parse(userJson); //se viene eseguita questa riga significa che i dati ci sono, quindi converto la stringa(che conteneva un json) in oggetto

    const tokenPayload = this.jwtHelper.decodeToken(accessData.token);

    const user = new AccessData(
      tokenPayload.UID,
      accessData.token,
      tokenPayload.role
    );
    this.userUID = tokenPayload.UID;

    //ora controllo se il token è scaduto, se lo è fermiamo la funzione
    if (this.jwtHelper.isTokenExpired(accessData.token)) return;

    //se nessun return viene eseguito proseguo
    this.authSubject.next(user); //invio i dati dell'utente al behaviorsubject
    const expDate = this.jwtHelper.getTokenExpirationDate(
      accessData.token
    ) as Date;
    this.autoLogout(expDate);
  }

  getUserData(id: string) {
    return this.http.get<User>(this.getUserByIdUrl + id);
  }
}
