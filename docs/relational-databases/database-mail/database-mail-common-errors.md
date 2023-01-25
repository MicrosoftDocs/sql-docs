---
description: "Common errors with database mail"
title: "Common errors with database mail| Microsoft Docs"
ms.custom: ""
ms.date: "04/22/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "architecture [SQL Server], Database Mail"
  - "Database Mail [SQL Server], architecture"
  - "Database Mail [SQL Server], components"
author: MashaMSFT
ms.author: mathoma
---
# Common errors with database mail 
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes some common errors encountered with database mail and their solutions.

## Could not find stored procedure 'sp_send_dbmail'
The [sp_send_dbmail](../system-stored-procedures/sp-send-dbmail-transact-sql.md) stored procedure is installed in the msdb database. You must either run **sp_send_dbmail** from the msdb database, or specify a three-part name for the stored procedure.

Example:
```sql
EXEC msdb.dbo.sp_send_dbmail ...
```

Or:

```sql
USE msdb;
GO
EXEC dbo.sp_send_dbmail ...
```

Use [Database Mail Configuration Wizard](configure-database-mail.md) to enable and configure database mail.

## Profile not valid
There are two possible causes for this message. Either the profile specified does not exist, or the user running [sp_send_dbmail (Transact-SQL)](../system-stored-procedures/sp-send-dbmail-transact-sql.md) does not have permission to access the profile.

To check permissions for a profile, run the stored procedure [sysmail_help_principalprofile_sp (Transact-SQL)](../system-stored-procedures/sysmail-help-principalprofile-sp-transact-sql.md) with name of the profile. Use the stored procedure [sysmail_add_principalprofile_sp (Transact-SQL)](../system-stored-procedures/sysmail-help-principalprofile-sp-transact-sql.md) or the [Database Mail Configuration Wizard](configure-database-mail.md) to grant permission for a msdb user or group to access a profile.

## Permission denied on sp_send_dbmail

This topic describes how to troubleshoot an error message stating that the user attempting to send Database Mail does not have permission to execute sp_send_dbmail.

The error text is:

```
EXECUTE permission denied on object 'sp_send_dbmail', 
database 'msdb', schema 'dbo'.
```

To send Database mail, users must be a user in the msdb database and a member of the DatabaseMailUserRole database role in the msdb database. To add msdb users or groups to this role use SQL Server Management Studio or execute the following statement for the user or role that needs to send Database Mail.

```sql
EXEC msdb.dbo.sp_addrolemember @rolename = 'DatabaseMailUserRole'
    ,@membername = '<user or role name>';
GO
```
For more information, see [sp_addrolemember](../system-stored-procedures/sp-addrolemember-transact-sql.md) and [sp_droprolemember](../system-stored-procedures/sp-droprolemember-transact-sql.md).

## Database mail queued, no entries in sysmail_event_log or Windows Application Event Log 

Database Mail relies on Service Broker for queuing e-mail messages. If Database Mail is stopped or if Service Broker message delivery is not activated in the **msdb** database, Database Mail queues messages in the database but cannot deliver the messages. In this case, the Service Broker messages remain in the Service Broker Mail queue. Service Broker does not activate the external program, so there are no log entries in **sysmail_event_log** and no updates to the item status in **sysmail_allitems** and the related views.

Execute the following statement to check whether Service Broker is enabled in the **msdb** database:

```sql
SELECT is_broker_enabled FROM sys.databases WHERE name = 'msdb';
```

A value of 0 indicates that Service Broker message delivery is not activated in the msdb database. To correct the problem, activate Service Broker in the database with the following Transact-SQL command:

```sql
USE master ;
GO

ALTER DATABASE msdb SET ENABLE_BROKER ;
GO
``` 

Database Mail relies on a number of internal stored procedures. To reduce the surface area, these stored procedures are disabled on new installation of SQL Server. To enable these stored procedures, use the [Database Mail XPs option](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md) of the **sp_configure** system stored procedure, as in the following example:

```sql
EXEC sp_configure 'show advanced options', 1;  
RECONFIGURE;
EXEC sp_configure 'Database Mail XPs', 1;  
RECONFIGURE  
GO  

Database Mail may be stopped in the **msdb** database. To check status of Database Mail, execute the following statement:

```sql
EXECUTE dbo.sysmail_help_status_sp;
```

To start Database Mail in a mail host database, run the following command in the msdb database:

```sql
EXECUTE dbo.sysmail_start_sp;
```

Service Broker examines the dialog lifetime for messages when it is activated; therefore, any messages that have been in the Service Broker transmission queue longer than the configured dialog lifetime immediately fails. Database Mail updates the status of failed messages in the [sysmail_allitems](../system-catalog-views/sysmail-allitems-transact-sql.md) and related views. You must decide whether to send the e-mail messages again. For more information about configuring the dialog lifetime that Database Mail uses, see [sysmail_configure_sp (Transact-SQL)](../system-stored-procedures/sysmail-configure-sp-transact-sql.md).



##  <a name="RelatedContent"></a> See also
  
-  [Database mail overview](database-mail.md)
-  [Create a database mail profile](create-a-database-mail-profile.md)
  
  
