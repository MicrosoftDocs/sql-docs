---
title: "sysmail_sentitems (Transact-SQL)"
description: sysmail_sentitems (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_sentitems_TSQL"
  - "sysmail_sentitems"
helpviewer_keywords:
  - "sysmail_sentitems database mail view"
dev_langs:
  - "TSQL"
ms.assetid: 16eb2a44-cebb-4cec-93ac-e2498c39989f
---
# sysmail_sentitems (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Contains one row for each message sent by Database Mail. Use **sysmail_sentitems** when you want to see which messages were successfully sent.  
  
 To see all messages processed by Database Mail, use [sysmail_allitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-allitems-transact-sql.md). To see only messages with the failed status, use [sysmail_faileditems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-faileditems-transact-sql.md). To see only unsent or retrying messages, use [sysmail_unsentitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-unsentitems-transact-sql.md). To see e-mail attachments, use [sysmail_mailattachments &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-mailattachments-transact-sql.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**mailitem_id**|**int**|Identifier of the mail item in the mail queue.|  
|**profile_id**|**int**|The identifier of the profile used to send the message.|  
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
|**send_request_user**|**sysname**|The user who sent the message. This is the user context of the database mail procedure, not the From: field of the message.|  
|**sent_account_id**|**int**|The identifier of the Database Mail account used to send the message.|  
|**sent_status**|**varchar(8)**|The status of the mail. Always **sent** for this view.|  
|**sent_date**|**datetime**|The date and time that the message was sent.|  
|**last_mod_date**|**datetime**|The date and time of the last modification of the row.|  
|**last_mod_user**|**sysname**|The user who last modified the row.|  
  
## Remarks  
 When troubleshooting Database Mail, this view may help you identify the nature of the problem, by showing you the attributes of the messages that were successfully sent. Database Mail marks messages as sent when they are successfully submitted to an SMTP mail server. Normally e-mail is received in a few minutes, but the e-mail can be delayed because of problems with the SMTP server. Database Mail marks the message as sent when it is accepted by the SMTP mail server. E-mail errors that occur on the SMTP mail server, such as an undeliverable recipient e-mail address, are not returned to Database Mail. Those e-mails are recorded as sent, even though they are not delivered. Troubleshoot that type of error on the SMTP server. Also, the SMTP mail server may send an undeliverable message notification to the reply e-mail address for a Database Mail account.  
  
## Permissions  
 Granted to **sysadmin** fixed server role and **databasemailuserrole** database role. When executed by a member of the **sysadmin** fixed server role, this view shows all sent messages. All other users only see the messages that they sent.  
  
## See Also  
 [Database Mail Messaging Objects](../../relational-databases/database-mail/database-mail-messaging-objects.md)  
  
  
