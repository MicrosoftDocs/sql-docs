---
title: "sys.sp_drop_trusted_assembly (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
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
caps.latest.revision: 
author: "tmullaney"
ms.author: "thmullan;rickbyh"
manager: "jhubbard"
---
# sys.sp_drop_trusted_assembly (Transact-SQL)  
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]

Adds an assembly to the list of trusted assemblies for the server.

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  


## Syntax
```  
sp_drop_trusted_assembly 
    [ @clr_name = ] 'clr_name'
```  

## Arguments

[ @clr_name = ] '*clr_name*'  
Canonical name that encodes the simple name, version number, culture, public key, and architecture of the assembly to trust. This value uniquely identifies the assembly on the common language runtime (CLR) side. The value is the same as the clr_name value in sys.assemblies.

## Remarks  

This procedure removes an assembly from **Need to add sys.trusted_assemblies**.

## Permissions

Requires membership in the `sysadmin` fixed server role or `CONTROL SERVER` permission.

## Examples  

The following example drops an assembly named `pointudt` from the list of trusted assemblies for the server.  

```  
EXEC sp_drop_trusted_assembly 
N'pointudt, version=0.0.0.0, culture=neutral, publickeytoken=null, processorarchitecture=msil'; 
```  

## See Also  
  **Need to add sp_add_trusted_assembly**
  **Need to add sys.trusted_assemblies**
  [DROP ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-assembly-transact-sql.md)  
  [sys.assemblies](../../relational-databases/system-catalog-views/sys-assemblies-transact-sql.md)  
  [sys.dm_clr_loaded_assemblies](../../relational-databases/system-dynamic-management-views/sys-dm-clr-loaded-assemblies-transact-sql.md)  

