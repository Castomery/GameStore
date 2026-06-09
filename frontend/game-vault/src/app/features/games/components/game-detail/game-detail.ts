import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GamesService } from '../../services/games.service';
import { Game } from '../../../../shared/models/game.model';
import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { ReviewList } from '../../../reviews/components/review-list/review-list';
import { ReviewForm } from "../../../reviews/components/review-form/review-form";
import { Observable, switchMap } from 'rxjs';
import { inject } from '@angular/core';

@Component({
  selector: 'app-game-detail',
  imports: [CurrencyPipe, ReviewList, ReviewForm, AsyncPipe],
  templateUrl: './game-detail.html',
  styleUrl: './game-detail.scss',
})
export class GameDetail {

  private gameService = inject(GamesService);
  private route = inject(ActivatedRoute);

  game$ : Observable<Game> = this.route.paramMap.pipe(
    switchMap(params => this.gameService.getGameById(Number(params.get('id'))))
  );

}
