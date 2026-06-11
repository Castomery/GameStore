export interface Game {
  id: number;
  title: string;
  description: string;
  price: number;
  releaseDate: Date;
  coverImageUrl: string;
  genreName: string;
  genreId: number;
}

export interface CreateGameDto {
  title: string;
  description: string;
  price: number;
  releaseDate: string;
  coverImageUrl: string;
  genreId: number;
}

export interface UpdateGameDto {
  title?: string;
  description?: string;
  price?: number;
  releaseDate?: string;
  coverImageUrl?: string;
  genreId?: number;
}