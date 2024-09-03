---
title: "Server configuration: in-doubt xact resolution"
description: Learn about the in-doubt xact resolution option. See how it determines the default outcome for in-doubt transactions in SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "distributed transactions [SQL Server], unresolved transactions"
  - "unresolved transactions"
  - "in-doubt xact resolution option"
---
# Server configuration: in-doubt xact resolution

[!INCLUDE [appliesto-sqlserver-sqlmi-sqlvm](../../includes/applies-to-version/appliesto-sqlserver-sqlmi-sqlvm.md)]

Use the `in-doubt xact resolution` option to control the default outcome of transactions that the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) is unable to resolve. Inability to resolve transactions might be related to the MS DTC down time or an unknown transaction outcome at the time of recovery.

The following table lists the possible outcome values for resolving an in-doubt transaction.

| Outcome value | Description |
| --- | --- |
| `0` | No presumption. Recovery fails if MS DTC can't resolve any in-doubt transactions. |
| `1` | Presume commit. Presumes that any MS DTC in-doubt transactions are committed. |
| `2` | Presume abort. Presumes that any MS DTC in-doubt transactions are aborted. |

To minimize the possibility of extended down time, an administrator might choose to configure this option either to presume commit or presume abort, as shown in the following example.

```sql
sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
sp_configure 'in-doubt xact resolution', 2; -- presume abort
GO
RECONFIGURE;
GO
sp_configure 'show advanced options', 0;
GO
RECONFIGURE;
GO
```

Alternatively, the administrator might want to leave the default (no presumption) and allow recovery to fail in order to be made aware of a DTC failure, as shown in the following example.

```sql
sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
sp_configure 'in-doubt xact resolution', 1; -- presume commit
GO
RECONFIGURE
GO
ALTER DATABASE pubs SET ONLINE -- run recovery again
GO
sp_configure 'in-doubt xact resolution', 0 -- back to no assumptions
GO
sp_configure 'show advanced options', 0
GO
RECONFIGURE
GO
```

The `in-doubt xact resolution` option is an advanced option. If you use the `sp_configure` system stored procedure to change the setting, you can change `in-doubt xact resolution` only when `show advanced options` is set to `1`. The setting takes effect immediately without a server restart.

> [!NOTE]  
> Consistent configuration of this option across all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances involved in any distributed transactions helps avoid data inconsistencies.

## Related content

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
