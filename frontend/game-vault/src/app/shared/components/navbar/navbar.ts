import { Component, inject } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class Navbar {
    private authService = inject(AuthService);
    private router = inject(Router);

    isLoggedIn = false;
    isAdmin = false;

    constructor() {
        this.authService.currentUser$$
            .pipe(takeUntilDestroyed())
            .subscribe(user => {
                this.isLoggedIn = user !== null;
                this.isAdmin = user?.role === 'Admin';
                console.log('User updated:', user?.role);
            });
    }

    logout() {
        this.authService.logout();
        this.router.navigate(['/auth/login']);
    }
}
