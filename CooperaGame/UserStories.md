# User Stories V1

## 0- Como sistema Quiero calcular automáticamente las metas de cada recurso al comenzar una nueva partida Para asegurar que cada partida tenga objetivos aleatorios pero balanceados
### Acceptance criteria: 
	Calcular metas para V1 (botones)
	Dado que se inicia una nueva partida en V1
	Cuando el sistema calcula las metas
	Entonces para cada recurso (madera, piedra, comida) se genera un número aleatorio entre 0 y 1
	Y se multiplica por el factor de dificultad 100
	Y el resultado se redondea al entero más cercano
	Y cada meta debe ser mínimo 10 y máximo 100

	Persistir metas de la partida
	Dado que se han calculado las metas de recursos
	Cuando se almacenan en el sistema
	Entonces las metas permanecen constantes durante toda la partida
	Y se pueden consultar en cualquier momento

### Requerimientos técnicos:
	- Usar un generador de números pseudoaleatorios con semilla basada en timestamp para reproducibilidad en testing
	- Almacenar las metas en sesión o base de datos según la arquitectura elegida
	- Validar que las metas generadas estén dentro de los rangos esperados


## 1- Como jugador Quiero ingresar mi nombre y presionar un botón por recurso Para contribuir a que el equipo alcance las metas de la partida
### Acceptance criteria:
	Ingresar nombre para jugar
	Dado que accedo a la aplicación en estado "jugando"
	Cuando se muestra el popup de ingreso de nombre
	E ingreso un nombre válido (1-20 caracteres alfanuméricos)
	Entonces puedo comenzar a recolectar recursos asociados a ese nombre Y el popup se cierra

	Validar nombre inválido
	Dado que se muestra el popup de ingreso de nombre Cuando ingreso un nombre vacío o con caracteres especiales
	Entonces se muestra un mensaje de error	Y el popup permanece abierto

	Recolectar recurso válido
	Dado que ya ingresé mi nombre Y la partida está en estado "jugando"
	Cuando presiono el botón de un recurso que no ha alcanzado su meta Entonces el contador de ese recurso aumenta en 1
	Y se registra mi nombre, el recurso recolectado y timestamp
	Y veo un feedback visual de éxito

	Intentar recolectar recurso completo
	Dado que un recurso ya alcanzó su meta
	Cuando intento presionar el botón de ese recurso Entonces el botón aparece deshabilitado Y no se suma ningún recurso

	Recolección concurrente
	Dado que múltiples jugadores presionan botones simultáneamente Cuando se procesa la recolección
	Entonces cada acción se registra correctamente Y el contador se incrementa en el orden correcto

	Mostrar progreso en números
	Dado que ya se han recolectado algunos recursos
	Cuando visualizo la pantalla de juego
	Entonces veo el progreso de cada recurso expresado como "X/Y" (recolectado/meta)
	Y los botones de recursos completados aparecen deshabilitados

### Requerimientos técnicos:
	- Implementar validación de nombres del lado cliente y servidor
	- Usar atomic operations para evitar race conditions en recolección concurrente
	- Almacenar timestamp con precisión de milisegundos
	- Implementar debounce en botones para evitar double-clicks accidentales
	- Sanitizar nombres de usuario para prevenir XSS

## 2- Finalizar la partida al cumplir metas, Como sistema Quiero detectar cuando todos los recursos alcanzan la meta Para marcar la partida como finalizada
### Acceptance Criteria:
	Partida completada:
		Dado que todos los recursos alcanzaron sus metas
		Cuando se registra el último recurso
		Entonces la partida pasa al estado "presentando resultados"	Y no se permite seguir sumando recursos
		Y se calcula el tiempo total de la partida

	Verificación automática de finalización:
		Dado que la partida está en estado "jugando"
		Cuando se registra un nuevo recurso
		Entonces el sistema verifica si todas las metas se cumplieron Y si es así, finaliza automáticamente la partida

	Transición atómica de estado:
		Dado que el último recurso necesario está siendo recolectado por múltiples jugadores
		Cuando se procesa la recolección simultánea
		Entonces solo una acción completa la partida
		Y las demás se ignoran correctamente

### Requerimientos técnicos:
	- Implementar verificación atómica de finalización para evitar race conditions
	- Usar transacciones de base de datos para garantizar consistencia en el cambio de estado
	- Calcular tiempo total como diferencia entre primer y último recurso recolectado

## 3- Mostrar resultados de la última partida: Como visitante Quiero ver una tabla con los resultados cuando una partida termina Para conocer cuánto recolectó cada jugador y el tiempo total
### Acceptance criteria:
	Ver tabla de resultados: 
		Dado que la partida ha terminado
		Cuando accedo a la aplicación
		Entonces veo una tabla con el nombre de cada jugador
		Y la cantidad de recursos recolectados de cada tipo (madera, piedra, comida)
		Y el total de recursos recolectados por jugador
		Y el tiempo total de la partida en formato legible (MM:SS)
		Y las metas finales alcanzadas de cada recurso

	Ordenar tabla por contribución:
		Dado que estoy viendo los resultados de la partida
		Cuando visualizo la tabla
		Entonces los jugadores aparecen ordenados por total de recursos recolectados (mayor a menor)
		Y en caso de empate, se ordena por orden alfabético de nombre

	Ver detalles de metas alcanzadas: 
		Dado que estoy viendo los resultados
		Cuando visualizo la información de la partida
		Entonces veo las metas originales establecidas para cada recurso
		Y el total final alcanzado (que debe coincidir con las metas)

### Requerimientos técnicos:
	- Formatear tiempo total en formato MM:SS o HH:MM:SS si supera 1 hora
	- Implementar ordenación estable para casos de empate
	- Validar que los totales mostrados coincidan con las metas establecidas
	- Preservar datos de la partida anterior hasta que se inicie una nueva

## 4- Iniciar una nueva partida: Como visitante Quiero tener un botón “Comenzar nueva partida” en la vista de resultados Para poder volver a jugar desde cero
### Acceptance criteria:
	Escenario: Reiniciar partida
	  Dado que estoy en la pantalla de resultados
	  Cuando presiono el botón "Comenzar nueva partida"
	  Entonces se crean nuevas metas de recursos usando US-0
	  Y se limpian todos los datos de la partida anterior
	  Y la aplicación pasa al estado "jugando"
	  Y al recargar cualquier jugador ve el popup para ingresar su nombre

	Escenario: Confirmación de nueva partida
	  Dado que estoy en la pantalla de resultados
	  Cuando presiono el botón "Comenzar nueva partida"
	  Entonces se muestra una confirmación "¿Está seguro de iniciar una nueva partida?"
	  Y si confirmo, se procede con el reinicio
	  Y si cancelo, permanezco en la pantalla de resultados

	Escenario: Múltiples usuarios iniciando partida
	  Dado que múltiples usuarios presionan "Comenzar nueva partida" simultáneamente
	  Cuando se procesa la acción
	  Entonces solo se crea una nueva partida
	  Y todos los usuarios ven el mismo estado actualizado

	Escenario: Acceso directo en estado jugando
	  Dado que la partida ya está en estado "jugando"
	  Cuando un usuario accede a la aplicación
	  Entonces no ve la pantalla de resultados
	  Y ve directamente el popup de ingreso de nombre

### Requerimientos técnicos:
	- Implementar soft confirmation antes de iniciar nueva partida
	- Limpiar sesiones y datos cached de la partida anterior
	- Usar operaciones atómicas para evitar múltiples inicializaciones simultáneas
	- Validar que la nueva partida tenga metas válidas antes de cambiar el estado

## 5- Visualizar progreso en gráfico (Chart.js): Como jugador Quiero ver el progreso representado en un gráfico de barras Para tener una representación visual más clara que los números
### Acceptance criteria:
	Escenario: Ver gráfico de recursos inicial
	  Dado que la partida está en estado jugando y no se ha recolectado ningún recurso
	  Cuando ingreso a la aplicación
	  Entonces veo un gráfico con tres barras en gris: madera, piedra y comida
	  Y cada barra muestra 0 de progreso hasta su meta correspondiente
	  Y las etiquetas indican "0/X" donde X es la meta de cada recurso

	Escenario: Ver gráfico con progreso parcial
	  Dado que algunos recursos han sido recolectados
	  Cuando visualizo el gráfico
	  Entonces las barras muestran el progreso actual con color distintivo
	  Y las barras no completadas permanecen en gris en su parte faltante
	  Y las etiquetas muestran "recolectado/meta" actualizados

	Escenario: Actualización periódica automática
	  Dado que estoy en la pantalla de juego
	  Cuando pasan exactamente 5 segundos
	  Entonces el gráfico se actualiza automáticamente via JavaScript
	  Y se hace una petición AJAX para obtener el progreso más reciente
	  Y la animación de barras muestra el cambio de progreso

	Escenario: Manejo de errores de actualización
	  Dado que estoy viendo el gráfico
	  Cuando falla la petición de actualización automática
	  Entonces el gráfico mantiene los datos anteriores
	  Y se reintenta la actualización en el próximo ciclo de 5 segundos
	  Y no se muestra error visible al usuario

	Escenario: Finalización del juego
	  Dado que todos los recursos alcanzaron sus metas
	  Cuando el gráfico se actualiza
	  Entonces todas las barras se muestran 100% completas
	  Y después de 2 segundos se redirige a la pantalla de resultados
	  Y el gráfico desaparece

	Escenario: Barras completadas individualmente
	  Dado que uno o más recursos alcanzaron su meta
	  Cuando visualizo el gráfico
	  Entonces las barras completas se muestran en color de éxito
	  Y las barras incompletas mantienen el color de progreso normal

### Requerimientos técnicos:
	- Usar Chart.js versión 3+ con animaciones suaves
	- Implementar peticiones AJAX cada 5 segundos usando setInterval
	- Manejar errores de red sin interrumpir la experiencia del usuario
	- Usar colores accesibles que distingan progreso, completo y pendiente
	- Implementar responsive design para el gráfico en diferentes pantallas
	- Limpiar intervalos de JavaScript cuando se cambia de vista para evitar memory leaks
		



