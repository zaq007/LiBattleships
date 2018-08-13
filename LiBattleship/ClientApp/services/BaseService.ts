import { fetch } from "domain-task";

export enum HttpMethod {
    GET = "GET",
    POST = "POST",
    PUT = "PUT",
}

export class BaseService {
    private static token: string | null;

    public static setToken(token: string){
        this.token = token;
    }

    public static fetch(method: string, url: string, data?: any) {
        return fetch(url, {
            method,
            headers: {
                "Authorization": "Bearer " + this.token,
                "Content-Type": "application/json; charset=utf-8",
            },
            body: JSON.stringify(data),
        });
    }


}