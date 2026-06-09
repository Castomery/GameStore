import { Component } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { AsyncPipe, CurrencyPipe, DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Order } from '../../../../shared/models/order.model';

@Component({
  selector: 'app-order-list',
  imports: [CurrencyPipe, DatePipe, RouterLink, AsyncPipe],
  templateUrl: './order-list.html',
  styleUrl: './order-list.scss',
})
export class OrderList {
  
  private orderService = inject(OrderService);
  orders$ : Observable<Order[]> = this.orderService.getAllUserOrders();

}
