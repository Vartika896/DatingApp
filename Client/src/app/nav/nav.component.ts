import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
model:any= {}

constructor(public accService: AccountService){}

login()
{
  console.log(this.model);
  this.accService.login(this.model)
    .subscribe({
      next: res=> {
        console.log(res)
      },
      error: error=> console.log(error)
    })
}
logout()
{
  this.accService.logout();

}

}
