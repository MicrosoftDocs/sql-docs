---
title: "Check the Status of E-Mail Messages Sent With Database Mail | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "e-mail [SQL Server], status information"
  - "mail [SQL Server], status information"
  - "Database Mail [SQL Server], message status"
  - "status information [Database Mail]"
ms.assetid: eb290f24-b52f-46bc-84eb-595afee6a5f3
author: stevestein
ms.author: sstein
manager: craigg
---
# Check the Status of E-Mail Messages Sent With Database Mail
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to check the status of the e-mail message sent using Database Mail  in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
-   **Before you begin:**  
  
-   **To view the status of the e-mail sent using Database Mail, using:**  [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 Database Mail keeps copies of outgoing e-mail messages and displays them in the **sysmail_allitems**, **sysmail_sentitems**, **sysmail_unsentitems**, **sysmail_faileditems** views of the **msdb** database. The Database Mail external program logs activity and displays the log through the Windows Application Event Log and the **sysmail_event_log** view in the **msdb** database. To check the status of an e-mail message, run a query against this view. E-mail messages have one of four possible statuses: **sent**, **unsent**, **retrying**, and **failed**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To view the status of the e-mail sent using Database Mail**  
  
1.  Select from the **sysmail_allitems** table, specifying the messages of interest by **mailitem_id** or **sent_status**.  
  
2.  To check the status returned from the external program for the e-mail messages, join **sysmail_allitems** to **sysmail_event_log** view on the **mailitem_id** column, as shown in the following section.  
  
     By default, the external program does not log information about messages that were successfully sent. To log all messages, set the logging level to verbose using the **Configure System Parameters** page of the **Database Mail Configuration Wizard.**  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 The following example lists information about any e-mail messages sent to `danw` that the external program could not send successfully. The statement lists the subject, the date and time that the external program failed to send the message, and the error message from the Database Mail log.  
  
```  
USE msdb ;  
GO  
  
-- Show the subject, the time that the mail item row was last  
-- modified, and the log information.  
-- Join sysmail_faileditems to sysmail_event_log   
-- on the mailitem_id column.  
-- In the WHERE clause list items where danw was in the recipients,  
-- copy_recipients, or blind_copy_recipients.  
-- These are the items that would have been sent  
-- to danw.  
  
SELECT items.subject,  
    items.last_mod_date  
    ,l.description FROM dbo.sysmail_faileditems as items  
INNER JOIN dbo.sysmail_event_log AS l  
    ON items.mailitem_id = l.mailitem_id  
WHERE items.recipients LIKE '%danw%'    
    OR items.copy_recipients LIKE '%danw%'   
    OR items.blind_copy_recipients LIKE '%danw%'  
GO  
```  
  
## See Also  
 [Database Mail Log and Audits](../../relational-databases/database-mail/database-mail-log-and-audits.md)  
  
  
