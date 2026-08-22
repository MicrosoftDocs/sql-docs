---
title: "Delete or Disable DML Triggers"
description: "Delete or Disable DML Triggers"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.topic: language-reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "DML triggers, disabling"
  - "removing DML triggers"
  - "disabling DML triggers"
  - "dropping DML triggers"
  - "deleting DML triggers"
  - "DML triggers, removing"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Delete or Disable DML Triggers
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]
  This topic describes how to delete or disable a DML trigger in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  

<a id="BeforeYouBegin"></a>

##  <a name="Recommendations"></a> Recommendations
  
-   When a trigger is deleted, it is dropped from the current database. The table and the data upon which it is based are not affected. Deleting a table automatically deletes any triggers on the table.  
  
-   A trigger is enabled by default when it is created.  
  
-   Disabling a trigger does not drop it. The trigger still exists as an object in the current database. However, the trigger will not fire when any INSERT, UPDATE, or DELETE statement on which it was programmed is executed. Triggers that are disabled can be reenabled. Enabling a trigger does not re-create it. The trigger fires in the same manner as when it was originally created.  
  
  
<a id="Security"></a>
<a id="Permissions"></a>

## Permissions

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
  
3.  Copy and paste the following examples into the query window. Execute the [CREATE TRIGGER](../../t-sql/statements/create-trigger-transact-sql.md) statement to create the `Sales.bonus_reminder` trigger. To delete the trigger, execute the [DROP TRIGGER](../../t-sql/statements/drop-trigger-transact-sql.md) statement.  
  
```sql  
--Create the trigger.  
USE AdventureWorks2022;  
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
USE AdventureWorks2022;  
GO  
IF OBJECT_ID ('Sales.bonus_reminder', 'TR') IS NOT NULL  
   DROP TRIGGER Sales.bonus_reminder;  
GO  
  
```  
  
#### To disable and enable a DML trigger  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following examples into the query window. Execute the [CREATE TRIGGER](../../t-sql/statements/create-trigger-transact-sql.md) statement to create the `Sales.bonus_reminder` trigger. To disable and enable the trigger, execute the [DISABLE TRIGGER](../../t-sql/statements/disable-trigger-transact-sql.md) and [ENABLE TRIGGER](../../t-sql/statements/enable-trigger-transact-sql.md) statements, respectively.  
  
```sql  
--Create the trigger.  
USE AdventureWorks2022;  
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
USE AdventureWorks2022;  
GO  
DISABLE TRIGGER Sales.bonus_reminder ON Sales.SalesPersonQuotaHistory;  
GO  
  
```  
  
```sql  
--Enable the trigger.  
USE AdventureWorks2022;  
GO  
ENABLE TRIGGER Sales.bonus_reminder ON Sales.SalesPersonQuotaHistory;  
GO  
```  
  
## Related content

- [ALTER TRIGGER (Transact-SQL)](../../t-sql/statements/alter-trigger-transact-sql.md)
- [CREATE TRIGGER (Transact-SQL)](../../t-sql/statements/create-trigger-transact-sql.md)
- [DROP TRIGGER (Transact-SQL)](../../t-sql/statements/drop-trigger-transact-sql.md)
- [ENABLE TRIGGER (Transact-SQL)](../../t-sql/statements/enable-trigger-transact-sql.md)
- [DISABLE TRIGGER (Transact-SQL)](../../t-sql/statements/disable-trigger-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../../t-sql/functions/eventdata-transact-sql.md)
- [Get Information About DML Triggers](get-information-about-dml-triggers.md)
- [sys.sp_help (Transact-SQL)](../system-stored-procedures/sp-help-transact-sql.md)
- [sys.sp_helptrigger (Transact-SQL)](../system-stored-procedures/sp-helptrigger-transact-sql.md)
- [sys.triggers (Transact-SQL)](../system-catalog-views/sys-triggers-transact-sql.md)
- [sys.trigger_events (Transact-SQL)](../system-catalog-views/sys-trigger-events-transact-sql.md)
- [sys.sql_modules (Transact-SQL)](../system-catalog-views/sys-sql-modules-transact-sql.md)
- [sys.assembly_modules (Transact-SQL)](../system-catalog-views/sys-assembly-modules-transact-sql.md)
- [sys.server_triggers (Transact-SQL)](../system-catalog-views/sys-server-triggers-transact-sql.md)
- [sys.server_trigger_events (Transact-SQL)](../system-catalog-views/sys-server-trigger-events-transact-sql.md)
- [sys.server_sql_modules (Transact-SQL)](../system-catalog-views/sys-server-sql-modules-transact-sql.md)
- [sys.server_assembly_modules (Transact-SQL)](../system-catalog-views/sys-server-assembly-modules-transact-sql.md)
