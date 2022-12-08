---
description: "Database mail: Mail queued, not delivered"
title: "Database mail: Mail queued, not delivered| Microsoft Docs"
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
# Database mail: Mail queued, not delivered 
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This topic describes how to troubleshoot a problem where e-mail messages are successfully queued, but the messages are not delivered.

## Diagnose the problem 

The Database Mail external program logs e-mail activity in the **msdb** database.

First, to confirm that Database Mail is enabled, use the [Database Mail XPs Option](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md) of the **sp_configure** system stored procedure, as in the following example:

```sql 
EXEC sp_configure 'show advanced', 1;  
RECONFIGURE; 
EXEC sp_configure; 
GO
```

Then, execute the following statement in the **msdb** database to check the status of the mail queue:

```sql
sysmail_help_queue_sp @queue_type = 'Mail' ;
```

For a detailed explanation of the columns, see [sysmail_help_queue_sp (Transact-SQL)](../system-stored-procedures/sysmail-help-queue-sp-transact-sql.md#result-set).

Check the **sysmail_event_log** view for activity. The view should contain an entry stating that the Database Mail external program has been started. If there is no entry in the **sysmail_event_log** view, see the symptom [Messages Queued, No Entries](database-mail-common-errors.md#database-mail-queued-no-entries-in-sysmail_event_log-or-windows-application-event-log) in **sysmail_event_log**. If there are errors in the **sysmail_event_log** view, troubleshoot the specific error.

If there are entries in the **sysmail_event_log** view, check the **sysmail_allitems** view for the status of the messages.

## Message status unsent 

A status of **unsent** indicates that the [Database Mail external program](database-mail-external-program.md) has not yet processed the e-mail message. The Database Mail external program may have fallen behind in processing messages; the rate at which the external program processes messages depends on network conditions, the retry time-out, the volume of messages, and the capacity of the SMTP server. If the problem persists, consider using more than one profile to distribute messages among more than one SMTP server.

Check the most recent modification date for successfully delivered messages. If the last successful delivery occurred some time ago, check the sysmail_event_log view to verify that the external program was successfully started by Service Broker. If the last attempt did not start the external program, verify that the Database Mail External Program is located in the correct directory, and that the service account for SQL Server has permission to run the executable.

   > [!NOTE]
   > To delete old unsent messages, wait until the undeliverable messages are the oldest messages in the queue, and then use [sysmail_delete_mailitems_sp](../system-stored-procedures/sysmail-delete-mailitems-sp-transact-sql.md) to delete them.

## Message status retrying

A status of retrying indicates that Database Mail has tried to deliver the message to the SMTP server but could not. Usually this is caused by a network interruption, a failure of the SMTP server, or an incorrectly configured Database Mail account. The message should eventually succeed or fail and post a message to the event log.

## Message status sent

A status of **sent** indicates that the Database Mail external program successfully delivered the e-mail message to the SMTP server. If the message did not arrive at the destination, the SMTP server accepted the message from Database Mail, but did not deliver the message to the final recipient. Check the logs of the SMTP server, or contact the administrator of the SMTP server. You can also test the SMTP mail server by using another client such as Outlook Express.

## Message status failed

A status of failed indicates that the Database Mail external program was unable to deliver the message to the SMTP server. In this case, the **sysmail_event_log** view contains the detailed information from the external program. For a sample query that joins **sysmail_faileditems** and **sysmail_event_log** to retrieve detailed error messages, see [Check the Status of E-Mail Messages Sent With Database Mail](check-the-status-of-e-mail-messages-sent-with-database-mail.md). The most common causes for failure are an incorrect destination address, or network problems that prevent the external program from reaching one or more of the failover accounts. Problems at the SMTP server can also cause that server to reject mail. By using the Database Mail Configuration Wizard, change the **Logging Level** to **Verbose** and send a test mail to investigate the point of failure.



##  <a name="RelatedContent"></a> See also
  
-  [Database mail overview](database-mail.md)

  
  
