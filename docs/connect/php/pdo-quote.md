---
title: "PDO::quote"
description: "API reference for the PDO::quote function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
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

New PDO constants were introduced in PHP 7.2 to add support for [binding Unicode and non-Unicode strings](https://wiki.php.net/rfc/extended-string-types-for-pdo). Unicode strings can be surrounded with quotes with an N as a prefix (i.e. N'string' instead of 'string').

1. PDO::PARAM_STR_NATL - a new type for Unicode strings, to be applied as a bitwise-OR to PDO::PARAM_STR
1. PDO::PARAM_STR_CHAR - a new type for non-Unicode strings, to be applied as a bitwise-OR to PDO::PARAM_STR
1. PDO::ATTR_DEFAULT_STR_PARAM - set to either PDO::PARAM_STR_NATL or PDO::PARAM_STR_CHAR to indicate a value to bitwise-OR to PDO::PARAM_STR by default

Beginning with version 5.8.0, you can use these constants with PDO::quote.
  
## Return Value  
A quoted string that can be passed to an SQL statement, or false if failure.  
  
## Remarks  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## String escape example  
  
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
  
## PDO quote example  

The following script shows a few examples of how extended string types affect PDO::quote() with PHP 7.2+.

```
<?php
$database = "test";
$server = "(local)";
$db = new PDO("sqlsrv:server=$server; Database=$database", "", "");

$db->quote('über', PDO::PARAM_STR | PDO::PARAM_STR_NATL); // N'über'
$db->quote('foo'); // 'foo'

$db->setAttribute(PDO::ATTR_DEFAULT_STR_PARAM, PDO::PARAM_STR_NATL);
$db->quote('über'); // N'über'
$db->quote('foo', PDO::PARAM_STR | PDO::PARAM_STR_CHAR); // 'foo'
?>
```
  
## See Also  
[PDO Class](../../connect/php/pdo-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
