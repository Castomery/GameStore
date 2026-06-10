import { Component, inject } from '@angular/core';
import { GenreService } from '../../../../core/services/genre.service';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { CreateGenreDto } from '../../../../shared/models/genre.model';

@Component({
  selector: 'app-genre-form',
  imports: [ReactiveFormsModule],
  templateUrl: './genre-form.html',
  styleUrl: './genre-form.scss',
})
export class GenreForm {
  private genreService = inject(GenreService);

  genreForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(2)]),
  });

  onSubmit(){
    if (this.genreForm.valid) {
        this.genreService.createGenre(this.genreForm.value as CreateGenreDto).subscribe({
            next: (genre) => {
                console.log('Genre created:', genre);
                this.genreForm.reset();
            },
            error: (err) => console.error(err)
        });
    }
  }
}
