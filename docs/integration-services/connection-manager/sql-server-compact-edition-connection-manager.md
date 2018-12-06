---
title: "SQL Server Compact Edition Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.sqlmobileconnection.connection.f1"
  - "sql13.dts.designer.sqlmobileconnection.all.f1"
helpviewer_keywords: 
  - "SQL Server Compact, connection manager"
  - "connections [Integration Services], SQL Server Compact"
  - "connection managers [Integration Services], SQL Server Compact"
ms.assetid: ba627d4d-41f4-49fc-a921-f534cde67770
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# SQL Server Compact Edition Connection Manager
  A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact connection manager enables a package to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact destination that [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes uses this connection manager to load data into a table in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database.  
  
> [!NOTE]  
>  On a 64-bit computer, you must run packages that connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact data sources in 32-bit mode. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact provider that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] uses to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact data sources is available only in a 32-bit version.  
  
## Configuration the SQL Server Compact Edition Connection Manager  
 When you add a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact connection at run time, sets the connection manager properties, and adds the connection manager to the **Connections** collection on the package.  
  
 The **ConnectionManagerType** property of the connection manager is set to **SQLMOBILE**.  
  
 You can configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact connection manager in the following ways:  
  
-   Provide a connection string that specifies the location of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database.  
  
-   Provide a password for a password-protected database.  
  
-   Specify the server on which the database is stored.  
  
-   Indicate whether the connection that is created from the connection manager is retained at run time.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
## SQL Server Compact Edition Connection Manager Editor (Connection Page)
  Use the **SQL Server Compact Edition Connection Manager** dialog box to specify properties for connecting to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database.  
  
 To learn more about the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact Edition connection manager, see [SQL Server Compact Edition Connection Manager](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager.md).  
  
### Options  
 **Enter the database file name and path**  
 Enter the path and filename for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database.  
  
 **Browse**  
 Locate the desired [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database file by using the **Select SQL Server Compact Edition database** dialog box.  
  
 **Enter the database password**  
 Enter the password for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database.  
  
## SQL Server Compact Edition Connection Manager Editor (All Page)
  Use the **SQL Server Compact Edition Connection Manager** dialog box to specify properties for connecting to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database.  
  
 To learn more about the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact Edition connection manager, see [SQL Server Compact Edition Connection Manager](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager.md).  
  
### Options  
 **AutoShrink Threshold**  
 Specify the amount of free space, as a percentage, that is allowed in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database before the autoshrink process runs.  
  
 **Default Lock Escalation**  
 Specify the number of database locks that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database acquires before it tries to escalate locks.  
  
 **Default Lock Timeout**  
 Specify the default interval, in milliseconds, that a transaction will wait for a lock.  
  
 **Flush Interval**  
 Specify the interval, in seconds, between committed transactions to flush data to disk.  
  
 **Locale Identifier**  
 Specify the Locale ID (LCID) of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database.  
  
 **Max Buffer Size**  
 Specify the maximum amount of memory, in kilobytes, that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact uses before flushing data to disk.  
  
 **Max Database Size**  
 Specify the maximum size, in megabytes, of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database.  
  
 **Mode**  
 Specify the file mode in which to open the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database. The default value for this property is **Read Write**.  
  
 The Mode option has four values, as described in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Read Only**|Specifies read-only access to the database.|  
|**Read Write**|Specifies read/write permission to the database.|  
|**Exclusive**|Specifies exclusive access to the database.|  
|**Shared Read**|Specifies that other users can read from the database at the same time.|  
  
 **Persist Security Info**  
 Specify whether security information is returned as part of the connection string. The default value for this option is **False**.  
  
 **Temp File Directory**  
 Specify the location of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact temporary database file.  
  
 **Data Source**  
 Specify the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database.  
  
 **Password**  
 Enter the password for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database.  
  
