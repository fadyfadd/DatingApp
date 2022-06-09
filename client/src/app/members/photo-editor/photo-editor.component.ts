import { Component, OnInit, Input } from '@angular/core';
 import { Member } from 'src/app/_models/member';
 import { environment } from 'src/environments/environment';
import { AccountService } from 'src/app/_services/account.service';
import { User } from 'src/app/_models/user';
import { take } from 'rxjs/operators';
import { MembersService } from 'src/app/_services/members.service';
import { Photo } from 'src/app/_models/photo';


@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  
  @Input() member: Member;
   hasBaseDropzoneOver = false;
  baseUrl = environment.apiUrl;
  user: User;
  
  constructor() { }

  ngOnInit(): void {
  }

}
