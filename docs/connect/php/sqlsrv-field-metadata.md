---
title: "sqlsrv_field_metadata"
description: "sqlsrv_field_metadata"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/29/2021"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "API Reference, sqlsrv_field_metadata"
  - "sqlsrv_field_metadata"
apiname: "sqlsrv_field_metadata"
apitype: "NA"
---
# sqlsrv_field_metadata
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Retrieves metadata for the fields of a prepared statement. For information about preparing a statement, see [sqlsrv_query](../../connect/php/sqlsrv-query.md) or [sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md). Note that **sqlsrv_field_metadata** can be called on any prepared statement, pre- or post-execution.  
  
## Syntax  
  
```  
  
sqlsrv_field_metadata( resource $stmt)  
```  
  
#### Parameters  
*$stmt*: A statement resource for which field metadata is sought.  
  
## Return Value  
An **array** of arrays or **false**. The array consists of one array for each field in the result set. Each sub-array has keys as described in the table below. If there is an error in retrieving field metadata, **false** is returned.  
  
|Key|Description|  
|-------|---------------|  
|Name|Name of the column to which the field corresponds.|  
|Type|Numeric value that corresponds to a SQL type.|  
|Size|Number of characters for fields of character type (char(n), varchar(n), nchar(n), nvarchar(n), XML). Number of bytes for fields of binary type (binary(n), varbinary(n), UDT). **NULL** for other SQL Server data types.|  
|Precision|The precision for types of variable precision (real, numeric, decimal, datetime2, datetimeoffset, and time). **NULL** for other SQL Server data types.|  
|Scale|The scale for types of variable scale (numeric, decimal, datetime2, datetimeoffset, and time). **NULL** for other SQL Server data types.|  
|Nullable|An enumerated value indicating whether the column is nullable (**SQLSRV_NULLABLE_YES**), the column is not nullable (**SQLSRV_NULLABLE_NO**), or it is not known if the column is nullable (**SQLSRV_NULLABLE_UNKNOWN**).|  
  
The following table gives more information on the keys for each sub-array (see the SQL Server documentation for more information on these types):  
  
|SQL Server 2008 data type|Type|Min/Max Precision|Min/Max Scale|Size|  
|-----------------------------|--------|----------------------|------------------|--------|  
|bigint|SQL_BIGINT (-5)|||8|  
|binary|SQL_BINARY (-2)|||0 < *n* < 8000 <sup>1</sup>|  
|bit|SQL_BIT (-7)||||  
|char|SQL_CHAR (1)|||0 < *n* < 8000 <sup>1</sup>|  
|date|SQL_TYPE_DATE (91)|10/10|0/0||  
|datetime|SQL_TYPE_TIMESTAMP (93)|23/23|3/3||  
|datetime2|SQL_TYPE_TIMESTAMP (93)|19/27|0/7||  
|datetimeoffset|SQL_SS_TIMESTAMPOFFSET (-155)|26/34|0/7||  
|decimal|SQL_DECIMAL (3)|1/38|0/precision value||  
|float|SQL_FLOAT (6)|4/8|||  
|image|SQL_LONGVARBINARY (-4)|||2 GB|  
|int|SQL_INTEGER (4)||||  
|money|SQL_DECIMAL (3)|19/19|4/4||  
|nchar|SQL_WCHAR (-8)|||0 < *n* < 4000 <sup>1</sup>|  
|ntext|SQL_WLONGVARCHAR (-10)|||1 GB|  
|numeric|SQL_NUMERIC (2)|1/38|0/precision value||  
|nvarchar|SQL_WVARCHAR (-9)|||0 < *n* < 4000 <sup>1</sup>|  
|real|SQL_REAL (7)|4/4|||  
|smalldatetime|SQL_TYPE_TIMESTAMP (93)|16/16|0/0||  
|smallint|SQL_SMALLINT (5)|||2 bytes|  
|Smallmoney|SQL_DECIMAL (3)|10/10|4/4||  
|sql_variant|SQL_SS_VARIANT (-150)|||variable|  
|text|SQL_LONGVARCHAR (-1)|||2 GB|  
|time|SQL_SS_TIME2 (-154)|8/16|0/7||  
|timestamp|SQL_BINARY (-2)|||8 bytes|  
|tinyint|SQL_TINYINT (-6)|||1 byte|  
|udt|SQL_SS_UDT (-151)|||variable|  
|uniqueidentifier|SQL_GUID (-11)|||16|  
|varbinary|SQL_VARBINARY (-3)|||0 < *n* < 8000 <sup>1</sup>|  
|varchar|SQL_VARCHAR (12)|||0 < *n* < 8000 <sup>1</sup>|  
|xml|SQL_SS_XML (-152)|||0|  
  
(1) Zero (0) indicates that the maximum size is allowed.  
  
The Nullable key can either be yes or no.  
  
## Example  
The following example creates a statement resource, then retrieves and displays the field metadata. The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
```
<?php
/* Connect to the local server using Windows Authentication and
specify the AdventureWorks database as the database in use. */
$serverName = "(local)";
$connectionInfo = array("Database"=>"AdventureWorks");
$conn = sqlsrv_connect($serverName, $connectionInfo);
if ($conn === false) {
    echo "Could not connect.\n";
    die( print_r( sqlsrv_errors(), true));
}

/* Prepare the statement. */
$tsql = "SELECT ReviewerName, Comments FROM Production.ProductReview";
$stmt = sqlsrv_prepare( $conn, $tsql);
  
/* Get and display field metadata. */
foreach( sqlsrv_field_metadata( $stmt) as $fieldMetadata) {
    foreach( $fieldMetadata as $name => $value) {
        echo "$name: $value\n";
    }  
    echo "\n";
}  
  
/* Note: sqlsrv_field_metadata can be called on any statement
resource, pre- or post-execution. */
  
/* Free statement and connection resources. */
sqlsrv_free_stmt($stmt);
sqlsrv_close($conn);
?>
```

## Sensitivity Data Classification Metadata

A new option `DataClassification` is introduced in version 5.8.0 for users to access the [sensitivity data classification metadata](../../relational-databases/security/sql-data-discovery-and-classification.md) in Microsoft SQL Server 2019 using `sqlsrv_field_metadata`, which requires Microsoft ODBC Driver 17.4.2 or above. 

By default, the option `DataClassification` is `false`, but when set to `true`, the array returned by `sqlsrv_field_metadata` will be populated with the sensitivity data classification metadata, if it exists. 

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

To access the metadata, invoke `sqlsrv_field_metadata` as shown in the snippet below:

```
$tableName = 'Patients';
$tsql = "SELECT * FROM $tableName";
$stmt = sqlsrv_prepare($conn, $tsql, array(), array('DataClassification' => true));
if (sqlsrv_execute($stmt)) {
    $fieldmeta = sqlsrv_field_metadata($stmt);

    foreach ($fieldmeta as $f) {
        if (count($f['Data Classification']) > 0) {
            echo $f['Name'] . ": \n";
            print_r($f['Data Classification']); 
        }
    }
}
```

The output will be:

```
SSN: 
Array
(
    [0] => Array
        (
            [Label] => Array
                (
                    [name] => Highly Confidential - secure privacy
                    [id] => 
                )

            [Information Type] => Array
                (
                    [name] => Credentials
                    [id] => 
                )

        )

)
BirthDate: 
Array
(
    [0] => Array
        (
            [Label] => Array
                (
                    [name] => Confidential Personal Data
                    [id] => 
                )

            [Information Type] => Array
                (
                    [name] => Birthdays
                    [id] => 
                )

        )

)
```

If using `sqlsrv_query` instead of `sqlsrv_prepare`, the above snippet can be modified, like this:

```
$tableName = 'Patients';
$tsql = "SELECT * FROM $tableName";
$stmt = sqlsrv_query($conn, $tsql, array(), array('DataClassification' => true));
$fieldmeta = sqlsrv_field_metadata($stmt);

foreach ($fieldmeta as $f) {
    $jstr = json_encode($f);
    echo $jstr . PHP_EOL;
}
```

As you can see in the JSON representation below, the data classification metadata is shown if associated with the columns:

```
{"Name":"PatientId","Type":4,"Size":null,"Precision":10,"Scale":null,"Nullable":0,"Data Classification":[]}
{"Name":"SSN","Type":1,"Size":11,"Precision":null,"Scale":null,"Nullable":1,"Data Classification":[{"Label":{"name":"Highly Confidential - secure privacy","id":""},"Information Type":{"name":"Credentials","id":""}}]}
{"Name":"FirstName","Type":-9,"Size":50,"Precision":null,"Scale":null,"Nullable":1,"Data Classification":[]}
{"Name":"LastName","Type":-9,"Size":50,"Precision":null,"Scale":null,"Nullable":1,"Data Classification":[]}
{"Name":"BirthDate","Type":91,"Size":null,"Precision":10,"Scale":0,"Nullable":1,"Data Classification":[{"Label":{"name":"Confidential Personal Data","id":""},"Information Type":{"name":"Birthdays","id":""}}]}
```

## Sensitivity Rank using a predefined set of values

Beginning with 5.9.0, PHP drivers added classification rank retrieval when using ODBC Driver 17.4.2 or above. The user may define rank when using [ADD SENSITIVITY CLASSIFICATION](../../t-sql/statements/add-sensitivity-classification-transact-sql.md) to classify any data column. 

For example, if the user assigns `NONE` and `LOW` to BirthDate and SSN respectively, the JSON representation is shown as follows:

```
{"0":{"Label":{"name":"Confidential Personal Data","id":""},"Information Type":{"name":"Birthdays","id":""},"rank":0},"rank":0}
{"0":{"Label":{"name":"Highly Confidential - secure privacy","id":""},"Information Type":{"name":"Credentials","id":""},"rank":10},"rank":10}
```

As shown in [sensitivity classification](../../relational-databases/system-catalog-views/sys-sensitivity-classifications-transact-sql.md), the numerical values of the ranks are:

```
0 for NONE
10 for LOW
20 for MEDIUM
30 for HIGH
40 for CRITICAL
```

Hence, if instead of `RANK=NONE`, the user defines `RANK=CRITICAL` when classifying the column BirthDate, the classification metadata will be:

```
  array(7) {
    ["Name"]=>
    string(9) "BirthDate"
    ["Type"]=>
    int(91)
    ["Size"]=>
    NULL
    ["Precision"]=>
    int(10)
    ["Scale"]=>
    int(0)
    ["Nullable"]=>
    int(1)
    ["Data Classification"]=>
    array(2) {
      [0]=>
      array(3) {
        ["Label"]=>
        array(2) {
          ["name"]=>
          string(26) "Confidential Personal Data"
          ["id"]=>
          string(0) ""
        }
        ["Information Type"]=>
        array(2) {
          ["name"]=>
          string(9) "Birthdays"
          ["id"]=>
          string(0) ""
        }
        ["rank"]=>
        int(40)
      }
      ["rank"]=>
      int(40)
    }
  }
```

The updated JSON representation is shown below:

```
{"0":{"Label":{"name":"Confidential Personal Data","id":""},"Information Type":{"name":"Birthdays","id":""},"rank":40},"rank":40}
```

## See Also  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  

[Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md)  

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)