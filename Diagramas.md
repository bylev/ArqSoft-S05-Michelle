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

    style Paciente fill:#dae8fc,stroke:#6c8ebf,stroke-width:2px,color:#000000
    style Cita fill:#dae8fc,stroke:#6c8ebf,stroke-width:2px,color:#000000
    style Medico fill:#dae8fc,stroke:#6c8ebf,stroke-width:2px,color:#000000
   ```