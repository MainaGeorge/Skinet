import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {BusyService} from '../services/busy.service';
import {delay, finalize} from 'rxjs/operators';
import {Injectable} from '@angular/core';

@Injectable({providedIn: 'root'})
export class LoadingInterceptor implements HttpInterceptor{
  constructor(private busyService: BusyService) {
  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.busyService.busy();
    return  next.handle(req).pipe(delay(300), finalize(() => this.busyService.idle()))
  }

}
