import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateOrderDto, Order } from '../../../shared/models/order.model';

@Injectable({
  providedIn: 'root',
})
export class OrderService {

    private apiUrl = `${environment.apiUrl}/orders`;

  constructor(private http: HttpClient) {}

  getAllUserOrders() : Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}`);
  }

  getOrderById(id: number) : Observable<Order>{
    return this.http.get<Order>(`${this.apiUrl}/${id}`);
  }

  createOrder(createOrderDto: CreateOrderDto) : Observable<Order> {
    return this.http.post<Order>(`${this.apiUrl}`, createOrderDto);
  }
}
