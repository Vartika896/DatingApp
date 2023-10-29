import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const accSer= inject(AccountService);
  const tostr= inject(ToastrService);

  return accSer.currentUser$.pipe(
    map(user=>{
      if(user) return true;
      else
      {
        tostr.error('You shall paas!');
        return false;
      }
    })
  )
};
