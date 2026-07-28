---
title: Hyperscale secondary replicas
description: This article describes the different types of secondary replicas available in the Hyperscale service tier.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: atsingh, mahyon
ms.date: 10/17/2025
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: overview
---

# Hyperscale secondary replicas

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

As described in [Distributed functions architecture](service-tier-hyperscale.md), Azure SQL Database Hyperscale has two different types of compute nodes, also referred to as replicas:

- Primary: serves read and write operations
- Secondary: provides [read scale-out](read-scale-out.md), [high availability](high-availability-sla-local-zone-redundancy.md), and [geo-replication](active-geo-replication-overview.md)

Secondary replicas are always read-only, and can be of three different types:

- High Availability replica
- Geo-replica
- Named replica

Each type has a different architecture, feature set, purpose, and cost. Based on the features you need, you can use just one or even all of the three together. You can use secondary replicas with either [serverless](serverless-tier-overview.md) or provisioned compute tiers.

For tutorials on configuring and managing Hyperscale named replicas, see:

- [Create a Hyperscale named replica](hyperscale-named-replica-configure.md#create-a-hyperscale-named-replica)
- [Connect to a Hyperscale named replica](hyperscale-named-replica-configure.md#connect-to-a-hyperscale-named-replica)
- [Modify a Hyperscale named replica](hyperscale-named-replica-configure.md#modify-a-hyperscale-named-replica)

## High Availability replica

A High Availability (HA) replica uses the same page servers as the primary replica, so no data copy is required to add an HA replica. HA replicas are used to increase database availability; they act as hot standby replicas for failover purposes. If the primary replica becomes unavailable, failover to one of the existing HA replicas is automatic and quick. The connection string doesn't need to change; during failover applications might experience minimal downtime due to active connections being dropped. As usual for this scenario, proper retry logic is recommended. Several drivers already provide some degree of automatic retry logic. If you're using .NET, the [latest Microsoft.Data.SqlClient](https://devblogs.microsoft.com/azure-sql/configurable-retry-logic-for-microsoft-data-sqlclient/) library provides native full support for configurable automatic retry logic.

HA replicas use the same server and database name as the primary replica. Their Service Level Objective (SLO) also always the same as for the primary replica. HA replicas aren't visible or manageable as stand-alone resources, from the portal or from any API. They're billed at the same compute rate as the primary replica, but storage costs don't apply to secondary replicas.

There can be zero to four HA replicas. You can specify the number replicas during the creation of a new database, or update the number of replicas for an existing database. You can use the common management endpoints and tools (for example: Azure PowerShell, Azure CLI, Azure portal, REST API) to specify the number of HA replicas. Creating or removing HA replicas doesn't affect active connections on the primary replica.

### Identify replicas

You can identify all the Azure SQL Database Hyperscale replicas and their roles when connected to a primary replica with the [sys.dm_hs_database_replicas (Transact-SQL)](/sql/relational-databases/system-functions/sys-dm-hs-database-replicas) dynamic management view (DMV). For example:

```sql
SELECT replica_role_desc, replica_server_name, replica_id
FROM sys.dm_hs_database_replicas(DB_ID(N'Contosodb'));
```

### Connect to an HA replica

In Hyperscale databases, the `ApplicationIntent` argument in the connection string used by the client dictates whether the connection is routed to the read-write primary replica or to a read-only HA replica. If `ApplicationIntent` is set to `ReadOnly` and the database doesn't have a secondary replica, connection is routed to the primary replica and defaults to the `ReadWrite` behavior.

```csharp
-- Connection string with application intent
Server=tcp:<myserver>.database.windows.net;Database=<mydatabase>;ApplicationIntent=ReadOnly;User ID=<myLogin>;Password=<password>;Trusted_Connection=False; Encrypt=True;
```

All HA replicas are identical in their resource capacity. If more than one HA replica is present, the read-intent workload is distributed arbitrarily across all available HA replicas. When there are multiple HA replicas, keep in mind that each one could have different data latency with respect to data changes made on the primary. Each HA replica uses the same data as the primary on the same set of page servers. However, local data caches on each HA replica reflect the changes made on the primary via the transaction log service, which forward log records from the primary replica to HA replicas. As a result, depending on the workload executing on the HA replica, application of log records can happen at different speeds, and thus different replicas could have different data latency relative to the primary replica.

## Named replica

A named replica, just like an HA replica, uses the same page servers as the primary replica. Similar to HA replicas, there's no data copy needed to add a named replica.

There are differences between HA replicas and named replicas:

- Named replicas appear as regular (read-only) Azure SQL databases in the portal and in API (Azure CLI, Azure PowerShell, T-SQL) calls.
- Named replicas can have database name different from the primary replica, and optionally be located on a different logical server (as long as it is in the same region as the primary replica).
- Named replicas have their own Service Level Objective that can be set and changed independently from the primary replica.
- Named replicas support for up to 30 named replicas (for each primary replica).
- Named replicas support different authentication for each named replica by creating different logins on logical servers hosting named replicas.

As a result, named replicas offer several benefits over HA replicas, for what concern read-only workloads:

- Users connected to a named replica aren't disconnected if the primary replica is scaled up or down.
- Users connected to the primary replica aren't affected when any named replicas are scaled up or down.

The main goal of named replicas is to enable a broad variety of [read scale-out](read-scale-out.md) scenarios, and to improve Hybrid Transactional and Analytical Processing (HTAP) workloads. Examples of how to create such solutions are available here:

- [OLTP scale-out sample](https://github.com/Azure-Samples/azure-sql-db-named-replica-oltp-scaleout)

In addition, named replicas offer flexibility and elasticity to also satisfy many other use cases:
- [Access Isolation](hyperscale-named-replica-security-configure.md): you can grant access to a specific named replica, but not the primary replica or other named replicas.
- Workload-dependent service level objective: as a named replica can have its own service level objective, it's possible to use different named replicas for different workloads and use cases. For example, one named replica could be used to serve Power BI requests, while another can be used to serve data to Apache Spark for Data Science tasks. Each one can have an independent service level objective and scale independently.
- Workload-dependent routing: with up to 30 named replicas, it's possible to use named replicas in groups so that an application can be isolated from another. For example, a group of four named replicas could be used to serve requests coming from mobile applications, while another group two named replicas can be used to serve requests coming from a web application. This approach would allow a fine-grained tuning of performance and costs for each group.

> [!NOTE]  
> For frequently asked questions on Hyperscale named replicas, see [Azure SQL Database Hyperscale named replicas FAQ](service-tier-hyperscale-frequently-asked-questions-faq.yml#read-scale-out-questions).

### Zone redundancy for Hyperscale named replicas

Hyperscale named replicas configured for zone redundancy use [Azure Availability Zones](/azure/reliability/availability-zones-overview) to distribute named replicas compute nodes across different physical locations within an Azure region. By choosing zone redundancy for named replicas, you can enhance the resilience of all layers of your Hyperscale databases to a wider range of failures, including datacenter outages, without any modifications of the application logic. For more information, see [Hyperscale zone redundant availability](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier-zone-redundant-availability).

For a tutorial to create a zone redundant Hyperscale named replica, see [Create a Hyperscale named replica](hyperscale-named-replica-configure.md#create-a-hyperscale-named-replica).

For troubleshooting and testing application fault resiliency, see [Test application fault resiliency](high-availability-sla-local-zone-redundancy.md?view=azuresql-db&preserve-view=true#testing-application-fault-resiliency).

## Geo-replica

With [active geo-replication](active-geo-replication-overview.md), you can create a readable secondary replica of the primary Hyperscale database in the same or in a different Azure region. Geo-replicas must be created on a different logical server. The database name of a geo-replica always matches the database name of the primary.

When you create a geo-replica, all data is copied from the primary to a different set of page servers. A geo-replica doesn't share page servers with the primary, even if they are in the same region. This architecture provides the necessary redundancy for geo-failovers.

Geo-replicas are used to maintain a transactionally consistent copy of the database via asynchronous replication. If a geo-replica is in a different Azure region, it can be used for disaster recovery if there's a disaster or outage in the primary region. Geo-replicas can also be used for geographic read scale-out scenarios. As of October 2022, database copy from a Hyperscale geo secondary replica is supported.

Geo-replication for Hyperscale database has following current limitations:

- Only one geo-replica can be created (in the same or different region).
- Point in time restore of the geo-replica isn't supported.
- Creating geo-replica of a geo-replica (also known as "geo-replica chaining") isn't supported.

> [!IMPORTANT]  
> The ability to create multiple geo-replicas in the same or different regions is a preview feature.

## Troubleshoot

### Troubleshoot zone redundant Hyperscale named replicas

- For troubleshooting and testing application fault resiliency, see [Test application fault resiliency](high-availability-sla-local-zone-redundancy.md?view=azuresql-db&preserve-view=true#testing-application-fault-resiliency).

- Ensure at least one high availability replica is specified when creating a zone redundant named replica, in PowerShell and CLI. For an example, see [Create a Hyperscale named replica](hyperscale-named-replica-configure.md#create-a-hyperscale-named-replica).
    - In Azure CLI, you must specify both the parameters `ha-replicas` and `redundant`.
    - In PowerShell, you must specify the parameter `HighAvailabilityReplicaCount` and `ZoneRedundant`. 
    - If omitted, you receive the error message: `(ProvisioningDisabled) There is an insufficient number of high availability replicas to enable zone redundancy for a Hyperscale database.`

- The Hyperscale database should have zone redundancy already enabled as a prerequisite to enable this feature for named replicas. 
    - It's optional to enable zone redundancy for named replicas, even if primary database has zone redundancy enabled.
    - If not enabled, you receive the error message: `(DatabaseNamedReplicaSourceDatabaseNotZoneRedundant) Zone Redundancy cannot be enabled on this Named Replica since the primary Hyperscale Database is not zone redundant.`

## Known issues

#### Partially incorrect data returned from sys.databases

Row values returned from `sys.databases`, for named replicas, in columns other than `name` and `database_id`, might be inconsistent and incorrect. For example, the `compatibility_level` column for a named replica could be reported as 140 even if the primary database corresponding to the named replica is set to compatibility level 150. A workaround, when possible, is to get the same data using the `DATABASEPROPERTYEX()` function, which returns correct data.

## Related content

For tutorials on configuring and managing Hyperscale named replicas, see:

- [Create a Hyperscale named replica](hyperscale-named-replica-configure.md#create-a-hyperscale-named-replica)
- [Connect to a Hyperscale named replica](hyperscale-named-replica-configure.md#connect-to-a-hyperscale-named-replica)
- [Modify a Hyperscale named replica](hyperscale-named-replica-configure.md#modify-a-hyperscale-named-replica)

For more information, see:

- [Hyperscale service tier](service-tier-hyperscale.md)
- [Active geo-replication](active-geo-replication-overview.md)
- [Configure isolated access to a Hyperscale named replica](hyperscale-named-replica-security-configure.md)
- [Azure SQL Database Hyperscale named replicas FAQ](service-tier-hyperscale-frequently-asked-questions-faq.yml#read-scale-out-questions)
- [Modifiable configuration reference for Azure SQL Database](modifiable-configuration-reference.md)
