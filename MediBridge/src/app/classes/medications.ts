export class Medications {
  constructor(
    public MedicationName: string,
    public DailyDosage: string,
    public MedicalConditionId: string,
    public PlanId?: string,
    public MedicationId?: string,
    public MedicalCondition?: string,
    public TPlanDescription?: string
  ) {}
}
