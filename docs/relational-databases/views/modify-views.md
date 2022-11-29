---
description: "Modify Views"
title: "Modify Views | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "views [SQL Server], renaming"
  - "views [SQL Server], modifying"
  - "modifying views"
  - "renaming views"
ms.assetid: 2d3c14dc-43e5-4324-b8fb-f2692d330b16
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Modify Views
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  After you define a view, you can modify its definition in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] without dropping and re-creating the view by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To modify a view, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   Modifying a view does not affect any dependent objects, such as stored procedures or triggers, unless the definition of the view changes in such a way that the dependent object is no longer valid.  
  
-   If a view currently used is modified by using ALTER VIEW, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] takes an exclusive schema lock on the view. When the lock is granted, and there are no active users of the view, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] deletes all copies of the view from the procedure cache. Existing plans referencing the view remain in the cache but are recompiled when invoked.  
  
-   ALTER VIEW can be applied to indexed views; however, ALTER VIEW unconditionally drops all indexes on the view.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 To execute ALTER VIEW, at a minimum, ALTER permission on OBJECT is required.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To modify a view  
  
1.  In **Object Explorer**, click the plus sign next to the database where your view is located and then click the plus sign next to the **Views** folder.  
  
2.  Right-click on the view you wish to modify and select **Design**.  
  
3.  In the diagram pane of the query designer, make changes to the view in one or more of the following ways:  
  
    1.  Select or clear the check boxes of any elements you wish to add or remove.  
  
    2.  Right-click within the diagram pane, select **Add Table...**, and then select the additional columns you want to add to the view from the **Add Table** dialog box.  
  
    3.  Right-click the title bar of the table you wish to remove and select **Remove**.  
  
4.  On the **File** menu, click **Save**_view name_.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To modify a view  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example first creates a view and then modifies the view by using ALTER VIEW. A WHERE clause is added to the view definition.  
  
    ```  
    USE AdventureWorks2012 ;  
    GO  
    -- Create a view.  
    CREATE VIEW HumanResources.EmployeeHireDate  
    AS  
    SELECT p.FirstName, p.LastName, e.HireDate  
    FROM HumanResources.Employee AS e JOIN Person.Person AS  p  
    ON e.BusinessEntityID = p.BusinessEntityID ;   
  
    -- Modify the view by adding a WHERE clause to limit the rows returned.  
    ALTER VIEW HumanResources.EmployeeHireDate  
    AS  
    SELECT p.FirstName, p.LastName, e.HireDate  
    FROM HumanResources.Employee AS e JOIN Person.Person AS  p  
    ON e.BusinessEntityID = p.BusinessEntityID  
    WHERE HireDate < CONVERT(DATETIME,'20020101',101) ;   
    GO  
    ```  
  
 For more information, see [ALTER VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/alter-view-transact-sql.md).  
  
  
