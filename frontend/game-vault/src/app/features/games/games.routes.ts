import { Routes } from '@angular/router';
import { GameList } from './components/game-list/game-list';
import { GameDetail } from './components/game-detail/game-detail';
import { GameForm } from '../admin/components/game-form/game-form';
import { adminGuard } from '../../core/guards/admin-guard';

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
