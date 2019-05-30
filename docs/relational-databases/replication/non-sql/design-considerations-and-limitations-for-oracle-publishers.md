---
title: "Design Considerations and Limitations for Oracle Publishers | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Oracle publishing [SQL Server replication], design considerations and limitations"
ms.assetid: 8d9dcc59-3de8-4d36-a61f-bc3ca96516b6
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Design Considerations and Limitations for Oracle Publishers
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Publishing from an Oracle database is designed to work nearly identically to publishing from a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database. However, you should be aware of the following limitations and issues:  
  
-   The Oracle Gateway option provides improved performance over the Oracle Complete option; however, this option cannot be used to publish the same table in multiple transactional publications. A table can appear in at most one transactional publication and any number of snapshot publications. If you need to publish the same table in multiple transactional publications, choose the Oracle Complete option.  
  
-   Replication supports publishing tables, indexes, and materialized views. Other objects are not replicated.  
  
-   There are some small differences between the storage and processing of data in Oracle and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases that affect replication.  
  
-   There are a number of differences in how transactional replication features are supported when using an Oracle Publisher.  
  
## Support for Publishing Objects from Oracle  
 Replication supports replicating the following objects from Oracle databases:  
  
-   Tables  
  
-   Index-organized tables  
  
-   Indexes  
  
-   Materialized views (they are replicated as tables)  
  
 The following can be present on published tables but are not replicated:  
  
-   Domain-based indexes  
  
-   Function-based indexes  
  
-   Defaults  
  
-   Check constraints  
  
-   Foreign keys  
  
-   Storage options (tablespaces, clusters, etc.)  
  
 The following objects cannot be replicated:  
  
-   Nested tables  
  
-   Views  
  
-   Packages, package bodies, procedures, and triggers  
  
-   Queues  
  
-   Sequences  
  
-   Synonyms  
  
 For information about supported data types, see [Data Type Mapping for Oracle Publishers](../../../relational-databases/replication/non-sql/data-type-mapping-for-oracle-publishers.md).  
  
## Differences between Oracle and SQL Server  
  
-   Oracle has different maximum size limits for some objects. Any objects created in the Oracle publication database should adhere to the maximum size limits for the corresponding objects in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. For information about limits in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see [Maximum Capacity Specifications for SQL Server](../../../sql-server/maximum-capacity-specifications-for-sql-server.md).  
  
-   By default Oracle object names are created in upper case. Ensure that you supply the names of Oracle objects in upper case when publishing them through a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor if they are upper case on the Oracle database. Failure to specify the objects in the correct case may result in an error message indicating that the object cannot be found.  
  
-   Oracle has a slightly different SQL dialect from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]; row filters should be written in Oracle-compliant syntax.  
  
### Considerations for Large Objects  
 Large object (LOB) data is not stored in the article log table; updates to LOB data are always retrieved directly from the published table. Updates are replicated in transactional publications only if the operation affecting the LOB fires the replication trigger on the replicated table. Oracle triggers fire when rows containing LOBs are inserted or deleted; however updates to LOB columns do not fire triggers. An update to a LOB column will be replicated immediately only if a non-LOB column of the same row is also updated in the same Oracle transaction. If not, the LOB column will be refreshed at the Subscriber when the next update to a non-LOB column in the same row occurs. Ensure that this behavior is acceptable for your application.  
  
 To replicate updates to LOB columns in transactional publications, consider one of the following strategies when writing the application:  
  
-   Delete and reinsert the row(s) within a transaction instead of updating the row: specify the new LOB when re-inserting the row. Because delete and insert both fire triggers, the row will be replicated.  
  
-   Include a non-LOB column in the row update in addition to the LOB column, or update a non-LOB column of the row as part of the same Oracle transaction. In both cases, the update of the non-LOB column ensures that the trigger fires.  
  
 For more information about LOBs, see [Data Type Mapping for Oracle Publishers](../../../relational-databases/replication/non-sql/data-type-mapping-for-oracle-publishers.md).  
  
### Unique Indexes and Constraints  
 For both snapshot and transactional replication, columns contained in unique indexes and constraints (including primary key constraints) must adhere to certain restrictions. If they do not adhere to these restrictions, the constraint or index is not replicated.  
  
-   The maximum number of columns allowed in an index on [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is 16.  
  
-   All columns included in unique constraints must have supported data types. For more information about data types, see [Data Type Mapping for Oracle Publishers](../../../relational-databases/replication/non-sql/data-type-mapping-for-oracle-publishers.md).  
  
-   All columns included in unique constraints must be published (they cannot be filtered).  
  
-   Columns involved in unique constraints or indexes should not be null.  
  
 Also consider the following issues:  
  
-   Oracle and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] treat NULL differently: Oracle permits multiple rows with NULL values for columns that allow NULL and are included in unique constraints or indexes. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] enforces uniqueness by only permitting a single row with a NULL value for the same column. You cannot publish a unique constraint or index that allows NULL because a constraint violation would occur on the Subscriber if the published table contains multiple rows with NULL values for any of the columns included in the index or constraint.  
  
-   When testing for uniqueness, trailing blanks in a field are ignored by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] but not by Oracle.  
  
 As in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] transactional replication, tables in Oracle transactional publications require a primary key; the primary key must be unique based on the rules specified above. If the primary key does not adhere to the rules outlined in the previous bullets, the table cannot be published for transactional replication.  
  
## Differences between Oracle Publishing and Standard Transactional Replication  
  
-   An Oracle Publisher cannot have the same name as: its [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor; any of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Publishers that use the Distributor; or any Subscribers that receive the publication. Publications serviced by the same Distributor must each have a unique name.  
  
-   A table published in an Oracle publication cannot receive replicated data. Therefore, Oracle publishing does not support: publications with immediate updating or queued updating subscriptions; or topologies in which publication tables also act as subscription tables, such as peer-to-peer and bidirectional replication.  
  
-   Primary key to foreign key relationships in the Oracle database are not replicated to Subscribers. However, the relationships are maintained in the data as changes are delivered.  
  
-   Standard transactional publications support tables of up to 1000 columns. Oracle transactional publications support 995 columns (replication adds five columns to each published table).  
  
-   Collate clauses are added to the CREATE TABLE statements to enable case sensitive comparisons, which is important for primary keys and unique constraints. This behavior is controlled with the schema option 0x1000, which is specified with the **@schema_option** parameter of [sp_addarticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md).  
  
-   If you use stored procedures to configure or maintain an Oracle Publisher, do not put the procedures inside an explicit transaction. This is not supported over the linked server used to connect to the Oracle Publisher.  
  
-   If you create a pull subscription to an Oracle publication with a wizard, you must use the New Subscription Wizard supplied with [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions. For previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], you can, however, use the stored procedure and SQL-DMO interfaces to setup pull subscriptions to Oracle publications.  
  
-   If you use stored procedures to propagate changes to Subscribers (the default), be aware that the MCALL syntax is supported, but it has different behavior when the publication is from an Oracle Publisher. Typically MCALL provides a bitmap that shows which columns were updated at the Publisher. With an Oracle publication, the bitmap always shows that all columns were updated. For more information about using stored procedures, see [Specify How Changes Are Propagated for Transactional Articles](../../../relational-databases/replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).  
  
### Transactional Replication Feature Support  
  
-   Oracle publications do not support all of the schema options that SQL Server publications support. For more information on schema options, see [sp_addarticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md).  
  
-   Subscribers to Oracle publications cannot use immediate updating or queued updating subscriptions, or be nodes in a peer-to-peer or bidirectional topology.  
  
-   Subscribers to Oracle publications cannot be automatically initialized from a backup.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports two types of validation: binary and rowcount. Oracle Publishers support rowcount validation. For more information, see [Validate Replicated Data](../../../relational-databases/replication/validate-data-at-the-subscriber.md).  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] offers two snapshot formats: native bcp-mode and character-mode. Oracle Publishers support character mode snapshots.  
  
-   Schema changes to published Oracle tables are not supported. To make schema changes, first drop the publication, make the changes, and then re-create the publication and any subscriptions.  
  
    > [!NOTE]  
    >  If the schema changes and the subsequent drop and re-creation of the publication and subscriptions are performed at a time when no activity is occurring on the published tables, you can specify the option 'replication support only' for the subscriptions. This allows them to be synchronized without having to copy a snapshot to each Subscriber. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md).  
  
### Replication Security Model  
 The security model for Oracle publishing is the same as the security model for standard transactional replication, with the following exceptions:  
  
-   The account under which the Snapshot Agent and Log Reader Agent make connections from the Distributor to the Publisher is specified through one of the following methods:  
  
    -   The **@security_mode** parameter of [sp_adddistpublisher &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md) (you also specify values for **@login** and **@password** if Oracle Authentication is used)  
  
    -   In the **Connect to Server** dialog box in SQL Server Management Studio, which you use when you configure the Oracle Publisher at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor.  
  
     In standard transactional replication, the account is specified with [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md) and [sp_addlogreader_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addlogreader-agent-transact-sql.md).  
  
-   The account under which the Snapshot Agent and Log Reader Agent make connections cannot be changed with [sp_changedistpublisher &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changedistpublisher-transact-sql.md) or through a property sheet, but the password can be changed.  
  
-   If you specify a value of 1 (Windows Integrated Authentication) for the **@security_mode** parameter of [sp_adddistpublisher &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md):  
  
    -   The process account and password used for both the Snapshot Agent and Log Reader Agent (the **@job_login** and **@job_password** parameters of [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md) and [sp_addlogreader_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addlogreader-agent-transact-sql.md)) must be the same as the account and password used to connect to the Oracle Publisher.  
  
    -   You cannot change the **@job_login** parameter through [sp_changepublication_snapshot &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changepublication-snapshot-transact-sql.md) or [sp_changelogreader_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changelogreader-agent-transact-sql.md), but the password can be changed.  
  
 For more information about replication security, see [View and modify replication security settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
## See Also  
 [Administrative Considerations for Oracle Publishers](../../../relational-databases/replication/non-sql/administrative-considerations-for-oracle-publishers.md)   
 [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md)   
 [Oracle Publishing Overview](../../../relational-databases/replication/non-sql/oracle-publishing-overview.md)  
  
  
