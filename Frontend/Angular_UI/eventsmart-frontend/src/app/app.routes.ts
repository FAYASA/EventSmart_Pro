import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login';
import { RegisterComponent } from './pages/register/register';
import { EventListComponent } from './pages/event-list/event-list';
import { EventDetailComponent } from './pages/event-detail/event-detail';
import { EventCreate } from './pages/event-create/event-create';
import { EventEditComponent} from './pages/event-edit/event-edit';
import { VenueCreateComponent } from './pages/venue-create/venue-create';
import { CategoryCreateComponent } from './pages/category-create/category-create';
import { EventRegistrationComponent } from './pages/event-register/event-register';


export const routes: Routes = [
  { path: '', component: EventListComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'events/new', component: EventCreate },
  { path: 'events', component: EventListComponent },
  { path: 'events/:id', component: EventDetailComponent },
  { path: 'events/edit/:id', component: EventEditComponent },
  { path: 'venue/new', component: VenueCreateComponent },
  { path: 'category/new', component: CategoryCreateComponent },
  { path: 'eventRegister/new', component: EventRegistrationComponent },
];