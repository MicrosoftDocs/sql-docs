---
title: "Grant Permissions on a Stored Procedure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.technology: stored-procedures
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "stored procedures [SQL Server], permissions"
ms.assetid: a7d15816-a788-4099-ad91-dc4b26618299
author: stevestein
ms.author: sstein
manager: craigg
---
# Grant Permissions on a Stored Procedure
  This topic describes how to grant permissions on a stored procedure in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Permissions can be granted to an existing user, database role, or application role in the database.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To grant permissions on a stored procedure, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   You cannot use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to grant permissions on system procedures or system functions. Use [GRANT Object Permissions](/sql/t-sql/statements/grant-object-permissions-transact-sql) instead.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 The grantor (or the principal specified with the AS option) must have either the permission itself with GRANT OPTION, or a higher permission that implies the permission being granted. Requires ALTER permission on the schema to which the procedure belongs, or CONTROL permission on the procedure. For more information, see [GRANT Object Permissions &#40;Transact-SQL&#41;](/sql/t-sql/statements/grant-object-permissions-transact-sql).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To grant permissions on a stored procedure  
  
1.  In Object Explorer, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, expand the database in which the procedure belongs, and then expand **Programmability**.  
  
3.  Expand **Stored Procedures**, right-click the procedure to grant permissions on, and then click **Properties**.  
  
4.  From **Stored Procedure Properties**, select the **Permissions** page.  
  
5.  To grant permissions to a user, database role, or application role, click **Search**.  
  
6.  In **Select Users or Roles**, click **Object Types** to add or clear the users and roles you want.  
  
7.  Click **Browse** to display the list of users or roles. Select the users or roles to whom permissions should be granted.  
  
8.  In the **Explicit Permissions** grid, select the permissions to grant to the specified user or role. For a description of the permissions, see [Permissions &#40;Database Engine&#41;](../security/permissions-database-engine.md).  
  
 Selecting **Grant** indicates the grantee will be given the specified permission. Selecting **Grant With** indicates that the grantee will also be able to grant the specified permission to other principals.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To grant permissions on a stored procedure  
  
1.  Connect to the [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example grants `EXECUTE` permission on the stored procedure `HumanResources.uspUpdateEmployeeHireInfo` to an application role named `Recruiting11`.  
  
```tsql  
USE AdventureWorks2012;   
GRANT EXECUTE ON OBJECT::HumanResources.uspUpdateEmployeeHireInfo  
    TO Recruiting11;  
GO  
```  
  
## See Also  
 [sys.fn_builtin_permissions &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql)   
 [GRANT Object Permissions &#40;Transact-SQL&#41;](/sql/t-sql/statements/grant-object-permissions-transact-sql)   
 [Create a Stored Procedure](../stored-procedures/create-a-stored-procedure.md)   
 [Modify a Stored Procedure](modify-a-stored-procedure.md)   
 [Delete a Stored Procedure](../stored-procedures/delete-a-stored-procedure.md)   
 [Rename a Stored Procedure](rename-a-stored-procedure.md)  
  
  
