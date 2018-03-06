export type FieldCheckResult = {
    isValid: boolean,
    ships: any
}


export class FieldChecker {
    private field: Array<Array<number>>;

    constructor(field: Array<Array<number>>) {
        this.field = field;
    }

    public check(): FieldCheckResult {
        
        let ships : any = { '1': 0, '2':0, '3':0, '4':0 };
        let isValid = true;
        //let shipsNumber = 0;
        //for (let i = 0; i < this.field.length; i++) {
        //    for (let j = 0; j < this.field.length; j++) {
        //        if (!temp[i][j] && this.field[i][j]) {
        //            temp[i][j] = true;
        //            ships[this.field[i][j]]++;
        //            if (isValid && i > 0 && j > 0 && this.field[i - 1][j - 1]) isValid = false;
        //            if (isValid && i > 0 && j < this.field.length - 1 && this.field[i - 1][j + 1]) isValid = false;
        //            if (isValid && j > 0 && i < this.field.length - 1 && this.field[i + 1][j - 1]) isValid = false;
        //            if (isValid && j < this.field.length - 1 && i < this.field.length - 1 && this.field[i + 1][j + 1]) isValid = false;
        //        }
        //    }
        //}
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