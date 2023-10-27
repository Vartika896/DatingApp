import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
model:any= {}

constructor(
    public accService: AccountService, 
    private router: Router,
    private tostr: ToastrService){}

login()
{
  console.log(this.model);
  this.accService.login(this.model)
    .subscribe({
      next: ()=>
        this.router.navigateByUrl('/members'),
      error: error=> 
            this.tostr.error(error.error)
    })
}
logout()
{
  this.router.navigateByUrl('/');
  this.accService.logout();

}

}
