import {Component, OnDestroy} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnDestroy {
  constructor(private auth: AuthService) {}
  public loginsub: Subscription | undefined;
  public login(e: Event) {
    e.preventDefault();
    if (this.loginForm.valid)
      this.loginsub = this.auth.requestLogin(this.loginForm.value as {username: string, password: string}).subscribe();
  }
  private fb = new FormBuilder();
  public loginForm = this.fb.nonNullable.group({
    username: ['', [Validators.required]],
    password: ['', [Validators.required]],
  });

  ngOnDestroy() {
    this.loginsub?.unsubscribe();
  }
}
