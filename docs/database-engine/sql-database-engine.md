---
title: SQL Database Engine
titleSuffix: Microsoft SQL
description: Learn about the Microsoft SQL Database Engine, the database platform.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 03/10/2026
ms.service: sql
ms.topic: overview
ai-usage: ai-assisted
---

# The Microsoft SQL Database Engine

The Microsoft SQL Database Engine is the core service for storing, processing, and securing data. It provides controlled access and rapid transaction processing for a wide variety of commercial and entrepreneurial applications.

The SQL Database Engine is the common underlying engine behind all Microsoft SQL offerings. It's an enterprise-scale, general-purpose relational database management system (RDBMS).

For millions of customers worldwide, in every industry and level of organization, the Microsoft SQL Database Engine is the database service for secure data processing and storage. Demanding applications can reliably read and modify information while preserving integrity at scale. The SQL Database Engine runs as a service that accepts client connections and then executes the requested operations against databases. Secured with enterprise-class data access and encryption features, and with built-in high availability and database recovery features, the SQL Database Engine is a complete database platform.

<a id="multimodal-database-engine"></a>

## Multi-model database engine

The SQL Database Engine is a multi-model database engine with purpose-built storage formats, purpose-built index structures, and a single query optimizer that makes cost-based decisions across all data models.

Products that use the SQL Database Engine can be the enterprise-class online transactional processing (OLTP), online analytical processing (OLAP), or non-relational solution for your modern applications. Relational, normalized data might be common for databases, but the SQL Database Engine supports many data models and data formats. 

All capabilities appear in the same SQL Database Engine, using the same Transact-SQL (T-SQL) query language, under the same security layer, and using the same HA/DR solutions.

- [Columnstore indexes: overview](../relational-databases/indexes/columnstore-indexes-overview.md)
- [Graph processing with SQL Server and Azure SQL Database](../relational-databases/graphs/sql-graph-overview.md)
- [Key-value pair](https://devblogs.microsoft.com/azure-sql/azure-sql-database-as-a-key-value-store/)
- [JSON data in SQL Server](../relational-databases/json/json-data-sql-server.md)
- [Spatial Data](../relational-databases/spatial/spatial-data-sql-server.md)
- [Vector search and vector indexes in the SQL Database Engine](../sql-server/ai/vectors.md)
- [XML data (SQL Server)](../relational-databases/xml/xml-data-sql-server.md)

## Tooling

The SQL Database Engine comes with a suite of [free, industry-leading tools](../tools/overview-sql-tools.md) for querying, data architecture, automation, and database development.

[!INCLUDE [tools](../includes/tools.md)]

## Database fundamentals: ACID compliance

A core tenet of any RDBMS is support for ACID properties of transactions. A transaction is a sequence of operations performed as a single logical unit of work. A logical unit of work must exhibit four properties to qualify as a transaction: atomicity, consistency, isolation, and durability (ACID). 

| Property | Description |
| --- | --- |
| **Atomicity** | A transaction must be an atomic unit of work; either all of its data modifications are performed, or none of them are performed. |
| **Consistency** | When completed, a transaction must leave all data in a consistent state. In a relational database, all rules and declared constraints must be applied to the transaction's modifications to maintain data integrity. |
| **Isolation** | Modifications made by one transaction must be isolated from the modifications made by other concurrent transactions. Partial or intermediate states between transactions aren't allowed. This property is also called serializability because it results in the ability to replay a series of sequential transactions that result in the same database state. |
| **Durability** | After a transaction completes, it writes to nonvolatile storage, so the system records its effects even in the event of a failure. Transactions committed only to volatile memory (RAM) aren't durable. |

For more information on transactions, see [Transaction locking and row versioning guide](../relational-databases/sql-server-transaction-locking-and-row-versioning-guide.md). For a deep dive into the SQL Database Engine, see [SQL Server internals and architecture guides](../relational-databases/sql-server-guides.md).

By design and by default, the SQL Database Engine is a fully ACID compliant database. In the interest of scale or performance, database developers can intentionally bypass some ACID principles in the SQL Database Engine. For example, they can use delayed durability, non-durable tables, or read uncommitted data. In all these cases, the developer makes a choice to trade off some ACID properties to achieve other goals. Such tradeoffs must be made with caution because they can result in data integrity issues and affect business outcomes.

## Platform feature support

Most Transact-SQL (T-SQL) features that applications use are fully supported on all SQL Database Engine platforms. For example, core SQL components such as data types, operators, and string, arithmetic, logical, and cursor functions work identically in all platforms. However, there are a few T-SQL differences in data definition language (DDL) and data manipulation language (DML) elements. These differences result in T-SQL statements and queries that are only partially supported in various platforms for design reasons. 

In the case of platforms as a service (PaaS) or software as a service (SaaS) platforms, for example, some operating system or local file capabilities are disabled due to logical or physical isolation. In a contained database, for example, T-SQL statements and options aren't available if they configure instance-level options, operating system components, or specify file system configuration. 

### Features and links

The following table lists major features of the SQL Database Engine with links to overview documentation and brief descriptions.

| Feature | Description |
| --- | --- |
| [Always On availability groups](../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) | Enterprise high availability and disaster recovery, synchronous or asynchronous, with secondary readable replicas |
| [Always Encrypted](../relational-databases/security/encryption/always-encrypted-database-engine.md) | Client-side encryption of sensitive data columns |
| [Audit](../relational-databases/security/auditing/sql-server-audit-database-engine.md) | Audit administrative activity |
| [Backup and restore](../relational-databases/backup-restore/backup-overview-sql-server.md) | Protect and recover database data |
| [Backup compression](../relational-databases/backup-restore/backup-compression-sql-server.md) | Built-in backup compression |
| [Backup encryption](../relational-databases/backup-restore/backup-encryption.md) | Built-in backup encryption |
| [Buffer pool extension](configure-windows/buffer-pool-extension.md) | Boost I/O by adding nonvolatile RAM to buffer pool | 
| [Change data capture](../relational-databases/track-changes/about-change-data-capture-sql-server.md) | Track and capture data changes |
| [Columnstore indexes](../relational-databases/indexes/columnstore-indexes-overview.md) | Columnar storage for analytics workloads |
| [Dynamic data masking](../relational-databases/security/dynamic-data-masking.md) | Limit sensitive data exposure |
| [Failover Clustering](availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md) | Enterprise failover clustering for high availability and disaster recovery |
| [Full-text search](../relational-databases/search/full-text-search.md) | Advanced text search capabilities |
| [Graph tables](../relational-databases/graphs/sql-graph-overview.md) | Model and query graph relationships |
| [In-Memory OLTP](../relational-databases/in-memory-oltp/overview-and-usage-scenarios.md) | Memory-optimized tables and procedures |
| [Indexed views](../relational-databases/views/create-indexed-views.md) | Materialize views by creating indexes |
| [JSON support](../relational-databases/json/json-data-sql-server.md) | Store and query JSON data with built-in JSON functions |
| [Ledger](../relational-databases/security/ledger/ledger-overview.md) | Tamper-evident database capabilities |
| [Microsoft Entra authentication](../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md) | Enterprise-wide user and service account authentication |
| [Online index maintenance](../relational-databases/indexes/guidelines-for-online-index-operations.md) | Maintenance that doesn't disrupt normal activity |
| [Optimized locking](../relational-databases/performance/optimized-locking.md) | Improved transaction locking mechanism |
| [Partitioning](../relational-databases/partitions/partitioned-tables-and-indexes.md) | Scale with partitioned tables and indexes |
| [PolyBase](../relational-databases/polybase/polybase-guide.md) | Data virtualization to query external data sources |
| [Query Store](../relational-databases/performance/monitoring-performance-by-using-the-query-store.md) | Built-in monitor for query performance tuning |
| [Replication](../relational-databases/replication/sql-server-replication.md) | Distribute data across servers |
| [Row-level security](../relational-databases/security/row-level-security.md) | Control filtered access to table data |
| [Spatial data](../relational-databases/spatial/spatial-data-sql-server.md) | Store and query geographic data |
| [Temporal tables](../relational-databases/tables/temporal-tables.md) | Track full history of changes |
| [Transparent data encryption](../relational-databases/security/encryption/transparent-data-encryption.md) | Encrypt database files at rest |
| [Vector support](../sql-server/ai/vectors.md) | Store and query vector embeddings, vector search on vector indexes. Support for LangChain integration and Semantic Kernel integration.|
| [XML support](../relational-databases/xml/xml-data-sql-server.md) | Store and query XML data, XML indexes |

Language and [driver support](../connect/driver-feature-matrix.md):

| Driver | Description |
| --- | --- |
| [.NET](../connect/ado-net/get-started-sqlclient-driver.md) | ADO.NET driver for SQL |
| [Go](/azure/azure-sql/database/connect-query-go) | Golang go-mssqldb driver for SQL |
| [Java](../connect/jdbc/microsoft-jdbc-driver-for-sql-server.md) | JDBC Driver for SQL |
| [Node.js](../connect/node-js/node-js-driver-for-sql-server.md) | Node.js driver for SQL |
| [ODBC](../connect/odbc/microsoft-odbc-driver-for-sql-server.md) | ODBC Driver for SQL Server |
| [OLE DB](../connect/oledb/oledb-driver-for-sql-server.md) | OLE DB Driver for SQL Server |
| [PHP](../connect/php/getting-started-with-the-php-sql-driver.md) | PHP driver for SQL | 
| [Python](../connect/python/mssql-python/python-sql-driver-mssql-python.md) | Python mssql-python driver for SQL |
| [Ruby](../connect/ruby/ruby-driver-for-sql-server.md) | Ruby driver for SQL |
| [Spark](../connect/spark/connector.md) | Spark connector for SQL |

## Modern platforms using the SQL Database Engine

The following modern platforms use the SQL Database Engine, starting with the flagship SQL Server product.

| Product | Deployment model | 
| --- | --- | 
| [SQL Server](../sql-server/what-is-sql-server.md) | On-premises, virtual machines including [SQL Server on Azure VM](/azure/azure-sql/virtual-machines), [Arc-enabled](/azure/azure-arc/data/overview), [Windows](install-windows/install-sql-server.md), [Linux](../linux/sql-server-linux-overview.md), and [Linux containers](../linux/sql-server-linux-docker-container-deployment.md) | 
| [Azure SQL Database](/azure/azure-sql/database?view=azuresqldb-current&preserve-view=true) | Fully managed database, Platform as a Service (PaaS) |
| [Azure SQL Managed Instance](/azure/azure-sql/managed-instance) | Fully managed database instance, Platform as a Service (PaaS) | 
| [Fabric Data Warehouse](/fabric/data-warehouse/data-warehousing) | Fully managed warehouse, Software as a Service (SaaS) in [Microsoft Fabric](/fabric/get-started/microsoft-fabric-overview) |
| [SQL database in Fabric](/fabric/database/sql/overview) | Fully managed database, Software as a Service (SaaS) in [Microsoft Fabric](/fabric/get-started/microsoft-fabric-overview) |

In [SQL documentation](../sql-server/index.yml), the **Version** selector dropdown list is key to understanding which version applies to an article, syntax reference, tutorial, or other content. Many Learn articles are customized to fit specific SQL platforms and capabilities. In most SQL reference articles, there's also an icon bar listing the applicable platforms for an article. For more information about navigating documentation, see [SQL Server docs navigation guide](../sql-server/sql-docs-navigation-guide.md).

## Free offers

Get started today, for free. You can try the SQL Database Engine for free, with a [free Azure subscription](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn):

- [SQL Server Developer editions](../sql-server/editions-and-components-of-sql-server-latest.md#sql-server-editions) for development and test systems
- [Azure SQL Database free offer](/azure/azure-sql/database/free-offer?view=azuresql-db&preserve-view=true)
- [Azure SQL Managed Instance free offer](/azure/azure-sql/managed-instance/free-offer?view=azuresql-mi&preserve-view=true)
- [Microsoft Fabric for free trial capacity](/fabric/fundamentals/fabric-trial)

## Migration

The [Azure Database Migration Guides](/data-migration/) landing page provides links to quickly start migrations from various platforms to various Microsoft SQL platforms. 

- [Compare SQL data migration tools](../sql-server/migrate/dma-azure-migrate-compare-migration-tools.md) for a wide variety of migrations.
- Visit the [Microsoft Fabric migration overview](/fabric/fundamentals/migration) to learn more about migration to Fabric. 
- Try out the [Azure Database Migration Service (Azure DMS)](/azure/dms/dms-overview), a fully managed service for migrations from multiple database sources to Azure data platforms.

## Related content

- [What is SQL Server?](../sql-server/what-is-sql-server.md)
