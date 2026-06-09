import { Component } from '@angular/core';
import { UserProfile } from '../../../../shared/models/user.model';
import { UserService } from '../../../../core/services/user.service';
import { OnInit } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { Observable } from 'rxjs';
import { inject } from '@angular/core';

@Component({
  selector: 'app-profile',
  imports: [AsyncPipe],
  templateUrl: './profile.html',
  styleUrl: './profile.scss',
})
export class Profile {

  private userService = inject(UserService);

  userProfile$: Observable<UserProfile> = this.userService.getUserProfile();

}
