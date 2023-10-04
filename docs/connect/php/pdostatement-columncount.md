---
title: "PDOStatement::columnCount"
description: "API reference for the PDOStatement::columnCount function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# PDOStatement::columnCount
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Returns the number of columns in a result set.  
  
## Syntax  
  
```  
  
int PDOStatement::columnCount ();  
```  
  
## Return Value  
Returns the number of columns in a result set. Returns zero if the result set is empty.  
  
## Remarks  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
  
```  
<?php  
$database = "AdventureWorks";  
$server = "(local)";  
$conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");  
  
$query = "select * from Person.ContactType";  
$stmt = $conn->prepare( $query );  
print $stmt->columnCount();   // 0  
  
echo "\n";  
$stmt->execute();  
print $stmt->columnCount();  
  
echo "\n";  
$stmt = $conn->query("select * from HumanResources.Department");  
print $stmt->columnCount();  
?>  
```  
  
## See Also  
[PDOStatement Class](../../connect/php/pdostatement-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
