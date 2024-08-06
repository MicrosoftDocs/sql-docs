---
title: "Get information about assemblies"
description: Learn how to get information about assemblies using catalog views and functions.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/02/2024
ms.service: sql
ms.topic: "reference"
helpviewer_keywords:
  - "assemblies [CLR integration], metadata"
  - "status information [SQL Server], assemblies"
  - "metadata [SQL Server], assemblies"
---
# Get information about assemblies

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The following catalog views and functions can be queried for metadata about Common language runtime (CLR) assemblies.

| Information | Article |
| --- | --- |
| Individual assemblies | [ASSEMBLYPROPERTY](../../t-sql/functions/assemblyproperty-transact-sql.md) |
| All assemblies in the database | [sys.assemblies](../system-catalog-views/sys-assemblies-transact-sql.md) |
| Assembly files, including assembly binaries, source files, and debug files | [sys.assembly_files](../system-catalog-views/sys-assembly-files-transact-sql.md) |
| Cross-assembly references | [sys.assembly_references](../system-catalog-views/sys-assembly-references-transact-sql.md) |
| User-defined types | [sys.assembly_types](../system-catalog-views/sys-assembly-types-transact-sql.md)<br />[sys.types](../system-catalog-views/sys-types-transact-sql.md) |
| CLR stored procedures, triggers, and functions | [sys.assembly_modules](../system-catalog-views/sys-assembly-modules-transact-sql.md) |
| To get information about non-CLR objects | [sys.sql_modules](../system-catalog-views/sys-sql-modules-transact-sql.md) |

## Related content

- [Assemblies (Database Engine)](assemblies-database-engine.md)
- [Designing assemblies](assemblies-designing.md)
- [Implementing assemblies](assemblies-implementing.md)
