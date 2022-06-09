<?php  //Todo lo que este entre estos brackets será php

	header("Access-Control-Allow-Origin: *");

	$con = mysqli_connect('remotemysql.com:3306', '******', '******', '******');

	if (mysqli_connect_errno()) //Si ocurre algún error en la conexión
	{
		echo "1: Connection failed"; //Esto es como el debug.log de php
		exit(); //Esto va a impedir que siga el resto del código.
	}

	$username = $_POST["username"];

	$row = "SELECT playerComplete, levelComplete, quantity FROM complete_level WHERE playerComplete = '". $username . "';";
	$VictoriasRow = mysqli_query($con, $row) or die("2: No se almaceno username");

	if (mysqli_num_rows($VictoriasRow) == 0)
	{
	
		exit("No hay estadisticas que recuperar");
	}


	$existinginfovictory = mysqli_fetch_assoc($VictoriasRow);


	echo "0\t" . $existinginfovictory["quantity"];

?>