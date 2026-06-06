export interface Order {
    id: number;
    createdAt: Date;
    totalPrice: number;
    items: OrderItem[];
}

export interface OrderItem {
    gameId: number;
    gameTitle: string;
    coverImageUrl: string;
    price: number;
}

export interface CreateOrderDto {
    gameIds: number[];
}