import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateGameDto, Game, UpdateGameDto } from '../../../shared/models/game.model';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  private apiUrl = `${environment.apiUrl}/games`;

  constructor(private http: HttpClient) {}

  getAllGames() : Observable<Game[]> {
    return this.http.get<Game[]>(this.apiUrl);
  }

  getGameById(id: number) : Observable<Game> {
    return this.http.get<Game>(`${this.apiUrl}/${id}`);
  }

  createGame(game: CreateGameDto) : Observable<Game> {
    return this.http.post<Game>(`${this.apiUrl}`, game);
  }

  updateGame(id: number, game: UpdateGameDto) : Observable<Game> {
    return this.http.put<Game>(`${this.apiUrl}/${id}`, game);
  }

  deleteGame(id: number) : Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getGamesByGenre(genreId: number) : Observable<Game[]> {
    return this.http.get<Game[]>(`${this.apiUrl}/genre/${genreId}`);
  }

  searchGames(title: string) : Observable<Game[]> {
    return this.http.get<Game[]>(`${this.apiUrl}/search?title=${title}`);
  }
}
