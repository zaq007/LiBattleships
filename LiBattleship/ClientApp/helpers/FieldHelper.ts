export type FieldCheckResult = {
    isValid: boolean,
    ships: any
}

export enum ShipState {
    Unknown = 0,
    Hitted = 16,
    Killed = 32,
}

export class FieldHelper {
    public static check(field: number[][]): FieldCheckResult {
        const ships : any = { '1': 0, '2':0, '3':0, '4':0 };
        for (let i = 0; i < field.length; i++) {
            for (let j = 0; j < field.length; j++) {
                if (field[i][j] > 0) {
                    ships[field[i][j]]++;
                }
            }
        }
        for (let ship in ships) {
            ships[ship] /= parseInt(ship, 10);
        }
        const isValid = ships['1'] === 4 && ships['2'] === 3 && ships['3'] === 2 && ships['4'] === 1;
        return { isValid, ships };
    }

    public static getEmptyField(): Array<Array<number>> {
        let temp = [];
        for (let i = 0; i < 10; i++) {
            temp.push([0, 0, 0, 0, 0, 0, 0, 0, 0, 0]);
        }
        return temp;
    }

    public static getShipSize(cell: number) {
        return cell & 7;
    }

    public static getCellState(cell: number): boolean {
        return (cell & 8) > 0;
    }

    public static getShipState(cell: number): ShipState {
        return cell & 48;
    }

    public static isShip(cell: number): boolean {
        return this.getShipSize(cell) > 0 || this.getShipState(cell) === ShipState.Hitted;
    }
}