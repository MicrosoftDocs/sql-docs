---
title: "Modify or Rename DML Triggers"
description: Learn how to modify or rename a DML trigger in the Database Engine using SQL Server Management Studio or Transact-SQL.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 08/08/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "renaming triggers"
  - "modifying triggers"
  - "DML triggers, modifying"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Modify or Rename DML Triggers

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to modify or rename a DML trigger in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

## Limitations

When you rename a trigger, the trigger must be in the current database, and the new name must follow the rules for [identifiers](../databases/database-identifiers.md).

## Recommendations

Avoid using the [sp_rename](../system-stored-procedures/sp-rename-transact-sql.md) stored procedure to rename a trigger. Changing any part of an object name can break scripts and stored procedures. Renaming a trigger doesn't change the name of the corresponding object name in the definition column of the [sys.sql_modules](../system-catalog-views/sys-sql-modules-transact-sql.md) catalog view. We recommend that you drop and re-create the trigger instead.

If you change the name of an object referenced by a DML trigger, you must modify the trigger so that its text reflects the new name. Therefore, before you rename an object, display the dependencies of the object first to determine whether the proposed change affects any triggers.

A DML trigger can also be modified to encrypt its definition.

To view the dependencies of a trigger, you can use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or the following function and catalog views:

- [sys.sql_expression_dependencies](../system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)
- [sys.dm_sql_referenced_entities](../system-dynamic-management-views/sys-dm-sql-referenced-entities-transact-sql.md)
- [sys.dm_sql_referencing_entities](../system-dynamic-management-views/sys-dm-sql-referencing-entities-transact-sql.md)

## Permissions

To alter a DML trigger requires `ALTER` permission on the table or view on which the trigger is defined.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

### Modify a DML trigger

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)] and then expand that instance.

1. Expand the database that you want, expand **Tables**, and then expand the table that contains the trigger that you want to modify.

1. Expand **Triggers**, right-click the trigger to modify, and then select **Modify**.

1. Modify the trigger, and then select **Execute**.

### Rename a DML trigger

1. [Delete or disable the DML trigger](delete-or-disable-dml-triggers.md) that you want to rename.
1. [Create the new DML trigger](create-dml-triggers.md), specifying the new name.

## <a id="TsqlProcedure"></a> Use Transact-SQL

### Modify a trigger using ALTER TRIGGER

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query. Execute the first example to create a DML trigger that prints a user-defined message to the client when a user tries to add or change data in the `SalesPersonQuotaHistory` table. Execute the [ALTER TRIGGER](../../t-sql/statements/alter-trigger-transact-sql.md) statement to modify the trigger to fire only on `INSERT` activities. This trigger is helpful because it reminds the user that updates or inserts rows into this table to also notify the `Compensation` department.

   1. Create trigger.

      ```sql
      USE AdventureWorks2022;
      GO
      
      IF OBJECT_ID(N'Sales.bonus_reminder', N'TR') IS NOT NULL
          DROP TRIGGER Sales.bonus_reminder;
      GO
      
      CREATE TRIGGER Sales.bonus_reminder
          ON Sales.SalesPersonQuotaHistory WITH ENCRYPTION
          AFTER INSERT, UPDATE
      AS RAISERROR ('Notify Compensation', 16, 10);
      GO
      ```

   1. Alter the trigger.

      ```sql
      USE AdventureWorks2022;
      GO
      ALTER TRIGGER Sales.bonus_reminder
      ON Sales.SalesPersonQuotaHistory
      AFTER INSERT
      AS RAISERROR ('Notify Compensation', 16, 10);
      GO
      ```

### Rename a trigger using DROP TRIGGER and ALTER TRIGGER

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example uses the [DROP TRIGGER](../../t-sql/statements/drop-trigger-transact-sql.md) and [CREATE TRIGGER](../../t-sql/statements/create-trigger-transact-sql.md) statements to rename the `Sales.bonus_reminder` trigger to `Sales.bonus_reminder_2`.

```sql
USE AdventureWorks2022;
GO

IF OBJECT_ID(N'Sales.bonus_reminder', N'TR') IS NOT NULL
    DROP TRIGGER Sales.bonus_reminder;
GO

CREATE TRIGGER Sales.bonus_reminder_2
    ON Sales.SalesPersonQuotaHistory WITH ENCRYPTION
    AFTER INSERT, UPDATE
AS RAISERROR ('Notify Compensation', 16, 10);
GO
```

## Related content

- [CREATE TRIGGER (Transact-SQL)](../../t-sql/statements/create-trigger-transact-sql.md)
- [DROP TRIGGER (Transact-SQL)](../../t-sql/statements/drop-trigger-transact-sql.md)
- [ENABLE TRIGGER (Transact-SQL)](../../t-sql/statements/enable-trigger-transact-sql.md)
- [DISABLE TRIGGER (Transact-SQL)](../../t-sql/statements/disable-trigger-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../../t-sql/functions/eventdata-transact-sql.md)
- [sp_rename (Transact-SQL)](../system-stored-procedures/sp-rename-transact-sql.md)
- [ALTER TRIGGER (Transact-SQL)](../../t-sql/statements/alter-trigger-transact-sql.md)
- [Get Information About DML Triggers](get-information-about-dml-triggers.md)
- [sp_help (Transact-SQL)](../system-stored-procedures/sp-help-transact-sql.md)
- [sp_helptrigger (Transact-SQL)](../system-stored-procedures/sp-helptrigger-transact-sql.md)
- [sys.triggers (Transact-SQL)](../system-catalog-views/sys-triggers-transact-sql.md)
- [sys.trigger_events (Transact-SQL)](../system-catalog-views/sys-trigger-events-transact-sql.md)
- [sys.sql_modules (Transact-SQL)](../system-catalog-views/sys-sql-modules-transact-sql.md)
- [sys.assembly_modules (Transact-SQL)](../system-catalog-views/sys-assembly-modules-transact-sql.md)
- [sys.server_triggers (Transact-SQL)](../system-catalog-views/sys-server-triggers-transact-sql.md)
- [sys.server_trigger_events (Transact-SQL)](../system-catalog-views/sys-server-trigger-events-transact-sql.md)
- [sys.server_sql_modules (Transact-SQL)](../system-catalog-views/sys-server-sql-modules-transact-sql.md)
- [sys.server_assembly_modules (Transact-SQL)](../system-catalog-views/sys-server-assembly-modules-transact-sql.md)
