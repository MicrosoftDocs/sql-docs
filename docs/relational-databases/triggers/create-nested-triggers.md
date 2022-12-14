---
description: "Create Nested Triggers"
title: "Create Nested Triggers | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice:
ms.topic: conceptual
helpviewer_keywords: 
  - "recursive DML triggers [SQL Server]"
  - "DML triggers, nested"
  - "triggers [SQL Server], nested"
  - "direct recursion [SQL Server]"
  - "triggers [SQL Server], recursive"
  - "DML triggers, recursive"
  - "RECURSIVE_TRIGGERS option"
  - "indirect recursion [SQL Server]"
  - "nested DML triggers"
ms.assetid: cd522dda-b4ab-41b8-82b0-02445bdba7af
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create Nested Triggers
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  Both DML and DDL triggers are nested when a trigger performs an action that initiates another trigger. These actions can initiate other triggers, and so on. DML and DDL triggers can be nested up to 32 levels. You can control whether AFTER triggers can be nested through the **nested triggers** server configuration option. INSTEAD OF triggers (only DML triggers can be INSTEAD OF triggers) can be nested regardless of this setting.  
  
> [!NOTE]  
>  Any reference to managed code from a [!INCLUDE[tsql](../../includes/tsql-md.md)] trigger counts as one level against the 32-level nesting limit. Methods invoked from within managed code do not count against this limit.  
  
 If nested triggers are allowed and a trigger in the chain starts an infinite loop, the nesting level is exceeded and the trigger terminates.  
  
 You can use nested triggers to perform useful housekeeping functions such as storing a backup copy of rows affected by a previous trigger. For example, you can create a trigger on `PurchaseOrderDetail` that saves a backup copy of the `PurchaseOrderDetail` rows that the `delcascadetrig` trigger deleted. With the `delcascadetrig` trigger in effect, deleting `PurchaseOrderID` 1965 from `PurchaseOrderHeader` deletes the corresponding row or rows from `PurchaseOrderDetail`. To save the data, you can create a DELETE trigger on `PurchaseOrderDetail` that saves the deleted data into another separately created table, `del_save`. For example:  
  
```  
CREATE TRIGGER Purchasing.savedel  
   ON Purchasing.PurchaseOrderDetail  
FOR DELETE  
AS  
   INSERT del_save
   SELECT * FROM deleted;  
```  
  
 We do not recommend using nested triggers in an order-dependent sequence. Use separate triggers to cascade data modifications.  
  
> [!NOTE]  
>  Because triggers execute within a transaction, a failure at any level of a set of nested triggers cancels the entire transaction, and all data modifications are rolled back. Include PRINT statements in your triggers so that you can determine where the failure has occurred.  
  
## Recursive Triggers  
 An AFTER trigger does not call itself recursively unless the RECURSIVE_TRIGGERS database option is set.  
  
 There are two types of recursion:  
  
-   Direct recursion  
  
     This recursion occurs when a trigger fires and performs an action that causes the same trigger to fire again. For example, an application updates table **T3**; this causes trigger **Trig3** to fire. **Trig3** updates table **T3** again; this causes trigger **Trig3** to fire again.  
  
     Direct recursion can also occur when the same trigger is called again, but after a trigger of a different type (AFTER or INSTEAD OF) is called. In other words, direct recursion of an INSTEAD OF trigger can occur when the same INSTEAD OF trigger is called for a second time, even if one or more AFTER triggers are called in between. Likewise, direct recursion of an AFTER trigger can occur when the same AFTER trigger is called for a second time, even if one or more INSTEAD OF triggers are called in between. For example, an application updates table **T4**. This update causes INSTEAD OF trigger **Trig4** to fire. **Trig4** updates table **T5**. This update causes AFTER trigger **Trig5** to fire. **Trig5** updates table **T4**, and this update causes INSTEAD OF trigger **Trig4** to fire again. This chain of events is considered direct recursion for **Trig4**.  
  
-   Indirect recursion  
  
     This recursion occurs when a trigger fires and performs an action that causes another trigger of the same type (AFTER or INSTEAD OF) to fire. This second trigger performs an action that causes the original trigger to fire again. In other words, indirect recursion can occur when an INSTEAD OF trigger is called for a second time, but not until another INSTEAD OF trigger is called in between. Likewise, indirect recursion can occur when an AFTER trigger is called for a second time, but not until another AFTER trigger is called in between. For example, an application updates table **T1**. This update causes AFTER trigger **Trig1** to fire. **Trig1** updates table **T2**, and this update causes AFTER trigger **Trig2** to fire. **Trig2** in turn updates table **T1** that causes AFTER trigger **Trig1** to fire again.  
  
 Only direct recursion of AFTER triggers is prevented when the RECURSIVE_TRIGGERS database option is set to OFF. To disable indirect recursion of AFTER triggers, also set the **nested triggers** server option to **0**.  
  
## Examples  
 The following example shows using recursive triggers to solve a self-referencing relationship (also known as transitive closure). For example, the table `emp_mgr` defines the following:  
  
-   An employee (`emp`) in a company.  
  
-   The manager for each employee (`mgr`).  
  
-   The total number of employees in the organizational tree reporting to each employee (`NoOfReports`).  
  
 A recursive UPDATE trigger can be used to keep the `NoOfReports` column up-to-date as new employee records are inserted. The INSERT trigger updates the `NoOfReports` column of the manager record, which recursively updates the `NoOfReports` column of other records up the management hierarchy.  
  
```  
USE AdventureWorks2012;  
GO  
-- Turn recursive triggers ON in the database.  
ALTER DATABASE AdventureWorks2012  
   SET RECURSIVE_TRIGGERS ON;  
GO  
CREATE TABLE dbo.emp_mgr (  
   emp char(30) PRIMARY KEY,  
    mgr char(30) NULL FOREIGN KEY REFERENCES emp_mgr(emp),  
    NoOfReports int DEFAULT 0  
);  
GO  
CREATE TRIGGER dbo.emp_mgrins ON dbo.emp_mgr  
FOR INSERT  
AS  
DECLARE @e char(30), @m char(30);  
DECLARE c1 CURSOR FOR  
   SELECT emp_mgr.emp  
   FROM   emp_mgr, inserted  
   WHERE emp_mgr.emp = inserted.mgr;  
  
OPEN c1;  
FETCH NEXT FROM c1 INTO @e;  
WHILE @@fetch_status = 0  
BEGIN  
   UPDATE dbo.emp_mgr  
   SET emp_mgr.NoOfReports = emp_mgr.NoOfReports + 1 -- Add 1 for newly  
   WHERE emp_mgr.emp = @e ;                           -- added employee.  
  
   FETCH NEXT FROM c1 INTO @e;  
END  
CLOSE c1;  
DEALLOCATE c1;  
GO  
-- This recursive UPDATE trigger works assuming:  
--   1. Only singleton updates on emp_mgr.  
--   2. No inserts in the middle of the org tree.  
CREATE TRIGGER dbo.emp_mgrupd ON dbo.emp_mgr FOR UPDATE  
AS  
IF UPDATE (mgr)  
BEGIN  
   UPDATE dbo.emp_mgr  
   SET emp_mgr.NoOfReports = emp_mgr.NoOfReports + 1 -- Increment mgr's  
   FROM inserted                            -- (no. of reports) by  
   WHERE emp_mgr.emp = inserted.mgr;         -- 1 for the new report.  
  
   UPDATE dbo.emp_mgr  
   SET emp_mgr.NoOfReports = emp_mgr.NoOfReports - 1 -- Decrement mgr's  
   FROM deleted                             -- (no. of reports) by 1  
   WHERE emp_mgr.emp = deleted.mgr;          -- for the new report.  
END  
GO  
-- Insert some test data rows.  
INSERT dbo.emp_mgr(emp, mgr) VALUES  
    ('Harry', NULL)  
    ,('Alice', 'Harry')  
    ,('Paul', 'Alice')  
    ,('Joe', 'Alice')  
    ,('Dave', 'Joe');  
GO  
SELECT emp,mgr,NoOfReports  
FROM dbo.emp_mgr;  
GO  
-- Change Dave's manager from Joe to Harry  
UPDATE dbo.emp_mgr SET mgr = 'Harry'  
WHERE emp = 'Dave';  
GO  
SELECT emp,mgr,NoOfReports FROM emp_mgr;  
  
GO  
```  
  
 Here are the results before the update.  
  
```  
emp                            mgr                           NoOfReports  
------------------------------ ----------------------------- -----------  
Alice                          Harry                          2  
Dave                           Joe                            0  
Harry                          NULL                           1  
Joe                            Alice                          1  
Paul                           Alice                          0  
```  
  
 Here are the results after the update.  
  
```  
emp                            mgr                           NoOfReports  
------------------------------ ----------------------------- -----------  
Alice                          Harry                          2  
Dave                           Harry                          0  
Harry                          NULL                           2  
Joe                            Alice                          0  
Paul                           Alice                          0  
```  
  
 **To set the nested triggers option**  
  
-   [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
 **To set the RECURSIVE_TRIGGERS database option**  
  
-   [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)  
  
## See Also  
 [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)   
 [Configure the nested triggers Server Configuration Option](../../database-engine/configure-windows/configure-the-nested-triggers-server-configuration-option.md)  
  
  
