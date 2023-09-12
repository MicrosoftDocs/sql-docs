---
title: "Connecting to the Server"
description: "Learn about the different methods to connect to the database using the Microsoft Drivers for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "03/26/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Connecting to the Server
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

The topics in this section describe the options and procedures for connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  

The [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] can connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using Windows Authentication or by using SQL Server Authentication. By default, the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] try to connect to the server by using Windows Authentication.  

## In This Section  

|Topic|Description|  
|---------|---------------|  
|[How to: Connect Using Windows Authentication](../../connect/php/how-to-connect-using-windows-authentication.md)|Describes how to establish a connection by using Windows Authentication.|  
|[How to: Connect Using SQL Server Authentication](../../connect/php/how-to-connect-using-sql-server-authentication.md)|Describes how to establish a connection by using SQL Server Authentication.|  
|[How to: Connect Using Azure Active Directory Authentication](../../connect/php/azure-active-directory.md)|Describes how to set the authentication mode and connect using Azure Active Directory identities.|  
|[How to: Connect on a Specified Port](../../connect/php/how-to-connect-on-a-specified-port.md)|Describes how to connect to the server on a specific port.|  
|[Connection Pooling](../../connect/php/connection-pooling-microsoft-drivers-for-php-for-sql-server.md)|Provides information about connection pooling in the driver.|  
|[How to: Disable Multiple Active Resultsets (MARS)](../../connect/php/how-to-disable-multiple-active-resultsets-mars.md)|Describes how to disable the MARS feature when making a connection.|  
|[Connection Options](../../connect/php/connection-options.md)|Lists the options that are permitted in the associative array that contains connection attributes.|  
|[Support for LocalDB](../../connect/php/php-driver-for-sql-server-support-for-localdb.md)|Describes [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] support for the LocalDB feature, which was added in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].|  
|[Support for High Availability, Disaster Recovery](../../connect/php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md)|Discusses how your application can be configured to take advantage of the high-availability, disaster recovery features added in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].|  
|[Connecting to Microsoft Azure SQL Database](../../connect/php/connecting-to-microsoft-azure-sql-database.md)|Discusses how to connect to an Azure SQL Database.|  
|[Connection Resiliency](../../connect/php/connection-resiliency.md)|Discusses the connection resiliency feature that reestablishes broken connections.|  

## See Also  
[Programming Guide for the Microsoft Drivers for PHP for SQL Server](../../connect/php/programming-guide-for-php-sql-driver.md)

[Example Application &#40;SQLSRV Driver&#41;](../../connect/php/example-application-sqlsrv-driver.md)  
