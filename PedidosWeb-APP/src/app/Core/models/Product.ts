import { Category } from "./Category";

export class Product {
    productId: number;
    name: string;
    price: number;
    categoryId: number;
    category: Category;
}