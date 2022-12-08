---
title: "sys.dm_qn_subscriptions (Transact-SQL)"
description: Query Notifications - sys.dm_qn_subscriptions
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_qn_subscriptions"
  - "dm_qn_subscriptions_TSQL"
  - "sys.dm_qn_subscriptions"
  - "sys.dm_qn_subscriptions_TSQL"
helpviewer_keywords:
  - "sys.dm_qn_subscriptions dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: a3040ce6-f5af-48fc-8835-c418912f830c
---
# Query Notifications - sys.dm_qn_subscriptions
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns information about the active query notifications subscriptions in the server. You can use this view to check for active subscriptions in the server or a specified database, or to check for a specified server principal.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|ID of a subscription.|  
|**database_id**|**int**|ID of the database in which the notification query was executed. This database stores information related to this subscription.|  
|**sid**|**varbinary(85)**|Security ID of the server principal that created and owns this subscription.|  
|**object_id**|**int**|ID of the internal table that stores information about subscription parameters.|  
|**created**|**datetime**|Date and time that the subscription was created.|  
|**timeout**|**int**|Time-out for the subscription in seconds. The notification will be flagged to fire after this time has elapsed.<br /><br /> Note: The actual firing time may be greater than the specified time-out. However, if a change that invalidates the subscription occurs after the specified time-out, but before the subscription is fired, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ensures that firing occurs at the time that the change was made.|  
|**status**|**int**|Indicates the status of the subscription. See the table under remarks for the list of codes.|  
  
## Relationship Cardinalities  
  
|From|To|On|Type|  
|----------|--------|--------|----------|  
|**sys.dm_qn_subscriptions**|**sys.databases**|**database_id**|Many-to-one|  
|**sys.dm_qn_subscriptions**|**sys.internal_tables**|**object_id**|Many-to-one|  
  
## Remarks  
 The status code of 0 indicates an undefined status.  
  
 The following status codes indicate that a subscription fired because of a change:  
  
|Code|Minor status|Info|  
|----------|------------------|----------|  
|65798|Subscription fired because data changed|Subscription triggered by insert|  
|65799|Subscription fired because data changed|Delete|  
|65800|Subscription fired because data changed|Update|  
|65801|Subscription fired because data changed|Merge|  
|65802|Subscription fired because data changed|Truncate table|  
|66048|Subscription fired because timeout expired|Undefined info mode|  
|66315|Subscription fired because object changed|object or user was dropped|  
|66316|Subscription fired because object changed|object was altered|  
|66565|Subscription fired because database was detached or dropped|server or db restarted|  
|66571|Subscription fired because database was detached or dropped|object or user was dropped|  
|66572|Subscription fired because database was detached or dropped|object was altered|  
|67341|subscription was triggered because of lack od resources on the server|subscription was triggered because of lack od resources on the server|  
  
 The following status codes indicate that a subscription failed to be created:  
  
|Code|Minor status|Info|  
|----------|------------------|----------|  
|132609|Subscription creation failed because the statement is not supported|Query is too complex|  
|132610|Subscription creation failed because the statement is not supported|Invalid statement for subscription|  
|132611|Subscription creation failed because the statement is not supported|Invalid set options for subscription|  
|132612|Subscription creation failed because the statement is not supported|Invalid isolation level|  
|132622|Subscription creation failed because the statement is not supported|used internally|  
|132623|Subscription creation failed because the statement is not supported|over the template limit per table|  
  
 The following status codes are used internally and are classed as check kill and init modes:  
  
|Code|Minor status|Info|  
|----------|------------------|----------|  
|198656|Used internally: check kill and init modes|Undefined info mode|  
|198928|Subscription was destroyed|Subscription fired because db was attached|  
|198929|Subscription was destroyed|Subscription fired because user was dropped|  
|198930|Subscription was destroyed|Subscription was dropped because of a resubscription|  
|198931|Subscription was destroyed|subscription was killed|  
|199168|Subscription is active|Undefined info mode|  
|199424|Subscription initialized but not yet active|Undefined info mode|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on server.  
  
> [!NOTE]  
>  If the user does not have VIEW SERVER STATE permission, this view returns information about subscriptions owned by current user.  
  
## Examples  
  
### A. Return active query notification subscriptions for the current user  
 The following example returns the active query notification subscriptions of the current user. If the user has VIEW SERVER STATE permissions, all active subscriptions in the server are returned.  
  
```  
SELECT id, database_id, sid, object_id, created, timeout, status  
FROM sys.dm_qn_subscriptions;  
GO  
```  
  
### B. Returning active query notification subscriptions for a specified user  
 The following example returns the active query notification subscriptions subscribed by login `Ruth0`.  
  
```  
SELECT id, database_id, sid, object_id, created, timeout, status  
FROM sys.dm_qn_subscriptions  
WHERE sid = SUSER_SID('Ruth0');  
GO  
```  
  
### C. Returning internal table metadata for query notification subscriptions  
 The following example returns the internal table metadata for query notification subscriptions.  
  
```  
SELECT qn.id AS query_subscription_id  
    ,it.name AS internal_table_name  
    ,it.object_id AS internal_table_id  
FROM sys.internal_tables AS it  
JOIN sys.dm_qn_subscriptions AS qn ON it.object_id = qn.object_id  
WHERE it.internal_type_desc = 'QUERY_NOTIFICATION';  
GO  
```  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Query Notifications Related Dynamic Management Views &#40;Transact-SQL&#41;](./system-dynamic-management-views.md)   
 [KILL QUERY NOTIFICATION SUBSCRIPTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-query-notification-subscription-transact-sql.md)  
  
