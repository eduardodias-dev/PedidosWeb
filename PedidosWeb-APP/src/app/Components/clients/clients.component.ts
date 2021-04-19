import { Component, OnInit } from '@angular/core';
import { Client } from 'src/app/Core/models/Client';
import { ClientService } from 'src/app/Core/services/client.service';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastService } from 'src/app/Core/services/toast.service';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit {
  clients: Client[] = [];
  clientInsert: Client = new Client();
  clientEdit: Client = new Client();
  clientDelete: Client = new Client();
  pagedClients: Client[];
  page = 1;
  pageSize = 10;
  collectionSize = 0;
  formCreate: FormGroup;
  savingClient = false;
  message = '';
  showMessage = false;
  typeMessage = 'success';

  constructor(private clientService: ClientService, 
      private modalService: NgbModal,
      public toastService: ToastService) { }

  ngOnInit(): void {
    this.formCreate = new FormGroup({
      name: new FormControl('', [Validators.required]),
      address: new FormControl('', [Validators.required])
    })
    this.getClients();
    
  }

  getClients(){
    this.clientService.getClients()
      .then(res => {
        this.clients = res;
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
    this.pagedClients = this.clients
      .map((client, i) => ({id: i + 1, ...client}))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  openModal(modal){
    this.modalService.open(modal);
  }

  saveClient(modal){
    this.savingClient = true;
    this.clientService.createClient(this.clientInsert)
      .then(res => {
        this.clients.push(res);
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
        this.savingClient = false;
        this.refresh();
      })
  }

  editClient(modal, client){
    this.modalService.open(modal);
    var clientEdit = new Client();
    clientEdit.clientId = client.clientId;
    clientEdit.name = client.name;
    clientEdit.address = client.address
    this.clientEdit = clientEdit;
  }

  saveEditClient(modal){
    this.savingClient = true;
    this.clientService.updateClient(this.clientEdit.clientId, this.clientEdit)
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
        this.savingClient = false;
        this.getClients();
      })
  }

  deleteClient(modal, client){
    this.modalService.open(modal);
    var clientDelete = new Client();
    clientDelete.clientId = client.clientId;
    clientDelete.name = client.name;
    
    this.clientDelete = clientDelete;
  }

  confirmDeleteClient(modal){
    this.savingClient = true;
    this.clientService.deleteClient(this.clientDelete.clientId)
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
        this.savingClient = false;
        this.getClients();
      })
  }

}
