---
title: Configure SQL Server master instance
titleSuffix: Configure SQL Server master instance of Big Data Cluster
description: Configure the master instance of a SQL Server Big Data Cluster
author: dacoelho
ms.author: dacoelho
ms.reviewer: wiassaf
ms.date: 10/04/2021
ms.topic: overview
ms.prod: sql
ms.technology: big-data-cluster
ms.custom:
  - intro-overview
---

# Configure master instance of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

Configure master instance of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)].

Server configuration settings cannot be configured for SQL Server master instance at deployment time. Starting in CU9, Big Data Clusters includes a configuration management feature that enables administrators to alter or tune various parts of the Big Data Cluster post-deployment and get deeper insights into the configurations running in their big data cluster. 

This article describes a temporary workaround on how to configure settings, including: SQL Server edition, enable or disable SQL Server Agent, enable specific trace flags, enable/disable customer feedback, or `domainmapping`.

To change any of these settings, follow these steps:

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

1. Restart SQL Server instance.  Replace `<namespaceName>` with the big data cluster name.

   ```bash
   kubectl exec -it master-0  -c mssql-server -n <namespaceName> -- /bin/bash
   supervisorctl restart mssql-server
   exit
   ```

> [!IMPORTANT]
> If SQL Server master instance is in an availability groups configuration, copy the `mssql-custom.conf` file in all the `master` pods. Note that each restart will cause a failover, so you must make sure you are timing this activity during downtime periods.

## Known limitations

- The steps above require Kubernetes cluster admin permissions
- You cannot change the server collation for SQL Server master instance of the big data cluster post deployment.

## Next steps

For more information about deploying SQL Server Big Data Clusters, see [Get started with SQL Server Big Data Clusters](deploy-get-started.md).
