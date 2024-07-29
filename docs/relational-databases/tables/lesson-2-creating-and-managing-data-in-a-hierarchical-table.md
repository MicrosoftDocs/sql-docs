---
title: "Lesson 2: Create and manage data in a hierarchical table"
description: "Lesson 2: Create and manage data in a hierarchical table."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/26/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "HierarchyID"
---
# Lesson 2: Create and manage data in a hierarchical table

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

In Lesson 1, you modified an existing table to use the **hierarchyid** data type, and populated the **hierarchyid** column with the representation of the existing data. In this lesson, you start with a new table, and insert data by using the hierarchical methods. Then, you query and manipulate the data by using the hierarchical methods.

## Prerequisites

To complete this tutorial, you need SQL Server Management Studio, access to a server that's running SQL Server, and an [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

- Install [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).
- Install [SQL Server 2022 Developer Edition](https://www.microsoft.com/sql-server/sql-server-downloads).
- Download [AdventureWorks sample database](../../samples/adventureworks-install-configure.md).

Instructions for restoring databases in SSMS are here: [Restore a Database Backup Using SSMS](../backup-restore/restore-a-database-backup-using-ssms.md).

## Create a table using the hierarchyid data type

The following example creates a table named `EmployeeOrg`, which includes employee data together with their reporting hierarchy. The example creates the table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database, but that is optional. To keep the example simple, this table includes only five columns:

- `OrgNode` is a **hierarchyid** column that stores the hierarchical relationship.
- `OrgLevel` is a computed column, based on the `OrgNode` column that stores each nodes level in the hierarchy. It is used for a breadth-first index.
- `EmployeeID` contains the typical employee identification number that is used for applications such as payroll. In new application development, applications can use the `OrgNode` column and this separate `EmployeeID` column isn't needed.
- `EmpName` contains the name of the employee.
- `Title` contains the title of the employee.

## Create the EmployeeOrg table

1. In a Query Editor window, run the following code to create the `EmployeeOrg` table. Specifying the `OrgNode` column as the primary key with a clustered index creates a depth-first index:

   ```sql
   USE AdventureWorks2022;
   GO
   
   IF OBJECT_ID('HumanResources.EmployeeOrg') IS NOT NULL
       DROP TABLE HumanResources.EmployeeOrg
   
   CREATE TABLE HumanResources.EmployeeOrg (
       OrgNode HIERARCHYID PRIMARY KEY CLUSTERED,
       OrgLevel AS OrgNode.GetLevel(),
       EmployeeID INT UNIQUE NOT NULL,
       EmpName VARCHAR(20) NOT NULL,
       Title VARCHAR(20) NULL
   );
   GO
   ```

1. Run the following code to create a composite index on the `OrgLevel` and `OrgNode` columns to support efficient breadth-first searches:

   ```sql
   CREATE UNIQUE INDEX EmployeeOrgNc1
   ON HumanResources.EmployeeOrg(OrgLevel, OrgNode);
   GO
   ```

The table is now ready for data. The next task will populate the table by using hierarchical methods.

## Populate a hierarchical table using hierarchical methods

[!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] has eight employees working in the Marketing department. The employee hierarchy looks like this:

`David`, `EmployeeID` 6, is the Marketing Manager. Three Marketing Specialists report to `David`:

- `Sariya`, `EmployeeID` 46
- `John`, `EmployeeID` 271
- `Jill`, `EmployeeID` 119

Marketing Assistant `Wanida` (`EmployeeID` 269), reports to `Sariya`, and Marketing Assistant `Mary` (`EmployeeID` 272), reports to `John`.

### Insert the root of the hierarchy tree

1. The following example inserts `David` the Marketing Manager into the table at the root of the hierarchy. The `OrdLevel` column is a computed column. Therefore, it's not part of the `INSERT` statement. This first record uses the [GetRoot (Database Engine)](../../t-sql/data-types/getroot-database-engine.md) method to populate this first record as the root of the hierarchy.

   ```sql
   INSERT HumanResources.EmployeeOrg (OrgNode, EmployeeID, EmpName, Title)
   VALUES (hierarchyid::GetRoot(), 6, 'David', 'Marketing Manager');
   GO
   ```

1. Execute the following code to examine initial row in the table:

   ```sql
   SELECT OrgNode.ToString() AS Text_OrgNode,
   OrgNode, OrgLevel, EmployeeID, EmpName, Title
   FROM HumanResources.EmployeeOrg;
   ```

   [!INCLUDE [ssResult](../../includes/ssresult-md.md)]

   ```output
   Text_OrgNode OrgNode OrgLevel EmployeeID EmpName Title
   ------------ ------- -------- ---------- ------- -----------------
   /            Ox      0        6          David   Marketing Manager
   ```

As in the previous lesson, we use the `ToString()` method to convert the **hierarchyid** data type to a format that is more easily understood.

### Insert a subordinate employee

1. `Sariya` reports to `David`. To insert `Sariya's` node, you must create an appropriate `OrgNode` value of data type **hierarchyid**. The following code creates a variable of data type **hierarchyid** and populates it with the root OrgNode value of the table. Then uses that variable with the [GetDescendant (Database Engine)](../../t-sql/data-types/getdescendant-database-engine.md) method to insert row that is a subordinate node. `GetDescendant` takes two arguments. Review the following options for the argument values:

   - If parent is `NULL`, `GetDescendant` returns `NULL`.
   - If parent isn't `NULL`, and both `child1` and `child2` are `NULL`, `GetDescendant` returns a child of parent.
   - If parent and `child1` aren't `NULL`, and `child2` is `NULL`, `GetDescendant` returns a child of parent greater than `child1`.
   - If parent and `child2` aren't `NULL` and `child1` is `NULL`, `GetDescendant` returns a child of parent less than `child2`.
   - If parent, `child1`, and `child2` are all not `NULL`, `GetDescendant` returns a child of parent greater than `child1` and less than `child2`.

   The following code uses the `(NULL, NULL)` arguments of the root parent because there aren't yet any rows in the table except the root. Execute the following code to insert `Sariya`:

   ```sql
   DECLARE @Manager HIERARCHYID
   SELECT @Manager = HIERARCHYID::GetRoot()
   FROM HumanResources.EmployeeOrg;

   INSERT HumanResources.EmployeeOrg (OrgNode, EmployeeID, EmpName, Title)
   VALUES
   (@Manager.GetDescendant(NULL, NULL), 46, 'Sariya', 'Marketing Specialist');
   ```

1. Repeat the query from the first procedure to query the table and see how the entries appear:

   ```sql
   SELECT OrgNode.ToString() AS Text_OrgNode,
   OrgNode, OrgLevel, EmployeeID, EmpName, Title
   FROM HumanResources.EmployeeOrg;
   ```

   [!INCLUDE [ssResult](../../includes/ssresult-md.md)]

   ```output
   Text_OrgNode OrgNode OrgLevel EmployeeID EmpName Title
   ------------ ------- -------- ---------- ------- -----------------
   /            Ox      0        6          David   Marketing Manager
   /1/          0x58    1        46         Sariya  Marketing Specialist
   ```

### Create a procedure for entering new nodes

1. To simplify entering data, create the following stored procedure to add employees to the `EmployeeOrg` table. The procedure accepts input values about the employee being added. This includes the `EmployeeID` of the new employee's manager, the new employee's `EmployeeID` number, and their first name and title. The procedure uses `GetDescendant()` and also the [GetAncestor (Database Engine)](../../t-sql/data-types/getancestor-database-engine.md) method. Execute the following code to create the procedure:

   ```sql
   CREATE PROCEDURE AddEmp (
       @mgrid INT,
       @empid INT,
       @e_name VARCHAR(20),
       @title VARCHAR(20)
   )
   AS
   BEGIN
       DECLARE @mOrgNode HIERARCHYID, @lc HIERARCHYID;
   
       SELECT @mOrgNode = OrgNode
       FROM HumanResources.EmployeeOrg
       WHERE EmployeeID = @mgrid;
   
       SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
   
       BEGIN TRANSACTION;
   
       SELECT @lc = max(OrgNode)
       FROM HumanResources.EmployeeOrg
       WHERE OrgNode.GetAncestor(1) = @mOrgNode;
   
       INSERT HumanResources.EmployeeOrg (OrgNode, EmployeeID, EmpName, Title)
       VALUES (@mOrgNode.GetDescendant(@lc, NULL), @empid, @e_name, @title);
   
       COMMIT;
   END;
   GO
   ```

1. The following example adds the remaining four employees that report directly or indirectly to `David`.

   ```sql
   EXEC AddEmp 6, 271, 'John', 'Marketing Specialist';
   EXEC AddEmp 6, 119, 'Jill', 'Marketing Specialist';
   EXEC AddEmp 46, 269, 'Wanida', 'Marketing Assistant';
   EXEC AddEmp 271, 272, 'Mary', 'Marketing Assistant';
   ```

1. Again, execute the following query examine the rows in the `EmployeeOrg` table:

   ```sql
   SELECT OrgNode.ToString() AS Text_OrgNode,
   OrgNode, OrgLevel, EmployeeID, EmpName, Title
   FROM HumanResources.EmployeeOrg;
   GO
   ```

   [!INCLUDE [ssResult](../../includes/ssresult-md.md)]

   ```output
   Text_OrgNode OrgNode OrgLevel EmployeeID EmpName Title
   ------------ ------- -------- ---------- ------- --------------------
   /            Ox      0        6          David   Marketing Manager
   /1/          0x58    1        46         Sariya  Marketing Specialist
   /1/1/        0x5AC0  2        269        Wanida  Marketing Assistant
   /2/          0x68    1        271        John    Marketing Specialist
   /2/1/        0x6AC0  2        272        Mary    Marketing Assistant
   /3/          0x78    1        119        Jill    Marketing Specialist
   ```

The table is now fully populated with the Marketing organization.

## Query a hierarchical table using hierarchy methods

Now that the HumanResources.EmployeeOrg table is fully populated, this task shows you how to query the hierarchy using some of the hierarchical methods.

### Find subordinate nodes

1. Sariya has one subordinate employee. To query for Sariya's subordinates, execute the following query that uses the [IsDescendantOf (Database Engine)](../../t-sql/data-types/isdescendantof-database-engine.md) method:

   ```sql
   DECLARE @CurrentEmployee HIERARCHYID

   SELECT @CurrentEmployee = OrgNode
   FROM HumanResources.EmployeeOrg
   WHERE EmployeeID = 46;

   SELECT *
   FROM HumanResources.EmployeeOrg
   WHERE OrgNode.IsDescendantOf(@CurrentEmployee ) = 1;
   ```

   The result lists both `Sariya` and `Wanida`. `Sariya` is listed because that value is the descendant at the `0` level. `Wanida` is the descendant at the `1` level.

1. You can also query for this information by using the [GetAncestor (Database Engine)](../../t-sql/data-types/getancestor-database-engine.md) method. `GetAncestor` takes an argument for the level that you're trying to return. Since Wanida is one level underneath Sariya, use `GetAncestor(1)` as demonstrated in the following code:

   ```sql
   DECLARE @CurrentEmployee HIERARCHYID

   SELECT @CurrentEmployee = OrgNode
   FROM HumanResources.EmployeeOrg
   WHERE EmployeeID = 46;

   SELECT OrgNode.ToString() AS Text_OrgNode, *
   FROM HumanResources.EmployeeOrg
   WHERE OrgNode.GetAncestor(1) = @CurrentEmployee
   ```

   This time the result lists only Wanida.

1. Now change the `@CurrentEmployee` to David (EmployeeID 6) and the level to 2. Execute the following to also return Wanida:

   ```sql
   DECLARE @CurrentEmployee HIERARCHYID

   SELECT @CurrentEmployee = OrgNode
   FROM HumanResources.EmployeeOrg
   WHERE EmployeeID = 6;

   SELECT OrgNode.ToString() AS Text_OrgNode, *
   FROM HumanResources.EmployeeOrg
   WHERE OrgNode.GetAncestor(2) = @CurrentEmployee
   ```

    This time, you also receive Mary who also reports to David, two levels down.

## Use GetRoot, and GetLevel

1. As the hierarchy grows larger, it's more difficult to determine where the members are in the hierarchy. Use the [GetLevel (Database Engine)](../../t-sql/data-types/getlevel-database-engine.md) method to find how many levels down each row is in the hierarchy. Execute the following code to view the levels of all the rows:

   ```sql
   SELECT OrgNode.ToString() AS Text_OrgNode,
   OrgNode.GetLevel() AS EmpLevel, *
   FROM HumanResources.EmployeeOrg;
   GO
   ```

1. Use the [GetRoot (Database Engine)](../../t-sql/data-types/getroot-database-engine.md) method to find the root node in the hierarchy. The following code returns the single row, which is the root:

   ```sql
   SELECT OrgNode.ToString() AS Text_OrgNode, *
   FROM HumanResources.EmployeeOrg
   WHERE OrgNode = HIERARCHYID::GetRoot();
   GO
   ```

## Reorder data in a hierarchical table using hierarchical methods

**Applies to:** [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]

Reorganizing a hierarchy is a common maintenance task. In this task, we use an `UPDATE` statement with the [GetReparentedValue (Database Engine)](../../t-sql/data-types/getreparentedvalue-database-engine.md) method to first move a single row to a new location in the hierarchy. Then we move an entire subtree to a new location.

The `GetReparentedValue` method takes two arguments. The first argument describes the part of the hierarchy to be modified. For example, if a hierarchy is `/1/4/2/3/` and you want to change the `/1/4/` section, the hierarchy becomes `/2/1/2/3/`, leaving the last two nodes (`2/3/`) unchanged, you must provide the changing nodes (`/1/4/`) as the first argument. The second argument provides the new hierarchy level, in our example `/2/1/`. The two arguments don't have to contain the same number of levels.

### Move a single row to a new location in the hierarchy

1. Currently Wanida reports to Sariya. In this procedure, you move Wanida from the current node `/1/1/`, so that this person reports to Jill. The new node becomes `/3/1/` so `/1/` is the first argument and `/3/` is the second. These correspond to the `OrgNode` values of Sariya and Jill. Execute the following code to move Wanida from Sariya's organization to Jill's:

   ```sql
   DECLARE @CurrentEmployee HIERARCHYID,
       @OldParent HIERARCHYID,
       @NewParent HIERARCHYID;
   
   SELECT @CurrentEmployee = OrgNode
   FROM HumanResources.EmployeeOrg
   WHERE EmployeeID = 269;
   
   SELECT @OldParent = OrgNode
   FROM HumanResources.EmployeeOrg
   WHERE EmployeeID = 46;
   
   SELECT @NewParent = OrgNode
   FROM HumanResources.EmployeeOrg
   WHERE EmployeeID = 119;
   
   UPDATE HumanResources.EmployeeOrg
   SET OrgNode = @CurrentEmployee.GetReparentedValue(@OldParent, @NewParent)
   WHERE OrgNode = @CurrentEmployee;
   GO
   ```

1. Execute the following code to see the result:

   ```sql
   SELECT OrgNode.ToString() AS Text_OrgNode,
   OrgNode, OrgLevel, EmployeeID, EmpName, Title
   FROM HumanResources.EmployeeOrg;
   GO
   ```

   Wanida is now at node `/3/1/`.

### Reorganize a section of a hierarchy

1. To demonstrate how to move a larger number of people at the same time, first execute the following code to add an intern reporting to Wanida:

   ```sql
   EXEC AddEmp 269, 291, 'Kevin', 'Marketing Intern';
   GO
   ```

1. Now Kevin reports to Wanida, who reports to Jill, who reports to David. That means that Kevin is at level `/3/1/1/`. To move all of Jill's subordinates to a new manager, we update all nodes that have `/3/` as their `OrgNode` to a new value. Execute the following code to update Wanida to report to Sariya, but keep Kevin reporting to Wanida:

   ```sql
   DECLARE @OldParent HIERARCHYID,
       @NewParent HIERARCHYID
   
   SELECT @OldParent = OrgNode
   FROM HumanResources.EmployeeOrg
   WHERE EmployeeID = 119;-- Jill
   
   SELECT @NewParent = OrgNode
   FROM HumanResources.EmployeeOrg
   WHERE EmployeeID = 46;-- Sariya
   
   DECLARE children_cursor CURSOR
   FOR
   SELECT OrgNode
   FROM HumanResources.EmployeeOrg
   WHERE OrgNode.GetAncestor(1) = @OldParent;
   
   DECLARE @ChildId HIERARCHYID;
   
   OPEN children_cursor
   
   FETCH NEXT
   FROM children_cursor
   INTO @ChildId;
   
   WHILE @@FETCH_STATUS = 0
   BEGIN
       START:
   
       DECLARE @NewId HIERARCHYID;
   
       SELECT @NewId = @NewParent.GetDescendant(MAX(OrgNode), NULL)
       FROM HumanResources.EmployeeOrg
       WHERE OrgNode.GetAncestor(1) = @NewParent;
   
       UPDATE HumanResources.EmployeeOrg
       SET OrgNode = OrgNode.GetReparentedValue(@ChildId, @NewId)
       WHERE OrgNode.IsDescendantOf(@ChildId) = 1;
   
       IF @@error <> 0
           GOTO START -- On error, retry
   
       FETCH NEXT
       FROM children_cursor
       INTO @ChildId;
   END
   
   CLOSE children_cursor;
   
   DEALLOCATE children_cursor;
   ```

1. Execute the following code to see the result:

   ```sql
   SELECT OrgNode.ToString() AS Text_OrgNode,
   OrgNode, OrgLevel, EmployeeID, EmpName, Title
   FROM HumanResources.EmployeeOrg;
   GO
   ```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Text_OrgNode OrgNode OrgLevel EmployeeID EmpName Title
------------ ------- -------- ---------- ------- -----------------
/            Ox      0        6          David   Marketing Manager
/1/          0x58    1        46         Sariya  Marketing Specialist
/1/1/        0x5AC0  2        269        Wanida  Marketing Assistant
/1/1/1/      0x5AD0  3        291        Kevin   Marketing Intern
/2/          0x68    1        271        John    Marketing Specialist
/2/1/        0x6AC0  2        272        Mary    Marketing Assistant
/3/          0x78    1        119        Jill    Marketing Specialist
```

The entire organizational tree that reported to Jill (both Wanida and Kevin), now reports to Sariya.

For a stored procedure to reorganize a section of a hierarchy, see the [Move subtrees](../../relational-databases/hierarchical-data-sql-server.md#move-subtrees) section of [Hierarchical data (SQL Server)](../hierarchical-data-sql-server.md).
