# 🔧 AESTH7 – E-Commerce Backend

Backend REST API para el e-commerce AESTH7, desarrollado con ASP.NET 8.  
Provee funcionalidades de gestión de productos, usuarios, autenticación y procesamiento de pagos.

---

## ✨ Características

- 🛠️ API RESTful con endpoints para productos, usuarios, autenticación y órdenes.
- 🔐 Autenticación y autorización con JWT.
- 🗄️ Base de datos relacional PostgreSQL con migraciones.
- 💳 Integración con Stripe para procesamiento de pagos.
- 🚀 Arquitectura limpia y modular.

---

## 🛠️ Tecnologías

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-blue?style=for-the-badge&logo=postgresql&logoColor=white) ![JWT](https://img.shields.io/badge/JWT-Auth-yellow?style=for-the-badge&logo=json-web-tokens&logoColor=white) ![Stripe](https://img.shields.io/badge/Stripe-Pagos-blueviolet?style=for-the-badge&logo=stripe&logoColor=white)

---

## 📚 Endpoints principales

<div align="center">

| Método | Ruta                  | Descripción                    |
|--------|-----------------------|-------------------------------|
| POST   | `/api/Auth/Login`      | Iniciar sesión y obtener JWT  |
| POST   | `/api/Auth/Register`   | Registrar nuevo usuario       |
| GET    | `/api/Product`         | Listar productos              |
| GET    | `/api/Product/{id}`    | Detalle de producto           |
| GET    | `/api/Order/ByEmail`   | Listar órdenes por email      |
| POST   | `/api/Order`           | Crear orden y procesar pago   |

</div>
