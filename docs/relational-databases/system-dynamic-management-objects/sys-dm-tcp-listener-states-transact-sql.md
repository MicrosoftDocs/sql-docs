---
title: "sys.dm_tcp_listener_states (Transact-SQL)"
description: sys.dm_tcp_listener_states returns a row containing dynamic-state information for each TCP listener.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/03/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.dm_tcp_listener_states"
  - "dm_tcp_listener_states"
  - "sys.dm_tcp_listener_states_TSQL"
  - "dm_tcp_listener_states_TSQL"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], listeners"
  - "sys.dm_tcp_listener_states dynamic management view"
dev_langs:
  - TSQL
---
# sys.dm_tcp_listener_states (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a row containing dynamic-state information for each TCP listener.

> [!NOTE]  
> The availability group listener could listen to the same port as the listener of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. In this case, the listeners are listed separately, the same as for a Service Broker listener.

| Column name | Data type | Description |
| --- | --- | --- |
| `listener_id` | **int** | Listener's internal ID. Not nullable.<br /><br />Primary key. |
| `ip_address` | **nvarchar(48)** | The listener IP address that's online and currently being listening to. Can be either `IPv4` or `IPv6`. If a listener possesses both types of addresses, they're listed separately. An IPv4 wildcard is displayed as `0.0.0.0`. An IPv6 wildcard is displayed as `::`.<br /><br />Not nullable. |
| `is_ipv4` | **bit** | Type of IP address. One of:<br /><br />`1` = IPv4<br />`0` = IPv6 |
| `port` | **int** | The port number on which the listener is listening. Not nullable. |
| `type` | **tinyint** | Listener type, one of:<br /><br />`0` = [!INCLUDE [tsql](../../includes/tsql-md.md)]<br />`1` = Service Broker<br />`2` = Database mirroring<br /><br />Not nullable. |
| `type_desc` | **nvarchar(20)** | Description of the `type`, one of:<br /><br />`TSQL`<br />`SERVICE_BROKER`<br />`DATABASE_MIRRORING`<br /><br />Not nullable. |
| `state` | **tinyint** | State of the availability group listener, one of:<br /><br />`0` = Online. The listener is listening and processing requests.<br />`1` = Pending restart. the listener is offline, pending a restart.<br /><br />If the availability group listener is listening to the same port as the server instance, these two listeners always have the same state.<br /><br />Not nullable.<br /><br />**Note:** The values in this column come from the `TSD_listener` object. The column doesn't support an offline state because when the `TDS_listener` is offline, it can't be queried for state. |
| `state_desc` | **nvarchar(16)** | Description of `state`, one of:<br /><br />`ONLINE`<br />`PENDING_RESTART`<br /><br />Not nullable. |
| `start_time` | **datetime** | Timestamp indicating when the listener was started. Not nullable. |

## Permissions

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions require `VIEW SERVER STATE` permission on the server.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER SECURITY STATE` permission on the server.

## Related content

- [Querying the SQL Server System Catalog FAQ](../system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)
- [Always On Availability Groups Catalog Views (Transact-SQL)](../system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)
- [Always On Availability Groups Dynamic Management Views - Functions](always-on-availability-groups-dynamic-management-views-functions.md)
