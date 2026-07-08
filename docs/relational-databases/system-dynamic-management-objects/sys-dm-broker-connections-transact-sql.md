---
title: "sys.dm_broker_connections (Transact-SQL)"
description: sys.dm_broker_connections returns a row for each Service Broker network connection.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/12/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.dm_broker_connections"
  - "dm_broker_connections"
  - "sys.dm_broker_connections_TSQL"
  - "dm_broker_connections_TSQL"
helpviewer_keywords:
  - "sys.dm_broker_connections dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || =azuresqldb-mi-current"
---
# sys.dm_broker_connections (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a row for each [!INCLUDE [ssSB](../../includes/sssb-md.md)] network connection. The following table provides more information:

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `connection_id` | **uniqueidentifier** | Yes | Identifier of the connection. |
| `transport_stream_id` | **uniqueidentifier** | Yes | Identifier of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Network Interface (SNI) connection used by this connection for TCP/IP communications. |
| `state` | **smallint** | Yes | Current state of the connection. Possible values:<br /><br />`1` = New<br />`2` = Connecting<br />`3` = Connected<br />`4` = Logged in<br />`5` = Closed |
| `state_desc` | **nvarchar(60)** | Yes | Current state of the connection. Possible values:<br /><br />`NEW`<br />`CONNECTING`<br />`CONNECTED`<br />`LOGGED_IN`<br />`CLOSED` |
| `connect_time` | **datetime** | Yes | Date and time at which the connection was opened. |
| `login_time` | **datetime** | Yes | Date and time at which login for the connection succeeded. |
| `authentication_method` | **nvarchar(128)** | Yes | Name of the Windows Authentication method, such as `NTLM` or `KERBEROS`. The value comes from Windows. |
| `principal_name` | **nvarchar(128)** | Yes | Name of the login that was validated for connection permissions. For Windows Authentication, this value is the remote user name. For certificate authentication, this value is the certificate owner. |
| `remote_user_name` | **nvarchar(128)** | Yes | Name of the peer user from the other database that is used by Windows Authentication. |
| `last_activity_time` | **datetime** | Yes | Date and time at which the connection was last used to send or receive information. |
| `is_accept` | **bit** | Yes | Indicates whether the connection originated on the remote side.<br /><br />`1` = The connection is a request accepted from the remote instance.<br /><br />`0` = The connection was started by the local instance. |
| `login_state` | **smallint** | Yes | State of the login process for this connection. For possible values, see [Login state values](#login-state-values). |
| `login_state_desc` | **nvarchar(60)** | Yes | Current state of login from the remote computer. For possible values, see [Login state values](#login-state-values). |
| `peer_certificate_id` | **int** | Yes | The local object ID of the certificate that is used by the remote instance for authentication. The owner of this certificate must have `CONNECT` permissions to the [!INCLUDE [ssSB](../../includes/sssb-md.md)] endpoint. |
| `encryption_algorithm` | **smallint** | Yes | Encryption algorithm that is used for this connection. For possible values, see [Encryption algorithm values](#encryption-algorithm-values). |
| `encryption_algorithm_desc` | **nvarchar(60)** | Yes | Textual representation of the encryption algorithm. For possible values, see the **encryption_algorithm_desc** column in [Encryption algorithm values](#encryption-algorithm-values). |
| `receives_posted` | **smallint** | Yes | Number of asynchronous network receives that aren't yet completed for this connection. |
| `is_receive_flow_controlled` | **bit** | Yes | Whether network receives are postponed due to flow control because the network is busy.<br /><br />`1` = True |
| `sends_posted` | **smallint** | Yes | The number of asynchronous network sends that aren't yet completed for this connection. |
| `is_send_flow_controlled` | **bit** | Yes | Whether network sends are postponed due to network flow control because the network is busy.<br /><br />`1` = True |
| `total_bytes_sent` | **bigint** | Yes | Total number of bytes sent by this connection. |
| `total_bytes_received` | **bigint** | Yes | Total number of bytes received by this connection. |
| `total_fragments_sent` | **bigint** | Yes | Total number of [!INCLUDE [ssSB](../../includes/sssb-md.md)] message fragments sent by this connection. |
| `total_fragments_received` | **bigint** | Yes | Total number of [!INCLUDE [ssSB](../../includes/sssb-md.md)] message fragments received by this connection. |
| `total_sends` | **bigint** | Yes | Total number of network send requests issued by this connection. |
| `total_receives` | **bigint** | Yes | Total number of network receive requests issued by this connection. |
| `peer_arbitration_id` | **uniqueidentifier** | Yes | Internal identifier for the endpoint. |
| `address` | **nvarchar(512)** | Yes | Peer address in the form of `TCP://peer_host:peer_port`. |
| `encryption_key_bit_length` | **int** | Yes | Length of the session encryption keys, in bits. Possible values are 128 or 256. |
| `encryption_protocol_version` | **nvarchar(32)** | Yes | When `encryption_algorithm_desc` is either "RC4" (deprecated) or "AES", the value is the negotiated UCS encryption protocol version number, from 1 to 4:<br /><br />`1` = SQL 2005/2008<br />`2` = SQL 2012<br />`3` = SQL 2012 with UCS Redirection Support<br />`4` = SQL 2016<br /><br />When `encryption_algorithm_desc` is "TLS" - the version of TLS (e.g. "1.2" or "1.3") |

[!INCLUDE [login-state-values](../../includes/login-state-values.md)]

[!INCLUDE [encryption-algorithm-values](../../includes/encryption-algorithm-values.md)]

## Permissions

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions require `VIEW SERVER STATE` permission on the server.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Physical joins

:::image type="content" source="media/join-dm-broker-connections.svg" alt-text="Diagram of physical joins for sys.dm_broker_connections.":::

## Relationship cardinalities

| From | To | Relationship |
| --- | --- | --- |
| `dm_broker_connections.connection_id` | `dm_exec_connections.connection_id` | One-to-one |

## Related content

- [System dynamic management views](system-dynamic-management-objects.md)
- [Service Broker related dynamic management views (Transact-SQL)](service-broker-related-dynamic-management-views-transact-sql.md)
