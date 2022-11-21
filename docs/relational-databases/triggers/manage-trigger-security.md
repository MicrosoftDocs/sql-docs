---
description: "Manage Trigger Security"
title: "Manage Trigger Security | Microsoft Docs"
ms.custom: ""
ms.date: "06/22/2020"
ms.service: sql
ms.reviewer: ""
ms.subservice:
ms.topic: conceptual
helpviewer_keywords: 
  - "triggers [SQL Server], security"
ms.assetid: e94720a8-a3a2-4364-b0a3-bbe86e3ce4d5
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Manage Trigger Security

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

By default, both DML and DDL triggers execute under the context of the user that calls the trigger. The caller of a trigger is the user that executes the statement that causes the trigger to run. For example, if user **Mary** executes a DELETE statement that causes DML trigger **DML_trigMary** to run, the code inside **DML_trigMary** executes in the context of the user privileges for **Mary**. This default behavior can be exploited by users who want to introduce malicious code in the database or server instance. For example, the following DDL trigger is created by user **JohnDoe**:  

```sql
CREATE TRIGGER DDL_trigJohnDoe
ON DATABASE
FOR ALTER_TABLE
AS
SET NOCOUNT ON;

BEGIN TRY
  EXEC(N'
    USE [master];
    GRANT CONTROL SERVER TO [JohnDoe];
');
END TRY
BEGIN CATCH
  DECLARE @DoNothing INT;
END CATCH;
GO
```

What this trigger means is that as soon as a user that has permission to execute a `GRANT CONTROL SERVER` statement, such as a member of the **sysadmin** fixed server role, executes an `ALTER TABLE` statement, **JohnDoe** is granted `CONTROL SERVER` permission. In other words, although **JohnDoe** cannot grant `CONTROL SERVER` permission to himself, he enabled the trigger code that grants him this permission to execute under escalated privileges. Both DML and DDL triggers are open to this kind of security threat.  
  
## Trigger Security Best Practices  
 You can take the following measures to prevent trigger code from executing under escalated privileges:  
  
::: moniker range=">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"

-   Be aware of the DML and DDL triggers that exist in the database and on the server instance by querying the [sys.triggers](../../relational-databases/system-catalog-views/sys-triggers-transact-sql.md) and [sys.server_triggers](../../relational-databases/system-catalog-views/sys-server-triggers-transact-sql.md) catalog views. The following query returns all DML and database-level DDL triggers in the current database, and all server-level DDL triggers on the server instance:  
  
    ```sql
    SELECT type, name, parent_class_desc FROM sys.triggers
    UNION ALL
    SELECT type, name, parent_class_desc FROM sys.server_triggers;
    ```  

   > [!NOTE]
   > Only **sys.triggers** is available for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] unless you are using [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)].

::: moniker-end

::: moniker range="=azuresqldb-current"

-   Be aware of the DML and DDL triggers that exist in the database by querying the [sys.triggers](../../relational-databases/system-catalog-views/sys-triggers-transact-sql.md) catalog view. The following query returns all DML and database-level DDL triggers in the current database:  
  
    ```sql
    SELECT type, name, parent_class_desc FROM sys.triggers;
    ```  
  
::: moniker-end

-   Use [DISABLE TRIGGER](../../t-sql/statements/disable-trigger-transact-sql.md) to disable triggers that can harm the integrity of the database or server if the triggers execute under escalated privileges. The following statement disables all database-level DDL triggers in the current database:  
  
    ```sql
    DISABLE TRIGGER ALL ON DATABASE;
    ```  
  
     This statement disables all server-level DDL triggers on the server instance:  
  
    ```sql
    DISABLE TRIGGER ALL ON ALL SERVER;
    ```  
  
     This statement disables all DML triggers in the current database:  
  
    ```sql
    DECLARE @schema_name sysname, @trigger_name sysname, @object_name sysname;
    DECLARE @sql nvarchar(max);
    DECLARE trig_cur CURSOR FORWARD_ONLY READ_ONLY FOR
        SELECT SCHEMA_NAME(schema_id) AS schema_name,
            name AS trigger_name,
            OBJECT_NAME(parent_object_id) AS object_name
        FROM sys.objects WHERE type IN ('TR', 'TA');

    OPEN trig_cur;
    FETCH NEXT FROM trig_cur INTO @schema_name, @trigger_name, @object_name;
  
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @sql = N'DISABLE TRIGGER ' + QUOTENAME(@schema_name) + N'.'
            + QUOTENAME(@trigger_name)
            + N' ON ' + QUOTENAME(@schema_name) + N'.'
            + QUOTENAME(@object_name) + N'; ';
        EXEC (@sql);
        FETCH NEXT FROM trig_cur INTO @schema_name, @trigger_name, @object_name;
    END;
    GO

    -- Verify triggers are disabled. Should return an empty result set.
    SELECT * FROM sys.triggers WHERE is_disabled = 0;
    GO

    CLOSE trig_cur;
    DEALLOCATE trig_cur;
    ```  
  
## See Also  
 [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)   
 [DML Triggers](../../relational-databases/triggers/dml-triggers.md)   
 [DDL Triggers](../../relational-databases/triggers/ddl-triggers.md)  
 [Logon Triggers](../../relational-databases/triggers/logon-triggers.md)  
  
