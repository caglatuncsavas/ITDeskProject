import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenubarModule } from 'primeng/menubar';
import { MenuItem } from 'primeng/api';
import { Router, RouterOutlet } from '@angular/router';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule, MenubarModule, RouterOutlet,InputTextModule,ButtonModule],
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {
  items: MenuItem[] | undefined;

  constructor(
    private router: Router
  ) { }

  ngOnInit() {
    this.items = [
      {
        label: 'Ana Sayfa',
        icon: 'pi pi-fw pi-home',
        routerLink: "/"
      }
    ];
  }
  
  logout(){
    localStorage.removeItem("response");
    this.router.navigateByUrl("/login");
  }
}
