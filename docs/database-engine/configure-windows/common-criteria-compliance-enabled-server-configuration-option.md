---
title: Common Criteria Compliance Enabled Configuration
description: Learn which criteria the common criteria compliance option enables in SQL Server. See how to comply with Common Criteria Evaluation Assurance Level. For EUCC certification approval. A world-wide compliance obligation across regulated industries and authorities.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wopeter
ms.date: 04/07/2021
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
f1_keywords:
  - "common criteria compliance"
helpviewer_keywords:
  - "CC (common criteria) [Database Engine]"
  - "common criteria compliance [Database Engine]"
  - "Risidual Information Protection [Database Engine]"
  - "RIP (Residual Information Protection)"
---

# Common Criteria Compliance Enabled Server Configuration

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The common criteria compliance option enables the following elements that are required for the [Common Criteria for Information Technology Security Evaluation](https://www.commoncriteriaportal.org). A requirement for a world-wide compliance obligation across regulated industries and authorities.

| Criteria | Description |
|----------|-------------|
| Residual Information Protection (RIP) | RIP requires a memory allocation to be overwritten with a known pattern of bits before memory is reallocated to a new resource. Meeting the RIP standard can contribute to improved security; however, overwriting the memory allocation can slow performance. After the common criteria compliance enabled option is enabled, the overwriting occurs. |
|The ability to view login statistics | Login auditing is enabled after the common criteria compliance option is enabled. </br></br></br> Login times that are made available on a per-session basis each time a user successfully logs in to SQL Server: </br> - Information about the last successful login time </br> - The last unsuccessful login time </br> - The number of attempts between the last successful login and the current login. </br></br></br> These login statistics can be viewed by querying the [sys.dm_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md) dynamic management view. |
|That column `GRANT` shouldn't override table `DENY` | After the common criteria compliance enabled option is enabled, a table-level `DENY` takes precedence over a column-level `GRANT`. When the option isn't enabled, a column-level `GRANT` takes precedence over a table-level `DENY`. |

The common criteria compliance enabled option is an advanced option. Common criteria is only evaluated and certified for the Enterprise edition and Datacenter edition. For the latest status of common criteria certification, see the [Microsoft SQL Server Common Criteria](https://go.microsoft.com/fwlink/?LinkId=616319) site.

> [!IMPORTANT]
> In addition to enabling the common criteria compliance enabled option, you also must download and run a script that finishes configuring SQL Server to comply with Common Criteria Evaluation Assurance Level 4+ (EAL4+). You can download this script from the [Microsoft SQL Server Common Criteria](https://go.microsoft.com/fwlink/?LinkId=616319) site.

If you're using the `sp_configure` system stored procedure to change the setting, you can change common criteria compliance enabled only when show advanced options is set to 1. The setting takes effect after the server is restarted. The possible values are 0 and 1:

- 0 indicates that common criteria compliance isn't enabled (default).

- 1 indicates that common criteria compliance is enabled.

## Examples

The following example enables common criteria compliance.

```sql
sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
sp_configure 'common criteria compliance enabled', 1;
GO
RECONFIGURE WITH OVERRIDE;
GO
```

Restart SQL Server.

## Next steps

- [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
