import { IAccessData } from '../interfaces/iaccess-data';

export class AccessData implements IAccessData {
  public UID!: string;
  public role!: string[] | null;
  public token!: string;

  constructor(id: string, token: string, role: string[] | null) {
    this.UID = id;
    this.token = token;
    this.role = role;
  }
}
