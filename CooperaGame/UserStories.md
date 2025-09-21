# User Stories V1

## 0- Como sistema Quiero calcular autom�ticamente las metas de cada recurso al comenzar una nueva partida Para asegurar que cada partida tenga objetivos aleatorios pero balanceados
### Acceptance criteria: 
	Calcular metas para V1 (botones)
	Dado que se inicia una nueva partida en V1
	Cuando el sistema calcula las metas
	Entonces para cada recurso (madera, piedra, comida) se genera un n�mero aleatorio entre 0 y 1
	Y se multiplica por el factor de dificultad 100
	Y el resultado se redondea al entero m�s cercano
	Y cada meta debe ser m�nimo 10 y m�ximo 100

	Persistir metas de la partida
	Dado que se han calculado las metas de recursos
	Cuando se almacenan en el sistema
	Entonces las metas permanecen constantes durante toda la partida
	Y se pueden consultar en cualquier momento

### Requerimientos t�cnicos:
	- Usar un generador de n�meros pseudoaleatorios con semilla basada en timestamp para reproducibilidad en testing
	- Almacenar las metas en sesi�n o base de datos seg�n la arquitectura elegida
	- Validar que las metas generadas est�n dentro de los rangos esperados


## 1- Como jugador Quiero ingresar mi nombre y presionar un bot�n por recurso Para contribuir a que el equipo alcance las metas de la partida
### Acceptance criteria:
	Ingresar nombre para jugar
	Dado que accedo a la aplicaci�n en estado "jugando"
	Cuando se muestra el popup de ingreso de nombre
	E ingreso un nombre v�lido (1-20 caracteres alfanum�ricos)
	Entonces puedo comenzar a recolectar recursos asociados a ese nombre Y el popup se cierra

	Validar nombre inv�lido
	Dado que se muestra el popup de ingreso de nombre Cuando ingreso un nombre vac�o o con caracteres especiales
	Entonces se muestra un mensaje de error	Y el popup permanece abierto

	Recolectar recurso v�lido
	Dado que ya ingres� mi nombre Y la partida est� en estado "jugando"
	Cuando presiono el bot�n de un recurso que no ha alcanzado su meta Entonces el contador de ese recurso aumenta en 1
	Y se registra mi nombre, el recurso recolectado y timestamp
	Y veo un feedback visual de �xito

	Intentar recolectar recurso completo
	Dado que un recurso ya alcanz� su meta
	Cuando intento presionar el bot�n de ese recurso Entonces el bot�n aparece deshabilitado Y no se suma ning�n recurso

	Recolecci�n concurrente
	Dado que m�ltiples jugadores presionan botones simult�neamente Cuando se procesa la recolecci�n
	Entonces cada acci�n se registra correctamente Y el contador se incrementa en el orden correcto

	Mostrar progreso en n�meros
	Dado que ya se han recolectado algunos recursos
	Cuando visualizo la pantalla de juego
	Entonces veo el progreso de cada recurso expresado como "X/Y" (recolectado/meta)
	Y los botones de recursos completados aparecen deshabilitados

### Requerimientos t�cnicos:
	- Implementar validaci�n de nombres del lado cliente y servidor
	- Usar atomic operations para evitar race conditions en recolecci�n concurrente
	- Almacenar timestamp con precisi�n de milisegundos
	- Implementar debounce en botones para evitar double-clicks accidentales
	- Sanitizar nombres de usuario para prevenir XSS

## 2- Finalizar la partida al cumplir metas, Como sistema Quiero detectar cuando todos los recursos alcanzan la meta Para marcar la partida como finalizada
### Acceptance Criteria:
	Partida completada:
		Dado que todos los recursos alcanzaron sus metas
		Cuando se registra el �ltimo recurso
		Entonces la partida pasa al estado "presentando resultados"	Y no se permite seguir sumando recursos
		Y se calcula el tiempo total de la partida

	Verificaci�n autom�tica de finalizaci�n:
		Dado que la partida est� en estado "jugando"
		Cuando se registra un nuevo recurso
		Entonces el sistema verifica si todas las metas se cumplieron Y si es as�, finaliza autom�ticamente la partida

	Transici�n at�mica de estado:
		Dado que el �ltimo recurso necesario est� siendo recolectado por m�ltiples jugadores
		Cuando se procesa la recolecci�n simult�nea
		Entonces solo una acci�n completa la partida
		Y las dem�s se ignoran correctamente

### Requerimientos t�cnicos:
	- Implementar verificaci�n at�mica de finalizaci�n para evitar race conditions
	- Usar transacciones de base de datos para garantizar consistencia en el cambio de estado
	- Calcular tiempo total como diferencia entre primer y �ltimo recurso recolectado

## 3- Mostrar resultados de la �ltima partida: Como visitante Quiero ver una tabla con los resultados cuando una partida termina Para conocer cu�nto recolect� cada jugador y el tiempo total
### Acceptance criteria:
	Ver tabla de resultados: 
		Dado que la partida ha terminado
		Cuando accedo a la aplicaci�n
		Entonces veo una tabla con el nombre de cada jugador
		Y la cantidad de recursos recolectados de cada tipo (madera, piedra, comida)
		Y el total de recursos recolectados por jugador
		Y el tiempo total de la partida en formato legible (MM:SS)
		Y las metas finales alcanzadas de cada recurso

	Ordenar tabla por contribuci�n:
		Dado que estoy viendo los resultados de la partida
		Cuando visualizo la tabla
		Entonces los jugadores aparecen ordenados por total de recursos recolectados (mayor a menor)
		Y en caso de empate, se ordena por orden alfab�tico de nombre

	Ver detalles de metas alcanzadas: 
		Dado que estoy viendo los resultados
		Cuando visualizo la informaci�n de la partida
		Entonces veo las metas originales establecidas para cada recurso
		Y el total final alcanzado (que debe coincidir con las metas)

### Requerimientos t�cnicos:
	- Formatear tiempo total en formato MM:SS o HH:MM:SS si supera 1 hora
	- Implementar ordenaci�n estable para casos de empate
	- Validar que los totales mostrados coincidan con las metas establecidas
	- Preservar datos de la partida anterior hasta que se inicie una nueva

## 4- Iniciar una nueva partida: Como visitante Quiero tener un bot�n �Comenzar nueva partida� en la vista de resultados Para poder volver a jugar desde cero
### Acceptance criteria:
	Escenario: Reiniciar partida
	  Dado que estoy en la pantalla de resultados
	  Cuando presiono el bot�n "Comenzar nueva partida"
	  Entonces se crean nuevas metas de recursos usando US-0
	  Y se limpian todos los datos de la partida anterior
	  Y la aplicaci�n pasa al estado "jugando"
	  Y al recargar cualquier jugador ve el popup para ingresar su nombre

	Escenario: Confirmaci�n de nueva partida
	  Dado que estoy en la pantalla de resultados
	  Cuando presiono el bot�n "Comenzar nueva partida"
	  Entonces se muestra una confirmaci�n "�Est� seguro de iniciar una nueva partida?"
	  Y si confirmo, se procede con el reinicio
	  Y si cancelo, permanezco en la pantalla de resultados

	Escenario: M�ltiples usuarios iniciando partida
	  Dado que m�ltiples usuarios presionan "Comenzar nueva partida" simult�neamente
	  Cuando se procesa la acci�n
	  Entonces solo se crea una nueva partida
	  Y todos los usuarios ven el mismo estado actualizado

	Escenario: Acceso directo en estado jugando
	  Dado que la partida ya est� en estado "jugando"
	  Cuando un usuario accede a la aplicaci�n
	  Entonces no ve la pantalla de resultados
	  Y ve directamente el popup de ingreso de nombre

### Requerimientos t�cnicos:
	- Implementar soft confirmation antes de iniciar nueva partida
	- Limpiar sesiones y datos cached de la partida anterior
	- Usar operaciones at�micas para evitar m�ltiples inicializaciones simult�neas
	- Validar que la nueva partida tenga metas v�lidas antes de cambiar el estado

## 5- Visualizar progreso en gr�fico (Chart.js): Como jugador Quiero ver el progreso representado en un gr�fico de barras Para tener una representaci�n visual m�s clara que los n�meros
### Acceptance criteria:
	Escenario: Ver gr�fico de recursos inicial
	  Dado que la partida est� en estado jugando y no se ha recolectado ning�n recurso
	  Cuando ingreso a la aplicaci�n
	  Entonces veo un gr�fico con tres barras en gris: madera, piedra y comida
	  Y cada barra muestra 0 de progreso hasta su meta correspondiente
	  Y las etiquetas indican "0/X" donde X es la meta de cada recurso

	Escenario: Ver gr�fico con progreso parcial
	  Dado que algunos recursos han sido recolectados
	  Cuando visualizo el gr�fico
	  Entonces las barras muestran el progreso actual con color distintivo
	  Y las barras no completadas permanecen en gris en su parte faltante
	  Y las etiquetas muestran "recolectado/meta" actualizados

	Escenario: Actualizaci�n peri�dica autom�tica
	  Dado que estoy en la pantalla de juego
	  Cuando pasan exactamente 5 segundos
	  Entonces el gr�fico se actualiza autom�ticamente via JavaScript
	  Y se hace una petici�n AJAX para obtener el progreso m�s reciente
	  Y la animaci�n de barras muestra el cambio de progreso

	Escenario: Manejo de errores de actualizaci�n
	  Dado que estoy viendo el gr�fico
	  Cuando falla la petici�n de actualizaci�n autom�tica
	  Entonces el gr�fico mantiene los datos anteriores
	  Y se reintenta la actualizaci�n en el pr�ximo ciclo de 5 segundos
	  Y no se muestra error visible al usuario

	Escenario: Finalizaci�n del juego
	  Dado que todos los recursos alcanzaron sus metas
	  Cuando el gr�fico se actualiza
	  Entonces todas las barras se muestran 100% completas
	  Y despu�s de 2 segundos se redirige a la pantalla de resultados
	  Y el gr�fico desaparece

	Escenario: Barras completadas individualmente
	  Dado que uno o m�s recursos alcanzaron su meta
	  Cuando visualizo el gr�fico
	  Entonces las barras completas se muestran en color de �xito
	  Y las barras incompletas mantienen el color de progreso normal

### Requerimientos t�cnicos:
	- Usar Chart.js versi�n 3+ con animaciones suaves
	- Implementar peticiones AJAX cada 5 segundos usando setInterval
	- Manejar errores de red sin interrumpir la experiencia del usuario
	- Usar colores accesibles que distingan progreso, completo y pendiente
	- Implementar responsive design para el gr�fico en diferentes pantallas
	- Limpiar intervalos de JavaScript cuando se cambia de vista para evitar memory leaks
		



