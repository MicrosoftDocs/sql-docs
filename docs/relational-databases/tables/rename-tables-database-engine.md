---
title: "Rename Tables (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-tables"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "table renaming [SQL Server]"
  - "table names [SQL Server]"
  - "tables [SQL Server], Visual Database Tools"
  - "renaming tables"
ms.assetid: 2f5c922d-4d71-4694-9fca-28dd99375799
caps.latest.revision: 16
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Rename Tables (Database Engine)
[!INCLUDE[tsql-appliesto-ss2016-all_md](../../includes/tsql-appliesto-ss2016-all-md.md)]

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
 Renaming a table will not automatically rename references to that table. You must manually modify any objects that reference the renamed table. For example, if you rename a table and that table is referenced in a trigger, you must modify the trigger to reflect the new table name. Use [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) to list dependencies on the table before renaming it.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To rename a table  
  
1.  In Object Explorer, right-click the table you want to rename and choose **Design** from the shortcut menu.  
  
2.  From the **View** menu, choose **Properties**.  
  
3.  In the field for the **Name** value in the **Properties** window, type a new name for the table.  
  
4.  To cancel this action, press the ESC key before leaving this field.  
  
5.  From the **File** menu choose **Save***table name*.  
  
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
  
 For additional examples, see [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md).  
  
  