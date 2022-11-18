---
title: "sysmail_unsentitems (Transact-SQL)"
description: sysmail_unsentitems (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_unsentitems_TSQL"
  - "sysmail_unsentitems"
helpviewer_keywords:
  - "sysmail_unsentitems database mail view"
dev_langs:
  - "TSQL"
ms.assetid: 993c12da-41e5-4e53-a188-0323feb70c67
---
# sysmail_unsentitems (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Contains one row for each Database Mail message with the **unsent** or **retrying** status. Messages with unsent or retrying status are still in the mail queue and may be sent at any time. Messages can have the **unsent** status for the following reasons:  
  
-   The message is new, and though the message has been placed on the mail queue, Database Mail is working on other messages and has not yet reached this message.  
  
-   The Database Mail external program is not running and no mail is being sent.  
  
 Messages can have the **retrying** status for the following reasons:  
  
-   Database Mail has attempted to send the mail, but the SMTP mail server could not be contacted. Database Mail will continue to attempt to send the message using other Database Mail accounts assigned to the profile that sent the message. If no accounts can send the mail, Database Mail will wait for the length of time configured for the **Account Retry Delay** parameter and then attempt to send the message again. Database Mail uses the **Account Retry Attempts** parameter to determine how many times to attempt to send the message. Messages retain **retrying** status as long as Database Mail is attempting to send the message.  
  
 Use this view when you want to see how many messages are waiting to be sent and how long they have been in the mail queue. Normally the number of **unsent** messages will be low. Conduct a benchmark test during normal operations to determine a reasonable number of messages in the message queue for your operations.  
  
 To see all messages processed by Database Mail, use [sysmail_allitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-allitems-transact-sql.md). To see only messages with the failed status, use [sysmail_faileditems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-faileditems-transact-sql.md). To see only messages that were sent, use [sysmail_sentitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-sentitems-transact-sql.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**mailitem_id**|**int**|Identifier of the mail item in the mail queue.|  
|**profile_id**|**int**|The identifier of the profile used to submit the message.|  
|**recipients**|**varchar(max)**|The e-mail addresses of the message recipients.|  
|**copy_recipients**|**varchar(max)**|The e-mail addresses of those who receive copies of the message.|  
|**blind_copy_recipients**|**varchar(max)**|The e-mail addresses of those who receive copies of the message but whose names do not appear in the message header.|  
|**subject**|**nvarchar(510)**|The subject line of the message.|  
|**body**|**varchar(max)**|The body of the message.|  
|**body_format**|**varchar(20)**|The body format of the message. The possible values are **TEXT** and **HTML**.|  
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
|**send_request_user**|**sysname**|The user who submitted the message. This is the user context of the database mail procedure, not the **From** field of the message.|  
|**sent_account_id**|**int**|The identifier of the Database Mail account used to send the message. Always NULL for this view.|  
|**sent_status**|**varchar(8)**|Will be **unsent** if Database Mail has not attempted to send the mail. Will be **retrying** if Database Mail failed to send the message but is trying again.|  
|**sent_date**|**datetime**|The date and time the Database Mail last attempted to send the mail. NULL if Database Mail has not attempted to send the message.|  
|**last_mod_date**|**datetime**|The date and time of the last modification of the row.|  
|**last_mod_user**|**sysname**|The user who last modified the row.|  
  
## Remarks  
 When troubleshooting Database Mail, this view may help you identify the nature of the problem, by showing you the number of messages waiting to be sent, and the amount of time the messages have waited. If no messages are being sent, the Database Mail external program may not be running, or there may be a network problem preventing Database Mail from contacting the SMTP servers. If many of the unsent messages have the same **profile_id**, there may be a problem with the SMTP server. Consider adding additional accounts to the profile. If messages are being sent, but messages are spending too much time in the queue, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may need more resources to process the volume of messages you require.  
  
## Permissions  
 Granted to **sysadmin** fixed server role and **DatabaseMailUserRole** database role. When executed by a member of the **sysadmin** fixed server role, this view shows all **unsent** or **retrying** messages. All other users only see the **unsent** or **retrying** messages that they submitted.  
  
  
