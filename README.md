# CustomerManagementApp


Customer Management API
Customer Management API is a simple ASP.NET Core web application built to demonstrate basic CRUD (Create, Read, Update, Delete) operations on customer data using a RESTful Web API. The application leverages Razor Pages for client-side rendering and a SQLite database for data storage. It follows SOLID principles and is designed to be extendable and easy to maintain.

# Features

API Operations:

GET /api/customers: Retrieve a list of all customers.
GET /api/customers/{id}: Retrieve a specific customer by ID.
POST /api/customers: Create a new customer.
PUT /api/customers/{id}: Update an existing customer.
DELETE /api/customers/{id}: Delete a customer.
Client-Side Operations:

View all customers in a responsive table using Razor Pages.
Add new customers using a dynamic form.
Edit existing customers with inline editing.
Delete customers with confirmation.
Undo delete operations.
Batch save changes to the server.

Seeding Data:

The application seeds the database with 5 initial customers upon the first run.

# Technology Stack

Backend:

ASP.NET Core 8
Entity Framework Core (EF Core) for data access
SQLite database

Frontend:

ASP.NET Razor Pages
Bootstrap 5 for responsive design
HTMX for dynamic updates without full-page refreshes

API Endpoints: https://localhost:7139/api/customers
Client UI: https://localhost:7133
Configuration
The application uses appsettings.json for configuration settings like the database connection string.
Modify the appsettings.Development.json file to configure your local development environment settings.
Project Structure
CustomerManagementApi/: Contains the Web API for managing customer data.


# Usage
Add New Customer:

Click the "Add" button at the bottom of the customer table.
Fill in the customer details in the new row and click the save icon.

Edit Customer:

Click the "Edit" button next to the customer you want to modify.
Update the customer details and click the save icon.

Delete Customer:

Click the "Delete" button next to the customer you want to remove.
Click "Undo" if you change your mind.
Save Changes:


Click the "Save Changes" button at the top of the table.
Confirm the changes in the modal dialog.
