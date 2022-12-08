import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddExamResultComponent } from './add-exam-result.component';

describe('AddExamResultComponent', () => {
  let component: AddExamResultComponent;
  let fixture: ComponentFixture<AddExamResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddExamResultComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddExamResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
