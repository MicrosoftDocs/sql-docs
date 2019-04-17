---
title: "Unicode Compression Implementation | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Unicode data compression"
  - "compression [SQL Server], Unicode data"
ms.assetid: 44e69e60-9b35-43fe-b9c7-8cf34eaea62a
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Unicode Compression Implementation
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses an implementation of the Standard Compression Scheme for Unicode (SCSU) algorithm to compress Unicode values that are stored in row or page compressed objects. For these compressed objects, Unicode compression is automatic for **nchar(n)** and **nvarchar(n)** columns. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] stores Unicode data as 2 bytes, regardless of locale. This is known as UCS-2 encoding. For some locales, the implementation of SCSU compression in SQL Server can save up to 50 percent in storage space.  
  
## Supported Data Types  
 Unicode compression supports the fixed-length **nchar(n)** and **nvarchar(n)** data types. Data values that are stored off row or in **nvarchar(max)** columns are not compressed.  
  
> [!NOTE]  
>  Unicode compression is not supported for **nvarchar(max)** data even if it is stored in row. However, this data type can still benefit from page compression.  
  
## Upgrading from Earlier Versions of SQL Server  
 When a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database is upgraded to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], Unicode compression-related changes are not made to any database object, compressed or uncompressed. After the database is upgraded, objects are affected as follows:  
  
-   If the object is not compressed, no changes are made and the object continues to function as it did previously.  
  
-   Row- or page-compressed objects continue to function as they did previously. Uncompressed data remains in uncompressed form until its value is updated.  
  
-   New rows that are inserted into a row- or page-compressed table are compressed using Unicode compression.  
  
    > [!NOTE]  
    >  To take full advantage of the benefits of Unicode compression, the object must be rebuilt with page or row compression.  
  
## How Unicode Compression Affects Data Storage  
 When an index is created or rebuilt or when a value is changed in a table that was compressed with row or page compression, the affected index or value is stored compressed only if its compressed size is less than its current size. This prevents rows in a table or index from increasing in size because of Unicode compression.  
  
 The storage space that compression saves depends on the characteristics of the data that is being compressed and the locale of the data. The following table lists the space savings that can be achieved for several locales.  
  
|Locale|Compression percent|  
|------------|-------------------------|  
|English|50%|  
|German|50%|  
|Hindi|50%|  
|Turkish|48%|  
|Vietnamese|39%|  
|Japanese|15%|  
  
## See Also  
 [Data Compression](../../relational-databases/data-compression/data-compression.md)   
 [sp_estimate_data_compression_savings &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-estimate-data-compression-savings-transact-sql.md)   
 [Page Compression Implementation](../../relational-databases/data-compression/page-compression-implementation.md)   
 [sys.dm_db_persisted_sku_features &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-persisted-sku-features-transact-sql.md)  
  
  
