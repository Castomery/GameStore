import { Component } from '@angular/core';
import { Order } from '../../../../shared/models/order.model';
import { OrderService } from '../../services/order.service';
import { ActivatedRoute } from '@angular/router';
import { AsyncPipe, CurrencyPipe, DatePipe } from '@angular/common';
import { switchMap } from 'rxjs/operators';
import { inject } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-order-detail',
  imports: [CurrencyPipe, DatePipe, AsyncPipe],
  templateUrl: './order-detail.html',
  styleUrl: './order-detail.scss',
})
export class OrderDetail {

  private route = inject(ActivatedRoute);
  private orderService = inject(OrderService);

  order$ : Observable<Order> = this.route.paramMap.pipe(
    switchMap(params => this.orderService.getOrderById(Number(params.get('id'))))
  );
}
