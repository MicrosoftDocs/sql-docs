---
title: "sqlsrv_query | Microsoft Docs"
ms.custom: ""
ms.date: "04/11/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "sqlsrv_query"
apitype: "NA"
helpviewer_keywords: 
  - "sqlsrv_query"
  - "executing queries"
  - "API Reference, sqlsrv_query"
ms.assetid: 9fa7c4c8-4da8-4299-9893-f61815055aa3
author: MightyPen
ms.author: genemi
manager: craigg
---
# sqlsrv_query
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Prepares and executes a statement.  
  
## Syntax  
  
```  
  
sqlsrv_query(resource $conn, string $tsql [, array $params [, array $options]])  
```  
  
#### Parameters  
*$conn*: The connection resource associated with the prepared statement.  
  
*$tsql*: The Transact-SQL expression that corresponds to the prepared statement.  
  
*$params* [OPTIONAL]: An **array** of values that correspond to parameters in a parameterized query. Each element of the array can be one of the following:
  
-   A literal value.  
  
-   A PHP variable.  
  
-   An **array** with the following structure:  
  
    ```  
    array($value [, $direction [, $phpType [, $sqlType]]])  
    ```  
  
    The description for each element of the array is in the following table:  
  
    |Element|Description|  
    |-----------|---------------|  
    |*$value*|A literal value, a PHP variable, or a PHP by-reference variable.|  
    |*$direction*[OPTIONAL]|One of the following **SQLSRV_PARAM_\*** constants used to indicate the parameter direction: **SQLSRV_PARAM_IN**, **SQLSRV_PARAM_OUT**, **SQLSRV_PARAM_INOUT**. The default value is **SQLSRV_PARAM_IN**.<br /><br />For more information about PHP constants, see [Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md).|  
    |*$phpType*[OPTIONAL]|A **SQLSRV_PHPTYPE_\*** constant that specifies PHP data type of the returned value.<br /><br />For more information about PHP constants, see [Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md).|  
    |*$sqlType*[OPTIONAL]|A **SQLSRV_SQLTYPE_\*** constant that specifies the SQL Server data type of the input value.<br /><br />For more information about PHP constants, see [Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md).|  
  
*$options* [OPTIONAL]: An associative array that sets query properties. It is the same list of keys also supported by [sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md#properties).
  
## Return Value  
A statement resource. If the statement cannot be created and/or executed, **false** is returned.  
  
## Remarks  
The **sqlsrv_query** function is well-suited for one-time queries and should be the default choice to execute queries unless special circumstances apply. This function provides a streamlined method to execute a query with a minimum amount of code. The **sqlsrv_query** function does both statement preparation and statement execution, and can be used to execute parameterized queries.  
  
For more information, see [How to: Retrieve Output Parameters Using the SQLSRV Driver](../../connect/php/how-to-retrieve-output-parameters-using-the-sqlsrv-driver.md).  
  
## Example  
In the following example, a single row is inserted into the *Sales.SalesOrderDetail* table of the AdventureWorks database. The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
> [!NOTE]  
> Although the following example uses an INSERT statement to demonstrate the use of **sqlsrv_query** for a one-time statement execution, the concept applies to any Transact-SQL statement.  
  
```  
<?php  
/* Connect to the local server using Windows Authentication and  
specify the AdventureWorks database as the database in use. */  
$serverName = "(local)";  
$connectionInfo = array("Database"=>"AdventureWorks");  
$conn = sqlsrv_connect($serverName, $connectionInfo);  
if ($conn === false) {  
    echo "Could not connect.\n";  
    die(print_r(sqlsrv_errors(), true));  
}  
  
/* Set up the parameterized query. */  
$tsql = "INSERT INTO Sales.SalesOrderDetail   
        (SalesOrderID,   
         OrderQty,   
         ProductID,   
         SpecialOfferID,   
         UnitPrice,   
         UnitPriceDiscount)  
        VALUES   
        (?, ?, ?, ?, ?, ?)";  
  
/* Set parameter values. */  
$params = array(75123, 5, 741, 1, 818.70, 0.00);  
  
/* Prepare and execute the query. */  
$stmt = sqlsrv_query($conn, $tsql, $params);  
if ($stmt) {  
    echo "Row successfully inserted.\n";  
} else {  
    echo "Row insertion failed.\n";  
    die(print_r(sqlsrv_errors(), true));  
}  
  
/* Free statement and connection resources. */  
sqlsrv_free_stmt($stmt);  
sqlsrv_close($conn);  
?>  
```  
  
## Example  
The following example updates a field in the *Sales.SalesOrderDetail* table of the AdventureWorks database. The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
```  
<?php  
/* Connect to the local server using Windows Authentication and  
specify the AdventureWorks database as the database in use. */  
$serverName = "(local)";  
$connectionInfo = array("Database"=>"AdventureWorks");  
$conn = sqlsrv_connect($serverName, $connectionInfo);  
if ($conn === false) {  
    echo "Could not connect.\n";  
    die(print_r(sqlsrv_errors(), true));  
}  
  
/* Set up the parameterized query. */  
$tsql = "UPDATE Sales.SalesOrderDetail   
         SET OrderQty = (?)   
         WHERE SalesOrderDetailID = (?)";  
  
/* Assign literal parameter values. */  
$params = array(5, 10);  
  
/* Execute the query. */  
if (sqlsrv_query($conn, $tsql, $params)) {  
    echo "Statement executed.\n";  
} else {  
    echo "Error in statement execution.\n";  
    die(print_r(sqlsrv_errors(), true));  
}  
  
/* Free connection resources. */  
sqlsrv_close($conn);  
?>  
```  
  
> [!NOTE]
> It is recommended to use strings as inputs when binding values to a [decimal or numeric column](https://docs.microsoft.com/sql/t-sql/data-types/decimal-and-numeric-transact-sql) to ensure precision and accuracy as PHP has limited precision for [floating point numbers](https://php.net/manual/en/language.types.float.php). The same applies to bigint columns, especially when the values are outside the range of an [integer](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md).

## Example  
This code sample shows how to bind a decimal value as an input parameter.  

```
<?php
$serverName = "(local)";
$connectionInfo = array("Database"=>"YourTestDB");  
$conn = sqlsrv_connect($serverName, $connectionInfo);  
if ($conn === false) {  
     echo "Could not connect.\n";  
     die(print_r(sqlsrv_errors(), true));  
}  

// Assume TestTable exists with a decimal field 
$input = "9223372036854.80000";
$params = array($input);
$stmt = sqlsrv_query($conn, "INSERT INTO TestTable (DecimalCol) VALUES (?)", $params);

sqlsrv_free_stmt($stmt);  
sqlsrv_close($conn);  

?>
```

## Example
This code sample shows how to create a table of [sql_variant](https://docs.microsoft.com/sql/t-sql/data-types/sql-variant-transact-sql) types and fetch the inserted data.

```
<?php
$server = 'serverName';
$dbName = 'databaseName';
$uid = 'yourUserName';
$pwd = 'yourPassword';

$options = array("Database"=>$dbName, "UID"=>$uid, "PWD"=>$pwd);
$conn = sqlsrv_connect($server, $options);
if($conn === false) {
    die(print_r(sqlsrv_errors(), true));
}

$tableName = 'testTable';
$query = "CREATE TABLE $tableName ([c1_int] sql_variant, [c2_varchar] sql_variant)";

$stmt = sqlsrv_query($conn, $query);
if($stmt === false) {
    die(print_r(sqlsrv_errors(), true));
}
sqlsrv_free_stmt($stmt);

$query = "INSERT INTO [$tableName] (c1_int, c2_varchar) VALUES (1, 'test_data')";
$stmt = sqlsrv_query($conn, $query);
if($stmt === false) {
    die(print_r(sqlsrv_errors(), true));
}
sqlsrv_free_stmt($stmt);

$query = "SELECT * FROM $tableName";
$stmt = sqlsrv_query($conn, $query);

if(sqlsrv_fetch($stmt) === false) {
    die(print_r(sqlsrv_errors(), true));
}

$col1 = sqlsrv_get_field($stmt, 0);
echo "First field:  $col1 \n";

$col2 = sqlsrv_get_field($stmt, 1);
echo "Second field:  $col2 \n";

sqlsrv_free_stmt($stmt);
sqlsrv_close($conn);

?>
```

The expected output would be:

```
First field:  1
Second field:  test_data
```

## See Also  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  

[How to: Perform Parameterized Queries](../../connect/php/how-to-perform-parameterized-queries.md)  

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)  

[How to: Send Data as a Stream](../../connect/php/how-to-send-data-as-a-stream.md)  

[Using Directional Parameters](../../connect/php/using-directional-parameters.md)  

  
