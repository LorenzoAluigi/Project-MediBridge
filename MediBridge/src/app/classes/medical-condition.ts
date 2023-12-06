export class MedicalCondition {
  constructor(
    public Description: string,
    public UserId: string,
    public DiagnosisYear?: number,
    public MedicalConditionId?: number
  ) {}
}
