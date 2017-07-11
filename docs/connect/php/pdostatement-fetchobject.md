---
title: "PDOStatement::fetchObject | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 71ad1932-cab3-4c29-8950-f5e82547d3b5
caps.latest.revision: 10
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
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
[PDO](http://go.microsoft.com/fwlink/?LinkID=187441)  
  
