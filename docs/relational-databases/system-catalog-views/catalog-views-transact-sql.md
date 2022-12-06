---
title: "System catalog views (Transact-SQL)"
description: Catalog views return information that is used by the Database Engine.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/02/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sql13.SysViewExpandPortal.f1"
helpviewer_keywords:
  - "catalog metadata [SQL Server]"
  - "system views [SQL Server], catalog"
  - "metadata [SQL Server], views"
  - "catalogs [SQL Server], metadata"
  - "catalog views [SQL Server]"
  - "Database Engine [SQL Server], metadata"
  - "catalog views [SQL Server], about catalog views"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# System catalog views (Transact-SQL)

[!INCLUDE[sql-asdb-asa-pdw](../../includes/applies-to-version/sql-asdb-asa-pdw.md)]

Catalog views return information that is used by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. We recommend that you use catalog views because they are the most general interface to the catalog metadata and provide the most efficient way to obtain, transform, and present customized forms of this information. All user-available catalog metadata is exposed through catalog views.

> [!NOTE]  
> Catalog views do not contain information about replication, backup, database maintenance plan, or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent catalog data.

## Remarks

Some catalog views inherit rows from other catalog views. For example, the [sys.tables](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md) catalog view inherits from the [sys.objects](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md) catalog view. The `sys.objects` catalog view is referred to as the base view, and the `sys.tables` view is called the derived view. The `sys.tables` catalog view returns the columns that are specific to tables and also all the columns that the `sys.objects` catalog view returns. The `sys.objects` catalog view returns rows for objects other than tables, such as stored procedures and views. After a table is created, the metadata for the table is returned in both views. Although the two catalog views return different levels of information about the table, there is only one entry in metadata for this table with one name and one `object_id`. This can be summarized as follows:

- The base view contains a subset of columns and a superset of rows.
- The derived view contains a superset of columns and a subset of rows.

> [!IMPORTANT]  
> In future releases of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[msCoName](../../includes/msconame-md.md)] may augment the definition of any system catalog view by adding columns to the end of the column list. We recommend against using the syntax `SELECT * FROM sys.<catalog_view_name>` in production code because the number of columns returned might change and break your application.

The catalog views in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] have been organized into the following categories:

:::row:::
    :::column:::
        [Always On Availability Groups Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)

        [Azure SQL Database Catalog Views](../../relational-databases/system-catalog-views/azure-sql-database-catalog-views.md)

        [Change Tracking Catalog Views (Transact-SQL)](../system-catalog-views/change-tracking-catalog-views-sys-change-tracking-databases.md)

        [CLR Assembly Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/clr-assembly-catalog-views-transact-sql.md)

        [Data Collector Views (Transact-SQL)](../../relational-databases/system-catalog-views/data-collector-views-transact-sql.md)

        [Data Spaces (Transact-SQL)](../../relational-databases/system-catalog-views/data-spaces-transact-sql.md)

        [Database Mail Views (Transact-SQL)](../../relational-databases/system-catalog-views/database-mail-views-transact-sql.md)

        [Database Mirroring Witness Catalog Views (Transact-SQL)](../system-catalog-views/database-mirroring-witness-catalog-views-sys-database-mirroring-witnesses.md)

        [Databases and Files Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/databases-and-files-catalog-views-transact-sql.md)

        [Endpoints Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/endpoints-catalog-views-transact-sql.md)

        [Extended Events Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/extended-events-catalog-views-transact-sql.md)

        [Extended Properties Catalog Views (Transact-SQL)](../system-catalog-views/extended-properties-catalog-views-sys-extended-properties.md)

        [External Operations Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/external-operations-catalog-views-transact-sql.md)

        [Filestream and FileTable Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/filestream-and-filetable-catalog-views-transact-sql.md)

        [Full-Text Search and Semantic Search Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/full-text-search-and-semantic-search-catalog-views-transact-sql.md)

        [Linked Servers Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/linked-servers-catalog-views-transact-sql.md)
    :::column-end:::
    :::column:::
        [Messages (for Errors) Catalog Views (Transact-SQL)](../system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)

        [Object Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)

        [Partition Function Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/partition-function-catalog-views-transact-sql.md)

        [Policy-Based Management Views (Transact-SQL)](../../relational-databases/system-catalog-views/policy-based-management-views-transact-sql.md)

        [Resource Governor Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/resource-governor-catalog-views-transact-sql.md)

        [Query Store Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)

        [Scalar Types Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/scalar-types-catalog-views-transact-sql.md)

        [Schemas Catalog Views (Transact-SQL)](../system-catalog-views/schemas-catalog-views-sys-schemas.md)

        [Security Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)

        [Service Broker Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/service-broker-catalog-views-transact-sql.md)

        [Server-wide Configuration Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/server-wide-configuration-catalog-views-transact-sql.md)

        [Spatial Data Catalog Views](../../relational-databases/system-catalog-views/spatial-data-catalog-views.md)

        [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)

        [Stretch Database Catalog Views (Transact-SQL)](../system-catalog-views/stretch-database-catalog-views-sys-remote-data-archive-databases.md)

        [XML Schemas (XML Type System) Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/xml-schemas-xml-type-system-catalog-views-transact-sql.md)
    :::column-end:::
:::row-end:::

## See also

- [Information Schema Views (Transact-SQL)](../../relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)
- [System Tables (Transact-SQL)](../../relational-databases/system-tables/system-tables-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)
