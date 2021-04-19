import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Client } from 'src/app/Core/models/Client';
import { Order } from 'src/app/Core/models/Order';
import { OrderItem } from 'src/app/Core/models/OrderItem';
import { OrderStatus } from 'src/app/Core/models/OrderStatus';
import { Product } from 'src/app/Core/models/Product';
import { ClientService } from 'src/app/Core/services/client.service';
import { OrderService } from 'src/app/Core/services/order.service';
import { ProductService } from 'src/app/Core/services/product.service';
import { ToastService } from 'src/app/Core/services/toast.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  orders: Order[] = [];
  products: Product[] = [];
  newItem: OrderItem = new OrderItem();
  clients: Client[] = [];
  orderInsert: Order = new Order();
  orderEdit: Order = new Order();
  orderDelete: Order = new Order();
  pagedOrders: Order[];
  page = 1;
  pageSize = 10;
  collectionSize = 0;
  formCreate: FormGroup;
  savingOrder = false;
  message = '';
  showMessage = false;
  showItemForm = false;
  typeMessage = 'success';
  orderStatus = OrderStatus;

  constructor(private orderService: OrderService,
      private productService: ProductService, 
      private clientService: ClientService, 
      private modalService: NgbModal,
      public toastService: ToastService) { }

  ngOnInit(): void {
    this.formCreate = new FormGroup({
      clientId: new FormControl('', [Validators.required]),
      deliveryAddress: new FormControl('', [Validators.required]),
      orderDate: new FormControl('', [Validators.required]),
      status: new FormControl('', [Validators.required])
    })
    this.getOrders();
    this.getProducts();
    this.getClients();
  }

  getOrders(){
    this.orderService.getOrders()
      .then(res => {
        this.orders = res;
        this.collectionSize = res.length;
        this.refresh();
      })
      .catch(err => {
        console.log(err);
      })
      .finally(()=>{
        
      });
  }
  getProducts(){
    this.productService.getProducts()
      .then(res => {
        this.products = res;
      })
      .catch(err => {
        console.log(err);
      })
      .finally(()=>{
        
      });
  }
  getClients(){
    this.clientService.getClients()
      .then(res => {
        this.clients = res;
      })
      .catch(err => {
        console.log(err);
      })
      .finally(()=>{
        
      });
  }

  refresh() {
    this.pagedOrders = this.orders
      .map((order, i) => ({id: i + 1, ...order}))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  openModal(modal){
    this.modalService.open(modal);
    this.orderInsert = new Order();
    this.orderInsert.orderDate = new Date();
  }

  saveOrder(modal){
    this.savingOrder = true;
    this.orderService.createOrder(this.orderInsert)
      .then(res => {
        this.orders.push(res);
        this.typeMessage = 'success';
        this.message = "Inserido com sucesso!";
        this.showMessage = true;
        modal.close();
      })
      .catch(err => {
        this.typeMessage = 'danger';
        this.message = err.error;
        this.showMessage = true;
      })
      .finally(()=>{
        this.savingOrder = false;
        this.refresh();
      })
  }

  editOrder(modal, order){
    this.modalService.open(modal, {size: 'lg'});
    var orderEdit = new Order();
    orderEdit.orderId = order.orderId;
    orderEdit.clientId = order.clientId;
    orderEdit.client = order.client;
    orderEdit.orderDate = order.orderDate;
    orderEdit.status = order.status;
    orderEdit.deliveryAddress = order.deliveryAddress;
    orderEdit.items = order.items;
    this.orderEdit = orderEdit;
  }

  saveEditOrder(modal){
    this.savingOrder = true;
    this.orderService.updateOrder(this.orderEdit.orderId, this.orderEdit)
      .then(res => {
        this.typeMessage = 'success';
        this.message = "Atualizado com sucesso!";
        this.showMessage = true;
        modal.close();
      })
      .catch(err => {
        this.typeMessage = 'danger';
        this.message = err.error;
        this.showMessage = true;
      })
      .finally(()=>{
        this.savingOrder = false;
        this.getOrders();
      })
  }

  deleteOrder(modal, order){
    this.modalService.open(modal);
    var orderDelete = new Order();
    orderDelete.orderId = order.orderId;
    orderDelete.clientId = order.clientId;
    orderDelete.client = order.client;
    orderDelete.orderDate = order.orderDate;
    orderDelete.status = order.status;
    orderDelete.deliveryAddress = order.deliveryAddress;
    orderDelete.items = order.items;
    
    this.orderDelete = orderDelete;
  }

  confirmDeleteOrder(modal){
    this.savingOrder = true;
    this.orderService.deleteOrder(this.orderDelete.orderId)
      .then(res => {
        this.typeMessage = 'success';
        this.message = "Excluido com sucesso!";
        this.showMessage = true;
        modal.close();
      })
      .catch(err => {
        this.typeMessage = 'danger';
        this.message = err.error;
        this.showMessage = true;
      })
      .finally(()=>{
        this.savingOrder = false;
        this.getOrders();
      })
  }

  updateDeliveryAddress(eventClientId, order: Order){
    var client =  this.clients.find(x => x.clientId == order.clientId);

    if(client != null && client != undefined){
      order.deliveryAddress = client.address;
    }
  }

  addItem(order: Order){
    this.newItem = new OrderItem();
    this.showItemForm = true;
  }

  insertToOrder(order: Order){
    this.newItem.orderId = this.orderEdit.orderId;
    order.items.push(this.newItem);
    this.showItemForm = false;
  }

  removeItem(item: OrderItem, order: Order){
    order.items = order.items.filter(x => x.productId != item.productId);
  }
  updateItem(item: OrderItem){
    var index = this.orderEdit.items.indexOf(item);
  }

  get filterProductsEdit(){
    return this.products.filter( x => this.orderEdit.items.find(y => y.productId == x.productId ) == null);
  }

  getTotal(order: Order){
    var total = order.items.reduce((tot,item) => {
      var product = this.products.find(x => x.productId == item.productId);
      if(product != null) return tot + (product.price * item.quantity);
      else return tot;
    }, 0)

    return total.toLocaleString();
  }

}
