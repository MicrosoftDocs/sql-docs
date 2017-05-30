---
title: "sys.trusted_assemblies (Transact-SQL) | Microsoft Docs"
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
  - "trusted_assemblies_TSQL"
  - "trusted_assemblies"
  - "sys.trusted_assemblies_TSQL"
  - "sys.trusted_assemblies"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.trusted_assemblies"
ms.assetid: 
caps.latest.revision: 
author: "tmullaney"
ms.author: "thmullan;rickbyh"
manager: "jhubbard"
---
# sys.trusted_assemblies (Transact-SQL)  
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]

Contains a row for each trusted assembly for the server.

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  


|Column name |Data type |Description |
|--- |--- |--- |
|clr_name |nvarchar(4000) |Canonical string that encodes the simple name, version number, culture, public key, and architecture of the assembly. This value uniquely identifies the assembly on the common language runtime (CLR) side. |
|hash |varbinary(8000) |SHA2_512 hash of the assembly content. |
|create_date |datetime2 |Date the assembly was added to the list of trusted assemblies. |
|created_by |nvarchar(128) |Login name of the principal who added the assembly to the list. |
| | | |


## Remarks  

Use **Need to add sp_add_trusted_assembly** and **Need to add sys.trusted_assemblies** add or remove assemblies from `sys.trusted_assemblies`.

## See Also  
  **Need to add sp_add_trusted_assembly**  
  **Need to add sys.trusted_assemblies**  
  [DROP ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-assembly-transact-sql.md)  
  [sys.assemblies](../../relational-databases/system-catalog-views/sys-assemblies-transact-sql.md)  
  [sys.dm_clr_loaded_assemblies](../../relational-databases/system-dynamic-management-views/sys-dm-clr-loaded-assemblies-transact-sql.md)  

