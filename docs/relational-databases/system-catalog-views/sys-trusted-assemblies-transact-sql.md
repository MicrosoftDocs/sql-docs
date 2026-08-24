---
title: "sys.trusted_assemblies (Transact-SQL)"
description: sys.trusted_assemblies contains a row for each trusted assembly for the server.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 11/05/2024
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
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sys.trusted_assemblies (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Contains a row for each trusted assembly for the server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

| Column name | Data type | Description |
| --- | --- | --- |
| `hash` | **varbinary(8000)** | SHA2_512 hash of the assembly content. |
| `description` | **nvarchar(4000)** | Optional user-defined description of the assembly. We recommend using the canonical name that encodes the simple name, version number, culture, public key, and architecture of the assembly to trust. This value uniquely identifies the assembly on the common language runtime (CLR) side and is the same as the `clr_name` value in `sys.assemblies.` |
| `create_date` | **datetime2(7)** | Date the assembly was added to the list of trusted assemblies. |
| `created_by` | **nvarchar(128)** | Login name of the principal who added the assembly to the list. |

### Permissions

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions require `VIEW SERVER STATE` permission on the server.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER SECURITY STATE` permission on the server.

## Remarks

Use [sys.sp_add_trusted_assembly](../system-stored-procedures/sys-sp-add-trusted-assembly-transact-sql.md) to add, and [sys.sp_drop_trusted_assembly](../system-stored-procedures/sys-sp-drop-trusted-assembly-transact-sql.md) to remove assemblies from `sys.trusted_assemblies`.

## Related content

- [sys.sp_add_trusted_assembly (Transact-SQL)](../system-stored-procedures/sys-sp-add-trusted-assembly-transact-sql.md)
- [sys.sp_drop_trusted_assembly (Transact-SQL)](../system-stored-procedures/sys-sp-drop-trusted-assembly-transact-sql.md)
- [DROP ASSEMBLY (Transact-SQL)](../../t-sql/statements/drop-assembly-transact-sql.md)
- [sys.assemblies (Transact-SQL)](sys-assemblies-transact-sql.md)
- [sys.dm_clr_loaded_assemblies (Transact-SQL)](../system-dynamic-management-objects/sys-dm-clr-loaded-assemblies-transact-sql.md)
