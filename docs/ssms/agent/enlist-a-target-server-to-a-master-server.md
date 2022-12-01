---
description: "Enlist a Target Server to a Master Server"
title: "Enlist a Target Server to a Master Server"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "enlisting target servers [SQL Server]"
  - "SQL Server Agent jobs, target servers"
  - "master servers [SQL Server], enlisting target servers"
  - "SQL Server Agent jobs, master servers"
  - "target servers [SQL Server], enlisting"
ms.assetid: 7633adb5-d140-4e58-a8f2-5b4b50c2f95b
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Enlist a Target Server to a Master Server
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to add target servers to a multiserver administration configuration. Run this procedure from the master server. in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio, [!INCLUDE[tsql](../../includes/tsql-md.md)], or SQL Server Management Objects (SMO).  
  
For information about how the Windows account used for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service affects a multiserver environment, see [Create a Multiserver Environment](../../ssms/agent/create-a-multiserver-environment.md).  
  
Full Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), encryption and certificate validation is enabled for connections between master servers and target servers by default. For more information, see [Set Encryption Options on Target Servers](../../ssms/agent/set-encryption-options-on-target-servers.md).  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To enlist a target server  
  
1.  In **Object Explorer**, expand a server that is configured as a master server.  
  
2.  Right-click **SQL Server Agent**, point to **Multi Server Administration**, and then click **Add Target Servers**.  
  
3.  Complete the Target Server Wizard, which guides you through the process.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To enlist a target server  
  
1.  Use the **sp_msx_enlist** stored procedure.  For more information, see [sp_msx_enlist (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-msx-enlist-transact-sql.md)  
  
## See Also  
[Automated Administration Across an Enterprise](../../ssms/agent/automated-administration-across-an-enterprise.md)  
