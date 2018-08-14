import { BaseService, HttpMethod } from './BaseService';

export class GameService {

    public static CreateGame(field: number[][]) {
        return BaseService.fetch(HttpMethod.POST, '/api/Game/', field);
    }
}