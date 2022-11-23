---
title: "GET_TRANSMISSION_STATUS (Transact-SQL)"
description: GET_TRANSMISSION_STATUS (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "07/26/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STATUS_TSQL"
  - "TRANSMISSION"
  - "TRANSMISSION_TSQL"
  - "GET_TRANSMISSION_STATUS"
  - "STATUS"
  - "GET_TRANSMISSION_STATUS_TSQL"
helpviewer_keywords:
  - "conversations [Service Broker], transmission status"
  - "Service Broker errors, transmission status"
  - "transmission status information"
  - "status information [SQL Server], conversations"
  - "GET_TRANSMISSION_STATUS statement"
dev_langs:
  - "TSQL"
---
# GET_TRANSMISSION_STATUS (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns the status for the last transmission for one side of a conversation.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
GET_TRANSMISSION_STATUS ( conversation_handle )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *conversation_id*  
 Is the conversation handle for the conversation. This parameter is of type **uniqueidentifier**.  
  
## Return Types  
 **nchar**  
  
## Remarks  
 Returns a string describing the status of the last transmission attempt for the specified conversation. Returns an empty string if the last transmission attempt succeeded, if no transmission attempt has yet been made, or if the *conversation_handle* does not exist.  
  
 The information returned by this function is the same information displayed in the last_transmission_error column of the management view sys.transmission_queue. However, this function can be used to find the transmission status for conversations that do not currently have messages in the transmission queue.  
  
> [!NOTE]  
>  GET_TRANSMISSION_STATUS does not provide information for messages that do not have a conversation endpoint in the current instance. That is, no information is available for messages to be forwarded.  
  
## Examples  
 The following example reports the transmission status for the conversation with the conversation handle `58ef1d2d-c405-42eb-a762-23ff320bddf0`.  
  
```sql  
SELECT Status =  
    GET_TRANSMISSION_STATUS('58ef1d2d-c405-42eb-a762-23ff320bddf0') ;  
```  
  
 Here is a sample result set, edited for line length:  
  
 ```
 Status  
 ------------------------------- 
 The Service Broker protocol transport is disabled or not configured.
 ```  
  
 In this case, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not configured to allow [!INCLUDE[ssSB](../../includes/sssb-md.md)] to communicate over the network.  
  
## See Also  
 [sys.conversation_endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-conversation-endpoints-transact-sql.md)   
 [sys.transmission_queue &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-transmission-queue-transact-sql.md)  
  
  
