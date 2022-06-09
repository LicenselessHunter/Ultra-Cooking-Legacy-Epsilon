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
	$namecheckquery = "SELECT username, salt, hash FROM player WHERE username='" . $username . "';"; 

	$namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed"); 

	if (mysqli_num_rows($namecheck) != 1)  //mysqli_num_rows obtiene el número de filas. En este caso estamos viendo la fila donde esta la consulta de namecheck. 
	{
		echo "5: No hay un usuario con el nombre o hay más de uno";
		exit();
	}	



	//get login info from query
	//Necesitamos obtener el salt y hash de la database y ver si la password escrita encriptada con el mismo salt resulta en el mismo hash. Si esto es así, entonces significa que se uso el mismo password en el registro y login por lo que es bueno. 
	$existinginfo = mysqli_fetch_assoc($namecheck); //Esto va a poner en un array los valores de namecheck, por lo que un índice tenrá el username, otro tendrá el hash, etc.
	$salt = $existinginfo["salt"]; //Aqui se agrega en una variable el salt que obtenemos de la consulta de namecheck.
	$hash = $existinginfo["hash"]; //Aqui se agrega en una variable el hash que obtenemos de la consulta de namecheck.

	$loginhash = crypt($password, $salt);
	if ($hash != $loginhash) //Si el hash de la database no el mismo que el de loginhash
	{
		echo "6: contraseña incorrecta";
		exit();
	}

	//Si se logea correctamente.

	echo "0"

?>