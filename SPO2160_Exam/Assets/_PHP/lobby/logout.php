<?php 
    // Start session - again...
    session_start();

    // Unset the session variables
    $_SESSION = array();

    // Destroy the session
    session_destroy();

    // Redirect to login page
    header("location: ./login.php");
?>