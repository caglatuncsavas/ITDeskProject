import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpErrorResponse, provideHttpClient } from '@angular/common/http';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { MessageService } from 'primeng/api';

export const appConfig: ApplicationConfig = {
  providers: [
    MessageService,
    provideHttpClient(),
    provideRouter(routes),
    importProvidersFrom(
      [
        BrowserAnimationsModule,
        SocialLoginModule
      ]
    ),
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '916544310471-os6k6egdve56g5p4stf7mauphksbesgc.apps.googleusercontent.com'
            )
          }
        ],
        onError: (err:HttpErrorResponse) => {
          console.error(err);
        }
      } as SocialAuthServiceConfig,
    }
  
  ]
};
