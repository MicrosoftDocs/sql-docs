---
title: "PDO::errorInfo"
description: "API reference for the PDO::errorInfo function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "01/29/2021"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# PDO::errorInfo
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Retrieves extended error information of the most recent operation on the database handle.  
  
## Syntax  
  
```  
array PDO::errorInfo();  
```  
  
## Return Value  
An array of error information about the most recent operation on the database handle. The array consists of the following fields:  
  
-   The SQLSTATE error code.  
  
-   The driver-specific error code.  
  
-   The driver-specific error message.  
  
If there is no error, or if the SQLSTATE is not set, then the driver-specific fields are NULL.  
  
## Remarks  
PDO::errorInfo only retrieves error information for operations performed directly on the database. Use PDOStatement::errorInfo when a PDOStatement instance is created using PDO::prepare or PDO::query.  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
In this example, the name of the column is misspelled (`Cityx` instead of `City`), causing an error, which is then reported.  
  
```php
<?php  
$conn = new PDO( "sqlsrv:server=(local) ; Database = AdventureWorks ", "");  
$query = "SELECT * FROM Person.Address where Cityx = 'Essen'";  
  
$conn->query($query);  
print $conn->errorCode();  
echo "\n";  
print_r ($conn->errorInfo());  
?>  
```  

## Additional ODBC messages

When an exception occurs, the ODBC Driver may return more than one error to help diagnose problems. However, PDO::errorInfo always shows only the first error. In response to this [bug report](https://bugs.php.net/bug.php?id=78196), [PDO::errorInfo](https://www.php.net/manual/en/pdo.errorinfo.php) and [PDOStatement::errorInfo](https://www.php.net/manual/en/pdostatement.errorinfo.php) have been updated to indicate that drivers should display *at least* the following three fields:
```
0	SQLSTATE error code (a five characters alphanumeric identifier defined in the ANSI SQL standard).
1	Driver specific error code.
2	Driver specific error message.
```

Starting with 5.9.0, the default behavior of PDO::errorInfo is to show additional ODBC errors, if they are available. For example:

```php
<?php  
try {
    $conn = new PDO("sqlsrv:server=$server;", $uid, $pwd);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    $stmt = $conn->prepare("SET NOCOUNT ON; USE $database; SELECT 1/0 AS col1");
    $stmt->execute();
} catch (PDOException $e) {
    var_dump($e->errorInfo);
}
?>  
```  

Running the above script should have thrown an exception, and the output is like this:

```
array(6) {
  [0]=>
  string(5) "01000"
  [1]=>
  int(5701)
  [2]=>
  string(91) "[Microsoft][ODBC Driver 17 for SQL Server][SQL Server]Changed database context to 'tempdb'."
  [3]=>
  string(5) "22012"
  [4]=>
  int(8134)
  [5]=>
  string(87) "[Microsoft][ODBC Driver 17 for SQL Server][SQL Server]Divide by zero error encountered."
}
```

If the user prefers the previous way, a new configuration option `pdo_sqlsrv.report_additional_errors` can be used to turn it off. Simply add the following line in the beginning of any php script:

```
ini_set('pdo_sqlsrv.report_additional_errors', 0);
```

In this case, the error info shown will be like this, when running the same example script:

```
array(3) {
  [0]=>
  string(5) "01000"
  [1]=>
  int(5701)
  [2]=>
  string(91) "[Microsoft][ODBC Driver 17 for SQL Server][SQL Server]Changed database context to 'tempdb'."
}
```

If necessary, the user may choose to add the following line to the php.ini file in order to turn off this feature in all their php scripts:

```
pdo_sqlsrv.report_additional_errors = 0
```

## Warnings and errors

Beginning with 5.9.0, ODBC warnings will no longer be logged as errors. That is, [error codes](../../odbc/reference/appendixes/appendix-a-odbc-error-codes.md) with prefix "01" are logged as warnings. In other words, if the user wants to log errors only, update the php.ini like this:

```
[pdo_sqlsrv]  
pdo_sqlsrv.log_severity = 1
```

In this case, the log file will not contain any warning message(s). Please check how [logging](./logging-activity.md#logging-activity-using-the-pdo_sqlsrv-driver) works for pdo_sqlsrv users.

## See Also  
[PDO Class](../../connect/php/pdo-class.md)

[PDO](https://php.net/manual/book.pdo.php)  

[PDOStatement::errorInfo](../../connect/php/pdostatement-errorinfo.md)