function setBotonAgregarMadera() {
    let boton = document.getElementById("btnWood");
    boton.addEventListener("click", DesplegarMinijuego("madera"));
}

function setBotonAgregarPiedra() {
    let boton = document.getElementById("btnStone");
    boton.addEventListener("click", DesplegarMinijuego("piedra"));
}

function setBotonAgregarComida() {
    let boton = document.getElementById("btnfood");
    boton.addEventListener("click", DesplegarMinijuego("comida"));
}

function DesplegarMinijuego(recurso) {
    preventDefault();
    const containerMinijuego = document.getElementById("modalContainter");

    const url = '/MiniGames/MiniGame_ClickIt';

    fetch(url)
    
}

function setUp() {
    setBotonAgregarMadera();
    setBotonAgregarPiedra();
    setBotonAgregarComida();
}

document.addEventListener("DOMContentLoaded", setUp());