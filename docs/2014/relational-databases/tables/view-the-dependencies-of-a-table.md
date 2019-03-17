---
title: "View the Dependencies of a Table | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "table dependencies [SQL Server]"
  - "dependencies [SQL Server], tables"
  - "displaying dependences"
  - "viewing dependencies"
ms.assetid: c4351ef5-e7d0-46e7-8367-88695e9974f8
author: stevestein
ms.author: sstein
manager: craigg
---
# View the Dependencies of a Table
  You can view a table's dependencies in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To view a table's dependencies, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires VIEW DEFINITION permission on the database and SELECT permission on sys.sql_expression_dependencies for the database. By default, SELECT permission is granted only to members of the db_owner fixed database role. When SELECT and VIEW DEFINITION permissions are granted to another user, the grantee can view all dependencies in the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view the dependencies of a table  
  
1.  In **Object Explorer**, expand **Databases**, expand a database, and then expand **Tables**.  
  
2.  Right-click a table, and then click **View Dependencies**.  
  
3.  In the **Object Dependencies**_\<object name>_ dialog box, select either **Objects that depend on** _\<object name>_, or **Objects on which**_\<object name>_**depends**.  
  
4.  Select an object in the **Dependencies** grid. The type of object (such as "Trigger" or "Stored Procedure"), appears in the **Type** box.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To view the objects that depend on a table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    SELECT * FROM sys.sql_expression_dependencies  
    WHERE referencing_id = OBJECT_ID(N'Production.vProductAndDescription');   
    GO  
  
    ```  
  
#### To view the objects on which a table depends  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  The following example returns the objects that depend on the table `Production.Product`. Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;   
    GO  
    SELECT * FROM sys.sql_expression_dependencies  
    WHERE referenced_id = OBJECT_ID(N'Production.Product');   
    GO  
  
    ```  
  
 For additional information, see [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql)  
  
  
