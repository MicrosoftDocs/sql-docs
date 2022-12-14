---
title: "Enabling CLR Integration"
description: Microsoft SQL Server hosting CLR is called CLR integration, which is disabled by default. Use the sp_configure stored procedure to enable CLR integration.
author: rwestMSFT
ms.author: randolphwest
ms.date: "01/14/2022"
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "clr enabled option"
  - "common language runtime [SQL Server], enabling"
---
# CLR Integration - Enabling
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  The common language runtime (CLR) integration feature is off by default, and must be enabled in order to use objects that are implemented using CLR integration. To enable CLR integration, use the **clr enabled** option of the **sp_configure** stored procedure in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]:  
  
```sql  
EXEC sp_configure 'clr enabled', 1;  
RECONFIGURE;  
GO  
```  
  
 You can disable CLR integration by setting the `clr enabled` option to 0. When you disable CLR integration, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stops executing all user-defined CLR routines and unloads all application domains. Features that rely upon the CLR, such as the **hierarchyid** data type, the `FORMAT` function, replication, and Policy-Based Management, are not affected by this setting and will continue to function.

> [!NOTE]
> Though the `clr enabled` configuration option is enabled in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], developing CLR user functions are not supported in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].
  
## Permissions

To enable CLR integration, you must have ALTER SETTINGS server level permission, which is implicitly held by members of the **sysadmin** and **serveradmin** fixed server roles.  

## Remarks
  
Computers configured with large amounts of memory and a large number of processors may fail to load the CLR integration feature of SQL Server when starting the server. To address this issue, start the server by using the **-gmemory_to_reserve**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service startup option, and specify a memory value large enough. For more information, see [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md).  
  
> [!NOTE]  
>  Common language runtime (CLR) execution is not supported under lightweight pooling. Before enabling CLR integration, you must disable lightweight pooling. For more information, see [lightweight pooling Server Configuration Option](../../database-engine/configure-windows/lightweight-pooling-server-configuration-option.md).  
  
## See Also  
 - [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 - [clr enabled Server Configuration Option](../../database-engine/configure-windows/clr-enabled-server-configuration-option.md)   
 - [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 - [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 - [Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md)  
  
  
