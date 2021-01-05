# Level Design Document
## Leyenda:
**Posición para trampa, obstáculo o vacío:** son puntos de las salas donde puede aparecer una trampa, un obstáculo que impida el paso (una columna, una piedra gigante, lo que sea) o nada. Si este punto tiene una R, quiere decir que se escogerá una de estas 3 opciones aleatoriamente. Si tiene una C, aparecerá un obstáculo, y si tiene una T, aparecerá una trampa. Los siguientes iconos representan puntos de trampas. 

![image](https://user-images.githubusercontent.com/44704611/103681459-ca692600-4f87-11eb-8840-cc73ad74a035.png) ![image](https://user-images.githubusercontent.com/44704611/103681503-d48b2480-4f87-11eb-80cf-b0272e87fb7b.png)

**Assets decorativos:** son pequeños elementos distribuidos por las salas a modo de decoración. Algunos se pueden romper. El siguiente icono representa una fila de assets decorativos.

![image](https://user-images.githubusercontent.com/44704611/103681960-7d398400-4f88-11eb-927e-aba2c5b5dff3.png)

**Puertas:** comunican unas salas con otras. Normalmente las que se encuentran en las zonas inferiores de las salas son por las que entrará el jugador, mientras que las que están en los muros superiores, son aquellas por las que el jugador avanzará. Esto facilita el gameplay, pues al llegar a una nueva sala, el jugador verá con claridad la mayoría de los elementos de esta (no todos, pues las salas serán más grandes que lo que la cámara pueda abarcar) y además genera una sensación de progreso al avanzar, pues parece que el jugador se adentra cada vez más en la cueva. Los siguientes iconos representan puertas. Las puertas que se estén abajo o a la derecha en la imagen, se corresponden a puertas de entrada a la sala. Las puertas de arriba o a la izquierda representan las de salida.

![image](https://user-images.githubusercontent.com/44704611/103682309-0355ca80-4f89-11eb-85a0-51524352c7a5.png) ![image](https://user-images.githubusercontent.com/44704611/103682330-0d77c900-4f89-11eb-9c6e-7e2276106353.png) ![image](https://user-images.githubusercontent.com/44704611/103682374-1b2d4e80-4f89-11eb-88c6-67da1ecc4224.png) ![image](https://user-images.githubusercontent.com/44704611/103682415-254f4d00-4f89-11eb-8645-68df13b9a2f8.png)

## Lista de posibles assets decorativos:
**Nivel 1:** vasija, pico, roca, vagoneta.

**Nivel 2:** calaveras (se rompen como las vasijas), huesos enterrados, cabezas de algún monstruo (esto nos permite decorar reciclando material, pero igual es muy violento para lo que buscamos). Las rocas se pueden reutilizar, y los picos y vagonetas también, pero en mucha menor medida para que parezca que a este nivel llega mucha menos gente.

**Nivel 3:** formaciones de cristales mágicos, trozos de cristal sueltos (se pueden romper), reciclar calaveras, huesos y cabezas, pero nada de vasijas, picos o vagonetas, para que parezca que a este nivel no ha llegado nadie.

## Las salas en cuestión:
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

