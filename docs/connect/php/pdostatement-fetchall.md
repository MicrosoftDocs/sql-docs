---
title: "PDOStatement::fetchAll"
description: "API reference for the PDOStatement::fetchAll function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# PDOStatement::fetchAll
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Returns the rows in a result set in an array.  
  
## Syntax  
  
```  
  
array PDOStatement::fetchAll([ $fetch_style[, $column_index ][, ctor_args]] );  
```  
  
#### Parameters  
$*fetch_style*: An (integer) symbol specifying the format of the row data. See [PDOStatement::fetch](../../connect/php/pdostatement-fetch.md) for a list of values. PDO::FETCH_COLUMN is also allowed. PDO::FETCH_BOTH is the default.  
  
$*column_index*: An integer value representing the column to return if $*fetch_style* is PDO::FETCH_COLUMN. 0 is the default.  
  
$*ctor_args*: An array of the parameters for a class constructor, when $*fetch_style* is PDO::FETCH_CLASS or PDO::FETCH_OBJ.  
  
## Return Value  
An array of the remaining rows in the result set, or false if the method call fails.  
  
## Remarks  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
  
```  
<?php  
   $server = "(local)";  
   $database = "AdventureWorks";  
   $conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");  
  
   print "-----------\n";  
   $stmt = $conn->query( "select * from Person.ContactType where ContactTypeID < 5 " );  
   $result = $stmt->fetchall(PDO::FETCH_BOTH);  
   print_r( $result );  
   print "\n-----------\n";  
  
   print "-----------\n";  
   $stmt = $conn->query( "select * from Person.ContactType where ContactTypeID < 5 " );  
   $result = $stmt->fetchall(PDO::FETCH_NUM);  
   print_r( $result );  
   print "\n-----------\n";  
  
   $stmt = $conn->query( "select * from Person.ContactType where ContactTypeID < 5 " );  
   $result = $stmt->fetchall(PDO::FETCH_COLUMN, 1);  
   print_r( $result );  
   print "\n-----------\n";  
  
   class cc {  
      function __construct( $arg ) {  
         echo "$arg\n";  
      }  
  
      function __toString() {  
         echo "To string\n";  
      }  
   };  
  
   $stmt = $conn->query( 'SELECT TOP(2) * FROM Person.ContactType' );  
   $all = $stmt->fetchAll( PDO::FETCH_CLASS, 'cc', array( 'Hi!' ));  
   var_dump( $all );  
?>  
```  
  
## See Also  
[PDOStatement Class](../../connect/php/pdostatement-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
