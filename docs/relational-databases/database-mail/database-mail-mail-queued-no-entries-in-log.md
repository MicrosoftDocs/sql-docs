---
title: "Database mail queued, no entries in sysmail_event_log or Windows Application Event Log| Microsoft Docs"
ms.custom: ""
ms.date: "04/22/2019"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "architecture [SQL Server], Database Mail"
  - "Database Mail [SQL Server], architecture"
  - "Database Mail [SQL Server], components"
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Database mail queued, no entries in sysmail_event_log or Windows Application Event Log 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This topic describes how to troubleshoot a problem where e-mail messages are successfully queued, but no activity from the external program appears in the [sysmail_event_log](../system-catalog-views/sysmail-event-log-transact-sql.md) view or the Windows Application Event Log.

Database Mail relies on Service Broker for queuing e-mail messages. If Database Mail is stopped or if Service Broker message delivery is not activated in the **msdb** database, Database Mail queues messages database but cannot deliver the messages. In this case, the Service Broker messages remain in the Service Broker Mail queue. Service Broker does not activate the external program, so there are no log entries in **sysmail_event_log** and no updates to the item status in **sysmail_allitems** and the related views.

Execute the following statement to check whether Database Mail is enabled:

```sql
SELECT is_broker_enabled FROM sys.databases WHERE name = 'msdb';
```

A value of 0 indicates that Service Broker message delivery is not activated in the msdb database. To correct the problem, activate Service Broker in the database with the following Transact-SQL command:

```sql
USE master ;
GO

ALTER DATABASE [DatabaseName] SET ENABLE_BROKER ;
GO
``` 

Database Mail relies on a number of internal stored procedures. To reduce the surface area, these stored procedures are disabled on new installation of SQL Server. To enable these stored procedures, use the [Database Mail XPs option](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md) of the **sp_configure** system stored procedure.

Database Mail may be stopped in the **msdb** database. To check status of Database Mail, execute the following statement:

```sql
EXECUTE dbo.sysmail_help_status_sp;
```

To start Database Mail in a mail host database, run the following command in the msdb database:

```sql
EXECUTE dbo.sysmail_start_sp;
```

Service Broker examines the dialog lifetime for messages when it is activated; therefore, any messages that have been in the Service Broker transmission queue longer than the configured dialog lifetime immediately fails. Database Mail updates the status of failed messages in the [sysmail_allitems](../system-catalog-views/sysmail-allitems-transact-sql.md) and related views. You must decide whether to send the e-mail messages again. For more information about configuring the dialog lifetime that Database Mail uses, see [sysmail_configure_sp (Transact-SQL)](../system-stored-procedures/sysmail-configure-sp-transact-sql.md).

##  <a name="RelatedContent"></a> Related content 
  
-  [Database mail overview](database-mail.md)

  
  
