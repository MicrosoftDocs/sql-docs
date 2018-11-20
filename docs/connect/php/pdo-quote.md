---
title: "PDO::quote | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: ab9ddc48-42f8-4edf-aa8b-b0fc66706161
author: MightyPen
ms.author: genemi
manager: craigg
---
# PDO::quote
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Processes a string for use in a query by placing quotes around the input string as required by the underlying SQL Server database. PDO::quote will escape special characters within the input string using a quoting style appropriate to SQL Server.  
  
## Syntax  
  
```  
  
string PDO::quote( $string[, $parameter_type ] )  
```  
  
#### Parameters  
$*string*: The string to quote.  
  
$*parameter_type*: An optional (integer) symbol indicating the data type.  The default is PDO::PARAM_STR.  
  
## Return Value  
A quoted string that can be passed to an SQL statement, or false if failure.  
  
## Remarks  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
  
```  
<?php  
$database = "test";  
$server = "(local)";  
$conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");  
  
$param = 'a \' g';  
$param2 = $conn->quote( $param );  
  
$query = "INSERT INTO Table1 VALUES( ?, '1' )";  
$stmt = $conn->prepare( $query );  
$stmt->execute(array($param));  
  
$query = "INSERT INTO Table1 VALUES( ?, ? )";  
$stmt = $conn->prepare( $query );  
$stmt->execute(array($param, $param2));  
?>  
```  
  
## See Also  
[PDO Class](../../connect/php/pdo-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
