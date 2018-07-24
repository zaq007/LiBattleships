import { BaseService, HttpMethod } from './BaseService';

export class AuthService {
    private static tokenPromise?: Promise<string>;

    public static getToken(){
        const token = localStorage.getItem("authToken");
        if(token) this.tokenPromise = Promise.resolve(token);
        if(!this.tokenPromise) this.tokenPromise = this.Auth().then((data: Response) => {
            return data.json();
        });
        return this.tokenPromise; 
    }

    public static Auth(username?: string, password?: string) {
        if(username && password) {
            return this.AuthAsUser(username, password)
        } else {
            return this.AuthAsGuest();
        }
    }

    private static AuthAsGuest(){
        return BaseService.fetch(HttpMethod.GET, '/api/Account/Guest');
    }

    private static AuthAsUser(username: string, password: string){
        return BaseService.fetch(HttpMethod.GET, '/api/Account/Guest');
    }
}