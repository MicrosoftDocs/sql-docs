---
title: "PDOStatement::columnCount"
description: "API reference for the PDOStatement::columnCount function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
ms.custom: ""
ms.date: "08/10/2020"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 8d89a568-0c7c-40dd-9f54-db7313600df3
author: David-Engel
ms.author: v-daenge
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
  
