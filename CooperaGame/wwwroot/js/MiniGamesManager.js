function setBotonAgregarMadera() {
    let boton = document.getElementById("btnWood");
    boton.addEventListener("click", () => DesplegarMinijuego("wood"));
}

function setBotonAgregarPiedra() {
    let boton = document.getElementById("btnStone");
    boton.addEventListener("click", () => DesplegarMinijuego("stone"));
}

function setBotonAgregarComida() {
    let boton = document.getElementById("btnFood");
    boton.addEventListener("click", () => DesplegarMinijuego("food"));
}

function DesplegarMinijuego(recurso) {
    //document.preventDefault();
    const containerMinijuego = document.getElementById("modalContainter");
    const partidaId = document.getElementById("partidaId").innerHTML;
    console.log(partidaId);

    //const url = '/MiniGames/MiniGame_ClickIt';
    /*const datosParaEnviar = {
        // Asegúrate de que los nombres de las propiedades coincidan (Id, Nombre)
        PartidaId: partidaId,
        Recurso: recurso,
        JugadorId: parseInt(2),
    };*/

    const payload = {
        partidaId: parseInt(partidaId, 10),
        recurso: recurso,
        nombreJugador: getUsuario()
    };

    //const url = `/Recoleccions/Crear?recurso=${encodeURIComponent(recurso) }&jugadorId=2&partidaId=25`;
    
    fetch('/Recoleccions/Crear', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json' 
        },
        body: JSON.stringify(payload)
    })
    .then(r => r.json())
    .then(data => window.location.href = data.redirectUrl);
        
    
    
}

function setUp() {
    setBotonAgregarMadera();
    setBotonAgregarPiedra();
    setBotonAgregarComida();
}

document.addEventListener("DOMContentLoaded", setUp());