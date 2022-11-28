---
description: "sys.sp_drop_trusted_assembly (Transact-SQL)"
title: "sys.sp_drop_trusted_assembly (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_drop_trusted_assembly_TSQL"
  - "sp_drop_trusted_assembly"
  - "sys.sp_drop_trusted_assembly_TSQL"
  - "sys.sp_drop_trusted_assembly"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_drop_trusted_assembly"
ms.assetid: 
author: VanMSFT
ms.author: vanto
monikerRange: ">=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.sp_drop_trusted_assembly (Transact-SQL)  
[!INCLUDE[SQL Server 2017](../../includes/applies-to-version/sqlserver2017.md)]

Drops an assembly from the list of trusted assemblies on the server.

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  


## Syntax
```  
sp_drop_trusted_assembly 
    [ @hash = ] 'value'
```  

## Arguments

[ @hash = ] '*value*'  
The SHA2_512 hash value of the assembly to drop from the list of trusted assemblies for the server. Trusted assemblies may load when clr strict security is enabled, even if the assembly is unsigned or the database is not marked as trustworthy.

## Remarks  

This procedure removes an assembly from [sys.trusted_assemblies](../../relational-databases/system-catalog-views/sys-trusted-assemblies-transact-sql.md).

## Permissions

Requires membership in the `sysadmin` fixed server role or `CONTROL SERVER` permission.

## Examples  

The following example drops an assembly hash from the list of trusted assemblies for the server.  

```  
EXEC sp_drop_trusted_assembly 
0x8893AD6D78D14EE43DF482E2EAD44123E3A0B684A8873C3F7BF3B5E8D8F09503F3E62370CE742BBC96FE3394477214B84C7C1B0F7A04DCC788FA99C2C09DFCCC; 
```  

## See Also  
  [sys.sp_add_trusted_assembly](sys-sp-add-trusted-assembly-transact-sql.md)
  [sys.trusted_assemblies](../../relational-databases/system-catalog-views/sys-trusted-assemblies-transact-sql.md) 
  [DROP ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-assembly-transact-sql.md)  
  [sys.assemblies](../../relational-databases/system-catalog-views/sys-assemblies-transact-sql.md)  
  [sys.dm_clr_loaded_assemblies](../../relational-databases/system-dynamic-management-views/sys-dm-clr-loaded-assemblies-transact-sql.md)  

