---
title: "Server Configuration: EKM provider enabled"
description: "Learn about the EKM provider enabled option. It controls Extensible Key Management device support in SQL Server. See how to turn this option on or off."
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
f1_keywords:
  - "external encryption provider"
helpviewer_keywords:
  - "EKM provider enabled option"
---
# Server configuration: EKM provider enabled

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The `EKM provider enabled` option controls Extensible Key Management device support in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. By default this option is off.

To enable or disable the feature, issue one of the following `sp_configure` commands:

```sql
/* Enable the external encryption provider option */
EXECUTE sp_configure 'EKM provider enabled', 1;

/* Disable the external encryption provider option */
EXECUTE sp_configure 'EKM provider enabled', 0;
```

> [!NOTE]  
> This option isn't enabled in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE [editions-latest](../../includes/editions-latest.md)]

## Related content

- [Extensible Key Management (EKM)](../../relational-databases/security/encryption/extensible-key-management-ekm.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [Monitor Resource Usage (Performance Monitor)](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)
- [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
