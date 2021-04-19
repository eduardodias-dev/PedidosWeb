import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Category } from 'src/app/Core/models/Category';
import { CategoryService } from 'src/app/Core/services/category.service';
import { ToastService } from 'src/app/Core/services/toast.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  categorys: Category[] = [];
  categoryInsert: Category = new Category();
  categoryEdit: Category = new Category();
  categoryDelete: Category = new Category();
  pagedCategories: Category[];
  page = 1;
  pageSize = 10;
  collectionSize = 0;
  formCreate: FormGroup;
  savingCategory = false;
  message = '';
  showMessage = false;
  typeMessage = 'success';

  constructor(private categoryService: CategoryService, 
      private modalService: NgbModal,
      public toastService: ToastService) { }

  ngOnInit(): void {
    this.formCreate = new FormGroup({
      name: new FormControl('', [Validators.required])
    })
    this.getCategories();
    
  }

  getCategories(){
    this.categoryService.getCategories()
      .then(res => {
        this.categorys = res;
        this.collectionSize = res.length;
        this.refresh();
      })
      .catch(err => {
        console.log(err);
      })
      .finally(()=>{
        
      });
  }

  refresh() {
    this.pagedCategories = this.categorys
      .map((category, i) => ({id: i + 1, ...category}))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  openModal(modal){
    this.modalService.open(modal);
  }

  saveCategory(modal){
    this.savingCategory = true;
    this.categoryService.createCategory(this.categoryInsert)
      .then(res => {
        this.categorys.push(res);
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
        this.savingCategory = false;
        this.refresh();
      })
  }

  editCategory(modal, category){
    this.modalService.open(modal);
    var categoryEdit = new Category();
    categoryEdit.categoryId = category.categoryId;
    categoryEdit.name = category.name;
    this.categoryEdit = categoryEdit;
  }

  saveEditCategory(modal){
    this.savingCategory = true;
    this.categoryService.updateCategory(this.categoryEdit.categoryId, this.categoryEdit)
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
        this.savingCategory = false;
        this.getCategories();
      })
  }

  deleteCategory(modal, category){
    this.modalService.open(modal);
    var categoryDelete = new Category();
    categoryDelete.categoryId = category.categoryId;
    categoryDelete.name = category.name;
    
    this.categoryDelete = categoryDelete;
  }

  confirmDeleteCategory(modal){
    this.savingCategory = true;
    this.categoryService.deleteCategory(this.categoryDelete.categoryId)
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
        this.savingCategory = false;
        this.getCategories();
      })
  }


}
