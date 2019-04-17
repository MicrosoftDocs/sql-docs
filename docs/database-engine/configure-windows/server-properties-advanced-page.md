---
title: "Server Properties (Advanced Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.serverproperties.advanced.f1"
ms.assetid: cc5e65c2-448e-4f37-9ad4-2dfb1cc84ebe
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Server Properties - Advanced Page
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use this page to view or modify your advanced server settings.  
  
 **To view the Server Properties pages**  
  
-   [View or Change Server Properties &#40;SQL Server&#41;](../../database-engine/configure-windows/view-or-change-server-properties-sql-server.md)  
  
## Containment  
 Enable Contained Databases  
 Indicates if this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] permits contained databases. When **True**, a contained database can be created, restored, or attached. When **False**, a contained database cannot be created, restored, or attached to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Changing the containment property can have an impact on the security of the database. Enabling contained databases lets database owners grant access to this [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Disabling contained databases can prevent users from connecting. To understand the impact of the containment property, see [Contained Databases](../../relational-databases/databases/contained-databases.md) and [Security Best Practices with Contained Databases](../../relational-databases/databases/security-best-practices-with-contained-databases.md).  
  
## FILESTREAM  
 **FILESTREAM Access Level**  
 Shows the current level of FILESTREAM support on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To change the access level, select one of the following values:  
  
 **Disabled**  
 Binary large object (BLOB) data cannot be stored on the file system. This is the default value.  
  
 **Transact-SQL access enabled**  
 FILESTREAM data is accessible by using [!INCLUDE[tsql](../../includes/tsql-md.md)], but not through the file system.  
  
 **Full access enabled**  
 FILESTREAM data is accessible by using [!INCLUDE[tsql](../../includes/tsql-md.md)] and through the file system.  
  
 When you enable FILESTREAM for the first time, you might have to restart the computer to configure drivers.  
  
 **FILESTREAM Share Name**  
 Displays the read-only name of the FILESTREAM share that was selected during setup. For more information, see [FILESTREAM &#40;SQL Server&#41;](../../relational-databases/blob/filestream-sql-server.md).  
  
## Miscellaneous  
 **Allow Triggers to Fire Others**  
 Allows triggers to fire other triggers. Triggers can be nested to a maximum of 32 levels. For more information, see the "Nested Triggers" section in [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md).  
  
 **Blocked Process Threshold**  
 The threshold, in seconds, at which blocked process reports are generated. The threshold can be set from 0 to 86,400. By default, no blocked process reports are produced. For more information, see [blocked process threshold Server Configuration Option](../../database-engine/configure-windows/blocked-process-threshold-server-configuration-option.md).  
  
 **Cursor Threshold**  
 Specifies the number of rows in the cursor set at which cursor keysets are generated asynchronously. When cursors generate a keyset for a result set, the query optimizer estimates the number of rows that will be returned for that result set. If the query optimizer estimates that the number of returned rows is greater than this threshold, the cursor is generated asynchronously, allowing the user to fetch rows from the cursor while the cursor continues to be populated. Otherwise, the cursor is generated synchronously, and the query waits until all rows are returned.  
  
 If set to -1, all keysets are generated synchronously; this benefits small cursor sets. If set to 0, all cursor keysets are generated asynchronously. With other values, the query optimizer compares the number of expected rows in the cursor set and builds the keyset asynchronously if it exceeds the number set. For more information, see [Configure the cursor threshold Server Configuration Option](../../database-engine/configure-windows/configure-the-cursor-threshold-server-configuration-option.md).  
  
 **Default Full Text Language**  
 Specifies a default language for full-text indexed columns. Linguistic analysis of full-text indexed data is dependent on the language of the data. The default value of this option is the language of the server. For the language that corresponds to the displayed setting, see [sys.fulltext_languages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md).  
  
 **Default Language**  
 The default language for all new logins, unless otherwise specified.  
  
 **Full-Text Upgrade Option**  
 Controls how full-text indexes are migrated when upgrading a database from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. This property applies to upgrading by attaching a database, restoring a database backup, restoring a file backup, or copying the database by using the Copy Database Wizard.  
  
 The alternatives are as follows:  
  
 **Import**  
 Full-text catalogs are imported. This operation is significantly faster than **Rebuild**. However, an imported full-text catalog does not use the new and enhanced word breakers that are introduced in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]. Therefore, you might want to rebuild your full-text catalogs eventually.  
  
 If a full-text catalog is not available, the associated full-text indexes are rebuilt. This option is available for only [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] databases.  
  
 **Rebuild**  
 Full-text catalogs are rebuilt using the new and enhanced word breakers. Rebuilding indexes can take awhile, and a significant amount of CPU and memory might be required after the upgrade.  
  
 **Reset**  
 Full-text catalogs are reset. [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] full-text catalog files are removed, but the metadata for full-text catalogs and full-text indexes is retained. After being upgraded, all full-text indexes are disabled for change tracking and crawls are not started automatically. The catalog will remain empty until you manually issue a full population, after the upgrade completes.  
  
 For information about how to choose the full-text upgrade option, see [Upgrade Full-Text Search](../../relational-databases/search/upgrade-full-text-search.md).  
  
> [!NOTE]  
>  The full-text upgrade option can also be set by using the [sp_fulltext_service](../../relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql.md)upgrade_option action.  
  
 After you attach, restore, or copy a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the database becomes available immediately and is then automatically upgraded. If the database has full-text indexes, the upgrade process either imports, resets, or rebuilds them, depending on the setting of the **Full-Text Upgrade Option** server property. If the upgrade option is set to **Import** or **Rebuild**, the full-text indexes will be unavailable during the upgrade. Depending on the amount of data being indexed, importing can take several hours, and rebuilding can take up to ten times longer. Note also that when the upgrade option is set to **Import**, if a full-text catalog is not available, the associated full-text indexes are rebuilt. For information about viewing or changing the setting of the **Full-Text Upgrade Option** property, see [Manage and Monitor Full-Text Search for a Server Instance](../../relational-databases/search/manage-and-monitor-full-text-search-for-a-server-instance.md).  
  
 **Max Text Replication Size**  
 Specifies the maximum size (in bytes) of **text**, **ntext**, **varchar(max)**, **nvarchar(max)**, **xml**, and **image** data that can be added to a replicated column or captured column in a single INSERT, UPDATE, WRITETEXT, or UPDATETEXT statement. Changing the setting takes effect immediately. For more information, see [Configure the max text repl size Server Configuration Option](../../database-engine/configure-windows/configure-the-max-text-repl-size-server-configuration-option.md).  
  
 **Scan For Startup Procs**  
 Specifies that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will scan for automatic execution of stored procedures at startup. If set to **True**, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] scans for and runs all automatically run stored procedures defined on the server. If set to **False** (the default), no scan is performed. For more information, see [Configure the scan for startup procs Server Configuration Option](../../database-engine/configure-windows/configure-the-scan-for-startup-procs-server-configuration-option.md).  
  
 **Two Digit Year Cutoff**  
 Indicates the highest year number that can be entered as a two-digit year. The year listed and the previous 99 years can be entered as a two-digit year. All other years must be entered as a four-digit year.  
  
 For example, the default setting of 2049 indicates that a date entered as '3/14/49' will be interpreted as March 14, 2049, and a date entered as '3/14/50' will be interpreted as March 14, 1950. For more information, see [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md).  
  
## Network  
 **Network Packet Size**  
 Sets the packet size (in bytes) used across the whole network. The default packet size is 4096 bytes. If an application does bulk-copy operations or sends or receives large amounts of **text** or **image** data, a packet size larger than the default may improve efficiency, because it results in fewer network reads and writes. If an application sends and receives small amounts of information, you can set the packet size to 512 bytes, which is sufficient for most data transfers. For more information, see [Configure the network packet size Server Configuration Option](../../database-engine/configure-windows/configure-the-network-packet-size-server-configuration-option.md).  
  
> [!NOTE]  
>  Do not change the packet size unless you are certain that it will improve performance. For most applications, the default packet size is best.  
  
 **Remote Login Timeout**  
 Specifies the number of seconds [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] waits before returning from a failed remote login attempt. This setting affects connections to OLE DB providers made for heterogeneous queries. The default value is 20 seconds. A value of 0 allows for an infinite wait. For more information, see [Configure the remote login timeout Server Configuration Option](../../database-engine/configure-windows/configure-the-remote-login-timeout-server-configuration-option.md).  
  
 Changing the setting takes effect immediately.  
  
## Parallelism:  
 **Cost Threshold for Parallelism**  
 Specifies the threshold above which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates and runs parallel plans for queries. The cost refers to an estimated elapsed time in seconds required to run the serial plan on a specific hardware configuration. Only set this option on symmetric multiprocessors. For more information, see [Configure the cost threshold for parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-cost-threshold-for-parallelism-server-configuration-option.md).  
  
 **Locks**  
 Sets the maximum number of available locks, thereby limiting the amount of memory [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses for them. The default setting is 0, which allows [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to allocate and deallocate locks dynamically based on changing system requirements.  
  
 Allowing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use locks dynamically is the recommended configuration. For more information, see [Configure the locks Server Configuration Option](../../database-engine/configure-windows/configure-the-locks-server-configuration-option.md).  
  
 **Max Degree of Parallelism**  
 Limits the number of processors (up to a maximum of 64) to use in parallel plan execution. The default value of 0 uses all available processors. A value of 1 suppresses parallel plan generation. A number greater than 1 restricts the maximum number of processors used by a single query execution. If a value greater than the number of available processors is specified, the actual number of available processors is used. For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).  
  
 **Query Wait**  
 Specifies the time in seconds (from 0 through 2147483647) that a query waits for resources before timing out. If the default value of -1 is used, the time-out is calculated as 25 times of the estimated query cost. For more information, see [Configure the query wait Server Configuration Option](../../database-engine/configure-windows/configure-the-query-wait-server-configuration-option.md).  
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)  
  
  
