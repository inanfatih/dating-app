import { Routes, CanActivate } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolver/member-detail.resolver';
import { MemberListResolver } from './_resolver/member-list.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolver/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '', // buraya "semih" ekleseydim, children larin basina otomatik olarak semih eklerdi ve semihmembers olurdu mesela
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard], // Bunun sayesinde loginsiz olarak bu sayfalara girisi engelliyoruz
    children: [
      {
        path: 'members',
        component: MemberListComponent,
        resolve: { users: MemberListResolver }, // Bu resolver sayesinde datayi o component a gitmeden once cekmis oluyoruz
      },
      {
        path: 'members/:id',
        component: MemberDetailComponent,
        resolve: { user: MemberDetailResolver }, // Bu resolver sayesinde datayi o component a gitmeden once cekmis oluyoruz
      },
      {
        path: 'member/edit',
        component: MemberEditComponent,
        resolve: { user: MemberEditResolver }, // Bu resolver sayesinde datayi o component a gitmeden once cekmis oluyoruz
        canDeactivate: [PreventUnsavedChanges],
      },
      { path: 'messages', component: MessagesComponent },
      { path: 'lists', component: ListsComponent },
    ],
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
