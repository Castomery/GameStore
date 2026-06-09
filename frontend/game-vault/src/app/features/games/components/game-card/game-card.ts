import { Component, Input } from '@angular/core';
import { Game } from '../../../../shared/models/game.model';
import { RouterLink } from '@angular/router';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-game-card',
  imports: [RouterLink, CurrencyPipe],
  templateUrl: './game-card.html',
  styleUrl: './game-card.scss',
})
export class GameCard {
  @Input() game!: Game;
}
