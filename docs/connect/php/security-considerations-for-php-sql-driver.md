---
title: "Security Considerations for the Microsoft Drivers for PHP for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "03/26/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "security considerations"
ms.assetid: a8c1a570-9204-454f-b94c-ba34f54d487c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Security Considerations for the Microsoft Drivers for PHP for SQL Server
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This topic describes security considerations that are specific to developing, deploying, and running applications that use the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]. For more detailed information about SQL Server security, see [Overview of SQL Server Security](https://docs.microsoft.com/dotnet/framework/data/adonet/sql/overview-of-sql-server-security).  
  
## Connect Using Windows Authentication  
Windows Authentication should be used to connect to SQL Server whenever possible for the following reasons:  
  
-   **No credentials are passed over the network during authentication.** User names and passwords are not embedded in the database connection string. Therefore, malicious users or attackers cannot obtain the credentials by monitoring the network or by viewing connection strings inside configuration files.  
  
-   **Users are subject to centralized account management.** Security policies such as password expiration periods, minimum password lengths, and account lockout after multiple invalid logon requests are enforced.  
  
For information about how to connect to a server with Windows Authentication, see [How to: Connect using Windows Authentication](../../connect/php/how-to-connect-using-windows-authentication.md).  
  
When you connect using Windows Authentication, it is recommended that you configure your environment so that SQL Server can use the Kerberos authentication protocol. For more information, see [How to make sure that you are using Kerberos authentication when you create a remote connection to an instance of SQL Server 2005](https://support.microsoft.com/en-ca/help/909801/how-to-make-sure-that-you-are-using-kerberos-authentication-when-you-c) or [Kerberos Authentication and SQL Server](https://msdn.microsoft.com/library/cc280744.aspx).  
  
## Use Encrypted Connections when Transferring Sensitive Data  
Encrypted connections should be used whenever sensitive data is being sent to or retrieved from SQL Server. For information about how to enable encrypted connections, see [How to Enable Encrypted Connections to the Database Engine (SQL Server Configuration Manager)](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md). To establish a secure connection with the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)], use the Encrypt connection attribute when connecting to the server. For more information about connection attributes, see [Connection Options](../../connect/php/connection-options.md).  
  
## Use Parameterized Queries  
Use parameterized queries to reduce the risk of SQL injection attacks. For examples of executing parameterized queries, see [How to: Perform Parameterized Queries](../../connect/php/how-to-perform-parameterized-queries.md).  
  
For more information about SQL injection attacks and related security considerations, see [SQL Injection](https://msdn.microsoft.com/library/ms161953.aspx).  
  
## Do Not Accept Server or Connection String Information from End Users  
Write applications so that end users cannot submit server or connection string information to the application. Maintaining strict control over server and connection string information reduces the surface area for malicious activity.  
  
## Turn WarningsAsErrors On During Application Development  
Develop applications with the **WarningsAsErrors** setting set to **true** so that warnings issued by the driver will be treated as errors. This will allow you to address warnings before you deploy your application. For more information, see [Handling Errors and Warnings](../../connect/php/handling-errors-and-warnings.md).  
  
## Secure Logs for Deployed Application  
For deployed applications, make sure that logs are written to a secure location or that logging is turned off. This helps protect against the possibility of end-users accessing information that has been written to the log files. For more information, see [Logging Activity](../../connect/php/logging-activity.md).  
  
## See Also  
[Programming Guide for the Microsoft Drivers for PHP for SQL Server](../../connect/php/programming-guide-for-php-sql-driver.md)
  
