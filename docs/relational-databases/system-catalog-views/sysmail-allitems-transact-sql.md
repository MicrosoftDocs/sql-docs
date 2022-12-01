---
title: "sysmail_allitems (Transact-SQL)"
description: sysmail_allitems (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_allitems_TSQL"
  - "sysmail_allitems"
helpviewer_keywords:
  - "sysmail_allitems database mail view"
dev_langs:
  - "TSQL"
ms.assetid: 21fb8432-7677-4435-902f-64a58bba4cbb
---
# sysmail_allitems (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Contains one row for each message processed by Database Mail. Use this view when you want to see the status of all messages.  
  
 To see only messages with the failed status, use [sysmail_faileditems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-faileditems-transact-sql.md). To see only unsent messages, use [sysmail_unsentitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-unsentitems-transact-sql.md). To see only messages that were sent, use [sysmail_sentitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-sentitems-transact-sql.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**mailitem_id**|**int**|Identifier of the mail item in the mail queue.|  
|**profile_id**|**int**|The identifier of the profile used to send the message.|  
|**recipients**|**varchar(max)**|The e-mail addresses of the message recipients.|  
|**copy_recipients**|**varchar(max)**|The e-mail addresses of those who receive copies of the message.|  
|**blind_copy_recipients**|**varchar(max)**|The e-mail addresses of those who receive copies of the message but whose names do not appear in the message header.|  
|**subject**|**nvarchar(510)**|The subject line of the message.|  
|**body**|**varchar(max)**|The body of the message.|  
|**body_format**|**varchar(20)**|The body format of the message. The possible values are TEXT and HTML.|  
|**importance**|**varchar(6)**|The **importance** parameter of the message.|  
|**sensitivity**|**varchar(12)**|The **sensitivity** parameter of the message.|  
|**file_attachments**|**varchar(max)**|A semicolon-delimited list of file names attached to the e-mail message.|  
|**attachment_encoding**|**varchar(20)**|The type of mail attachment.|  
|**query**|**varchar(max)**|The query executed by the mail program.|  
|**execute_query_database**|**sysname**|The database context within which the mail program executed the query.|  
|**attach_query_result_as_file**|**bit**|When the value is 0, the query results were included in the body of the e-mail message, after the contents of the body. When the value is 1, the results were returned as an attachment.|  
|**query_result_header**|**bit**|When the value is 1, query results contained column headers. When the value is 0, query results did not include column headers.|  
|**query_result_width**|**int**|The **query_result_width** parameter of the message.|  
|**query_result_separator**|**char(1)**|The character used to separate columns in the query output.|  
|**exclude_query_output**|**bit**|The **exclude_query_output** parameter of the message. For more information, see [sp_send_dbmail &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-send-dbmail-transact-sql.md).|  
|**append_query_error**|**bit**|The **append_query_error** parameter of the message. 0 indicates that Database Mail should not send the e-mail message if there is an error in the query.|  
|**send_request_date**|**datetime**|The date and time the message was placed on the mail queue.|  
|**send_request_user**|**sysname**|The user who submitted the message. This is the user context of the database mail procedure, not the From: field of the message.|  
|**sent_account_id**|**int**|The identifier of the Database Mail account used to send the message.|  
|**sent_status**|**varchar(8)**|The status of the mail. Possible values are:<br /><br /> **sent** - The mail was sent.<br /><br /> **unsent** - Database mail is still attempting to send the message.<br /><br /> **retrying** - Database Mail failed to send the message but is attempting to send it again.<br /><br /> **failed** - Database mail was unable to send the message.|  
|**sent_date**|**datetime**|The date and time that the message was sent.|  
|**last_mod_date**|**datetime**|The date and time of the last modification of the row.|  
|**last_mod_user**|**sysname**|The user who last modified the row.|  
  
## Remarks  
 Use the **sysmail_allitems** view to see the status of all messages processed by Database Mail. When troubleshooting Database Mail, this view may help you identify the nature of the problem, by showing you the attributes of the messages that were sent compared with the attributes of the messages that were not sent.  
  
 The system tables exposed by this view contain all messages and may cause the **msdb** database to grow. Delete old messages from the view periodically to reduce the size of the tables. For more information, see [Create a SQL Server Agent Job to Archive Database Mail Messages and Event Logs](../../relational-databases/database-mail/create-a-sql-server-agent-job-to-archive-database-mail-messages-and-event-logs.md).  
  
## Permissions  
 Granted to **sysadmin** fixed server role and **DatabaseMailUserRole** database role. When executed by a member of the **sysadmin** fixed server role, this view shows all messages. All other users only see the messages that they submitted.  
  
  
