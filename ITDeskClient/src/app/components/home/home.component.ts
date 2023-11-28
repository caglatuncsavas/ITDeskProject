import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ButtonModule } from 'primeng/button';
import { DialogService, DynamicDialogModule, DynamicDialogRef } from 'primeng/dynamicdialog';
import { CreateComponent } from '../create/create.component';
import { MessageService } from 'primeng/api';
import { customers } from 'src/app/constants/customer';
import { DialogModule } from 'primeng/dialog';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, BreadcrumbModule, TableModule, TagModule, ButtonModule,DynamicDialogModule,DialogModule ],
  providers: [DialogService,MessageService],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export default class HomeComponent {
  customers= customers;

  selectedCustomers!: any;

  ref: DynamicDialogRef | undefined;

  constructor(public dialogService: DialogService, public messageService: MessageService) {}


  getSeverity(status: string) {
    switch (status) {
      case 'unqualified':
        return 'danger';

      case 'qualified':
        return 'success';

      case 'new':
        return 'info';

      case 'negotiation':
        return 'warning';

      case 'renewal':
        return "danger";
      default:
              return "danger";
    }
  }
  
  show() {
      this.ref = this.dialogService.open(CreateComponent, {
          header: 'Yeni Destek Oluştur',
          width: '30%',
          contentStyle: { overflow: 'auto' },
          baseZIndex: 10000,
          maximizable: false,
         
      });

      this.ref.onClose.subscribe((data:any) => {
          if (data) {
              this.messageService.add({ severity: 'info', summary: 'Product Selected', detail: data });
          }
      });

      this.ref.onMaximize.subscribe((value) => {
          this.messageService.add({ severity: 'info', summary: 'Maximized', detail: `maximized: ${value.maximized}` });
      });
  }

  ngOnDestroy() {
      if (this.ref) {
          this.ref.close();
      }
  }

}