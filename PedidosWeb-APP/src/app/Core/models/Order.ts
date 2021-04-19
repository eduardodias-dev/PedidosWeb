import { Client } from "./Client";
import { OrderItem } from "./OrderItem";

export class Order {
    orderId: number;
    clientId: number;
    client: Client;
    orderDate: Date;
    status: number;
    deliveryAddress: string;
    items: OrderItem[];
}