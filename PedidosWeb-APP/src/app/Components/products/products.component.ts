import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Category } from 'src/app/Core/models/Category';
import { Product } from 'src/app/Core/models/Product';
import { CategoryService } from 'src/app/Core/services/category.service';
import { ProductService } from 'src/app/Core/services/product.service';
import { ToastService } from 'src/app/Core/services/toast.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  categories: Category[] = [];
  productInsert: Product = new Product();
  productEdit: Product = new Product();
  productDelete: Product = new Product();
  pagedProducts: Product[];
  page = 1;
  pageSize = 10;
  collectionSize = 0;
  formCreate: FormGroup;
  savingProduct = false;
  message = '';
  showMessage = false;
  typeMessage = 'success';

  constructor(private productService: ProductService,
      private categoryService: CategoryService, 
      private modalService: NgbModal,
      public toastService: ToastService) { }

  ngOnInit(): void {
    this.formCreate = new FormGroup({
      name: new FormControl('', [Validators.required]),
      price: new FormControl('', [Validators.required]),
      categoryId: new FormControl('', [Validators.required])
    })
    this.getProducts();
    this.getCategories();
  }

  getProducts(){
    this.productService.getProducts()
      .then(res => {
        this.products = res;
        this.collectionSize = res.length;
        this.refresh();
      })
      .catch(err => {
        console.log(err);
      })
      .finally(()=>{
        
      });
  }
  getCategories(){
    this.categoryService.getCategories()
      .then(res => {
        this.categories = res;
      })
      .catch(err => {
        console.log(err);
      })
      .finally(()=>{
        
      });
  }

  refresh() {
    this.pagedProducts = this.products
      .map((product, i) => ({id: i + 1, ...product}))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  openModal(modal){
    this.modalService.open(modal);
  }

  saveProduct(modal){
    this.savingProduct = true;
    this.productService.createProduct(this.productInsert)
      .then(res => {
        this.products.push(res);
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
        this.savingProduct = false;
        this.refresh();
      })
  }

  editProduct(modal, product){
    this.modalService.open(modal);
    var productEdit = new Product();
    productEdit.productId = product.productId;
    productEdit.name = product.name;
    productEdit.price = product.price;
    productEdit.categoryId = product.categoryId;
    this.productEdit = productEdit;
  }

  saveEditProduct(modal){
    this.savingProduct = true;
    this.productService.updateProduct(this.productEdit.productId, this.productEdit)
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
        this.savingProduct = false;
        this.getProducts();
      })
  }

  deleteProduct(modal, product){
    this.modalService.open(modal);
    var productDelete = new Product();
    productDelete.productId = product.productId;
    productDelete.name = product.name;
    
    this.productDelete = productDelete;
  }

  confirmDeleteProduct(modal){
    this.savingProduct = true;
    this.productService.deleteProduct(this.productDelete.productId)
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
        this.savingProduct = false;
        this.getProducts();
      })
  }

}
