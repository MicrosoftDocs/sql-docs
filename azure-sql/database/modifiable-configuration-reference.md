---
title: Modifiable Configuration Reference
titleSuffix: Azure SQL Database
description: Learn which configuration settings you can modify after you create Azure SQL Database, and which settings are immutable.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: strrodic
ms.date: 06/30/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: reference
ai-usage: ai-assisted
---
# Modifiable configuration reference for Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](modifiable-configuration-reference.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/modifiable-configuration-reference.md?view=azuresql-mi&preserve-view=true)

This article identifies which [Azure SQL Database](sql-database-paas-overview.md) configuration settings you can modify after you create your database, and which settings are immutable and require you to recreate the database.

## Legend

| Symbol | Meaning |
| --- | --- |
| **Yes** | Fully modifiable after you create your database. |
| **No** | You can't change it. Requires recreation or a workaround. |
| **Partially** | Modifiable with caveats or restrictions. |
| **N/A** | Not a persistent setting. Only relevant when you create the database. |

The **Scope** column indicates whether a setting is for the [logical server](logical-servers.md) and applies to all its databases, or for an individual database, or for both.

## Basics

Settings you provide (or accept defaults for) when you create the database, plus basic metadata.

| Configuration | Scope | Values | Can modify after deployment|
| --- | --- | --- | --- |
| **Subscription or resource group** | Logical server (databases move with it) | Any active Azure subscription or resource group | **Partially**.<br /><br />You can't move a database on its own. You move the parent logical server by using [Azure Resource Manager](/azure/azure-resource-manager/management/move-support-resources), and all of its databases and elastic pools move with it. To move a single database, [copy](database-copy.md) or [geo-replicate](active-geo-replication-overview.md) it to a server in the target subscription or resource group. |
| **Database name** | Database | User-defined (unique per server) | **Yes**.<br /><br />You can rename a database with [`ALTER DATABASE ... MODIFY NAME`](/sql/t-sql/statements/alter-database-transact-sql?view=azuresqldb-current&preserve-view=true). No users can be connected during the rename, and the database can't participate in [active geo-replication](active-geo-replication-overview.md) or be in the process of creating a [database copy](database-copy.md). |
| **Logical server name** | Logical server | User-defined (globally unique on Azure) | **No**. |
| **Parent logical server** | Database | Existing or new logical server | **No**.<br /><br />You can't change a database's logical server after you create the database. [Copy](database-copy.md) or [geo-replicate](active-geo-replication-overview.md) to a different server. |
| **Server location** | Logical server | Any Azure region supported by Azure SQL Database. | **No**.<br /><br />See [Azure SQL Database region availability](region-availability.md). |
| **SQL authentication method** | Logical server | SQL authentication only, Microsoft Entra + SQL authentication (mixed), Microsoft Entra-only | **Yes**.<br /><br />See [Configure Microsoft Entra authentication](authentication-aad-configure.md). |
| **SQL authentication server admin login** | Logical server | User-defined (set when you create the server) | **No**.<br /><br />You can't rename the SQL authentication server admin login after the server is provisioned. |
| **SQL authentication server admin password** | Logical server | User-defined | **Yes**. |
| **Microsoft Entra admin** | Logical server | User or group | **Yes**.<br /><br />See [Configure and manage Microsoft Entra authentication](authentication-aad-configure.md). |
| **Workload environment** | Database (creation-time preset) | Development, Production | **N/A**.<br /><br />The workload environment is an Azure portal-only preset that influences defaults (compute, backup redundancy) when you create the database. It isn't a stored configuration. |
| **Collation** | Database | Database collation (for example, `SQL_Latin1_General_CP1_CI_AS`). | **No**.<br /><br />You can't change the database collation after you create the database. You must recreate the database. See [Collation and Unicode support](/sql/relational-databases/collations/collation-and-unicode-support). |
| **Tags** | Logical server and each database (independent) | Key-value pairs | **Yes**. |

## Compute and storage

| Configuration | Scope | Values | Can modify after deployment|
| --- | --- | --- | --- |
| **Elastic pool membership** | Database | Elastic pool, Standalone | **Yes**.<br /><br />See [Elastic pools](elastic-pool-overview.md). |
| **Service tier** | Database (or elastic pool) | General Purpose, Business Critical, Hyperscale (vCore) <br /> Basic, Standard, Premium (DTU)  | **Partially**.<br /><br />You can switch between service tiers, and you can [upgrade to Hyperscale](convert-to-hyperscale.md) in the vCore purchasing model. [Reverse migration from Hyperscale](reverse-migrate-from-hyperscale.md) back to General Purpose is possible within 45 days of conversion. Databases originally created as Hyperscale can't revert. To learn more, see [Scale resources](scale-resources.md). |
| **Compute tier** | Database (or elastic pool) | Provisioned, Serverless. | **Yes**.<br /><br />You can switch between Provisioned and Serverless. Serverless is available only on General Purpose and Hyperscale. It isn't available on Business Critical. See [Serverless compute tier](serverless-tier-overview.md). |
| **Compute hardware** | Database (or elastic pool) | Standard-series (Gen5), Premium-series, Premium-series memory optimized, DC-series | **Yes**.<br /><br />Availability depends on the service tier and compute tier. See [Hardware configuration](service-tiers-sql-database-vcore.md#hardware-configuration). |
| **vCores (Provisioned, or min/max for Serverless)** | Database (or elastic pool) | From 0.5 vCores (serverless) up to 128+ vCores, depending on service tier, compute tier, and hardware | **Yes**.<br /><br />You can scale up or down within applicable limits. See [single database limits](resource-limits-vcore-single-databases.md), [elastic pool limits](resource-limits-vcore-elastic-pools.md), and [Hyperscale limits](service-tier-hyperscale.md). |
| **Auto-pause delay (Serverless)** | Database | Configurable delay, Disabled | **Yes**.<br /><br />Available for General Purpose serverless only. Hyperscale serverless doesn't support auto-pause. See [Auto-pause and auto-resume](serverless-tier-overview.md#auto-pause-and-auto-resume). |
| **Reserved storage size** | Database (or elastic pool) | Varies by service tier and hardware. | **Yes**.<br /><br />You can scale reserved storage up or down, but never below the storage space already in use. See [Resource limits for single databases](resource-limits-vcore-single-databases.md), [Resource limits for elastic pools](resource-limits-vcore-elastic-pools.md), and [Hyperscale resource limits](service-tier-hyperscale.md). |

## Networking

| Configuration | Scope | Values | Can modify after deployment|
| --- | --- | --- | --- |
| **Connectivity method** | Logical server | Public endpoint, Private endpoint, No public access. | **Yes**.<br /><br />See [Azure SQL Database connectivity architecture](connectivity-architecture.md). |
| **Allow Azure services access** | Logical server (firewall rule) | Yes, No | **Yes**.<br /><br />Implemented as the server-level firewall rule `AllowAllWindowsAzureIps` (0.0.0.0). See [Network access controls](network-access-controls-overview.md). |
| **Firewall rules** | Logical server and database | IP-based rules. | **Yes**.<br /><br />See [Server-level and database-level firewall rules](firewall-configure.md). |
| **Virtual network rules** | Logical server | Subnet-based service endpoints. | **Yes**.<br /><br />See [Virtual network service endpoints](vnet-service-endpoint-rule-overview.md). |
| **Private endpoints** | Logical server | Private endpoint connections to the logical server. | **Yes**.<br /><br />See [Azure Private Link for Azure SQL Database](private-endpoint-overview.md). |
| **Connection policy** | Logical server | Default, Proxy, Redirect. | **Yes**.<br /><br />See [Azure SQL Database connection policy](connectivity-architecture.md#connection-policy). |
| **Minimum TLS version** | Logical server | TLS 1.2 | **Yes**.<br /><br />TLS 1.2 is the default. TLS 1.0 and 1.1 are retired. See [Minimum TLS version](connectivity-settings.md#minimum-tls-version) and [TLS 1.3 support](/sql/relational-databases/security/networking/tls-1-3). |

## Security

| Configuration | Scope | Values | Can modify after deployment|
| --- | --- | --- | --- |
| **Microsoft Defender for SQL** | Logical server | Enabled, Disabled. | **Yes**.<br /><br />See [Microsoft Defender for SQL](/azure/defender-for-cloud/defender-for-sql-introduction). |
| **Ledger** | Database | Enabled (Ledger database), Disabled | **No**.<br /><br />You can't convert a ledger database to a regular database, or the reverse. See [Azure SQL Database ledger](/sql/relational-databases/security/ledger/ledger-overview). |
| **Managed identity** | Logical server | System-assigned, User-assigned, None. | **Yes**.<br /><br />See [Managed identities for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md). |
| **Transparent data encryption (TDE)** | Logical server (TDE protector); enforced per database | Service-managed key, Customer-managed key (CMK). | **Yes**.<br /><br />You can switch between service-managed and customer-managed keys, and rotate keys manually or automatically. See [TDE for Azure SQL Database](transparent-data-encryption-tde-overview.md). |
| **TDE encryption key (CMK)** | Logical server (TDE protector) | Specific Azure Key Vault key. | **Yes**.<br /><br />You can rotate the key manually or automatically. See [Customer-managed TDE](transparent-data-encryption-byok-overview.md). |
| **Auditing** | Logical server and database | Enabled, Disabled. | **Yes**.<br /><br />Server-level auditing applies to all databases on the server. Database-level auditing can be configured independently. See [Auditing for Azure SQL Database](auditing-overview.md). |

## High availability and disaster recovery

| Configuration | Scope | Values | Can modify after deployment|
| --- | --- | --- | --- |
| **Zone redundancy** | Database (or elastic pool) | Enabled, Disabled. | **Partially**.<br /><br />You can enable or disable zone redundancy for Azure SQL Database General Purpose (vCore) and Business Critical/Premium tiers as an online operation. **Azure SQL Database Hyperscale**: you can only specify zone redundancy when you create the database, and you can't change it later. To change it, use [database copy](database-copy.md), [point-in-time restore](recovery-using-backups.md#point-in-time-restore), or a [geo-replica](active-geo-replication-overview.md). See [Zone-redundant availability](high-availability-sla-local-zone-redundancy.md#high-availability-through-zone-redundancy). |
| **High availability replicas** | Database | 0-4 (Hyperscale); fixed at 3 (Business Critical and Premium) | **Yes (Hyperscale only)**.<br /><br />In Business Critical and Premium, the three HA replicas are automatic and you can't configure the count. In Hyperscale, you can configure 0-4 HA replicas. See [Hyperscale high availability](service-tier-hyperscale.md#database-high-availability-in-hyperscale). |
| **Read scale-out** | Database | Enabled, Disabled | **Yes**.<br /><br />Read scale-out replicas are available on Premium and Business Critical tiers. Hyperscale provides equivalent read scale-out through HA replicas and up to 30 [named replicas](service-tier-hyperscale-replicas.md#named-replica) with independently configurable compute (see the **High availability replicas** row). See [Read scale-out](read-scale-out.md). |
| **Geo-replication** | Database (active geo-replication); logical server (failover groups) | Active geo-replication, Failover groups | **Yes**.<br /><br />You can configure geo-replication after you create the database. See [Active geo-replication](active-geo-replication-overview.md) and [Failover groups](failover-group-sql-db.md). |

## Backup and restore

| Configuration | Scope | Values | Can modify after deployment|
| --- | --- | --- | --- |
| **Backup storage redundancy** | Database | LRS, ZRS, GRS, GZRS | **Partially**.<br /><br />**General Purpose/Business Critical**: backup storage redundancy is modifiable. Changes apply to future backups and might take up to 48 hours to take effect. **Hyperscale and Basic**: backup storage redundancy is set at creation and can't be changed afterward. To change the redundancy, use [active geo-replication](active-geo-replication-overview.md), [database copy](database-copy.md), or [point-in-time restore](recovery-using-backups.md#point-in-time-restore). Hyperscale supports LRS, ZRS, GRS, and GZRS where available. See [Backup storage redundancy](automated-backups-overview.md#backup-storage-redundancy). |
| **Backup retention (PITR)** | Database | 1–35 days (default 7) | **Yes**.<br /><br />Varies by service tier. See [Point-in-time restore (PITR)](recovery-using-backups.md#point-in-time-restore). |
| **Long-term retention (LTR)** | Database | Weekly, Monthly, Yearly, up to 10 years. | **Yes**.<br /><br />See [Long-term retention](long-term-retention-overview.md). |

## Additional settings

| Configuration | Scope | Values | Can modify after deployment|
| --- | --- | --- | --- |
| **Maintenance window** | Database (or elastic pool) | System default (5 PM–8 AM), Weekday (10 PM–6 AM Mon–Thu), Weekend (10 PM–6 AM Fri–Sun) | **Yes**.<br /><br />For Azure SQL Database, you can change your desired maintenance window at any time. For supported service tiers and hardware, see [Maintenance window](maintenance-window.md). |
| **Diagnostic settings** | Database | Log Analytics, Event Hubs, Storage Account. | **Yes**.<br /><br />See [Diagnostic telemetry for export](metrics-diagnostic-telemetry-logging-streaming-export-configure.md). |

## Unchangeable settings after deployment

You can't change the following settings after you create your database. Plan them carefully before you provision the database.

| Setting | Why this matters |
| --- | --- |
| **Collation** | You can't change the database collation after you create the database. |
| **Ledger** | After you create a database as a ledger database (or as a regular database), you can't switch between the two. |
| **Logical server name and region** | You can't change the logical server name or its region after you create the server. |
| **Parent logical server** | You can't change a database's logical server after you create the database. Copy or geo-replicate to a different server. |
| **SQL authentication server admin login** | You can't rename the SQL authentication server admin login after you create the server. |
| **Hyperscale-only specifics** | When you create the database in Hyperscale, you can't change the backup storage redundancy or zone redundancy later. You can [convert databases to Hyperscale](convert-to-hyperscale.md) and [reverse-migrate from Hyperscale](reverse-migrate-from-hyperscale.md) within 45 days, but databases originally created as Hyperscale can't revert. |

## Related content

- [What is Azure SQL Database?](sql-database-paas-overview.md)
- [Resource limits for single databases](resource-limits-vcore-single-databases.md)
- [Service tiers - vCore purchasing model](service-tiers-sql-database-vcore.md)
- [What's new in Azure SQL Database?](doc-changes-updates-release-notes-whats-new.md)