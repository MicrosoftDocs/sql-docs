---
title: "Server Configuration: max full-text crawl range"
description: "Find out about the max full-text crawl range option. See how it optimizes SQL Server CPU utilization to improve crawl performance during a full crawl."
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/27/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "crawls [full-text search]"
  - "max full-text crawl range option"
dev_langs:
  - "TSQL"
---
# Server configuration: max full-text crawl range

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `max full-text crawl range` option to optimize CPU utilization, which improves crawl performance during a full crawl. Using this option, you can specify the number of partitions that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] should use during a full index crawl. For example, if there are many CPUs and their utilization isn't optimal, you can increase the maximum value of this option. In addition to this option, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses several other factors, such as the number of rows in the table and the number of CPUs, to determine the actual number of partitions used.

The default value of this option is 4; the minimum value is 1, and the maximum value is 256. Changes made to this option affect only subsequent crawls. Crawls in process aren't affected by a change in this option setting.

The `max full-text crawl range` option is an advanced option. If you're using the `sp_configure` system stored procedure to change the setting, you can change `max full-text crawl range` only when `show advanced options` is set to `1`. The setting takes effect immediately without a server restart.

## Related content

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
