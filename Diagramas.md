# Diagrama de clases

---

## ¿Qué es?

Describe la estructura del sistema mostrando las clases, sus atributos, operaciones y relaciones entre objetos. 
En citas médicas, el diagrama de clases puede incluir clases como `Cita`, `Paciente`, `Medico`, y `Especialidad`, mostrando cómo se relacionan entre sí.


```mermaid
classDiagram
    class Paciente {
        +int Id
        +string Nombre
        +string Apellido
        +string Email
        +string Telefono
    }

    class Cita {
        +int Id
        +int PacienteId
        +int MedicoId
        +date Fecha
        +time Hora
        +string Motivo
        +string Estado
    }

    class Medico {
        +int Id
        +string Nombre
        +string Apellido
        +string Especialidad
        +string NumeroLicencia
    }

    Paciente "1" -- "*" Cita : realiza
    Medico "1" -- "*" Cita : atiende

    style Paciente fill:#fdfd96,stroke:#333,stroke-width:1px
    style Cita fill:#fdfd96,stroke:#333,stroke-width:1px
    style Medico fill:#fdfd96,stroke:#333,stroke-width:1px
   ```