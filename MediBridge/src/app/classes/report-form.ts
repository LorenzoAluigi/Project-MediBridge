export class ReportForm {
  public description: string = '';
  public userId: string = '';
  public reportDate: Date = new Date();
  public reportTypeId: number = 0;
  public fileBase64: string = ''; // Campo per l'array di byte codificato in Base64
  public fileName: string = '';

  constructor(
    description: string,
    userId: string,
    reportDate: Date,
    reportTypeId: number,
    file: Uint8Array,
    fileName: string = ''
  ) {
    this.description = description;
    this.userId = userId;
    this.reportDate = reportDate;
    this.reportTypeId = reportTypeId;
    this.fileBase64 = this.encodeArrayToBase64(file); // Codifica l'array di byte in Base64
    this.fileName = fileName;
  }

  private encodeArrayToBase64(array: Uint8Array): string {
    // Converte l'array di byte in una stringa Base64
    const binaryString = Array.from(array)
      .map((byte) => String.fromCharCode(byte))
      .join('');
    return btoa(binaryString);
  }
}
