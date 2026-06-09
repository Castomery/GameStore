import { Component } from '@angular/core';
import { ReactiveFormsModule, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class RegisterComponent {

  errorMessage = '';

  registerForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    if (this.registerForm.valid) {
      const { userName, email, password } = this.registerForm.value;
      this.authService.register({ userName: userName!, email: email!, password: password! }).subscribe({
        next: () => {
          this.router.navigate(['/games']);
        },
        error: (err) => {
          this.errorMessage = 'Registration failed. Please try again.';
        }
      });
    }
  }
}
