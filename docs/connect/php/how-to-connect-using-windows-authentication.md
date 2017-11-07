---
title: "How to: Connect Using Windows Authentication | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "connecting to the server, Windows Authentication"
ms.assetid: f403a4e0-b0a8-4939-9dc1-e1209626367e
caps.latest.revision: 35
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# How to: Connect Using Windows Authentication
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

By default, the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] use Windows Authentication to connect to SQL Server. It is important to notice that in most scenarios, this means that the Web server's process identity or thread identity (if the Web server is using impersonation) is used to connect to the server, not an end-user's identity.  
  
The following points must be considered when you use Windows Authentication to connect to SQL Server:  
  
-   The credentials under which the Web server's process (or thread) is running must map to a valid SQL Server login in order to establish a connection.  
  
-   If SQL Server and the Web server are on different computers, SQL Server must be configured to enable remote connections.  
  
> [!NOTE]  
> Connection attributes such as *Database* and *ConnectionPooling* can be set when you establish a connection. For a complete list of supported connection attributes, see [Connection Options](../../connect/php/connection-options.md).  
  
Windows Authentication should be used to connect to SQL Server whenever possible for the following reasons:  
  
-   No credentials are passed over the network during authentication; user names and passwords are not embedded in the database connection string. This means that malicious users or attackers cannot obtain the credentials by monitoring the network or by viewing connection strings inside configuration files.  
  
-   Users are subject to centralized account management; security policies such as password expiration periods, minimum password lengths, and account lockout after multiple invalid logon requests are enforced.  
  
If Windows Authentication is not a practical option, see [How to: Connect Using SQL Server Authentication](../../connect/php/how-to-connect-using-sql-server-authentication.md).  
  
## Example  
Using the SQLSRV driver of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)], the following example uses the Windows Authentication to connect to a local instance of SQL Server. After the connection has been established, the server is queried for the login of the user who is accessing the database.  
  
The example assumes that SQL Server and the [AdventureWorks](http://go.microsoft.com/fwlink/?LinkID=67739) database are installed on the local computer. All output is written to the browser when the example is run from the browser.  
  
```  
<?php  
/* Specify the server and connection string attributes. */  
$serverName = "(local)";  
$connectionInfo = array( "Database"=>"AdventureWorks");  
  
/* Connect using Windows Authentication. */  
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
The following example uses the PDO_SQLSRV driver to accomplish the same task as the previous sample.  
  
```  
<?php  
try {  
   $conn = new PDO( "sqlsrv:Server=(local);Database=AdventureWorks", NULL, NULL);   
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
?>  
```  
  
## See Also  
[How to: Connect Using SQL Server Authentication](../../connect/php/how-to-connect-using-sql-server-authentication.md)  
[Programming Guide for PHP SQL Driver](../../connect/php/programming-guide-for-php-sql-driver.md)
[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)  
[How to: Create a SQL Server Login](http://go.microsoft.com/fwlink/?LinkId=106325)  
[How to: Create a Database User](http://go.microsoft.com/fwlink/?LinkId=106327)  
[Managing Users, Roles, and Logins](http://go.microsoft.com/fwlink/?LinkId=106329)  
[User-Schema Separation](http://go.microsoft.com/fwlink/?LinkId=106330)  
[Grant Object Permissions (Transact-SQL)](http://go.microsoft.com/fwlink/?LinkId=106332)  
  
