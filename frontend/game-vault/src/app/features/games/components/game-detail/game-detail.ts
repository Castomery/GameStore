import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GamesService } from '../../services/games.service';
import { OnInit } from '@angular/core';
import { Game } from '../../../../shared/models/game.model';
import { CurrencyPipe } from '@angular/common';
import { ReviewList } from '../../../reviews/components/review-list/review-list';
import { ReviewForm } from "../../../reviews/components/review-form/review-form";

@Component({
  selector: 'app-game-detail',
  imports: [CurrencyPipe, ReviewList, ReviewForm],
  templateUrl: './game-detail.html',
  styleUrl: './game-detail.scss',
})
export class GameDetail implements OnInit {

  game: Game | null = null;

  constructor(private gameService: GamesService, private route: ActivatedRoute) {}

  ngOnInit() {
    const gameId = this.route.snapshot.paramMap.get('id');
    if (gameId) {
      this.gameService.getGameById(Number(gameId)).subscribe({
        next: (game) => {
          this.game = game;
        },
        error: (err) => console.error(err),
      });
    } 
  }
}
