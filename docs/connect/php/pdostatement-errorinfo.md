---
title: "PDOStatement::errorInfo"
description: "API reference for the PDOStatement::errorInfo function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "01/29/2021"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# PDOStatement::errorInfo
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Retrieves extended error information of the most recent operation on the statement handle.  
  
## Syntax  

```
array PDOStatement::errorInfo();
```
  
## Return Value  
An array of error information about the most recent operation on the statement handle. The array consists of the following fields:  
  
-   The SQLSTATE error code  
  
-   The driver-specific error code  
  
-   The driver-specific error message  
  
If there is no error, or if the SQLSTATE is not set, the driver-specific fields will be NULL.  
  
## Remarks  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
In this example, the SQL statement has an error, which is then reported.  
  
```php
<?php  
$conn = new PDO( "sqlsrv:server=(local) ; Database = AdventureWorks", "", "");  
$stmt = $conn->prepare('SELECT * FROM Person.Addressx');  
  
$stmt->execute();  
print_r ($stmt->errorInfo());  
?>  
```

## Additional ODBC messages

When an exception occurs, the ODBC Driver may return more than one error to help diagnose problems. However, PDOStatement::errorInfo always shows only the first error. In response to this [bug report](https://bugs.php.net/bug.php?id=78196), [PDO::errorInfo](https://www.php.net/manual/en/pdo.errorinfo.php) and [PDOStatement::errorInfo](https://www.php.net/manual/en/pdostatement.errorinfo.php) have been updated to indicate that drivers should display *at least* the following three fields:
```
0	SQLSTATE error code (a five characters alphanumeric identifier defined in the ANSI SQL standard).
1	Driver specific error code.
2	Driver specific error message.
```

Starting with 5.9.0, the default behavior of PDOStatement::errorInfo is to show additional ODBC errors, if they are available. See [PDO::errorInfo](../../connect/php/pdo-errorinfo.md) for more details.
  
## See Also  
[PDOStatement Class](../../connect/php/pdostatement-class.md)

[PDO::errorInfo](../../connect/php/pdo-errorinfo.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
