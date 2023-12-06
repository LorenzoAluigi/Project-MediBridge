export class TherapeuticPlan {
  constructor(
    public MedicalConditionId: string,
    public PlanDescription: string,
    public StartDate: Date,
    public ExpiryDate: Date,
    public UserId?: string,
    public PlanId?: string,
    public MedicalCondition?: string
  ) {}
}
