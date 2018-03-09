export class Field {
    map: Array<Array<number>>;
    ships: any;

    constructor(map?: Array<Array<number>>) {
        this.ships = { '1': 0, '2': 0, '3': 0, '4': 0 };
        if (map) {
            this.map = map;
            this.recalcShips();
        } else {
            this.map = [];
            for (let i = 0; i < 10; i++) {
                this.map.push([0, 0, 0, 0, 0, 0, 0, 0, 0, 0]);
            }
        }
    }

    public makeAction(x: number, y: number): boolean {
        if (this.map[x][y] == 0 && !this.isValidMove(x, y)) return false;
        let nearestPoints = this.getNearestPoints(x, y);

        if (this.map[x][y] == 0) {
            nearestPoints.map((point) => this.map[x + point.dx][y + point.dy])
                .forEach((ship) => this.ships[ship]--);
            this.setPointWeight(x, y, 1);
            this.ships[this.map[x][y]]++;
        } else {
            this.ships[this.map[x][y]]--;
            this.setPointWeight(x, y, 0);
            this.getNearestPoints(x, y).map((point) => this.map[x + point.dx][y + point.dy])
                .forEach((ship) => this.ships[ship]++);
        }
        console.log(this.ships);
        return true;
    }

    public getState(x: number, y: number): boolean {
        return this.map[x][y] > 0;
    }

    private setPointWeight(x: number, y: number, weight: number) {
        this.map[x][y] = weight;
        if (this.map[x][y] > 0) this.setRegionWeight(x, y, this.getRegionWeight(x, y));
        else {
            this.getNearestPoints(x, y).forEach((point) => {
                if (point.dx != 0 || point.dy != 0) this.setRegionWeight(x + point.dx, y + point.dy, this.getRegionWeight(x + point.dx, y + point.dy));
            });
        }
    }

    private getRegionWeight(x: number, y: number): number {
        let sum = 0;
        let nearestPoints = this.getNearestPoints(x, y);
        nearestPoints.forEach((point) => {
            let i = x, j = y;
            while (true) {
                sum++;
                if (i + point.dx < this.map.length && i + point.dx >= 0
                    && j + point.dy < this.map.length && j + point.dy >= 0 && this.map[i + point.dx][j + point.dy] > 0) {
                    i = i + point.dx;
                    j = j + point.dy;
                }
                else break;
                if (point.dx == 0 && point.dy == 0) break;
            }
        })

        return sum - nearestPoints.length + 1;
    }

    private setRegionWeight(x: number, y: number, weight: number) {
        this.getNearestPoints(x, y).forEach((point) => {
            let i = x, j = y;
            while (true) {
                this.map[i][j] = weight;
                if (i + point.dx < this.map.length && i + point.dx >= 0 && j + point.dy < this.map.length
                    && j + point.dy >= 0 && this.map[i + point.dx][j + point.dy] > 0) {
                    i = i + point.dx;
                    j = j + point.dy;
                }
                else break;
                if (point.dx == 0 && point.dy == 0) break;
            }
        })
    }

    private getNearestPoints(x: number, y: number): Array<{ dx: number, dy: number }> {
        let result = [];
        if (this.map[x][y] > 0) { result.push({ dx: 0, dy: 0 }) }
        if (x > 0 && this.map[x - 1][y] > 0) { result.push({ dx: -1, dy: 0 }) }
        if (y < this.map.length - 1 && this.map[x][y + 1] > 0) { result.push({ dx: 0, dy: 1 }) }
        if (y > 0 && this.map[x][y - 1] > 0) { result.push({ dx: 0, dy: -1 }) }
        if (x < this.map.length - 1 && this.map[x + 1][y] > 0) { result.push({ dx: 1, dy: 0 }) }
        return result;
    }

    private isValidMove(i: number, j: number): boolean {
        let result = true;
        if (i > 0 && j > 0 && this.map[i - 1][j - 1]) return false;
        if (i > 0 && j < this.map.length - 1 && this.map[i - 1][j + 1]) return false;
        if (j > 0 && i < this.map.length - 1 && this.map[i + 1][j - 1]) return false;
        if (j < this.map.length - 1 && i < this.map.length - 1 && this.map[i + 1][j + 1]) return false;
        let probableShip = this.getNearestPoints(i, j).map((point) => this.map[i + point.dx][j + point.dy]).reduce((prev, curr, i, array) => curr + prev, 0) + 1;
        if (probableShip > 4) return false;
        if (this.ships[probableShip] + 1 + probableShip > 5) return false;
        return true;
    } 

    private recalcShips() {
        this.ships = { '1': 0, '2': 0, '3': 0, '4': 0 };
        for (let i = 0; i < this.map.length; i++) {
            for (let j = 0; j < this.map.length; j++) {
                if (this.map[i][j]) {
                    this.ships[this.map[i][j]]++;
                }
            }
        }
    }
}