import { BaseService, HttpMethod } from './BaseService';

export class GameService {

    public static CreateGame(field: number[][]) {
        return BaseService.fetch(HttpMethod.POST, '/api/Game/', field);
    }

    public static JoinGame(gameId: string, field: number[][]) {
        return BaseService.fetch(HttpMethod.POST, `/api/Game/${gameId}/join`, field).then((data) => data.json());
    }

    public static GetAvailableGames() {
        return BaseService.fetch(HttpMethod.GET, '/api/Game/').then( (data) => data.json() );
    }

    public static MakeMove(gameId: string, x: number, y :number) {
        return BaseService.fetch(HttpMethod.POST, `/api/Game/${gameId}/move/${x}/${y}`).then((data) => data.json());
    }
}