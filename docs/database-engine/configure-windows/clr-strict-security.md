---
title: "CLR strict security | Microsoft Docs"
ms.custom: ""
ms.date: "04/21/2017"
ms.prod: "sql-vnext"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 
caps.latest.revision: 0
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CLR strict security   
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]   

Controls the interpretation of the `SAFE`, `EXTERNAL ACCESS`, `UNSAFE` permission in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].   

|Value |Description | 
|----- |----- | 
|0 |Disabled - Provided for backwards compatibility. `Disabled` value is not recommended. | 
|1 |Enabled - The [!INCLUDE[ssde-md](../../includes/ssde-md.md)] causes the engine to ignore the permission_set information on the assemblies, and always interprets them as `UNSAFE`.  `Enabled` is the default value for [!INCLUDE[sssqlv14](../../includes/sssqlv14-md.md)]. | 

When enabled, the `PERMISSION_SET` option in the `CREATE ASSEMBLY` and `ALTER ASSEMBLY` statements is ignored at run-time, but the `PERMISSION_SET` options are preserved in metadata. Ignoring the option, minimizes breaking existing code statements.

`CLR strict security` is an `advanced option`.  

## Remarks   

The following permissions required to create a CLR assembly when `CLR strict security` is enabled:

- The user must have the `CREATE ASSEMBLY` permission  
- And one of the following conditions must also be true:  
  - The assembly is signed with a certificate or asymmetric key that has a corresponding login with the `UNSAFE ASSEMBLY` permission on the server. Signing the assembly is recommended.  
  - The database has the `TRUSTWORTHY` property set to `ON`, and the database is owned by a login that has the `UNSAFE ASSEMBLY` permission on the server. This option is not recommended.  

## Permissions  

Requires `CONTROL SERVER` permission, or membership in the `sysadmin` fixed server role.
  
  
## See Also  
  
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [clr enabled Server Configuration Option](../../database-engine/configure-windows/clr-enabled-server-configuration-option.md)
  
  
