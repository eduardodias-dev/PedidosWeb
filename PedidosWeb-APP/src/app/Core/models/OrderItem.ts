import { Order } from "./Order";
import { Product } from "./Product";

export class OrderItem {
    orderItemId: number;
    orderId: number;
    order: Order;
    productId: number;
    product: Product;
    quantity: number;
}