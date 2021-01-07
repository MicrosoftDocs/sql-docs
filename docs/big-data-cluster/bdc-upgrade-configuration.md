---
title: Upgrade to a Configuration Management Enabled Big Data Cluster
titleSuffix: SQL Server big data clusters
description: Upgrade to a Configuration Management Enabled Big Data Cluster
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: rahul.ajmera
ms.date: 2/01/2021
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# Upgrading to a Configuration Management Enabled Cluster (CU8 or lower to CU9+)

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]
Starting in CU9, Big Data Clusters includes a configuration management feature that enables administrators to alter or tune various parts of the Big Data Cluster post-deployment and get deeper insights into the configurations running in their BDC. Prior to CU9, Big Data Cluster configurations were generally only modifiable at deployment time, with a workaround to configure some SQL configurations through a custom `mssql-custom.conf` file. This workaround has now been resolved and these settings are configurable through the configuration management feature.

## Migrating SQL configurations in mssql-custom.conf to the configuration management system
If you created a custom `mssql-custom.conf` for your SQL Server master instance(s), please follow the one-time instructions below to manage the settings through the configuration system and not the file. If you do not follow these steps, the configuration management functionality will not manage those SQL configurations and the `mssql-custom.conf` settings will override any changes made to those settings through the configuration management functionality.

Steps:
1. Upgrade the Big Data Cluster to CU9
> [!NOTE]
> Settings defined through `mssql-custom.conf` will not be changed or removed. They will just not be reflected and managed by the configuration framework.

2. Set and apply any settings previously defined in the `mssql-custom.conf` using the new configuration functionality. See [SQL Server Big Data Clusters Post-Deployment Configuration Overview](configure-bdc-postdeployment.md) for a step-by-step guide to change settings. See [SQL Server Big Data Clusters Configuration Properties](reference-config-bdc-overview.md) for a complete list of available settings for each scope. Please note that some settings like customerFeedback may have changed scope but are still available.
3. Delete the `mssql-custom.conf` file from the `mssql-server` container in each master pod. If you only have one master, `master-0`.
```bash
kubectl delete -f master-0:/var/opt/mssql/mssql-custom.conf -c mssql-server <namespaceName>
```

## Downgrading from a configuration management enabled Cluster to a non-configuration management enabled cluster (CU9+ to CU8 or lower)
Downgrading from a configuration management enabled Cluster to a non-configuration management enabled cluster will remove the ability to tune the Big Data Cluster post-deployment and will once again require the `mssql-custom.conf` file to set some SQL configurations. All settings defined and changed through the configuration management experience will be reverted.

Once the cluster is downgraded, the settings will revert to either their defaults or the values specified in the deployment bdc.json. No other steps are required after downgrade.