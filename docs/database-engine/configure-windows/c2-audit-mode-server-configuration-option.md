---
title: "Server Configuration: c2 audit mode"
description: Get acquainted with C2 audit mode, a SQL Server configuration option that can help you profile system activity and track possible security policy violations.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "auditing [SQL Server]"
  - "audits [SQL Server], C2 Audit Mode option"
  - "C2 auditing"
  - "C2 Audit Mode option"
  - "recording attempts"
---
# Server configuration: c2 audit mode

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

C2 audit mode can be configured through [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or with the `c2 audit mode` option in `sp_configure`. Selecting this option configures the server to record both failed and successful attempts to access statements and objects. This information can help you profile system activity and track possible security policy violations.

> [!NOTE]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] The C2 security standard has been superseded by Common Criteria Certification. See the [common criteria compliance enabled Server Configuration Option](common-criteria-compliance-enabled-server-configuration-option.md).

## Audit log file

C2 audit mode data is saved in a file in the default data directory of the instance. If the audit log file reaches its size limit of 200 megabytes (MB), [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] creates a new file, close the old file, and write all new audit records to the new file. This process continues until the audit data directory fills up or auditing is turned off. To determine the status of a C2 trace, query the `sys.traces` catalog view.

> [!IMPORTANT]  
> C2 audit mode saves a large amount of event information to the log file, which can grow quickly. If the data directory in which logs are being saved runs out of space, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] shuts itself down. If auditing is set to start automatically, you must either restart the instance with the `-f` flag (which bypasses auditing), or free up additional disk space for the audit log.

## Permissions

Requires membership in the **sysadmin** fixed server role.

## Examples

The following example turns on C2 audit mode.

```sql
EXECUTE sp_configure 'show advanced options', 1;
GO

RECONFIGURE;
GO

EXECUTE sp_configure 'c2 audit mode', 1;
GO

RECONFIGURE;
GO
```

## Related content

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
