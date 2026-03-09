---
title: MSSQLSERVER_17892
description: "MSSQLSERVER_17892"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: jopilov, mathoma
ms.date: 02/06/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "17892 (Database Engine error)"
---
# MSSQLSERVER_17892
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|17892|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|SRV_LOGON_FAILED_BY_TRIGGER|
|Message Text|Logon failed for login \<Login Name> due to trigger execution.|

## Explanation

Error 17892 is raised when a logon trigger code cannot execute successfully. [Logon Triggers](../triggers/logon-triggers.md) fire stored procedures in response to a LOGON event. This event is raised when a user session is established with an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The following error message is reported to the user:

> Msg 17892, Level 14, State 1, Server \<Server Name>, Line 1  
Logon failed for login \<Login Name> due to trigger execution.

## Possible causes

The problem could occur if there is an error when executing trigger code for that specific user account. Some of the scenarios include:

- The trigger tries to insert data into a table that does not exist.
- The login does not have permissions to the object that is referred to by the logon trigger.

## User action

You can use one of the following resolutions depending on your scenario:

- **Scenario 1**: You currently have access to an open session to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] under an admin account

  In this case, you can take the corrective action that is needed to fix your trigger code.

  - Example 1: If an object referred to by the trigger code does not exist, create that object so that the login trigger can execute successfully.

  - Example 2: If an object referred to by the trigger code does exist but users do not have permissions, grant them the necessary privileges to access the object.  
  
  Alternatively, you can just drop or disable the login trigger so that users can continue to log in to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

### Manage logon triggers
  
List all logon triggers on your server:
  
  ```sql
  SELECT name, is_disabled, create_date, modify_date
  FROM sys.server_triggers
  WHERE type_desc = 'LOGON';
  ```
  
Disable a logon trigger temporarily without deleting it:
  
  ```sql
  DISABLE TRIGGER trigger_name ON ALL SERVER;
  ```
  
Drop (delete) a logon trigger permanently:
  
  ```sql
  DROP TRIGGER trigger_name ON ALL SERVER;
  ```
  
  For more information, see [Manage trigger security](../triggers/manage-trigger-security.md#trigger-security-best-practices).

- **Scenario 2**: You do not have any current session that is open under admin privileges, but Dedicated Administrator Connection (DAC) is enabled on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

    In this case, you can use the DAC connection to take the same steps described in Scenario 1. Logon triggers don't affect DAC connections. For more information on DAC connection, see:
    [Diagnostic Connection for Database Administrators](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md).

    To check whether DAC is enabled, review the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log. Look for a message similar to this example:

    > 2020-02-09 16:17:44.150 Server Dedicated admin connection support was established for listening locally on port 1434.  

- **Scenario 3**: DAC isn't enabled on your server, and you don't have an existing admin session to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

    In this scenario, the only way to remediate the problem would be to take the following steps:
  
    1. Stop [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and related services.
    2. Start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the [command prompt](/previous-versions/sql/sql-server-2008-r2/ms180965(v=sql.105)) using the startup parameters `-c`, `-m`, and `-f`. This action disables the login trigger and lets you perform the same remedial measures described in Scenario 1.
  
        > [!NOTE]
        > This procedure requires a system administrator (SA) or equivalent administrator account.
  
         For more information about these and other startup options, see: [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md).

## More information

Logon triggers can also fail when using the `EVENTDATA` function incorrectly. The `EVENTDATA` function returns XML and is case-sensitive. For example, if you create the following logon trigger to block access based on IP address, you might encounter error 17892 if the XML path uses incorrect casing:

```sql
 CREATE TRIGGER tr_logon_CheckIP  
 ON ALL SERVER  
 FOR LOGON  
 AS
 BEGIN
  IF IS_SRVROLEMEMBER ( 'sysadmin' ) = 1  
     BEGIN
         DECLARE @IP NVARCHAR ( 15 );  
         SET @IP = ( SELECT EVENTDATA ().value ( '(/EVENT_INSTANCE/ClientHost)[1]' , 'NVARCHAR(15)' ));  
         IF NOT EXISTS( SELECT IP FROM DBAWork.dbo.ValidIP WHERE IP = @IP )  
         ROLLBACK ;  
     END ;  
 END ;  
 GO
```

If you don't maintain proper case-sensitivity when copying this script, specifically in this part of the trigger, the trigger fails:

```sql
-- Incorrect: lowercase 'event_instance' and 'clienthost' will cause EVENTDATA to return NULL
 SELECT EVENTDATA().value ( '(/event_instance/clienthost)[1]' , 'NVARCHAR(15)');
```

As a consequence, `EVENTDATA` always returns **NULL**, and all SA equivalent logins are denied access. In this case, if the DAC connection isn't enabled, you need to restart the server with the startup parameters described earlier to drop the trigger.

## Related content

- [Logon Triggers](../triggers/logon-triggers.md)
- [sys.server_triggers (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-triggers-transact-sql.md)
- [DISABLE TRIGGER (Transact-SQL)](../../t-sql/statements/disable-trigger-transact-sql.md)
- [DROP TRIGGER (Transact-SQL)](../../t-sql/statements/drop-trigger-transact-sql.md)
- [Manage trigger security](../triggers/manage-trigger-security.md)
