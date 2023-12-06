import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authSvc: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      firstName: this.fb.control(null, [Validators.required]),
      LastName: this.fb.control(null, [Validators.required]),
      gender: this.fb.control(null, [Validators.required]),
      dateOfBirth: this.fb.control(null, [Validators.required]),
      cf: this.fb.control(null, [Validators.required]),
      address: this.fb.control(null, [Validators.required]),
      city: this.fb.control(null, [Validators.required]),
      province: this.fb.control(null, [Validators.required]),
      country: this.fb.control(null, [Validators.required]),
      email: this.fb.control(null, [Validators.required]),
      password: this.fb.control(null, [Validators.required]),
    });
  }
  register() {
    this.authSvc.signUp(this.form.value).subscribe((res) => {
      console.log('registrato');
      this.router.navigate(['/register']);
    });
  }
}
