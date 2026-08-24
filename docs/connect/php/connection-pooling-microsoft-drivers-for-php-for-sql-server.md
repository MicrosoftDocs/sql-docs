---
title: "Connection Pooling (Microsoft Drivers for PHP for SQL Server)"
description: "Learn the details of connection pooling when using the Microsoft Drivers for PHP for SQL Server and how the experience may differ depending on your operating system."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 07/23/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
helpviewer_keywords:
  - "connection pooling support"
---
# Connection Pooling (Microsoft Drivers for PHP for SQL Server)
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

The following points are important about connection pooling in the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]:  
  
-   The [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] uses ODBC connection pooling.  
  
-   By default, connection pooling is enabled in Windows. In Linux and macOS, connections are pooled only if connection pooling is enabled for ODBC (see [Enabling or disabling connection pooling](#enablingdisabling-connection-pooling)). When connection pooling is enabled and you connect to a server, the driver attempts to use a pooled connection before it creates a new one. If an equivalent connection isn't found in the pool, a new connection is created and added to the pool. The driver determines whether connections are equivalent based on a comparison of connection strings.  
  
-   When a connection from the pool is used, the connection state is reset (Windows only).  
  
-   Closing the connection returns the connection to the pool.  
  
For more information about connection pooling, see [Driver Manager Connection Pooling](../../odbc/reference/develop-app/driver-manager-connection-pooling.md).  
  
## Enabling/Disabling connection pooling
### Windows
You can force the driver to create a new connection (instead of looking for an equivalent connection in the connection pool) by setting the value of the *ConnectionPooling* attribute in the connection string to **false** (or 0).  
  
If the *ConnectionPooling* attribute is omitted from the connection string or if it's set to **true** (or 1), the driver only creates a new connection if an equivalent connection doesn't exist in the connection pool.  

> [!NOTE]  
> Multiple Active Result Sets (MARS) is enabled by default. When both MARS and pooling are in use, for MARS to work correctly, the driver requires a longer time to reset the connection on the *first* query, thus ignoring any query timeout specified. However, the query timeout setting takes effect in subsequent queries.
  
If necessary, see [How to: Disable Multiple Active Resultsets (MARS)](../../connect/php/how-to-disable-multiple-active-resultsets-mars.md). For information about other connection attributes, see [Connection Options](../../connect/php/connection-options.md).  

### Linux and macOS
The *ConnectionPooling* attribute can't be used to enable or disable connection pooling. 

Connection pooling can be enabled or disabled by editing the odbcinst.ini configuration file. The driver should be reloaded for the changes to take effect.

Setting `Pooling` to `Yes` and a positive `CPTimeout` value in the odbcinst.ini file enables connection pooling. 
```ini
[ODBC]
Pooling=Yes

[ODBC Driver 17 for SQL Server]
CPTimeout=<int value>
```
  
At a minimum, the odbcinst.ini file should look like the following example:

```ini
[ODBC]
Pooling=Yes

[ODBC Driver 17 for SQL Server]
Description=Microsoft ODBC Driver 17 for SQL Server
Driver=/opt/microsoft/msodbcsql17/lib64/libmsodbcsql-17.5.so.2.1
UsageCount=1
CPTimeout=120
```

Setting `Pooling` to `No` in the odbcinst.ini file forces the driver to create a new connection.
```ini
[ODBC]
Pooling=No
```

## Remarks
- In Linux or macOS, connection pooling is not recommended with unixODBC < 2.3.7. All connections will be pooled if pooling is enabled in the odbcinst.ini file, which means the ConnectionPooling connection option has no effect. To disable pooling, set Pooling=No in the odbcinst.ini file and reload the drivers. 
  - unixODBC <= 2.3.4 (Linux and macOS) might not return proper diagnostic information, such as error messages, warnings, and informative messages.
  - For this reason, SQLSRV and PDO_SQLSRV drivers might not be able to properly fetch long data (such as xml, binary) as strings. Long data can be fetched as streams as a workaround. See the following example for SQLSRV.

- When using `AccessToken` with connection pooling, the driver appends a hash of the token content to the `APP` connection keyword to ensure that connections with different access tokens are not pooled together. This prevents identity cross-contamination where a pooled connection authenticated as one identity could be reused for a different identity. Token data is cached with TTL-based expiry to ensure pointer stability for the ODBC driver. This behavior was introduced in version 5.13.1.

```php
<?php
$connectionInfo = array("Database"=>"test", "UID"=>"username", "PWD"=>"password");

$conn1 = sqlsrv_connect("servername", $connectionInfo);

$longSample = str_repeat("a", 8500);
$xml1 = 
'<ParentXMLTag>
  <ChildTag01>'.$longSample.'</ChildTag01>
</ParentXMLTag>';

// Create table and insert xml string into it
sqlsrv_query($conn1, "CREATE TABLE xml_table (field xml)");
sqlsrv_query($conn1, "INSERT into xml_table values ('$xml1')");

// retrieve the inserted xml
$column1 = getColumn($conn1);

// return the connection to the pool
sqlsrv_close($conn1);

// This connection is from the pool
$conn2 = sqlsrv_connect("servername", $connectionInfo);
$column2 = getColumn($conn2);

sqlsrv_query($conn2, "DROP TABLE xml_table");
sqlsrv_close($conn2);

function getColumn($conn)
{
    $tsql = "SELECT * from xml_table";
    $stmt = sqlsrv_query($conn, $tsql);
    sqlsrv_fetch($stmt);
    // This might fail in Linux and macOS
    // $column = sqlsrv_get_field($stmt, 0, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR));
    // The workaround is to fetch it as a stream
    $column = sqlsrv_get_field($stmt, 0, SQLSRV_PHPTYPE_STREAM(SQLSRV_ENC_CHAR));
    sqlsrv_free_stmt($stmt);
    return ($column);
}
?>
```


## Related content

- [How to: Connect Using Windows Authentication](how-to-connect-using-windows-authentication.md)
- [How to: Connect Using SQL Server Authentication](how-to-connect-using-sql-server-authentication.md)
