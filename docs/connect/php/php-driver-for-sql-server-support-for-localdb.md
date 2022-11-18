---
title: "PHP Driver Support for LocalDB"
description: "Learn how the Microsoft Drivers for PHP for SQL Server support connections to LocalDB database instances."
author: David-Engel
ms.author: v-davidengel
ms.date: "05/06/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Support for LocalDB

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

LocalDB is a lightweight version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] which has been available since [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. This topic discusses how to connect to a database in a LocalDB instance.

## Remarks

For more information about LocalDB, including how to install LocalDB and configure your LocalDB instance, see the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online topic on [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Express LocalDB.

In brief, LocalDB allows you to:

-   Use **sqllocaldb.exe i** to discover the name of the default instance.

-   Use the **AttachDBFilename** connection string keyword to specify which database file the server should attach. When using **AttachDBFilename**, if you do not specify the name of the database with the **Database** connection string keyword, the database will be removed from the LocalDB instance when the application closes.

-   Specify a LocalDB instance in your connection string. For example, here is a sample SQLSRV connection string:

    ```php
    $conn = sqlsrv_connect( '(localdb)\\v11.0',
        array( 'Database'=>'myData'));

    $conn = sqlsrv_connect( '(localdb)\\v11.0',
        array('AttachDBFileName'=>'c:\\myData.MDF','Database'=>'myData'));

    $conn = sqlsrv_connect( '(localdb)\\v11.0',
        array('AttachDBFileName'=>'c:\\myData.MDF'));
    ```

    Next is a sample PDO_SQLSRV connection string:  

    ```php
    $conn = new PDO( 'sqlsrv:server=(localdb)\\v11.0;'
        . 'Database=myData', NULL, NULL);

    $conn = new PDO( 'sqlsrv:server=(localdb)\\v11.0;'
        . 'AttachDBFileName=c:\\myData.MDF;Database=myData ',
        NULL, NULL);

    $conn = new PDO( 'sqlsrv:server=(localdb)\\v11.0;'
        . 'AttachDBFileName=c:\\myData.MDF', NULL, NULL);  
    ```

If necessary, you can create a LocalDB instance with sqllocaldb.exe. You can also use sqlcmd.exe to add and modify databases in a LocalDB instance. For example, `sqlcmd -S (localdb)\v11.0`. (When running in IIS, you need to run under the correct account to get the same results as when you run at the command line; see [Using LocalDB with Full IIS, Part 2: Instance Ownership](/archive/blogs/sqlexpress/using-localdb-with-full-iis-part-2-instance-ownership) for more information.)

The following are example connection strings using the SQLSRV driver that connect to a database in a LocalDB named instance called myInstance:

```php
$conn = sqlsrv_connect( '(localdb)\\myInstance',
    array( 'Database'=>'myData'));
```

The following are example connection strings using the PDO_SQLSRV driver that connect to a database in a LocalDB named instance called myInstance:  
  
```php
$conn = new PDO( 'sqlsrv:server=(localdb)\\myInstance;'
    . 'database=myData', NULL, NULL);
```

For instructions on installing LocalDB, see the [LocalDB documentation](../../database-engine/configure-windows/sql-server-express-localdb.md). If you use sqlcmd.exe to modify data in your LocalDB instance, you will need the [sqlcmd utility](../../tools/sqlcmd-utility.md).

## See Also

[Connecting to the Server](../../connect/php/connecting-to-the-server.md)