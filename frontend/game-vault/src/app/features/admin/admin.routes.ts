import { Routes } from '@angular/router';
import { AdminDashboard } from './components/admin-dashboard/admin-dashboard';
import { GenreForm } from './components/genre-form/genre-form';
import { adminGuard } from '../../core/guards/admin-guard';
import { GameForm } from './components/game-form/game-form';

export const ADMIN_ROUTES: Routes = [
    {
        path: 'dashboard',
        component: AdminDashboard,
    },
    {
        path: 'new',
        canActivate: [adminGuard],
        component: GameForm
    },

];