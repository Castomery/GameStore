import { Component, inject, signal } from '@angular/core';
import { GenreService } from '../../../../core/services/genre.service';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { CreateGenreDto } from '../../../../shared/models/genre.model';
import { Genre } from '../../../../shared/models/genre.model'; 

@Component({
  selector: 'app-genre-form',
  imports: [ReactiveFormsModule],
  templateUrl: './genre-form.html',
  styleUrl: './genre-form.scss',
})
export class GenreForm {
  private genreService = inject(GenreService);

   genres = signal<Genre[]>([]);

    constructor() {
        this.loadGenres();
    }

    loadGenres() {
        this.genreService.getAllGenres().subscribe({
            next: (genres) => this.genres.set(genres)
        });
    }

  genreForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(2)]),
  });

  onSubmit(){
    if (this.genreForm.valid) {
        this.genreService.createGenre(this.genreForm.value as CreateGenreDto).subscribe({
            next: (genre) => {
                console.log('Genre created:', genre);
                this.genreForm.reset();
                this.loadGenres();
            },
            error: (err) => console.error(err)
        });
    }
  }

  onDeleteGenre(id: number) {
        this.genreService.deleteGenre(id).subscribe({
            next: () => this.genres.update(g => g.filter(x => x.id !== id))
        });
    }
}
