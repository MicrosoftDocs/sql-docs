---
title: "Optimizing the NewOrg Table | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "optimizing tables"
ms.assetid: 89ff6d37-94c0-4773-8be9-dde943fff023
author: stevestein
ms.author: sstein
manager: craigg
---
# Optimizing the NewOrg Table
  The **NewOrd** table that you created in the [Populating a Table with Existing Hierarchical Data](lesson-1-2-populating-a-table-with-existing-hierarchical-data.md) task contains all the employee information, and represents the hierarchical structure by using a `hierarchyid` data type. This task adds new indexes to support searches on the `hierarchyid` column.  
  
## Clustered Index  
 The `hierarchyid` column (**OrgNode**) is the primary key for the **NewOrg** table. When the table was created, it contained a clustered index named **PK_NewOrg_OrgNode** to enforce the uniqueness of the **OrgNode** column. This clustered index also supports a depth-first search of the table.  
  
## Nonclustered Index  
 This step creates two nonclustered indexes to support typical searches.  
  
#### To index the NewOrg table for efficient searches  
  
1.  To help queries at the same level in the hierarchy, use the [GetLevel](/sql/t-sql/data-types/getlevel-database-engine) method to create a computed column that contains the level in the hierarchy. Then, create a composite index on the level and the `Hierarchyid`. Run the following code to create the computed column and the breadth-first index:  
  
    ```  
    ALTER TABLE NewOrg   
       ADD H_Level AS OrgNode.GetLevel() ;  
    CREATE UNIQUE INDEX EmpBFInd   
       ON NewOrg(H_Level, OrgNode) ;  
    GO  
    ```  
  
2.  Create a unique index on the **EmployeeID** column. This is the traditional singleton lookup of a single employee by **EmployeeID** number. Run the following code to create an index on **EmployeeID**:  
  
    ```  
    CREATE UNIQUE INDEX EmpIDs_unq ON NewOrg(EmployeeID) ;  
    GO  
    ```  
  
3.  Run the following code to retrieve data from the table in the order of each of the three indexes:  
  
    ```  
    SELECT OrgNode.ToString() AS LogicalNode,  
    OrgNode, H_Level, EmployeeID, LoginID  
    FROM NewOrg   
    ORDER BY OrgNode;  
  
    SELECT OrgNode.ToString() AS LogicalNode,  
    OrgNode, H_Level, EmployeeID, LoginID   
    FROM NewOrg   
    ORDER BY H_Level, OrgNode;  
  
    SELECT OrgNode.ToString() AS LogicalNode,  
    OrgNode, H_Level, EmployeeID, LoginID   
    FROM NewOrg   
    ORDER BY EmployeeID;  
    GO  
    ```  
  
4.  Compare the result sets to see how the order is stored in each type of index. Only the first four rows of each output follow.  
  
     [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
     Depth-first index: Employee records are stored adjacent to their manager.  
  
     `LogicalNode OrgNode    H_Level EmployeeID LoginID`  
  
     `/             0x         0         1      zarifin`  
  
     `/1/          0x58        1         2      tplate`  
  
     `/1/1/       0x5AC0       2         4      schai`  
  
     `/1/1/1/     0x5AD6       3         9      jwang`  
  
     `/1/1/2/     0x5ADA       3        10      malexander`  
  
     `/1/2/       0x5B40       2         5      elang`  
  
     `/1/3/       0x5BC0       2         6      gsmits`  
  
     `/2/         0x68         1         3      hjensen`  
  
     `/2/1/       0x6AC0       2         7      sdavis`  
  
     `/2/2/       0x6B40       2         8      norint`  
  
     **EmployeeID**-first index: Rows are stored in **EmployeeID** sequence.  
  
     `LogicalNode OrgNode    H_Level EmployeeID LoginID`  
  
     `/             0x         0         1      zarifin`  
  
     `/1/          0x58        1         2      tplate`  
  
     `/2/         0x68         1         3      hjensen`  
  
     `/1/1/       0x5AC0       2         4      schai`  
  
     `/1/2/       0x5B40       2         5      elang`  
  
     `/1/3/       0x5BC0       2         6      gsmits`  
  
     `/2/1/       0x6AC0       2         7      sdavis`  
  
     `/2/2/       0x6B40       2         8      norint`  
  
     `/1/1/1/     0x5AD6       3         9      jwang`  
  
     `/1/1/2/     0x5ADA       3        10      malexander`  
  
> [!NOTE]  
>  For diagrams that show the difference between a depth-first index and a breadth-first index, see [Hierarchical Data &#40;SQL Server&#41;](../hierarchical-data-sql-server.md).  
  
#### To drop the unnecessary columns  
  
1.  The **ManagerID** column represents the employee/manager relationship, which is now represented by the **OrgNode** column. If other applications do not need the **ManagerID** column, consider dropping it by using the following statement:  
  
    ```  
    ALTER TABLE NewOrg DROP COLUMN ManagerID ;  
    GO  
    ```  
  
2.  The **EmployeeID** column is also redundant. The **OrgNode** column uniquely identifies each employee. If other applications do not need the **EmployeeID** column, consider dropping the index and then the column by using the following code:  
  
    ```  
    DROP INDEX EmpIDs_unq ON NewOrg ;  
    ALTER TABLE NewOrg DROP COLUMN EmployeeID ;  
    GO  
    ```  
  
#### To replace the original table with the new table  
  
1.  If your original table contained any additional indexes or constraints, add them to the **NewOrg** table.  
  
2.  Replace the old **EmployeeDemo** table with the new table. Run the following code to drop the old table, and then rename the new table with the old name:  
  
    ```  
    DROP TABLE EmployeeDemo ;  
    GO  
    sp_rename 'NewOrg', EmployeeDemo ;  
    GO  
    ```  
  
3.  Run the following code to examine the final table:  
  
    ```  
    SELECT * FROM EmployeeDemo ;  
    ```  
  
## Next Task in Lesson  
 [Summary: Converting a Table to a Hierarchical Structure](lesson-1-4-summary-converting-a-table-to-a-hierarchical-structure.md)  
  
  
