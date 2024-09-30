---
title: Move database files
description: Learn how to move system and user databases by specifying the new file location in the FILENAME clause of the ALTER DATABASE statement.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/19/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "disaster recovery [SQL Server], moving database files"
  - "files [SQL Server], moving"
  - "data files [SQL Server], moving"
  - "file moving [SQL Server]"
  - "moving full-text catalogs"
  - "moving databases"
  - "full-text catalogs [SQL Server], moving"
  - "moving database files"
  - "scheduled disk maintenance [SQL Server]"
  - "moving files"
  - "relocating database files"
  - "planned database relocations [SQL Server]"
  - "databases [SQL Server], moving"
---
# Move database files

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can move *system* and *user* databases by specifying the new file location in the `FILENAME` clause of the [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement. Data, log, and full-text catalog files can be moved in this way. This option might be useful in the following situations:

- Failure recovery. For example, the database is in suspect mode, or is shut down, because of a hardware failure.

- Planned relocation.

- Relocation for scheduled disk maintenance.

## In this section

| Article | Description |
| --- | --- |
| [Move user databases](move-user-databases.md) | Describes the procedures for moving user database files and full-text catalog files to a new location. |
| [Move system databases](move-system-databases.md) | Describes the procedures for moving system database files to a new location. |

## Related content

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [Database detach and attach (SQL Server)](database-detach-and-attach-sql-server.md)
