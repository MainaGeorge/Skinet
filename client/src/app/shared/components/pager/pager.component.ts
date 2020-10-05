import {Component, EventEmitter, Input, Output, OnInit} from '@angular/core';
import {PageChangedEvent} from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.css']
})
export class PagerComponent implements OnInit {
  @Input() totalCount: number;
  @Input() pageSize: number;
  @Output() pageNumberChanged = new EventEmitter<number>();
  constructor() { }

  ngOnInit(): void {
  }

  onPageChanged(event: PageChangedEvent) {
    this.pageNumberChanged.emit(event.page)
  }
}
