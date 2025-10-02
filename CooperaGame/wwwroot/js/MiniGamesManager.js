// Funciones de configuración (No reciben evento)
const setBotonAgregarMadera = () => {
    let boton = document.getElementById("btnWood");
    // Al hacer click, pasamos el objeto evento (e) y el recurso.
    boton.addEventListener("click", (e) => DesplegarMinijuego(e, "wood"));
};

const setBotonAgregarPiedra = () => {
    let boton = document.getElementById("btnStone");
    // Al hacer click, pasamos el objeto evento (e) y el recurso.
    boton.addEventListener("click", (e) => DesplegarMinijuego(e, "stone"));
};

const setBotonAgregarComida = () => {
    let boton = document.getElementById("btnFood");
    // Corregido: La función anónima DEBE capturar el evento (e) y pasarlo.
    boton.addEventListener("click", (e) => DesplegarMinijuego(e, "food"));
};

// Función principal de lógica (Recibe el evento y el recurso)
const DesplegarMinijuego = (e, recurso) => {
    // 1. Esto ahora funciona correctamente porque 'e' es el objeto Event del click.
    e.preventDefault();

    // ... Tu lógica de juego/fetch ...
    const partidaId = document.getElementById("partidaId").innerHTML;
    console.log(partidaId);

    const payload = {
        partidaId: parseInt(partidaId, 10),
        recurso: recurso,
        nombreJugador: getUsuario()
    };

    fetch('/Recoleccions/Crear', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(payload)
    })
        .then(r => r.json())
        .then(data => window.location.href = data.redirectUrl);
};

// Función de inicialización
function setUp() {
    setBotonAgregarMadera();
    setBotonAgregarPiedra();
    setBotonAgregarComida();
}

// Inicializa el setup cuando el DOM esté cargado
document.addEventListener("DOMContentLoaded", () => setUp());