---
title: "Server Configuration: common criteria compliance enabled"
description: Learn how to enable Common Criteria compliance. See how to comply with Common Criteria evaluation assurance level 2 (EAL2) and 4+ (EAL4+) for EU cybersecurity certification scheme on Common Criteria (EUCC) certification approval. A world-wide compliance obligation across regulated industries and authorities.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dianas
ms.date: 01/22/2026
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
f1_keywords:
  - "common criteria compliance"
helpviewer_keywords:
  - "CC (common criteria) [Database Engine]"
  - "common criteria compliance [Database Engine]"
  - "Risidual Information Protection [Database Engine]"
  - "RIP (Residual Information Protection)"
---

# Server configuration: common criteria compliance enabled

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Common Criteria Certification (CCC) is an international program used to confirm that an IT product meets defined security requirements.

Use the `common criteria compliance enabled` server configuration option to help SQL Server meet Common Criteria security requirements. This configuration option helps you comply with Common Criteria evaluation assurance level 2 (EAL2) or 4+ (EAL4+).

## Common Criteria requirements

The `common criteria compliance enabled` configuration setting aligns with the following elements as required for the [Common Criteria for Information Technology Security Evaluation](https://www.commoncriteriaportal.org).

### Residual Information Protection

Residual Information Protection requires a memory allocation to be overwritten with a known pattern of bits before memory is reallocated to a new resource. Meeting the Residual Information Protection standard can contribute to improved security; however, overwriting the memory allocation can slow performance. After the `common criteria compliance enabled` option is enabled, the overwriting occurs.

### The ability to view login statistics

Login auditing is enabled after the `common criteria compliance enabled` option is enabled.

Login times that are made available on a per-session basis each time a user successfully logs in to SQL Server:

- Information about the last successful login time
- The last unsuccessful login time
- The number of attempts between the last successful login and the current login

To view these login statistics, query [sys.dm_exec_sessions](../../relational-databases/system-dynamic-management-objects/sys-dm-exec-sessions-transact-sql.md).

### Column `GRANT` shouldn't override table `DENY`

After you enable the `common criteria compliance enabled` configuration option, a table-level `DENY` takes precedence over a column-level `GRANT`. If this configuration option isn't enabled, a column-level `GRANT` takes precedence over a table-level `DENY`.

## Common Criteria certification

For the latest status of Common Criteria certification, download and review the [Common Criteria for SQL Server Datasheet](https://go.microsoft.com/fwlink/?LinkId=616319). The datasheet links to the latest scripts to finish configuration. The scripts are required to comply with Common Criteria evaluation assurance level 2 (EAL2) and 4+ (EAL4+).

The scripts create triggers. These triggers are required to configure a Common Criteria compliant instance. There are specific scripts for Windows and Linux. The datasheet also instructs how to verify the scripts before you run them.

To comply with Common Criteria evaluation assurance level EAL2 and EAL4+:

1. Enable `show advanced options`.
1. Enable compliance with `sp_configure` as demonstrated in [Examples](#examples).
1. Install common criteria triggers.

## Remarks

Common Criteria compliance is only evaluated and certified for [!INCLUDE [ssenterprise-md](../../includes/ssenterprise-md.md)].

The `common criteria compliance enabled` setting is an advanced option. To view the setting, enable [show advanced options](show-advanced-options-server-configuration-option.md).

When you enable the `common criteria compliance enabled` configuration option, you might notice a degradation in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] performance. For more information, see [Performance degradation in SQL Server with common criteria compliance enabled](/troubleshoot/sql/database-engine/performance/performance-degradation-ccc-enabled).

## Examples

The following example enables Common Criteria compliance.

```sql
EXECUTE sp_configure 'show advanced options', 1;
RECONFIGURE;
GO

EXECUTE sp_configure 'common criteria compliance enabled', 1;
RECONFIGURE WITH OVERRIDE;
GO
```

After you modify the configuration, restart the SQL Server services for the changes to take effect.

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
