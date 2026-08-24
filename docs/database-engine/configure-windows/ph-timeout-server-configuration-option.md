---
title: "Server configuration: PH timeout"
description: "Learn about the PH timeout option. See how it limits the time that the full-text protocol handler allots for connecting to a SQL Server database."
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "time limit for protocol handler wait [SQL Server]"
  - "timeout options [SQL Server], ph timeout option"
  - "protocols [SQL Server], timing out"
  - "ph timeout option"
dev_langs:
  - "TSQL"
---
# Server configuration: PH timeout

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The full-text protocol handler (PH) is hosted in the filter daemon host, and is used to fetch from SQL Server the data to be full-text indexed.

Use the `PH timeout` option to specify the time, in seconds, that the full-text protocol handler should wait to connect to a database before timing out. The default value is 60 seconds. Increase the `PH timeout` value when connection attempts are timing out due to temporary network issues.

For more information about full-text search components, see [Full-Text Search](../../relational-databases/search/full-text-search.md).

## Remarks

When attempting to fetch a data row, if the protocol handler can't connect to SQL Server within the specified time, it reports a timeout error for that row. The full-text gatherer retries the row later. For more information about the full-text gatherer, see [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md).

## Related content

- [Full-Text Search](../../relational-databases/search/full-text-search.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
