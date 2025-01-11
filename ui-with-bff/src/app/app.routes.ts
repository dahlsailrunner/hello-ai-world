import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserClaimsComponent } from './user-claims/user-claims.component';
import { loggedInGuard } from './core/auth.guard';
import { AccessDeniedComponent } from './access-denied/access-denied.component';
import { ChatComponent } from './chat/chat.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'user-claims',
    component: UserClaimsComponent,
    canActivate: [loggedInGuard],
  },
  {
    path: 'chat',
    component: ChatComponent,
    canActivate: [loggedInGuard] 
  },
  {
    path: 'access-denied',
    component: AccessDeniedComponent,
    canActivate: [loggedInGuard],
  }
];
