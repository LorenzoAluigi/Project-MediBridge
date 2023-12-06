export class MyReport {
  constructor(
    public IdReport: string,
    public description: string,
    public fileName: string,
    public filePath: string,
    public reportDate: Date,
    public reportTypeId: number,
    public reportsType: string,
    public userId: string
  ) {}
}
