import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { ReviewService } from '../../services/review.service';

@Component({
  selector: 'app-review-form',
  imports: [ReactiveFormsModule],
  templateUrl: './review-form.html',
  styleUrl: './review-form.scss',
})
export class ReviewForm {

  @Input() gameId!: number;

  reviewForm = new FormGroup({
    rating: new FormControl(0, [Validators.required, Validators.min(1), Validators.max(5)]),
    text: new FormControl('', [Validators.required, Validators.minLength(10)]),
  });

  constructor(private reviewService: ReviewService) {}

  onSubmit() {
    if (this.reviewForm.valid) {
        const { rating, text } = this.reviewForm.value;
        this.reviewService.createReview(this.gameId, {
            rating: rating!,
            text: text!
        }).subscribe({
            next: () => {
                this.reviewForm.reset();
                this.reviewService.reviewAdded$.next();
            },
            error: (err) => console.error(err)
        });
    }
}
}
