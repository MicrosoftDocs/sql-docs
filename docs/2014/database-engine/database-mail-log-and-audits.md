---
title: "Database Mail Log and Audits | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "auditing [SQL Server]"
  - "Database Mail [SQL Server], auditing"
  - "logs [Database Mail]"
  - "audits [SQL Server], Database Mail"
  - "Database Mail [SQL Server], logging"
ms.assetid: 846589ee-5fe5-4ab3-b335-0c253e569f99
caps.latest.revision: 29
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Database Mail Log and Audits
  The Database Mail logging functionality is designed to provide a way to isolate and correct problems. Database Mail stores the log information in the **msdb** database. Information about Database Mail e-mail content, status of e-mails, and any messages received, such as errors  are logged by Database Mail and can be used for troubleshooting and auditing purposes.  
  
## Database Mail Logs  
 Tables in the **msdb** database log information from the [Database Mail External Program](../../2014/database-engine/database-mail-external-program.md). [Database Mail Views &#40;Transact-SQL&#41;](../Topic/Database%20Mail%20Views%20\(Transact-SQL\).md) expose the tables for troubleshooting purposes. Errors appear in the [sysmail_event_log &#40;Transact-SQL&#41;](../Topic/sysmail_event_log%20\(Transact-SQL\).md) view if Service Broker cannot activate the external program, if the external program encounters networking errors or if the Simple Mail Transport Protocol (SMTP) server refuses an e-mail message. In the event that the external program cannot log to the **msdb** tables, the program logs errors to the Windows application event log.  
  
 Internal tables in the **msdb** database contain the e-mail messages and attachments sent from Database Mail, together with the current status of each message. Database Mail updates these tables as each message is processed.  
  
## Database Mail Auditing tasks  
  
|||  
|-|-|  
|**Reviewing and managing Database Mail logs**|**Link to Topic**|  
|Check the delivery status of an individual message|[Check the Status of E-Mail Messages Sent With Database Mail](../../2014/database-engine/check-the-status-of-e-mail-messages-sent-with-database-mail.md)|  
|Clean up Database Mail messages, attachments, and log entries|[sysmail_delete_mailitems_sp &#40;Transact-SQL&#41;](../Topic/sysmail_delete_mailitems_sp%20\(Transact-SQL\).md)<br /><br /> [sysmail_delete_log_sp &#40;Transact-SQL&#41;](../Topic/sysmail_delete_log_sp%20\(Transact-SQL\).md)|  
|Archive the Database Email Messages and Logs|[Create a SQL Server Agent Job to Archive Database Mail Messages and Event Logs](../../2014/database-engine/create-a-sql-server-agent-job-to-archive-database-mail-messages-and-event-logs.md)|  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../2014/database-engine/monitor-resource-usage-system-monitor.md)  
  
  