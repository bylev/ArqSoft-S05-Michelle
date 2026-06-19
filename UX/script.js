// URL API
const API_URL = "https://localhost:7288/api/calculadora";

async function calcular(operacion) {
    const a = document.getElementById("numA").value;
    const b = document.getElementById("numB").value;
    const resultadoDiv = document.getElementById("resultado");

    if (a === "" || b === "") {
        resultadoDiv.className = "resultado error";
        resultadoDiv.textContent = "Por favor, ingrese ambos números.";
        return;
    }

    try {
        const respuesta = await fetch(`${API_URL}/${operacion}?a=${a}&b=${b}`);

        if (!respuesta.ok) {
            throw new Error(`Error en la petición: ${API_URL}`);
        }

        const datos = await respuesta.json();

        resultadoDiv.className = "resultado";
        resultadoDiv.textContent = `${datos.operacion}: ${datos.resultado}`;
    } catch (error) {
        resultadoDiv.className = "resultado error";
        resultadoDiv.textContent = "No se pudo conectar con la API";
    }
}