¡Entendido! Como no tengo una función directa de "crear un archivo o libreta nueva" en tu computadora, **puedes copiar y pegar directamente este contenido** en tu nuevo documento, cuaderno digital (como Notion, Obsidian) o README.md de GitHub.

Aquí tienes el documento completo, limpio y organizado bajo el nombre **StockPulse**:

---

# StockPulse: Proyecto de Práctica TDD (.NET Core Web API + React)

## 📌 Descripción del Proyecto

StockPulse es un sistema inteligente de control de inventario y alertas diseñado para demostrar buenas prácticas de ingeniería de software, arquitectura limpia y desarrollo guiado por pruebas (**TDD**).

---

## 📋 Product Backlog (User Stories Atómicas)

### Módulo 1: Catálogo Base (Productos)

#### **US-01: Crear Producto Base**

* **Como** gestor de inventario,
* **Quiero** registrar un nuevo producto enviando `Name` y `SKU`,
* **Para** darlo de alta en el catálogo base de la tienda.
* **Criterios de Aceptación:**
* Debe retornar `201 Created` con el ID generado si los datos son válidos.
* Si el `Name` o el `SKU` están vacíos, debe retornar `400 Bad Request`.



#### **US-02: Validar Unicidad de SKU**

* **Como** sistema de inventarios,
* **Quiero** rechazar el registro de un producto si el `SKU` ya existe en la base de datos,
* **Para** evitar duplicidades en el catálogo.
* **Criterios de Aceptación:**
* Si el `SKU` existe, el backend retorna `400 Bad Request` con el mensaje de error: `"El SKU ya está registrado"`.
* Debe probarse unitariamente en la capa de servicio (.NET) simulando el repositorio mediante *Mocks*.



---

### Módulo 2: Lógica de Alertas y Stock (Core Domain TDD)

#### **US-03: Definir Umbrales de Stock**

* **Como** gestor de inventario,
* **Quiero** asignar un `CurrentStock` y un `MinimumStock` a un producto existente,
* **Para** configurar sus límites de inventario.
* **Criterios de Aceptación:**
* No se permite asignar valores negativos a `CurrentStock` ni a `MinimumStock`.
* Si el valor enviado es menor a 0, la API retorna error de validación `400`.



#### **US-04: Cálculo de Estado de Alerta**

* **Como** encargado de logística,
* **Quiero** que el servicio domine el cálculo del campo calculado `IsLowStock` (`CurrentStock <= MinimumStock`),
* **Para** saber qué productos están en riesgo de desabasto.
* **Criterios de Aceptación:**
* Si `CurrentStock` = 5 y `MinimumStock` = 10, `IsLowStock` debe ser `true`.
* Si `CurrentStock` = 10 y `MinimumStock` = 10, `IsLowStock` debe ser `true` (límite).
* Si `CurrentStock` = 12 y `MinimumStock` = 10, `IsLowStock` debe ser `false`.
* **TDD:** Debe implementarse comenzando 100% por tests de dominio (xUnit) antes de escribir la entidad/servicio.



---

### Módulo 3: Movimientos de Inventario

#### **US-05: Registrar Entrada de Stock (Compras)**

* **Como** operador de almacén,
* **Quiero** aumentar la cantidad de stock enviando una transacción de tipo `IN`,
* **Para** reflejar la llegada de nuevo proveedor.
* **Criterios de Aceptación:**
* El nuevo `CurrentStock` debe ser igual al `CurrentStock` anterior + la cantidad ingresada.
* La cantidad a ingresar debe ser estrictamente mayor a 0.



#### **US-06: Validar Salida de Stock (Ventas / Reajustes)**

* **Como** operador de almacén,
* **Quiero** que el sistema impida registrar una salida `OUT` si la cantidad supera el `CurrentStock` disponible,
* **Para** evitar inventario negativo.
* **Criterios de Aceptación:**
* Si `CurrentStock` es 5 e intento retirar 6, debe lanzar una excepción de dominio (`InsufficientStockException`) y retornar HTTP `422 Unprocessable Entity`.



---

## 🛠️ Flujo de Trabajo TDD Sugerido para tu Repositorio

1. **Rojo:** Crea el test unitario en xUnit (Backend) o React Testing Library (Frontend) que valide uno de los criterios de aceptación anteriores y verifica que falle.
2. **Verde:** Escribe la implementación mínima indispensable en tu Web API o componente de React para que pase el test.
3. **Refactor:** Limpia o estructura el código de manera elegante y sube un Commit atómico a GitHub vinculado a su respectiva US.

---