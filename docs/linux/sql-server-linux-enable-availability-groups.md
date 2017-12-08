---
title: Enable availability groups for SQL Server on Linux | Microsoft Docs
description: How to use mssql-conf to enable availability groups for SQL Server on Linux.
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 12/4/2017
ms.topic: article
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "linux"
ms.suite: "sql"
ms.custom: ""
ms.technology: database-engine
ms.workload: "On Demand"
---

# Enable availability groups for SQL Server on Linux

[!INCLUDE[tsql-appliesto-sslinux-only](../includes/tsql-appliesto-sslinux-only.md)]

Under Linux, you must use `mssql-conf` to enable the availability groups (AG) feature. Unlike on Windows, you cannot use PowerShell or SQL Server Configuration Manager to enable the feature, and unlike SQL Server 2016 and earlier on Windows, you can enable AGs with or without creating the underlying Pacemaker cluster first. Integration with the cluster, if needed, is not done until later.

> [!IMPORTANT]
> The AG feature must be enabled for configuration-only replicas, even on SQL Server Express Edition.

## Prerequisite
The `mssql-server-ha` package must already be installed. See [Install the HA and SQL Server Agent packages](sql-server-linux-deploy-pacemaker-cluster.md#install-the-ha-and-sql-server-agent-packages).

## Enable the availability groups feature
There are two ways to enable the availability groups feature: use the `mssql-conf` utility, or edit the `mssql.conf` file manually.

### Use the mssql-conf utility

At a prompt, issue the following:

```bash
sudo /opt/mssql/bin/mssql-conf set hadr.hadrenabled 1
```

### Edit the mssql.conf file

You can also modify the `mssql.conf` file, located under the `/var/opt/mssql` folder, to add the following lines:

```
[hadr]

hadr.hadrenabled = 1
```

## Restart SQL Server
After enabling availability groups, as on Windows, you must restart SQL Server. That can be done by the following:

```bash
sudo systemctl restart mssql-server
```

