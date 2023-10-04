---
title: "PDOStatement::fetchObject"
description: "API reference for the PDOStatement::fetchObject function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# PDOStatement::fetchObject
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Retrieves the next row as an object.  
  
## Syntax  
  
```  
  
mixed PDOStatement::fetchObject([ $class_name[,$ctor_args ]] )  
```  
  
#### Parameters  
$*class_name*: An optional string specifying the name of the class to create. The default is stdClass.  
  
$*ctor_args*: An optional array with arguments to a custom class constructor.  
  
## Return Value  
On success, returns an object with an instance of the class. Properties map to columns. Returns false on failure.  
  
## Remarks  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
  
```  
<?php  
   $server = "(local)";  
   $database = "AdventureWorks";  
   $conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");  
  
   $stmt = $conn->query( "select * from Person.ContactType where ContactTypeID < 5 " );  
   $result = $stmt->fetchObject();  
   print $result->Name;  
?>  
```  
  
## See Also  
[PDOStatement Class](../../connect/php/pdostatement-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
