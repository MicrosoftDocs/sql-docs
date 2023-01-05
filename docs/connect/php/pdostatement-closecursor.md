---
title: "PDOStatement::closeCursor"
description: "API reference for the PDOStatement::closeCursor function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# PDOStatement::closeCursor
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Closes the cursor, enabling the statement to be executed again.  
  
## Syntax  
  
```  
  
bool PDOStatement::closeCursor();  
```  
  
## Return Value  
true on success, otherwise false.  
  
## Remarks  
closeCursor has an effect when the MultipleActiveResultSets connection option is set to false.  For more information about the MultipleActiveResultSets connection option, see [How to: Disable Multiple Active Resultsets (MARS)](../../connect/php/how-to-disable-multiple-active-resultsets-mars.md).  
  
Instead of calling closeCursor, you can also just set the statement handle to null.  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
  
```  
<?php  
$database = "AdventureWorks";  
$server = "(local)";  
$conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "", array('MultipleActiveResultSets' => false ) );  
  
$stmt = $conn->prepare('SELECT * FROM Person.ContactType');  
  
$stmt2 = $conn->prepare('SELECT * FROM HumanResources.Department');  
  
$stmt->execute();  
  
$result = $stmt->fetch();  
print_r($result);  
  
$stmt->closeCursor();  
  
$stmt2->execute();  
$result = $stmt2->fetch();  
print_r($result);  
?>  
```  
  
## See Also  
[PDOStatement Class](../../connect/php/pdostatement-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
