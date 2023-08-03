---
title: Connection resiliency
description: Connection resiliency can transparently restore broken idle connections. This feature improves application behavior when the server closes idle connections.
author: David-Engel
ms.author: v-davidengel
ms.date: 01/30/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Connection resiliency (JDBC)

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Connection resiliency allows a broken idle connection to be reestablished, within limitations. If an initial connection fails, connection resiliency also allows the driver to automatically retry the connection. Only SQL Server 2014 and later and Azure SQL Database support reconnecting a broken idle connection. This feature is available starting with Microsoft JDBC Driver 10.2.0 for SQL Server.

## Connection retry

There are two aspects to connection resiliency. The first is the ability to transparently retry an initial database connection. The second is the ability to transparently restore an existing, idle connection. A typical idle connection might be a connection sitting in a connection pool. An "idle" connection is generally one that has been idle for at least 30 seconds. These connections often can be closed by the server or by network devices between the client and server.

The JDBC driver has two connection options that control connection resiliency behavior. These options can be added to the connection string or set via data source properties.

| Keyword | Values | Default | Description |
|--|--|--|--|
| `connectRetryCount` | Integer between 0 and 255 (inclusive) | 1 | The maximum number of attempts to establish or reestablish a connection before giving up. By default, a single retry attempt is made. A value of `0` means that a retry won't be attempted. |
| `connectRetryInterval` | Integer between 1 and 60 (inclusive) | 10 | The time, in seconds, between connection retry attempts. The driver will attempt to reconnect immediately upon detecting a broken idle connection, and will then wait `connectRetryInterval` seconds before trying again. This keyword is ignored if `connectRetryCount` is 0. |

If the product of `connectRetryCount` multiplied by `connectRetryInterval` is larger than `loginTimeout`, then the driver will stop attempting to connect once `loginTimeout` is reached. Otherwise, it will continue to try to reconnect until `connectRetryCount` is reached.

## Connection recovery

To detect broken idle connections, the driver relies on TCP keepalive packets at the socket level. On Linux and Java 11+, the driver automatically enables keepalive packets at a 30-second interval (`KeepAliveTime`) with a 1-second delay between retries when a failure occurs (`KeepAliveInterval`).

> [!IMPORTANT]
> On Windows and macOS or on Java 8, keepalives must be manually configured in the operating system in order to take advantage of restoring broken idle connections. For information on how to configure keepalives, see [Connection to Azure SQL database](connecting-to-an-azure-sql-database.md#connections-dropped).

## Limitations

Broken idle connections can't be restored when:

- There's an open result set that hasn't been completely parsed or buffered
- Switching databases against Azure SQL
- There's an open transaction

## See also

[Connecting to an Azure SQL database](connecting-to-an-azure-sql-database.md)  
[Technical Article - Idle Connection Resiliency](https://download.microsoft.com/download/D/2/0/D20E1C5F-72EA-4505-9F26-FEF9550EFD44/Idle%20Connection%20Resiliency.docx)  
[Overview of the JDBC driver](overview-of-the-jdbc-driver.md)  
