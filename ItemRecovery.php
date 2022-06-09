<?php  //Todo lo que este entre estos brackets será php

	header("Access-Control-Allow-Origin: *");

	$con = mysqli_connect('remotemysql.com:3306', '******', '******', '******');

	if (mysqli_connect_errno()) //Si ocurre algún error en la conexión
	{
		echo "1: Connection failed"; //Esto es como el debug.log de php
		exit(); //Esto va a impedir que siga el resto del código.
	}

	$username = $_POST["username"];

	$row = "SELECT inventory_player, inventory_item, quantity FROM inventory WHERE inventory_player = '". $username . "';";
	$rowrow = mysqli_query($con, $row) or die("2: No se almaceno username");

	if (mysqli_num_rows($rowrow) == 0)
	{
	
		exit("No hay items que recuperar");
	}

	$CherryTomatoRow = "SELECT inventory_player, inventory_item, quantity FROM inventory WHERE inventory_item = 'Cherry_Tomato' AND inventory_player = '". $username . "';";
	$mozzarellaRow = "SELECT inventory_player, inventory_item, quantity FROM inventory WHERE inventory_item = 'Mozzarella' AND inventory_player = '". $username . "';";
	$OliveOilRow = "SELECT inventory_player, inventory_item, quantity FROM inventory WHERE inventory_item = 'Olive_oil' AND inventory_player = '". $username . "';";
	$SauceRow = "SELECT inventory_player, inventory_item, quantity FROM inventory WHERE inventory_item = 'Sauce' AND inventory_player = '". $username . "';";

	$checkTomato = mysqli_query($con, $CherryTomatoRow) or die("2: Select tomate fallo"); 
	$checkMozzarella = mysqli_query($con, $mozzarellaRow) or die("2: Select Mozzarella fallo"); 
	$checkOliveOil = mysqli_query($con, $OliveOilRow) or die("2: Select OliveOil fallo"); 
	$checkSauce  = mysqli_query($con, $SauceRow) or die("2: Select Sauce fallo");

	$existinginfo1 = mysqli_fetch_assoc($checkTomato);
	$existinginfo2 = mysqli_fetch_assoc($checkOliveOil);
	$existinginfo3 = mysqli_fetch_assoc($checkMozzarella);
	$existinginfo4 = mysqli_fetch_assoc($checkSauce);


	echo "0\t" . $existinginfo1["quantity"] . "\t" . $existinginfo2["quantity"] . "\t" . $existinginfo3["quantity"] . "\t" . $existinginfo4["quantity"];

?>