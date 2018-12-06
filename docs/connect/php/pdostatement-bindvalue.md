---
title: "PDOStatement::bindValue | Microsoft Docs"
ms.custom: ""
ms.date: "05/22/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 13bc4ece-420e-4887-8809-bf0705ddf126
author: MightyPen
ms.author: genemi
manager: craigg
---
# PDOStatement::bindValue
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Binds a value to a named or question mark placeholder in the SQL statement.  
  
## Syntax  
  
```  
  
bool PDOStatement::bindValue($parameter, $value[, $data_type]);  
```  
  
#### Parameters  
$*parameter*: A (mixed) parameter identifier. For a statement using named placeholders, use a parameter name (:name). For a prepared statement using the question mark syntax, it is the 1-based index of the parameter.
  
$*value*: The (mixed) value to bind to the parameter.  
  
$*data_type*: The optional (integer) data type represented by a PDO::PARAM_* constant. The default is PDO::PARAM_STR.  
  
## Return Value  
TRUE on success, otherwise FALSE.  
  
## Remarks  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
This example shows that after the value of $contact is bound, changing the value does not change the value passed in the query.  
  
```  
<?php  
$database = "AdventureWorks";  
$server = "(local)";  
$conn = new PDO("sqlsrv:server=$server ; Database = $database", "", "");  
  
$contact = "Sales Agent";  
$stmt = $conn->prepare("select * from Person.ContactType where name = ?");  
$stmt->bindValue(1, $contact);  
$contact = "Owner";  
$stmt->execute();  
  
while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {  
   print "$row[Name]\n\n";  
}  
  
$stmt = null;  
$contact = "Sales Agent";  
$stmt = $conn->prepare("select * from Person.ContactType where name = :contact");  
$stmt->bindValue(':contact', $contact);  
$contact = "Owner";  
$stmt->execute();  
  
while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {  
   print "$row[Name]\n\n";  
}  
?>  
```

> [!NOTE]
> It is recommended to use strings as inputs when binding values to a [decimal or numeric column](https://docs.microsoft.com/sql/t-sql/data-types/decimal-and-numeric-transact-sql) to ensure precision and accuracy as PHP has limited precision for [floating point numbers](https://php.net/manual/en/language.types.float.php). The same applies to bigint columns, especially when the values are outside the range of an [integer](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md).

## Example  
This code sample shows how to bind a decimal value as an input parameter.  

```
<?php  
$database = "Test";  
$server = "(local)";  
$conn = new PDO("sqlsrv:server=$server ; Database = $database", "", "");  

// Assume TestTable exists with a decimal field 
$input = 9223372036854.80000;
$stmt = $conn->prepare("INSERT INTO TestTable (DecimalCol) VALUES (?)");
// by default it is PDO::PARAM_STR, rounding of a large input value may
// occur if PDO::PARAM_INT is specified
$stmt->bindValue(1, $input, PDO::PARAM_STR);
$stmt->execute();
```
  
## See Also  
[PDOStatement Class](../../connect/php/pdostatement-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
