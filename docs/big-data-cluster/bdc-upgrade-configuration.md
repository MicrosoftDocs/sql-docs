---
title: Upgrade to a Configuration Management Enabled Big Data Cluster
titleSuffix: SQL Server Big Data Clusters
description: Upgrade to a Configuration Management Enabled Big Data Cluster
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: rahul.ajmera, hudequei
ms.date: 02/11/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# Upgrading to a Configuration Management Enabled Cluster (CU8 or lower to CU9+)

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Starting in CU9, Big Data Clusters includes a configuration management feature that enables administrators to alter or tune various parts of the Big Data Cluster post-deployment and get deeper insights into the configurations running in their BDC. Prior to CU9, Big Data Cluster configurations were generally only modifiable at deployment time, with a workaround to configure some SQL configurations through a custom `mssql-custom.conf` file. This workaround has now been resolved and these settings are configurable through the configuration management feature.

## Migrating SQL configurations in mssql-custom.conf to the configuration management system
If you created a custom `mssql-custom.conf` for your SQL Server master instance(s), please follow the one-time instructions below to manage the settings through the configuration system and not the file. If you do not follow these steps, the configuration management functionality will not manage those SQL configurations and the `mssql-custom.conf` settings will override any changes made to those settings through the configuration management functionality.

Steps:
1. Upgrade the Big Data Cluster to CU9
> [!NOTE]
> Settings defined through `mssql-custom.conf` will not be changed or removed. They will just not be reflected and managed by the configuration framework.

2. Set and apply any settings previously defined in the `mssql-custom.conf` using the new configuration functionality. See [SQL Server Big Data Clusters Post-Deployment Configuration Overview](configure-bdc-postdeployment.md) for a step-by-step guide to change settings. See [SQL Server Big Data Clusters Configuration Properties](reference-config-bdc-overview.md) for a complete list of available settings for each scope. Please note that some settings like customerFeedback may have changed scope but are still available.
3. Rename the `mssql-custom.conf` file to `deprecated-mssql-custom.conf` in the `mssql-server` container in each master pod. If you only have one master, `master-0`. In the event a downgrade or rollback to a non-configuration enabled cluster (CU8 or lower) is needed, this file can be reused to apply these custom SQL configurations.

## Downgrading from a configuration management enabled cluster to a non-configuration management enabled cluster (CU9+ to CU8 or lower)
Downgrading from a configuration management enabled cluster (CU9+) to a non-configuration management enabled cluster (CU8 or lower) will remove the ability to tune the Big Data Cluster post-deployment. It will also require use of the optional `mssql-custom.conf` file to set SQL configurations. If you renamed the file to `deprecated-mssql-custom.conf` upon upgrade to CU9+, rename it back to `mssql-custom.conf`. If you deleted the file or had not previously created it and now need to define these special SQL configurations, create it following the instructions here: [SQL Server Master Instance Configuration Properties -  Pre CU9 Release](reference-config-master-instance.md). All settings defined and changed through the configuration management experience will be reverted to your prior configurations or system defaults. 

Once the cluster is downgraded, the settings will revert to either their defaults or the values specified in the deployment bdc.json. No other steps are required after downgrade.