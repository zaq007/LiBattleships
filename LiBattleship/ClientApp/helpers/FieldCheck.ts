export type FieldCheckResult = {
    isValid: boolean,
    ships: any
}


export class FieldChecker {
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
}