---
title: "Enhance Merge Replication Performance | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "publications [SQL Server replication], design and performance"
  - "designing databases [SQL Server], replication performance"
  - "Merge Agent, performance"
  - "snapshots [SQL Server replication], performance considerations"
  - "merge replication performance [SQL Server replication]"
  - "subscriptions [SQL Server replication], performance considerations"
  - "performance [SQL Server replication], merge replication"
  - "agents [SQL Server replication], performance"
ms.assetid: f929226f-b83d-4900-a07c-a62f64527c7f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Enhance Merge Replication Performance
  After considering the general performance tips described in [Enhancing General Replication Performance](enhance-general-replication-performance.md), consider these additional areas specific to merge replication.  
  
## Database Design  
  
-   Index columns used in row filters and join filters.  
  
     When you use a row filter on a published article, create an index on each of the columns that is used in the filter's WHERE clause. Without an index, [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] has to read each row in the table to determine whether the row should be included in the partition. With an index, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can quickly locate which rows should be included. The fastest processing takes place if replication can fully resolve the WHERE clause of the filter from the index alone.  
  
     Indexing all the columns used in join filters is also important. Each time the Merge Agent runs, it searches the base table to determine which rows in a parent table and which rows in related tables are included in a partition. Creating an index on the joined columns avoids having [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] read each row in the table every time the Merge Agent runs.  
  
     For more information on filtering, see [Filter Published Data for Merge Replication](../merge/filter-published-data-for-merge-replication.md).  
  
-   Consider overnormalizing tables that include Large Object (LOB) data types.  
  
     When synchronization occurs, the Merge Agent might need to read and transfer the entire data row from a Publisher or Subscriber. If the row contains columns that use LOBs, this process can require additional memory allocation and negatively impact performance even though these columns may not have been updated. To reduce the likelihood that this performance impact occurs, consider putting LOB columns in a separate table using a one-to-one relationship to the rest of the row data. The data types `text`, `ntext`, and `image` are deprecated. If you do include LOBs, we recommend that you use the data types `varchar(max)`, `nvarchar(max)`, `varbinary(max)`, respectively.  
  
## Publication Design  
  
-   Use a publication compatibility level of 90RTM ([!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or a later version).  
  
     Unless one or more Subscribers use a different version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], specify that the publication must support only [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or a later version. This allows the publication to take advantage of new features and performance optimizations.  
  
-   Use appropriate publication retention settings.  
  
     The publication retention period, which is the maximum amount of time before a subscription must be synchronized, determines how long tracking metadata is stored. A high value can affect storage and processing performance. For more information about setting the publication retention period, see [Subscription Expiration and Deactivation](../subscription-expiration-and-deactivation.md).  
  
-   Use download-only articles on those tables that are only changed at the Publisher. For more information, see [Optimize Merge Replication Performance with Download-Only Articles](../merge/optimize-merge-replication-performance-with-download-only-articles.md).  
  
### Filter Design and Use  
  
-   Limit the complexity of row filter clauses.  
  
     Limiting the complexity of the filtering criteria helps improve performance when the Merge Agent is evaluating row changes to send to Subscribers. Avoid using sub-selects within merge row filter clauses. Instead, consider using join filters, which are generally more efficient when used to partition data in one table based on the row filter clause in another table. For more information about filtering, see [Filter Published Data for Merge Replication](../merge/filter-published-data-for-merge-replication.md).  
  
-   Use precomputed partitions with parameterized filters (this feature is used by default). For more information, see [Optimize Parameterized Filter Performance with Precomputed Partitions](../merge/parameterized-filters-optimize-for-precomputed-partitions.md).  
  
     Precomputed partitions impose a number of limits on filtering behavior. If your application cannot adhere to these limitations, set the **keep_partition_changes** option to **True**, which provides a performance benefit. For more information, see [Parameterized Row Filters](../merge/parameterized-filters-parameterized-row-filters.md).  
  
-   Use nonoverlapping partitions if data is filtered but not shared among users.  
  
     Replication can optimize performance for data that is not shared between partitions or subscriptions. For more information, see [Parameterized Row Filters](../merge/parameterized-filters-parameterized-row-filters.md).  
  
-   Do not create complex join filter hierarchies.  
  
     Join filters with five or more tables can significantly impact performance during merge processing. We recommend that if you are generating join filters of five or more tables that you consider other solutions:  
  
    -   Avoid filtering tables that are primarily lookup tables, smaller tables, and tables that are not subject to change. Make those tables part of the publication in their entirety. We recommend that you use join filters only between tables that must be partitioned among Subscribers. For more information, see [Join Filters](../merge/join-filters.md).  
  
    -   Consider denormalizing the database design or using a mapping table if there are a large number of tables in a join. For example, if a sales person needs only the data for her customers, but it requires six joins to associate a customer with a sales person, consider adding a column to the customer table that identifies the sales person. The sales person data is redundant, but the costs of denormalizing the tables somewhat might be outweighed by the performance benefits for replication partitioning.  
  
    -   To improve the performance of precomputed partitions when batches contain lots of data changes, design your application with care. Make sure that changes to data in the parent table in a join filter are made before corresponding changes in the child tables.  
  
-   Set the **join_unique_key** option to **1** if logic allows.  
  
     Setting this parameter to **1** indicates that the relationship between the child and parent tables in a join filter is one to one or one to many. Only set this parameter to **1** if you have a constraint on the joining column in the child table that guarantees uniqueness. If the parameter is set to **1** incorrectly, non-convergence of data can occur. For more information, see [Join Filters](../merge/join-filters.md).  
  
-   Avoid executing batches with lots of changes when you use precomputed partitions.  
  
     When the Merge Agent is run after you run a batch that contains lots of data changes, the agent tries to divide the large batch into smaller batches. During this time, other Merge Agent processes may be blocked. Consider reducing the number of changes in a batch and run the Merge Agent between batches. If this cannot be done, increase the value of **generation_leveling_threshold** for the publication.  
  
## Subscription Considerations  
  
-   Stagger subscription synchronization schedules.  
  
     If a large number of Subscribers synchronize with a Publisher, consider staggering the schedules so that Merge Agents run at different times. For more information, see [Specify Synchronization Schedules](../specify-synchronization-schedules.md).  
  
## Merge Agent Parameters  
 For information about the Merge Agent and its parameters, see [Replication Merge Agent](../agents/replication-merge-agent.md).  
  
-   Upgrade all Subscribers for pull subscriptions to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or a later version.  
  
     Upgrading the Subscriber to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or a later version upgrades the Merge Agent used by the subscriptions at that Subscriber. To take advantage of many of the new features and performance optimizations, the Merge Agent from [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or a later version is required.  
  
-   If a subscription is synchronized over a fast connection and changes are sent from the Publisher and Subscriber, use the **-ParallelUploadDownload** parameter for the Merge Agent.  
  
     [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] introduced a new Merge Agent parameter: **-ParallelUploadDownload**. Setting this parameter allows the Merge Agent to process in parallel the changes uploaded to the Publisher and those downloaded to the Subscriber. This is useful in high volume environments with high network bandwidth. Agent parameters can be specified in agent profiles and on the command line. For more information, see:  
  
    -   [Work with Replication Agent Profiles](../agents/replication-agent-profiles.md)  
  
    -   [View and Modify Replication Agent Command Prompt Parameters &#40;SQL Server Management Studio&#41;](../agents/view-and-modify-replication-agent-command-prompt-parameters.md)  
  
    -   [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md)  
  
-   Consider increasing the value of the **-MakeGenerationInterval** parameter, especially if synchronization involves more uploads from Subscribers than downloads to Subscribers.  
  
-   When you synchronize data rows with a large amount of data, such as rows with LOB columns, Web synchronization can require additional memory allocation and hurt performance. This occurs when the Merge Agent generates an XML message that contains too many data rows with large amounts of data. If the Merge Agent is consuming too many resources during Web synchronization, reduce the number of rows sent in a single message in one of the following ways:  
  
    -   Use the slow link agent profile for the Merge Agent. For more information, see [Replication Agent Profiles](../agents/replication-agent-profiles.md).  
  
    -   Decrease the **-DownloadGenerationsPerBatch** and **-UploadGenerationsPerBatch** parameters for the Merge Agent to a value of 10 or less. The default value of these parameters is 50.  
  
## Snapshot Considerations  
  
-   Create a ROWGUIDCOL column on large tables prior to generating the initial snapshot.  
  
     Merge replication requires that each published table have a ROWGUIDCOL column. If a ROWGUIDCOL column does not exist in the table before the Snapshot Agent creates the initial snapshot files, the agent must first add and populate the ROWGUIDCOL column. To gain a performance advantage when generating snapshots during merge replication, create the ROWGUIDCOL column on each table before publishing. The column can have any name (**rowguid** is used by the Snapshot Agent by default), but must have the following data type characteristics:  
  
    -   A data type of UNIQUEIDENTIFIER.  
  
    -   A default of NEWSEQUENTIALID() or NEWID(). NEWSEQUENTIALID() is recommended because it can provide increased performance when making and tracking changes.  
  
    -   The ROWGUIDCOL property set.  
  
    -   A unique index on the column.  
  
-   Pre-generate snapshots and/or allow Subscribers to request snapshot generation and application the first time they synchronize.  
  
     Use one or both of these options to provide snapshots for publications that use parameterized filters. If you do not specify one of these options, subscriptions are initialized using a series of SELECT and INSERT statements, rather than using the **bcp** utility; this process is much slower. For more information, see [Snapshots for Merge Publications with Parameterized Filters](../snapshots-for-merge-publications-with-parameterized-filters.md).  
  
## Maintenance and Monitoring Considerations  
  
-   Occasionally re-index merge replication system tables.  
  
     As part of maintenance for merge replication, occasionally check the growth of the system tables associated with merge replication: **MSmerge_contents**, **MSmerge_genhistory**, and **MSmerge_tombstone**, **MSmerge_current_partition_mappings**, and **MSmerge_past_partition_mappings**. Periodically re-index these tables. For more information, see [Reorganize and Rebuild Indexes](../../indexes/reorganize-and-rebuild-indexes.md).  
  
-   Monitor synchronization performance using the **Synchronization History** tab in Replication Monitor.  
  
     For merge replication, Replication Monitor displays detailed statistics in the **Synchronization History** tab for each article processed during synchronization, including the amount of time spent in each processing phase (uploading changes, downloading changes, and so on). It can help pinpoint specific tables that are causing slow downs and is the best place to troubleshoot performance issues with merge subscriptions. For more information on viewing detailed statistics, see [View Information and Perform Tasks using Replication Monitor](../monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
  
