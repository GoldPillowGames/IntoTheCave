![5](https://user-images.githubusercontent.com/44704611/104491410-7f2bc480-55d2-11eb-868a-574900f7831b.png)

# Documento de diseño 
## Visión general
### Tema  

La aventura, la mejora constante y la superación. 

### Género  

Rogue Lite, aventura y exploración. 

### Plataformas objetivo 

Navegador  Google Chrome, Mozilla Firefox 

### Modelo de monetización  

Estrategia: el objetivo es lanzar un videojuego del género Rogue-Lite que sea gratuito, con la posibilidad de adquirir expansiones de contenido (nuevos enemigos, objetos y escenarios) de pago, o contenido estético para el personaje principal mediante micropagos. Para lograr vender este contenido se pretenden utilizar 3 estrategias: 

Cebo y Anzuelo: el juego será de carácter gratuito con el fin de que más gente lo pruebe, y se enganche al juego. Será a partir de estos jugadores, que la empresa obtendrá beneficios de las futuras expansiones de pago que el juego recibirá, y de las figuras en 3D de los personajes que se venderán. 

Fidelización: el juego será de carácter gratuito y con una jugabilidad muy directa y sencilla de entender, pero que premia la mejora y el progreso como jugador. Esto hará que los jugadores quieran jugar más y traten de hacerlo cada vez mejor, conservándose así la base de jugadores en el tiempo. El contenido nuevo constante servirá de la misma forma para conservar a los jugadores durante un buen periodo de tiempo. 

Freemium: el juego será gratis, pero recibirá con el tiempo expansiones de pago, cuya compra será puramente opcional, y aquellos jugadores que decidan no comprarlas seguirán pudiendo jugar al juego base, pero se perderán una parte del contenido. Además, al añadir contenido nuevo con frecuencia, se mantiene el interés de los jugadores en el juego. 

Por último, se pretende obtener ingresos mediante la venta de figuras en 3D de los personajes del videojuego, caracterizándose estos por un gran carisma visual, que incentiva a su compra. 

  

Público objetivo: jugadores aficionados al género rogué-lite.  

  

Fuente de ingresos: dentro del juego se podrá adquirir contenido adicional, que podrá ser de 2 tipos: 

Contenido estético: este consiste en distintos cascos que el jugador podrá equiparse para cambiar su aspecto de forma notable. Algunos cascos se podrán adquirir jugando, mientras que otros se podrán desbloquear mediante micropagos. 

Expansiones de contenido: se trata de grandes añadidos de contenido con nuevos escenarios, objetos y enemigos. Para poder disfrutar de este contenido nuevo, que se irá incorporando a lo largo de 2 años, el jugador, deberá pagar por dichas expansiones, siendo cada expansión de pago único. No obstante, no pagar estas expansiones no impide seguir jugando al juego base. 

Figuras 3D: son figuras decorativas que podrán ser compradas por los jugadores. 

Libro de arte: este podrá ser adquirido de forma digital desde la propia plataforma de publicación del videojuego (itch.io) ya que está permite la venta de productos independientes en la propia página del videojuego, aunque este sea gratuito. El libro de arte tendrá un precio de 2,49€. 


### Canvas 
![Canvas](https://user-images.githubusercontent.com/44704611/104492148-869f9d80-55d3-11eb-945a-7ed86d2007d2.png)

### Caja de herramientas
![Diagrama toolkit into the cave](https://user-images.githubusercontent.com/44704611/104491930-33c5e600-55d3-11eb-916c-f25efcb40642.png)


## Alcance del proyecto 
### Tiempo y coste
- **Coste principal:** 65 € 
- **Tiempo:** 7 semanas 

### Equipo
| Nombre | Roles |
| ----------- | ----------- |
| Germán López Gutiérrez | Game Designer, Programador |
| Elvira Gutiérrez Bartolomé  | Arte 2D |
| Ignacio Atance Loras | Level Designer, Programador |
| Jorge Sánchez Sánchez | Arte 2D |
| Fernando Martín Espina | Balance Designer, Programador |


### Licencia y Hardware
| Nombre | Software / Licencia | Coste Total
| ----------- | ----------- | ----------- |
| Ignacio Atance Loras  | 1. Cakewalk | 0€ |
| Jorge Sánchez Sánchez  | 1. Clip Studio Paint<br/> | 45€ |
| Elvira Gutiérrez Bartolomé  | 1. Photoshop | 20€ |
| Germán López Gutiérrez  | 1. Gimp | 0€ |
| Fernando Martín Espina  | 1. Gimp | 0€ |
|  | **Coste Total**  | 65€ |

## Influencias
### Hades 
- **Medio:** Videojuego 
- **Motivo:** El sistema de mazmorras, dividas en habitaciones con enemigos que deben ser eliminados para avanzar.

### Little Big Planet 
- **Medio:** Videojuego 
- **Motivo:** Estilo de personajes.

### Moonlighter
- **Medio:** Videojuego 
- **Motivo:** el gameplay que consiste en explorar una mazmorra y luego utilizar lo que encuentres en ella para mejorar al personaje.

## Elevator Pitch 
Into the Cave es un videojuego del género Rogue-Lite para uno o dos jugadores, en el que se debe avanzar a través de distintas salas en una cueva, generadas procedimentalmente, derrotando a todos los enemigos que nos encontremos y haciéndose más fuertes en el proceso, ya que las salas serán cada vez más complicadas. Los jugadores contarán para ello con 3 armas distintas que podrán utilizar y la posibilidad de esquivar ataques. Es un juego fuertemente inspirado en juegos como Hades. El diseño de los personajes se inspira fuertemente en Little Big Planet, mientras que la estética se basa en juegos como Genshin Impact o The Legend of Zelda concluyendo en un resultado simpático y amigable para todos los públicos.  

## Descripción general del proyecto 
Se trata de un juego del género Rogue-Lite, en vista isométrica, en el que el jugador controla a un personaje el cual se encuentra en un campamento, cerca del cual hay una cueva. En el campamento el jugador puede mejorar su personaje, o cambiar su apariencia. Una vez entra a la cueva, esta estará formada de distintas salas llenas de enemigos, a los cuales debe derrotar. Una vez derrotados podrá pasar por una, de varias salidas, a la siguiente sala. A lo largo de las salas, se irá encontrando distintos objetos, los cuales podrán ser armas nuevas, objetos que apliquen pasivas, o habilidades. También cada cierto número de salas aparecerá un jefe, hasta llegar al jefe final y completar el juego.  
Las mejoras adquiridas en el campamento se mantienen para siempre. Las mejoras adquiridas dentro de la cueva se reinician al morir, o una vez completado el juego. 
El componente narrativo del videojuego se desarrolla hablando con los personajes del campamento, que pueden ser vendedores, u otros aventureros. Cada uno aportará información de valor para el contexto y la trama. 

## ¿Qué hace al juego especial? 
- El ritmo rápido de las partidas.  
- Una gran cantidad de objetos a conseguir y coleccionar. 
- Varias armas con las que jugar. 
- Cada partida cambia respecto a la anterior. 
- La posibilidad de jugar con un amigo.

## Historia 
### Sinopsis 
Se ha descubierto una misteriosa cueva, la cual cambia cada vez que alguien se adentra en ella. El protagonista es un aventurero que se ha propuesto llegar hasta el final de la misma y descubrir sus secretos. 

## Resumen
Se ha descubierto una misteriosa cueva que cambia cada vez que alguien entra en ella. Es una cueva llena de monstruos y tesoros, y nuestro personaje principal, un curioso y tranquilo caballero, es un aventurero que se ha propuesto llegar al final de ella y revelar todos sus secretos. Muchos comerciantes han establecido sus negocios a la entrada, y el personaje principal los utilizará para adquirir objetos útiles para su aventura.  

## ¿Cómo se cuenta la historia?
Al iniciar una nueva partida, se muestra una cinemática en el jugador en la que se ve al caballero encontrando un tesoro dentro de la cueva, y siendo rodeado por monstruos justo después. Tras este suceso, el personaje aparece en el campamento, entendiéndose así que, si se es derrotado dentro de la cueva, se vuelve a fuera y hay que volver a entrar, cambiando entonces la cueva.  

## Jugabilidad
Se trata de un juego del género Rogue-Lite, en vista isométrica, en el que el jugador controla a un personaje el cual se encuentra en un campamento, cerca del cual hay una cueva. En el campamento el jugador puede mejorar su personaje, o cambiar su apariencia. Una vez entra a la cueva, esta estará formada de distintas salas llenas de enemigos, a los cuales debe derrotar. Una vez derrotados podrá pasar por una, de varias salidas, a la siguiente sala. A lo largo de las salas, se irá encontrando distintos objetos, los cuales podrán ser armas nuevas, objetos que apliquen pasivas, o habilidades. También cada cierto número de salas aparecerá un jefe, hasta llegar al jefe final y completar el juego.  

Las mejoras adquiridas en el campamento se mantienen para siempre, o se utilizaran durante la siguiente partida en la cueva. Las mejoras adquiridas dentro de la cueva se reinician al morir, o una vez completado el juego. 

### Qué experiencia se busca
El objetivo es una experiencia de mejora y progreso constante para el jugador. según el jugador explora la cueva múltiples veces, comprobará como sus habilidades en combate mejoran, sintiéndose bien por ello. Al mismo tiempo, al mejorar a su personaje entre partida y partida, y al ir desbloqueando nuevos aspectos, el jugador sentirá que progresa dentro del juego, y que tiene incentivos para seguir jugando. Por su parte, el combate será dinámico y preciso, para que se sienta justo y divertido, siendo así una mecánica adictiva para el jugador. 

### Mecánicas principales
#### Avance por salas:
El juego consiste en ir avanzando entre salas, limpiándolas de enemigos y encontrando objetos y mejoras en el proceso para ser cada vez más fuerte. Las salas, a su vez, serán cada vez más grandes y tendrán más enemigos, debiendo hacer el jugador uso de las mejoras adquiridas para conseguir avanzar. 

#### Combate cuerpo a cuerpo (salvo que el personaje lleve ballesta)
Atacar: el jugador cuenta con un ataque cuerpo a cuerpo, con el que hacer daño a los enemigos que tiene enfrente de él. 
Bloquear: el jugador cuenta con la posibilidad de bloquear ataques. 
Esquivar: el jugador cuenta con la posibilidad de esquivar, consistiendo esto en un movimiento rápido en una dirección. 

#### Combate a distancia
Este se activa si el jugador cambia su escudo en algún momento por una ballesta. 
Se deja de poder bloquear, pero atacar ahora lanza una flecha en la dirección del ratón. 

#### Mejora del personaje dentro de la run
A lo largo de las salas, el jugador podrá ir encontrando mejoras, siendo estas nuevas armas, que podrá cambiar por el arma actual, mejoras pasivas, o nuevas habilidades. 

#### Mejora del personaje fuera de la run, en un campamento
Al comenzar a jugar, y al terminar cada run dentro de la cueva, el jugador irá a un campamento en la entrada de la cueva. En el campamento se pueden desbloquear estructuras con el dinero recaudado durante las runs. Dichas estructuras sirven para distintas cosas: 
Una tienda de ropa, en la que se podrá cambiar de aspecto, o mejorar ciertos atributos del personaje, antes de volver a entrar a la cueva. 
Un vestuario, en el que el jugador podrá cambiarse el casco (esto es únicamente estético). 
Una tienda de objetos, dirigida por 2 personajes, en la que se podrán comprar objetos para la siguiente run, como armas o pasivas. 
Habrá una joyería, en la que es podrán comprar anillos, los cuales otorgan pasivas permanentes. 
Una tienda de hechizos, en la que podrán comprar objetos, los cuales pasarán a aparecer de forma aleatoria en la cueva. 
Un tablón de corcho, con papeles pegados, que muestra las estadísticas de tu última run. 

#### Armas
- **Espada:** salvo que compres un arma en la tienda, esta será el arma que se utilizará al principio de la run (hasta que encuentre otra, y decida cambiar). Esta arma cuenta con un combo de 3 ataques, uno hacia un lado, otro hacia el lado contrario, y el último en estocada. Con la espada, el jugador cuenta también con un escudo. 
- **Escudo:** el jugador contará con él siempre que vayas con la espada. Pulsando un botón, el jugador se puede cubrir. 
- **Estoque:** es un arma de ataques rápidos, siendo estos únicamente estocadas. 
- **Ballesta:** el jugador la portará siempre que porte el estoque. Le sirve para atacar a distancia. Las flechas no se acaban. 
- **Lanza:** no cuenta con un arma secundaria, como el estoque o la espada, pero tiene mucho daño. Cuenta con 2 ataques, uno principal que es hacia delante y hace mucho daño, y uno en área que hace muy poco daño, pero empuja a los enemigos hacia atrás. 

#### Enemigos
- **Huesitos cuerpo a cuerpo:** se trata de un enemigo cuerpo a cuerpo, que perseguirá al jugador siempre que pueda. Tiene una vida y daño medios. 

- **Huesitos con arco:** mismo enemigo que el anterior, pero con un arco y ataca a distancia. 

- **Enemigo templario:** se trata de un enemigo cuerpo a cuerpo, que perseguirá al jugador siempre que pueda. Contará con una cantidad de vida relativamente alta (sin pasarse) debido a que es un caballero. 

- **Espectro:** se trata de un enemigo a distancia que atacará al jugador disparándole. 

#### Bosses
- **Roquita:** primer jefe del juego. Persigue al jugador para atacarle con sus 2 grandes manos, o bien salta para intentar aplastar al jugador. 

- **Criatura con 2 brazos y una esfera debajo del cuerpo para moverse:** segundo jefe del juego. 

- **Boss fijo al suelo:** se trata del boss final. Un boss de gran tamaño y fijo al suelo que ataca con los brazos. Deben esquivarse estos ataques, habiendo tiempo entre ellos para atacar, ya que se mueve de forma lenta. Puede haber ataques de 2 tipos. Uno en el que golpee el suelo con la mano y otro que coloque la mano encima del jugador para disparar un rayo láser. 

#### Escenario del campamento 

Se trata de un escenario predefinido en el que está la entrada a la cueva, y una serie de estructuras (descritas anteriormente) rodeando dicha entrada, formándose una especie de círculo en el centro para caminar, e ir a las distintas estructuras para interactuar con los personajes y comprar o equiparse cosas. El objetivo de que sea todo un círculo es que al jugador no le sea tedioso ir de un lado a otro, y que, si además si quiere ir directamente a jugar, pueda entrar a la cueva rápidamente sin demasiado recorrido. 

#### Escenarios en la cueva 
Las salas están generadas de antemano, cambiando únicamente el contenido de estas en cada run. Además, el jugador puede elegir entre una de varias salidas en cada sala, por lo que no verá todos los tipos de salas en cada partida. 
Por su parte, el contenido de las salas no será 100% aleatorio. Los elementos estáticos, como trampas o muros, tendrán asignados unos puntos de spawn en cada sala, implicando esto que en cada punto de spawn, o aparece una trampa o muro de cualquier tipo, o no aparece nada. Los enemigos por su parte también tendrán unas posibilidades de aparecer concretas. Por ejemplo, los enemigos a distancia siempre estarán alejados, y los enemigos cuerpo a cuerpo estarán cerca del jugador. 

#### Items que se pueden encontrar durante las partidas
A la hora de entrar en una habitación, se ve en la entrada un icono con el tipo de mejora que habrá en esa habitación, siendo estos tipos los siguientes: 
Item01: recupera un 5% de la vida del jugador. 
Item02: aumenta en un 5% el daño del jugador. 
Item03: aumenta la velocidad de movimiento del jugador en un 5%. 
Item04: aumenta el oro que el jugador recibirá de los enemigos un 10%. 
Item05: le da al jugador un 25% del oro que tiene en ese momento. 
Item06: aumenta el robo de vida del jugador en 5 puntos. 
Item07: cambia el arma del jugador a una lanza. 
Item08: cambia el arma del jugador a un estoque. 
Item09: potencia la habilidad especial del jugador. 
Item10: aumenta la suerte del jugador en un 5%. 
Item11: hace que los enemigos exploten. 
Item12: reduce la vida del jugador un 50%, aumenta su daño un 50%. 
Item13: reduce el daño del jugador un 50%, aumenta su vida un 50%. 
Item14: aumenta la agilidad del jugador en un 10%. 
Item15: permite al jugador sobrevivir a un ataque que de otra forma le hubiera derrotado. 
Item16: los enemigos pasan a tener una menor vida inicial. 
Item17: aumenta las curaciones del jugador. 
Item18: otorga una ventaja aleatoria. 
Item19: reduce el oro del jugador a 0, aumenta su vida y daño en un 100%. 
Item20: reduce el robo de vida en 3 puntos. Aumenta el daño un 10%. 
Item21: el jugador deja de poder rodar, pero aumenta su vida en un 75%. 
Item22: aumenta la fuerza del jugador al empujar enemigos un 10%. 
Item23: el jugador pasa a instanciar granadas cuando rueda. 
Item24: aumenta el umbral de vida a partir del cual el enemigo muere.

## Aspecto visual

### Referencias Visuales 

#### HADES 
Además de una referencia a niver jugable, el videojuego HADES se ha tomado como referencia visual a la hora de estructurar los distintos niveles por colores.

![image](https://user-images.githubusercontent.com/44704611/103665696-04303180-4f74-11eb-8f04-492159678403.png)

#### LittleBigPlanet 
El estilo de sus personajes, los Sackboys, son una referencia a nivel de diseño de personajes por su estilo cabezón y amigable.

![image](https://user-images.githubusercontent.com/44704611/103665737-11e5b700-4f74-11eb-9bcf-fbe00af1245e.png)

#### Hollow Knight 
Algunas zonas del Hollow Kinght han servido como referencia para la paleta de colores empleada, sobre todo para las del segundo y tercer nivel. 

![image](https://user-images.githubusercontent.com/44704611/103665771-1ad68880-4f74-11eb-92de-c2e019f78190.png)

#### Genshin Impact y The Legend of Zelda: Breath of the Wild 
Estos dos juegos se consideran una referencia debido al estilo cell shading de los mismos. Basándose en su estética, el videojuego cuenta con colores planos y sombras hechas siguiendo esta técnica. 

![image](https://user-images.githubusercontent.com/44704611/103665810-25911d80-4f74-11eb-80f9-40421252e61c.png) 

#### Shovel Knight 
En este caso tiene gran influencia su ambientacion caballeresca con personajes diferenciados entre si ya fuera por sus cascos o sus cuerpos estilizados. 

![image](https://user-images.githubusercontent.com/44704611/103665841-2de95880-4f74-11eb-9143-5feaa0857da8.png)
 
#### Paleta de color de referencia 
Para diseñar la paleta de color en la que se basará la ambientación del juego se ha seguido una progresión que irá variando entre niveles. De derecha a izquierda se pueden ir apreciando cómo se pasa de unos tonos tierras, más realistas, típicos de una mina en una montaña, a los colores oscuros que comienzan a entrar en los tonos azulados del segundo nivel. Para el tercer y último nivel, se toman como base colores muy saturados, muy brillantes, que hacen un juego entre colores rosados y turquesas. Con esta progresión se pretende transmitirle al jugador la sensación de alejamiento desde el mundo que conoce para llegar a un lugar mágico. 

![image](https://user-images.githubusercontent.com/44704611/103665893-3d68a180-4f74-11eb-9a01-4ff6cf906f04.png)
 
## Interfaz de Usuario 
### HUD 
En la interfaz del juego se mostrarán la barra de vida del jugador arriba a la izquierda, y encima de los enemigos sus correspondientes barras de vida. 

### Menú principal 
Tras mostrar el logo del estudio y un aviso sobre la epilepsia fotosensible, se muestra una pantalla en la que elegir el idioma del juego, después una pantalla en la que se puede elegir el brillo por último se muestra el menú principal con los botones de selección ubicados en la parte inferior izquierda de la pantalla ordenados en formato de lista, y noticias y novedades ubicadas en la parte central derecha de la pantalla.  

### Menú de opciones 
El menú de opciones cuenta con 3 pestañas: 

### General: para cambiar el idioma, y decidir si se muestra o no el contador de frames. 
Gráficos: se puede cambiar la calidad general de los gráficos, o cambiar aspectos concretos. Estos son las sombras, el antialiasing, la escala de la interfaz, la sincronización vertical, el límite de frames por segundo y la escala de renderizado. 

### Audio: permitirá modificar los volúmenes de la música y efectos de sonido por separado. 
Abajo habrá un botón para volver al menú principal. 

### Pantalla de créditos y contacto 
En la pantalla de créditos se mostrarán en scroll vertical los distintos roles que ha habido en el proyecto, y la persona o personas que los han desempeñado. 


## Assets necesarios 
### Diseños 2D 
#### Personaje principal (Turn around) 
Caballero cabezón, con casco medieval estilo cartoon y variaciones de color. 

#### Enemigos (Turn around) 
- Caballero Zombie 
- Huesitos 
- Espectro 
- Hiedra 
- Boss 1 (Gordo) 
- Boss 2 (Pelota) 
- Boss 3 (Haunter) 

#### NPCs (Turn Around) 
- Armero 
- Herrero 
- Maga 
- Joyera 
- Sastre 
- Caballero 1 
- Caballero 2 
- Caballero 3 

#### Props 

### Sonido 
#### Música: 
- Música de menú. 
- Música de Zona 1. 
- Música de Zona  2. 
- Música de la Zona 3. 
- Música del campamento. 

#### Efectos de sonidos: 
- Sonido para los botones. 
- Sonidos para los pasos. 
- Sonido para la espada. 
- Sonido para la flecha. 
- Sonido de objetos rotos 
- Sonidos ambientales 
- Sonidos de roca 

## Salas
### Leyenda:
**Posición para trampa, obstáculo o vacío:** son puntos de las salas donde puede aparecer una trampa, un obstáculo que impida el paso (una columna, una piedra gigante, lo que sea) o nada. Si este punto tiene una R, quiere decir que se escogerá una de estas 3 opciones aleatoriamente. Si tiene una C, aparecerá un obstáculo, y si tiene una T, aparecerá una trampa. Los siguientes iconos representan puntos de trampas. 

![image](https://user-images.githubusercontent.com/44704611/103681459-ca692600-4f87-11eb-8840-cc73ad74a035.png) ![image](https://user-images.githubusercontent.com/44704611/103681503-d48b2480-4f87-11eb-80cf-b0272e87fb7b.png)

**Assets decorativos:** son pequeños elementos distribuidos por las salas a modo de decoración. Algunos se pueden romper. El siguiente icono representa una fila de assets decorativos.

![image](https://user-images.githubusercontent.com/44704611/103681960-7d398400-4f88-11eb-927e-aba2c5b5dff3.png)

**Puertas:** comunican unas salas con otras. Normalmente las que se encuentran en las zonas inferiores de las salas son por las que entrará el jugador, mientras que las que están en los muros superiores, son aquellas por las que el jugador avanzará. Esto facilita el gameplay, pues al llegar a una nueva sala, el jugador verá con claridad la mayoría de los elementos de esta (no todos, pues las salas serán más grandes que lo que la cámara pueda abarcar) y además genera una sensación de progreso al avanzar, pues parece que el jugador se adentra cada vez más en la cueva. Los siguientes iconos representan puertas. Las puertas que se estén abajo o a la derecha en la imagen, se corresponden a puertas de entrada a la sala. Las puertas de arriba o a la izquierda representan las de salida.

![image](https://user-images.githubusercontent.com/44704611/103682309-0355ca80-4f89-11eb-85a0-51524352c7a5.png) ![image](https://user-images.githubusercontent.com/44704611/103682330-0d77c900-4f89-11eb-9c6e-7e2276106353.png) ![image](https://user-images.githubusercontent.com/44704611/103682374-1b2d4e80-4f89-11eb-88c6-67da1ecc4224.png) ![image](https://user-images.githubusercontent.com/44704611/103682415-254f4d00-4f89-11eb-8645-68df13b9a2f8.png)

### Lista de posibles assets decorativos:
**Nivel 1:** vasija, pico, roca, vagoneta.

**Nivel 2:** calaveras (se rompen como las vasijas), huesos enterrados, cabezas de algún monstruo (esto nos permite decorar reciclando material, pero igual es muy violento para lo que buscamos). Las rocas se pueden reutilizar, y los picos y vagonetas también, pero en mucha menor medida para que parezca que a este nivel llega mucha menos gente.

**Nivel 3:** formaciones de cristales mágicos, trozos de cristal sueltos (se pueden romper), reciclar calaveras, huesos y cabezas, pero nada de vasijas, picos o vagonetas, para que parezca que a este nivel no ha llegado nadie.

### Las salas en cuestión:
Respecto a las salas, hay que mencionar 2 detalles importantes. El primero es que estas deben ser imaginas en vista isométrica. Los dibujos recuerdan más a una vista aérea. Lo segundo que hay que mencionar es que estas salas podrían utilizarse también invertidas (en modo espejo, como quien dice). 

**Sala 1 (zona 1):** 

![image](https://user-images.githubusercontent.com/44704611/103682992-e53c9a00-4f89-11eb-87d7-5cb2937e0f8e.png)

**Sala 2 (zona 3):** 

![image](https://user-images.githubusercontent.com/44704611/103683050-fc7b8780-4f89-11eb-84a6-39e1c169fa45.png)

**Sala 3 (zona 2):**

![image](https://user-images.githubusercontent.com/44704611/103683115-10bf8480-4f8a-11eb-8cc1-e9f3aa202b29.png)

**Sala 4 (zona 3):**

![image](https://user-images.githubusercontent.com/44704611/103683238-35b3f780-4f8a-11eb-8a54-fece571b70f9.png)

**Sala 5 (zona 3):**

![image](https://user-images.githubusercontent.com/44704611/103683610-b2df6c80-4f8a-11eb-9438-b418e85baa10.png)

**Sala 6 (zona 1):**

![image](https://user-images.githubusercontent.com/44704611/103683955-241f1f80-4f8b-11eb-82fd-7895a6d9c6d5.png)

**Salas 7 (zona 2):**

![image](https://user-images.githubusercontent.com/44704611/103684001-3600c280-4f8b-11eb-8206-b0fbdb0557e9.png)

**Sala 8 (zona 2):**

![image](https://user-images.githubusercontent.com/44704611/103684023-3dc06700-4f8b-11eb-98da-3b80b1d8bcc6.png)

**Sala 9 (zona 2):**

![image](https://user-images.githubusercontent.com/44704611/103684049-4618a200-4f8b-11eb-916a-16ef2b39c4d7.png)

**Sala 10 (zona 2):**

![image](https://user-images.githubusercontent.com/44704611/103684137-5e88bc80-4f8b-11eb-86c3-4a8f438c7954.png)

**Sala 11 (zona 3):**

![image](https://user-images.githubusercontent.com/44704611/103684167-6d6f6f00-4f8b-11eb-87fc-01ca8b2af274.png)

**Sala 12 (zona 3):**

![image](https://user-images.githubusercontent.com/44704611/103684183-74967d00-4f8b-11eb-9f19-9d79dc3eb489.png)

**Sala 13 (zona 1):**

![image](https://user-images.githubusercontent.com/44704611/103684200-7b24f480-4f8b-11eb-9b24-be3eb66a8b42.png)

**Sala 14 (zona 1):**

![image](https://user-images.githubusercontent.com/44704611/103684224-8710b680-4f8b-11eb-9b2e-dca1e2c73c17.png)

**Sala 15 (zona 1):**

![image](https://user-images.githubusercontent.com/44704611/103684240-8ed05b00-4f8b-11eb-88e7-86636928081d.png)



## Código 
### General 
- AttackButton: controla el comportamiento de un botón de la interfaz que sirve para atacar. 
- AudioManager: contiene variables estáticas a las que se puede acceder desde otros scripts para reproducir música y efectos de sonido. 
- Bonfire: reproduce un efecto de sonido. 
- BrightnessMenuManager: cambia el brillo del juego en función del valor que asigne el jugador mediante un slider. 
- CameraController: gestiona el comportamiento de la cámara en función de su estado (4 estados posibles). 
- CameraFollower: mueve la cámara hacia el jugador con un lerp, cuando un booleano tiene valor verdadero. 
- CampController: en función de cuantas partidas haya jugado el jugador, se activan una o varias tiendas en el campamento. 
- CreditsController: contiene un método público que sirve para transicionar de un menú a otro. 
- CursorManager: le asigna al puntero del ratón el aspecto que debe tener. 
- DamageText: clase estática que permite instanciar texto que refleja el daño de los ataques.  
- DamageTextController: controla la animación del texto que refleja el daño de los ataques. 
- DeathMenuManager: contiene funciones para controlar el menú de muerte o derrota, tanto su aparición, como su desaparición al reiniciar la partida. 
- EnemyUIController: controla la barra de vida de los enemigos. 
- FadeManager: contiene funciones para reproducir fades de distintos tipos. 
- FirstTimeAnimatorController: muestra la cinemática inicial cuando el jugador inicia partida por primera vez. 
- GameManager: configura los gráficos del juego (elegidos por el jugador en el menú de opciones) y tiene un método que termina la partida y guarda los datos de forma persistente. 
- ItemSpawner: instancia un punto de spawn de elementos 
- LanguageManager: permite cambiar el idioma del videojuego. 
- LogoManager: gestiona la aparición del logo, el aviso sobre la epilepsia y el icono de la pantalla de carga, entre otras cosas. 
- MenuManager: gestiona todo lo relacionado con los menús. 
- MeshCombiner: optimiza ciertos mayados al combinarlos. 
- OnlineLobby: gestiona el funcionamiento de las salas en el modo multijugador. 
- PauseMenuManager: gestiona todo lo relacionado al menú de pausa. 
- PlayerHelmetController: gestiona lo relacionado a la apariencia del personaje del jugador. Concretamente cambia la estética de la cabeza del jugador. 
- PlayerObjectManager: 
- PlayerWeaponry: gestiona el arma que tiene equipada el jugador en cada momento. 
- PostProcess: gestiona el valor del post-proceso. 
- PostProcessManager: inicializa el post-proceso. 
- SettingsMenuManager: gestiona el comportamiento del menú de opciones. 
- TimeManager: gestiona el transcurso del tiempo en el videojuego. 
- Translator: gestiona el comportamiento de la selección de idioma. 
- UIElement: sirve para crear elementos de interfaz. Mueve el objeto en cuestión para que siempre apunte a la cámara y sea visible. 

### Scripts 
- SimpleCameraController: mueve la cámara en función de los controles del jugador. 

### Player 
- PlayerWeaponController: gestiona el comportamiento del ataque del jugador.  

### Patterns 
- State: estado genérico de la máquina de estados. 
- PoolObjectReturn: devuelve un objeto del pool de objetos. 
- ObjectPool: clase pensada para contener un grupo de objetos, de forma que estos no tengan que instanciarse y eliminarse cada vez que se van a usar.  
- FiniteStateMachine: controlador de la máquina de estados. Contiene una referencia a cada estado posible, y se encarga de tener activado uno y desactivar el resto. 

### Enemy 
- EnemyState: estado genérico de la máquina de estados de los enemigos. 
- EnemyController: controla la máquina de estados del enemigo. 

### Scripts/Enemy/SinCabeza 
- AttackState: estado del enemigo sin cabeza en el que ataca. 
- DeathState: estado del enemigo sin cabeza en el que está muerto 
- FollowingState: estado del enemigo sin cabeza en el que persigue al jugador. 
- OrbController: controlador del enemigo sin cabeza. 
- SinCabezaController: controlador del arma que porta el enemigo sin cabeza. 
- HurtState: estado del enemigo sin cabeza en el que le hieren. 

### Scripts/Enemy/Roquita 
- Attack1State: estado del enemigo Roquita en el que hace uno de sus ataques (tiene varios tipos). 
- Attack2State: estado del enemigo Roquita en el realiza su segundo ataque. 
- JumpAttackState: estado del enemigo Roquita en el realiza su ataque en salto. 
- RoarState: estado del enemigo Roquita en el que ruge. 
- DeathState: estado del enemigo Roquita en el que está muerto 
- FollowingState: estado del enemigo Roquita en el que persigue al jugador. 
- RoquitaAnimatorEvents: controla los posibles eventos (y activa sus animaciones) de Roquita. 
- RoquitaController: controla las posibles acciones de Roquita. 
- RoquitaHandController: controla la mano de Roquita. 
- RoquitaParticlesController: controla los efectos de particulas de Roquita. 

### Scripts/Enemy/Pinchitos 
- AttackCanonState: ataque a distancia del enemigo Pinchistos. 
- AttackMelee1State:  ataque cuerpo a cuerpo número 1 del enemigo Pinchitos. 
- AttackMelee21State: ataque cuerpo a cuerpo número 2 del enemigo Pinchitos. 
- AttackMelee22State: ataque cuerpo a cuerpo número 3 del enemigo Pinchitos. 
- DeathState: estado del enemigo Pinchistos, que se activa al ser derrotado. 
- FollowingState: estado del enemigo Pinchitos en el que persigue al jugador. 
- PinchitosAnimatorEvents: gestiona todos los posibles eventos del enemigo Pinchitos (y sus animaciones). 
- PinchitosController: controla todas las posibles acciones de Pinchitos.  
- SpikeBallController: controla todas las posibles acciones de la bola de Pinchitos. 
- StaticSpikeBallController: gestiona todo lo relacionado con la bola de pinchitos. 

### Scripts/Enemy/Litos 
- DeathState: estado del enemigo Litos, cuando es derrotado. 
- IdleState: estado del enemigo Litos cuando no está combatiendo. 
- LitosAnimatorEvents: gestiona los posibles eventos de Litos. 
- LitosController: controla las posibles acciones de Litos. 

Scripts/Enemy/Litos/SlapHand 
FollowingState: estado en el que la mano de Litos persigue al jugador. 
IdleState: estado neutral de la mano de Litos. 
LitosSlapHandController: controla lo relacionado a la mano de Litos.	 
RecoveringState: estado de la mano de Litos en el que se recupera tras atacar. 
SlapAttackState: estado de la mano de Litos en el que ataca.

Scripts/Enemy/Litos/LaserHand 
FollowingState: estado en el que la mano laser de Litos persigue al jugador. 
IdleState: estado neutral de la mano laser de Litos. 
LaserSlapHandController: controla lo relacionado a la mano laser de Litos.	 
RecoveringState: estado de la mano laser de Litos en el que se recupera tras atacar. 
LaserAttackState: estado de la mano laser de Litos en el que ataca.

### Enemy/HuesitosArcher 
- ArrowController: gestiona el comportamiento de las flechas. 
- BowController: gestiona que las flechas se inicialicen y muestren. 
- BowFiringState: estado del enemigo esqueleto arquero en el que dispara flechas. 
- BowPreparingState: estado de la flecha en el que está preparada para ser disparada. 
- DeathState: estado del enemigo esqueleto arquero muerto. 
- FollowingState: estado del enemigo esqueleto arquero en el que persigue al jugador. 
- HuesitosArcherController: controla el comportamiento del enemigo esqueleto arquero. 
- HurtState: estado del enemigo esqueleto arquero herido. 

### Enemy/Huesitos 
- AttackState: estado del enemigo esqueleto en el que ataca. 
- DeathState: estado del enemigo esqueleto en el que está muerto 
- FollowingState: estado del enemigo esqueleto en el que persigue al jugador. 
- HuesitosController: controlador del enemigo esqueleto. 
- HuesitosWeaponController: controlador del arma que porta el enemigo esqueleto. 
- HurtState: estado del enemigo esqueleto en el que le hieren. 

### Core 
- AgentPropeller: permite empujar en una dirección ciertos objetos. 
- (interface)ITargetFollower: contiene métodos para aquellos elementos que deban seguir un objetivo. 
- MethodDelayer: clase que permite establecer un tiempo de espera entre llamada y llamada a ciertos métodos. 
- NavMeshTargetFollower: le indica a un agente navmesh que persiga a un objetivo. 
- RandomStringSelector: clase que permite elegir un string aleatorio entre varios añadidos anteriormente. 
- RotateTowardsTarget: rota un GameObject alrededor de otro. 

### Game/Scripts 
- AnimationSoundController: contiene una función para reproducir el sonido de ataque. 
- AnimatorController: contiene funciones para gestionar las animaciones del personaje del jugador. 
- Audio: accede al audio manager para gestionar todo lo relacionado con la música y efectos de sonido. 
- CameraShaker: permite hacer temblar la cámara una cámara. 
- Config: permite guardar los datos de la partida, o resetear estos datos al estado inicial. 
- DialogueController: gestiona el sistema de diálogos. 
- EventTrigger: permite activar eventos de distintos tipos (de música, de batalla, de muerte...). 
- FPSCounter: se trata de un contador de fotogramas por segundo. 
- Interactable: se asigna a los objetos con los que el jugador puede interactuar. 
- MainMenuManager: gestiona el comportamiento del menú principal. 
- NetworkController: inicia la conexión para el juego multijugador. 
- TableReader: lee una línea concreta de la base de datos que contiene la información de los diálogos. 

### Game/Scripts/Camera 
- MovementOrientation: gestiona la rotación de la cámara principal. 
- Game/Scripts/Editor 
- EventTriggerEditor: modifica la apariencia en el inspector de un event trigger, en función de si este es de un tipo u otro. 
- Game/Scripts/Multiplayer 
- Launcher: gestiona lo referente al arranque del multijugador y a la creación de salas. 
- LauncherMenu: activa o desactiva el menú del multijugador. 
- LobbyMenuManager: contiene funciones para abrir o cerrar el menú  
- OnlineRoomManager: inicializa la sala multijugador, una vez se inicia la partida. 
- PlayerListItem: gestiona los objetos obtenidos por los jugadores a lo largo de la partida. 
- PlayerManager: instancia el objeto del player y el run manager. 
- RoomListItem: gestiona los objetos de la sala. 

### Game/Scripts/Player 
- PlayerController: gestiona todas las acciones posibles del jugador. 
- PlayerStatus: contiene todos los datos y estadísticas del jugador. 

### Game/Scripts/Run 
- Item: es la clase para los items que el jugador puede recoger, y le dan ventajas. 
- RoomManager: gestiona la sala en la que se encuentra el jugador. 
- RunManager: gestiona la partida que está jugando el jugador. 

### Game/Scripts/Run/Items 
- Item01: recupera un 5% de la vida del jugador. 
- Item02: aumenta en un 5% el daño del jugador. 
- Item03: aumenta la velocidad de movimiento del jugador en un 5%. 
- Item04: aumenta el oro que el jugador recibirá de los enemigos un 10%. 
- Item05: le da al jugador un 25% del oro que tiene en ese momento. 
- Item06: aumenta el robo de vida del jugador en 5 puntos. 
- Item07: cambia el arma del jugador a una lanza. 
- Item08: cambia el arma del jugador a un estoque. 
- Item15: permite al jugador sobrevivir a un ataque que de otra forma le hubiera derrotado. 
- Item16: los enemigos pasan a tener una menor vida inicial. 
- Item17: aumenta las curaciones del jugador. 
- Item22: aumenta la fuerza del jugador al empujar enemigos un 10%. 
- Item24: aumenta el umbral de vida a partir del cual el enemigo muere. 

### Game/Scripts/SaveGame 
- Helper: clase estática para escribir y leer los datos guardados del juego. 
- SaveManager: gestiona el guardado de datos, la lectura de estos, o su reseteo al estado inicial. 
- SaveState: guarda la configuración elegida por el jugador para el juego. 

### Game/Scripts/UI 
- UIController: gestiona la interfaz del jugador. 

### Game/Scripts/UI/Fade 
- Fade: permite ejecutar un efecto fade. 

## Animación 
### Personaje principal  
- Idle 
- Correr 
- Voltereta 
- Ataque 1 con espada 
- Ataque 2 con espada 
- Ataque especial con espada (cubrirse con escudo) 
- Ataque 1 con alabarda 
- Ataque 2 con alabarda 
- Ataque 1 con estoque 
- Ataque 2 con estoque 
- Ataque con ballesta 

### NPC hostil 1 – Huesitos cuerpo a cuerpo 
- Correr 
- Ataque 

### NPC hostil 2 – Huesitos con arco 
- Correr 
- Ataque 

### NPC hostil 3 – Templario 
- Correr 
- Ataque 

### NPC hostil 4 – Espectro 
- Moverse 
- Ataque 

### NPC hostil 5 – Roquita 
- Moverse 
- Saltar y aterrizar 
- Ataque con mano derecha 
- Ataque con mano izquierda 

### NPC hostil 6 – Pinchitos 
- Moverse 
- Disparar bola de pinchos 
- Ataque de derecha a izquierda 
- Ataque de izquierda a derecha 
- Ataque de abajo a arriba 

### NPC hostil 7 – Litos 
- Idle 
- Ataque con mano derecha (golpea el suelo) 
- Ataque con la mano izquierda (la coloca encima) 

### NPC de tienda – Armero 
- Idle 

### NPC de tienda – Herrero 
- Idle 

### NPC de tienda - Tintorero 
- Idle 

### NPC de tienda – Maga 
- Idle 

### Enlaces de interés 
https://goldpillowgames.github.io/  
https://goldpillowgames.itch.io/into-the-cave  
https://github.com/GoldPillowGames/practica-2   
https://twitter.com/GoldPillowGames  
https://www.instagram.com/goldpillowgames/?hl=es  
https://www.youtube.com/channel/UCn0IeU3ajap8zqTpq0sX6gQ  


## Calendario 
### Equipo
| Hitos | Fecha |
| ----------- | ----------- |
| Inicio del proyecto  | 23/11/2020 |
| Definición completa del juego   | 20/12/2020  |
| Se puede jugar una partida completa al juego (faltan elementos del juego final)  | 31/12/2020  |
| Se completa una versión del videojuego que permite a los testers probarlo y dar feedback. | 3/01/2021  |
| El juego está completo (faltan arreglos pequeños de bug)  | 11/01/2021  |
| Fin del proyecto (lanzamiento del videojuego)  | 13/01/2021  |

## Post mortem 

### Opiniones individuales: 

#### Opiniones sobre Jorge Sánchez Sánchez: 

Jorge: creo que he cumplido siempre en su mayoría los plazos de tiempo, con una calidad más que adecuada y me he adaptado con facilidad a todo aquello que se tuviera que hacer en último momento, corregir o eliminar por falta de tiempo. Lo malo se podría decir que sobre todo en el tema de diálogos me comprometí a echar una mano y ya fuera por falta de tiempo o por mala organización mía no le he dedicado el tiempo necesario aumentando la carga de trabajo al resto de los compañeros e incluso la falta de concept en props que sí que en su momento habrían hecho falta. 

Elvira: Gran artista. Las fechas se han llevado muy bien, la organización ha mejorado de forma considerable, el trabajo al día y cualquier cosa que te he pedido la has tenido en cuestión de horas. Todo genial. Nuestro flujo de trabajo y nuestra comunicación ha sido muy fluido y hemos llevado un muy buen ritmo. 

Fernando: Sin mucho que comentar porque no he trabajado mano a mano con él, pero se ha notado un incremento en el ritmo de trabajo con respecto al anterior proyecto. 

Germán: Ha tenido un rendimiento increíble y finalmente ha explotado el potencial que llevo intentando ver desde el 2º curso. Ha sido ágil y sus entregables han sido de una calidad impresionable. 

Ignacio: trabajo constante y a buen ritmo. Por norma general, buena comunicación. En alguna ocasión puntual, al menos por mi parte, no tenía muy claro que estaba haciendo. 

#### Opiniones sobre Elvira Gutiérrez Bartolomé: 

Jorge: Me parece una bestialidad la velocidad y la calidad que tiene de modelado una vez le proporciono turn arounds. Me siento muy a gusto con el flujo de trabajo que hemos tenido en la parte de artes y como nos hemos coordinado siempre con facilidad, gran apoyo artístico en el momento del concept. 

Elvira: He mantenido mi ritmo de trabajo constante y procurado no tener puntos bajos. Se ha sacado todo el trabajo posible adelante y los errores en el mismo se han corregido con rapidez. Como punto negativo, mi forma de comunicarme con los demás es un tanto cuestionable. 

Fernando: Sin mucho que comentar porque no he trabajado mano a mano con ella, aunque destacar en general un ritmo bastante rápido a la hora de hacer las tareas planteadas. 

Germán: Un trabajo espectacular. Es rápida, y pese a que a veces por las prisas hay imperfecciones, si se le indican las arregla al instante. Un muy buen trabajo. Se nota la diferencia de su trabajo en 2D al 3D. 

Ignacio: un gran trabajo, constante, a buen ritmo y de calidad. Buena comunicación con los compañeros. Nada negativo destacable. 

#### Opiniones sobre Fernando Martín Espina: 

Jorge: A nivel de programación no puedo opinar mucho, aunque sí que sería de agradecer que hubiera cierta mayor organización, que en mi opinión sí que podríamos haber tenido ciertas IAs con algo más de tiempo. Aun así, las ias me molan como van pese a este problema de plazos de tiempo. 

Elvira: Aunque se le vio ausente en los primeros días, luego ha llevado un muy buen desempeño. No he trabajado directamente con él, pero no tengo ninguna queja de su trabajo y de sus resultados. Ha hecho un gran trabajo. 

Fernando: Durante la primera semana me dediqué a investigar y documentarme sobre cómo realizar un buen código escalable y leyendo sobre patrones de diseño, convenciones de código… Debido a ello y sumado a una ligera dejadez durante el principio del proyecto, los resultados mostrados durante ese tiempo no fueron los esperados. A partir de ese momento he mantenido un ritmo más constante. El hecho de tener que hacer todas las IAs se me atragantó y perdí un poco la motivación hacia el principio-mitad del desarrollo, pero luego la recuperé. 

Germán: Tengo sentimientos encontrados. Al inicio me pareció que su parte avanzaba demasiado lenta, como si no estuviese poniendo ganas, y desconociendo los motivos o si fue realmente así o no, ha cumplido con sus tareas en las últimas semanas. Quizás se hubiese agradecido un poco más de participación en otras áreas, como en el game design, ya que era una de las cosas que quería hacer en un inicio. En cuanto a la calidad del código, este es de muchísima calidad. 

Ignacio: buen trabajo y de calidad. Lo he notado algo distante al principio del proyecto, tanto en cuanto a cantidad de trabajo realizado como en cuanto a comunicación. Al final, por el contrario, ha sido excelente. 

#### Opiniones sobre Germán López Gutiérrez: 

Jorge: Brutal apoyo en el trabajo y a nivel artístico. Gran compañero de tras-nochamientos mientras trabajamos hasta las mil, me parece genial la calidad de trabajo que ha habido en sus partes y de quitarse el sombrero la funcionalidad e las ideas que me ha dado con los elementos artísticos y a nivel de juego, como los diálogos o las viñetas. TRABAJAS DEMASIADO Y DUERMES POCO (menos que yo a veces y ya es decir). 

Elvira: Ha trabajado muchísimo más de lo que debería, y aunque alucinante, se ha dejado la salud en el proceso haciendo el trabajo de dos programadores en vez de uno. Su comunicación con los demás y su forma de dar feedback ha mejorado muchísimo. Debería cuidar más su salud. 

Fernando: Sin duda la persona que más horas le ha echado al proyecto. Es increíble todo el trabajo que ha realizado, desde todas las animaciones hasta gran parte del código del juego. En la parte negativa, en general la organización dentro del proyecto de Unity podría haber sido mejor: scripts sin usar, códigos muy largos sin descomponer, scripts arrastrados desde el prototipo… En general un código poco legible. 

Germán: Considero haber sido rápido y, aunque me he encargado de hacer un número de tareas excesivo en mi opinión, he sido capaz de llevarlas a cabo. Además, considero que he organizado bien al grupo, especialmente a los artistas. Me gustaría que hubiese habido una distribución de tareas mucho más equitativa, pero teniendo en cuenta los conocimientos y aptitudes de cada uno, no quedaba más remedio que hacerlo de esta forma. Eso sí, me hubiese gustado poder haber tenido más tiempo para el game design. 

Ignacio: Muchísimo trabajo y de calidad. A veces me costaba saber en qué estaba trabajando, aunque esto es en mayor parte culpa mía, y también era debido a la enorme cantidad de cosas distintas que hacía. 

#### Opiniones sobre Ignacio Atance Loras: 

Jorge: Aunque diga que ha tenido ciertos problemas de continuidad en su trabajo, al menos eso le oí comentar en ocasiones, me parece que ha tenido un gran trabajo y a muy buen nivel en la parte musical y a nivel de level design. Gran Scrum máster, la verdad que sí que es de agradecer la labor de escritura durante los dailys y que siempre esté atento en que se realicen los dailys y los sprint reviews. 

Elvira: Gran trabajo como compositor, level designer y scrum máster. Siempre dispuesto a organizar con eficacia y a llevar un seguimiento diario muy útil y necesario de la metodología de trabajo. Además, es muy fácil comunicarse con él. 

Fernando: Es admirable el hecho de haber tenido que aprender a utilizar un software de realización de música desde cero y crear varias pistas dinámicas para cada una de las partes del juego. Por otra parte, aparte del diseño de niveles, se habría agradecido algo más de apoyo por su parte en la parte de programación. 

Germán: Pese a algunas dificultades que le han surgido, ha cumplido con su parte y pese a que me hubiese gustado ver más participación en otras áreas del desarrollo como es el campo de la programación, soy consciente de la situación personal y ha hecho un buen trabajo, sobretodo el sonoro, donde la calidad de la banda sonora es muy buena teniendo en cuenta la temática del videojuego. 

Ignacio: Hice buen trabajo y comunicación, constantes entregas de material y disponibilidad para ayudar al resto de compañeros. Sobre todo, en la última semana (aunque también en momentos puntuales por motivos personales que no estuvieron todo lo bien gestionados que deberían). Al centrarme mucho en mis roles (level designer, encargado de música y sonido, encargado de la documentación...) esta última semana ha podido ayudar menos de lo esperado a quienes más lo necesitaban. 

 

### ¿Qué ha ido bien? 

La organización y flujo de trabajos han sido mucho mejor que en proyectos anteriores debido a lo aprendido, y al nivel de seriedad y compromiso con el que se ha afrontado el desarrollo del videojuego. Desde el inicio se dejaron claros los conceptos y las ideas, con el objetivo de evitar cambios o ideas poco concisas en el futuro. Debido al conocimiento de Unity previo o proveniente del autodidactismo, el desarrollo del proyecto a nivel técnico ha sido extremadamente ágil para la cantidad de contenido planeado a realizar. 

### Flujo de correcciones:  

Debido a problemas iniciales con la comunicación (concretamente con la falta de esta), se comenzaron a hacer daily stand-ups, lo cual nos ayudó a fijar el rumbo del desarrollo del proyecto y mejorar el flujo de trabajo. 

### Feedback o retroalimentación externa: 

Tras permitir a los testers probar el videojuego en su versión casi acabada, les fue enviado un cuestionario para que pudieran contar sus conclusiones de forma anónima. 

Este cuestionario se iniciaba preguntando por el dispositivo de juego (ordenador o móviles) como por el navegador utilizado. Después se les pedía que explicaran si habían tenido problemas de rendimiento, y en último lugar se les preguntaba por errores dentro del juego y problemas de balance (daño, vida, oro...) que los testers hubieran encontrado. 

En total fueron 9 personas las que probaron el juego, y se encontró que todas optaban por la versión de ordenador frente a la de móvil, y que los navegadores más usados fueron Chrome, Firefox, y Opera GX. Esto es importante, pues Opera GX es un navegador cada vez más extendido entre jugadores de videojuego, pero no es un navegador contemplado por los creadores de webgl Unity, lo que puede dar lugar a conflictos. 

Respecto a problemas de rendimiento, no se reportó ninguno de gravedad, siendo todos ligeras diferencias en el framerate esperado y el obtenido (se esperaban 60 frames frente a los 50-40 que obtuvieron algunos jugadores con los gráficos a máxima calidad). 

En cuanto al balance, muchos jugadores coincidieron en que la cantidad de vida inicial del jugador era demasiado alta, por lo que esta fue reducida.  

Por último, respecto a errores del videojuego, todos fueron errores de poca gravedad y que no afectaban de forma drástica a la experiencia y no impedían disfrutar de la misma (a excepción de uno que permitía al jugador abandonar el escenario de juego). Todos estos errores se han corregido con el objetivo de mejorar la experiencia de los jugadores. 

### ¿Qué se podría haber mejorado? 

Debido al dinamismo con el que hemos afrontado el trabajo (cada persona se organizaba como consideraba, siempre intentando cumplir con los plazos) en general el trabajo de cada uno ha ido con altibajos y no con constancia, como sería lo ideal. 

