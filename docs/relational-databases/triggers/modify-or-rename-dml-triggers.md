---
description: "Modify or Rename DML Triggers"
title: "Modify or Rename DML Triggers | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice:
ms.topic: conceptual
helpviewer_keywords: 
  - "renaming triggers"
  - "modifying triggers"
  - "DML triggers, modifying"
ms.assetid: c7317eec-c0e9-479e-a4a7-83b6b6c58d59
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Modify or Rename DML Triggers
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  This topic describes how to modify or rename a DML trigger in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To modify or rename a DML trigger, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   When you rename a trigger, the trigger must be in the current database, and the new name must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   We recommend you do not use the [sp_rename](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md) stored procedure to rename a trigger. Changing any part of an object name can break scripts and stored procedures. Renaming a trigger does not change the name of the corresponding object name in the definition column of the [sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md) catalog view. We recommend that you drop and re-create the trigger instead.  
  
-   If you change the name of an object referenced by a DML trigger, you must modify the trigger so that its text reflects the new name. Therefore, before you rename an object, display the dependencies of the object first to determine whether any triggers are affected by the proposed change.  
  
-   A DML trigger can also be modified to encrypt its definition.  
  
-   To view the dependencies of a trigger, you can use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or the following function and catalog views:  
  
    -   [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)  
  
    -   [sys.dm_sql_referenced_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referenced-entities-transact-sql.md)  
  
    -   [sys.dm_sql_referencing_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referencing-entities-transact-sql.md)  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 To alter a DML trigger requires ALTER permission on the table or view on which the trigger is defined.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To modify a DML trigger  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then expand that instance.  
  
2.  Expand the database that you want, expand **Tables**, and then expand the table that contains the trigger that you want to modify.  
  
3.  Expand **Triggers**, right-click the trigger to modify, and then click **Modify**.  
  
4.  Modify the trigger, and then click **Execute**.  
  
#### To rename a DML trigger  
  
1.  [Delete the trigger](../../relational-databases/triggers/delete-or-disable-dml-triggers.md) that you want to rename.  
  
2.  [Re-create the trigger](../../relational-databases/triggers/create-dml-triggers.md), specifying the new name.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To modify a trigger using ALTER TRIGGER  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following examples into the query. Execute the first example to create a DML trigger that prints a user-defined message to the client when a user tries to add or change data in the `SalesPersonQuotaHistory` table. Execute the [ALTER TRIGGER](../../t-sql/statements/alter-trigger-transact-sql.md) statement to modify the trigger to fire only on `INSERT` activities. This trigger is helpful because it reminds the user that updates or inserts rows into this table to also notify the `Compensation` department.  
  
```sql  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID(N'Sales.bonus_reminder', N'TR') IS NOT NULL  
    DROP TRIGGER Sales.bonus_reminder;  
GO  
CREATE TRIGGER Sales.bonus_reminder  
ON Sales.SalesPersonQuotaHistory  
WITH ENCRYPTION  
AFTER INSERT, UPDATE   
AS RAISERROR ('Notify Compensation', 16, 10);  
GO  
  
```  
  
```sql  
USE AdventureWorks2012;  
GO  
ALTER TRIGGER Sales.bonus_reminder  
ON Sales.SalesPersonQuotaHistory  
AFTER INSERT  
AS RAISERROR ('Notify Compensation', 16, 10);  
GO  
  
```  
  
#### To rename a trigger using DROP TRIGGER and ALTER TRIGGER  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example use the [DROP TRIGGER](../../t-sql/statements/drop-trigger-transact-sql.md) and [ALTER TRIGGER](../../t-sql/statements/alter-trigger-transact-sql.md) statements to rename the `Sales.bonus_reminder` trigger to `Sales.bonus_reminder_2`.  
  
```sql  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID(N'Sales.bonus_reminder', N'TR') IS NOT NULL  
    DROP TRIGGER Sales.bonus_reminder;  
GO  
CREATE TRIGGER Sales.bonus_reminder_2  
ON Sales.SalesPersonQuotaHistory  
WITH ENCRYPTION  
AFTER INSERT, UPDATE   
AS RAISERROR ('Notify Compensation', 16, 10);  
GO  
  
```  
  
## See Also  
 [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)   
 [DROP TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/drop-trigger-transact-sql.md)   
 [ENABLE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/enable-trigger-transact-sql.md)   
 [DISABLE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/disable-trigger-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md)   
 [ALTER TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-trigger-transact-sql.md)   
 [Get Information About DML Triggers](../../relational-databases/triggers/get-information-about-dml-triggers.md)   
 [sp_help &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)   
 [sp_helptrigger &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptrigger-transact-sql.md)   
 [sys.triggers &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-triggers-transact-sql.md)   
 [sys.trigger_events &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-trigger-events-transact-sql.md)   
 [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)   
 [sys.assembly_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-assembly-modules-transact-sql.md)   
 [sys.server_triggers &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-triggers-transact-sql.md)   
 [sys.server_trigger_events &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-trigger-events-transact-sql.md)   
 [sys.server_sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-sql-modules-transact-sql.md)   
 [sys.server_assembly_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-assembly-modules-transact-sql.md)  
  
  
