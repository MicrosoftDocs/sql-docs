---
title: "PDOStatement::bindColumn"
description: "API reference for the PDOStatement::bindColumn function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# PDOStatement::bindColumn
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Binds a variable to a column in a result set.  
  
## Syntax  
  
```  
  
bool PDOStatement::bindColumn($column, &$param[, $type[, $maxLen[, $driverdata ]]] );  
```  
  
#### Parameters  
$*column*: The (mixed) number of the column (1-based index) or name of the column in the result set.  
  
&$*param*: The (mixed) name of the PHP variable to which the column will be bound.  
  
$*type*: The optional data type of the parameter, represented by a PDO::PARAM_* constant.  
  
$*maxLen*: Optional integer, not used by the Microsoft Drivers for PHP for SQL Server.  
  
$*driverdata*: Optional mixed parameter(s) for the driver. For example, you could specify PDO::SQLSRV_ENCODING_UTF8 to bind the column to a variable as a string encoded in UTF-8.  
  
## Return Value  
TRUE if success, otherwise FALSE.  
  
## Remarks  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
This example shows how a variable can be bound to a column in a result set.  
  
```  
<?php  
$database = "AdventureWorks";  
$server = "(local)";  
$conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");  
  
$query = "SELECT Title, FirstName, EmailAddress FROM Person.Contact where LastName = 'Estes'";  
$stmt = $conn->prepare($query);  
$stmt->execute();  
  
$stmt->bindColumn('EmailAddress', $email);  
while ( $row = $stmt->fetch( PDO::FETCH_BOUND ) ){  
   echo "$email\n";  
}  
?>  
```  
  
## See Also  
[PDOStatement Class](../../connect/php/pdostatement-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
