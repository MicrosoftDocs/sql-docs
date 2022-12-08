---
title: "CONNECTIONPROPERTY (Transact-SQL)"
description: "CONNECTIONPROPERTY (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CONNECTIONPROPERTY_TSQL"
  - "CONNECTIONPROPERTY"
helpviewer_keywords:
  - "CONNECTIONPROPERTY statement"
dev_langs:
  - "TSQL"
---
# CONNECTIONPROPERTY (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

For a request that comes in to the server, this function returns information about the connection properties of the unique connection which supports that request.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
CONNECTIONPROPERTY ( property )  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*property*  
The property of the connection. *property* can have one of these values:
  
|Value|Data type|Description|  
|---|---|---|
|net_transport|**nvarchar(40)**|Returns the physical transport protocol used by this connection. This value is not nullable. Possible return values:<br /><br /> **HTTP**<br /> **Named pipe**<br /> **Session**<br /> **Shared memory**<br /> **SSL**<br /> **TCP**<br /><br /> and<br /><br /> **VIA**<br /><br /> Note: Always returns **Session** when a connection has both multiple active result sets (MARS) enabled, and connection pooling enabled.|  
|protocol_type|**nvarchar(40)**|Returns the payload protocol type. It currently distinguishes between TDS (TSQL) and SOAP. Is nullable.|  
|auth_scheme|**nvarchar(40)**|Returns the connection [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication scheme. The authentication scheme is either Windows Authentication (NTLM, KERBEROS, DIGEST, BASIC, NEGOTIATE) or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. Is not nullable.|  
|local_net_address|**varchar(48)**|Returns the IP address on the server that this specific connection targeted. Available only for connections that use the TCP transport provider. Is nullable.|  
|local_tcp_port|**int**|Returns the server TCP port that this connection targeted, if the connection were a connection that uses the TCP transport. Is nullable.|  
|client_net_address|**varchar(48)**|Asks for the address of the client that tries to connect to this server. Is nullable.|  
|physical_net_transport|**nvarchar(40)**|Returns the physical transport protocol used by this connection. Accurate when a connection has multiple active result sets (MARS) enabled.|  
|\<Any other string>||Returns NULL for invalid input.|  
  
## Remarks  
**local_net_address** and **local_tcp_port** return NULL in [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].
  
The returned values match the options shown for the corresponding columns in the [sys.dm_exec_connections](../../relational-databases/system-dynamic-management-views/sys-dm-exec-connections-transact-sql.md) dynamic management view. For example:
  
```sql
SELECT   
ConnectionProperty('net_transport') AS 'Net transport',   
ConnectionProperty('protocol_type') AS 'Protocol type';  
```  
  
## See also
[sys.dm_exec_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md)  
[sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)
  
  
