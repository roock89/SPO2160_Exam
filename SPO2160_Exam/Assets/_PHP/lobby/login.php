<?php 
    session_start();
    require_once("../include/config.php");

    // check if the user is already logged in
    if(isset($_SESSION["loggedin"]) && !isset($_SESSION["loggedin"]) == true)
    {
        header("location: ../index.php");
        exit;
    }

    // Define variables and initialize
    $username = $password = "";
    $username_err = $password_err = "";

    // Processing form data when submitted
    if($_SERVER["REQUEST_METHOD"] == "POST") // checking if we have received data
    {
        // check if username is empty
        if(empty(trim($_POST["username"]))) // "username" is taken from the form
            $username_err = "*Please enter username";
        else
            $username = trim($_POST["username"]);
        // check if password is empty
        if(empty(trim($_POST["password"])))
            $password_err = "*Please enter password";
        else
            $password = trim($_POST["password"]);

        // validate credentials and try to log in
        if(empty($username_err) && empty($password_err))
        {
            // prepare sql statement
            $sql = "SELECT UserID, UserName, UserPassword FROM `RegisteredUsers` WHERE UserName = :username";
            
            if($stmt = $conn->prepare($sql))
            {
                $stmt->bindParam(":username", $param_username, PDO::PARAM_STR); // PDO::PARAM_STR means that the variable is a string
                $param_username = $username;

                if($stmt->execute())
                {
                    if($stmt->rowCount() == 1)
                    {
                        if($row = $stmt->fetch())
                        {
                            $id = $row["UserID"];
                            $username = $row["UserName"]; // not needed to retrieve username since we know its already correct
                            $hashed_password = $row["UserPassword"];
                            if(password_verify($password, $hashed_password))
                            {
                                // Password verified - start a new session
                                session_start();
                                // Store data in session variables
                                $_SESSION["loggedin"] = true;
                                $_SESSION["id"] = $id;
                                $_SESSION["username"] = $username;
                                
                                /** Prepare SQL statement, 
                                 ** UPDATE means to change already existing values in the database, while INSERT creates new values. */
                                $sql = "UPDATE `RegisteredUsers` SET UserLastActive = CURRENT_TIMESTAMP WHERE UserID = :ID";
                                if($stmt = $conn->prepare($sql)) 
                                {
                                    $stmt->bindParam(":ID", $param_ID, PDO::PARAM_INT);

                                    $param_ID = $id;

                                    if(!$stmt->execute())
                                    {
                                        echo "Something went wrong! Try again later";   
                                    }
                                }

                                // Redirect to welcome page
                                header("location: ../index.php");
                            } 
                            else
                            {
                                // Display an error message if password was not valid
                                $password_err = "*The password was not valid";
                            }
                        }
                    }
                }
            }
        }
    }
?>

<!DOCTYPE html>
<html>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <style>
        .center {
            /* width: 110%;
            height: 35%; */
            width: 110%;
            border: 5px solid #82c0e7;
            text-align: center;
            background-color: white;
        }   
        .background{
            background-image: url("https://media.discordapp.net/attachments/743859694346436628/885999670042718238/Kanye_komme_inn.jpg");
        }
        html, body {
            image-resolution: 100;
            height: 100%;
        }
        html {
            display: table;
            margin: auto;
        }
        body {
            display: table-cell;
            vertical-align: middle;
        }
    </style>
    <head>
        <meta charset="UTF-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Please login</title>
    </head>
    <body class="background">
        <div class="center">
            <form action="<?php echo htmlspecialchars($_SERVER["PHP_SELF"]); ?>" method="post">
                <h2>Login</h2>
                <p>Please fill in your credentials</p>
                <label>Username</label> <br />
                <input type="text" name="username" value="<?php echo $username; ?>"> <br />
                <p style="color:red;"><?php echo  $username_err; ?></p>
                <label>Password</label> <br />
                <input type="password" name="password"> <br />
                <p style="color:red;"><?php echo  $password_err; ?></p>
                <input type="submit" value="Login">
                <input type="reset" value="Clear"> <br />
                No account yet? <a href="./registeruser.php">Sign up now</a>
            </form>
        </div>
    </body>
</html>