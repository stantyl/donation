import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder
} from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { Donation } from '../_models/donation';
import { UserService } from 'src/app/_services/user.service';
@Component({
  selector: 'app-donation',
  templateUrl: './donation.component.html',
  styleUrls: ['./donation.component.css']
})
export class DonationComponent implements OnInit {
  @Output() canceldonation = new EventEmitter();
  donationObj: Donation;
  donationForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private router: Router,
    private alertify: AlertifyService,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-red'
    };
    this.createdonationForm();
  }

  createdonationForm() {
    this.donationForm = this.fb.group(
      {
        title: ['', Validators.required],
        content: ['', Validators.required]
      },
    );
  }



  donation() {

    if (this.donationForm.valid) {
      this.donationObj = Object.assign({}, this.donationForm.value);

      console.log(this.donationObj);

      this.userService.addDonation(this.donationObj).subscribe(data => {
        this.alertify.success('You have add donation' );
      }, error => {
        this.alertify.error(error);
      });
    }

  }

  cancel() {
    this.canceldonation.emit(false);
  }
}
