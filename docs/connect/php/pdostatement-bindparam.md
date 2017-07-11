---
title: "PDOStatement::bindParam | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 65212058-2632-47a4-ba7d-2206883abf09
caps.latest.revision: 17
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# PDOStatement::bindParam
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Binds a parameter to a named or question mark placeholder in the SQL statement.  
  
## Syntax  
  
```  
  
bool PDOStatement::bindParam( $parameter, &$variable [,$data_type[, $length[, $driver_options]]] );  
```  
  
#### Parameters  
$*parameter*: A (mixed) parameter identifier. For a statement using named placeholders, a parameter name (:name). For a prepared statement using the question mark syntax, this will be the 1-based index of the parameter.  
  
&$*variable*: The (mixed) name of the PHP variable to bind to the SQL statement parameter.  
  
$*data_type*: An optional (integer) PDO::PARAM_* constant. Default is PDO::PARAM_STR.  
  
$*length*: An optional (integer) length of the data type. You can specify PDO::SQLSRV_PARAM_OUT_DEFAULT_SIZE to indicate the default size when using PDO::PARAM_INT or PDO::PARAM_BOOL in $*data_type*.  
  
$*driver_options*: The optional (mixed) driver-specific options. For example, you could specify PDO::SQLSRV_ENCODING_UTF8 to bind the column to a variable as a string encoded in UTF-8.  
  
## Return Value  
TRUE on success, otherwise FALSE.  
  
## Remarks  
When binding null data to server columns of type varbinary, binary, or varbinary(max) you should specify binary encoding (PDO::SQLSRV_ENCODING_BINARY) using the $*driver_options*. See [Constants](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md) for more information about encoding constants.  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
This code sample shows that after $contact is bound to the parameter, changing the value does change the value passed in the query.  
  
```  
<?php  
$database = "AdventureWorks";  
$server = "(local)";  
$conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");  
  
$contact = "Sales Agent";  
$stmt = $conn->prepare("select * from Person.ContactType where name = ?");  
$stmt->bindParam(1, $contact);  
$contact = "Owner";  
$stmt->execute();  
  
while ( $row = $stmt->fetch( PDO::FETCH_ASSOC ) ){  
   print "$row[Name]\n\n";  
}  
  
$stmt = null;  
$contact = "Sales Agent";  
$stmt = $conn->prepare("select * from Person.ContactType where name = :contact");  
$stmt->bindParam(':contact', $contact);  
$contact = "Owner";  
$stmt->execute();  
  
while ( $row = $stmt->fetch( PDO::FETCH_ASSOC ) ){  
   print "$row[Name]\n\n";  
}  
?>  
```  
  
## Example  
This code sample shows how to access an output parameter.  
  
```  
<?php  
$database = "Test";  
$server = "(local)";  
$conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");  
  
$input1 = 'bb';  
  
$stmt = $conn->prepare("select ? = count(* ) from Sys.tables");  
$stmt->bindParam( 1, $input1, PDO::PARAM_STR, 10 );  
$stmt->execute();  
echo $input1;  
?>  
```  
  
## Example  
This code sample shows how to use an input/output parameter.  
  
```  
<?php  
   $database = "AdventureWorks";  
   $server = "(local)";  
   $dbh = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");  
  
   $dbh->query("IF OBJECT_ID('dbo.sp_ReverseString', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_ReverseString");  
   $dbh->query("CREATE PROCEDURE dbo.sp_ReverseString @String as VARCHAR(2048) OUTPUT as SELECT @String = REVERSE(@String)");  
   $stmt = $dbh->prepare("EXEC dbo.sp_ReverseString ?");  
   $string = "123456789";  
   $stmt->bindParam(1, $string, PDO::PARAM_STR | PDO::PARAM_INPUT_OUTPUT, 2048);  
   $stmt->execute();  
   print $string;   // Expect 987654321  
?>  
```  
  
## See Also  
[PDOStatement Class](../../connect/php/pdostatement-class.md)  
[PDO](http://go.microsoft.com/fwlink/?LinkID=187441)  
  
