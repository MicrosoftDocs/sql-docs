---
title: "CONNECTIONPROPERTY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CONNECTIONPROPERTY_TSQL"
  - "CONNECTIONPROPERTY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CONNECTIONPROPERTY statement"
ms.assetid: 6bd9ccae-af77-4a05-b97f-f8ab41cfde42
caps.latest.revision: 25
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CONNECTIONPROPERTY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns information about the connection properties for the unique connection that a request came in on.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CONNECTIONPROPERTY ( property )  
```  
  
## Arguments  
 *property*  
 Is the property of the connection. *property* can be one of the following values.  
  
|Value|Data type|Description|  
|-----------|---------------|-----------------|  
|net_transport|**nvarchar(40)**|Returns the physical transport protocol that is used by this connection. Is not nullable.<br /><br /> Return values are: **HTTP**, **Named pipe**, **Session**, **Shared memory**, **SSL**, **TCP**, and **VIA**.<br /><br /> Note: Always returns **Session** when a connection has multiple active result sets (MARS) enabled, and connection pooling is enabled.|  
|protocol_type|**nvarchar(40)**|Returns the protocol type of the payload. It currently distinguishes between TDS (TSQL) and SOAP. Is nullable.|  
|auth_scheme|**nvarchar(40)**|Returns the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication scheme for a connection. The authentication scheme is either Windows Authentication (NTLM, KERBEROS, DIGEST, BASIC, NEGOTIATE) or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. Is not nullable.|  
|local_net_address|**varchar(48)**|Returns the IP address on the server that this connection targeted. Available only for connections that are using the TCP transport provider. Is nullable.|  
|local_tcp_port|**int**|Returns the server TCP port that this connection targeted if the connection were a connection that is using the TCP transport. Is nullable.|  
|client_net_address|**varchar(48)**|Asks for the address of the client that is connecting to this server. Is nullable.|  
|physical_net_transport|**nvarchar(40)**|Returns the physical transport protocol that is used by this connection. Accurate when a connection has multiple active result sets (MARS) enabled.|  
|\<Any other string>||Returns NULL if the input is not valid.|  
  
## Remarks  
 **local_net_address** and **local_tcp_port** return NULL in [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 The values that are returned are the same as the options shown for the corresponding columns in the [sys.dm_exec_connections](../../relational-databases/system-dynamic-management-views/sys-dm-exec-connections-transact-sql.md) dynamic management view. For example:  
  
```  
SELECT   
ConnectionProperty('net_transport') AS 'Net transport',   
ConnectionProperty('protocol_type') AS 'Protocol type';  
```  
  
## See Also  
 [sys.dm_exec_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md)   
 [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)  
  
  