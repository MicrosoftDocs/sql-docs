---
title: "sys.extended_procedures (Transact-SQL)"
description: sys.extended_procedures contains a row for each object that is an extended stored procedure.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/12/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "extended_procedures"
  - "sys.extended_procedures"
  - "sys.extended_procedures_TSQL"
  - "extended_procedures_TSQL"
helpviewer_keywords:
  - "sys.extended_procedures catalog view"
dev_langs:
  - "TSQL"
---
# sys.extended_procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Contains a row for each object that is an extended stored procedure, with `sys.all_objects.type` = `X`. Because extended stored procedures are installed into the `master` database, they're only visible from that database context. Selecting from the `sys.extended_procedures` view in any other database context returns an empty result set.

| Column name | Data type | Description |
| --- | --- | --- |
| Columns inherited from `sys.objects` | | For a list of columns that this view inherits, see [sys.objects](sys-objects-transact-sql.md). |
| `dll_name` | **nvarchar(260)** | Name, including path, of the DLL for this extended stored procedure. |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Related content

- [Object Catalog Views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
