# 🔧 AESTH7 – E-Commerce Backend

REST API backend for the AESTH7 e-commerce, developed with ASP.NET 8.  
Provides functionality for managing products, users, authentication, and payment processing.

---

## ✨ Features

- 🛠️ RESTful API with endpoints for products, users, authentication, and orders.
- 🔐 Authentication and authorization using JWT.
- 🗄️ Relational PostgreSQL database with migrations.
- 💳 Stripe integration for payment processing.
- 🚀 Clean and modular architecture.

---

## 🛠️ Technologies

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-blue?style=for-the-badge&logo=postgresql&logoColor=white) ![JWT](https://img.shields.io/badge/JWT-Auth-yellow?style=for-the-badge&logo=json-web-tokens&logoColor=white) ![Stripe](https://img.shields.io/badge/Stripe-Payments-blueviolet?style=for-the-badge&logo=stripe&logoColor=white)

---

## 📚 Main Endpoints

<div align="center">

| Method | Route                  | Description                    |
|--------|------------------------|-------------------------------|
| POST   | `/api/Auth/Login`       | Login and obtain JWT token    |
| POST   | `/api/Auth/Register`    | Register new user             |
| GET    | `/api/Product`          | List products                |
| GET    | `/api/Product/{id}`     | Product details              |
| GET    | `/api/Order/ByEmail`    | List orders by email         |
| POST   | `/api/Order`            | Create order and process payment |

</div>
