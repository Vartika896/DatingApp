import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
model:any= {}
_username: any='test'

constructor(
    public accService: AccountService, 
    private router: Router,
    private tostr: ToastrService){}
    
login()
{
  this.accService.login(this.model)
    .subscribe({
      next: ()=>
        this.router.navigateByUrl('/members'),
    })

}
logout()
{
  this.router.navigateByUrl('/');
  this.accService.logout();

}

}
