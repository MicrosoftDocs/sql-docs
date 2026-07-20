---
title: "Database Mail Log and Audits"
description: "Learn more about Database Mail logs and audit capability."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 05/16/2025
ms.service: sql
ms.topic: concept-article
helpviewer_keywords:
  - "auditing [SQL Server]"
  - "Database Mail [SQL Server], auditing"
  - "logs [Database Mail]"
  - "audits [SQL Server], Database Mail"
  - "Database Mail [SQL Server], logging"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Database Mail log and audits

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

 The Database Mail logging functionality is designed to provide a way to isolate and correct problems. 

 Database Mail stores log information in the `msdb` database. Information about Database Mail e-mail content, status of e-mails, and any messages received, such as errors are logged by Database Mail and can be used for troubleshooting and auditing purposes.


## Database Mail logs

System tables in the `msdb` database collect information from the [Database Mail External Program](database-mail-external-program.md). Internal tables in the `msdb` database contain the e-mail messages and attachments sent from Database Mail, together with the current status of each message. Database Mail updates these tables as each message is processed.  

 - [Database Mail views](../system-catalog-views/database-mail-views-transact-sql.md) expose the tables for troubleshooting purposes. 
 
 - Errors appear in the [sysmail_event_log (Transact-SQL)](../system-catalog-views/sysmail-event-log-transact-sql.md) view if Service Broker cannot activate the external program, if the external program encounters networking errors or if the Simple Mail Transport Protocol (SMTP) server refuses an e-mail message. 

- When the external program cannot log to the `msdb` tables, the program logs errors to the Windows Application event log.  
 
## Database Mail auditing tasks

|Reviewing and managing Database Mail logs|Link|  
|-|-|  
| Check the delivery status of an individual message |[Check the Status of E-Mail Messages Sent With Database Mail](check-the-status-of-e-mail-messages-sent-with-database-mail.md)|  
| Clean up Database Mail messages, attachments, and log entries |[sysmail_delete_mailitems_sp (Transact-SQL)](../system-stored-procedures/sysmail-delete-mailitems-sp-transact-sql.md)<br /><br /> [sysmail_delete_log_sp (Transact-SQL)](../system-stored-procedures/sysmail-delete-log-sp-transact-sql.md)|  
| Archive the Database Email Messages and Logs |[Create a SQL Server Agent Job to Archive Database Mail Messages and Event Logs](create-a-sql-server-agent-job-to-archive-database-mail-messages-and-event-logs.md)|  

## Related content

- [Monitor Resource Usage (Performance Monitor)](../performance-monitor/monitor-resource-usage-system-monitor.md)
