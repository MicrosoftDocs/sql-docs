---
title: "Unique constraints and check constraints"
description: UNIQUE constraints and CHECK constraints are two types of constraints that can be used to enforce data integrity.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 12/29/2023
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "constraints [SQL Server], Visual Database Tools"
  - "Visual Database Tools [SQL Server], constraints"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Unique constraints and check constraints

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

`UNIQUE` constraints and `CHECK` constraints are two types of constraints that can be used to enforce data integrity in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tables. These are important database objects.

This article contains the following sections.

- [UNIQUE constraints](#unique-constraints)
- [CHECK constraints](#Check)
- [Related tasks](#related-tasks)

## UNIQUE constraints

Constraints are rules that the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] enforces for you. For example, you can use `UNIQUE` constraints to make sure that no duplicate values are entered in specific columns that don't participate in a primary key. Although both a `UNIQUE` constraint and a `PRIMARY KEY` constraint enforce uniqueness, use a `UNIQUE` constraint instead of a `PRIMARY KEY` constraint when you want to enforce the uniqueness of a column (or combination of columns) that isn't the primary key.

Unlike `PRIMARY KEY` constraints, `UNIQUE` constraints allow for the value `NULL`. However, as with any value participating in a `UNIQUE` constraint, only one null value is allowed per column. A `UNIQUE` constraint can be referenced by a `FOREIGN KEY` constraint.

When a `UNIQUE` constraint is added to an existing column or columns in the table, by default, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] examines the existing data in the columns to make sure all values are unique. If a `UNIQUE` constraint is added to a column that has duplicate values, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] returns an error and doesn't add the constraint.

The [!INCLUDE [ssDE](../../includes/ssde-md.md)] automatically creates a `UNIQUE` index to enforce the uniqueness requirement of the `UNIQUE` constraint. Therefore, if an attempt to insert a duplicate row is made, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] returns an error message that states the `UNIQUE` constraint was violated, and doesn't add the row to the table. Unless a clustered index is explicitly specified, a unique, nonclustered index is created by default to enforce the `UNIQUE` constraint.

## <a id="Check"></a> CHECK constraints

`CHECK` constraints enforce domain integrity by limiting the values that are accepted by one or more columns. You can create a `CHECK` constraint with any logical (Boolean) expression that returns `TRUE` or `FALSE` based on the logical operators. For example, the range of values for a `salary` column can be limited by creating a `CHECK` constraint that allows for only data that ranges from $15,000 through $100,000. This prevents salaries from being entered beyond the regular salary range. The logical expression would be the following: `salary >= 15000 AND salary <= 100000`.

You can apply multiple `CHECK` constraints to a single column. You can also apply a single `CHECK` constraint to multiple columns by creating it at the table level. For example, a multiple-column `CHECK` constraint could be used to confirm that any row with a `country_region` column value of `USA` also has a two-character value in the `state` column. This allows for multiple conditions to be checked in one location.

`CHECK` constraints are similar to `FOREIGN KEY` constraints in that they control the values that are put in a column. The difference is in how they determine which values are valid: `FOREIGN KEY` constraints obtain the list of valid values from another table, while `CHECK` constraints determine the valid values from a logical expression.

> [!CAUTION]  
> Constraints that include implicit or explicit data type conversion might cause certain operations to fail. For example, such constraints defined on tables that are sources of partition switching might cause an `ALTER TABLE...SWITCH` operation to fail. Avoid data type conversion in constraint definitions.

### Limitations of CHECK constraints

`CHECK` constraints reject values that evaluate to `FALSE`. Because null values evaluate to UNKNOWN, their presence in expressions might override a constraint. For example, suppose you place a constraint on an **int** column `MyColumn` specifying that `MyColumn` can contain only the value 10 (`MyColumn=10`). If you insert the value `NULL` into `MyColumn`, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] inserts `NULL` and doesn't return an error.

A `CHECK` constraint returns `TRUE` when the condition it is checking isn't `FALSE` for any row in the table. A `CHECK` constraint works at the row level. If a table that was created doesn't have any rows, any `CHECK` constraint on this table is considered valid. This situation can produce unexpected results, as in the following example.

```sql
CREATE TABLE CheckTbl (col1 INT, col2 INT);
GO

CREATE FUNCTION CheckFnctn()
RETURNS INT
AS
BEGIN
    DECLARE @retval INT;
    SELECT @retval = COUNT(*)
    FROM CheckTbl;

    RETURN @retval;
END;
GO

ALTER TABLE CheckTbl ADD CONSTRAINT chkRowCount CHECK (dbo.CheckFnctn() >= 1);
GO
```

The `CHECK` constraint being added specifies that there must be at least one row in table `CheckTbl`. However, because there are no rows in the table against which to check the condition of this constraint, the `ALTER TABLE` statement succeeds.

`CHECK` constraints aren't validated during `DELETE` statements. Therefore, executing `DELETE` statements on tables with certain types of check constraints might produce unexpected results. For example, consider the following statements executed on table `CheckTbl`.

```sql
INSERT INTO CheckTbl VALUES (10, 10);
GO
DELETE CheckTbl WHERE col1 = 10;
```

The `DELETE` statement succeeds, even though the `CHECK` constraint specifies that table `CheckTbl` must have at least `1` row.

> [!NOTE]  
> If the table is published for replication, you must make schema changes using the Transact-SQL statement [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or SQL Server Management Objects (SMO). When schema changes are made using the Table Designer or the Database Diagram Designer, it attempts to drop and recreate the table. You can't drop published objects, therefore the schema change will fail.

## Related tasks

| Task | Article |
| --- | --- |
| Describes how to create a unique constraint. | [Create Unique constraints](create-unique-constraints.md) |
| Describes how to modify a unique constraint. | [Modify Unique Constraints](modify-unique-constraints.md) |
| Describes how to delete a unique constraint. | [Delete Unique Constraints](delete-unique-constraints.md) |
| Describes how to create a check constraint. | [Create Check Constraints](create-check-constraints.md) |
| Describes how to disable a check constraint when a replication agent inserts or updates data in your table. | [Disable Check Constraints for Replication](disable-check-constraints-for-replication.md) |
| Describes how to disable a check constraint when data is added to, updated in, or deleted from a table. | [Disable Check Constraints with INSERT and UPDATE Statements](disable-check-constraints-with-insert-and-update-statements.md) |
| Describes how to change the constraint expression or the options that enable or disable the constraint for specific conditions. | [Modify Check Constraints](modify-check-constraints.md) |
| Describes how to delete a check constraint. | [Delete Check Constraints](delete-check-constraints.md) |
| Describes how to view the properties of a check constraint. | [Unique Constraints and Check Constraints](unique-constraints-and-check-constraints.md) |
