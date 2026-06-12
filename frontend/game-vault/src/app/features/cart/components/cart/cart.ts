import { Component, inject,computed } from '@angular/core';
import { CartService } from '../../../../core/services/cart.service';
import { OrderService } from '../../../orders/services/order.service';
import { Router, RouterLink } from '@angular/router';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-cart',
  imports: [CurrencyPipe, RouterLink],
  templateUrl: './cart.html',
  styleUrl: './cart.scss',
})
export class Cart {
  private cartService = inject(CartService);
  private orderService = inject(OrderService);
  private router = inject(Router);

  items = this.cartService.cartItems;

  totalPrice = computed(() => this.items().reduce((sum, game) => sum + game.price, 0));

  removeItem(gameId: number) {
    this.cartService.removeFromCart(gameId);
  }

  checkout() {
    const gameIds = this.items().map((game) => game.id);
    this.orderService.createOrder({ gameIds }).subscribe({
      next: () => {
        this.cartService.clearCart();
        this.router.navigate(['/orders']);
      },
      error: (err) => console.error(err),
    });
  }
}
