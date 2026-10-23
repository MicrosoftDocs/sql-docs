---
title: "Database Properties (General Page)"
description: Learn how to use the General tab in the Database Properties dialog box to view or modify the properties of a database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 10/20/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
f1_keywords:
  - "sql13.swb.databaseproperties.general.f1"
  - "sql13.swb.databaseproperties.segments.f1"
  - "sql13.swb.databaseproperties.databasecapabilities.f1"
---
# Database Properties (General page)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]

Use this page to view or modify properties for the selected database.

## Options

#### Last Database Backup

Displays the date that the database was last backed up.

#### Last Database Log Backup

Displays the date that the database transaction log was last backed up.

#### Name

Displays the name of the database.

#### Status

Displays the database state. For more information, see [Database States](database-states.md).

#### Owner

Displays the name of the database owner. The owner can be changed on the **Files** page.

#### Date Created

Displays the date and time that the database was created.

#### Size

Displays the size of the database in megabytes.

#### Space Available

Displays the amount of available space in the database in megabytes.

#### Number of Users

Displays the number of users configured for the database.

#### Collation Name

Displays the collation used for the database. The collation can be changed on the **Options** page.

## Related content

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [sys.databases (Transact-SQL)](../system-catalog-views/sys-databases-transact-sql.md)
- [DATABASEPROPERTYEX (Transact-SQL)](../../t-sql/functions/databasepropertyex-transact-sql.md)
