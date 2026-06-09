import { Routes } from '@angular/router';
import { GameList } from './components/game-list/game-list';
import { GameDetail } from './components/game-detail/game-detail';

export const GAMES_ROUTES: Routes = [
    {
        path: '',
        component: GameList,
    },
    {
        path: ':id',
        component: GameDetail,
    }
];
