import { fetch } from "domain-task";
import { AuthService } from "./AuthService";

export enum HttpMethod {
    GET = "GET",
    POST = "POST",
    PUT = "PUT",
}

export class BaseService {
    public static fetch(method: string, url: string, data?: any): Promise<any> {
        return AuthService.getToken().then((token: string) => fetch(url, {
            method,
            headers: {
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json; charset=utf-8",
            },
            body: JSON.stringify(data),
        }));
    }

    public static fetchAnonymous(method: string, url: string, data?: any): Promise<any> {
        return fetch(url, {
            method,
            headers: {
                "Content-Type": "application/json; charset=utf-8",
            },
            body: JSON.stringify(data),
        });
    }
}