<?php 
    // Database credentials. Use your own credentials for your database on hera.nord.no
    // Defining constants
    define("DB_SERVER", "localhost");
    define("DB_USERNAME", "344423");
    define("DB_PASSWORD", "Exams2021");
    define("DB_NAME", "student_344423");
    
    // Attempt to connect to database
    try {
        $conn = new PDO("mysql:host=" . DB_SERVER . ";dbname=" . DB_NAME, DB_USERNAME, DB_PASSWORD);

        // Set the PDO error mode to exceptions only
        $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    } catch (PDOException $e) {
        die("ERROR: Could not Connect. " . $e->getMessage());
    }
?>