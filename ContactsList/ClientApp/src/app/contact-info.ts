
export class ContactInfo {
  constructor(
    public value: string,
    public type: ContactInfoType,
    ) { }
}

export enum ContactInfoType {
  Telephone,
  Email,
  Skype,
  Other
}
