---
title: Configure SQL Server to receive remote table copies - Parallel Data Warehouse | Microsoft Docs
description: Describes how to configure an external SMP SQL Server instance to receive remote table copies from Parallel Data Warehouse. 
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Configure an external SMP SQL Server to receive remote table copies - Parallel Data Warehouse
Describes how to configure an external SQL Server instance to receive remote table copies from Parallel Data Warehouse.  

This topic describes one of the configuration steps for configuring remote table copy. For a list of all the configuration steps, see [Remote Table Copy](remote-table-copy.md).  
  
## Before You Begin  
Before you can configure the external SQL Server, you must:  
  
-   Have a Windows system with SQL Server 2008 Enterprise Edition or a later version ready to be installed or already installed. The Windows system must already be configured according to the instructions in [Configure an External Windows System To Receive Remote Table Copies Using InfiniBand](configure-an-external-windows-system-to-receive-remote-table-copies-using-infiniband.md).  
  
-   A Windows admin account with the ability to configure the SQL Server instance and the Windows system.  
  
-   A SQL Server login account (if SQL Server is already installed) with the ability to create logins and grant permissions on the destination database(s).  
  
## <a name="HowToSQLServer"></a>Configure an External SMP SQL Server To Receive Remote Table Copies  
The remote table copy feature copies tables from the SQL Server PDW appliance to an external SMP SQL Server database that is running on a Windows system. After configuring the external Windows system to receive remote table copies, the next step is to install and configure SQL Server onto the Windows system.  
  
To configure SQL Server, use the following steps:  
  
1.  Install SQL Server 2008 Enterprise Edition or a later version on the Windows system. This is referred to as the SMP SQL Server.  
  
2.  Configure SQL Server to accept TCP/IP connections on a fixed TCP port. This configuration is disabled by default and must be enabled to allow SQL Server PDW to connect to the SMP SQL Server.  
  
3.  Either disable Windows firewall or configure the SMP SQL Server TCP port so that it will work with Windows firewall enabled.  
  
4.  Configure SQL Server to allow SQL Server authentication mode. The parallel data export always uses SQL Server accounts for authentication.  
  
5.  Determine a SQL Server account on the SMP SQL Server that will be used for authentication. Grant that account the privilege to create, drop, and insert data into tables in the destination database for the parallel data export operation.  
  
## <a name="BPSQLConfig"></a>Best Practices for SMP SQL Server Configuration for Remote Table Copy  
When configuring the SMP SQL Server to receive remote table copies, use the following best practices to improve performance.  
  
1.  Follow best practices as documented in SQL Server product documentation. For example, enable data encryption. For more information about securing SQL Server, see [Securing SQL Server](../relational-databases/security/securing-sql-server.md) on MSDN.  
  
2.  Use the bulk-logged or simple recovery model.  
  
    During the parallel data export operations, data is bulk inserted into the newly created destination table. For maximum performance during the bulk insert, set the destination database to use the bulk-logged or the simple recovery model.  
  
3.  Use the batch_size option to reclaim log space.  
  
    Although the bulk-logged or simple recovery models use minimal logging for the bulk inserted data, some logging still occurs. To prevent the log files from growing too large, use the SQL Server batch_size option to periodically reclaim log space.  
  
<!-- MISSING LINKS 
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
-->
  
