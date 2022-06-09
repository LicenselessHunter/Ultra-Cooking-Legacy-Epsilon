<?php  //Todo lo que este entre estos brackets será php

	header("Access-Control-Allow-Origin: *");

	$con = mysqli_connect('remotemysql.com:3306', '******', '******', '******'); //Con el signo % es como se definen variables. En este caso, esta variable almacenara la conexión a nuestra base de datos. Se tendrán que pasar algunos parametros: El primero corresponde a la localización de la database. El segundo y tercer parametro será el usuario y contraseña y el último parámetro es el nombre de la database a conectar.

	//Ver si la conexión sucede
	if (mysqli_connect_errno()) //Si ocurre algún error en la conexión
	{
		echo "1: Connection failed"; //Esto es como el debug.log de php
		exit(); //Esto va a impedir que siga el resto del código.
	}

	//Estas variables obtendran los campos del mismo nombre escrito en unity. Estos los sacaremos del form con el POST.
	$username = $_POST["username"];
	$password = $_POST["password"];

	//Ver si el nombre existe
	$namecheckquery = "SELECT username FROM player WHERE username='" . $username . "';"; //Seleccionar valor de campo username de tabla players donde username = el valor de username (Que sera el que esta almacenado en el script de unity y que a su vez fue excrito en el campo de texto) (Los puntos se usan como concatenaciones, el parentesis solo cierra el otro parentesis solo).

	$namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed"); //Esta variable va a realizar el query del nombre. or die sería lo mismo que exit()

	if (mysqli_num_rows($namecheck) > 0)  //mysqli_num_rows obtiene el número de filas. En este caso estamos viendo la fila donde esta la consulta de namecheck. Si esto es mayor a 0, significa que al menos hay una fila que coincide con la consulta "namecheck" es decir, el nombre ya esta en la tabla.
	{
		echo "3: Name already exists";
		exit();
	}

	//Esta es una wea de seguridad de las contraseñas.
	$salt = "\$5\$rounds=5000\$" . "algo" . $username . "\$";
	$hash = crypt($password, $salt);
	//Agregar el usuario a la tabla.
	$insertuserquery = "INSERT INTO player (username, hash, salt) VALUES ('" . $username . "', '" . $hash . "', '" . $salt . "');";

	mysqli_query($con, $insertuserquery) or die("4: Insert user fallo"); //Consulta de inserción de usuario.

	echo "0"; //Esto me va a decir que todo el proceso de inserción de usuario fue exitoso.

?>