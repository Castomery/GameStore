import { Component, inject } from '@angular/core';
import { Game } from '../../../../shared/models/game.model';
import { GamesService } from '../../services/games.service';
import { GameCard } from '../game-card/game-card';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-game-list',
  imports: [GameCard, AsyncPipe],
  templateUrl: './game-list.html',
  styleUrl: './game-list.scss',
})
export class GameList {
  
  private gamesService = inject(GamesService);
  games$ : Observable<Game[]> = this.gamesService.getAllGames();
}
