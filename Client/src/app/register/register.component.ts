import { Component, EventEmitter, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  model: any={}
  @Output() cancelRegister= new EventEmitter();

  constructor(private accServ: AccountService)
  {

  }
  register()
  {
    this.accServ.register(this.model).subscribe({
      next: res=>{
        console.log(res)
        this.cancel();
      },
      error: error=> console.log(error)
      })
  }

  cancel()
  {
    this.cancelRegister.emit(false);
    console.log("cancelead...");
  }
}
