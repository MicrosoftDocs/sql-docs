---
title: "How to: specify SQL Server data types when using the SQLSRV driver"
description: "Learn how to specify SQL Server Data Types with the optional *$params* array in prepared statements or direct queries when using the SQLSRV Driver for PHP"
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "converting data types"
  - "streaming data"
---
# How to: Specify SQL Server Data Types When Using the SQLSRV Driver
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This topic demonstrates how to use the SQLSRV driver to specify the SQL Server data type for data that is sent to the server. This topic does not apply when using the PDO_SQLSRV driver.  
  
To specify the SQL Server data type, you must use the optional *$params* array when you prepare or execute a query that inserts or updates data. For details about the structure and syntax of the *$params* array, see [sqlsrv_query](../../connect/php/sqlsrv-query.md) or [sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md).  
  
The following steps summarize how to specify the SQL Server data type when sending data to the server:  
  
> [!NOTE]  
> If no SQL Server data type is specified, default types are used. For information about default SQL Server data types, see [Default SQL Server Data Types](../../connect/php/default-sql-server-data-types.md).  
  
1.  Define a Transact-SQL query that inserts or updates data. Use question marks (?) as placeholders for parameter values in the query.  
  
2.  Initialize or update PHP variables that correspond to the placeholders in the Transact-SQL query.  
  
3.  Construct the *$params* array to be used when preparing or executing the query. Note that each element of the *$params* array must also be an array when you specify the SQL Server data type.  
  
4.  Specify the desired SQL Server data type by using the appropriate **SQLSRV_SQLTYPE_&#42;** constant as the fourth parameter in each subarray of the *$params* array. For a complete list of the **SQLSRV_SQLTYPE_&#42;** constants, see the SQLTYPEs section of [Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md). For example, in the code below, *$changeDate*, *$rate*, and *$payFrequency* are specified respectively as the SQL Server types **datetime**, **money**, and **tinyint** in the *$params* array. Because no SQL Server type is specified for *$employeeId* and it is initialized to an integer, the default SQL Server type **integer** is used.  
  
    ```  
    $employeeId = 5;  
    $changeDate = "2005-06-07";  
    $rate = 30;  
    $payFrequency = 2;  
    $params = array(  
                array($employeeId, null),  
                array($changeDate, null, null, SQLSRV_SQLTYPE_DATETIME),  
                array($rate, null, null, SQLSRV_SQLTYPE_MONEY),  
                array($payFrequency, null, null, SQLSRV_SQLTYPE_TINYINT)  
              );  
    ```  
  
## Example  
The following example inserts data into the *HumanResources.EmployeePayHistory* table of the AdventureWorks database. SQL Server types are specified for the *$changeDate*, *$rate*, and *$payFrequency* parameters. The default SQL Server type is used for the *$employeeId* parameter. To verify that the data was inserted successfully, the same data is retrieved and displayed.  
  
This example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
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
  
/* Define the query. */  
$tsql1 = "INSERT INTO HumanResources.EmployeePayHistory (EmployeeID,  
                                                        RateChangeDate,  
                                                        Rate,  
                                                        PayFrequency)  
           VALUES (?, ?, ?, ?)";  
  
/* Construct the parameter array. */  
$employeeId = 5;  
$changeDate = "2005-06-07";  
$rate = 30;  
$payFrequency = 2;  
$params1 = array(  
               array($employeeId, null),  
               array($changeDate, null, null, SQLSRV_SQLTYPE_DATETIME),  
               array($rate, null, null, SQLSRV_SQLTYPE_MONEY),  
               array($payFrequency, null, null, SQLSRV_SQLTYPE_TINYINT)  
           );  
  
/* Execute the INSERT query. */  
$stmt1 = sqlsrv_query($conn, $tsql1, $params1);  
if( $stmt1 === false )  
{  
     echo "Error in execution of INSERT.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Retrieve the newly inserted data. */  
/* Define the query. */  
$tsql2 = "SELECT EmployeeID, RateChangeDate, Rate, PayFrequency  
          FROM HumanResources.EmployeePayHistory  
          WHERE EmployeeID = ? AND RateChangeDate = ?";  
  
/* Construct the parameter array. */  
$params2 = array($employeeId, $changeDate);  
  
/*Execute the SELECT query. */  
$stmt2 = sqlsrv_query($conn, $tsql2, $params2);  
if( $stmt2 === false )  
{  
     echo "Error in execution of SELECT.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Retrieve and display the results. */  
$row = sqlsrv_fetch_array( $stmt2 );  
if( $row === false )  
{  
     echo "Error in fetching data.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
echo "EmployeeID: ".$row['EmployeeID']."\n";  
echo "Change Date: ".date_format($row['RateChangeDate'], "Y-m-d")."\n";  
echo "Rate: ".$row['Rate']."\n";  
echo "PayFrequency: ".$row['PayFrequency']."\n";  
  
/* Free statement and connection resources. */  
sqlsrv_free_stmt($stmt1);  
sqlsrv_free_stmt($stmt2);  
sqlsrv_close($conn);  
?>  
```  
  
## See Also  
[Retrieving Data](../../connect/php/retrieving-data.md)

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)

[How to: Specify PHP Data Types](../../connect/php/how-to-specify-php-data-types.md)

[Converting Data Types](../../connect/php/converting-data-types.md)

[How to: Send and Retrieve UTF-8 Data Using Built-In UTF-8 Support](../../connect/php/how-to-send-and-retrieve-utf-8-data-using-built-in-utf-8-support.md)  
  
