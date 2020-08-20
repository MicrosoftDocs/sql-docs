---
title: "PDOStatement::getColumnMeta"
description: "API reference for the PDOStatement::getColumnMeta function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
ms.custom: ""
ms.date: "08/10/2020"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: c92a21cc-8e53-43d0-a4bf-542c77c100c9
author: David-Engel
ms.author: v-daenge
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
  
## Sensitivity Data Classification Metadata

Beginning with version 5.8.0, a new statement attribute `PDO::SQLSRV_ATTR_DATA_CLASSIFICATION` is available for users to access the [sensitivity data classification metadata](https://docs.microsoft.com/sql/relational-databases/security/sql-data-discovery-and-classification?view=sql-server-ver15&tabs=t-sql#subheading-4) in Microsoft SQL Server 2019 using `PDOStatement::getColumnMeta`, which requires Microsoft ODBC Driver 17.4.2 or above.

Note the attribute `PDO::SQLSRV_ATTR_DATA_CLASSIFICATION` is `false` by default, but when set to `true`, the aforementioned array field, `flags`, will be populated with the sensitivity data classification metadata, if it exists. 

Take a Patients table for example:

```
CREATE TABLE Patients 
      [PatientId] int identity,
      [SSN] char(11),
      [FirstName] nvarchar(50),
      [LastName] nvarchar(50),
      [BirthDate] date)
```

We can classify the SSN and BirthDate columns as shown below:

```
ADD SENSITIVITY CLASSIFICATION TO [Patients].SSN WITH (LABEL = 'Highly Confidential - secure privacy', INFORMATION_TYPE = 'Credentials')
ADD SENSITIVITY CLASSIFICATION TO [Patients].BirthDate WITH (LABEL = 'Confidential Personal Data', INFORMATION_TYPE = 'Birthdays')
```

To access the metadata, use `PDOStatement::getColumnMeta` after setting `PDO::SQLSRV_ATTR_DATA_CLASSIFICATION` to true, as shown in the snippet below:

```
$options = array(PDO::SQLSRV_ATTR_DATA_CLASSIFICATION => true);
$tableName = 'Patients';
$tsql = "SELECT * FROM $tableName";
$stmt = $conn->prepare($tsql, $options);
$stmt->execute();
$numCol = $stmt->columnCount();

for ($i = 0; $i < $numCol; $i++) {
    $metadata = $stmt->getColumnMeta($i);
    $jstr = json_encode($metadata);
    echo $jstr . PHP_EOL;
}
```

The output of metadata for all columns is:

```
{"flags":{"Data Classification":[]},"sqlsrv:decl_type":"int identity","native_type":"string","table":"","pdo_type":2,"name":"PatientId","len":10,"precision":0}
{"flags":{"Data Classification":[{"Label":{"name":"Highly Confidential - secure privacy","id":""},"Information Type":{"name":"Credentials","id":""}}]},"sqlsrv:decl_type":"char","native_type":"string","table":"","pdo_type":2,"name":"SSN","len":11,"precision":0}
{"flags":{"Data Classification":[]},"sqlsrv:decl_type":"nvarchar","native_type":"string","table":"","pdo_type":2,"name":"FirstName","len":50,"precision":0}
{"flags":{"Data Classification":[]},"sqlsrv:decl_type":"nvarchar","native_type":"string","table":"","pdo_type":2,"name":"LastName","len":50,"precision":0}
{"flags":{"Data Classification":[{"Label":{"name":"Confidential Personal Data","id":""},"Information Type":{"name":"Birthdays","id":""}}]},"sqlsrv:decl_type":"date","native_type":"string","table":"","pdo_type":2,"name":"BirthDate","len":10,"precision":0}
```

If we modify the above snippet by setting `PDO::SQLSRV_ATTR_DATA_CLASSIFICATION` to `false` (the default case), the `flags` field will always be `0` as before, like this:

```
{"flags":0,"sqlsrv:decl_type":"int identity","native_type":"string","table":"","pdo_type":2,"name":"PatientId","len":10,"precision":0}
{"flags":0,"sqlsrv:decl_type":"char","native_type":"string","table":"","pdo_type":2,"name":"SSN","len":11,"precision":0}
{"flags":0,"sqlsrv:decl_type":"nvarchar","native_type":"string","table":"","pdo_type":2,"name":"FirstName","len":50,"precision":0}
{"flags":0,"sqlsrv:decl_type":"nvarchar","native_type":"string","table":"","pdo_type":2,"name":"LastName","len":50,"precision":0}
{"flags":0,"sqlsrv:decl_type":"date","native_type":"string","table":"","pdo_type":2,"name":"BirthDate","len":10,"precision":0}
```

      
## See Also  
[PDOStatement Class](../../connect/php/pdostatement-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
