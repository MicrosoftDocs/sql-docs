---
title: "Set the PAGE_VERIFY Database Option to CHECKSUM"
description: Check whether PAGE_VERIFY option is CHECKSUM, which controls whether the SQL Server Database Engine calculates a checksum to help provide data-file integrity.
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Set the page_verify database option to checksum

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks whether PAGE_VERIFY database option is set to CHECKSUM. When CHECKSUM is enabled for the PAGE_VERIFY database option, the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] calculates a checksum over the contents of the whole page, and stores the value in the page header when a page is written to disk. When the page is read from disk, the checksum is recomputed and compared to the checksum value that is stored in the page header. This helps provide a high level of data-file integrity. If you use the PAGE VERIFY CHECKSUM option for a database, when SQL Server detects a page has been altered after it has been written to disk, SQL Server reports [MSSQLSERVER_824](../errors-events/mssqlserver-824-database-engine-error.md) after reading the page back from disk.

## Best practices recommendations

Set the PAGE_VERIFY database option to CHECKSUM. Using the PAGE_VERIFY CHECKSUM database option can provide the most robust detection of database consistency problems caused by the system I/O path.

## For more information

[ALTER DATABASE SET Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
