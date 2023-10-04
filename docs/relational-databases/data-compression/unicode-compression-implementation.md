---
title: "Unicode compression implementation"
description: SQL Server uses the Standard Compression Scheme for Unicode algorithm to compress Unicode values that are stored in row or page compressed objects.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 08/21/2023
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords:
  - "Unicode data compression"
  - "compression [SQL Server], Unicode data"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Unicode compression implementation

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses an implementation of the Standard Compression Scheme for Unicode (SCSU) algorithm to compress Unicode values that are stored in row or page compressed objects. For these compressed objects, Unicode compression is automatic for **nchar(*n*)** and **nvarchar(*n*)** columns. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] stores Unicode data as 2 bytes, regardless of locale. This is known as UCS-2 encoding. For some locales, the implementation of SCSU compression in SQL Server can save up to 50 percent in storage space.

## Supported data types

Unicode compression supports the fixed-length **nchar(*n*)** and **nvarchar(*n*)** data types. Data values that are stored off row or in **nvarchar(max)** columns aren't compressed.

> [!NOTE]  
> Unicode compression is not supported for **nvarchar(max)** data even if it is stored in row. However, this data type can still benefit from page compression.

## Upgrade from earlier versions of SQL Server

When a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database is upgraded to [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], Unicode compression-related changes aren't made to any database object, compressed or uncompressed. After the database is upgraded, objects are affected as follows:

- If the object isn't compressed, no changes are made and the object continues to function as it did previously.

- Row- or page-compressed objects continue to function as they did previously. Uncompressed data remains in uncompressed form until its value is updated.

- New rows that are inserted into a row- or page-compressed table are compressed using Unicode compression.

  > [!NOTE]  
  > To take full advantage of the benefits of Unicode compression, rebuild the object with page or row compression.

## How Unicode compression affects data storage

When an index is created or rebuilt or when a value is changed in a table that was compressed with row or page compression, the affected index or value is stored compressed only if its compressed size is less than its current size. This prevents rows in a table or index from increasing in size because of Unicode compression.

The storage space that compression saves depends on the characteristics of the data that is being compressed and the locale of the data. The following table lists the space savings that can be achieved for several locales.

| Locale | Compression percent |
| --- | --- |
| English | 50% |
| German | 50% |
| Hindi | 50% |
| Turkish | 48% |
| Vietnamese | 39% |
| Japanese | 15% |

## Next steps

- [Data compression](data-compression.md)
- [sp_estimate_data_compression_savings (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-estimate-data-compression-savings-transact-sql.md)
- [sys.dm_db_persisted_sku_features (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-persisted-sku-features-transact-sql.md)
- [Row compression implementation](row-compression-implementation.md)
- [Page compression implementation](page-compression-implementation.md)
