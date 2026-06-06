import { Injectable } from '@angular/core';
import { AuthResponse, UserProfile } from '../../shared/models/user.model';
import { BehaviorSubject, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  currentUser$ = new BehaviorSubject<UserProfile | null>(null);
  public currentUser$$ = this.currentUser$.asObservable();
  private tokenKey = 'authToken';

  constructor(private http: HttpClient) {
    this.loadUserFromLocalStorage();
  }

  private loadUserFromLocalStorage() {
    const token = localStorage.getItem(this.tokenKey);
    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));

      const userProfile: UserProfile = {
        id: payload.nameid,
        userName: payload.unique_name,
        email: payload.email,
      };
      this.currentUser$.next(userProfile);
    }
  }

  login(loginDto: { email: string; password: string }) {
    return this.http.post<AuthResponse>(`${environment.apiUrl}/auth/login`, loginDto).pipe(
      tap((response) => {
        this.handleAuthResponse(response);
      }),
    );
  }

  register(registerDto: { userName: string; email: string; password: string }) {
    return this.http.post<AuthResponse>(`${environment.apiUrl}/auth/register`, registerDto).pipe(
      tap((response) => {
        this.handleAuthResponse(response);
      }),
    );
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.currentUser$.next(null);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  private handleAuthResponse(response: AuthResponse) {
    localStorage.setItem(this.tokenKey, response.token);
    const payload = JSON.parse(atob(response.token.split('.')[1]));
    this.currentUser$.next({
      id: payload.nameid,
      userName: response.userName,
      email: response.email,
    });
  }
}
