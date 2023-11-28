---
title: Create and Update Tables
description: "Create and update database tables"
author: markingmyname
ms.author: maghan
ms.date: 11/28/2023
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "Visual Database Tools [SQL Server], Table Designer"
  - "Table Designer, designing tables"
  - "opening tables"
  - "opening Table Designer"
  - "tables [SQL Server], opening"
  - "Table Designer, opening"
---

# Create and update database tables

[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]

The Table Designer is a visual tool where you design and visualize [database tables](../../relational-databases/tables/tables.md). Use the Table Designer in [SQL Server Management Studio (SSMS)](../download-sql-server-management-studio-ssms.md) or [Azure Data Studio (ADS)](/azure-data-studio/download-azure-data-studio) to create, edit, or delete tables, columns, keys, indexes, relationships, and constraints.  

## Create a table

1. Right-click the **Tables** node in your database and select  **New** > **Table** in SSMS, or **New Table** in ADS:
1. Add [columns](column-properties-visual-database-tools.md) to your table, specifying name, data type, and whether NULL values are allowed for each column.
1. Additional table properties can be configured in Azure Data Studio including keys, constraints, and indexes.  See [https://learn.microsoft.com/azure-data-studio/overview-of-the-table-designer-in-azure-data-studio](/azure-data-studio/overview-of-the-table-designer-in-azure-data-studio) for more information.
1. In SSMS, close the designer and specify a table name to save your changes.  In Azure Data Studio, ensure the table name has been updated in the **Table name** field, then select the **Publish Changes** icon or close the designer and save the changes to create the table.

## Update a table

1. Right-click the table under the **Tables** node of your database and select **Design**
1. Update the desired table settings.
3. Close the designer and save your changes, or use the **Publish Changes** icon in Azure Data Studio.

## Permissions

In order to create table, you must have ```CREATE TABLE``` permissions in the database and ```ALTER``` permission on the schema in which the table is being created.  See [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md) for more information.

To alter a table, you must have ```ALTER``` permissions on the table.  See [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md) for more information.

## See Also

- [Tables](../../relational-databases/tables/tables.md)
- [Table Properties &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/table-properties-visual-database-tools.md)
- [Column Properties](column-properties-visual-database-tools.md)
- [Add Columns to a Table](../../relational-databases/tables/add-columns-to-a-table-database-engine.md)
- [Primary and Foreign Keys](../../relational-databases/tables/primary-and-foreign-key-constraints.md)
- [Indexes](../../relational-databases/indexes/indexes.md)
- [Data types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
- [Create a database and add tables in Visual Studio](/visualstudio/data-tools/create-a-sql-database-by-using-a-designer)
