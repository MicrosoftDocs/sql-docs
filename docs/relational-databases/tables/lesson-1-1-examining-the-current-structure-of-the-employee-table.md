---
title: "Examining the Current Structure of the Employee Table | Microsoft Docs"
ms.custom: ""
ms.date: "03/27/2018"
ms.prod: "sql"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "tables"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
helpviewer_keywords: 
  - "examining the current structure of the employee"
ms.assetid: d546a820-105a-417d-ac35-44a6d1d70ac6
caps.latest.revision: 15
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# Lesson 1-1 - Examining the Current Structure of the Employee Table
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]
The sample [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] (or later) database contains an **Employee** table in the **HumanResources** schema. To avoid changing the original table, this step makes a copy of the **Employee** table named **EmployeeDemo**. To simplify the example, you only copy five columns from the original table. Then, you query the **HumanResources.EmployeeDemo** table to review how the data is structured in a table without using the **hierarchyid** data type.  
  
### To copy the Employee table  
  
1.  In a Query Editor window, run the following code to copy the table structure and data from the **Employee** table into a new table named **EmployeeDemo**. Since the original table already uses hierarchyid, this query essentially flattens the hierarchy to retrieve the manager of the employee. In subsequent parts of this lesson we will be reconstructing this hierarchy.
  
    ```  
    USE AdventureWorks ;  
    GO  
  
    SELECT emp.BusinessEntityID AS EmployeeID, emp.LoginID, 
      (SELECT  man.BusinessEntityID FROM HumanResources.Employee man 
		    WHERE emp.OrganizationNode.GetAncestor(1)=man.OrganizationNode OR 
			    (emp.OrganizationNode.GetAncestor(1) = 0x AND man.OrganizationNode IS NULL)) AS ManagerID
	    , emp.JobTitle, emp.HireDate
    INTO HumanResources.EmployeeDemo   
    FROM HumanResources.Employee emp ;
    GO
    ```  
  
### To examine the structure and data of the EmployeeDemo table  
  
-   This new **EmployeeDemo** table represents a typical table in an existing database that you might want to migrate to a new structure. In a Query Editor window, run the following code to show how the table uses a self join to display the employee/manager relationships:  
  
    ```  
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
    NULL	NULL	1	adventure-works\ken0	Chief Executive Officer
    1	adventure-works\ken0	2	adventure-works\terri0	Vice President of Engineering
    1	adventure-works\ken0	16	adventure-works\david0	Marketing Manager
    1	adventure-works\ken0	25	adventure-works\james1	Vice President of Production
    1	adventure-works\ken0	234	adventure-works\laura1	Chief Financial Officer
    1	adventure-works\ken0	263	adventure-works\jean0	Information Services Manager
    1	adventure-works\ken0	273	adventure-works\brian3	Vice President of Sales
    2	adventure-works\terri0	3	adventure-works\roberto0	Engineering Manager
    3	adventure-works\roberto0	4	adventure-works\rob0	Senior Tool Designer
    ...  
    ```  
  
    The results continue for a total of 290 rows.  
  
Notice that the **ORDER BY** clause caused the output to list the direct reports of each management level together. For instance, all seven of the direct reports of **MgrID** 1 (ken0) are listed adjacent to each other. Although not impossible, it is much more difficult to group all those who eventually report to **MgrID** 1.  
  
In the next task, we will create a new table with a **hierarchyid** data type, and move the data into the new table.  
  
## Next Task in Lesson  
[Populating a Table with Existing Hierarchical Data](../../relational-databases/tables/lesson-1-2-populating-a-table-with-existing-hierarchical-data.md)  
  
  
  
