---
title: "PDOStatement::fetchColumn"
description: "API reference for the PDOStatement::fetchColumn function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 07/23/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# PDOStatement::fetchColumn
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Returns one column in a row.  
  
## Syntax  
  
```php  
  
string PDOStatement::fetchColumn ([ $column_number ] );  
```  
  
#### Parameters  
$*column_number*: An optional integer indicating the zero-based column number. The default is 0 (the first column in the row).  
  
## Return Value  
One column or false if there are no more rows.  
  
## Remarks  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
  
```php  
<?php  
   $server = "(local)";  
   $database = "AdventureWorks";  
   $conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");  
  
   $stmt = $conn->query( "select * from Person.ContactType where ContactTypeID < 5 " );  
   while ( $result = $stmt->fetchColumn(1)) {   
      print($result . "\n");   
   }  
?>  
```  
  
## Related content

- [PDOStatement Class](pdostatement-class.md)
- [PDO](https://php.net/manual/book.pdo.php)
