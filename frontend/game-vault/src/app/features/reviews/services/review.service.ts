import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CreateReviewDto, Review, UpdateReviewDto } from '../../../shared/models/review.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {

  private apiUrl = `${environment.apiUrl}/reviews`;

  constructor(private http: HttpClient) {}

  getReviewsByGameId(gameId: number) : Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiUrl}/game/${gameId}`);
  }

  createReview(gameId: number, review: CreateReviewDto) : Observable<Review> {
    return this.http.post<Review>(`${this.apiUrl}/game/${gameId}`, review);
  }

  updateReview(reviewId: number, review: UpdateReviewDto) : Observable<Review> {
    return this.http.put<Review>(`${this.apiUrl}/${reviewId}`, review);
  }

  deleteReview(reviewId: number) {
    return this.http.delete(`${this.apiUrl}/${reviewId}`);
  }
}
