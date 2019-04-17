---
title: "Verify autogrow is turned on for all data and log files during the upgrade process | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "log files [SQL Server], size"
  - "data files [SQL Server], size"
  - "tempdb [SQL Server], size"
  - "autogrow [SQL Server]"
ms.assetid: a5860904-e2be-4224-8a51-df18a10d3fb9
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Verify autogrow is turned on for all data and log files during the upgrade process
  Upgrade Advisor detected data or log files that are not set to autogrow. New and enhanced features require additional disk space for user databases and the **tempdb** system database. To ensure resources can accommodate size increases during upgrade and subsequent production operations, we recommend setting autogrow to ON for all user data and log files and the **tempdb** data and log files before upgrading.  
  
 After you have upgraded and tested your workloads, you may want to turn autogrow off or adjust the FILEGROWTH increment accordingly for user data and log files. We recommend that autogrow remain on for the **tempdb** system database. For more information, see "Capacity Planning for tempdb" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 **Data files**  
  
 The following table lists changes to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that result in additional disk space requirements for user-defined data files.  
  
|Feature|Changes Introduced in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|-------------|-----------------------------------------------------|  
|Full-text|The document ID (DOCID) map is stored in the data file instead of in the full-text catalog.|  
|`text`, `ntext`, and `image` columns|Large object (LOB) columns defined as `text`, `ntext`, or `image` data types require an additional 40 bytes of disk space per column. This one-time space increase occurs during the first update of each LOB column.|  
|metadata|Additional system metadata is created and maintained in the PRIMARY filegroup of each user database for database objects and user permissions. For example, in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the permissions associated with a grantor or grantee is stored in a single row as a bitmap. The bitmap is expanded into multiple rows.<br /><br /> During the upgrade process, there must be enough disk space to store both the old and new metadata. The old metadata is deleted after the upgrade is complete.|  
  
 **Transaction log files**  
  
 The following table lists changes to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that result in additional disk space requirements for user-defined transaction log files.  
  
|Feature|Changes Introduced in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|-------------|-----------------------------------------------------|  
|Restore and recovery|During the undo phase of a crash recovery, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows users to access the database. This is possible because the transactions that were uncommitted when the crash occurred reacquire any locks they held before the crash. While transactions are being rolled back, their locks protect them from interference by users. This additional locking information must be maintained in the transaction log.|  
  
 **tempdb data and log files**  
  
 In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the **tempdb** database is used to store the following objects:  
  
-   Temporary objects explicitly created such as tables, stored procedures, table variables, or cursors.  
  
-   Internal work tables created by the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   Results from temporary sorts when you create or rebuild indexes, if SORT_IN_TEMPDB is specified.  
  
 Additional objects also use the **tempdb** database. The following table lists changes or additions to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that result in additional disk space requirements for **tempdb** data and log files.  
  
|Feature|Changes Introduced in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|-------------|-----------------------------------------------------|  
|Row versioning|Row versioning is a general framework in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is used to:<br /><br /> Support triggers: Build the inserted and deleted tables in triggers. Any rows modified by the trigger are versioned. This includes the rows modified by the statement that launched the trigger, as well as any data modifications made by the trigger. AFTER triggers use the version store of **tempdb** to hold the before images of the rows changed by the trigger. When you bulkload data with triggers enabled, a copy of each row is added to the version store.<br /><br /> Support Multiple Active Result Sets (MARS). If a MARS session issues a data modification statement (such as INSERT, UPDATE, or DELETE) at a time there is an active result set, the rows affected by the modification statement are versioned.<br /><br /> Support index operations that specify the ONLINE option. Online index operations use row versioning to isolate the index operation from the effects of modifications made by other transactions. This avoids the need for requesting share locks on rows that have been read. In addition, concurrent user update and delete operations during online index operations require space for version records in **tempdb**.<br /><br /> Support row versioning-based transaction isolation levels: A new implementation of read committed isolation level that uses row versioning to provide statement-level read consistency. A new isolation level, snapshot, to provide transaction-level read consistency.<br /><br /> <br /><br /> Row versions are held in the **tempdb** version store long enough to satisfy the requirements of transactions running under row versioning-based isolation levels.<br /><br /> For more information about row versioning and the version store, see the topic "Understanding Row Versioning-based Isolation Levels" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.|  
|Caching of temp table and temp variable metadata|For every metadata of temp table and temp variable cached in the metadata cache by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], two extra pages are allocated for **tempdb**.<br /><br /> If a stored procedure or trigger creates a temp table or temp variable, the temporary object is not deleted when the procedure or trigger completes execution. Instead, the temporary object is truncated to one page and reused the next time the procedure or trigger is executed.|  
|Indexes on partitioned tables|When the [!INCLUDE[ssDE](../../includes/ssde-md.md)] performs sorting to build partitioned indexes, enough space to hold the intermediate sort runs of each partition is required in **tempdb** if the SORT_IN_TEMPDB index option is specified.|  
|[!INCLUDE[ssSB](../../includes/sssb-md.md)]|[!INCLUDE[ssSB](../../includes/sssb-md.md)] explicitly uses **tempdb** when preserving existing dialog context that cannot stay in memory (approximately 1 KB per dialog).<br /><br /> [!INCLUDE[ssSB](../../includes/sssb-md.md)] implicitly uses **tempdb** through the caching of objects in the context of query execution. For example, work tables used for timer events and background delivered conversations.<br /><br /> The features DBMail, event notifications, and query notifications implicitly use [!INCLUDE[ssSB](../../includes/sssb-md.md)].|  
|Large object (LOB) data type<br /><br /> LOB variables and parameters|The data types `varchar(max)`, `nvarchar(max)`, **varbinary(max)text**, `ntext`, `image,` and `xml` are large object types.<br /><br /> When a row versioning-based transaction isolation level is enabled on the database and modifications of large objects are made, the changed fragment of the LOB is copied to the version store in **tempdb**.<br /><br /> Parameters defined as a large object data type are stored in **tempdb**.|  
|Common table expressions (CTEs)|Temporary work tables for spool operations are created in **tempdb** when common table expression queries are executed.|  
  
## Corrective Action  
 To set a data or log file to autogrow, modify the following statements to specify the data and log for your database. You should adjust the FILEGROWTH increment to a value that is appropriate for your environment.  
  
```  
ALTER DATABASE tempdb  
MODIFY FILE  
    (NAME = tempdev,  
    FILEGROWTH = 20MB);  
GO  
ALTER DATABASE tempdb  
MODIFY FILE  
    (NAME = templog,  
    FILEGROWTH = 10MB);  
```  
  
 The default growth increment for data files is 1 MB. The log file default is10%. We recommend the following general guidelines when setting the FILEGROWTH increment:  
  
|File size|FILEGROWTH increment|  
|---------------|--------------------------|  
|0-50MB|10MB|  
|100-200MB|20MB|  
|500MB or more|10%|  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
