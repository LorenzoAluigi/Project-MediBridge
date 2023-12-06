import { Doctor } from './doctor';
import { Patient } from './patient';
export class PatientDoctor {
  constructor(
    public DoctorsId: number,
    public UsersId: string,
    public PatientDoctorId?: number,
    public Doctor?: Doctor,
    public Patient?: Patient
  ) {}
}
