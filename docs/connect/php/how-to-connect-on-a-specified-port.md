---
title: "How to: Connect on a Specified Port | Microsoft Docs"
ms.custom: ""
ms.date: "03/26/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "connecting to the server, specifying a port"
ms.assetid: 65a154d1-375c-439b-a653-7815c9d70ff3
author: MightyPen
ms.author: genemi
manager: craigg
---
# How to: Connect on a Specified Port
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This topic describes how to connect to SQL Server on a specified port with the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
### To connect on a specified port  
  
1.  Verify the port on which the server is configured to accept connections. For information about configuring a server to accept connections on a specified port, see [How to: Configure a Server to Listen on a Specific TCP Port (SQL Server Configuration Manager)](../../database-engine/configure-windows/configure-a-server-to-listen-on-a-specific-tcp-port.md).  
  
2.  Add the desired port to the *$serverName* parameter of the [sqlsrv_connect](../../connect/php/sqlsrv-connect.md) function. Separate the server name and the port with a comma. For example, the following lines of code use the SQLSRV driver to demonstrate how to connect to a server named *myServer* on port 1521:  
  
    ```  
    $serverName = "myServer, 1521";  
    sqlsrv_connect( $serverName );  
    ```  
  
    The following lines of code use the PDO_SQLSRV driver to demonstrate how to connect to a server named *myServer* on port 1521:  
  
    ```  
    $serverName = "(local), 1521";  
    $database = "AdventureWorks";  
    $conn = new PDO( "sqlsrv:server=$serverName;Database=$database", "", "");  
    ```  
  
## See Also  
[Connecting to the Server](../../connect/php/connecting-to-the-server.md)

[Programming Guide for the Microsoft Drivers for PHP for SQL Server](../../connect/php/programming-guide-for-php-sql-driver.md)

[Getting Started with the Microsoft Drivers for PHP for SQL Server](../../connect/php/getting-started-with-the-php-sql-driver.md)

[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)

[PDO_SQLSRV Driver Reference](../../connect/php/pdo-sqlsrv-driver-reference.md)  
  
