import { TemplateRef } from '@angular/core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  toasts: any[] = [];

  success(textOrTpl: string | TemplateRef<any>) {
    var options = { classname: 'bg-success text-light', delay: 10000 };
    this.toasts.push({ textOrTpl, ...options });
  }
  danger(textOrTpl: string | TemplateRef<any>) {
    var options = { classname: 'bg-danger text-light', delay: 10000 };
    this.toasts.push({ textOrTpl, ...options });
  }

  remove(toast) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }
}
