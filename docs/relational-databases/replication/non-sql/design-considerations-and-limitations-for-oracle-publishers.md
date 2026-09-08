---
title: Considerations and Limitations (Oracle Publishers)
description: Oracle Publisher replication in SQL Server has key design limits. Review supported objects, LOB behavior, index rules, and security differences before you publish.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: concept-article
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "Oracle publishing [SQL Server replication], design considerations and limitations"
---
# Design considerations and limitations for Oracle publishers
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Publishing from an Oracle database works almost the same as publishing from a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database. However, be aware of the following limitations and issues:  
  
-   The Oracle Gateway option provides better performance than the Oracle Complete option. However, you can't use the Oracle Gateway option to publish the same table in multiple transactional publications. A table can appear in only one transactional publication but can appear in any number of snapshot publications. If you need to publish the same table in multiple transactional publications, choose the Oracle Complete option.  
  
-   Replication supports publishing tables, indexes, and materialized views. It doesn't replicate other objects.  
  
-   Some small differences between the storage and processing of data in Oracle and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases affect replication.  
  
-   There are differences in how transactional replication features are supported when you use an Oracle Publisher.  
  
## Support for publishing objects from Oracle  
 Replication supports replicating the following objects from Oracle databases:  
  
-   Tables  
  
-   Index-organized tables  
  
-   Indexes  
  
-   Materialized views (replicated as tables)  
  
 The following objects can be present on published tables but aren't replicated:  
  
-   Domain-based indexes  
  
-   Function-based indexes  
  
-   Defaults  
  
-   Check constraints  
  
-   Foreign keys  
  
-   Storage options (tablespaces, clusters, and so on)  
  
 The following objects can't be replicated:  
  
-   Nested tables  
  
-   Views  
  
-   Packages, package bodies, procedures, and triggers  
  
-   Queues  
  
-   Sequences  
  
-   Synonyms  
  
 For information about supported data types, see [Data Type Mapping for Oracle Publishers](../../../relational-databases/replication/non-sql/data-type-mapping-for-oracle-publishers.md).  
  
## Differences between Oracle and SQL Server  
  
-   Oracle has different maximum size limits for some objects. Any objects you create in the Oracle publication database should adhere to the maximum size limits for the corresponding objects in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. For information about limits in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see [Maximum Capacity Specifications for SQL Server](../../../sql-server/maximum-capacity-specifications-for-sql-server.md).  
  
-   By default, Oracle creates object names in uppercase. Ensure that you supply the names of Oracle objects in uppercase when publishing them through a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor if they are uppercase on the Oracle database. Failure to specify the objects in the correct case might result in an error message indicating that the object can't be found.  
  
-   Oracle uses a slightly different SQL dialect from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]; write row filters in Oracle-compliant syntax.  
  
### Considerations for large objects  
  The article log table doesn't store large object (LOB) data. Updates to LOB data are always retrieved directly from the published table. Updates are replicated in transactional publications only if the operation affecting the LOB fires the replication trigger on the replicated table. Oracle triggers fire when rows containing LOBs are inserted or deleted. However, updates to LOB columns don't fire triggers. An update to a LOB column is replicated immediately only if a non-LOB column of the same row is also updated in the same Oracle transaction. If not, the LOB column is refreshed at the Subscriber when the next update to a non-LOB column in the same row occurs. Ensure that this behavior is acceptable for your application.    
  
 To replicate updates to LOB columns in transactional publications, consider one of the following strategies when writing the application:  
  
-   Delete and reinsert the rows within a transaction instead of updating the row: specify the new LOB when reinserting the row. Because delete and insert both fire triggers, the row is replicated.  
  
-   Include a non-LOB column in the row update in addition to the LOB column, or update a non-LOB column of the row as part of the same Oracle transaction. In both cases, the update of the non-LOB column ensures that the trigger fires.  
  
 For more information about LOBs, see [Data Type Mapping for Oracle Publishers](../../../relational-databases/replication/non-sql/data-type-mapping-for-oracle-publishers.md).  
  
### Unique indexes and constraints  
 For both snapshot and transactional replication, columns in unique indexes and constraints (including primary key constraints) must follow certain restrictions. If they don't follow these restrictions, the constraint or index isn't replicated.  
  
-   You can include up to 16 columns in an index on [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
-   All columns in unique constraints must have supported data types. For more information about data types, see [Data Type Mapping for Oracle Publishers](../../../relational-databases/replication/non-sql/data-type-mapping-for-oracle-publishers.md).  
  
-   You must publish all columns in unique constraints (you can't filter them).  
  
-   Columns in unique constraints or indexes shouldn't be null.  
  
 Also consider the following issues:  
  
-   Oracle and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] treat NULL differently: Oracle permits multiple rows with NULL values for columns that allow NULL and are included in unique constraints or indexes. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] enforces uniqueness by only permitting a single row with a NULL value for the same column. You can't publish a unique constraint or index that allows NULL because a constraint violation would occur on the Subscriber if the published table contains multiple rows with NULL values for any of the columns included in the index or constraint.  
  
-   When testing for uniqueness, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] ignores trailing blanks in a field but Oracle doesn't.  
  
 As in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] transactional replication, tables in Oracle transactional publications require a primary key. The primary key must be unique based on the rules specified earlier. If the primary key doesn't follow these rules, you can't publish the table for transactional replication.  
  
## Differences between Oracle Publishing and Standard Transactional Replication  
  
-   An Oracle Publisher cannot have the same name as: its [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor; any of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Publishers that use the Distributor; or any Subscribers that receive the publication. Publications serviced by the same Distributor must each have a unique name.  
  
-   A table published in an Oracle publication cannot receive replicated data. Therefore, Oracle publishing does not support: publications with immediate updating or queued updating subscriptions; or topologies in which publication tables also act as subscription tables, such as peer-to-peer and bidirectional replication.  
  
-   Primary key to foreign key relationships in the Oracle database are not replicated to Subscribers. However, the relationships are maintained in the data as changes are delivered.  
  
-   Standard transactional publications support tables of up to 1000 columns. Oracle transactional publications support 995 columns (replication adds five columns to each published table).  
  
-   Collate clauses are added to the CREATE TABLE statements to enable case sensitive comparisons, which is important for primary keys and unique constraints. This behavior is controlled with the schema option 0x1000, which is specified with the `@schema_option` parameter of [sp_addarticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md).  
  
-   If you use stored procedures to configure or maintain an Oracle Publisher, do not put the procedures inside an explicit transaction. This is not supported over the linked server used to connect to the Oracle Publisher.  
  
-   If you create a pull subscription to an Oracle publication with a wizard, you must use the New Subscription Wizard supplied with [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions. For previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], you can, however, use the stored procedure and SQL-DMO interfaces to setup pull subscriptions to Oracle publications.  
  
-   If you use stored procedures to propagate changes to Subscribers (the default), be aware that the MCALL syntax is supported, but it has different behavior when the publication is from an Oracle Publisher. Typically MCALL provides a bitmap that shows which columns were updated at the Publisher. With an Oracle publication, the bitmap always shows that all columns were updated. For more information about using stored procedures, see [Specify How Changes Are Propagated for Transactional Articles](../../../relational-databases/replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).  
  
### Transactional replication feature support  
  
-   Oracle publications don't support all of the schema options that SQL Server publications support. For more information about schema options, see [sp_addarticle (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md).  
  
-   Subscribers to Oracle publications can't use immediate updating or queued updating subscriptions, or be nodes in a peer-to-peer or bidirectional topology.  
  
-   Subscribers to Oracle publications can't be automatically initialized from a backup.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports two types of validation: binary and rowcount. Oracle Publishers support rowcount validation. For more information, see [Validate Replicated Data](../../../relational-databases/replication/validate-data-at-the-subscriber.md).  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] offers two snapshot formats: native bcp-mode and character-mode. Oracle Publishers support character mode snapshots.  
  
-   Schema changes to published Oracle tables aren't supported. To make schema changes, first drop the publication, make the changes, and then re-create the publication and any subscriptions.  
  
    > [!NOTE]  
    >  If you make schema changes and drop and re-create the publication and subscriptions when no activity is occurring on the published tables, you can specify the option `replication support only` for the subscriptions. This option synchronizes them without copying a snapshot to each Subscriber. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md).  
  
### Replication security model  
 The replication security model for Oracle publishing is the same as the security model for standard transactional replication, with the following exceptions:  
  
-   You specify the account under which the Snapshot Agent and Log Reader Agent make connections from the Distributor to the Publisher through one of the following methods:  
  
    -   The `@security_mode` parameter of [sp_adddistpublisher &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md) (also specify values for `@login` and `@password` if Oracle Authentication is used)  
  
    -   The **Connect to Server** dialog box in SQL Server Management Studio, which you use when you configure the Oracle Publisher at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor.  
  
     In standard transactional replication, specify the account with [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md) and [sp_addlogreader_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addlogreader-agent-transact-sql.md).  
  
-   You can't change the account under which the Snapshot Agent and Log Reader Agent make connections by using [sp_changedistpublisher &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changedistpublisher-transact-sql.md) or through a property sheet, but you can change the password.  
  
-   If you specify a value of 1 (Windows Integrated Authentication) for the `@security_mode` parameter of [sp_adddistpublisher &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md):  
  
    -   The process account and password used for both the Snapshot Agent and Log Reader Agent (the `@job_login` and `@job_password` parameters of [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md) and [sp_addlogreader_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addlogreader-agent-transact-sql.md)) must be the same as the account and password used to connect to the Oracle Publisher.  
  
    -   You can't change the `@job_login` parameter through [sp_changepublication_snapshot &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changepublication-snapshot-transact-sql.md) or [sp_changelogreader_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changelogreader-agent-transact-sql.md), but you can change the password.  
  
 For more information about replication security, see [View and modify replication security settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
## Related content

- [Administrative Considerations for Oracle Publishers](administrative-considerations-for-oracle-publishers.md)
- [Configure an Oracle Publisher](configure-an-oracle-publisher.md)
- [Oracle Publishing Overview](oracle-publishing-overview.md)
