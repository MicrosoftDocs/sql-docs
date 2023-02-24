---
title: "sysmail_mailattachments (Transact-SQL)"
description: sysmail_mailattachments (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_mailattachments_TSQL"
  - "sysmail_mailattachments"
helpviewer_keywords:
  - "sysmail_mailattachments database mail view"
dev_langs:
  - "TSQL"
ms.assetid: aee87059-a4c1-459a-a95c-641b4e3f0e73
---
# sysmail_mailattachments (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each attachment submitted to Database Mail. Use this view when you want information about Database Mail attachments. To review all e-mails processed by Database Mail use [sysmail_allitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-allitems-transact-sql.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**attachment_id**|**int**|Identifier of the attachment.|  
|**mailitem_id**|**int**|Identifier of the mail item that contained the attachment.|  
|**filename**|**nvarchar(520)**|The file name of the attachment. When **attach_query_result** is 1 and **query_attachment_filename** is NULL, Database Mail creates an arbitrary filename.|  
|**filesize**|**int**|The size of the attachment in bytes.|  
|**attachment**|**varbinary(max)**|The content of the attachment.|  
|**last_mod_date**|**datetime**|The date and time of the last modification of the row.|  
|**last_mod_user**|**sysname**|The user who last modified the row.|  
  
## Remarks  
 When troubleshooting Database Mail, use this view to see the properties of the attachments.  
  
 Attachments stored in the system tables can cause the **msdb** database to grow. Use **sysmail_delete_mailitems_sp** to delete mail items and their associated attachments. For more information, see [Create a SQL Server Agent Job to Archive Database Mail Messages and Event Logs](../../relational-databases/database-mail/create-a-sql-server-agent-job-to-archive-database-mail-messages-and-event-logs.md).  
  
## Permissions  
 Granted to the **sysadmin** fixed server role and the **DatabaseMailUserRole** database role. When executed by a member of the **sysadmin** fixed server role, this view shows all attachments. All other users only see the attachments for messages that they submitted.  
  
## See Also  
 [sysmail_allitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-allitems-transact-sql.md)   
 [sysmail_faileditems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-faileditems-transact-sql.md)   
 [sysmail_sentitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-sentitems-transact-sql.md)   
 [sysmail_unsentitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-unsentitems-transact-sql.md)   
 [sysmail_event_log &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-event-log-transact-sql.md)  
  
  
