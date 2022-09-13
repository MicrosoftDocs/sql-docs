---
title: Limitations for Stretch Database
description: Limitations for Stretch Database
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql-server-stretch-database
ms.topic: conceptual
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "Stretch Database, limitations"
  - "Stretch Database, blocking issues"
  - "limitations (Stretch Database)"
  - "blocking issues (Stretch Database)"
---
# Limitations for Stretch Database

[!INCLUDE [sqlserver2016-windows-only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

Learn about limitations for Stretch-enabled tables, and about limitations that currently prevent you from enabling Stretch for a table.

## Limitations for Stretch-enabled tables

Stretch-enabled tables have the following limitations.

### Constraints

- Uniqueness isn't enforced for UNIQUE constraints and PRIMARY KEY constraints in the Azure table that contains the migrated data.

### DML operations

- You can't UPDATE or DELETE rows that have been migrated, or rows that are eligible for migration, in a Stretch-enabled table or in a view that includes Stretch-enabled tables.

- You can't INSERT rows into a Stretch-enabled table on a linked server.

### Indexes

- You can't create an index for a view that includes Stretch-enabled tables.

- Filters on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] indexes aren't propagated to the remote table.

## Limitations that currently prevent you from enabling Stretch for a table

The following items currently prevent you from enabling Stretch for a table.

### Table properties

- Tables that have more than 1,023 columns or more than 998 indexes

- FileTables or tables that contain FILESTREAM data

- Tables that are replicated, or that are actively using Change Tracking or Change Data Capture

- Memory-optimized tables

### Data types

- **text**, **ntext** and **image**
- **timestamp**
- **sql_variant**
- **xml**
- CLR data types including **geometry**, **geography**, **hierarchyid**, and CLR user-defined types

### Column types

- COLUMN_SET

- Computed columns

### Constraints

- Default constraints and check constraints

- Foreign key constraints that reference the table. In a parent-child relationship (for example, Order and Order_Detail), you can enable Stretch for the child table (Order_Detail) but not for the parent table (Order).

### Indexes

- Full text indexes

- XML indexes

- Spatial indexes

- Indexed views that reference the table

## See also

- [Enable Stretch Database for a database](enable-stretch-database-for-a-database.md)
- [Enable Stretch Database for a table](enable-stretch-database-for-a-table.md)
