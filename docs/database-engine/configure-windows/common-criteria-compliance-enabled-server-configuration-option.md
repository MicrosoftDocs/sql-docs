---
title: Enable common criteria compliance configuration
description: Learn how to enable Common Criteria compliance. See how to comply with Common Criteria evaluation assurance level 2 (EAL2) and 4+ (EAL4+) for EU cybersecurity certification scheme on Common Criteria (EUCC) certification approval. A world-wide compliance obligation across regulated industries and authorities.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dianas
ms.date: 06/12/2024
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

# Enable common criteria compliance configuration

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The `common criteria compliance enabled` configuration setting aligns with the following elements as required for the [Common Criteria for Information Technology Security Evaluation](https://www.commoncriteriaportal.org).

| Criteria | Description |
|----------|-------------|
| Residual Information Protection (RIP) | RIP requires a memory allocation to be overwritten with a known pattern of bits before memory is reallocated to a new resource. Meeting the RIP standard can contribute to improved security; however, overwriting the memory allocation can slow performance. After the common criteria compliance enabled option is enabled, the overwriting occurs. |
|The ability to view login statistics | Login auditing is enabled after the common criteria compliance option is enabled.</br></br></br> Login times that are made available on a per-session basis each time a user successfully logs in to SQL Server: </br> - Information about the last successful login time </br> - The last unsuccessful login time </br> - The number of attempts between the last successful login and the current login</br></br></br> To view these login statistics, query [sys.dm_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md). |
|That column `GRANT` shouldn't override table `DENY` | After the common criteria compliance enabled option is enabled, a table-level `DENY` takes precedence over a column-level `GRANT`. When the option isn't enabled, a column-level `GRANT` takes precedence over a table-level `DENY`. |

Common criteria compliance is only evaluated and certified for the Enterprise edition and Datacenter edition.

The `common criteria compliance enabled` setting is an advanced option. To view the setting, enable [`show advanced options`](show-advanced-options-server-configuration-option.md).

For the latest status of Common Criteria certification, download and review the [Common Criteria for SQL Server Datasheet](https://go.microsoft.com/fwlink/?LinkId=616319). The datasheet links to the latest scripts to finish configuration. The scripts are required to comply with Common Criteria evaluation assurance level 2 (EAL2) and 4+ (EAL4+). The scripts create triggers. These triggers are required to configure a Common Criteria compliant instance. There are specific scripts for Windows and Linux. The datasheet also instructs how to verify the scripts before you run them.

To comply with Common Criteria evaluation assurance level EAL2 and EAL4+:

1. Enable `show advanced options`.
1. Enable compliance with `sp_configure` as demonstrated in [Examples](#examples).
1. Install common criteria triggers.

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
