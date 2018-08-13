import { BaseService, HttpMethod } from './BaseService';

export class GameService {

    public static CreateGame(field: number[][]) {
        BaseService.fetch(HttpMethod.POST, '/api/Game/', field).then((val) => console.log(val));
    }
}