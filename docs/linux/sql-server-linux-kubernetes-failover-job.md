---
title: SQL Server Always On availability group Kubernetes failover job environment variables
description: This article explains the environment variables for the SQL Server Kubernetes Always On availability group failover job
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
ms.date: 7/16/2018
ms.topic: article
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: linux
---

# SQL Server Always On availability group Kubernetes failover job environment variables

To fail over an Always On availability group primary replica to a different node in Kubernetes, use a job. This article identifies the environment variables for this job.

After you run the job, delete it. This also delets Kubernetes logs. If you do not delete the job, future failover jobs will fail undless you change the job name and the pod selector.

## Failover job environment variables

* `MSSQL_K8S_AG_NAME`
  * Optional
  * **Description**: The availability group to failover.

* `MSSQL_K8S_NEW_PRIMARY`
  * Optional
  * **Description**: The target of the failover. Accepts the target SQL Server pod name, IP address, StatefulSet name, server name, or host name. Additionally the `!` prefix will prevent failover to a  specified target. Example `!sql-1`.

* `FORCE_FAILOVER_ALLOW_DATA_LOSS` 
  * Optional
  * **Description**: Boolean to force the failover with potential data loss.

* `MSSQL_K8S_NAMESPACE`
  * Optional
  * **Description**: Kubernetes namespace of the availability group