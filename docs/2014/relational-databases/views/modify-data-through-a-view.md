---
title: "Modify Data Through a View | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "data modifications [SQL Server], views"
  - "views [SQL Server], modifying data through"
  - "modifying data [SQL Server], views"
ms.assetid: 410e2812-4ebe-48b2-b95f-c7784f1c4336
author: stevestein
ms.author: sstein
manager: craigg
---
# Modify Data Through a View
  You can modify the data of an underlying base table in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To modify table data through a view, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   See the section 'Updatable Views' in [CREATE VIEW &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-view-transact-sql).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires UPDATE, INSERT, or DELETE permissions on the target table, depending on the action being performed.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To modify table data through a view  
  
1.  In **Object Explorer**, expand the database that contains the view and then expand **Views**.  
  
2.  Right-click the view and select **Edit Top 200 Rows**.  
  
3.  You may need to modify the SELECT statement in the **SQL** pane to return the rows to be modified.  
  
4.  In the **Results** pane, locate the row to be changed or deleted. To delete the row, right-click the row and select **Delete**. To change data in one or more columns, modify the data in the column.  
  
    > [!IMPORTANT]  
    >  You cannot delete a row if the view references more than one base table. You can only update columns that belong to a single base table.  
  
5.  To insert a row, scroll down to the end of the rows and insert the new values.  
  
    > [!IMPORTANT]  
    >  You cannot insert a row if the view references more than one base table.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To update table data through a view  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example changes the value in the `StartDate` and `EndDate` columns for a specific employee by referencing columns in the view `HumanResources.vEmployeeDepartmentHistory`. This view returns values from two tables. This statement succeeds because the columns being modified are from only one of the base tables.  
  
    ```  
    USE AdventureWorks2012 ;   
    GO  
    UPDATE HumanResources.vEmployeeDepartmentHistory  
    SET StartDate = '20110203', EndDate = GETDATE()   
    WHERE LastName = N'Smith' AND FirstName = 'Samantha';   
    GO  
    ```  
  
 For more information, see [UPDATE &#40;Transact-SQL&#41;](/sql/t-sql/queries/update-transact-sql).  
  
#### To insert table data through a view  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example inserts a new row into the base table `HumanResouces.Department` by specifying the relevant columns from the view `HumanResources.vEmployeeDepartmentHistory`. The statement succeeds because only columns from a single base table are specified and the other columns in the base table have default values.  
  
    ```  
    USE AdventureWorks2012 ;  
    GO  
    INSERT INTO HumanResources.vEmployeeDepartmentHistory (Department, GroupName)   
    VALUES ('MyDepartment', 'MyGroup');   
    GO  
    ```  
  
 For more information, see [INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/insert-transact-sql).  
  
  
