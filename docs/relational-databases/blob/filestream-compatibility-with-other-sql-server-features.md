---
title: "FILESTREAM compatibility | Microsoft Docs"
description: FILESTREAM stores data in the file system. Read about guidelines, limitations, and tips to keep in mind when using FILESTREAM with various SQL Server features.
ms.custom: "seo-lt-2019"
ms.date: "12/13/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], other SQL Server features and"
  - "FILESTREAM [SQL Server], limitations"
ms.assetid: d2c145dc-d49a-4f5b-91e6-89a2b0adb4f3
author: MikeRayMSFT
ms.author: mikeray
---
# FILESTREAM compatibility with other SQL Server features

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Because FILESTREAM data is in the file system, this topic provides some considerations, guidelines, and limitations for using FILESTREAM with the following features in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   [SQL Server Integration Services (SSIS)](#ssis)  
  
-   [Distributed Queries and Linked Servers](#distqueries)  
  
-   [Encryption](#encryption)  
  
-   [Database Snapshots](#DatabaseSnapshot)  
  
-   [Replication](#Replication)  
  
-   [Log Shipping](#LogShipping)  
  
-   [Database Mirroring](#DatabaseMirroring)  
  
-   [Full-Text Indexing](#FullText)  
  
-   [Failover Clustering](#FailoverClustering)  
  
-   [SQL Server Express](#SQLServerExpress)  
  
-   [Contained Databases](#contained)  
  
##  <a name="ssis"></a> SQL Server Integration Services (SSIS)  
 SQL Server Integration Services (SSIS) handles FILESTREAM data in the data flow like any other BLOB data by using the DT_IMAGE SSIS data type.  
  
 You can use the Import Column transformation to load files from the file system into a FILESTREAM column. You can also use the Export Column transformation to extract files from a FILESTREAM column to another location in the file system.  
  
##  <a name="distqueries"></a> Distributed Queries and Linked Servers  
 You can work with FILESTREAM data through distributed queries and linked servers by treating it as **varbinary(max)** data. You cannot use the FILESTREAM **PathName()** function in distributed queries that use a four-part name, even when the name refers to the local server. However you can use **PathName()** in the inner query of a pass-through query that uses **OPENQUERY()**.  
  
##  <a name="encryption"></a> Encryption  
 FILESTREAM data is not encrypted even when transparent data encryption is enabled.  
  
##  <a name="DatabaseSnapshot"></a> Database Snapshots  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not support [database snapshots](../../relational-databases/databases/database-snapshots-sql-server.md) for FILESTREAM filegroups. If a FILESTREAM filegroup is included in a CREATE DATABASE ON clause, the statement will fail and an error will be raised.  
  
 When you are using FILESTREAM, you can create database snapshots of standard (non-FILESTREAM) filegroups. The FILESTREAM filegroups are marked as offline for those database snapshots.  
  
 A SELECT statement that is executed on a FILESTREAM table in a database snapshot must not include a FILESTREAM column; otherwise, the following error message will be returned:  
  
 `Could not continue scan with NOLOCK due to data movement.`  
  
##  <a name="Replication"></a> Replication  
 A **varbinary(max)** column that has the FILESTREAM attribute enabled at the Publisher can be replicated to a Subscriber with or without the FILESTREAM attribute. To specify the way in which the column is replicated, use the **Article Properties - \<Article>** dialog box or the @schema_option parameter of [sp_addarticle](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) or [sp_addmergearticle](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md). Data that is replicated to a **varbinary(max)** column that does not have the FILESTREAM attribute must not exceed the 2-GB limit for that data type; otherwise, a run-time error is generated. We recommend that you replicate the FILESTREAM attribute, unless you are replicating data to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. Replicating tables that have FILESTREAM columns to [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] Subscribers is not supported, regardless of the schema option that is specified.  
  
> [!NOTE]  
>  Replicating large data values from [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Subscribers is limited to a maximum of 256 MB data values. For more information, see [Maximum Capacity Specifications](../../sql-server/maximum-capacity-specifications-for-sql-server.md).  
  
### Considerations for Transactional Replication  
 If you use FILESTREAM columns in tables that are published for transactional replication, note the following considerations:  
  
-   If any tables include columns that have the FILESTREAM attribute, you cannot use values of *database snapshot* or *database snapshot character* for the @sync_method property of [sp_addpublication](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md).  
  
-   The max text repl size option specifies the maximum amount of data that can be inserted into a column that is published for replication. This option can be used to control the size of FILESTREAM data that is replicated.  
  
-   If you specify the schema option to replicate the FILESTREAM attribute, but you filter out the **uniqueidentifier** column that FILESTREAM requires or you specify not to replicate the UNIQUE constraint for the column, replication does not replicate the FILESTREAM attribute. The column is replicated only as a **varbinary(max)** column.  
  
### Considerations for Merge Replication  
 If you use FILESTREAM columns in tables that are published for merge replication, note the following considerations:  
  
-   Both merge replication and FILESTREAM require a column of data type **uniqueidentifier** to identify each row in a table. Merge replication automatically adds a column if the table does not have one. Merge replication requires that the column have the ROWGUIDCOL property set and a default of NEWID() or NEWSEQUENTIALID(). In addition to these requirements, FILESTREAM requires that a UNIQUE constraint be defined for the column. These requirements have the following consequences:  
  
    -   If you add a FILESTREAM column to a table that is already published for merge replication, make sure that the **uniqueidentifier** column has a UNIQUE constraint. If it does not have a UNIQUE constraint, add a named constraint to the table in the publication database. By default, merge replication will publish this schema change, and it will be applied to each subscription database.  
  
         If you add a UNIQUE constraint manually as described and you want to remove merge replication, you must first remove the UNIQUE constraint; otherwise, replication removal will fail.  
  
    -   By default, merge replication uses NEWSEQUENTIALID() because it can provide better performance than NEWID(). If you add a **uniqueidentifier** column to a table that will be published for merge replication, specify NEWSEQUENTIALID() as the default.  
  
-   Merge replication includes an optimization for replicating large object types. This optimization is controlled by the @stream_blob_columns parameter of [sp_addmergearticle](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md). If you set the schema option to replicate the FILESTREAM attribute, the @stream_blob_columns parameter value is set to **true**. This optimization can be overridden by using [sp_changemergearticle](../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). This stored procedure enables you to set @stream_blob_columns to **false**. If you add a FILESTREAM column to a table that is already published for merge replication, we recommend that you set the option to **true** by using sp_changemergearticle.  
  
-   Enabling the schema option for FILESTREAM after an article is created can cause replication to fail if the data in a FILESTREAM column exceeds 2 GB and there is a conflict during replication. If you expect this situation to arise, it is recommended that you drop and re-create the table article with the appropriate FILESTREAM schema option enabled at creation time.  
  
-   Merge replication can synchronize FILESTREAM data over an HTTPS connection by using [Web Synchronization](../../relational-databases/replication/web-synchronization-for-merge-replication.md). This data cannot exceed the 50 MB limit for Web Synchronization; otherwise, a run-time error is generated.  
  
##  <a name="LogShipping"></a> Log Shipping  
 [Log shipping](../../database-engine/log-shipping/about-log-shipping-sql-server.md) supports FILESTREAM. Both the primary and secondary servers must be running [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], or a later version, and have FILESTREAM enabled.  
  
##  <a name="DatabaseMirroring"></a> Database Mirroring  
 Database mirroring does not support FILESTREAM. A FILESTREAM filegroup cannot be created on the principal server. Database mirroring cannot be configured for a database that contains FILESTREAM filegroups.  
  
##  <a name="FullText"></a> Full-Text Indexing  
 [Full-text indexing](../../relational-databases/search/populate-full-text-indexes.md) works with a FILESTREAM column in the same way that it does with a **varbinary(max)** column. The FILESTREAM table must have a column that contains the file name extension for each FILESTREAM BLOB. For more information, see [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md), [Configure and Manage Filters for Search](../../relational-databases/search/configure-and-manage-filters-for-search.md), and [sys.fulltext_document_types &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-document-types-transact-sql.md).  
  
 The full-text engine indexes the contents of the FILESTREAM BLOBs. Indexing files such as images might not be useful. When a FILESTREAM BLOB is updated it is reindexed.  
  
##  <a name="FailoverClustering"></a> Failover Clustering  
 For failover clustering, FILESTREAM filegroups must be put on a shared disk. FILESTREAM must be enabled on each node in the cluster that will host the FILESTREAM instance. For more information, see [Set Up FILESTREAM on a Failover Cluster](../../relational-databases/blob/set-up-filestream-on-a-failover-cluster.md).  
  
##  <a name="SQLServerExpress"></a> SQL Server Express  
 [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] supports FILESTREAM. The 10-GB database size limit does not include the FILESTREAM data container.  
  
##  <a name="contained"></a> Contained Databases  
 The FILESTREAM feature requires some configuration outside of the database. Therefore a database that uses FILESTREAM or FileTable is not fully contained.  
  
 You can set database containment to PARTIAL if you want to use certain features of contained databases, such as contained users. In this case, however, you must be aware that some of the database settings are not contained in the database and are not automatically moved when the database moves.  
  
## See Also  
 [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../../relational-databases/blob/binary-large-object-blob-data-sql-server.md)  
  
