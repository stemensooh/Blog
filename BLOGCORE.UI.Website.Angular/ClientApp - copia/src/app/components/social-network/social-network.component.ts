import {
  Component,
  OnInit,
  OnChanges,
  SimpleChanges,
  Input,
} from '@angular/core';
import { ProfileService } from '../../core/services/profile.service';
import { RedesSocialesModel } from '../../core/models/profile/redes-sociales.model';

@Component({
  selector: 'app-social-network',
  templateUrl: './social-network.component.html',
  styleUrls: ['./social-network.component.css'],
})
export class SocialNetworkComponent implements OnInit, OnChanges {
  @Input() username?: string;

  redes: RedesSocialesModel[] = [];
  constructor(private profileService: ProfileService) {}

  ngOnChanges(): void {
    console.log('ngOnChanges', this.username);
    if (this.username) {
      this.profileService
        .misRedesSociales(this.username ?? 'NotFound')
        .subscribe((data) => {
          this.redes = data;
        });
    }
  }

  ngOnInit(): void {}
}
