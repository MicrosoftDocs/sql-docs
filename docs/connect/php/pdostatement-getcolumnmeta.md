---
title: "PDOStatement::getColumnMeta | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: c92a21cc-8e53-43d0-a4bf-542c77c100c9
author: MightyPen
ms.author: genemi
manager: craigg
---
# PDOStatement::getColumnMeta
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Retrieves metadata for a column.  
  
## Syntax  
  
```  
  
array PDOStatement::getColumnMeta ( $column );  
```  
  
#### Parameters  
*$conn*: (Integer) The zero-based number of the column whose metadata you want to retrieve.  
  
## Return Value  
An associative array (key and value) containing the metadata for the column. See the Remarks section for a description of the fields in the array.  
  
## Remarks  
The following table describes the fields in the array returned by getColumnMeta.  
  
|NAME|VALUES|  
|--------|----------|  
|native_type|Specifies the PHP type for column. Always string.|  
|driver:decl_type|Specifies the SQL type used to represent the column value in the database. If the column in the result set is the result of a function, this value is not returned by PDOStatement::getColumnMeta.|  
|flags|Specifies the flags set for this column. Always 0.|  
|name|Specifies the name of the column in the database.|  
|table|Specifies the name of the table that contains the column in the database. Always blank.|  
|len|Specifies the column length.|  
|precision|Specifies the numeric precision of this column.|  
|pdo_type|Specifies the type of this column as represented by the PDO::PARAM_* constants. Always PDO::PARAM_STR (2).|  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
  
```  
<?php  
$database = "AdventureWorks";  
$server = "(local)";  
$conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");  
  
$stmt = $conn->query("select * from Person.ContactType");  
$metadata = $stmt->getColumnMeta(2);  
var_dump($metadata);  
  
print $metadata['sqlsrv:decl_type'] . "\n";  
print $metadata['native_type'] . "\n";  
print $metadata['name'];  
?>  
```  
  
## See Also  
[PDOStatement Class](../../connect/php/pdostatement-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
