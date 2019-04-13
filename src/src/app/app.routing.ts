import { ModuleWithProviders }  from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent }  from './home/home.component';
import { DetailComponent } from './detail/detail.component';

const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'detail', component: DetailComponent},
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);