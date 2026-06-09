import { Component } from '@angular/core';
import { Game } from '../../../../shared/models/game.model';
import { GamesService } from '../../services/games.service';
import { OnInit } from '@angular/core';
import { GameCard } from '../game-card/game-card';

@Component({
  selector: 'app-game-list',
  imports: [GameCard],
  templateUrl: './game-list.html',
  styleUrl: './game-list.scss',
})
export class GameList implements OnInit {
  games: Game[] = [];

  constructor(private gamesService: GamesService) {}

  ngOnInit() {
    this.gamesService.getAllGames().subscribe({
      next: (games) => (this.games = games),
      error: (err) => console.error(err),
    });
  }
}
