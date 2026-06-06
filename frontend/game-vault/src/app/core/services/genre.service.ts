import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CreateGenreDto, Genre } from '../../shared/models/genre.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GenreService {

  private apiUrl = `${environment.apiUrl}/genres`;

  constructor(private http: HttpClient) {}

  getAllGenres() : Observable<Genre[]> {
    return this.http.get<Genre[]>(`${this.apiUrl}`);
  }

  createGenre(createGenreDto: CreateGenreDto) : Observable<Genre> {
    return this.http.post<Genre>(`${this.apiUrl}`, createGenreDto);
  }

  deleteGenre(id: number) : Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
