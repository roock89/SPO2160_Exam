<?php 
    // require_once("./include/config.php");
    session_start();

    // check if user is already logged ni. if yes - redirect to welcome page
    if(!isset($_SESSION["loggedin"]))
    {
        header("location: ./lobby/login.php");
        exit;
    }
?>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="UTF-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Login Success</title>
    </head>
    <body>
        <video width="320" height="240" autoplay loop>
            <source src="./DUCK/the_duck.mp4" type="video/mp4">
            Your browser does not support the video tag.
        </video>
        <p><a href="./lobby/logout.php">logout</a></p>
        <p><a href="./Tank/unity/createTankGameLobby.php">Create Game</a></p>
        <p><a href="./Tank/unity/JoinTankGameLobby.php">Join Game</a></p>
        Welcome!
    </body>
</html>