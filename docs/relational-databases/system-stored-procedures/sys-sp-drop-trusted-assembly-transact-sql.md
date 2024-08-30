---
title: "sys.sp_drop_trusted_assembly (Transact-SQL)"
description: Drops an assembly from the list of trusted assemblies on the server.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_drop_trusted_assembly_TSQL"
  - "sp_drop_trusted_assembly"
  - "sys.sp_drop_trusted_assembly_TSQL"
  - "sys.sp_drop_trusted_assembly"
helpviewer_keywords:
  - "sys.sp_drop_trusted_assembly"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sys.sp_drop_trusted_assembly (Transact-SQL)

[!INCLUDE [sqlserver2017-asdbmi](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

Drops an assembly from the list of trusted assemblies on the server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_drop_trusted_assembly
    [ @hash = ] 'value'
[ ; ]
```

## Arguments

#### [ @hash = ] '*value*'

The SHA2_512 hash value of the assembly to drop from the list of trusted assemblies for the server. Trusted assemblies might load when CLR strict security is enabled, even if the assembly is unsigned or the database isn't marked as trustworthy.

## Remarks

This procedure removes an assembly from [sys.trusted_assemblies](../system-catalog-views/sys-trusted-assemblies-transact-sql.md).

## Permissions

Requires membership in the **sysadmin** fixed server role or CONTROL SERVER permission.

## Examples

The following example drops an assembly hash from the list of trusted assemblies for the server.

```sql
EXEC sp_drop_trusted_assembly
    0x8893AD6D78D14EE43DF482E2EAD44123E3A0B684A8873C3F7BF3B5E8D8F09503F3E62370CE742BBC96FE3394477214B84C7C1B0F7A04DCC788FA99C2C09DFCCC;
```

## Related content

- [sys.sp_add_trusted_assembly (Transact-SQL)](sys-sp-add-trusted-assembly-transact-sql.md)
- [sys.trusted_assemblies (Transact-SQL)](../system-catalog-views/sys-trusted-assemblies-transact-sql.md)
- [DROP ASSEMBLY (Transact-SQL)](../../t-sql/statements/drop-assembly-transact-sql.md)
- [sys.assemblies (Transact-SQL)](../system-catalog-views/sys-assemblies-transact-sql.md)
- [sys.dm_clr_loaded_assemblies (Transact-SQL)](../system-dynamic-management-views/sys-dm-clr-loaded-assemblies-transact-sql.md)
