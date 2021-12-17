<?php
   // define and init variables needed
   $username = $password = $confirm_password = "";
   $username_err = $password_err = $confirm_password_err = "";

   // connect to the database
   if($_SERVER["REQUEST_METHOD"] == "POST") 
   {
      // include the config file
       require_once("../include/config.php");

      // validate username
      if(empty(trim($_POST["username"])))
          $username_err = "Please enter a username"; 
      else {
         // check if username is in use
         $sql = "SELECT UserId FROM `RegisteredUsers` WHERE UserName = :username";
         if($stmt = $conn->prepare($sql)) 
         {
            $stmt->bindParam(":username", $param_username, PDO::PARAM_STR);
            $param_username = trim($_POST["username"]);

            if($stmt->execute()) 
            {
               if($stmt->rowCount() == 1) 
                  $username_err = "*This username is already taken";
               else
                  $username = trim($_POST["username"]);

            } else {
               echo "Oops! something went wrong.";
            }
         }
         // closing sql statement
         unset($stmt);
      }

      // validate password
      if(empty(trim($_POST["password"])))
         $password_err = "*Password can not be empty";
      elseif(strlen(trim($_POST["password"])) < 8)
         $password_err = "*Password must be at least 8 characters";
      else
      {
         $password = trim($_POST["password"]);
      }

      // validate confirm password
      if(empty(trim($_POST["confirm_password"])))
         $confirm_password_err = "*Please confirm the password";
      else
      {
         $confirm_password = trim(trim($_POST["confirm_password"]));
         if(empty($password_err) && ($password != $confirm_password))
            $confirm_password_err = "*Passwords do not match";
      }

      // when there is no errors, create the user
      if(empty($username_err) && empty($password_err) && empty($confirm_password_err))
      {
         $sql = "INSERT INTO `RegisteredUsers` (UserName, UserPassword) VALUES (:username, :password)";

         if($stmt = $conn->prepare($sql)) 
         {
            $stmt->bindParam(":username", $param_username, PDO::PARAM_STR);
            $stmt->bindParam(":password", $param_password, PDO::PARAM_STR);

            $param_username = $username;
            $param_password = password_hash($password, PASSWORD_DEFAULT);

            if($stmt->execute())
            {
               header("location: ./login.php");
               exit;
            } else {
               echo "Something went wrong! Try again later";
            }
         }
         unset($stmt);
      }         
   }
   unset($conn);

?>

<!DOCTYPE html>
<html>
   <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
   <style>
      .center {
         border: 5px solid #82c0e7;
         text-align: center;
         background-color: white;
      }   
      .background{
         background-image: url("https://images-ext-1.discordapp.net/external/UBbeF8TOJZCKMFPjY6qmhE6MH_g4IAwEKHpXy_XpNbs/https/media.discordapp.net/attachments/796341981387030538/888927641443401749/hotdog.gif");
         background-size: cover;
         background-repeat: repeat;
         background-position: center;
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
      <title>Sign Up</title>
   </head>
   <body class="background">
      <div class="center">
         <h2>Sign Up</h2>
         <p>  Please complete this form to create an account  </p>
         <form action="<?php echo htmlspecialchars($_SERVER["PHP_SELF"]); ?>" method="post">
            <label>Username</label> <br />
            <input type="text" name="username" value="<?php echo $username; ?>"> <br />
            <label>Password</label> <br />
            <input type="text" name="password" value="<?php echo $password; ?>"> <br />
            <label>Confirm Password</label> <br />
            <input type="text" name="confirm_password" value="<?php echo $confirm_password; ?>"> <br /><br />
            <input type="submit" value="Register">
            <input type="reset" value="Clear"> <br />
            <a href="./login.php">Already have an account?</a>
         </form>
         <?php
            if (!empty($username_err))
               echo  "<br /> <p style=\"color:red;\">" . $username_err . "</p>";
            elseif(!empty($password_err))
               echo "<br />  <p style=\"color:red;\">" . $password_err . "</p>";
            elseif (!empty($confirm_password_err))
               echo "<br />  <p style=\"color:red;\">" . $confirm_password_err . "</p>";
         ?>
      </div>
   </body>
</html>