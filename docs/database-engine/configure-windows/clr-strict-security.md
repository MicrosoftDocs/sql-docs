---
title: "CLR strict security | Microsoft Docs"
description: How to configure CLR strict security in SQL Server
ms.custom: ""
ms.date: "04/19/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "clr strict security"
  - "clr_strict_security_TSQL"
  - "clr strict security"
  - "strict_security_TSQL"
helpviewer_keywords: 
  - "assemblies [CLR integration], strick security"
  - "clr strict security option"
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
|1 |Enabled - Causes the [!INCLUDE[ssde-md](../../includes/ssde-md.md)] to ignore the `PERMISSION_SET` information on the assemblies, and always interpret them as `UNSAFE`.  `Enabled` is the default value for [!INCLUDE[sssqlv14](../../includes/sssqlv14-md.md)]. | 

>  [!WARNING]
>  CLR uses Code Access Security (CAS) in the .NET Framework, which is no longer supported as a security boundary. A CLR assembly created with `PERMISSION_SET = SAFE` may be able to access external system resources, call unmanaged code, and acquire sysadmin privileges. Beginning with [!INCLUDE[sssqlv14](../../includes/sssqlv14-md.md)], an `sp_configure` option called `clr strict security` is introduced to enhance the security of CLR assemblies. `clr strict security` is enabled by default, and treats `SAFE` and `EXTERNAL_ACCESS` assemblies as if they were marked `UNSAFE`. The `clr strict security` option can be disabled for backward compatibility, but this is not recommended. Microsoft recommends that all assemblies be signed by a certificate or asymmetric key with a corresponding login that has been granted `UNSAFE ASSEMBLY` permission in the master database.

## Remarks   

When enabled, the `PERMISSION_SET` option in the `CREATE ASSEMBLY` and `ALTER ASSEMBLY` statements is ignored at run-time, but the `PERMISSION_SET` options are preserved in metadata. Ignoring the option, minimizes breaking existing code statements.

`CLR strict security` is an `advanced option`.  

>  [!IMPORTANT]  
>  After enabling strict security, any assemblies that are not signed will fail to load. You must either alter or drop and recreate each assembly so that it is signed with a certificate or asymmetric key that has a corresponding login with the `UNSAFE ASSEMBLY` permission on the server.

## Permissions 

### To change this option  
Requires `CONTROL SERVER` permission, or membership in the `sysadmin` fixed server role.

### To create an CLR assembly   
The following permissions required to create a CLR assembly when `CLR strict security` is enabled:

- The user must have the `CREATE ASSEMBLY` permission  
- And one of the following conditions must also be true:  
  - The assembly is signed with a certificate or asymmetric key that has a corresponding login with the `UNSAFE ASSEMBLY` permission on the server. Signing the assembly is recommended.  
  - The database has the `TRUSTWORTHY` property set to `ON`, and the database is owned by a login that has the `UNSAFE ASSEMBLY` permission on the server. This option is not recommended.  

  
## See Also  
  
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [clr enabled Server Configuration Option](../../database-engine/configure-windows/clr-enabled-server-configuration-option.md)
