---
title: SQL Server Always On availability group Kubernetes operator global requirements
description: This article explains the parameters for the SQL Server Kubernetes Always On availability group operator global requirements
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
ms.date: 08/09/2018
ms.topic: article
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# SQL Server Always On availability group Kubernetes operator parameters

An Always On availability group on Kubernetes requires an operator. A manifest describes the operator. The manifest is a `.yaml` file. See an example of the specification in [Always On availability groups for SQL Server containers](sql-server-ag-kubernetes.md).

This article explains the operator global environment variables.

## Example

The following example describes a deployment for the `mssql-operator`.

## Global environment variables

* `MSSQL_K8S_POD_NAMESPACE` 
  * Required
  * **Description**: The Kubernetes namespace of the operator.

* `MSSQL_K8S_SQL_WRITE_LEASE_PERIOD_SECONDS`
  * Optional
  * **Description**: The duration of the sql server write-lease. Used to keep the sql server writable and prevent split-brain scenarios. Secondary replicas wait for this number of seconds after selecting a new leader.

* `MSSQL_K8S_MONITOR_PERIOD_SECONDS`
  * Optional
  * **Description**: The period to monitor the state of the availability group. Determines how quickly replicas are added and dropped. Must be less than `MSSQL_K8S_SQL_WRITE_LEASE_PERIOD_SECONDS`.
  * **Default**: 1

* `MSSQL_K8S_LEASE_DURATION_SECONDS`
  * Optional
  * **Description**: Duration of availability group leader lease. Determines how long the secondary replicas wait before re-electing if the primary replica crashed. This is different than SQL Server write lease. 
  * **Default**: 10
  
  >[!NOTE]
  >Other settings automatically calculated based on `MSSQL_K8S_LEASE_DURATION_SECONDS`.

* `MSSQL_K8S_RENEW_DEADLINE_SECONDS`
  * Optional
  * **Description**: Duration that the acting primary replica retries refreshing leadership before giving up. Must be less than `MSSQL_K8S_LEASE_DURATION_SECONDS`.
  * **Default**: `MSSQL_K8S_LEASE_DURATION_SECONDS`/2

* `MSSQL_K8S_RETRY_PERIOD_SECONDS`
  * Optional
  * **Description**: Duration the acting [master](https://kubernetes.io/docs/concepts/architecture/master-node-communication/) will wait before renewing the leader lease. Must be less than `MSSQL_K8S_LEASE_DURATION_SECONDS`.
  * **Default**: `MSSQL_K8S_RENEW_DEADLINE_SECONDS`/2

* `MSSQL_K8S_ACQUIRE_PERIOD_SECONDS` 
  * Optional
  * **Description**: Period that secondary replicas poll if the leader lease has expired. 
  * **Default**: 1


  ## Next steps

[SQL Server availability group on Kubernetes cluster](sql-server-ag-kubernetes.md)
