---
title: "View Collation Information | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "collations [SQL Server], view"
ms.assetid: 1338b4ea-7142-44bc-a3b9-44e54431405f
author: stevestein
ms.author: sstein
manager: craigg
---
# View Collation Information
    
##  <a name="Top"></a> You can view the collation of a server, database, or column in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] using Object Explorer menu options or by using [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
##  <a name="Procedures"></a> How to View a Collation Setting  
 You can use one of the following:  
  
-   [SQL Server Management Studio](#SSMSProcedure)  
  
-   [Transact-SQL](#TsqlProcedure)  
  
###  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To view a collation setting for a server (instance of SQL Server) in Object Explorer**  
  
1.  In Object Explorer, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  Right-click the instance and select **Properties**.  
  
 **To view a collation setting for a database in Object Explorer**  
  
1.  In Object Explorer, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, right-click the database and select **Properties**.  
  
 **To view a collation setting for a column in Object Explorer**  
  
1.  In Object Explorer, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, expand the database and then expand **Tables**.  
  
3.  Expand the table that contains the column and then expand **Columns**.  
  
4.  Right-click the column and select **Properties**. If the collation property is empty, the column is not a character data type.  
  
###  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To view the collation setting of a server**  
  
1.  In Object Explorer, connect to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and on the toolbar, click **New Query**.  
  
2.  In the query window, enter the following statement that uses the SERVERPROPERTY system function.  
  
    ```  
    SELECT CONVERT (varchar, SERVERPROPERTY('collation'));  
    ```  
  
3.  Alternatively, you can use the sp_helpsort system stored procedure.  
  
    ```  
    EXECUTE sp_helpsort;  
    ```  
  
 **To view all collations supported by [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]**  
  
1.  In Object Explorer, connect to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and on the toolbar, click **New Query**.  
  
2.  In the query window, enter the following statement that uses the SERVERPROPERTY system function.  
  
    ```  
    SELECT name, description FROM sys.fn_helpcollations();  
    ```  
  
 **To view the collation setting of a database**  
  
1.  In Object Explorer, connect to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and on the toolbar, click **New Query**.  
  
2.  In the query window, enter the following statement that uses the sys.databases system catalog view.  
  
    ```  
    SELECT name, collation_name FROM sys.databases;  
    ```  
  
3.  Alternatively, you can use the DATABASEPROPERTYEX system function.  
  
    ```  
    SELECT CONVERT (varchar, DATABASEPROPERTYEX('database_name','collation'));  
    ```  
  
 **To view the collation setting of a column**  
  
1.  In Object Explorer, connect to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and on the toolbar, click **New Query**.  
  
2.  In the query window, enter the following statement that uses the sys.columns system catalog view.  
  
    ```  
    SELECT name, collation_name FROM sys.columns WHERE name = N'<insert character data type column name>';  
    ```  
  
## See Also  
 [SERVERPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/serverproperty-transact-sql)   
 [sys.fn_helpcollations &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/sys-fn-helpcollations-transact-sql)   
 [sys.databases &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql)   
 [sys.columns &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-columns-transact-sql)   
 [Collation Precedence &#40;Transact-SQL&#41;](/sql/t-sql/statements/collation-precedence-transact-sql)   
 [sp_helpsort &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-helpsort-transact-sql)  
  
  
