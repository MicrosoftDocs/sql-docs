---
title: Connection resiliency
description: Connection resiliency can transparently restore broken idle connections. This feature improves application behavior when the server closes idle connections.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs, randolphwest
ms.date: 08/27/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---

# Connection resiliency (JDBC)

[!INCLUDE [Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

*Connection resiliency* lets the JDBC driver transparently restore a broken idle connection and retry the initial connection if it fails. This article covers the two connection-string properties that control this behavior (`connectRetryCount` and `connectRetryInterval`) and the keepalive settings the driver uses to detect a dropped idle connection. Connection resiliency is available starting with Microsoft JDBC Driver 10.2.0 for SQL Server. Reconnecting a broken idle connection requires SQL Server 2014 and later versions, or Azure SQL Database.

> [!TIP]  
> Connection resiliency only retries the **initial connection** and **silently restores broken idle connections**. To automatically retry **failed statements** (for example, deadlock victim 1205 or lock timeout 1222), or to extend the connection retry list with **custom error numbers** (for example, Azure SQL transient errors such as 40197 or 40613), use [Configurable retry logic](configurable-retry-logic.md). CRL is rule-based, you pick the errors and the backoff, and it works alongside the features in this article.

## How the JDBC driver retries

The JDBC driver provides three independent retry mechanisms. They work together, so you can use all of them at once:

| Mechanism | What it does | Where to learn more |
| --- | --- | --- |
| Idle connection resiliency | Transparently restores a broken idle connection (for example, a pooled connection closed by the server or a load balancer). | [Detect broken idle connections](#detect-broken-idle-connections) (this article) |
| Initial connection retry | Retries a failed initial connection on a fixed schedule for a built-in list of transient errors. | [Retry initial connections](#retry-initial-connections) (this article) |
| Configurable retry logic (CRL) | Rule-based retry for failed statements and for custom error numbers. Introduced in Microsoft JDBC Driver 12.10. | [Configurable retry logic](configurable-retry-logic.md) |

## Retry initial connections

The JDBC driver includes two connection properties that control how often and how long the driver waits before retrying the initial connection. Add these properties to the connection string or set them through data source properties.

| Keyword | Values | Default | Description |
| --- | --- | --- | --- |
| `connectRetryCount` | Integer between 0 and 255 (inclusive) | 1 | The maximum number of attempts to establish or reestablish a connection before giving up. By default, the driver makes a single retry attempt. A value of `0` disables retry. |
| `connectRetryInterval` | Integer between 1 and 60 (inclusive) | 10 | The time, in seconds, between connection retry attempts. The driver attempts to reconnect immediately when it detects a broken idle connection, then waits `connectRetryInterval` seconds before trying again. This property is ignored when `connectRetryCount` is `0`. |

The driver runs the first retry immediately and waits `connectRetryInterval` seconds before each later one, so `connectRetryCount` retries span about `(connectRetryCount - 1) * connectRetryInterval` seconds. `loginTimeout` bounds the whole sequence: the driver stops retrying once the elapsed time plus `connectRetryInterval` reaches `loginTimeout`, which happens one interval before `loginTimeout` itself.

These properties retry only the **built-in list of transient connection errors**. For the full list of errors covered (4060, 40197, 40501, 40613, 49918-49920, and others), see [Built-in transient connection error list](configurable-retry-logic.md#built-in-transient-connection-error-list). To add custom error numbers to this set, or replace it entirely, use `retryConn` in [Configurable retry logic](configurable-retry-logic.md). To retry failed statements, use `retryExec` in the same article.

> [!CAUTION]  
> If you set `retryConn` without a leading `+`, it *replaces* the built-in list instead of extending it. Any built-in error you don't list yourself, including 40613, is no longer retried.

### Set the properties

Set `connectRetryCount` and `connectRetryInterval` in the JDBC URL, on a `Properties` object, or on a `SQLServerDataSource`.

In the JDBC URL:

```text
jdbc:sqlserver://server;databaseName=db;connectRetryCount=3;connectRetryInterval=10
```

With a `Properties` object. The Java snippets in this article omit imports and class wrappers for brevity.

```java
Properties props = new Properties();
props.setProperty("user", "...");
props.setProperty("password", "...");
props.setProperty("connectRetryCount", "3");
props.setProperty("connectRetryInterval", "10");
Connection c = DriverManager.getConnection("jdbc:sqlserver://server;databaseName=db", props);
```

With `SQLServerDataSource`:

```java
SQLServerDataSource ds = new SQLServerDataSource();
ds.setServerName("server");
ds.setDatabaseName("db");
ds.setUser("...");
ds.setPassword("...");
ds.setConnectRetryCount(3);
ds.setConnectRetryInterval(10);
```

## Connect to an auto-paused serverless database

When you use [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled, the database resumes on the first connection attempt. That attempt fails with error 40613 while the resume runs. Databases generally resume in less than one minute. For more information, see [Auto-pause and auto-resume](/azure/azure-sql/database/serverless-tier-auto-pause-resume).

Error 40613 is in the built-in transient connection error list, so the driver retries the connection. Your application doesn't need its own retry loop for this case. The default settings don't cover a resume: `connectRetryCount` is `1`, and the driver runs that single retry immediately. Both attempts happen while the database is still resuming, so the application sees the error.

To handle a resume, set all three properties together:

| Property | Why it matters |
| --- | --- |
| `connectRetryCount` | Sets how many retries you get. Set it higher than the default of `1`. |
| `connectRetryInterval` | Spaces the retries out. The first retry is immediate; the driver waits this long before each later one. |
| `loginTimeout` | Bounds the whole sequence. The driver stops retrying once the elapsed time plus `connectRetryInterval` reaches `loginTimeout`. |

The following values keep retrying for about a minute, which covers a typical resume:

```text
jdbc:sqlserver://<server>.database.windows.net;databaseName=<database>;encrypt=true;loginTimeout=120;connectRetryCount=5;connectRetryInterval=15
```

Raising `loginTimeout` on its own doesn't help, because the connection attempt fails fast with 40613 rather than hanging. Raising `connectRetryCount` on its own doesn't help either, because `loginTimeout` cuts the sequence short.

## Detect broken idle connections

A typical idle connection is one sitting in a connection pool. The driver considers a connection idle after about 30 seconds with no activity. The server or a network device between the client and the server can close idle connections, so the driver needs a way to notice that the socket is dead before the next query runs.

To detect broken idle connections, the driver relies on TCP keepalive packets at the socket level. On Linux with Java 11 and later versions, the driver automatically enables keepalive packets at a 30-second interval (`KeepAliveTime`), with a 1-second delay between retries when a failure occurs (`KeepAliveInterval`).

> [!IMPORTANT]  
> On Windows, and on Java 11 or earlier, you must configure keepalives manually in the operating system to take advantage of broken-idle-connection recovery. For information on how to configure keepalives, see [Connection to Azure SQL database](connecting-to-an-azure-sql-database.md#connections-dropped).

## Limitations

The driver can't restore a broken idle connection when any of the following conditions are true:

- There's an open result set that isn't completely parsed or buffered.
- The connection switched databases against Azure SQL.
- There's an open transaction.

## Related content

- [Configurable retry logic (JDBC)](configurable-retry-logic.md)
- [Connect to an Azure SQL database](connecting-to-an-azure-sql-database.md)
- [Understanding timeout properties in the JDBC driver](understand-timeouts.md)
- [Technical article: Idle Connection Resiliency](https://download.microsoft.com/download/D/2/0/D20E1C5F-72EA-4505-9F26-FEF9550EFD44/Idle%20Connection%20Resiliency.docx)
- [Overview of the JDBC driver](overview-of-the-jdbc-driver.md)
