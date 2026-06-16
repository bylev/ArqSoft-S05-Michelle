using Microsoft.AspNetCore.Mvc;
using CitasApp.Api.Models;

namespace CitasApp.Api.Controllers
{

        [ApiController]
        [Route("api/[controller]")]
        public class CalculadoraController : ControllerBase
        {
        // Este controlador es solo para fines de demostración y no tiene una función real en la aplicación de citas.
        [HttpGet("sumar")]
            public IActionResult Sumar(double a, double b)
            {
                var resultado = a + b;
                return Ok(new Calculadora { 
                    Operacion = "Suma", 
                    A = a, B = b, 
                    Resultado = resultado});
            }

            [HttpGet("restar")]
            public IActionResult Restar(double a, double b)
            {
                var resultado = a - b;
                return Ok(new Calculadora
                {
                    Operacion = "Resta",
                    A = a,
                    B = b,
                    Resultado = resultado
                });
            }

            [HttpGet("multiplicar")]
            public IActionResult Multiplicar(double a, double b)
            {
                var resultado = a * b;
                return Ok(new Calculadora
                {
                    Operacion = "Multiplicación",
                    A = a,
                    B = b,
                    Resultado = resultado
                });
            }

            [HttpGet("dividir")]
            public IActionResult Dividir(double a, double b)
            {
                var resultado = a / b;
                return Ok(new Calculadora
                {
                    Operacion = "División",
                    A = a,
                    B = b,
                    Resultado = resultado
                });
            }
        }
    }
