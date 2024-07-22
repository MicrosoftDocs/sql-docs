---
title: Create tables (Database Engine)
description: Create a new table, name it, and add it to an existing database using the Database Engine.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/19/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "table creation [SQL Server], Visual Database Tools"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create tables (Database Engine)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

You can create a new table, name it, and add it to an existing database, by using the table designer in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS), or [!INCLUDE [tsql](../../includes/tsql-md.md)].

## Permissions

This task requires `CREATE TABLE` permission in the database, and ALTER permission on the schema in which the table is being created.

If any columns in the `CREATE TABLE` statement are defined as a CLR user-defined type, either ownership of the type, or REFERENCES permission on it is required.

If any columns in the `CREATE TABLE` statement have an XML schema collection associated with them, either ownership of the XML schema collection or REFERENCES permission on it is required.

## Use table designer in SQL Server Management Studio

1. In SSMS, in **Object Explorer**, connect to the instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)] that contains the database to be modified.

1. In **Object Explorer**, expand the **Databases** node and then expand the database that will contain the new table.

1. In Object Explorer, right-click the **Tables** node of your database and then select **New Table**.

1. Type column names, choose data types, and choose whether to allow nulls for each column as shown in the following illustration:

   :::image type="content" source="media/create-tables-database-engine/add-columns-in-table-designer.gif" alt-text="Screenshot showing the Allow Nulls option selected for the ModifiedDate column.":::

1. To specify more properties for a column, such as identity or computed column values, select the column and in the column properties tab, choose the appropriate properties. For more information about column properties, see [Table Column Properties (SQL Server Management Studio)](../../relational-databases/tables/table-column-properties-sql-server-management-studio.md).

1. To specify a column as a primary key, right-click the column and select **Set Primary Key**. For more information, see [Create Primary Keys](../../relational-databases/tables/create-primary-keys.md).

1. To create foreign key relationships, check constraints, or indexes, right-click in the Table Designer pane and select an object from the list as shown in the following illustration:

   :::image type="content" source="media/create-tables-database-engine/add-table-objects.gif" alt-text="Screenshot showing the Relationships option.":::

   For more information about these objects, see [Create Foreign Key Relationships](../../relational-databases/tables/create-foreign-key-relationships.md), [Create Check Constraints](../../relational-databases/tables/create-check-constraints.md) and [Indexes](../../relational-databases/indexes/indexes.md).

1. By default, the table is contained in the `dbo` schema. To specify a different schema for the table, right-click in the **Table Designer** pane and select **Properties** as shown in the following illustration. From the **Schema** dropdown list, select the appropriate schema.

   :::image type="content" source="media/create-tables-database-engine/specify-table-schema.gif" alt-text="Screenshot of the Properties pane showing the Schema option.":::

   For more information about schemas, see [Create a Database Schema](../../relational-databases/security/authentication-access/create-a-database-schema.md).

1. From the **File** menu, choose **Save** *table name*.

1. In the **Choose Name** dialog box, type a name for the table and select **OK**.

1. To view the new table, in **Object Explorer**, expand the **Tables** node and press **F5** to refresh the list of objects. The new table is displayed in the list of tables.

## Use Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   ```sql
   CREATE TABLE dbo.PurchaseOrderDetail (
       PurchaseOrderID INT NOT NULL,
       LineNumber SMALLINT NOT NULL,
       ProductID INT NULL,
       UnitPrice MONEY NULL,
       OrderQty SMALLINT NULL,
       ReceivedQty FLOAT NULL,
       RejectedQty FLOAT NULL,
       DueDate DATETIME NULL
   );
   ```

## Next step

> [!div class="nextstepaction"]
> [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)
