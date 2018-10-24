import { BaseService, HttpMethod } from './BaseService';

export class GameService {

    public static CreateGame(field: number[][]) {
        return BaseService.fetch(HttpMethod.POST, '/api/Game/', field);
    }

    public static JoinGame(gameId: string, field: number[][]) {
        return BaseService.fetch(HttpMethod.POST, `/api/Game/${gameId}/join`, field);
    }

    public static GetAvailableGames() {
        return BaseService.fetch(HttpMethod.GET, '/api/Game/');
    }

    public static MakeMove(gameId: string, x: number, y :number) {
        return BaseService.fetch(HttpMethod.POST, `/api/Game/${gameId}/move/${x}/${y}`);
    }
}