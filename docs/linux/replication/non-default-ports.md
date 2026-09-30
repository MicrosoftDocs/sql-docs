---
title: Configure Replication Snapshot Folder (Nondefault Ports)
titleSuffix: SQL Server on Linux
description: Learn to configure snapshot folder shares with nondefault ports for SQL Server replication on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 05/07/2026
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - linux-related-content
ai-usage: ai-assisted
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
---
# Configure replication with nondefault ports (SQL Server on Linux)

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

You can configure replication with SQL Server on Linux instances listening on any port configured with the `network.tcpport` mssql-conf setting. The port needs to be appended to the server name during configuration if the following conditions are true:

- Replication setup involves an instance of SQL Server on Linux.
- Any instance (Windows or Linux) is listening on a nondefault port.

The server name of an instance can be found by running `@@SERVERNAME` on the instance. Don't use the IP address instead of the server name. Using the IP address for the publisher, distributor, or subscriber might result in an error.

> [!NOTE]  
> Creating SQL Server replication on Linux with a nondefault port works only with SQL Server 2019 and later versions.

## Examples

`Server1` listens on port 1500 on Linux. To configure `Server1` for distribution, run 'sp_adddistributor` with `@distributor`. For example:

```sql
EXECUTE sys.sp_adddistributor @distributor = N'Server1,1500';
```

`Server1` listens on port 1500 on Linux. To configure a publisher for the distributor, run `sp_adddistpublisher` with `@publisher` and `@distribution_db`. For example:

```sql
EXECUTE sys.sp_adddistpublisher
  @publisher = N'Server1,1500',
  @distribution_db = N'<distribution_database>';
```

`Server2` listens on port 6549 on Linux. To configure `Server2` as a subscriber, run `sp_addsubscription` with `@publication` and `@subscriber`. For example:

```sql
EXECUTE sys.sp_addsubscription
  @publication = N'<publication>',
  @subscriber = N'Server2,6549';
```

`Server3` listens on port 6549 on Windows with server name `Server3` and instance name `MSSQL2017`. To configure `Server3` as a subscriber, run `sp_addsubscription` with `@publication` and `@subscriber`. For example:

```sql
EXECUTE sys.sp_addsubscription
  @publication = N'<publication>',
  @subscriber = N'Server3\MSSQL2017,6549';
```

## Known issues

### Linked server port not updated when recreating subscription

When you delete and recreate a subscription with a nondefault port on the Subscriber, the system reuses the existing linked server but fails to update the port configuration. This can cause replication to fail when attempting to connect to the Subscriber.

For more information about this known issue, including symptoms, cause, and workaround, see [Delete a push subscription](../../relational-databases/replication/delete-a-push-subscription.md#known-issue-port).

## Related content

- [SQL Server replication on Linux](overview.md)
- [Replication stored procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)
