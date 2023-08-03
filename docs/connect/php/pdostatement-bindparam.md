---
title: "PDOStatement::bindParam"
description: "API reference for the PDOStatement::bindParam function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# PDOStatement::bindParam
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Binds a parameter to a named or question mark placeholder in the SQL statement.  
  
## Syntax  
  
```  
  
bool PDOStatement::bindParam($parameter, &$variable[, $data_type[, $length[, $driver_options]]]);  
```  
  
#### Parameters  
$*parameter*: A (mixed) parameter identifier. For a statement using named placeholders, use a parameter name (:name). For a prepared statement using the question mark syntax, it is the 1-based index of the parameter.  
  
&$*variable*: The (mixed) name of the PHP variable to bind to the SQL statement parameter.  
  
$*data_type*: An optional (integer) PDO::PARAM_* constant. Default is PDO::PARAM_STR.  
  
$*length*: An optional (integer) length of the data type. You can specify PDO::SQLSRV_PARAM_OUT_DEFAULT_SIZE to indicate the default size when using PDO::PARAM_INT or PDO::PARAM_BOOL in $*data_type*.  
  
$*driver_options*: The optional (mixed) driver-specific options. For example, you could specify PDO::SQLSRV_ENCODING_UTF8 to bind the column to a variable as a string encoded in UTF-8.  
  
## Return Value  
TRUE on success, otherwise FALSE.  
  
## Remarks  
When binding null data to server columns of type varbinary, binary, or varbinary(max) you should specify binary encoding (PDO::SQLSRV_ENCODING_BINARY) using the $*driver_options*. For more information about encoding constants, see [Constants](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md).  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  

## Parameter example  
This code sample shows that after $contact is bound to the parameter, changing the value does change the value passed in the query.  
  
```  
<?php  
$database = "AdventureWorks";  
$server = "(local)";  
$conn = new PDO("sqlsrv:server=$server ; Database = $database", "", "");  
  
$contact = "Sales Agent";  
$stmt = $conn->prepare("select * from Person.ContactType where name = ?");  
$stmt->bindParam(1, $contact);  
$contact = "Owner";  
$stmt->execute();  
  
while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {  
   print "$row[Name]\n\n";  
}  
  
$stmt = null;  
$contact = "Sales Agent";  
$stmt = $conn->prepare("select * from Person.ContactType where name = :contact");  
$stmt->bindParam(':contact', $contact);  
$contact = "Owner";  
$stmt->execute();  
  
while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {  
   print "$row[Name]\n\n";  
}  
?>  
```  
  
## Output parameter example  
This code sample shows how to access an output parameter.  
  
```  
<?php  
$database = "Test";  
$server = "(local)";  
$conn = new PDO("sqlsrv:server=$server ; Database = $database", "", "");  
  
$input1 = 'bb';  
  
$stmt = $conn->prepare("select ? = count(*) from Sys.tables");  
$stmt->bindParam(1, $input1, PDO::PARAM_STR, 10);  
$stmt->execute();  
echo $input1;  
?>  
```  
  
> [!NOTE]
> When binding an output parameter to a bigint type, if the value may end up outside the range of an [integer](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md), using PDO::PARAM_INT with PDO::SQLSRV_PARAM_OUT_DEFAULT_SIZE may result in a "value out of range" exception. Therefore, use the default PDO::PARAM_STR instead and provide the size of the resulting string, which is at most 21. It is the maximum number of digits, including the negative sign, of any bigint value. 

## Input/Output example  
This code sample shows how to use an input/output parameter.  
  
```  
<?php  
   $database = "AdventureWorks";  
   $server = "(local)";  
   $dbh = new PDO("sqlsrv:server=$server ; Database = $database", "", "");  
  
   $dbh->query("IF OBJECT_ID('dbo.sp_ReverseString', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_ReverseString");  
   $dbh->query("CREATE PROCEDURE dbo.sp_ReverseString @String as VARCHAR(2048) OUTPUT as SELECT @String = REVERSE(@String)");  
   $stmt = $dbh->prepare("EXEC dbo.sp_ReverseString ?");  
   $string = "123456789";  
   $stmt->bindParam(1, $string, PDO::PARAM_STR | PDO::PARAM_INPUT_OUTPUT, 2048);  
   $stmt->execute();  
   print $string;   // Expect 987654321  
?>  
```  

> [!NOTE]
> It is recommended to use strings as inputs when binding values to a [decimal or numeric column](../../t-sql/data-types/decimal-and-numeric-transact-sql.md) to ensure precision and accuracy as PHP has limited precision for [floating point numbers](https://php.net/manual/en/language.types.float.php). The same applies to bigint columns, especially when the values are outside the range of an [integer](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md).

## Decimal input example  
This code sample shows how to bind a decimal value as an input parameter.  

```
<?php  
$database = "Test";  
$server = "(local)";  
$conn = new PDO("sqlsrv:server=$server ; Database = $database", "", "");  

// Assume TestTable exists with a decimal field 
$input = "9223372036854.80000";
$stmt = $conn->prepare("INSERT INTO TestTable (DecimalCol) VALUES (?)");
// by default it is PDO::PARAM_STR, rounding of a large input value may
// occur if PDO::PARAM_INT is specified
$stmt->bindParam(1, $input, PDO::PARAM_STR);
$stmt->execute();
```


## See Also  
[PDOStatement Class](../../connect/php/pdostatement-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
