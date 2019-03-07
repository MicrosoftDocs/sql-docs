---
title: "Rename a Stored Procedure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.technology: stored-procedures
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "stored procedures [SQL Server], renaming"
  - "renaming stored procedures"
ms.assetid: 5d2e4c68-7e0b-4405-8919-f5b203e46770
author: stevestein
ms.author: sstein
manager: craigg
---
# Rename a Stored Procedure
  This topic describes how to rename a stored procedure in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To rename a stored procedure, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   Procedure names must comply with the rules for [identifiers](../databases/database-identifiers.md).  
  
-   Renaming a stored procedure will not change the name of the corresponding object name in the definition column of the **sys.sql_modules** catalog view. Therefore, we recommend that you do not rename this object type. Instead, drop and re-create the stored procedure with its new name.  
  
-   Changing the name or definition of a procedure can cause dependent objects to fail when the objects are not updated to reflect the changes that have been made to the procedure. For more information, see [View the Dependencies of a Stored Procedure](view-the-dependencies-of-a-stored-procedure.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 CREATE PROCEDURE  
 Requires CREATE PROCEDURE permission in the database and ALTER permission on the schema in which the procedure is being created, or requires membership in the **db_ddladmin** fixed database role.  
  
 ALTER PROCEDURE  
 Requires ALTER permission on the procedure or requires membership in the **db_ddladmin** fixed database role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To rename a stored procedure  
  
1.  In Object Explorer, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, expand the database in which the procedure belongs, and then expand **Programmability**.  
  
3.  [Determine the dependencies of the stored procedure](view-the-dependencies-of-a-stored-procedure.md).  
  
4.  Expand **Stored Procedures**, right-click the procedure to rename, and then click **Rename**.  
  
5.  Modify the procedure name.  
  
6.  Modify the procedure name referenced in any dependent objects or scripts.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To rename a stored procedure  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to rename a procedure by dropping the procedure and re-creating the procedure with a new name. The first example creates the stored procedure `'HumanResources.uspGetAllEmployeesTest`. The second example renames the stored procedure to `HumanResources.uspEveryEmployeeTest`.  
  
```tsql  
--Create the stored procedure.  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID ( 'HumanResources.uspGetAllEmployeesTest', 'P' ) IS NOT NULL   
    DROP PROCEDURE HumanResources.uspGetAllEmployeesTest;  
GO  
CREATE PROCEDURE HumanResources.uspGetAllEmployeesTest  
AS  
    SET NOCOUNT ON;  
    SELECT LastName, FirstName, Department  
    FROM HumanResources.vEmployeeDepartmentHistory;  
GO  
  
--Rename the stored procedure.  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID ( 'HumanResources.uspGetAllEmployeesTest', 'P' ) IS NOT NULL   
    DROP PROCEDURE HumanResources.uspGetAllEmployeesTest;  
GO  
CREATE PROCEDURE HumanResources.uspEveryEmployeeTest  
AS  
    SET NOCOUNT ON;  
    SELECT LastName, FirstName, Department  
    FROM HumanResources.vEmployeeDepartmentHistory;  
GO  
```  
  
## See Also  
 [ALTER PROCEDURE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-procedure-transact-sql)   
 [CREATE PROCEDURE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-procedure-transact-sql)   
 [Create a Stored Procedure](../stored-procedures/create-a-stored-procedure.md)   
 [Modify a Stored Procedure](../stored-procedures/modify-a-stored-procedure.md)   
 [Delete a Stored Procedure](../stored-procedures/delete-a-stored-procedure.md)   
 [View the Definition of a Stored Procedure](view-the-definition-of-a-stored-procedure.md)   
 [View the Dependencies of a Stored Procedure](view-the-dependencies-of-a-stored-procedure.md)  
  
  
