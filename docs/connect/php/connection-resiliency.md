---
title: "Idle Connection Resiliency"
ms.date: "07/13/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.custom: ""
ms.technology: connectivity
ms.topic: conceptual
author: "david-puglielli"
ms.author: "v-dapugl"
manager: "v-hakaka"
---
# Idle Connection Resiliency
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

[Connection resiliency](../odbc/windows/connection-resiliency-in-the-windows-odbc-driver.md) is the principle that a broken idle connection can be reestablished, within certain constraints. If a connection to Microsoft SQL Server fails, connection resiliency allows the client to automatically attempt to reestablish the connection. Connection resiliency is a property of the data source; only SQL Server 2014 and later and Azure SQL Database support connection resiliency.

Connection resiliency is implemented with two connection keywords that can be added to connection strings: **ConnectRetryCount** and **ConnectRetryInterval**.

|Keyword|Values|Default|Description|
|-|-|-|-|
|**ConnectRetryCount**| Integer between 0 and 255 (inclusive)|1|The maximum number of attempts to reestablish a broken connection before giving up. By default, a single attempt is made to reestablish a connection when broken. A value of 0 means that no reconnection will be attempted.|
|**ConnectRetryInterval**| Integer between 1 and 60 (inclusive)|1| The time, in seconds, between attempts to reestablish a connection. The application will attempt to reconnect immediately upon detecting a broken connection, and will then wait **ConnectRetryInterval** seconds before trying again. This keyword is ignored if **ConnectRetryCount** is equal to 0.

If the product of **ConnectRetryCount** multiplied by **ConnectRetryInterval** is larger than **LoginTimeout**, then the client will cease attempting to connect once **LoginTimeout** is reached; otherwise, it will continue to try to reconnect until **ConnectRetryCount** is reached.

#### Remarks

Connection resiliency applies when the connection is idle. Failures that occur while executing a transaction, for example, will not trigger reconnection attempts - they will fail as would otherwise be expected. The following situations, known as non-recoverable session states, will not trigger reconnection attempts:

* Temporary tables
* Global and local cursors
* Transaction context and session level transaction locks
* Application locks
* EXECUTE AS/REVERT security context
* OLE automation handles
* Prepared XML handles
* Trace flags

## Example

The following code connects to a database and executes a query. The connection is interrupted by killing the session and a new query is attempted using the broken connection. This example uses the [AdventureWorks](https://msdn.microsoft.com/library/ms124501%28v=sql.100%29.aspx) sample database.

In this example, we specify a buffered cursor before breaking the connection. If we do not specify a buffered cursor, the connection would not be reestablished because there would be an active server-side cursor and thus the connection would not be idle when broken. However, in that case we could call sqlsrv_free_stmt() before breaking the connection to vacate the cursor, and the connection would be successfully reestablished.

```php
<?php
// This function breaks the connection by determining its session ID and killing it.
// A separate connection is used to break the main connection because a session
// cannot kill itself. The sleep() function ensures enough time has passed for KILL
// to finish ending the session.
function BreakConnection( $conn, $conn_break )
{
    $stmt1 = sqlsrv_query( $conn, "SELECT @@SPID" );
    if ( sqlsrv_fetch( $stmt1 ) )
    {
        $spid=sqlsrv_get_field( $stmt1, 0 );
    }

    $stmt2 = sqlsrv_prepare( $conn_break, "KILL ".$spid );
    sqlsrv_execute( $stmt2 );
    sleep(1);
}

// Connect to the local server using Windows authentication and specify
// AdventureWorks as the database in use. Specify values for
// ConnectRetryCount and ConnectRetryInterval as well.
$databaseName = 'AdventureWorks2014';
$serverName = '(local)';
$connectionInfo = array( "Database"=>$databaseName, "ConnectRetryCount"=>10, "ConnectRetryInterval"=>10 );

$conn = sqlsrv_connect( $serverName, $connectionInfo );
if( $conn === false)  
{  
     echo "Could not connect.\n";  
     die( print_r( sqlsrv_errors(), true));  
}

// A separate connection that will be used to break the main connection $conn
$conn_break = sqlsrv_connect( $serverName, array( "Database"=>$databaseName) );

// Create a statement to retrieve the contents of a table
$stmt1 = sqlsrv_query( $conn, "SELECT * FROM HumanResources.Employee",
                       array(), array( "Scrollable"=>"buffered" ) );
if( $stmt1 === false )
{
     echo "Error in statement 1.\n";
     die( print_r( sqlsrv_errors(), true ));
}
else
{
    echo "Statement 1 successful.\n";
    $rowcount = sqlsrv_num_rows( $stmt1 );
    echo $rowcount." rows in result set.\n";
}

// Now break the connection $conn
BreakConnection( $conn, $conn_break );

// Create another statement. The connection will be reestablished.
$stmt2 = sqlsrv_query( $conn, "SELECT * FROM HumanResources.Department",
                       array(), array( "Scrollable"=>"buffered" ) );
if( $stmt2 === false )
{
     echo "Error in statement 2.\n";
     die( print_r( sqlsrv_errors(), true ));
}
else
{
    echo "Statement 2 successful.\n";
    $rowcount = sqlsrv_num_rows( $stmt2 );
    echo $rowcount." rows in result set.\n";
}

sqlsrv_close( $conn );
sqlsrv_close( $conn_break );
?>
```
Expected output:
```
Statement 1 successful.
290 rows in result set.
Statement 2 successful.
16 rows in result set.
```

## See Also
[Connection Resiliency in the Windows ODBC Driver](../odbc/windows/connection-resiliency-in-the-windows-odbc-driver.md)
