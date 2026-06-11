import { Component, inject } from '@angular/core';
import { GamesService } from '../../../games/services/games.service';
import { AsyncPipe } from '@angular/common';
import { Game } from '../../../../shared/models/game.model';
import { BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  imports: [AsyncPipe],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.scss',
})

export class AdminDashboard {
  private gamesService = inject(GamesService);
  private router = inject(Router);

  selectedGame: Game | null = null;


   private refresh$ = new BehaviorSubject<void>(undefined);
    games$ = this.refresh$.pipe(
        switchMap(() => this.gamesService.getAllGames())
    );

    onDeleteGame(gameId: number) {
        if (confirm('Are you sure?')) {
            this.gamesService.deleteGame(gameId).subscribe({
                next: () => this.refresh$.next(),
                error: (err) => console.error(err)
            });
        }
    }

    onEditGame(game: Game) {
    this.router.navigate(['/admin/games', game.id, 'edit']);
    }
}
