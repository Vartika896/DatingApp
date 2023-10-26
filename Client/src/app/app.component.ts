import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Client';
  users: any

  constructor(private http: HttpClient, private accService: AccountService){}

  ngOnInit(): void 
  {
    this.getUser();
    this.setCurrentUser();
  }

  getUser()
  {
    this.http.get('https://localhost:5000/api/users').subscribe(
      {
        next: response => this.users= response,
        error: error => console.log(error),
        complete: () => console.log("request completed")
      }
    )

  }

  setCurrentUser()
  {
    const setString = localStorage.getItem('user');
    if(!setString)
    return;
    const user: User= JSON.parse(setString);
    this.accService.setCurrntUser(user);
  }
}
