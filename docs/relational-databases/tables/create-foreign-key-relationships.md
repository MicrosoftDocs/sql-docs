---
title: "Create Foreign Key Relationships"
description: "Create Foreign Key Relationships"
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "relationships [SQL Server], creating"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto, randolphwest
ms.custom: FY22Q2Fresh
ms.date: 07/25/2022
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create Foreign Key Relationships

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

This article describes how to create foreign key relationships in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. You create a relationship between two tables when you want to associate rows of one table with rows of another.

## Permissions

Creating a new table with a foreign key requires [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) permission in the database, and [ALTER](../../t-sql/statements/alter-schema-transact-sql.md) permission on the schema in which the table is being created.

Creating a foreign key in an existing table requires [ALTER](../../t-sql/statements/alter-table-transact-sql.md) permission on the table.

## <a name="BeforeYouBegin"></a> Limits and restrictions

- A foreign key constraint doesn't have to be linked only to a primary key constraint in another table. Foreign keys can also be defined to reference the columns of a UNIQUE constraint in another table.
- When a value other than NULL is entered into the column of a FOREIGN KEY constraint, the value must exist in the referenced column. Otherwise, a foreign key violation error message is returned. To make sure that all values of a composite foreign key constraint are verified, specify NOT NULL on all the participating columns.
- FOREIGN KEY constraints can reference only tables within the same database on the same server. Cross-database referential integrity must be implemented through triggers. For more information, see [CREATE TRIGGER](../../t-sql/statements/create-trigger-transact-sql.md).
- FOREIGN KEY constraints can reference another column in the same table, and is referred to as a self-reference.
- A FOREIGN KEY constraint specified at the column level can list only one reference column. This column must have the same data type as the column on which the constraint is defined.
- A FOREIGN KEY constraint specified at the table level must have the same number of reference columns as the number of columns in the constraint column list. The data type of each reference column must also be the same as the corresponding column in the column list.
- The [!INCLUDE[ssDE](../../includes/ssde-md.md)] doesn't have a predefined limit on the number of FOREIGN KEY constraints a table can contain that reference other tables. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] also doesn't limit the number of FOREIGN KEY constraints owned by other tables that reference a specific table. However, the actual number of FOREIGN KEY constraints used is limited by the hardware configuration, and by the design of the database and application. A table can reference a maximum of 253 other tables and columns as foreign keys (outgoing references). [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later increases the limit for the number of other tables and columns that can reference columns in a single table (incoming references), from 253 to 10,000. (Requires at least 130 compatibility level.) The increase has the following restrictions:

  - Greater than 253 foreign key references are supported for DELETE and UPDATE DML operations. MERGE operations aren't supported.
  - A table with a foreign key reference to itself is still limited to 253 foreign key references.
  - Greater than 253 foreign key references aren't currently available for columnstore indexes, memory-optimized tables, or Stretch Database.

  > [!IMPORTANT]  
  > Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

- FOREIGN KEY constraints aren't enforced on temporary tables.
- If a foreign key is defined on a CLR user-defined type column, the implementation of the type must support binary ordering. For more information, see [CLR User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md).
- A column of type **varchar(max)** can participate in a FOREIGN KEY constraint only if the primary key it references is also defined as type **varchar(max)**.

## Create a foreign key relationship in Table Designer

### Use SQL Server Management Studio

1. In Object Explorer, right-click the table that will be on the foreign-key side of the relationship and select **Design**.

   The table opens in [**Table Designer**](../../ssms/visual-db-tools/design-tables-visual-database-tools.md).
2. From the **Table Designer** menu, select **Relationships**. (See the **Table Designer** menu in the header, or, right-click in the empty space of the table definition, then select **Relationships...**.)
3. In the **Foreign-key Relationships** dialog box, select **Add**.

   The relationship appears in the **Selected Relationship** list with a system-provided name in the format FK_\<*tablename*>_\<*tablename*>, where the first *tablename* is the name of the foreign key table, and the second *tablename* is the name of the primary key table. This is simply a default and common naming convention for the **(Name)** field of the foreign key object.
     
4. Select the relationship in the **Selected Relationship** list.
5. Select **Tables and Columns Specification** in the grid to the right and select the ellipses (**...**) to the right of the property.
6. In the **Tables and Columns** dialog box, in the **Primary Key** drop-down list, choose the table that will be on the primary-key side of the relationship.
7. In the grid beneath, choose the columns contributing to the table's primary key. In the adjacent grid cell to the right of each column, choose the corresponding foreign-key column of the foreign-key table.

   **Table Designer** suggests a name for the relationship. To change this name, edit the contents of the **Relationship Name** text box.
8. Choose **OK** to create the relationship.
9. Close the table designer window and **Save** your changes for the foreign key relationship change to take effect.

## Create a foreign key in a new table

### Use Transact-SQL

The following example creates a table and defines a foreign key constraint on the column `TempID` that references the column `SalesReasonID` in the `Sales.SalesReason` table in the `AdventureWorks` database. The ON DELETE CASCADE and ON UPDATE CASCADE clauses are used to ensure that changes made to `Sales.SalesReason` table are automatically propagated to the `Sales.TempSalesReason` table.    

```sql
CREATE TABLE Sales.TempSalesReason 
   (
      TempID int NOT NULL, Name nvarchar(50)
      , CONSTRAINT PK_TempSales PRIMARY KEY NONCLUSTERED (TempID)
      , CONSTRAINT FK_TempSales_SalesReason FOREIGN KEY (TempID)
        REFERENCES Sales.SalesReason (SalesReasonID)
        ON DELETE CASCADE
        ON UPDATE CASCADE
   )
;
```

## Create a foreign key in an existing table

### Use Transact-SQL
The following example creates a foreign key on the column `TempID` and references the column `SalesReasonID` in the `Sales.SalesReason` table in the `AdventureWorks` database.

```sql
ALTER TABLE Sales.TempSalesReason
   ADD CONSTRAINT FK_TempSales_SalesReason FOREIGN KEY (TempID)
      REFERENCES Sales.SalesReason (SalesReasonID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;
```

## Next steps

- [Primary and Foreign Key Constraints](primary-and-foreign-key-constraints.md)
- [GRANT Database Permissions](../../t-sql/statements/grant-database-permissions-transact-sql.md)
- [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md)
- [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md)
- [ALTER TABLE table_constraint](../../t-sql/statements/alter-table-table-constraint-transact-sql.md).
