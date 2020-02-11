---
title: "Tutorial: Ownership Chains and Context Switching | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "context switching [SQL Server], tutorials"
  - "ownership chains [SQL Server]"
ms.assetid: db5d4cc3-5fc5-4cf5-afc1-8d4edc1d512b
author: rothja
ms.author: jroth
manager: craigg
---
# Tutorial: Ownership Chains and Context Switching
  This tutorial uses a scenario to illustrate [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] security concepts involving ownership chains and user context switching.  
  
> [!NOTE]  
>  To run the code in this tutorial you must have both Mixed Mode security configured and the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database installed. For more information about Mixed Mode security, see [Choose an Authentication Mode](security/choose-an-authentication-mode.md).  
  
## Scenario  
 In this scenario, two users need accounts to access purchase order data stored in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database. The requirements are as follows:  
  
-   The first account (TestManagerUser) must be able to see all details in every purchase order.  
  
-   The second account (TestEmployeeUser) must be able to see the purchase order number, order date, shipping date, product ID numbers, and the ordered and received items per purchase order, by purchase order number, for items where partial shipments have been received.  
  
-   All other accounts must retain their current permissions.  
  
 To fulfill the requirements of this scenario, the example is broken into four parts that demonstrate the concepts of ownership chains and context switching:  
  
1.  Configuring the environment.  
  
2.  Creating a stored procedure to access data by purchase order.  
  
3.  Accessing the data through the stored procedure.  
  
4.  Resetting the environment.  
  
 Each code block in this example is explained in line. To copy the complete example, see [Complete Example](#CompleteExample) at the end of this tutorial.  
  
## 1. Configure the Environment  
 Use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] and the following code to open the `AdventureWorks2012` database, and use the `CURRENT_USER` [!INCLUDE[tsql](../includes/tsql-md.md)] statement to check that the dbo user is displayed as the context.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT CURRENT_USER AS 'Current User Name';  
GO  
```  
  
 For more information about the CURRENT_USER statement, see [CURRENT_USER &#40;Transact-SQL&#41;](/sql/t-sql/functions/current-user-transact-sql).  
  
 Use this code as the dbo user to create two users on the server and in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database.  
  
```  
CREATE LOGIN TestManagerUser   
    WITH PASSWORD = '340$Uuxwp7Mcxo7Khx';  
GO  
CREATE USER TestManagerUser   
   FOR LOGIN TestManagerUser  
   WITH DEFAULT_SCHEMA = Purchasing;  
GO   
  
CREATE LOGIN TestEmployeeUser  
    WITH PASSWORD = '340$Uuxwp7Mcxo7Khy';  
GO  
CREATE USER TestEmployeeUser   
   FOR LOGIN TestEmployeeUser;  
GO   
```  
  
 For more information about the CREATE USER statement, see [CREATE USER &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-user-transact-sql). For more information about the CREATE LOGIN statement, see [CREATE LOGIN &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-login-transact-sql).  
  
 Use the following code to change the ownership of the `Purchasing` schema to the `TestManagerUser` account. This allows that account to use all Data Manipulation Language (DML) statement access (such as `SELECT` and `INSERT` permissions) on the objects it contains. `TestManagerUser` is also granted the ability to create stored procedures.  
  
```  
/* Change owner of the Purchasing Schema to TestManagerUser */  
ALTER AUTHORIZATION   
   ON SCHEMA::Purchasing   
   TO TestManagerUser;  
GO  
  
GRANT CREATE PROCEDURE   
   TO TestManagerUser   
   WITH GRANT OPTION;  
GO  
```  
  
 For more information about the GRANT statement, see [GRANT &#40;Transact-SQL&#41;](/sql/t-sql/statements/grant-transact-sql). For more information about stored procedures, see [Stored Procedures &#40;Database Engine&#41;](stored-procedures/stored-procedures-database-engine.md). For a poster of all [!INCLUDE[ssDE](../includes/ssde-md.md)] permissions, see [https://go.microsoft.com/fwlink/?LinkId=229142](https://go.microsoft.com/fwlink/?LinkId=229142).  
  
## 2. Create a Stored Procedure to Access Data  
 To switch context within a database, use the EXECUTE AS statement. EXECUTE AS requires IMPERSONATE permissions.  
  
 Use the `EXECUTE AS` statement in the following code to change the context to `TestManagerUser` and create a stored procedure showing only the data required by `TestEmployeeUser`. To satisfy the requirements, the stored procedure accepts one variable for the purchase order number and does not display financial information, and the WHERE clause limits the results to partial shipments.  
  
```  
EXECUTE AS LOGIN = 'TestManagerUser'  
GO  
SELECT CURRENT_USER AS 'Current User Name';  
GO  
  
/* Note: The user that calls the EXECUTE AS statement must have IMPERSONATE permissions on the target principal */  
CREATE PROCEDURE usp_ShowWaitingItems @ProductID int  
AS  
BEGIN   
   SELECT a.PurchaseOrderID, a.OrderDate, a.ShipDate  
      , b.ProductID, b.OrderQty, b.ReceivedQty  
   FROM Purchasing.PurchaseOrderHeader a  
      INNER JOIN Purchasing.PurchaseOrderDetail b  
         ON a.PurchaseOrderID = b.PurchaseOrderID  
   WHERE b.OrderQty > b.ReceivedQty  
      AND @ProductID = b.ProductID  
   ORDER BY b.ProductID ASC  
END  
GO  
```  
  
 Currently `TestEmployeeUser` does not have access to any database objects. The following code (still in the `TestManagerUser` context) grants the user account the ability to query base-table information through the stored procedure.  
  
```  
GRANT EXECUTE  
   ON OBJECT::Purchasing.usp_ShowWaitingItems  
   TO TestEmployeeUser;  
GO  
```  
  
 The stored procedure is part of the `Purchasing` schema, even though no schema was explicitly specified, because `TestManagerUser` is assigned by default to the `Purchasing` schema. You can use system catalog information to locate objects, as shown in the following code.  
  
```  
SELECT a.name AS 'Schema'  
   , b.name AS 'Object Name'  
   , b.type AS 'Object Type'  
FROM sys.schemas a  
   INNER JOIN sys.objects b  
      ON a.schema_id = b.schema_id   
WHERE b.name = 'usp_ShowWaitingItems';  
GO  
```  
  
 With this section of the example completed, the code switches context back to dbo using the `REVERT` statement.  
  
```  
REVERT;  
GO  
```  
  
 For more information about the REVERT statement, see [REVERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/revert-transact-sql).  
  
## 3. Access Data Through the Stored Procedure  
 `TestEmployeeUser` has no permissions on the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database objects other than a login and the rights assigned to the public database role. The following code returns an error when `TestEmployeeUser` attempts to access base tables.  
  
```  
EXECUTE AS LOGIN = 'TestEmployeeUser'  
GO  
SELECT CURRENT_USER AS 'Current User Name';  
GO  
/* This won't work */  
SELECT *  
FROM Purchasing.PurchaseOrderHeader;  
GO  
SELECT *  
FROM Purchasing.PurchaseOrderDetail;  
GO  
```  
  
 Because the objects referenced by the stored procedure created in the last section are owned by `TestManagerUser` by virtue of the `Purchasing` schema ownership, `TestEmployeeUser` can access the base tables through the stored procedure. The following code, still using the `TestEmployeeUser` context, passes purchase order 952 as a parameter.  
  
```  
EXEC Purchasing.usp_ShowWaitingItems 952  
GO  
```  
  
## 4. Reset the Environment  
 The following code uses the `REVERT` command to return the context of the current account to `dbo`, and then resets the environment.  
  
```  
REVERT;  
GO  
ALTER AUTHORIZATION   
ON SCHEMA::Purchasing TO dbo;  
GO  
DROP PROCEDURE Purchasing.usp_ShowWaitingItems;  
GO  
DROP USER TestEmployeeUser;  
GO  
DROP USER TestManagerUser;  
GO  
DROP LOGIN TestEmployeeUser;  
GO  
DROP LOGIN TestManagerUser;  
GO  
```  
  
##  <a name="CompleteExample"></a> Complete Example  
 This section displays the complete example code.  
  
> [!NOTE]  
>  This code does not include the two expected errors that demonstrate the inability of `TestEmployeeUser` to select from base tables.  
  
```  
/*   
Script:       UserContextTutorial.sql  
Author:       Microsoft  
Last Updated: Books Online  
Conditions:   Execute as DBO or sysadmin in the AdventureWorks database  
Section 1:    Configure the Environment   
*/  
USE AdventureWorks2012;  
GO  
SELECT CURRENT_USER AS 'Current User Name';  
GO  
/* Create server and database users */  
CREATE LOGIN TestManagerUser   
    WITH PASSWORD = '340$Uuxwp7Mcxo7Khx';  
  
GO  
  
CREATE USER TestManagerUser   
   FOR LOGIN TestManagerUser  
   WITH DEFAULT_SCHEMA = Purchasing;  
GO   
  
CREATE LOGIN TestEmployeeUser  
    WITH PASSWORD = '340$Uuxwp7Mcxo7Khy';  
GO  
CREATE USER TestEmployeeUser   
   FOR LOGIN TestEmployeeUser;  
GO   
  
/* Change owner of the Purchasing Schema to TestManagerUser */  
ALTER AUTHORIZATION   
   ON SCHEMA::Purchasing   
   TO TestManagerUser;  
GO  
  
GRANT CREATE PROCEDURE   
   TO TestManagerUser   
   WITH GRANT OPTION;  
GO  
  
/*   
Section 2: Switch Context and Create Objects  
*/  
EXECUTE AS LOGIN = 'TestManagerUser';  
GO  
SELECT CURRENT_USER AS 'Current User Name';  
GO  
  
/* Note: The user that calls the EXECUTE AS statement must have IMPERSONATE permissions on the target principal */  
CREATE PROCEDURE usp_ShowWaitingItems @ProductID int  
AS  
BEGIN   
   SELECT a.PurchaseOrderID, a.OrderDate, a.ShipDate  
      , b.ProductID, b.OrderQty, b.ReceivedQty  
   FROM Purchasing.PurchaseOrderHeader AS a  
      INNER JOIN Purchasing.PurchaseOrderDetail AS b  
         ON a.PurchaseOrderID = b.PurchaseOrderID  
   WHERE b.OrderQty > b.ReceivedQty  
      AND @ProductID = b.ProductID  
   ORDER BY b.ProductID ASC  
END;  
GO  
  
/* Give the employee the ability to run the procedure */  
GRANT EXECUTE   
   ON OBJECT::Purchasing.usp_ShowWaitingItems  
   TO TestEmployeeUser;  
GO   
  
/* Notice that the stored procedure is located in the Purchasing   
schema. This also demonstrates system catalogs */  
SELECT a.name AS 'Schema'  
   , b.name AS 'Object Name'  
   , b.type AS 'Object Type'  
FROM sys.schemas AS a  
   INNER JOIN sys.objects AS b  
      ON a.schema_id = b.schema_id   
WHERE b.name = 'usp_ShowWaitingItems';  
GO  
  
/* Go back to being the dbo user */  
REVERT;  
GO  
  
/*  
Section 3: Switch Context and Observe Security   
*/  
EXECUTE AS LOGIN = 'TestEmployeeUser';  
GO  
SELECT CURRENT_USER AS 'Current User Name';  
GO  
EXEC Purchasing.usp_ShowWaitingItems 952;  
GO  
  
/*   
Section 4: Clean Up Example  
*/  
REVERT;  
GO  
ALTER AUTHORIZATION   
ON SCHEMA::Purchasing TO dbo;  
GO  
DROP PROCEDURE Purchasing.usp_ShowWaitingItems;  
GO  
DROP USER TestEmployeeUser;  
GO  
DROP USER TestManagerUser;  
GO  
DROP LOGIN TestEmployeeUser;  
GO  
DROP LOGIN TestManagerUser;  
GO  
```  
  
## See Also  
 [Security Center for SQL Server Database Engine and Azure SQL Database](security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)  
  
  
