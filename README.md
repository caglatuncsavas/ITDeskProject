# ITDesk Project

The ITDesk project is developed with Angular 17 on the client side and .NET 8 on the server side. Both Users and Admins can log in to the system. Users can access the system using Google Login.
They can also attach files to their tickets and communicate with admins via the chat page. Admins have the ability to view all users' tickets. Below, you can find details about the libraries used.

## Frontend

### Technologies Used:
- Angular 17
- PrimeNG library
- ag-grid-angular library
- jwt-decode library
- abacritt/angularx-social-login library (for Google login)

### Operations Performed:
- Integrated the Primeng library to add UI components. Designed the Login Page, Home Page, TicketDetailPage.
- Modified table structure and integrated AG Data Grid.
- Implemented generic structures for handling HTTP errors and operations.
- Integrated AG Data Grid for displaying tabular data.

## Backend

### Technologies Used:
- .NET 8
- Identity library
- FluentValidation library
- JWT library

### Operations Performed:
- Utilized UserManager and SignInManager services from the Identity library.
- Users were created and user information was searched for login operations using the UserManager service.
- Validated and managed user sessions using SignInManager service.
- Implemented a mechanism to lock user accounts for 15 minutes after 3 incorrect password attempts using SignInManager service.
- Generated JWT tokens.
- Implemented validation checks using FluentValidation library.
- Completed ticket creation processes.
- Listed tickets, displaying all tickets for admins and only user-specific tickets for non-admin users.
- Enabled messaging between users and admins during ticket creation.
- Provided an optional feature for users to upload/send files during messaging.




https://github.com/caglatunc/ITDeskProject/assets/95507765/bba1ef51-7bcb-4103-9fb4-cf4595dfa5c8

