---
title: "Modify views"
description: "After you define a view, you can modify its definition in the Database Engine without dropping and re-creating the view."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 05/10/2023
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "views [SQL Server], renaming"
  - "views [SQL Server], modifying"
  - "modifying views"
  - "renaming views"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Modify views

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

After you define a view, you can modify its definition in the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] without dropping and re-creating the view by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

## Limitations and restrictions

- Modifying a view doesn't affect any dependent objects, such as stored procedures or triggers, unless the definition of the view changes in such a way that the dependent object is no longer valid.

- If a view currently used is modified by using ALTER VIEW, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] takes an exclusive schema lock on the view. When the lock is granted, and there are no active users of the view, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] deletes all copies of the view from the procedure cache. Existing plans referencing the view remain in the cache but are recompiled when invoked.

- ALTER VIEW can be applied to indexed views; however, ALTER VIEW unconditionally drops all indexes on the view.

## Permissions

To execute ALTER VIEW, at a minimum, ALTER permission on OBJECT is required.

## Use SQL Server Management Studio

1. In **Object Explorer**, select the plus sign next to the database where your view is located and then select the plus sign next to the **Views** folder.

1. Right-click on the view you wish to modify and select **Design**.

1. In the diagram pane of the query designer, make changes to the view in one or more of the following ways:

   1. Select or clear the check boxes of any elements you wish to add or remove.

   1. Right-click within the diagram pane, select **Add Table...**, and then select the additional columns you want to add to the view from the **Add Table** dialog box.

   1. Right-click the title bar of the table you wish to remove and select **Remove**.

1. On the **File** menu, select **Save _view name_**.

## <a id="TsqlProcedure"></a> Use Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. The example first creates a view and then modifies the view by using ALTER VIEW. A WHERE clause is added to the view definition.

   ```sql
   USE AdventureWorks2022;
   GO
   
   -- Create a view
   CREATE VIEW HumanResources.EmployeeHireDate
   AS
   SELECT p.FirstName,
        p.LastName,
        e.HireDate
   FROM HumanResources.Employee AS e
   INNER JOIN Person.Person AS p
        ON e.BusinessEntityID = p.BusinessEntityID;
   
   -- Modify the view by adding a WHERE clause to limit the rows returned
   ALTER VIEW HumanResources.EmployeeHireDate
   AS
   SELECT p.FirstName,
        p.LastName,
        e.HireDate
   FROM HumanResources.Employee AS e
   INNER JOIN Person.Person AS p
        ON e.BusinessEntityID = p.BusinessEntityID
   WHERE HireDate < CONVERT(DATETIME, '20020101', 101);
   GO
   ```

## Next steps

- [ALTER VIEW (Transact-SQL)](../../t-sql/statements/alter-view-transact-sql.md)
