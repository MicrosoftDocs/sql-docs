---
title: Scaling out
description: Software as a Service (SaaS) developers can easily create elastic, scalable databases in the cloud using these tools.
author: bgavrilMS
ms.author: bogavril
ms.reviewer: wiassaf, mathoma
ms.date: 09/05/2024
ms.service: azure-sql-database
ms.subservice: scale-out
ms.topic: conceptual
ms.custom:
  - sqldbrb=1
---
# Scale out with Azure SQL Database
[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

You can easily scale out databases in Azure SQL Database using the **Elastic Database** tools. These tools and features let you use the database resources of **Azure SQL Database** to create solutions for transactional workloads, and especially Software as a Service (SaaS) applications. Elastic Database features are composed of the:

- [Elastic Database client library](elastic-database-client-library.md): The client library is a feature that allows you to create and maintain sharded databases. See [Get started with Elastic Database Tools](elastic-scale-get-started.md).
- [Move data between scaled-out cloud databases](elastic-scale-overview-split-and-merge.md): moves data between sharded databases. This tool is useful for moving data from a multitenant database to a single-tenant database (or vice-versa). See [Deploy a split-merge service to move data between sharded databases](elastic-scale-configure-deploy-split-and-merge.md).
- [Elastic jobs in Azure SQL Database](elastic-jobs-overview.md): Use jobs to manage large numbers of databases in Azure SQL Database. Easily perform administrative operations such as schema changes, credentials management, reference data updates, performance data collection, or tenant (customer) telemetry collection using jobs.
- [Azure SQL Database elastic query overview (preview)](elastic-query-overview.md) (preview): Enables you to run a Transact-SQL query that spans multiple databases. This enables connection to reporting tools such as Excel, Power BI, Tableau, etc.
- [Distributed transactions across cloud databases](elastic-transactions-overview.md): This feature allows you to run transactions that span several databases. Elastic database transactions are available for .NET applications using ADO .NET and integrate with the familiar programming experience using the [System.Transaction classes](/dotnet/api/system.transactions).

The following graphic shows an architecture that includes the **Elastic Database features** in relation to a collection of databases.

In this graphic, colors of the database represent schemas. Databases with the same color share the same schema.

1. A set of **SQL databases** is hosted on Azure using sharding architecture.
1. The **Elastic Database client library** is used to manage a shard set.
1. A subset of the databases is put into an [elastic pool](elastic-pool-overview.md).
1. An **Elastic Database job** runs T-SQL scripts against all databases.
1. The **split-merge tool** is used to move data from one shard to another.
1. The **Elastic Database query** allows you to write a query that spans all databases in the shard set.
1. **Elastic transactions** allow you to run transactions that span several databases. 

:::image type="content" source="media/elastic-scale-introduction/tools.png" alt-text="Diagram of elastic database tools.":::

## Why use the tools?

Achieving elasticity and scale for cloud applications is straightforward for VMs and blob storage - simply add or subtract units, or increase power. But it has remained a challenge for stateful data processing in relational databases. Challenges emerged in these scenarios:

- Growing and shrinking capacity for the relational database part of your workload.
- Managing hotspots that might arise affecting a specific subset of data - such as a busy end-customer (tenant).

Traditionally, scenarios like these have been addressed by investing in larger-scale servers to support the application. However, this option is limited in the cloud where all processing happens on predefined commodity hardware. Instead, distributing data and processing across many identically structured databases (a scale-out pattern known as "sharding") provides an alternative to traditional scale-up approaches both in terms of cost and elasticity.

## Horizontal and vertical scaling

The following figure shows the horizontal and vertical dimensions of scaling, which are the basic ways the elastic databases can be scaled.

:::image type="content" source="media/elastic-scale-introduction/horizontal-versus-vertical.png" alt-text="Diagram explaining horizontal versus vertical scale-out." lightbox="media/elastic-scale-introduction/horizontal-versus-vertical.png":::

Horizontal scaling refers to adding or removing databases in order to adjust capacity or overall performance, also called "scaling out." Sharding, in which data is partitioned across a collection of identically structured databases, is a common way to implement horizontal scaling.  

Vertical scaling refers to increasing or decreasing the compute size of an individual database, also known as "scaling up."

Most cloud-scale database applications use a combination of these two strategies. For example, a Software as a Service application might use horizontal scaling to provision new end-customers and vertical scaling to allow each end-customer's database to grow or shrink resources as needed by the workload.

- Horizontal scaling is managed using the [Elastic Database client library](elastic-database-client-library.md).
- Vertical scaling is accomplished using Azure PowerShell cmdlets to change the service tier, or by placing databases in an elastic pool.

## Sharding

*Sharding* is a technique to distribute large amounts of identically structured data across many independent databases. It is especially popular with cloud developers creating Software as a Service (SAAS) offerings for end customers or businesses. These end customers are often referred to as "tenants." Sharding might be required for any number of reasons:  

- The total amount of data is too large to fit within the constraints of an individual database
- The transaction throughput of the overall workload exceeds the capabilities of an individual database
- Tenants might require physical isolation from each other, so separate databases are needed for each tenant
- Different sections of a database might need to reside in different geographies for compliance, performance, or geopolitical reasons.

In other scenarios, such as ingestion of data from distributed devices, sharding can be used to fill a set of databases that are organized temporally. For example, a separate database can be dedicated to each day or week. In that case, the sharding key can be an integer representing the date (present in all rows of the sharded tables) and queries retrieving information for a date range must be routed by the application to the subset of databases covering the range in question.

Sharding works best when every transaction in an application can be restricted to a single value of a sharding key. That ensures that all transactions are local to a specific database.

## Multitenant and single-tenant

Some applications use the simplest approach of creating a separate database for each tenant. This approach is the **single tenant sharding pattern** that provides isolation, backup/restore ability, and resource scaling at the granularity of the tenant. With single tenant sharding, each database is associated with a specific tenant ID value (or customer key value), but that key does not need to be present in the data itself. It is the application's responsibility to route each request to the appropriate database - and the client library can simplify this task.

:::image type="content" source="media/elastic-scale-introduction/single-versus-multitenant.png" alt-text="Diagram of single tenant versus multitenant." lightbox="media/elastic-scale-introduction/single-versus-multitenant.png":::

Others scenarios pack multiple tenants together into databases, rather than isolating them into separate databases. This pattern is a typical **multitenant sharding pattern** - and it might be driven by the fact that an application manages large numbers of small tenants. In multitenant sharding, the rows in the database tables are all designed to carry a key identifying the tenant ID or sharding key. Again, the application tier is responsible for routing a tenant's request to the appropriate database, and this can be supported by the elastic database client library. In addition, row-level security can be used to filter which rows each tenant can access - for details, see [Multitenant applications with elastic database tools and row-level security](saas-tenancy-elastic-tools-multi-tenant-row-level-security.md). Redistributing data among databases might be needed with the multitenant sharding pattern, and is facilitated by the elastic database split-merge tool. To learn more about design patterns for SaaS applications using elastic pools, see [Multitenant SaaS database tenancy patterns](saas-tenancy-app-design-patterns.md).

### Move data from multiple to single-tenancy databases
When creating a SaaS application, it is typical to offer prospective customers a trial version of the software. In this case, it is cost-effective to use a multitenant database for the data. However, when a prospect becomes a customer, a single-tenant database is better since it provides better performance. If the customer creates data during the trial period, use the [split-merge tool](elastic-scale-overview-split-and-merge.md) to move the data from the multitenant to the new single-tenant database.

> [!NOTE]
> Restoring from multitenant databases to a single tenant is not possible.

## Samples and tutorials

For a sample app that demonstrates the client library, see [Get started with Elastic Database tools](elastic-scale-get-started.md).

To convert existing databases to use the tools, see [Migrate existing databases to scale out](elastic-convert-to-use-elastic-tools.md).

To see the specifics of the elastic pool, see [Price and performance considerations for an elastic pool](elastic-pool-overview.md), or create a new pool with [elastic pools](elastic-pool-manage.md).  

[!INCLUDE [elastic-scale-include](../includes/elastic-scale-include.md)]
