import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { AuthService } from 'src/app/services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from 'src/app/services/http.service';
import { TicketDetailModel, TicketModel } from 'src/app/models/ticket.model';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-detail',
  standalone: true,
  imports: [CommonModule, CardModule, ButtonModule,FormsModule],
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export default class DetailComponent {
  details: TicketDetailModel[] = [];
  content: string = "";
  ticketId: string = "";
  ticket:TicketModel = new TicketModel();

  constructor(
    public auth: AuthService,
    private http: HttpService,
    private activated: ActivatedRoute //Adres çubuğuna yazılan value almak için
  ) {
    this.activated.params.subscribe((res) => {
      this.ticketId = res["value"];
      this.getDetail();
      this.getTicket();
    });
  }

getTicket(){
  this.http.get(`Tickets/GetById?ticketId=${this.ticketId}`, (res) => {
    this.ticket = res;
  })
}

  getDetail() {
    this.http.get(`Tickets/GetDetails/${this.ticketId}`, (res) => {
      this.details = res;
    })
  }

  addDetailContent() {
    const data = {
      appUserId: this.auth.token.userId,
      content: this.content,
      ticketId: this.ticketId
    }

    this.http.post(`Tickets/AddDetailContent`, data, () => {
      this.content = "";//inputu temizle
      this.getDetail();
      this.getTicket(); //ticketın durumunu güncellemek için
    })
  }

  closeTicket(){
    this. http.get(`Tickets/CloseTicketByTicketId?ticketId=${this.ticket.id}`,()=>{
      this.getTicket();
  })
}
}
