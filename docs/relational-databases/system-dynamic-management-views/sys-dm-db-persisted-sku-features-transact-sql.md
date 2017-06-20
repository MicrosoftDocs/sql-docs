---
title: "sys.dm_db_persisted_sku_features (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_db_persisted_sku_features_TSQL"
  - "sys.dm_db_persisted_sku_features"
  - "dm_db_persisted_sku_features_TSQL"
  - "dm_db_persisted_sku_features"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "editions [SQL Server], feature restrictions"
  - "sys.dm_db_persisted_sku_features dynamic management view"
ms.assetid: b4b29e97-b523-41b9-9528-6d4e84b89e09
caps.latest.revision: 26
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.dm_db_persisted_sku_features (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Some features of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] change the way that [!INCLUDE[ssDE](../../includes/ssde-md.md)] stores information in the database files. These features are restricted to specific editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A database that contains these features cannot be moved to an edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that does not support them Use the sys.dm_db_persisted_sku_features dynamic management view to list all edition-specific features that are enabled in the current database.  
  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|feature_name|**sysname**|External name of the feature that is enabled in the database but not supported on the all the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This feature must be removed before the database can be migrated to all available editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|feature_id|**Int**|Feature ID that is associated with the feature. [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)].|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the database.  
  
## Remarks  
 If no features that are restricted by edition are used by the database, the view returns no rows.  
  
 sys.dm_db_persisted_sku_features may list the following database-changing features as restricted to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise or Developer editions:  
  
-   **ChangeCapture.** Indicates that a database has change data capture enabled. To remove change data capture, use the sys.sp_cdc_disable_db stored procedure.  
  
-   **ColumnStoreIndex.** Indicates that at least one table has an xVelocity memory-optimized columnstore index. To enable a database to be moved to an edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] other than Enterprise or Developer, use the [DROP INDEX](../../t-sql/statements/drop-index-transact-sql.md) or [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md) statement to remove the columnstore index.  
  
    **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]).  
  
-   **Compression.** Indicates that at least one table or index uses data compression or the vardecimal storage format. To enable a database to be moved to an edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] other than Enterprise or Developer, use the [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md) statement to remove data compression. To remove vardecimal storage format, use the sp_tableoption statement.  
  
-   **InMemoryOLTP.** Indicates that the database uses In-Memory OLTP. The database has a MEMORY_OPTIMIZED_DATA filegroup.  
  
  **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]). 
  
-   **Partitioning.** Indicates that the database contains partitioned tables, partitioned indexes, partition schemes, or partition functions. To enable a database to be moved to an edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] other than Enterprise or Developer, it is insufficient to modify the table to be on a single partition. You must remove the partitioned table. If the table contains data, use SWITCH PARTITION to convert each partition into a nonpartitioned table. Then delete the partitioned table, the partition scheme, and the partition function.  
  
-   **TransparentDataEncryption.** Indicates that a database is encrypted by using transparent data encryption. To remove transparent data encryption, use the ALTER DATABASE statement. For more information, see [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md).  
  
 To determine whether a database uses any features that are restricted to specific editions, execute the following statement in the database:  
  
```tsql  
SELECT feature_name FROM sys.dm_db_persisted_sku_features;  
GO  
```  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)  
  
