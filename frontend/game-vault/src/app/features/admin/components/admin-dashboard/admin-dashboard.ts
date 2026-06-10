import { Component, inject } from '@angular/core';
import { GamesService } from '../../../games/services/games.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-admin-dashboard',
  imports: [AsyncPipe],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.scss',
})
export class AdminDashboard {
  private gamesService = inject(GamesService);

  games$ = this.gamesService.getAllGames();

  onDeleteGame(gameId: number) {
    if (confirm('Are you sure you want to delete this game?')) {
      this.gamesService.deleteGame(gameId).subscribe({
        next: () => {
          console.log('Game deleted');
          this.games$ = this.gamesService.getAllGames();
        },
        error: (err) => console.error(err)
      });
    }
  }

  onEditGame(gameId: number) {
    // Implement edit logic, e.g., navigate to an edit form
    console.log('Edit game with ID:', gameId);
  }
}
