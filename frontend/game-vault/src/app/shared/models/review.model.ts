export interface Review{
    id: number;
    text: string;
    rating: number;
    createdAt: Date;
    userName: string;
}

export interface CreateReviewDto {
    text: string;
    rating: number;
}

export interface UpdateReviewDto {
    text: string;
    rating: number;
}