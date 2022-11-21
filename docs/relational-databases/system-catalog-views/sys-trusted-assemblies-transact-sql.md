---
title: "sys.trusted_assemblies (Transact-SQL)"
description: sys.trusted_assemblies (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "trusted_assemblies_TSQL"
  - "trusted_assemblies"
  - "sys.trusted_assemblies_TSQL"
  - "sys.trusted_assemblies"
helpviewer_keywords:
  - "sys.trusted_assemblies"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.trusted_assemblies (Transact-SQL)  
[!INCLUDE[SQL Server 2017](../../includes/applies-to-version/sqlserver2017.md)]

Contains a row for each trusted assembly for the server.

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  


|Column name |Data type |Description |
|--- |--- |--- |
|hash |varbinary(8000) |SHA2_512 hash of the assembly content. |
|description |nvarchar(4000) |Optional user-defined description of the assembly. Microsoft recommends using the canonical name that encodes the simple name, version number, culture, public key, and architecture of the assembly to trust. This value uniquely identifies the assembly on the common language runtime (CLR) side and is the same as the clr_name value in sys.assemblies. |
|create_date |datetime2 |Date the assembly was added to the list of trusted assemblies. |
|created_by |nvarchar(128) |Login name of the principal who added the assembly to the list. |

### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
 
## Remarks  
Use **[sys.sp_add_trusted_assembly](../../relational-databases/system-stored-procedures/sys-sp-add-trusted-assembly-transact-sql.md)** to add and **[sys.sp_drop_trusted_assembly](../../relational-databases/system-stored-procedures/sys-sp-drop-trusted-assembly-transact-sql.md)** to remove assemblies from `sys.trusted_assemblies`.

## See Also  
  [sys.sp_add_trusted_assembly](../../relational-databases/system-stored-procedures/sys-sp-add-trusted-assembly-transact-sql.md)  
  [sys.sp_drop_trusted_assembly](../../relational-databases/system-stored-procedures/sys-sp-drop-trusted-assembly-transact-sql.md)  
  [DROP ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-assembly-transact-sql.md)  
  [sys.assemblies](../../relational-databases/system-catalog-views/sys-assemblies-transact-sql.md)  
  [sys.dm_clr_loaded_assemblies](../../relational-databases/system-dynamic-management-views/sys-dm-clr-loaded-assemblies-transact-sql.md)  
