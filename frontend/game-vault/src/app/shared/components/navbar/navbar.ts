import { Component } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class Navbar implements OnInit {
  isLoggedIn = false;

  private subscription = new Subscription();

  constructor(private authService: AuthService,private router: Router,) {}

    ngOnInit() {
        this.subscription.add(
            this.authService.currentUser$$.subscribe(user => {
                console.log('Navbar user:', user);
                this.isLoggedIn = user !== null;
            })
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    logout() {
        this.authService.logout();
        this.router.navigate(['/auth/login']);
    }
}
