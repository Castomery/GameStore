import { Component, inject, Input } from '@angular/core';
import { FormGroup, ReactiveFormsModule, FormControl, Validators } from '@angular/forms';
import { GamesService } from '../../../games/services/games.service';
import { CreateGameDto, Game } from '../../../../shared/models/game.model';
import { GenreService } from '../../../../core/services/genre.service';
import { AsyncPipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-game-form',
  imports: [ReactiveFormsModule, AsyncPipe],
  templateUrl: './game-form.html',
  styleUrl: './game-form.scss',
})
export class GameForm {
  private gamesService = inject(GamesService);
  private genreService = inject(GenreService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  genres$ = this.genreService.getAllGenres();

  editGameId: number | null = null;
  previewUrl: string | null = null;

  constructor() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.editGameId = Number(id);
      this.gamesService.getGameById(Number(id)).subscribe({
        next: (game) => {
          this.gameForm.patchValue({
            title: game.title,
            description: game.description,
            price: game.price,
            releaseDate: new Date(game.releaseDate).toISOString().split('T')[0],
            coverImageUrl: game.coverImageUrl,
            genreId: game.genreId,
          });
        },
        error: (err) => console.error(err),
      });
    }

    this.gameForm.get('coverImageUrl')?.valueChanges.subscribe(url => {
        this.previewUrl = url;
    });
  }
  

  gameForm = new FormGroup({
    title: new FormControl('', [Validators.required, Validators.minLength(2)]),
    description: new FormControl('', [Validators.required]),
    price: new FormControl(0, [Validators.required, Validators.min(0)]),
    releaseDate: new FormControl('', [Validators.required]),
    coverImageUrl: new FormControl('', [Validators.required]),
    genreId: new FormControl(0, [Validators.required]),
  });

  onSubmit() {
    if (this.gameForm.valid) {
      const dto = this.gameForm.value as CreateGameDto;

      const request$ = this.editGameId
        ? this.gamesService.updateGame(this.editGameId, dto)
        : this.gamesService.createGame(dto);

      request$.subscribe({
        next: () => {
          this.gameForm.reset();
          this.router.navigate(['/admin/dashboard']);
        },
        error: (err) => console.error(err),
      });
    }
  }
}
