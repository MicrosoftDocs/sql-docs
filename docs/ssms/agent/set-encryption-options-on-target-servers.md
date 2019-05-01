---
title: "Set Encryption Options on Target Servers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent, encryption"
  - "target servers [SQL Server], encryption"
  - "multiserver environments [SQL Server], setting encryption options on target servers"
ms.assetid: 1a9fd539-e166-4ea8-9f21-ac400ca74dee
author: "markingmyname"
ms.author: "maghan"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Set Encryption Options on Target Servers
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

If you cannot use a certificate for Secure Sockets Layer (SSL) encrypted communications between master servers and some or all of your target servers, but you want to encrypt the channel between them, configure the target server to use the level of security required.  
  
To configure the appropriate level of security required for a specific master server/target server communication channel, set the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent registry subkey **\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\\**\<*instance_name*>**\SQLServerAgent\MsxEncryptChannelOptions(REG_DWORD)** on the target server to one of the following values. The value of \<*instance_name*> is **MSSQL.**_n_. For example, **MSSQL.1** or **MSSQL.3**.  
  
|Value|Description|  
|---------|---------------|  
|**0**|Disables encryption between this target server and the master server. Choose this option only when the channel between the target server and master server is secured by another means.|  
|**1**|Enables encryption only between this target server and the master server, but no certificate validation is required.|  
|**2**|Enables full SSL encryption and certificate validation between this target server and the master server. This setting is the default. Unless you have specific reason to choose a different value, we recommend not changing it.|  
  
If **1** or **2** is specified, you must have SSL enabled on both the master and target servers. If **2** is specified, you must also have a properly signed certificate present on the master server.  
  
> [!CAUTION]  
> [!INCLUDE[ssNoteRegistry](../../includes/ssnoteregistry-md.md)]  
  
## See Also  
[How to: Enable Encrypted Connections to the Database Engine (SQL Server Configuration Manager)](https://msdn.microsoft.com/e1e55519-97ec-4404-81ef-881da3b42006)  
  
