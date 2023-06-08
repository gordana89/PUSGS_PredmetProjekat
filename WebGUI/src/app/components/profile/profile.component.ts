import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DomSanitizer } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';
import { EditProfileComponent } from '../edit-profile/edit-profile.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  user = JSON.parse(localStorage.getItem('user') || '{}');
  userEditInfo: any;

  public reqHeader = new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('token')),
  });

  constructor(
    private http: HttpClient,
    private dom: DomSanitizer,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.http
      .get<any>(`${environment.users}/${this.user.email}`, {
        headers: this.reqHeader,
      })
      .subscribe(
        (res) => {
          this.userEditInfo = res;
          this.http
            .get<any>(`${environment.users}/${this.user.email}/download`, {
              responseType: 'Blob' as 'json',
              headers: this.reqHeader,
            })
            .subscribe((fileRes) => {
              let url = URL.createObjectURL(fileRes);
              this.userEditInfo.file = this.dom.bypassSecurityTrustUrl(url);
            });
        },
        (err) => {
          alert('Something went wrong');
        }
      );
  }

  editButtonClick(userEditInfo: any): void {
    this.dialog.open(EditProfileComponent, {
      data: { userEditInfo },
    });
  }

  updateButtonClick(): void {}
}
