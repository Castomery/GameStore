import { Routes } from '@angular/router';

export const GAMES_ROUTES: Routes = [
    {
        path: '',
        component: GameListComponent,
    },
    {
        path: ':id',
        component: GameDetailComponent,
    }
];
