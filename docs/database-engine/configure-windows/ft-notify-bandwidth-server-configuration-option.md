---
title: "Server Configuration: ft notify bandwidth"
description: "Learn about the ft notify bandwidth option. See how it affects the number of buffers that SQL Server maintains in the pool of small memory buffers."
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/27/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "ft notify bandwidth option"
  - "small memory buffers"
  - "memory [SQL Server], buffers"
dev_langs:
  - "TSQL"
---
# Server configuration: ft notify bandwidth

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `ft notify bandwidth` option to specify the size to which the pool of small memory buffers can grow. Small memory buffers are 64 kilobytes (KB) in size. The *max* parameter value specifies the maximum number of buffers that the full-text memory manager should maintain in a small buffer pool. If the `max` value is zero, then there's no upper limit to the number of buffers that can be in a small buffer pool.

The `min` parameter specifies the minimum number of memory buffers that must be maintained in the pool of small memory buffers. Upon request from the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] memory manager, all extra buffer pools will be released but this minimum number of buffers will be maintained. If, however, the `min` value specified is zero, then all memory buffers are released.

Under certain circumstances the number of buffers currently allocated is less than the value specified by the `min` parameter.

> [!NOTE]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
- [Server configuration: ft crawl bandwidth](ft-crawl-bandwidth-server-configuration-option.md)
- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
