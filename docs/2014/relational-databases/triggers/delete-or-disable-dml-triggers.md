---
title: "Delete or Disable DML Triggers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "DML triggers, disabling"
  - "removing DML triggers"
  - "disabling DML triggers"
  - "dropping DML triggers"
  - "deleting DML triggers"
  - "DML triggers, removing"
ms.assetid: 0f97f953-33c5-4b26-afeb-db2a26ce38b4
author: rothja
ms.author: jroth
manager: craigg
---
# Delete or Disable DML Triggers
  This topic describes how to delete or disable a DML trigger in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To delete or disable a DML trigger, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   When a trigger is deleted, it is dropped from the current database. The table and the data upon which it is based are not affected. Deleting a table automatically deletes any triggers on the table.  
  
-   A trigger is enabled by default when it is created.  
  
-   Disabling a trigger does not drop it. The trigger still exists as an object in the current database. However, the trigger will not fire when any INSERT, UPDATE, or DELETE statement on which it was programmed is executed. Triggers that are disabled can be reenabled. Enabling a trigger does not re-create it. The trigger fires in the same manner as when it was originally created.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 To delete a DML trigger requires ALTER permission on the table or view on which the trigger is defined.  
  
 To disable or enable a DML trigger, at a minimum, a user must have ALTER permission on the table or view on which the trigger was created.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To delete a DML trigger  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then expand that instance.  
  
2.  Expand the database that you want, expand **Tables**, and then expand the table that contains the trigger that you want to delete.  
  
3.  Expand **Triggers**, right-click the trigger to delete, and then click **Delete**.  
  
4.  In the **Delete Object** dialog box, verify the trigger to delete, and then click **OK**.  
  
#### To disable and enable a DML trigger  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then expand that instance.  
  
2.  Expand the database that you want, expand **Tables**, and then expand the table that contains the trigger that you want to disable.  
  
3.  Expand **Triggers**, right-click the trigger to disable, and then click **Disable**.  
  
4.  To enable the trigger, click **Enable**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To delete a DML trigger  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following examples into the query window. Execute the [CREATE TRIGGER](/sql/t-sql/statements/create-trigger-transact-sql) statement to create the `Sales.bonus_reminder` trigger. To delete the trigger, execute the [DROP TRIGGER](/sql/t-sql/statements/drop-trigger-transact-sql) statement.  
  
```sql  
--Create the trigger.  
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
--Delete the trigger.  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID ('Sales.bonus_reminder', 'TR') IS NOT NULL  
   DROP TRIGGER Sales.bonus_reminder;  
GO  
  
```  
  
#### To disable and enable a DML trigger  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following examples into the query window. Execute the [CREATE TRIGGER](/sql/t-sql/statements/create-trigger-transact-sql) statement to create the `Sales.bonus_reminder` trigger. To disable and enable the trigger, execute the [DISABLE TRIGGER](/sql/t-sql/statements/disable-trigger-transact-sql) and [ENABLE TRIGGER](/sql/t-sql/statements/enable-trigger-transact-sql) statements, respectively.  
  
```sql  
--Create the trigger.  
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
--Disable the trigger.  
USE AdventureWorks2012;  
GO  
DISABLE TRIGGER Sales.bonus_reminder ON Sales.SalesPersonQuotaHistory;  
GO  
  
```  
  
```sql  
--Enable the trigger.  
USE AdventureWorks2012;  
GO  
ENABLE TRIGGER Sales.bonus_reminder ON Sales.SalesPersonQuotaHistory;  
GO  
```  
  
## See Also  
 [ALTER TRIGGER &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-trigger-transact-sql)   
 [CREATE TRIGGER &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-trigger-transact-sql)   
 [DROP TRIGGER &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-trigger-transact-sql)   
 [ENABLE TRIGGER &#40;Transact-SQL&#41;](/sql/t-sql/statements/enable-trigger-transact-sql)   
 [DISABLE TRIGGER &#40;Transact-SQL&#41;](/sql/t-sql/statements/disable-trigger-transact-sql)   
 [EVENTDATA &#40;Transact-SQL&#41;](/sql/t-sql/functions/eventdata-transact-sql)   
 [Get Information About DML Triggers](dml-triggers.md)   
 [sp_help &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-help-transact-sql)   
 [sp_helptrigger &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-helptrigger-transact-sql)   
 [sys.triggers &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-triggers-transact-sql)   
 [sys.trigger_events &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-trigger-events-transact-sql)   
 [sys.sql_modules &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-sql-modules-transact-sql)   
 [sys.assembly_modules &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-assembly-modules-transact-sql)   
 [sys.server_triggers &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-server-triggers-transact-sql)   
 [sys.server_trigger_events &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-server-trigger-events-transact-sql)   
 [sys.server_sql_modules &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-server-sql-modules-transact-sql)   
 [sys.server_assembly_modules &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-server-assembly-modules-transact-sql)  
  
  
