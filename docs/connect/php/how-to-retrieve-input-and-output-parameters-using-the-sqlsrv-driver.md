---
title: "How to: Retrieve I/O Parameters Using the SQLSRV Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "stored procedure support"
ms.assetid: 9a7c5f60-67f9-4968-a3a8-c256ee481da2
caps.latest.revision: 15
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# How to: Retrieve Input and Output Parameters Using the SQLSRV Driver
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This topic demonstrates how to use the SQLSRV driver to call a stored procedure in which one parameter has been defined as an input/output parameter, and how to retrieve the results. Note that when retrieving an output or input/output parameter, all results returned by the stored procedure must be consumed before the returned parameter value is accessible.  
  
> [!NOTE]  
> Variables that are initialized or updated to **null**, **DateTime**, or stream types cannot be used as output parameters.  
  
## Example  
The following example calls a stored procedure that subtracts used vacation hours from the available vacation hours of a specified employee. The variable that represents used vacation hours, *$vacationHrs*, is passed to the stored procedure as an input parameter. After updating the available vacation hours, the stored procedure uses the same parameter to return the number of remaining vacation hours.  
  
> [!NOTE]  
> Initializing *$vacationHrs* to 4 sets the returned PHPTYPE to integer. To ensure data type integrity, input/output parameters should be initialized before calling the stored procedure, or the desired PHPTYPE should be specified. For information about specifying the PHPTYPE, see [How to: Specify PHP Data Types](../../connect/php/how-to-specify-php-data-types.md).  
  
Because the stored procedure returns two results, [sqlsrv_next_result](../../connect/php/sqlsrv-next-result.md) must be called after the stored procedure has been executed to make the value of the output parameter available. After calling **sqlsrv_next_result**, *$vacationHrs* contains the value of the output parameter returned by the stored procedure.  
  
> [!NOTE]  
> Calling stored procedures using canonical syntax is the recommended practice. For more information about canonical syntax, see [Calling a Stored Procedure](http://go.microsoft.com/fwlink/?linkid=119517).  
  
The example assumes that SQL Server and the [AdventureWorks](http://go.microsoft.com/fwlink/?LinkID=67739) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
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
$tsql_dropSP = "IF OBJECT_ID('SubtractVacationHours', 'P') IS NOT NULL  
                DROP PROCEDURE SubtractVacationHours";  
$stmt1 = sqlsrv_query( $conn, $tsql_dropSP);  
if( $stmt1 === false )  
{  
     echo "Error in executing statement 1.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Create the stored procedure. */  
$tsql_createSP = "CREATE PROCEDURE SubtractVacationHours  
                        @EmployeeID int,  
                        @VacationHrs smallint OUTPUT  
                  AS  
                  UPDATE HumanResources.Employee  
                  SET VacationHours = VacationHours - @VacationHrs  
                  WHERE EmployeeID = @EmployeeID;  
                  SET @VacationHrs = (SELECT VacationHours  
                                      FROM HumanResources.Employee  
                                      WHERE EmployeeID = @EmployeeID)";  
  
$stmt2 = sqlsrv_query( $conn, $tsql_createSP);  
if( $stmt2 === false )  
{  
     echo "Error in executing statement 2.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/*--------- The next few steps call the stored procedure. ---------*/  
  
/* Define the Transact-SQL query. Use question marks (?) in place of  
the parameters to be passed to the stored procedure */  
$tsql_callSP = "{call SubtractVacationHours( ?, ?)}";  
  
/* Define the parameter array. By default, the first parameter is an  
INPUT parameter. The second parameter is specified as an INOUT  
parameter. Initializing $vacationHrs to 8 sets the returned PHPTYPE to  
integer. To ensure data type integrity, output parameters should be  
initialized before calling the stored procedure, or the desired  
PHPTYPE should be specified in the $params array.*/  
  
$employeeId = 4;  
$vacationHrs = 8;  
$params = array(   
                 array($employeeId, SQLSRV_PARAM_IN),  
                 array($vacationHrs, SQLSRV_PARAM_INOUT)  
               );  
  
/* Execute the query. */  
$stmt3 = sqlsrv_query( $conn, $tsql_callSP, $params);  
if( $stmt3 === false )  
{  
     echo "Error in executing statement 3.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Display the value of the output parameter $vacationHrs. */  
sqlsrv_next_result($stmt3);  
echo "Remaining vacation hours: ".$vacationHrs;  
  
/*Free the statement and connection resources. */  
sqlsrv_free_stmt( $stmt1);  
sqlsrv_free_stmt( $stmt2);  
sqlsrv_free_stmt( $stmt3);  
sqlsrv_close( $conn);  
?>  
```  
  
## See Also  
[How to: Specify Parameter Direction Using the SQLSRV Driver](../../connect/php/how-to-specify-parameter-direction-using-the-sqlsrv-driver.md)  
[How to: Retrieve Output Parameters Using the SQLSRV Driver](../../connect/php/how-to-retrieve-output-parameters-using-the-sqlsrv-driver.md)  
[Retrieving Data](../../connect/php/retrieving-data.md)  
  
