import { TherapeuticPlan } from './therapeutic-plan';
import { MedicalCondition } from './medical-condition';
import { MyReport } from './my-report';

export class User {
  constructor(
    public UserId: string,
    public FirstName: string,
    public LastName: String,
    public Gender: string,
    public DateOfBirth: string,
    public CF: string,
    public Address: string,
    public City: string,
    public Province: string,
    public Country: string,
    public Email: string,
    public Reports?: MyReport[],
    public MedicalCondition?: MedicalCondition[],
    public TherapeuticPlan?: TherapeuticPlan[],
    public roles?: string[]
  ) {}

  static createEmptyUser() {
    return new User('', '', '', '', '', '', '', '', '', '', '');
  }
}
