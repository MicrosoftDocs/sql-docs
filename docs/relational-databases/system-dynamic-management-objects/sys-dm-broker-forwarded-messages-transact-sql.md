---
title: "sys.dm_broker_forwarded_messages (Transact-SQL)"
description: sys.dm_broker_forwarded_messages returns a row for each Service Broker message that an instance of SQL Server is in the process of forwarding.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/07/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.dm_broker_forwarded_messages"
  - "dm_broker_forwarded_messages"
  - "sys.dm_broker_forwarded_messages_TSQL"
  - "dm_broker_forwarded_messages_TSQL"
helpviewer_keywords:
  - "sys.dm_broker_forwarded_messages dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || =azuresqldb-mi-current"
---
# sys.dm_broker_forwarded_messages (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a row for each Service Broker message that an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is in the process of forwarding.

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `conversation_id` | **uniqueidentifier** | Yes | ID of the conversation to which this message belongs. |
| `is_initiator` | **bit** | Yes | Indicates whether this message is from the initiator of the conversation.<br /><br />`0` = Not from initiator<br />`1` = From initiator |
| `to_service_name` | **nvarchar(256)** | Yes | Name of the service to which this message is sent. |
| `to_broker_instance` | **nvarchar(256)** | Yes | Identifier of the broker that hosts the service to which this message is sent. |
| `from_service_name` | **nvarchar(256)** | Yes | Name of the service that this message is from. |
| `from_broker_instance` | **nvarchar(256)** | Yes | Identifier of the broker that hosts the service that this message is from. |
| `adjacent_broker_address` | **nvarchar(256)** | Yes | Network address to which this message is being sent. |
| `message_sequence_number` | **bigint** | Yes | Sequence number of the message in the dialog box. |
| `message_fragment_number` | **int** | Yes | If the dialog message is fragmented, this is the fragment number that this transport message contains. |
| `hops_remaining` | **tinyint** | Yes | Number of times the message might be retransmitted before reaching the final destination. Every time the message is forwarded, this number decreases by 1. |
| `time_to_live` | **int** | Yes | Maximum time for the message to remain active. When this reaches 0, the message is discarded. |
| `time_consumed` | **int** | No | Time that the message was already active. Every time the message is forwarded, this number is increased by the time it takes to forward the message. |
| `message_id` | **uniqueidentifier** | Yes | ID of the message. |

## Permissions

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions require `VIEW SERVER STATE` permission on the server.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Related content

- [System dynamic management views](system-dynamic-management-objects.md)
- [Service Broker related dynamic management views (Transact-SQL)](service-broker-related-dynamic-management-views-transact-sql.md)
