---
title: "Rename a Stored Procedure | Microsoft Docs"
ms.custom: ""
ms.date: "07/06/2017"
ms.prod: sql
ms.technology: stored-procedures
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "stored procedures [SQL Server], renaming"
  - "renaming stored procedures"
ms.assetid: 5d2e4c68-7e0b-4405-8919-f5b203e46770
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Rename a Stored Procedure
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
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
  
-   Procedure names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
-   Renaming a stored procedure retains the `object_id` and all the permissions that are specifically assigned to the procedure. Dropping and recreating the object creates a new `object_id` and removes any permissions specifically assign to the procedure.

-   Renaming a stored procedure does not change the name of the corresponding object name in the definition column of the **sys.sql_modules** catalog view. To do that, you must drop and re-create the stored procedure with its new name.  

-   Changing the name or definition of a procedure can cause dependent objects to fail when the objects are not updated to reflect the changes that have been made to the procedure. For more information, see [View the Dependencies of a Stored Procedure](../../relational-databases/stored-procedures/view-the-dependencies-of-a-stored-procedure.md).  
  
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
3.  [Determine the dependencies of the stored procedure](../../relational-databases/stored-procedures/view-the-dependencies-of-a-stored-procedure.md).  
4.  Expand **Stored Procedures**, right-click the procedure to rename, and then click **Rename**.  
5.  Modify the procedure name.  
6.  Modify the procedure name referenced in any dependent objects or scripts.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To rename a stored procedure  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
2.  From the Standard bar, click **New Query**.  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to rename a procedure by dropping the procedure and re-creating the procedure with a new name. The first example creates the stored procedure `'HumanResources.uspGetAllEmployeesTest`. The second example renames the stored procedure to `HumanResources.uspEveryEmployeeTest`.  
  
```sql  
--Create the stored procedure.  
USE AdventureWorks2012;  
GO  

CREATE PROCEDURE HumanResources.uspGetAllEmployeesTest  
AS  
    SET NOCOUNT ON;  
    SELECT LastName, FirstName, Department  
    FROM HumanResources.vEmployeeDepartmentHistory;  
GO  
  
--Rename the stored procedure.  
EXEC sp_rename 'HumanResources.uspGetAllEmployeesTest', 'uspEveryEmployeeTest'; 
```  
  
## See Also  
 [ALTER PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-procedure-transact-sql.md)   
 [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)   
 [Create a Stored Procedure](../../relational-databases/stored-procedures/create-a-stored-procedure.md)   
 [Modify a Stored Procedure](../../relational-databases/stored-procedures/modify-a-stored-procedure.md)   
 [Delete a Stored Procedure](../../relational-databases/stored-procedures/delete-a-stored-procedure.md)   
 [View the Definition of a Stored Procedure](../../relational-databases/stored-procedures/view-the-definition-of-a-stored-procedure.md)   
 [View the Dependencies of a Stored Procedure](../../relational-databases/stored-procedures/view-the-dependencies-of-a-stored-procedure.md)  
  
  
