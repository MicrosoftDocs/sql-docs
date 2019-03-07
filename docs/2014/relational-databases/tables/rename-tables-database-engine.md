---
title: "Rename Tables (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "table renaming [SQL Server]"
  - "table names [SQL Server]"
  - "tables [SQL Server], Visual Database Tools"
  - "renaming tables"
ms.assetid: 2f5c922d-4d71-4694-9fca-28dd99375799
author: stevestein
ms.author: sstein
manager: craigg
---
# Rename Tables (Database Engine)
  You can rename a table in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
> [!CAUTION]  
>  Think carefully before you rename a table. If existing queries, views, user-defined functions, stored procedures, or programs refer to that table, the name modification will make these objects invalid.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To rename a table, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 Renaming a table will not automatically rename references to that table. You must manually modify any objects that reference the renamed table. For example, if you rename a table and that table is referenced in a trigger, you must modify the trigger to reflect the new table name. Use [sys.sql_expression_dependencies](/sql/relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql) to list dependencies on the table before renaming it.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To rename a table  
  
1.  In Object Explorer, right-click the table you want to rename and choose **Design** from the shortcut menu.  
  
2.  From the **View** menu, choose **Properties**.  
  
3.  In the field for the **Name** value in the **Properties** window, type a new name for the table.  
  
4.  To cancel this action, press the ESC key before leaving this field.  
  
5.  From the **File** menu choose **Save**_table name_.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To rename a table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  The following example renames the `SalesTerritory` table to `SalesTerr` in the `Sales` schema. Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;   
    GO  
    EXEC sp_rename 'Sales.SalesTerritory', 'SalesTerr';  
    ```  
  
 For additional examples, see [sp_rename &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-rename-transact-sql).  
  
  
