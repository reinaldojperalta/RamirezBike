[33mcommit 11a3b8de1f448f2765c4a1032abc98ddfd026372[m[33m ([m[1;36mHEAD[m[33m -> [m[1;32msteven[m[33m, [m[1;31morigin/steven[m[33m)[m
Merge: 240da1b 239787b
Author: stevenaraque <164960258+stevenaraque@users.noreply.github.com>
Date:   Sat Nov 8 20:32:22 2025 -0500

    Merge branch 'main' into steven

[33mcommit 240da1bfc211d075d7749c79a13b691ce8d03b32[m
Author: Steven <stevenalejandroaraquecastro@gmail.com>
Date:   Sat Nov 8 20:19:31 2025 -0500

    arreglo del merge xd

[33mcommit 239787b5e69a2a08626f69d180d9447ceabee95b[m
Author: Steven <stevenalejandroaraquecastro@gmail.com>
Date:   Sat Nov 8 19:22:17 2025 -0500

    Revert "Merge pull request #2 from reinaldojperalta/steven"
    
    This reverts commit 7c5d810a0aa7d6e605c149a11f2c715ff048095f, reversing
    changes made to e78c3ade164730964c20b55109dcf4bb73f812b5.

[33mcommit 7c5d810a0aa7d6e605c149a11f2c715ff048095f[m
Merge: e78c3ad eee0463
Author: stevenaraque <164960258+stevenaraque@users.noreply.github.com>
Date:   Sat Nov 8 18:07:41 2025 -0500

    Merge pull request #2 from reinaldojperalta/steven
    
    fusion de la funcionalidad de añadir carrito

[33mcommit eee04631298d31b7cb9be35465e14eead40ada86[m
Merge: ba5d818 e78c3ad
Author: stevenaraque <164960258+stevenaraque@users.noreply.github.com>
Date:   Sat Nov 8 18:06:30 2025 -0500

    Merge branch 'main' into steven

[33mcommit e78c3ade164730964c20b55109dcf4bb73f812b5[m
Merge: 771f0ef 744fee0
Author: JS-Tiago-B <joans_becerra@soy.sena.edu.co>
Date:   Sat Nov 8 17:39:41 2025 -0500

    Merge Tiago
    
    Implementacion De Registro De Usuario
    
    Se Ajusto El Web.Config Para La Validacion De Campos Vacios.
    Se Implemento Funcion Para Verificar Que No Hayan Registros Con El Mismo Documento.
    Se Implemento Logica De Roles.

[33mcommit 744fee0791a4e74b8285d2fceac32e2668eab268[m[33m ([m[1;31morigin/Tiago[m[33m)[m
Merge: 7285830 771f0ef
Author: Santiago Becerra <Santiago603m@gmail.com>
Date:   Sat Nov 8 17:03:35 2025 -0500

    Merge branch 'main' into Tiago

[33mcommit 72858306f63db87e2d2adb9ae90ced9f86c5fd2e[m
Author: Santiago Becerra <Santiago603m@gmail.com>
Date:   Sat Nov 8 16:42:21 2025 -0500

    Se añadieron validaciones en los campos del formulario y se edito el web.config para que pudieran funcionar las validaciones, si van hacer un merge tengan cuidado de dejar el archivo web.config como yo lo subi

[33mcommit ba5d8181a3b33de42dd9c7326800cb3e02b2d113[m
Author: Steven <stevenalejandroaraquecastro@gmail.com>
Date:   Fri Nov 7 22:22:09 2025 -0500

    en este commit se agrego en productodatos un nuevo metodo que es el que buscara el campo segun el id mediante una consulta luego corregi el metodo de logica que lo que ara es que no pase por un bucle y no consumira memoria inecesariamente sino inmediatamente arrojara los datos encontrados en la propiedad producto y si arroja null arrojara datos de prueba pero pues no llegara alla por que simepre para llegar a detalles se tiene que pasar por catalogo y asi terminaria la primera tarea de historia de usuario siguiente sera crear carrito.aspx

[33mcommit a78fda1e75686c4e2a854c46eba31d0e8d66c655[m
Merge: 6d84b8a 771f0ef
Author: Steven <stevenalejandroaraquecastro@gmail.com>
Date:   Fri Nov 7 21:24:06 2025 -0500

    merge branch 'main' of https://github.com/reinaldojperalta/RamirezBike into stevesss

[33mcommit 6d84b8a9ae7530c98a096f8b8e12f75332ca5600[m
Author: Steven <stevenalejandroaraquecastro@gmail.com>
Date:   Fri Nov 7 01:09:02 2025 -0500

    bueno en este commit e realizado el codigo de javascript para el carrito agrege funciones como cargar carrito añadir al carrito guardar carritoy actualizar numero la mas importante por asi decirlo es añadir al carrito que lo que hace es verificar si existe y si no lo agrega si existe lo aumenta en el contador de carrito lo mas importante es esto aun sigue siendo un borrador pero hice test y todo funciona con los datos de la bd lo proximo a ser es crear el asp de carrito y maquetar pagina pero por lo general funcionalidad va bien queda pendiente aclarar dudas con el grupo

[33mcommit 771f0ef0bf53f21b26c7ec2b7d4d5af6da1ad0e9[m
Author: reinaldo-peralta <rjperalta07@soy.sena.edu.co>
Date:   Thu Nov 6 20:17:57 2025 -0500

    se relizo la paginacion quedan pendiente los estados

[33mcommit 4028aa97edd45afc1385bf9ea2d2403f828b6fad[m
Author: Steven <stevenalejandroaraquecastro@gmail.com>
Date:   Thu Nov 6 20:03:29 2025 -0500

    primera prueba se creo una nueva rama y empece con la historia de usuario añadir producto al carrito desde la página de detalles en este commit se creo un boton de añadir del carito de prueba se añadio un metodo en logica y codigo en behind de detalle y se creo un archivo de carrito.js en el cual ira todo el codigo js del local storage y el toast lo siguiente commit sera codigo js

[33mcommit 6ad1f156556f1ef14a329dc9903a858f840dff5a[m
Author: Santiago Becerra <Santiago603m@gmail.com>
Date:   Wed Nov 5 21:48:26 2025 -0500

    Se Crearon Clases De Usuario En Las Capas Modelo,Logica Y Datos Y Se Creo La Pagina De Registrar Usuario En La Capa Vista

[33mcommit 6d3e17011978d42a1751e71a8665ad545de0a02d[m
Author: reinaldo-peralta <rjperalta07@soy.sena.edu.co>
Date:   Tue Nov 4 19:47:37 2025 -0500

    cambio irrelevante de .gitignore

[33mcommit 2ce35515655aa4939424ea1f6c9c65a8ec283d21[m
Author: reinaldo-peralta <rjperalta07@soy.sena.edu.co>
Date:   Tue Nov 4 19:26:11 2025 -0500

    nuevo intento de arreglar la subida del proyecto

[33mcommit a74fce43eb5fbbc7be10f40ed71d38a67e28c9c0[m
Author: reinaldo-peralta <rjperalta07@soy.sena.edu.co>
Date:   Tue Nov 4 19:18:44 2025 -0500

    se aplica git ignore definitivo y se limpio el repositorio en la rama Reinaldo

[33mcommit 986622b6122d093dd89f174db54c59ec36a1bb9b[m
Author: reinaldo-peralta <rjperalta07@soy.sena.edu.co>
Date:   Tue Nov 4 19:14:10 2025 -0500

    realize la conexion de vista y datos para el catologo

[33mcommit 86504bf3549c774568748d482a8929ee77c795fb[m
Author: Reinaldo Peralta <rjperalta07@soy.sena.edu.co>
Date:   Tue Nov 4 08:23:51 2025 -0500

    gitignore

[33mcommit cb6d7debb521fd13a1518f4d71044be36d325136[m
Author: Reinaldo Peralta <rjperalta07@soy.sena.edu.co>
Date:   Tue Nov 4 08:17:15 2025 -0500

    agrego la pag maestra con la intregacion de boostrap, y realizo al card de la pagina del catalogo

[33mcommit 179e8cec66c5cde750ccdac5687fe0e3de72c9b6[m
Author: Reinaldo Peralta <rjperalta07@soy.sena.edu.co>
Date:   Tue Nov 4 07:10:19 2025 -0500

    intento de arreglar el proyecto v2

[33mcommit 58e709fbf90249a0168d39d9e30da6de6ac7cfb1[m
Author: Reinaldo Peralta <rjperalta07@soy.sena.edu.co>
Date:   Tue Nov 4 06:46:58 2025 -0500

    intento de arreglar el proyecto

[33mcommit 6bc48e3bfdc28229a886d78528611c9b4ff80aa9[m
Author: reinaldo-peralta <rjperalta07@soy.sena.edu.co>
Date:   Mon Nov 3 21:49:54 2025 -0500

    la guia de asp xd

[33mcommit d75e300b944c1bc8ae8b9c886525128418069e3b[m
Author: reinaldo-peralta <rjperalta07@soy.sena.edu.co>
Date:   Mon Nov 3 21:03:55 2025 -0500

    realize el metodo de listar productos

[33mcommit 38e3d0f1bcfc38b7713ea82cef71d9531a81564b[m
Author: reinaldo-peralta <rjperalta07@soy.sena.edu.co>
Date:   Mon Nov 3 14:46:49 2025 -0500

    creando la clae de conexion

[33mcommit 9ce6b5694e9729d6c9c7e774b2d4f86e1a987bde[m
Merge: 97471cb 69cfc3e
Author: reinaldo-peralta <rjperalta07@soy.sena.edu.co>
Date:   Mon Nov 3 13:12:10 2025 -0500

    Merge branch 'Reinaldo' of https://github.com/reinaldojperalta/RamirezBike into Reinaldo

[33mcommit 97471cb4d7e6d4d8edea5e07eb9cdd65fc109b6c[m
Author: Reinaldo Peralta <rjperalta07@soy.sena.edu.co>
Date:   Fri Oct 31 07:23:45 2025 -0500

    subo el rar del esqueleto de la app por posible corrupcion, junto a un git ignore que intenta limpiar el git add para proximos commits

[33mcommit 69cfc3ee5a4faf2a1506e656c8832caed83e335c[m
Author: Reinaldo Peralta <rjperalta07@soy.sena.edu.co>
Date:   Fri Oct 31 07:18:42 2025 -0500

    agregando el git ignore

[33mcommit 9e02da391bda0c368ccb2a416bd919bb5f1d277c[m
Author: Reinaldo Peralta <rjperalta07@soy.sena.edu.co>
Date:   Fri Oct 31 07:13:48 2025 -0500

    cambio de prueba, creando Producto

[33mcommit 663bebf6886500083688e24acbbc1e8c78fd9d9d[m
Author: Reinaldo Peralta <rjperalta07@soy.sena.edu.co>
Date:   Fri Oct 31 07:10:58 2025 -0500

    commit prueba, creando ClConexion

[33mcommit a0951db448589fdbdd6f272242b5caa94750a2b3[m
Author: Reinaldo Peralta <rjperalta07@soy.sena.edu.co>
Date:   Fri Oct 31 07:08:04 2025 -0500

    cambio de prueba, creando ClConexion

[33mcommit b4d2190b64eaf6a011e8df441341ae656fd71029[m
Author: Reinaldo Peralta <rjperalta07@soy.sena.edu.co>
Date:   Fri Oct 31 06:52:30 2025 -0500

    subida del esqueleto
