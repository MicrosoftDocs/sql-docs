---
title: Configure SQL Server master instance
description: Configure the master instance of a SQL Server Big Data Cluster
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 6/22/2022
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: overview
ms.custom: kr2b-contr-experiment
---

# Configure the master instance of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Learn how to configure the master instance of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)].

You can configure the master instance of SQL Server Big Data Clusters. However server configuration settings can't be configured at deployment time. Beginning in *Cumulative Update 9*, *Big Data Cluster* includes a configuration management feature. This enables administrators to alter or tune various parts of the Big Data Cluster post-deployment. It also gives them deeper insights into configurations running in their big data cluster.

This article describes a temporary workaround for configuring SQL Server master instance settings, including: SQL Server edition, enable or disable SQL Server Agent, enable specific trace flags, enable/disable customer feedback, or `domainmapping`.

To change master instance settings, follow these steps:

1. Create a custom `mssql-custom.conf` file that includes targeted settings. The following example enables SQL Agent, telemetry, sets a PID for Enterprise Edition, and enables trace flag 1204:

   ```
   [sqlagent]
   enabled=true
   
   [telemetry]
   customerfeedback=true
   userRequestedLocalAuditDirectory = /tmp/audit

   [DEFAULT]
   pid = Enterprise

   [traceflag]
   traceflag0 = 1204
   ```

1. Copy `mssql-custom.conf` file to `/var/opt/mssql` in the `mssql-server` container in the `master-0` pod. Replace `<namespaceName>` with the big data cluster name.

   ```bash
   kubectl cp mssql-custom.conf master-0:/var/opt/mssql/mssql-custom.conf -c mssql-server -n <namespaceName>
   ```

1. Restart the SQL Server instance.  Replace `<namespaceName>` with the big data cluster name.

   ```bash
   kubectl exec -it master-0  -c mssql-server -n <namespaceName> -- /bin/bash
   supervisorctl restart mssql-server
   exit
   ```

> [!IMPORTANT]
> If SQL Server master instance is in an availability groups configuration, copy the `mssql-custom.conf` file in all the `master` pods. Note that each restart will cause a failover, so you must make sure you're scheduling this activity during downtime.

## Known limitations

- The steps above require Kubernetes cluster admin permissions
- You can't change the server collation for the SQL Server master instance of the big data cluster post deployment.

## Next steps

For more information on deploying SQL Server Big Data Clusters, see [Get started with SQL Server Big Data Clusters](deploy-get-started.md).
