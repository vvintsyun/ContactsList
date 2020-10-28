import {ContactInfo} from './contact-info';

export class Contact {
  constructor(
    public id: number,
    public firstName: string,
    public lastName: string,
    public middleName: string,
    public birthDate: string,
    public organizationName: string,
    public organizationPost: string,
    public contactInfos: ContactInfo[]
    ) { }
}
