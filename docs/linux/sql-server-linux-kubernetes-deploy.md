---
title: Deploy SQL Server Always On Availability Group on Kubernetes Cluster
description: This article explains the parameters for the SQL Server Kubernetes Always On availability group operator global requirements
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
ms.date: 08/09/2018
ms.topic: article
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: linux
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Deploy a SQL Server Always On Availability Group on Kubernetes Cluster

To deploy a SQL Server Always On Availability Group on a Kubernetes Cluster

1. Configure the cluster

  Use a cluster with at least four nodes. One node is the master. The other nodes host SQL Server containers with the replicas. At least three replicas are required for high-availability.

1. Configure storage

  In cloud environments like Azure, configure persistent volumes.

1. Create Kubernetes secrets

1. Configure and deploy the SQL Server operator manifest

  Copy the SQL Server operator `.yaml` file from [sql-server-samples](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Linux)

  The `.yaml` file is the deployment manifiest for the Kubernetes.

  To configure the manifest, update the `.yaml` file for your environment.

  Apply the manifest to the Kubernetes cluster.

  ```azurecli
  kubectl apply -f .yaml
  ```

## Example

The following example describes a deployment for the `mssql-operator`.

[operator.yaml](http://sqlhelsinki.visualstudio.com/_git/pm-tools?path=%2Fkubernetes-ag-samples%2Fazure-kubernetes-service-sql-ag-example&version=GBmaster#path=%2Fkubernetes-ag-samples%2Fazure-kubernetes-service-sql-ag-example%2Foperator.yaml&version=GBmaster)

## Next steps

[SQL Server availability group on Kubernetes cluster](sql-server-ag-kubernetes.md)