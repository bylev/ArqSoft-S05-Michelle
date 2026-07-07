# Diagrama de clases

---

## ¿Qué es?

Describe la estructura del sistema mostrando las clases, sus atributos, operaciones y relaciones entre objetos. 
En citas médicas, el diagrama de clases puede incluir clases como `Cita`, `Paciente`, `Medico`, y `Especialidad`, mostrando cómo se relacionan entre sí.


classDiagram
    direction LR

    class Paciente {
        Id
        Nombre
        Apellido
        Email
        Telefono
    }

    class Cita {
        Id
        PacienteId
        MedicoId
        Fecha
        Hora
        Motivo
        Estado
    }

    class Medico {
        Id
        Nombre
        Apellido
        Especialidad
        NumeroLicencia
    }

    Cita ..> Paciente : PacienteId
    Cita ..> Medico : MedicoId