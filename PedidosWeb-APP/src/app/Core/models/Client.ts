import { Order } from "./Order";

export class Client {
    clientId: number;
    name: string;
    address: string;
    orders: Order[];
}