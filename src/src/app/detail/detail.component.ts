import { Component, OnInit } from '@angular/core';
import { ProductDetail } from '../dashboard/models/home.details.interface';
import { DashboardService } from '../dashboard/services/dashboard.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {

  productDetail: ProductDetail;

  constructor(private dashboardService: DashboardService, private route: ActivatedRoute,
        private router: Router) { }

  ngOnInit() {
    
    this.dashboardService.getProductDetails()
    .subscribe((homeDetails: ProductDetail) => {
      this.productDetail = homeDetails;
      console.log(this.productDetail);
    },
    error => {
        //this.notificationService.printErrorMessage(error);
        //this.router.navigateByUrl("account/login");
    });
    
  }

}
