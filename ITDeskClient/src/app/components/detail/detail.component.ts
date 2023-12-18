import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { AuthService } from 'src/app/services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from 'src/app/services/http.service';
import { TicketDetailModel } from 'src/app/models/ticket.model';

@Component({
  selector: 'app-detail',
  standalone: true,
  imports: [CommonModule, CardModule],
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export default class DetailComponent {
  details:TicketDetailModel[]=[];
  
ticketId: string="";

  constructor(
    public auth: AuthService,
    private http: HttpService,
    private activated: ActivatedRoute //Adres çubuğuna yazılan value almak için
  ) {
    this.activated.params.subscribe((res) => {
      this.ticketId = res["value"];
      this.getDetail();
    });
   }

   getDetail(){
    this.http.get(`Tickets/GetDetails/${this.ticketId}`,(res)=>{
      this.details=res;
    })
   }
}
