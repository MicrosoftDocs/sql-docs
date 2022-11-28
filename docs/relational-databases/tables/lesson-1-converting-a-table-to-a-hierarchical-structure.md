---
description: "Lesson 1: Converting a Table to a Hierarchical Structure"
title: "Lesson 1: Converting a Table to a Hierarchical Structure | Microsoft Docs"
ms.custom: ""
ms.date: "08/22/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "HierarchyID"
ms.assetid: 5ee6f19a-6dd7-4730-a91c-bbed1bd77e0b
author: MashaMSFT
ms.author: mathoma
---
# Lesson 1: Converting a Table to a Hierarchical Structure
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
Customers who have tables using self joins to express hierarchical relationships can convert their tables to a hierarchical structure using this lesson as a guide. It is relatively easy to migrate from this representation to one using **hierarchyid**. After migration, users will have a compact and easy to understand hierarchical representation, which can be indexed in several ways for efficient queries.  
  
This lesson, examines an existing table, creates a new table containing a **hierarchyid** column, populates the table with the data from the source table, and then demonstrates three indexing strategies. This lesson contains the following topics:  
 
  
## Prerequisites  
To complete this tutorial, you need SQL Server Management Studio, access to a server that's running SQL Server, and an AdventureWorks database.

- Install [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md).
- Install [SQL Server 2017 Developer Edition](https://www.microsoft.com/sql-server/sql-server-downloads).
- Download [AdventureWorks2017 sample databases](../../samples/adventureworks-install-configure.md).

Instructions for restoring databases in SSMS are here: [Restore a database](../backup-restore/restore-a-database-backup-using-ssms.md).  

## Examine the current structure of the employee table
The sample Adventureworks2017 (or later) database contains an **Employee** table in the **HumanResources** schema. To avoid changing the original table, this step makes a copy of the **Employee** table named **EmployeeDemo**. To simplify the example, you only copy five columns from the original table. Then, you query the **HumanResources.EmployeeDemo** table to review how the data is structured in a table without using the **hierarchyid** data type.  
  
### Copy the Employee table  
  
1.  In a Query Editor window, run the following code to copy the table structure and data from the **Employee** table into a new table named **EmployeeDemo**. Since the original table already uses hierarchyid, this query essentially flattens the hierarchy to retrieve the manager of the employee. In subsequent parts of this lesson we will be reconstructing this hierarchy.

   ```sql  
   USE AdventureWorks2017;  
   GO  
     if OBJECT_ID('HumanResources.EmployeeDemo') is not null
    drop table HumanResources.EmployeeDemo 

    SELECT emp.BusinessEntityID AS EmployeeID, emp.LoginID, 
     (SELECT  man.BusinessEntityID FROM HumanResources.Employee man 
	    WHERE emp.OrganizationNode.GetAncestor(1)=man.OrganizationNode OR 
		    (emp.OrganizationNode.GetAncestor(1) = 0x AND man.OrganizationNode IS NULL)) AS ManagerID,
          emp.JobTitle, emp.HireDate
   INTO HumanResources.EmployeeDemo   
   FROM HumanResources.Employee emp ;
   GO
   ```  
  
### Examine the structure and data of the EmployeeDemo table  
  
-   This new **EmployeeDemo** table represents a typical table in an existing database that you might want to migrate to a new structure. In a Query Editor window, run the following code to show how the table uses a self join to display the employee/manager relationships:  
  
    ```sql  
    SELECT   
        Mgr.EmployeeID AS MgrID, Mgr.LoginID AS Manager,   
        Emp.EmployeeID AS E_ID, Emp.LoginID, Emp.JobTitle  
    FROM HumanResources.EmployeeDemo AS Emp  
    LEFT JOIN HumanResources.EmployeeDemo AS Mgr  
    ON Emp.ManagerID = Mgr.EmployeeID  
    ORDER BY MgrID, E_ID  
    ```  
  
    [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
    ```  
    MgrID Manager                 E_ID LoginID                  JobTitle  
    NULL	NULL	                1	adventure-works\ken0	    Chief Executive Officer
    1	adventure-works\ken0	    2	adventure-works\terri0   	Vice President of Engineering
    1	adventure-works\ken0	   16	adventure-works\david0	  Marketing Manager
    1	adventure-works\ken0	   25	adventure-works\james1	  Vice President of Production
    1	adventure-works\ken0	  234	adventure-works\laura1	  Chief Financial Officer
    1	adventure-works\ken0  	263	adventure-works\jean0	    Information Services Manager
    1	adventure-works\ken0	  273	adventure-works\brian3	  Vice President of Sales
    2	adventure-works\terri0	  3	adventure-works\roberto0	Engineering Manager
    3	adventure-works\roberto0	4	adventure-works\rob0	    Senior Tool Designer
    ...  
    ```  
  
    The results continue for a total of 290 rows.  
  
Notice that the **ORDER BY** clause caused the output to list the direct reports of each management level together. For instance, all seven of the direct reports of **MgrID** 1 (ken0) are listed adjacent to each other. Although not impossible, it is much more difficult to group all those who eventually report to **MgrID** 1.  


## Populate a Table with Existing Hierarchical Data
This task creates a new table and populates it with the data in the **EmployeeDemo** table. This task has the following steps:  
  
-   Create a new table that contains a **hierarchyid** column. This column could replace the existing **EmployeeID** and **ManagerID** columns. However, you will retain those columns. This is because existing applications might refer to those columns, and also to help you understand the data after the transfer. The table definition specifies that **OrgNode** is the primary key, which requires the column to contain unique values. The clustered index on the **OrgNode** column will store the date in **OrgNode** sequence.    
-   Create a temporary table that is used to track how many employees report directly to each manager. 
-   Populate the new table by using data from the **EmployeeDemo** table.  
  
### To create a new table named NewOrg  
  
-   In a Query Editor window, run the following code to create a new table named **HumanResources.NewOrg**:  
  
    ```sql   
    CREATE TABLE HumanResources.NewOrg  
    (  
      OrgNode hierarchyid,  
      EmployeeID int,  
      LoginID nvarchar(50),  
      ManagerID int  
    CONSTRAINT PK_NewOrg_OrgNode  
      PRIMARY KEY CLUSTERED (OrgNode)  
    );  
    GO  
    ```  
  
### Create a temporary table named #Children  
  
1.  Create a temporary table named **#Children** with a column named **Num** that will contain the number of children for each node:  
  
    ```sql  
    CREATE TABLE #Children   
       (  
        EmployeeID int,  
        ManagerID int,  
        Num int  
    );  
    GO  
    ```  
  
2.  Add an index that will significantly speed up the query that populates the **NewOrg** table:  
  
    ```sql  
    CREATE CLUSTERED INDEX tmpind ON #Children(ManagerID, EmployeeID);  
    GO  
    ```  
  
### Populate the NewOrg table  
  
1.  Recursive queries forbid subqueries with aggregates. Instead, populate the **#Children** table with the following code, which uses the [ROW_NUMBER()](../../t-sql/functions/row-number-transact-sql.md) method to populate the **Num** column:  
  
    ```sql 
    INSERT #Children (EmployeeID, ManagerID, Num)  
    SELECT EmployeeID, ManagerID,  
      ROW_NUMBER() OVER (PARTITION BY ManagerID ORDER BY ManagerID)   
    FROM HumanResources.EmployeeDemo  
    GO 
    ```  
  
2.  Review the **#Children** table. Note how the **Num** column contains sequential numbers for each manager.  
  
    ```sql  
    SELECT * FROM #Children ORDER BY ManagerID, Num  
    GO  
  
    ```  
  
    [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  

    ```
    EmployeeID	ManagerID	Num
    1  	NULL  	1
    2	     1    1
    16	   1	  2
    25	   1	  3
    234	   1	  4
    263	   1	  5
    273	   1	  6
    3	     2	  1
    4	     3	  1
    5	     3	  2
    6	     3	  3
    7	     3	  4
    ```


3.  Populate the **NewOrg** table. Use the GetRoot and ToString methods to concatenate the **Num** values into the **hierarchyid** format, and then update the **OrgNode** column with the resultant hierarchical values:  
  
    ```sql  
    WITH paths(path, EmployeeID)   
    AS (  
    -- This section provides the value for the root of the hierarchy  
    SELECT hierarchyid::GetRoot() AS OrgNode, EmployeeID   
    FROM #Children AS C   
    WHERE ManagerID IS NULL   

    UNION ALL   
    -- This section provides values for all nodes except the root  
    SELECT   
    CAST(p.path.ToString() + CAST(C.Num AS varchar(30)) + '/' AS hierarchyid),   
    C.EmployeeID  
    FROM #Children AS C   
    JOIN paths AS p   
       ON C.ManagerID = P.EmployeeID   
    )  
    INSERT HumanResources.NewOrg (OrgNode, O.EmployeeID, O.LoginID, O.ManagerID)  
    SELECT P.path, O.EmployeeID, O.LoginID, O.ManagerID  
    FROM HumanResources.EmployeeDemo AS O   
    JOIN Paths AS P   
       ON O.EmployeeID = P.EmployeeID  
    GO 
    ```  
  
4.  A **hierarchyid** column is more understandable when you convert it to character format. Review the data in the **NewOrg** table by executing the following code, which contains two representations of the **OrgNode** column:  
  
    ```sql  
    SELECT OrgNode.ToString() AS LogicalNode, *   
    FROM HumanResources.NewOrg   
    ORDER BY LogicalNode;  
    GO  
    ```  
  
    The **LogicalNode** column converts the **hierarchyid** column into a more readable text form that represents the hierarchy. In the remaining tasks, you will use the `ToString()` method to show the logical format of the **hierarchyid** columns.  
  
5.  Drop the temporary table, which is no longer needed:  
  
    ```sql  
    DROP TABLE #Children  
    GO  
    ```  
  
## Optimizing the NewOrg Table
The **NewOrd** table that you created in the [Populating a Table with Existing Hierarchical Data]() task contains all the employee information, and represents the hierarchical structure by using a **hierarchyid** data type. This task adds new indexes to support searches on the **hierarchyid** column.  
  

The **hierarchyid** column (**OrgNode**) is the primary key for the **NewOrg** table. When the table was created, it contained a clustered index named **PK_NewOrg_OrgNode** to enforce the uniqueness of the **OrgNode** column. This clustered index also supports a depth-first search of the table.  
  
  
### Create index on NewOrg table for efficient searches  
  
1.  To help queries at the same level in the hierarchy, use the [GetLevel](../../t-sql/data-types/getlevel-database-engine.md) method to create a computed column that contains the level in the hierarchy. Then, create a composite index on the level and the **Hierarchyid**. Run the following code to create the computed column and the breadth-first index:  
  
    ```sql  
    ALTER TABLE HumanResources.NewOrg   
       ADD H_Level AS OrgNode.GetLevel() ;  
    CREATE UNIQUE INDEX EmpBFInd   
       ON HumanResources.NewOrg(H_Level, OrgNode) ;  
    GO  
    ```  
  
2.  Create a unique index on the **EmployeeID** column. This is the traditional singleton lookup of a single employee by **EmployeeID** number. Run the following code to create an index on **EmployeeID**:  
  
    ```sql  
    CREATE UNIQUE INDEX EmpIDs_unq ON HumanResources.NewOrg(EmployeeID) ;  
    GO
    ```  
  
3.  Run the following code to retrieve data from the table in the order of each of the three indexes:  
  
    ```sql  
    SELECT OrgNode.ToString() AS LogicalNode,  
    OrgNode, H_Level, EmployeeID, LoginID  
    FROM HumanResources.NewOrg   
    ORDER BY OrgNode;  

    SELECT OrgNode.ToString() AS LogicalNode,  
    OrgNode, H_Level, EmployeeID, LoginID   
    FROM HumanResources.NewOrg   
    ORDER BY H_Level, OrgNode;  

    SELECT OrgNode.ToString() AS LogicalNode,  
    OrgNode, H_Level, EmployeeID, LoginID   
    FROM HumanResources.NewOrg   
    ORDER BY EmployeeID;  
    GO  
    ```  
  
4.  Compare the result sets to see how the order is stored in each type of index. Only the first four rows of each output follow.  
  
    [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
    Depth-first index: Employee records are stored adjacent to their manager.  

    ```
    LogicalNode	OrgNode	H_Level	EmployeeID	LoginID
    /	0x	0	1	adventure-works\ken0
    /1/	0x58	1	2	adventure-works\terri0
    /1/1/	0x5AC0	2	3	adventure-works\roberto0
    /1/1/1/	0x5AD6	3	4	adventure-works\rob0
    /1/1/2/	0x5ADA	3	5	adventure-works\gail0
    /1/1/3/	0x5ADE	3	6	adventure-works\jossef0
    /1/1/4/	0x5AE1	3	7	adventure-works\dylan0
    /1/1/4/1/	0x5AE158	4	8	adventure-works\diane1
    /1/1/4/2/	0x5AE168	4	9	adventure-works\gigi0
    /1/1/4/3/	0x5AE178	4	10	adventure-works\michael6
    /1/1/5/	0x5AE3	3	11	adventure-works\ovidiu0
    ```

    **EmployeeID**-first index: Rows are stored in **EmployeeID** sequence.  

    ```
    LogicalNode	OrgNode	H_Level	EmployeeID	LoginID
    /	0x	0	1	adventure-works\ken0
    /1/	0x58	1	2	adventure-works\terri0
    /1/1/	0x5AC0	2	3	adventure-works\roberto0
    /1/1/1/	0x5AD6	3	4	adventure-works\rob0
    /1/1/2/	0x5ADA	3	5	adventure-works\gail0
    /1/1/3/	0x5ADE	3	6	adventure-works\jossef0
    /1/1/4/	0x5AE1	3	7	adventure-works\dylan0
    /1/1/4/1/	0x5AE158	4	8	adventure-works\diane1
    /1/1/4/2/	0x5AE168	4	9	adventure-works\gigi0
    /1/1/4/3/	0x5AE178	4	10	adventure-works\michael6
    /1/1/5/	0x5AE3	3	11	adventure-works\ovidiu0
    /1/1/5/1/	0x5AE358	4	12	adventure-works\thierry0
    ```
  
> [!NOTE]  
> For diagrams that show the difference between a depth-first index and a breadth-first index, see [Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md).  
  
### Drop the unnecessary columns  
  
1.  The **ManagerID** column represents the employee/manager relationship, which is now represented by the **OrgNode** column. If other applications do not need the **ManagerID** column, consider dropping it by using the following statement:  
  
    ```sql  
    ALTER TABLE HumanResources.NewOrg DROP COLUMN ManagerID ;  
    GO  
    ```  
  
2.  The **EmployeeID** column is also redundant. The **OrgNode** column uniquely identifies each employee. If other applications do not need the **EmployeeID** column, consider dropping the index and then the column by using the following code:  
  
    ```sql  
    DROP INDEX EmpIDs_unq ON HumanResources.NewOrg ;  
    ALTER TABLE HumanResources.NewOrg DROP COLUMN EmployeeID ;  
    GO  
    ```  
  
### Replace the original table with the new table  
  
1.  If your original table contained any additional indexes or constraints, add them to the **NewOrg** table.  
  
2.  Replace the old **EmployeeDemo** table with the new table. Run the following code to drop the old table, and then rename the new table with the old name:  
  
    ```sql  
    DROP TABLE HumanResources.EmployeeDemo ;  
    GO  
    sp_rename 'HumanResources.NewOrg', 'EmployeeDemo' ;  
    GO  
    ```  
  
3.  Run the following code to examine the final table:  
  
    ```sql  
    SELECT * FROM HumanResources.EmployeeDemo ;  
    ```  
  
## Next steps
The next article teaches you to create and manage data in a hierarchical table. 

Go to the next article to learn more:
> [!div class="nextstepaction"]
> [Next steps](lesson-2-creating-and-managing-data-in-a-hierarchical-table.md)
