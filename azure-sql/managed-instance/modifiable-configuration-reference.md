---
title: Modifiable Configuration Reference
description: Learn which configuration settings you can modify after you create Azure SQL Managed Instance, and which settings are immutable.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: mlandzic, vladiv, urmilano
ms.date: 08/10/2026
ms.service: azure-sql-managed-instance
ms.subservice: service-overview
ms.topic: reference
ai-usage: ai-assisted
---
# Modifiable configuration reference for Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/modifiable-configuration-reference.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](modifiable-configuration-reference.md?view=azuresql-mi&preserve-view=true)

This article identifies which [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) configuration settings you can modify after you create your instance, and which settings are immutable and require you to create a new instance.

## Basics

| Configuration | Values | Can modify after deployment |
| --- | --- | --- |
| **Subscription** | Any active Azure subscription | **No**.<br /><br />You must create a new instance in the target subscription and migrate databases. |
| **Resource group** | Any existing or new resource group | **No**.<br /><br />You must create a new instance in the target resource group and migrate databases. |
| **Instance name** | User-defined (unique, forms part of DNS name) | **No**. |
| **Region** | Any Azure region | **No**.<br /><br />You must create a new instance in the target region and migrate databases. For guidance, see [Move resources across regions](move-resources-across-regions.md). |
| **Collation** | Instance-level collation (for example, `SQL_Latin1_General_CP1_CI_AS`) | **No**.<br /><br />You must create a new instance. For supported values, see [Collation and Unicode support](/sql/relational-databases/collations/collation-and-unicode-support). |
| **Time zone** | Instance-level time zone (for example, `UTC`, `Central European Standard Time`) | **No**.<br /><br />See [Time zones overview](timezones-overview.md). |
| **Tags** | Key-value pairs | **Yes**. |

## Compute and storage

| Configuration | Values | Can modify after deployment |
| --- | --- | --- |
| **Service tier** | General Purpose, Next-gen General Purpose, Business Critical | **Yes**.<br /><br />You can switch service tiers. Switching to, or from, the Business Critical service tier involves a full data seed and can take significant time. See [Service tiers](service-tiers-managed-instance-vcore.md), [Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) and [Scale resources](../database/scale-resources.md). |
| **Compute hardware** | Standard-series (Gen5), Premium-series, Premium-series memory optimized | **Yes**.<br /><br />You can change the hardware generation. For current options, see [Resource limits](resource-limits.md#hardware-configuration-characteristics). |
| **vCores** | Varies by service tier and hardware | **Yes**.<br /><br />You can scale up or down within the limits documented in [Resource limits](resource-limits.md#service-tier-characteristics). |
| **Instance storage size** | Varies by service tier and hardware | **Yes**.<br /><br />You can scale reserved storage up or down, but never below the storage space already in use. For current maximums, see [Resource limits](resource-limits.md#service-tier-characteristics). |
| **License type** | Azure Hybrid Benefit, License Included | **Yes**.<br /><br />You can switch between license types at any time with no downtime. See [Azure Hybrid Benefit](../azure-hybrid-benefit.md). |
| **Free offer** | Free, Paid | **Yes (with limits)**.<br /><br />You can upgrade a free instance to a paid instance from the Azure portal, but the change is irreversible. To use the free offer again, create a new instance. See [Free offer](free-offer.md). |
| **Instance pool membership** | Standalone, Pooled | **Yes (with limits)**.<br /><br />Only General Purpose tier instances can join a pool. You can create a new instance inside a pool from the Azure portal, but moving an existing instance into, or out of, a pool requires PowerShell or the Azure CLI. The instance must be in the same subnet and resource group as the pool. See [Instance pools overview](instance-pools-overview.md) and [Configure instance pools](instance-pools-configure.md). |
| **`tempdb` configuration** | Number of files, file size | **Yes**.<br /><br />Configurable at the instance level. See [Configure tempdb](tempdb-configure.md). |

## Networking

| Configuration | Values | Can modify after deployment |
| --- | --- | --- |
| **Virtual network or subnet** | Dedicated, delegated subnet in an Azure virtual network | **Yes (with limits)**.<br /><br />You can move to a different subnet in the same or a peered virtual network in the same region. Moving to a subnet in an unrelated virtual network, or to another region, isn't supported. Brief downtime occurs during cutover. See [Move an instance to another subnet](vnet-subnet-move-instance.md). |
| **Connection type** | Proxy, Redirect | **Yes**.<br /><br />See [Connection types](connection-types-overview.md). |
| **Public endpoint** | Enabled, Disabled | **Yes**.<br /><br />The public endpoint always uses the proxy connection type. See [Configure the public endpoint](public-endpoint-configure.md). |
| **Private endpoint** | User-configured endpoints | **Yes**.<br /><br />See [Private endpoint overview](private-endpoint-overview.md). |
| **Minimum TLS version** | 1.2 | **Yes**.<br /><br />See [Configure minimum TLS version](minimal-tls-version-configure.md). |
| **Service endpoint policies** | User-configured endpoints | **Yes**.<br /><br />See [Configure service endpoint policies](service-endpoint-policies-configure.md). |

## Security

| Configuration | Values | Can modify after deployment |
| --- | --- | --- |
| **Authentication method** | SQL authentication only, Microsoft Entra + SQL authentication (mixed), Microsoft Entra-only | **Yes**. |
| **SQL authentication admin login name** | User-defined (set when you create the instance) | **No**. |
| **SQL authentication admin password** | User-defined | **Yes**.<br /><br />Reset it through the Azure portal, PowerShell, or the Azure CLI. |
| **Microsoft Entra admin** | User or group | **Yes**. |
| **Microsoft Defender for SQL** | Enabled, Disabled | **Yes**. |
| **Managed identity** | System-assigned, User-assigned, None | **Yes**. |
| **Transparent data encryption (TDE)** | Service-managed key, Customer-managed key (CMK) | **Yes**.<br /><br />You can switch between service-managed and customer-managed keys. |
| **TDE encryption key (CMK)** | Specific Azure Key Vault key | **Yes**.<br /><br />You can rotate the key manually or automatically. |
| **Auditing** | Enabled, Disabled | **Yes**.<br /><br />See [Configure auditing](auditing-configure.md). |

## High availability and disaster recovery

| Configuration | Values | Can modify after deployment |
| --- | --- | --- |
| **Zone redundancy** | Enabled, Disabled | **Yes**.<br /><br />Available for General Purpose and Business Critical service tiers. Zone redundancy for the Next-gen General Purpose service tier is currently in preview. Enabling or disabling is an online operation similar to a service tier change. See [Configure zone redundancy](instance-zone-redundancy-configure.md). |
| **Failover groups** | Auto-failover groups with geo-replication | **Yes**.<br /><br />See [Failover groups overview](failover-group-sql-mi.md) and [Configure failover groups](failover-group-configure-sql-mi.md). |
| **Managed Instance link** | Enabled, Disabled | **Yes**.<br /><br />See [Managed Instance link overview](managed-instance-link-feature-overview.md). |
| **Stop/start instance** | Stopped, Running | **Yes (with limits)**.<br /><br />Only available for the General Purpose service tier. Not supported for instances in [failover groups](failover-group-sql-mi.md), [instance pools](instance-pools-overview.md), with [Managed Instance link](managed-instance-link-feature-overview.md), or with [zone redundancy](instance-zone-redundancy-configure.md) enabled. See [Stop and start an instance](instance-stop-start-how-to.md). |

## Backup and restore

| Configuration | Values | Can modify after deployment |
| --- | --- | --- |
| **Backup storage redundancy** | Locally redundant (LRS), Zone redundant (ZRS), Geo redundant (GRS), Geo-zone redundant (GZRS) | **Yes**.<br /><br />You can change backup storage redundancy after you create your instance. The change applies to future backups only and might take up to 24 hours to take effect. See [Change backup settings](automated-backups-change-settings.md). |
| **Point-in-time restore (PITR) retention** | 1–35 days (default 7 days) | **Yes**.<br /><br />See [Automated backups](automated-backups-overview.md) and [Change backup settings](automated-backups-change-settings.md). |
| **Long-term retention (LTR)** | Weekly, Monthly, Yearly, up to 10 years | **Yes**.<br /><br />See [Configure long-term backup retention](long-term-backup-retention-configure.md). |

## Additional settings

| Configuration | Values | Can modify after deployment |
| --- | --- | --- |
| **Update policy** | SQL Server 2022, SQL Server 2025, Always-up-to-date | **Yes (one-way)**.<br /><br />You can upgrade to a higher SQL Server version or to Always-up-to-date, but the change is irreversible. You can't downgrade to an earlier update policy. See [Update policy](update-policy.md). |
| **Maintenance window** | System default, Weekday, Weekend | **Yes**.<br /><br />In Azure SQL Managed Instance, the system default maintenance window is 5 PM–8 AM local time. See [Configure maintenance window](maintenance-window-configure.md). |
| **Advance notifications** | Enabled, Disabled | **Yes**.<br /><br />See [Advance notifications](advance-notifications.md). |
| **Diagnostic settings** | Log Analytics, Event Hubs, Storage Account | **Yes**.<br /><br />See [Monitor with Azure Monitor](monitoring-sql-managed-instance-azure-monitor.md). |
| **Max degree of parallelism (MAXDOP)** | Default MAXDOP for queries on the instance | **Yes**.<br /><br />Configurable with [`sp_configure`](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql?view=azuresqldb-mi-current&preserve-view=true). |
| **Automatic tuning** | `FORCE_LAST_GOOD_PLAN` | **Yes**.<br /><br />Azure SQL Managed Instance supports only the `FORCE_LAST_GOOD_PLAN` option (automatic plan correction). The `CREATE INDEX` and `DROP INDEX` options aren't supported on SQL managed instances. See [Automatic tuning](../database/automatic-tuning-overview.md). |

## Unchangeable settings after deployment

You can't change the following settings after you create your instance. Plan them carefully before you provision the instance.

| Setting | Why this matters |
| --- | --- |
| **Collation** | You can't change the [instance collation](/sql/relational-databases/collations/set-or-change-the-server-collation?view=azuresqldb-mi-current&preserve-view=true#set-the-server-collation-in-azure-sql-managed-instance) after you create your SQL managed instance. All databases inherit the instance collation as the default. You can change the [database collation](/sql/relational-databases/collations/set-or-change-the-database-collation?view=azuresqldb-mi-current&preserve-view=true) in Azure SQL Managed Instance. |
| **Time zone** | You can't change the time zone after you create your instance. |
| **Admin login name** | You can't change the SQL authentication admin login name after you create your instance, but you can reset the password. |
| **Subscription, resource group, region, and instance name** | You can't change these identifiers after you create your instance. To change any of them, create a new instance and migrate your databases. |
| **Update policy and free offer upgrades** | Upgrading the [update policy](update-policy.md) to a higher SQL Server version (or to **Always-up-to-date**) and upgrading a [free offer](free-offer.md) instance to paid are both one-way operations that you can't reverse. |

## Related content

- [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md)
- [Resource limits](resource-limits.md)
- [Service tiers](service-tiers-managed-instance-vcore.md)
- [What's new in Azure SQL Managed Instance?](doc-changes-updates-release-notes-whats-new.md)
