---
title: "PDO::errorCode | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 5864b1d8-6814-41cd-a88d-415124484c13
caps.latest.revision: 13
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# PDO::errorCode
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

PDO::errorCode retrieves the SQLSTATE of the most recent operation on the database handle.  
  
## Syntax  
  
```  
  
mixed PDO::errorCode();  
```  
  
## Return Value  
PDO::errorCode returns a five-char SQLSTATE as a string or NULL if there was no operation on the database handle.  
  
## Remarks  
PDO::errorCode in the PDO_SQLSRV driver will return warnings on some successful operations. For example, on a successful connection, PDO::errorCode will return "01000" indicating SQL_SUCCESS_WITH_INFO.  
  
PDO::errorCode only retrieves error codes for operations performed directly on the database connection. If you create a PDOStatement instance through PDO::prepare or PDO::query and generate an error on the statement object, PDO::errorCode will not retrieve that error. You must call PDOStatement::errorCode to return the error code for an operation performed on a particular statement object.  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
In this example, the name of the column is misspelled (`Cityx` instead of `City`), causing an error, which is then reported.  
  
```  
<?php  
$conn = new PDO( "sqlsrv:server=(local) ; Database = AdventureWorks ", "", "");  
$query = "SELECT * FROM Person.Address where Cityx = 'Essen'";  
  
$conn->query($query);  
print $conn->errorCode();  
?>  
```  
  
## See Also  
[PDO Class](../../connect/php/pdo-class.md)  
[PDO](http://go.microsoft.com/fwlink/?LinkID=187441)  
  
