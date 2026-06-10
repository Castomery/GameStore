import { Component, inject } from '@angular/core';
import { FormGroup, ReactiveFormsModule, FormControl, Validators } from '@angular/forms';
import { GamesService } from '../../../games/services/games.service';
import { CreateGameDto } from '../../../../shared/models/game.model';
import { GenreService } from '../../../../core/services/genre.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-game-form',
  imports: [ReactiveFormsModule, AsyncPipe],
  templateUrl: './game-form.html',
  styleUrl: './game-form.scss',
})
export class GameForm {

  private gamesService = inject(GamesService);
  private genreService = inject(GenreService);
  genres$ = this.genreService.getAllGenres();


  gameForm = new FormGroup({
    title: new FormControl('', [Validators.required, Validators.minLength(2)]),
    description: new FormControl('', [Validators.required]),
    price: new FormControl(0, [Validators.required, Validators.min(0)]),
    releaseDate: new FormControl('', [Validators.required]),
    coverImageUrl: new FormControl('', [Validators.required]),
    genreId: new FormControl(0, [Validators.required]),
  });

  onSubmit(){
    if (this.gameForm.valid) {
        this.gamesService.createGame(this.gameForm.value as CreateGameDto).subscribe({
            next: (game) => {
                console.log('Game created:', game);
                this.gameForm.reset();
            },
            error: (err) => console.error(err)
        });
    }
  }
}
