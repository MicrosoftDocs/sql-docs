---
title: "Change data capture (CDC) with Azure SQL Database"
description: "Learn about change data capture (CDC) in Azure SQL Database, which records insert, update, and delete activity that applies to a table."
author: croblesm
ms.author: roblescarlos
ms.reviewer: "mathoma"
ms.date: "09/11/2023"
ms.service: sql-database
ms.subservice: replication
ms.topic: conceptual
ms.custom: intro-overview
helpviewer_keywords:
  - "change data capture"
  - "change data capture for Azure SQL Database"
---

# Change data capture (CDC) with Azure SQL Database
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](change-data-capture-overview.md)

In this article, learn about how change data capture (CDC) is implemented in Azure SQL Database to record activity on a database when tables and rows have been modified. For details about the CDC feature, including how it's implemented in SQL Server and Azure SQL Managed Instance, see [CDC with SQL Server](/sql/relational-databases/track-changes/about-change-data-capture-sql-server). 

## Overview

A good example of a data consumer that this technology targets is an extraction, transformation, and loading (ETL) application. An ETL application incrementally loads change data from [!INCLUDE [ssnoversion-md](../../docs/includes/ssnoversion-md.md)] source tables to a data warehouse or data mart. Although the representation of the source tables within the data warehouse must reflect changes in the source tables, an end-to-end technology that refreshes a replica of the source isn't appropriate. Instead, you need a reliable stream of change data that is structured so that consumers can apply it to dissimilar target representations of the data. [!INCLUDE [ssnoversion-md](../../docs/includes/ssnoversion-md.md)] change data capture provides this technology.  

In Azure SQL Database, a change data capture scheduler replaces the SQL Server Agent jobs that invoke stored procedures to start periodic capture and cleanup of the change data for the source tables. The scheduler runs the capture and cleanup processes automatically in the scope of the Azure SQL Database, without any external dependency for reliability or performance purposes. Users retain the option to manually initiate capture and cleanup processes as needed.

To learn more about Change Data Capture, refer to this Data Exposed episode:
> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Track-and-Record-Data-Changes-with-Change-Data-Capture-CDC-in-Azure-SQL/player?WT.mc_id=dataexposed-c9-niner]

## Data flow

The following illustration shows the principal data flow for change data capture.  

:::image type="content" source="media/change-data-capture-overview/cdc-sql-db.png" alt-text="Flow chart depicts data flow for change data capture." lightbox="media/change-data-capture-overview/cdc-sql-db-highres.png":::

## Prerequisites  

### Permissions

The `db_owner` role is required to enable change data capture for Azure SQL Database.  

### Azure SQL Database compute requirements

CDC can be enabled only for databases provisioned starting in the **GP_Gen5_4 SLO or higher**. For more information about the Azure SQL Database vCore purchasing model, see [Resource limits for single databases using the vCore purchasing model](/azure/azure-sql/database/resource-limits-vcore-single-databases).  

> [!WARNING]
> If your Azure SQL Database was provisioned under the DTU-based pricing model. CDC can only be enabled on database tiers **S3 and higher**. Subcore (Basic, S0, S1, S2) Azure SQL Databases aren't supported for CDC. For more information about Azure SQL Database DTU purchasing model, see [**Resource limits for single databases using the DTU purchasing model - Azure SQL Database**](/azure/azure-sql/database/resource-limits-vcore-single-databases).  

## Enable CDC for Azure SQL Database

Before you can create a capture instance for individual tables, you must enable Change Data Capture (CDC) for your Azure SQL Database.

To enable CDC, connect to your Azure SQL Database through Azure Data Studio or SQL Server Management Studio (SSMS). Open a new query window, then enable CDC by running the following T-SQL:

```sql
EXEC sys.sp_cdc_enable_db;
GO
```
> [!NOTE]
> To determine if a database is already enabled, query the **is_cdc_enabled** column in the **sys.databases** catalog view.  

When a database is enabled for change data capture, the **cdc** schema, **cdc** user, metadata tables, and other system objects are created for the database. The **cdc** schema contains the change data capture metadata tables and, after source tables are enabled for change data capture, the individual change tables serve as a repository for change data. The **cdc** schema also contains associated system functions used to query for change data.  

> [!IMPORTANT]    
> Change data capture requires exclusive use of the **cdc** schema and **cdc** user. If either a schema or a database user named *cdc* currently exists in a database, the database cannot be enabled for change data capture until the schema and or user are dropped or renamed.

## Enable CDC for a table  

After enabling CDC for your Azure SQL Database, you can proceed to enable CDC at the table level by selecting one or more tables to track data changes. Members of the **db_owner** fixed database role can create a capture instance for individual source tables by using the stored procedure **sys.sp_cdc_enable_table**. 

To enable CDC for a table, once again connect to your Azure SQL Database through Azure Data Studio or SQL Server Management Studio (SSMS). Open a new query window, then run the following T-SQL:

```sql  
EXEC sys.sp_cdc_enable_table  
    @source_schema = N'SchemaName',  
    @source_name   = N'TableName',  
    @role_name     = NULL
GO  
```
> [!IMPORTANT]
> Notice the previous example does not use an explicit @role_name setting the parameter to NULL, but you can use a gating role to limit access to the change data.

> [!NOTE]
> To determine whether a source table has already been enabled for change data capture, examine the is_tracked_by_cdc column in the **sys.tables** catalog view.  

## Disable CDC for Azure SQL Database

Disabling CDC for your Azure SQL Database removes all associated change data capture metadata, including the **cdc** user and schema and the external scheduler capture and cleanup processes. However, any gating roles created by change data capture won't be removed automatically and must be explicitly deleted.

> [!NOTE]
> To determine if a database is enabled, query the **is_cdc_enabled** column in the sys.databases catalog view.  

It isn't necessary to disable individual tables before you disable CDC at the database level. Once again connect to your Azure SQL Database through Azure Data Studio or SQL Server Management Studio (SSMS). Open a new query window, then run the following T-SQL:

```sql  
EXEC sys.sp_cdc_disable_db;
GO  
```

> [!IMPORTANT]  
> If a change data capture enabled database is dropped, change data capture jobs are automatically removed. See the Disable Database for change data capture template for an example of disabling a database.  

## CDC capture and cleanup in Azure SQL Database

In Azure SQL Database, a change data capture scheduler takes the place of the SQL Server Agent that invokes stored procedures to start periodic capture and cleanup of the change data capture tables.  

The scheduler runs capture and cleanup automatically within the SQL Database, without any external dependency for reliability or performance. Users still have the option to run capture and cleanup manually on demand using the [sp_cdc_scan](/sql/relational-databases/system-stored-procedures/sys-sp-cdc-cleanup-change-table-transact-sql) and [sp_cdc_cleanup_change_tables](/sql/relational-databases/system-stored-procedures/sys-sp-cdc-cleanup-change-table-transact-sql) procedures.  

The CDC capture job runs every 20 seconds, and the cleanup job runs every hour.

Azure SQL Database includes two dynamic management views to help you monitor change data capture: [sys.dm_cdc_log_scan_sessions](/sql/relational-databases/system-dynamic-management-views/change-data-capture-sys-dm-cdc-log-scan-sessions) and [sys.dm_cdc_errors](/sql/relational-databases/system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors).  

## Performance considerations and recommendations

Enabling change data capture (CDC) on Azure SQL Database has a performance effect comparable to enabling CDC on SQL Server or Azure SQL Managed Instance. However, several factors influence the performance effect when enabling CDC, including:

* The number of CDC-enabled tables in your Azure SQL Database.

* Frequency of changes in the tracked tables, or volume of transactions. Active transactions continue to hold the transaction log truncation until the transaction commits and CDC scan catches up, or the transaction aborts. This might result in the transaction log filling up more than usual and should be monitored so that the transaction log doesn't fill.

* Make sure there's free space available in the source database, as CDC artifacts (for example, CT tables, cdc_jobs etc.) are stored in the same database.

* Whether the database is a single Azure SQL Database or part of an elastic pool.

* Databases in an elastic pool share resource among them (such as disk space), therefore enabling CDC on multiple databases runs the risk of reaching the maximum size of the elastic pool disk size. Monitor resources such as CPU, memory and log throughput. For more information, see [Resource management in dense elastic pools](/azure/azure-sql/database/elastic-pool-resource-management).

* When dealing with databases in elastic pools, it's crucial to not only consider the count of tables with CDC enabled but also take into account the number of databases to which those tables belong. We suggest assessing your workload and taking the required measures as scaling the elastic pool. For more information, see [Scale elastic pool resources in Azure SQL Database](/azure/azure-sql/database/elastic-pool-scale).

> [!IMPORTANT]
> Consider all these considerations as general guidance. In order to offer precise guidance for optimizing performance, we require additional information about each specific workload.

In addition to the multiple aspects that can affect the performance of your Azure SQL Database CDC-enabled database. Consider the following recommendations as good practices to use before/after enabling CDC for your Azure SQL Database:

* Test your workload thoroughly before enabling CDC on databases in production. That helps you determine which SLO fits better for your workload. For more information about Azure SQL Database compute sizes, see [Service tiers](/azure/azure-sql/database/sql-database-paas-overview#service-tiers).

* Consider scaling the number of vCores or transitioning to a higher database tier, such as Hyperscale, to maintain the previous performance level once CDC has been enabled on your Azure SQL Database. For more information, see [vCore purchasing model - Azure SQL Database](/azure/azure-sql/database/service-tiers-sql-database-vcore) and [Hyperscale service tier](/azure/azure-sql/database/service-tier-hyperscale)

* Monitor space utilization closely, for more information, see [Manage file space for databases in Azure SQL Database](/azure/azure-sql/database/file-space-manage)

* Monitor log generation rate, for more information, see [Resource consumption by user workloads and internal processes](/azure/azure-sql/database/resource-limits-logical-server#resource-consumption-by-user-workloads-and-internal-processes).  

* The [CDC scan and cleanup processes](#cdc-capture-and-cleanup-in-azure-sql-database) are part of your regular database workload (also consuming resources). Depending on the volume of transactions, performance degradation can be substantial due to the scan and cleanup processes not keeping up with the workload as entire rows are added to change tables and for update operations, preimage is also included. We suggest assessing your workload and taking the required measures according to the previous recommendations.  

> [!IMPORTANT]
> The scheduler runs capture and cleanup automatically within the SQL Database, without any external dependency for reliability or performance. **The CDC capture job runs every 20 seconds, and the cleanup job runs every hour**.

* To prevent an increase in latency, it's advisable to ensure that the number of CDC-enabled databases doesn't surpass the count of vCores allocated to an elastic pool. To learn more, see [Resource management in dense elastic pools](/azure/azure-sql/database/elastic-pool-resource-management).  

> [!NOTE]
> Make sure testing your workload thoroughly before enabling CDC on databases. If you experience performance degradation, asses your workload again and consider scaling the number of vCores or transitioning to a higher database tier.

* Based on your workload and performance level, consider changing CDC retention period to a smaller number the default three days. This helps ensure that the CDC cleanup process can keep pace with changes in the change table. In practice, maintaining a lower retention period while monitoring the database size is a good practice.

* **No Service Level Agreement (SLA)** is provided for when changes are populated to the change tables. Subsecond latency is also not supported.

> [!NOTE]  
> In Azure SQL Database, the Agent Jobs are replaced by an scheduler which runs capture and cleanup automatically. For more information, see the [**CDC capture and cleanup in Azure SQL Database**](#cdc-capture-and-cleanup-in-azure-sql-database) section in this article.

## Known issues and limitations

### Aggressive log truncation  

When you enable change data capture (CDC) on Azure SQL Database, the aggressive log truncation feature of Accelerated Database Recovery (ADR) is disabled. This is because the CDC scan accesses the database transaction log. Active transactions continue to hold the transaction log truncation until the transaction commits and CDC scan catches up, or transaction aborts. This might result in the transaction log filling up more than usual and should be monitored so that the transaction log doesn't fill.

When enabling CDC, we recommend using the Resumable index option. Resumable index create or rebuild doesn't require you to keep open a long-running transaction, allowing log truncation during this operation and better log space management. For more information, see [Guidelines for online index operations - Resumable Index considerations](/sql/relational-databases/indexes/guidelines-for-online-index-operations#resumable-index-considerations)

### Azure SQL Database service tier

CDC can be enabled only for databases provisioned starting in the **GP_Gen5_4 SLO or higher**. For more information about the Azure SQL Database vCore purchasing model, see [Resource limits for single databases using the vCore purchasing model](/azure/azure-sql/database/resource-limits-vcore-single-databases).  

> [!WARNING]
> If your Azure SQL Database was provisioned under the DTU-based pricing model. CDC can only be enabled on database tiers **S3 and higher**. Subcore (Basic, S0, S1, S2) Azure SQL Databases aren't supported for CDC. For more information about Azure SQL Database DTU purchasing model, see [**Resource limits for single databases using the DTU purchasing model - Azure SQL Database**](/azure/azure-sql/database/resource-limits-vcore-single-databases).  

> Dbcopy from database tiers higher than S3 having CDC enabled to a subcore SLO presently retains the CDC artifacts, but CDC artifacts may be removed in the future.

### Azure SQL Database log limits - Single database

Accelerated Database Recovery (ADR) aggressively truncates the transaction log of your Azure SQL Database, even when there are active long-running transactions. However, it's essential to understand that ADR and Change Data Capture (CDC) aren't fully compatible when enabling CDC on Azure SQL Database. This is because the CDC scan actively accesses and interacts with the database transaction log, which can conflict with Accelerated Database Recovery's (ADR) aggressive log truncation behavior.  

To prevent scalability and space management issues, we recommend closely monitoring your Azure SQL Database. Also consider scaling to a higher database tier, which allows your transaction log to grow according to your workload needs.

> [!IMPORTANT]
> CDC can be enabled only for databases provisioned starting in the **GP_Gen5_4 SLO or higher**. For more information about the Azure SQL Database vCore purchasing model, see [Resource limits for single databases using the vCore purchasing model](/azure/azure-sql/database/resource-limits-vcore-single-databases).  
>
> If your Azure SQL Database was provisioned under the DTU-based pricing model. CDC can only be enabled on database tiers **S3 and higher**. Subcore (Basic, S0, S1, S2) Azure SQL Databases aren't supported for CDC.  

The following section contains the maximum data and transaction log size limits by compute size for Azure SQL Database:

#### General Purpose (Provisioned compute) - Standard-series (Gen5)

| SLO      | Max data size (GB) | Max log size (GB) |
| ----------- | ----------- | ----------- |
| GP_Gen5_4 | 1024 | 307 |
| GP_Gen5_6 | 1536 | 461 |
| GP_Gen5_8 | 2048 | 461 |
| GP_Gen5_10 | 2048 | 461 |

#### General Purpose (Serverless compute) - Standard-series (Gen5)

| SLO      | Max data size (GB) | Max log size (GB) |
| ----------- | ----------- | ----------- |
| GP_S_Gen5_4 | 1024 | 307 |
| GP_S_Gen5_6 | 1024 | 307 |
| GP_S_Gen5_8 | 2048 | 614 |

#### Business Critical (Provisioned compute) - Standard-series (Gen5)

| SLO      | Max data size (GB) | Max log size (GB) |
| ----------- | ----------- | ----------- |
| BC_Gen5_4 | 1024 | 307 |
| BC_Gen5_6 | 1536 | 461 |
| BC_Gen5_8 | 2048 | 461 |
| BC_Gen5_10 | 2048 | 461 |

For detailed information about Azure SQL Database (single database) compute sizes (SLO) see [Resource limits for single databases using the vCore purchasing model](/azure/azure-sql/database/resource-limits-vcore-single-databases) and [Resource limits for single databases using the DTU purchasing model - Azure SQL Database](/azure/azure-sql/database/resource-limits-dtu-single-databases).

> [!WARNING]
> In case your workload demands a higher overall performance due to higher transaction log throughput and faster transaction commit times regardless of data volumes we recommend the Azure SQL Database Hyperscale service tier. For more information, see [Hyperscale service tier](/azure/azure-sql/database/service-tier-hyperscale)

### Azure SQL Database log limits - Elastic pool

Accelerated Database Recovery (ADR) aggressively truncates the transaction log of your Azure SQL Database, even when there are active long-running transactions. However, it's essential to be aware that ADR and Change Data Capture (CDC) aren't fully compatible when enabling CDC on Azure SQL Database. This is because the CDC scan actively accesses and interacts with the database transaction log, which can conflict with Accelerated Database Recovery's (ADR) aggressive log truncation behavior.  

To prevent scalability and space management issues, we recommend closely monitoring your Azure SQL Database. Also consider scaling to a higher database tier, which will allow your transaction log to grow according to your workload needs.

> [!IMPORTANT]
> CDC can be enabled only for databases provisioned starting in the **GP_Gen5_4 SLO or higher**. For more information about the Azure SQL Database vCore purchasing model, see [Resource limits for single databases using the vCore purchasing model](/azure/azure-sql/database/resource-limits-vcore-single-databases).  
>
> If your Azure SQL Database was provisioned under the DTU-based pricing model. CDC can only be enabled on database tiers **S3 and higher**. Subcore (Basic, S0, S1, S2) Azure SQL Databases aren't supported for CDC.  

The following section contains the maximum data and transaction log size limits by compute size for Azure SQL Databases in an elastic pool:  

#### General Purpose (Provisioned compute) - Standard-series (Gen5)

| SLO      | Max data size (GB) | Max log size (GB) |
| ----------- | ----------- | ----------- |
| GP_Gen5_4 | 756 | 227 |
| GP_Gen5_6 | 1536 | 461 |
| GP_Gen5_8 | 2048 | 461 |
| GP_Gen5_10 | 2048 | 461 |

#### Business Critical (Provisioned compute) - Standard-series (Gen5)

| SLO      | Max data size (GB) | Max log size (GB) |
| ----------- | ----------- | ----------- |
| BC_Gen5_4 | 1024 | 307 |
| BC_Gen5_6 | 1536 | 307 |
| BC_Gen5_8 | 2048 | 461 |
| BC_Gen5_10 | 2048 | 461 |
| BC_Gen5_12 | 3072 | 922 |

For detailed information about Azure SQL Database (single database) compute sizes (SLO) see [Resource limits for elastic pools using the vCore purchasing model](/azure/azure-sql/database/resource-limits-vcore-elastic-pools) and [Resource limits for elastic pools using the DTU purchasing model](/azure/azure-sql/database/resource-limits-dtu-elastic-pools).

> [!WARNING]
> In case your workload demands a higher overall performance due to higher transaction log throughput and faster transaction commit times regardless of data volumes we recommend the Azure SQL Database Hyperscale service tier. For more information, see [Hyperscale service tier](/azure/azure-sql/database/service-tier-hyperscale)

### Capture and Cleanup Customization on Azure SQL Databases

Configuring the frequency of the capture and the cleanup processes for CDC in Azure SQL Databases isn't possible. The scheduler runs capture and cleanup automatically.

### Enabling CDC fails on restored Azure SQL DB created with Microsoft Azure Active Directory (Azure AD)

Enabling CDC fails if you create a database in Azure SQL Database as a Microsoft Azure Active Directory (Azure AD) user and don't enable CDC, then restore the database and enable CDC on the restored database.

To resolve this issue, follow these steps:  

* Using your Azure AD Admin, connect to your Azure SQL Database through Azure Data Studio or SQL Server Management Studio (SSMS)
* Run the following T-SQL

```sql
ALTER AUTHORIZATION ON DATABASE::[<restored_db_name>] TO [<azuread_admin_login_name>];
EXEC sys.sp_cdc_enable_db
```

### Microsoft Azure Active Directory (Azure AD)

If you create a database in Azure SQL Database as a Microsoft Azure Active Directory (Azure AD) user and enable change data capture (CDC) on it, a SQL user (for example, even sysadmin role) won't be able to disable/make changes to CDC artifacts. However, another Azure AD user is able to enable/disable CDC on the same database.

Similarly, if you create an Azure SQL Database as a SQL user, enabling/disabling change data capture as an Azure AD user doesn't work.

### Point-in-time restore (PITR)

If you enable CDC on your database as a Microsoft Azure Active Directory (Azure AD) user, it isn't possible to Point-in-time restore (PITR) to a subcore SLO. It's recommended that you restore the database to the same as the source or higher SLO, and then disable CDC if necessary.

## Troubleshooting

This section provides guidance and troubleshooting steps associated with CDC on Azure SQL Database. CDC-related errors may obstruct the proper functioning of the capture process and lead to the expansion of the database transaction log.

To examine these errors, you can query the dynamic management view [sys.dm_cdc_errors](/sql/relational-databases/system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors). If the **sys.dm_cdc_errors** dynamic management view returns any errors,  refer to the following section to understand the mitigation steps.

> [!NOTE]
> For more information on a particular error code, see [Database Engine events and errors](/sql/relational-databases/errors-events/database-engine-events-and-errors).  

### Metadata modified  

#### Error 200/208 - Invalid object name  

* **Cause**: The error might occur when CDC metadata has been dropped. For Change data capture (CDC) to function properly, you shouldn't manually modify any CDC metadata such as CDC schema, change tables, CDC system stored procedures, default `cdc` user permissions (sys.database_principals) or rename `cdc` user.

* **Recommendation**: To address this problem, you need to disable and re-enable CDC for your database. When enabling change data capture for a database, it creates the cdc schema, cdc user, metadata tables, and other system objects for the database.

> [!NOTE]
> Objects found in the [**sys.objects**](/sql/relational-databases/system-catalog-views/sys-objects-transact-sql) system catalog view with **is_ms_shipped=1 and schema_name='cdc'** should not be altered or dropped.

### Error 1202 - Database principal doesn't exist, or user isn't a member

* **Cause**: The error might occur when CDC user has been dropped. For Change data capture (CDC) to function properly, you shouldn't manually modify any CDC metadata such as CDC schema, change tables, CDC system stored procedures, default `cdc` user permissions (sys.database_principals) or rename `cdc` user.

* **Recommendation**: Ensure the `cdc` user exists in your database, and also has the `db_owner` role assigned. To create the `cdc` user, see the example [Create cdc user and assign role](#create-user-and-assign-role).

### Error 15517 - Can't execute as the database principal because the principal doesn't exist

* **Cause**: This type of principal can't be impersonated, or you don't have permission. The error might occur when CDC metadata has been dropped or it's no longer part of the `db_owner` role. For Change data capture (CDC) to function properly, you shouldn't manually modify any CDC metadata such as CDC schema, change tables, CDC system stored procedures, default `cdc` user permissions (sys.database_principals) or rename `cdc` user.

* **Recommendation**: Ensure the `cdc` user exists in your database, and also has the `db_owner` role assigned. To create the `cdc` user, see the example [Create cdc user and assign role](#create-user-and-assign-role).

### Error 18807 - Can't find an object ID for the replication system table

* **Cause**: This error happens when SQL Server can't find or access the replication system table '%s.' This could be because the table is missing or unreachable. For Change data capture (CDC) to function properly, you shouldn't manually modify any CDC metadata such as CDC schema, change tables, CDC system stored procedures, default `cdc` user permissions (sys.database_principals) or rename `cdc` user.

* **Recommendation**: Verify that the system table exists and is accessible by querying the table directly. Query the [**sys.objects**](/sql/relational-databases/system-catalog-views/sys-objects-transact-sql) system catalog, set predicate clause with **is_ms_shipped=1 and schema_name='cdc'** to list all CDC-related objects. If the query doesn't return any objects, you should disable and then re-enable CDC for your database. Enabling change data capture for a database creates the cdc schema, cdc user, metadata tables, and other system objects for the database.

### Error 21050 - Only members of the sysadmin or db_owner fixed server role can perform this operation

* **Cause**: The `cdc` user has been removed from the `db_owner` database role, or from the `sysadmin` server role.

* **Recommendation**: Ensure the `cdc` user has the `db_owner` role assigned. To create the `cdc` user, see the example [Create cdc user and assign role](#create-user-and-assign-role).

### Data size insufficient

### Error 1105 - Couldn't allocate space for object in database because the filegroup is full

* **Cause**: This error occurs when the primary filegroup of a database runs out of space, and SQL Server is unable to allocate more space for an object (such as a table or index) within that filegroup.

* **Recommendation**: To resolve this issue, delete any unnecessary data within your database to free up space. Identify unused tables, indexes, or other objects in the filegroup that can be safely removed. Monitor space utilization closely, for more information, see [Manage file space for databases in Azure SQL Database](/azure/azure-sql/database/file-space-manage)

    In case dropping unnecessary data/objects is **not an option**, consider scaling to a higher database tier. For more information about Azure SQL Database (Single database) data and log size limits, see [**Azure SQL Database log limits - Single Database**](#azure-sql-database-log-limits---single-database) in this article.  

> [!IMPORTANT]
> For detailed information about Azure SQL Database (single database) compute sizes (SLO) see [**Resource limits for single databases using the vCore purchasing model**](/azure/azure-sql/database/resource-limits-vcore-single-databases) and [**Resource limits for single databases using the DTU purchasing model - Azure SQL Database**](/azure/azure-sql/database/resource-limits-dtu-single-databases).
    
### Error 1132 - The elastic pool has reached its storage limit

* **Cause**: This error occurs when the storage usage in your elastic pool has exceeded the allocated limit,

* **Recommendation**: To resolve this issue, implement data archiving and purging strategies to keep only the necessary data in the databases that are part of the elastic pool. Monitor space utilization closely, for more information, see [Manage file space for databases in Azure SQL Database](/azure/azure-sql/database/file-space-manage)

    In case archiving data or dropping unnecessary data/objects is **not an option**, consider scaling to a higher database tier. For more information about Azure SQL Database (Elastic pools) data and log size limits, see [**Azure SQL Database log limits - Elastic Pool**](#azure-sql-database-log-limits---elastic-pool) in this article.  

> [!IMPORTANT]
> For detailed information about Azure SQL Database (single database) compute sizes (SLO) see [**Resource limits for elastic pools using the vCore purchasing model**](/azure/azure-sql/database/resource-limits-vcore-elastic-pools) and [**Resource limits for elastic pools using the DTU purchasing model**](/azure/azure-sql/database/resource-limits-dtu-elastic-pools).

### CDC limitation

### Error 913 - CDC capture job fails when processing changes for a table with system CLR datatype

* **Cause**: This error occurs when enabling CDC on a table with system CLR datatype, making DML changes, and then making DDL changes on the same table while the CDC capture job is processing changes related to other tables.

* **Recommendation**: The recommended steps are to quiesce DML to the table, run a capture job to process changes, run DDL for the table, run a capture job to process DDL changes, and then re-enable DML processing. For more information, see [CDC capture job fails when processing changes](/troubleshoot/sql/database-engine/replication/cdc-capture-job-fails-processing-changes-table).

### Create user and assign role

Use the following T-SQL script, to create a user (`cdc`), and assign the proper role for the same (`db_owner`).

```sql
IF NOT EXISTS 
(
    SELECT * 
    FROM sys.database_principals 
    WHERE NAME = 'cdc'
)
BEGIN
    CREATE USER [cdc] 
    WITHOUT LOGIN WITH DEFAULT_SCHEMA = [cdc];
END

EXEC sp_addrolemember 'db_owner', 'cdc';
```

### Check and add role membership

To verify if `cdc` user belongs to either the `sysadmin` or `db_owner` role, run the following T-SQL query:

```sql
EXECUTE AS USER = 'cdc';

SELECT is_srvrolemember('sysadmin'), is_member('db_owner');
```

If the `cdc` user doesn't belong to either role, execute the following T-SQL query to add `db_owner` role to the `cdc` user.

```sql
EXEC sp_addrolemember 'db_owner' , 'cdc';
```

## Next steps 
[Work with Change Data](/sql/relational-databases/track-changes/work-with-change-data-sql-server)   
[Track Data Changes](/sql/relational-databases/track-changes/track-data-changes-sql-server)   
[Administer and Monitor change data capture](/sql/relational-databases/track-changes/administer-and-monitor-change-data-capture-sql-server)  
[Temporal Tables](/sql/relational-databases/tables/temporal-tables)