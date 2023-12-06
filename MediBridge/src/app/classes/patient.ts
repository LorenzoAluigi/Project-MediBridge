export class Patient {
  constructor(
    public UserId: number,
    public FirstName: string,
    public LastName: string,
    public Gender: string,
    public DateOfBirth: Date,
    public CF: string,
    public Address: string,
    public City: string,
    public Province: string,
    public Country: string,
    public Email: string
  ) {}
}
