<?php  //Todo lo que este entre estos brackets será php

	header("Access-Control-Allow-Origin: *");

	$con = mysqli_connect('remotemysql.com:3306', '******', '******', '******');

	if (mysqli_connect_errno()) //Si ocurre algún error en la conexión
	{
		echo "1: Connection failed"; //Esto es como el debug.log de php
		exit(); //Esto va a impedir que siga el resto del código.
	}

	$username = $_POST["username"];

	$CherryTomato = $_POST["CherryQuantity"];
	$mozzarella = $_POST["mozzarellaQuantity"];
	$OliveOil = $_POST["OliveOilQuantity"];
	$Sauce = $_POST["SauceQuantity"];


	$row = "SELECT inventory_player, inventory_item, quantity FROM inventory WHERE inventory_player = '". $username . "';";
	$rowrow = mysqli_query($con, $row) or die("2: Name check query failed");

	if (mysqli_num_rows($rowrow) == 0)
	{
		$INSERTinventory = "INSERT INTO inventory VALUES ('". $username . "', 'Cherry_Tomato', " . $CherryTomato . ");";
		mysqli_query($con, $INSERTinventory) or die("2: Insert fallo");

		$INSERTinventory = "INSERT INTO inventory VALUES ('". $username . "', 'Mozzarella', " . $mozzarella . ");";
		mysqli_query($con, $INSERTinventory) or die("2: Insert fallo");

		$INSERTinventory = "INSERT INTO inventory VALUES ('". $username . "', 'Olive_oil', " . $OliveOil . ");";
		mysqli_query($con, $INSERTinventory) or die("2: Insert fallo");

		$INSERTinventory = "INSERT INTO inventory VALUES ('". $username . "', 'Sauce', " . $Sauce . ");";
		mysqli_query($con, $INSERTinventory) or die("2: Insert fallo");

		exit();
	}
	
	$updatequery = "UPDATE inventory SET quantity = " . $CherryTomato . " WHERE inventory_item = 'Cherry_Tomato' AND inventory_player = '". $username . "' ;";
	mysqli_query($con, $updatequery) or die("Save query failed"); //Se hace la consulta a la base de datos con el update.

	$updatequery = "UPDATE inventory SET quantity = " . $mozzarella . " WHERE inventory_item = 'Mozzarella' AND inventory_player = '". $username . "' ;";
	mysqli_query($con, $updatequery) or die("Save query failed"); //Se hace la consulta a la base de datos con el update.

	$updatequery = "UPDATE inventory SET quantity = " . $OliveOil . " WHERE inventory_item = 'Olive_oil' AND inventory_player = '". $username . "' ;";
	mysqli_query($con, $updatequery) or die("Save query failed"); //Se hace la consulta a la base de datos con el update.

	$updatequery = "UPDATE inventory SET quantity = " . $Sauce . " WHERE inventory_item = 'Sauce' AND inventory_player = '". $username . "' ;";
	mysqli_query($con, $updatequery) or die("Save query failed"); //Se hace la consulta a la base de datos con el update.

	echo "0";


?>