---
title: "sys.dm_broker_connections (Transact-SQL) "
description: sys.dm_broker_connections returns a row for each Service Broker network connection.
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/03/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_broker_connections"
  - "dm_broker_connections"
  - "sys.dm_broker_connections_TSQL"
  - "dm_broker_connections_TSQL"
helpviewer_keywords:
  - "sys.dm_broker_connections dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_broker_connections (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for each [!INCLUDE[ssSB](../../includes/sssb-md.md)] network connection. The following table provides more information:  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**connection_id**|**uniqueidentifier**|Identifier of the connection. NULLABLE.|  
|**transport_stream_id**|**uniqueidentifier**|Identifier of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Network Interface (SNI) connection used by this connection for TCP/IP communications. NULLABLE.|  
|**state**|**smallint**|Current state of the connection. NULLABLE. Possible values:<br /><br /> 1 = NEW<br /><br /> 2 = CONNECTING<br /><br /> 3 = CONNECTED<br /><br /> 4 = LOGGED_IN<br /><br /> 5 = CLOSED|  
|**state_desc**|**nvarchar(60)**|Current state of the connection. NULLABLE. Possible values:<br /><br /> NEW<br /><br /> CONNECTING<br /><br /> CONNECTED<br /><br /> LOGGED_IN<br /><br /> CLOSED|  
|**connect_time**|**datetime**|Date and time at which the connection was opened. NULLABLE.|  
|**login_time**|**datetime**|Date and time at which login for the connection succeeded. NULLABLE.|  
|**authentication_method**|**nvarchar(128)**|Name of the Windows Authentication method, such as NTLM or KERBEROS. The value comes from Windows. NULLABLE.|  
|**principal_name**|**nvarchar(128)**|Name of the login that was validated for connection permissions. For Windows Authentication, this value is the remote user name. For certificate authentication, this value is the certificate owner. NULLABLE.|  
|**remote_user_name**|**nvarchar(128)**|Name of the peer user from the other database that is used by Windows Authentication. NULLABLE.|  
|**last_activity_time**|**datetime**|Date and time at which the connection was last used to send or receive information. NULLABLE.|  
|**is_accept**|**bit**|Indicates whether the connection originated on the remote side. NULLABLE.<br /><br /> 1 = The connection is a request accepted from the remote instance.<br /><br /> 0 = The connection was started by the local instance.|  
|**login_state**|**smallint**|State of the login process for this connection. Possible values:<br /><br /> 0 = INITIAL<br /><br /> 1 = WAIT LOGIN NEGOTIATE<br /><br /> 2 = ONE ISC<br /><br /> 3 = ONE ASC<br /><br /> 4 = TWO ISC<br /><br /> 5 = TWO ASC<br /><br /> 6 = WAIT ISC Confirm<br /><br /> 7 = WAIT ASC Confirm<br /><br /> 8 = WAIT REJECT<br /><br /> 9 = WAIT PRE-MASTER SECRET<br /><br /> 10 = WAIT VALIDATION<br /><br /> 11 = WAIT ARBITRATION<br /><br /> 12 = ONLINE<br /><br /> 13 = ERROR|  
|**login_state_desc**|**nvarchar(60)**|Current state of login from the remote computer. Possible values:<br /><br /> Connection handshake is initializing.<br /><br /> Connection handshake is waiting for Login Negotiate message.<br /><br /> Connection handshake has initialized and sent security context for authentication.<br /><br /> Connection handshake has received and accepted security context for authentication.<br /><br /> Connection handshake has initialized and sent security context for authentication. There is an optional mechanism available for authenticating the peers.<br /><br /> Connection handshake has received and sent accepted security context for authentication. There is an optional mechanism available for authenticating the peers.<br /><br /> Connection handshake is waiting for Initialize Security Context Confirmation message.<br /><br /> Connection handshake is waiting for Accept Security Context Confirmation message.<br /><br /> Connection handshake is waiting for SSPI rejection message for failed authentication.<br /><br /> Connection handshake is waiting for Pre-Master Secret message.<br /><br /> Connection handshake is waiting for Validation message.<br /><br /> Connection handshake is waiting for Arbitration message.<br /><br /> Connection handshake is complete and is online (ready) for message exchange.<br /><br /> Connection is in error.|  
|**peer_certificate_id**|**int**|The local object ID of the certificate that is used by the remote instance for authentication. The owner of this certificate must have CONNECT permissions to the [!INCLUDE[ssSB](../../includes/sssb-md.md)] endpoint. NULLABLE.|  
|**encryption_algorithm**|**smallint**|Encryption algorithm that is used for this connection. NULLABLE. Possible values:<br /><br /> **Value &#124; Description  &#124; Corresponding DDL option**<br /><br /> 0 &#124; none &#124; Disabled<br /><br /> 1 &#124; SIGNING ONLY<br /><br /> 2 &#124; AES , RC4 &#124; Required &#124; Required algorithm RC4}<br /><br /> 3 &#124; AES &#124;Required algorithm AES<br /><br /> **Note:** The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100. (Not recommended.) Use a newer algorithm such as one of the AES algorithms instead. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions, material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level.|  
|**encryption_algorithm_desc**|**nvarchar(60)**|Textual representation of the encryption algorithm. NULLABLE. Possible Values:<br /><br /> **Description  &#124; Corresponding DDL option**<br /><br /> NONE &#124; Disabled<br /><br /> RC4  &#124; {Required &#124; Required Algorithm RC4}<br /><br /> AES &#124; Required Algorithm AES<br /><br /> NONE, RC4  &#124; {Supported &#124; Supported Algorithm RC4}<br /><br /> NONE, AES &#124; Supported Algorithm RC4<br /><br /> RC4, AES  &#124; Required Algorithm RC4 AES<br /><br /> AES, RC4  &#124; Required Algorithm AES RC4<br /><br /> NONE, RC4, AES  &#124; Supported Algorithm RC4 AES<br /><br /> NONE, AES, RC4  &#124;  Supported Algorithm AES RC4|  
|**receives_posted**|**smallint**|Number of asynchronous network receives that have not yet completed for this connection. NULLABLE.|  
|**is_receive_flow_controlled**|**bit**|Whether network receives have been postponed due to flow control because the network is busy. NULLABLE.<br /><br /> 1 = True|  
|**sends_posted**|**smallint**|The number of asynchronous network sends that have not yet completed for this connection. NULLABLE.|  
|**is_send_flow_controlled**|**bit**|Whether network sends have been postponed due to network flow control because the network is busy. NULLABLE.<br /><br /> 1 = True|  
|**total_bytes_sent**|**bigint**|Total number of bytes that were sent by this connection. NULLABLE.|  
|**total_bytes_received**|**bigint**|Total number of bytes that were received by this connection. NULLABLE.|  
|**total_fragments_sent**|**bigint**|Total number of [!INCLUDE[ssSB](../../includes/sssb-md.md)] message fragments that were sent by this connection. NULLABLE.|  
|**total_fragments_received**|**bigint**|Total number of [!INCLUDE[ssSB](../../includes/sssb-md.md)] message fragments that were received by this connection. NULLABLE.|  
|**total_sends**|**bigint**|Total number of network send requests that were issued by this connection. NULLABLE.|  
|**total_receives**|**bigint**|Total number of network receive requests that were issued by this connection. NULLABLE.|  
|**peer_arbitration_id**|**uniqueidentifier**|Internal identifier for the endpoint. NULLABLE.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Physical joins  

:::image type="content" source="../../relational-databases/system-dynamic-management-views/media/join-dm-broker-connections-1.svg" alt-text="Diagram of physical joins for sys.dm_broker_connections.":::
  
## Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|`dm_broker_connections.connection_id`|`dm_exec_connections.connection_id`|One-to-one|  
  
## Next steps
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Service Broker Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/service-broker-related-dynamic-management-views-transact-sql.md)  
