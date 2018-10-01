---
title: "How to: Connect Using SQL Server Authentication | Microsoft Docs"
ms.custom: ""
ms.date: "03/26/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "connecting to the server, SQL Server Authentication"
ms.assetid: 8d298830-3186-47e7-aef6-586b457901c1
author: MightyPen
ms.author: genemi
manager: craigg
---
# How to: Connect Using SQL Server Authentication
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

The [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] supports SQL Server Authentication when you connect to SQL Server.  
  
SQL Server Authentication should be used only when Windows Authentication is not possible. For information about connecting with Windows Authentication, see [How to: Connect Using Windows Authentication](../../connect/php/how-to-connect-using-windows-authentication.md).  
  
The following points must be considered when you use SQL Server Authentication to connect to SQL Server:  
  
-   SQL Server Mixed Mode Authentication must be enabled on the server.  
  
-   The user ID and password (*UID* and *PWD* connection attributes in the SQLSRV driver) must be set when you try to establish a connection. The user ID and password must map to a valid SQL Server user and password.  
  
> [!NOTE]  
> Passwords that contain a closing curly brace (}) must be escaped with a second closing curly brace. For example, if the SQL Server password is "pass}word", the value of the *PWD* connection attribute must be set to "pass}}word".  
  
The following precautions should be taken when you use SQL Server Authentication to connect to SQL Server:  
  
-   Protect (encrypt) the credentials passed over the network from the Web server to the database. Credentials are encrypted by default beginning in SQL Server 2005. For added security, set the Encrypt connection attribute to "on" in order to encrypt all data sent to the server.  
  
> [!NOTE]  
> Setting the Encrypt connection attribute to "on" can cause slower performance because data encryption can be computationally intensive.  
  
-   Do not include values for the connection attributes *UID* and *PWD* in plain text in PHP scripts. These values should be stored in an application-specific directory with the appropriate restricted permissions.  
  
-   Avoid use of the *sa* account. Map the application to a database user who has the desired privileges and use a strong password.  
  
> [!NOTE]  
> Connection attributes besides user ID and password can be set when you establish a connection. For a complete list of supported connection attributes, see [Connection Options](../../connect/php/connection-options.md).  
  
## Example  
The following example uses the SQLSRV driver with SQL Server Authentication to connect to a local instance of SQL Server. The values for the required *UID* and *PWD* connection attributes are taken from application-specific text files, *uid.txt* and *pwd.txt*, in the *C:\AppData* directory. After the connection has been established, the server is queried to verify the user login.  
  
The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the browser when the example is run from the browser.  
  
```  
<?php  
/* Specify the server and connection string attributes. */  
$serverName = "(local)";  
  
/* Get UID and PWD from application-specific files.  */  
$uid = file_get_contents("C:\AppData\uid.txt");  
$pwd = file_get_contents("C:\AppData\pwd.txt");  
$connectionInfo = array( "UID"=>$uid,  
                         "PWD"=>$pwd,  
                         "Database"=>"AdventureWorks");  
  
/* Connect using SQL Server Authentication. */  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
if( $conn === false )  
{  
     echo "Unable to connect.</br>";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Query SQL Server for the login of the user accessing the  
database. */  
$tsql = "SELECT CONVERT(varchar(32), SUSER_SNAME())";  
$stmt = sqlsrv_query( $conn, $tsql);  
if( $stmt === false )  
{  
     echo "Error in executing query.</br>";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Retrieve and display the results of the query. */  
$row = sqlsrv_fetch_array($stmt);  
echo "User login: ".$row[0]."</br>";  
  
/* Free statement and connection resources. */  
sqlsrv_free_stmt( $stmt);  
sqlsrv_close( $conn);  
?>  
```  
  
## Example  
This sample uses the PDO_SQLSRV driver to demonstrate how to connect with SQL Server Authentication.  
  
```  
<?php  
   $serverName = "(local)";   
   $database = "AdventureWorks";  
  
   // Get UID and PWD from application-specific files.   
   $uid = file_get_contents("C:\AppData\uid.txt");  
   $pwd = file_get_contents("C:\AppData\pwd.txt");  
  
   try {  
      $conn = new PDO( "sqlsrv:server=$serverName;Database = $database", $uid, $pwd);   
      $conn->setAttribute( PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION );   
   }  
  
   catch( PDOException $e ) {  
      die( "Error connecting to SQL Server" );   
   }  
  
   echo "Connected to SQL Server\n";  
  
   $query = 'select * from Person.ContactType';   
   $stmt = $conn->query( $query );   
   while ( $row = $stmt->fetch( PDO::FETCH_ASSOC ) ){   
      print_r( $row );   
   }  
  
   // Free statement and connection resources.   
   $stmt = null;   
   $conn = null;   
?>  
```  
  
## See Also  
[How to: Connect Using SQL Server Authentication](../../connect/php/how-to-connect-using-sql-server-authentication.md)

[Programming Guide for the Microsoft Drivers for PHP for SQL Server](../../connect/php/programming-guide-for-php-sql-driver.md)

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)

[SUSER_SNAME (Transact-SQL)](../../t-sql/functions/suser-sname-transact-sql.md)

[How to: Create a SQL Server Login](../../relational-databases/security/authentication-access/create-a-login.md)

[How to: Create a Database User](../../relational-databases/security/authentication-access/create-a-database-user.md)

[Managing Users, Roles, and Logins](../../relational-databases/server-management-objects-smo/tasks/managing-users-roles-and-logins.md)

[User-Schema Separation](../../relational-databases/server-management-objects-smo/tasks/managing-users-roles-and-logins.md)

[Grant Object Permissions (Transact-SQL)](../../t-sql/statements/grant-object-permissions-transact-sql.md)  
  
