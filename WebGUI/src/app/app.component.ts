import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from './components/dialog/dialog.component';
import { LoginService } from './services/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'angularWeb2';
  user = JSON.parse(localStorage.getItem('user') || '{}');

  constructor(private dialog: MatDialog, private service: LoginService) {}

  logoutUser(): void {
    this.service.logoutUser();
  }

  openDialog() {
    const dialogRef = this.dialog.open(DialogComponent, {
      width: '30%',
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log(`Dialog result: ${result}`);
    });
  }
}
