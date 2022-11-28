---
title: "dbo.sysalerts (Transact-SQL)"
description: dbo.sysalerts (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "10/24/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dbo.sysalerts"
  - "sysalerts_TSQL"
  - "dbo.sysalerts_TSQL"
  - "sysalerts"
helpviewer_keywords:
  - "sysalerts system table"
dev_langs:
  - "TSQL"
ms.assetid: a2c2f50d-61f3-4951-996a-add5ad092cc2
---
# dbo.sysalerts (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each alert. An alert is a message sent in response to an event. An alert can forward messages beyond the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environment, and an alert can be an e-mail or pager message. An alert also can generate a task.  This table is stored in the **msdb** database.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|Alert ID.|  
|**name**|**sysname**|Alert name.|  
|**event_source**|**nvarchar(100)**|Source of the event: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**event_category_id**|**int**|Reserved for future use.|  
|**event_id**|**int**|Reserved for future use.|  
|**message_id**|**int**|User-defined message ID or reference to **sysmessages** message that triggers this alert.|  
|**severity**|**int**|Severity that triggers this alert.|  
|**enabled**|**tinyint**|Status of the alert:<br /><br /> **0** = Disabled.<br /><br /> **1** = Enabled.|  
|**delay_between_responses**|**int**|Wait period, in seconds, between notifications for this alert.|  
|**last_occurrence_date**|**int**|Last occurrence (date) of the alert.|  
|**last_occurrence_time**|**int**|Last occurrence (time of day) of the alert.|  
|**last_response_date**|**int**|Last notification (date) of the alert.|  
|**last_response_time**|**int**|Last notification (time of day) of the alert.|  
|**notification_message**|**nvarchar(512)**|Additional information sent with the alert.|  
|**include_event_description**|**tinyint**|Bitmask representing whether the event description is sent by E-mail, Pager, or Net send. See chart below for values.|  
|**database_name**|**nvarchar(512)**|Database in which this alert must occur to trigger this alert.|  
|**event_description_keyword**|**nvarchar(100)**|Pattern the error must match in order for the alert to trigger.|  
|**occurrence_count**|**int**|Number of occurrences for this alert.|  
|**count_reset_date**|**int**|Day (date) count will be reset to **0**.|  
|**count_reset_time**|**int**|Time of day count will be reset to **0**.|  
|**job_id**|**uniqueidentifier**|ID of the task executed when this alert occurs.|  
|**has_notification**|**int**|Number of operators who receive e-mail notification when alert occurs.|  
|**flags**|**int**|Reserved.|  
|**performance_condition**|**nvarchar(512)**|Reserved.|  
|**category_id**|**int**|Reserved.|  
  
 ## Remarks

The following table shows the values for the include_event_description bitmask. The decimal value is returned by dbo.sysalerts. 

|decimal | binary | meaning |
|------|------|------|
|0 |0000 |no message |
|1 |0001 |email |
|2 |0010 |pager |
|3 |0011 |pager and email |
|4 |0100 |Net send |
|5 |0101 |Net send and email |
|6 |0110 |Net send and pager |
|7 |0111 |Net send, pager, and email |
  
