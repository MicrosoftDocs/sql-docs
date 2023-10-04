---
title: "How to: Retrieve Output Parameters Using the SQLSRV Driver"
description: "Learn how to use and retrieve output parameters in a stored procedure with the Microsoft SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "04/11/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "stored procedure support"
---
# How to: Retrieve Output Parameters Using the SQLSRV Driver
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This topic demonstrates how to call a stored procedure in which one parameter has been defined as an output parameter. When retrieving an output or input/output parameter, all results returned by the stored procedure must be consumed before the returned parameter value is accessible.  
  
> [!NOTE]  
> Variables that are initialized or updated to **null**, **DateTime**, or stream types cannot be used as output parameters.  
  
Data truncation can occur when stream types such as SQLSRV_SQLTYPE_VARCHAR('max') are used as output parameters. Stream types are not supported as output parameters. For non-stream types, data truncation can occur if the length of the output parameter is not specified or if the specified length is not sufficiently large for the output parameter.  
  
## Example 1
The following example calls a stored procedure that returns the year-to-date sales by a specified employee. The PHP variable *$lastName* is an input parameter and *$salesYTD* is an output parameter.  
  
> [!NOTE]  
> Initializing *$salesYTD* to 0.0 sets the returned PHPTYPE to **float**. To ensure data type integrity, output parameters should be initialized before calling the stored procedure, or the desired PHPTYPE should be specified. For information about specifying the PHPTYPE, see [How to: Specify PHP Data Types](../../connect/php/how-to-specify-php-data-types.md).  
  
Because only one result is returned by the stored procedure, *$salesYTD* contains the returned value of the output parameter immediately after the stored procedure is executed.  
  
> [!NOTE]  
> Calling stored procedures using canonical syntax is the recommended practice. For more information about canonical syntax, see [Calling a Stored Procedure](../../relational-databases/native-client-odbc-stored-procedures/calling-a-stored-procedure.md).  
  
The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
```  
<?php  
/* Connect to the local server using Windows Authentication and   
specify the AdventureWorks database as the database in use. */  
$serverName = "(local)";  
$connectionInfo = array( "Database"=>"AdventureWorks");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
if( $conn === false )  
{  
     echo "Could not connect.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Drop the stored procedure if it already exists. */  
$tsql_dropSP = "IF OBJECT_ID('GetEmployeeSalesYTD', 'P') IS NOT NULL  
                DROP PROCEDURE GetEmployeeSalesYTD";  
$stmt1 = sqlsrv_query( $conn, $tsql_dropSP);  
if( $stmt1 === false )  
{  
     echo "Error in executing statement 1.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Create the stored procedure. */  
$tsql_createSP = " CREATE PROCEDURE GetEmployeeSalesYTD  
                   @SalesPerson nvarchar(50),  
                   @SalesYTD money OUTPUT  
                   AS  
                   SELECT @SalesYTD = SalesYTD  
                   FROM Sales.SalesPerson AS sp  
                   JOIN HumanResources.vEmployee AS e   
                   ON e.EmployeeID = sp.SalesPersonID  
                   WHERE LastName = @SalesPerson";  
$stmt2 = sqlsrv_query( $conn, $tsql_createSP);  
if( $stmt2 === false )  
{  
     echo "Error in executing statement 2.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/*--------- The next few steps call the stored procedure. ---------*/  
  
/* Define the Transact-SQL query. Use question marks (?) in place of  
 the parameters to be passed to the stored procedure */  
$tsql_callSP = "{call GetEmployeeSalesYTD( ?, ? )}";  
  
/* Define the parameter array. By default, the first parameter is an  
INPUT parameter. The second parameter is specified as an OUTPUT  
parameter. Initializing $salesYTD to 0.0 sets the returned PHPTYPE to  
float. To ensure data type integrity, output parameters should be  
initialized before calling the stored procedure, or the desired  
PHPTYPE should be specified in the $params array.*/  
$lastName = "Blythe";  
$salesYTD = 0.0;  
$params = array(   
                 array($lastName, SQLSRV_PARAM_IN),  
                 array(&$salesYTD, SQLSRV_PARAM_OUT)  
               );  
  
/* Execute the query. */  
$stmt3 = sqlsrv_query( $conn, $tsql_callSP, $params);  
if( $stmt3 === false )  
{  
     echo "Error in executing statement 3.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Display the value of the output parameter $salesYTD. */  
echo "YTD sales for ".$lastName." are ". $salesYTD. ".";  
  
/*Free the statement and connection resources. */  
sqlsrv_free_stmt( $stmt1);  
sqlsrv_free_stmt( $stmt2);  
sqlsrv_free_stmt( $stmt3);  
sqlsrv_close( $conn);  
?>  
```  

> [!NOTE]
> When binding an output parameter to a bigint type, if the value may end up outside the range of an [integer](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md), you will need to specify its SQL field type as SQLSRV_SQLTYPE_BIGINT. Otherwise, it may result in a "value out of range" exception.

## Example 2
This code sample shows how to bind a large bigint value as an output parameter.  

```
<?php
$serverName = "(local)";
$connectionInfo = array("Database"=>"testDB");  
$conn = sqlsrv_connect($serverName, $connectionInfo);  
if ($conn === false) {  
    echo "Could not connect.\n";  
    die(print_r(sqlsrv_errors(), true));  
}  

// Assume the stored procedure spTestProcedure exists, which retrieves a bigint value of some large number
// e.g. 9223372036854
$bigintOut = 0;
$outSql = "{CALL spTestProcedure (?)}";
$stmt = sqlsrv_prepare($conn, $outSql, array(array(&$bigintOut, SQLSRV_PARAM_OUT, null, SQLSRV_SQLTYPE_BIGINT)));
sqlsrv_execute($stmt);
echo "$bigintOut\n";   // Expect 9223372036854

sqlsrv_free_stmt($stmt);  
sqlsrv_close($conn);  

?>
```

## See Also  
[How to: Specify Parameter Direction Using the SQLSRV Driver](../../connect/php/how-to-specify-parameter-direction-using-the-sqlsrv-driver.md)

[How to: Retrieve Input and Output Parameters Using the SQLSRV Driver](../../connect/php/how-to-retrieve-input-and-output-parameters-using-the-sqlsrv-driver.md)

[Retrieving Data](../../connect/php/retrieving-data.md)  
  
