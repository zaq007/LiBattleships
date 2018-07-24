import * as JwtDecode from 'jwt-decode';

const ANONYMOUS_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous";

export class JwtToken {
    public plainToken: string;
    public IsGuest: boolean;

    constructor(plainToken: any) {
        this.plainToken = plainToken;
        const plainObject: any = JwtDecode(plainToken);
        this.IsGuest = plainObject[ANONYMOUS_CLAIM];
    }

}