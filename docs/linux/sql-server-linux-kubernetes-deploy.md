---
title: Deploy SQL Server Always On Availability Group on Kubernetes Cluster
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
# Deploy a SQL Server Always On Availability Group on Kubernetes Cluster

## Requirements

- A Kubernetes cluster
- Kubernetes version 1.11.0 or higher
- Four or more nodes
- [kubectl](http://kubernetes.io/docs/tasks/tools/install-kubectl/).

  >[!NOTE]
  >You can use any type of Kubernetes cluster. To create a Kubernetes cluster on Azure Kubernetes Service (AKS), see [Create an AKS cluster](http://docs.microsoft.com/azure/aks/create-cluster).
  > The following script creates a four node Kubernetes cluster in Azure.
  >```azure-cli
  az aks create --resource-group myResourceGroup --name myAKSCluster --node-count 4 --kubernetes-version 1.11.1
  >```

## Steps

1. Configure storage

  In cloud environments like Azure, configure [persistent volumes](http://kubernetes.io/docs/concepts/storage/persistent-volumes/) for each instance of SQL Server.

  To create persistent volumes in Azure, see `pv.yaml` and `pvc.yaml` in [sql-server-samples](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-deployment-script/templates).

  To create the storage run the following command:

  ```azurecli
  kubectl apply -f <pv.yaml>
  ```

1. Create Kubernetes secrets for the SA password and the master key.

  The following example creates two secrets. `sapassword` is for the SA password and `masterkeypassword` is for the master key. Before you run this script replace `<MyC0mp13xP@55w04d!>` with different complex password for each secret.

   ```azurecli
   kubectl create secret generic sql-secrets --from-literal='sapassword=<MyC0mp13xP@55w04d!>' --from-literal='masterkeypassword=<MyC0mp13xP@55w04d!>'
   ```

1. Configure and deploy the SQL Server operator manifest.

  Copy the SQL Server operator `operator.yaml` file from [sql-server-samples](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-manifest-files).

  The `operator.yaml` file is the deployment manifiest for the Kubernetes operator.

  To configure the manifest, update the `operator.yaml` file for your environment.

  Apply the manifest to the Kubernetes cluster.

  ```azurecli
  kubectl apply -f operator.yaml
  ```

1. Deploy the SQL Server custom resource.

  Copy the SQL Server manifest `sqlserver.yaml` from [sql-server-samples](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-manifest-files).

  Apply the manifest to the Kubernetes cluster.

  ```azurecli
  kubectl apply -f sqlserver.yaml
  ```

After you deploy the SQL Server manifest, the operator deploys the instances of SQL Server as pods in containers.

After the script completes, the Kubernetes operator will create the storage, the SQL Server instances, the load balancer services. You can monitor the deployment with [Kubernetes dashboard](http://docs.microsoft.com/azure/aks/kubernetes-dashboard).

After Kubernetes creates the SQL Server containers complete the following steps to add a database to the availability group:

1. [Connect](sql-server-linux-kubernetes-connect.md) to a SQL Server instance in the cluster.

1. Create a database.

1. Take a full backup of the database to start the log chain.

1. Add the database to the availability group.

The availability group is created with automatic seeding so SQL Server will automatically create the secondary replicas.

## Next steps

[Connect to SQL Server availability group on Kubernetes cluster](sql-server-linux-kubernetes-connect.md)

[Manage SQL Server availability group on Kubernetes cluster](sql-server-linux-kubernetes-manage.md)

[SQL Server availability group on Kubernetes cluster](sql-server-ag-kubernetes.md)
