function guardarUsuario(usuario) {
    if (usuario == null) {
        return false;
    }
    try {
        localStorage.setItem("user",usuario);
        return true;
    } catch (error) {
        console.log(error);
        return false;
    };
};

function getUsuario() {
    const user = localStorage.getItem("user");
    
    return user;
};

function deleteUsuario() {
    localStorage.setItem("user", null);
};