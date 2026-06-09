import { Component, Input, OnDestroy } from '@angular/core';
import { OnInit } from '@angular/core';
import { ReviewService } from '../../services/review.service';
import { Review } from '../../../../shared/models/review.model';
import { DatePipe } from '@angular/common';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-review-list',
  imports: [DatePipe],
  templateUrl: './review-list.html',
  styleUrl: './review-list.scss',
})
export class ReviewList implements OnInit, OnDestroy {
  @Input() gameId!: number;

  private subscription = new Subscription();

  reviews: Review[] = [];

  constructor(private reviewService: ReviewService) {}

  ngOnInit() {
    this.loadReviews();
    this.subscription.add(
      this.reviewService.reviewAdded$.subscribe(() => {
        this.loadReviews();
      }),
    );
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  loadReviews() {
    this.reviewService.getReviewsByGameId(this.gameId).subscribe({
      next: (reviews) => (this.reviews = reviews),
      error: (err) => console.error(err),
    });
  }
}
