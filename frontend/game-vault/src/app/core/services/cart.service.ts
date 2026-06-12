import { computed, Injectable, signal } from '@angular/core';
import { Game } from '../../shared/models/game.model';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private items = signal<Game[]>([]);

  cartItems = this.items.asReadonly();
  cartCount = computed(() => this.items().length);

  constructor() {
    this.loadFromLocalStorage();
  }
  addToCart(game: Game) {
    const exists = this.items().find((i) => i.id === game.id);
    if (!exists) {
      this.items.update((items) => [...items, game]);
      this.saveToLocalStorage();
    }
  }

  removeFromCart(gameId: number) {
    this.items.update((items) => items.filter((i) => i.id !== gameId));
    this.saveToLocalStorage();
  }

  clearCart() {
    this.items.set([]);
    localStorage.removeItem('cart');
  }

  private loadFromLocalStorage() {
    const saved = localStorage.getItem('cart');
    if (saved) {
      this.items.set(JSON.parse(saved)); // ✅
    }
  }

  private saveToLocalStorage() {
    localStorage.setItem('cart', JSON.stringify(this.items()));
  }
}
