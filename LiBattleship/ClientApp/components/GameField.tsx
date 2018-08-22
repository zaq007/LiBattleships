import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as LoginStore from '../store/Login';
import { ApplicationState } from 'ClientApp/store';
import { connect } from 'react-redux';
import GameCell from './GameCell';
import * as FieldHelper from '../helpers/FieldHelper';
//type LoginProps =
//    LoginStore.LoginState
//    & typeof LoginStore.actionCreators
//    & RouteComponentProps<{}>;

type GameFieldProps = {
    field: Array<Array<number>>;
    readonly?: boolean;
    onClick?: Function;
}

export default class GameField extends React.Component<GameFieldProps, {}> {
    ships: any;

    constructor(props: GameFieldProps) {
        super(props);
        this.ships = { '1': 0, '2': 0, '3': 0, '4': 0 };
    }

    public render() {
        var cells = [];
        for (let i = 0; i < 100; i++) {
            cells.push(<GameCell x={i % 10} y={Math.trunc(i / 10)} key={i} clickHandler={this.cellClickHandler.bind(this)} states={this.props.field} />);
        }

        return <div className="gameField">{cells}</div>;
    }

    cellClickHandler(x: number, y: number): boolean {
        if (!!this.props.readonly || this.props.field[x][y] == 0 && !this.isValidMove(x, y)) return false;
        let nearestPoints = this.getNearestPoints(x, y);

        if (this.props.field[x][y] == 0) {
            this.setPointWeight(x, y, 1);
        } else {
            this.setPointWeight(x, y, 0);
        }

        if (this.props.onClick) this.props.onClick(x, y);
        return true;
    }

    setPointWeight(x: number, y: number, weight: number) {
        this.props.field[x][y] = weight;
        if (this.props.field[x][y] > 0) this.setRegionWeight(x, y, this.getRegionWeight(x, y));
        else {
            this.getNearestPoints(x, y).forEach((point) => {
                if (point.dx != 0 || point.dy != 0) this.setRegionWeight(x + point.dx, y + point.dy, this.getRegionWeight(x + point.dx, y + point.dy));
            });
        }
    }

    getRegionWeight(x: number, y: number): number {
        let sum = 0;
        let nearestPoints = this.getNearestPoints(x, y);
        nearestPoints.forEach((point) => {
            let i = x, j = y;
            while (true) {
                sum++;
                if (i + point.dx < this.props.field.length && i + point.dx >= 0
                    && j + point.dy < this.props.field.length && j + point.dy >= 0 && this.props.field[i + point.dx][j + point.dy] > 0) {
                    i = i + point.dx;
                    j = j + point.dy;
                }
                else break;
                if (point.dx == 0 && point.dy == 0) break;
            }
        })

        return sum - nearestPoints.length + 1;
    }

    setRegionWeight(x: number, y: number, weight: number) {
        this.getNearestPoints(x, y).forEach((point) => {
            let i = x, j = y;
            while (true) {
                this.props.field[i][j] = weight;
                if (i + point.dx < this.props.field.length && i + point.dx >= 0 && j + point.dy < this.props.field.length
                    && j + point.dy >= 0 && this.props.field[i + point.dx][j + point.dy] > 0) {
                    i = i + point.dx;
                    j = j + point.dy;
                }
                else break;
                if (point.dx == 0 && point.dy == 0) break;
            }
        })
    }

    getNearestPoints(x: number, y: number): Array<{ dx: number, dy: number }> {
        let result = [];
        if (this.props.field[x][y] > 0) { result.push({ dx: 0, dy: 0 }) }
        if (x > 0 && this.props.field[x - 1][y] > 0) { result.push({ dx: -1, dy: 0 }) }
        if (y < this.props.field.length - 1 && this.props.field[x][y + 1] > 0) { result.push({ dx: 0, dy: 1 }) }
        if (y > 0 && this.props.field[x][y - 1] > 0) { result.push({ dx: 0, dy: -1 }) }
        if (x < this.props.field.length - 1 && this.props.field[x + 1][y] > 0) { result.push({ dx: 1, dy: 0 }) }
        return result;
    }

    isValidMove(i: number, j: number): boolean {
        let result = true;
        if (FieldHelper.FieldHelper.getCellState(this.props.field[i][j])) return false;
        if (i > 0 && j > 0 && FieldHelper.FieldHelper.isShip(this.props.field[i - 1][j - 1])) return false;
        if (i > 0 && j < this.props.field.length - 1 && FieldHelper.FieldHelper.isShip(this.props.field[i - 1][j + 1])) return false;
        if (j > 0 && i < this.props.field.length - 1 && FieldHelper.FieldHelper.isShip(this.props.field[i + 1][j - 1])) return false;
        if (j < this.props.field.length - 1 && i < this.props.field.length - 1 && FieldHelper.FieldHelper.isShip(this.props.field[i + 1][j + 1])) return false;
        let probableShip = this.getNearestPoints(i, j)
            .map((point) => FieldHelper.FieldHelper.getShipSize(this.props.field[i + point.dx][j + point.dy]))
            .reduce((prev, curr, i, array) => curr + prev, 0) + 1;
        if (probableShip > 4) return false;
        //if (this.ships[probableShip] + 1 + probableShip > 5) return false;
        return true;
    }


}

//export default connect(
//    (state: ApplicationState) => state.login, // Selects which state properties are merged into the component's props
//    LoginStore.actionCreators                 // Selects which action creators are merged into the component's props
//)(Login);
