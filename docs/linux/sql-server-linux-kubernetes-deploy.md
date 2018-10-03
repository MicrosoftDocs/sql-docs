---
title: Deploy SQL Server Always On Availability Group on Kubernetes Cluster
description: This article explains the parameters for the SQL Server Kubernetes Always On availability group operator global requirements
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
ms.date: 10/02/2018
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
- Access to the [sql-server-samples](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-manifest-files) github repository

  >[!NOTE]
  >You can use any type of Kubernetes cluster. To create a Kubernetes cluster on Azure Kubernetes Service (AKS), see [Create an AKS cluster](http://docs.microsoft.com/azure/aks/create-cluster).
  > The following script creates a four node Kubernetes cluster in Azure.
  >```azure-cli
  az aks create --resource-group myResourceGroup --name myAKSCluster --node-count 4 --kubernetes-version 1.11.1
  >```

## Steps

1. Create a [namespace](https://kubernetes.io/docs/concepts/overview/working-with-objects/namespaces/).

  This example uses a namespace called `ag1`. Run the following command to create the namespace.

  ```azurecli
  kubectl create namespace ag1
  ```

1. Create Kubernetes a secret for the SA password.

  Create the secret with  `kubectl`. The following script creates a secret named `sql-secrets` in the `ag1` namespace. The secret stores a password named `sapassword`.

  Copy the script to your terminal. Replace `<>` with a complex password, and run the script to create the secret with the complex password.

  ```azurecli
  kubectl create secret generic sql-secrets --from-literal=sapassword="<>" --namespace ag1
  ```

1. Configure and deploy the SQL Server operator manifest.

  Copy the SQL Server [`operator.yaml`](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-manifest-files/operator.yaml) file from [sql-server-samples](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-manifest-files).

  The `operator.yaml` file is the deployment manifest for the Kubernetes operator.

  Apply the manifest to the Kubernetes cluster.

  ```azurecli
  kubectl apply -f operator.yaml
  ```

1. Deploy the SQL Server custom resource.

  Copy the SQL Server manifest [`sqlserver.yaml`](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-manifest-files/sqlserver.yaml) from [sql-server-samples](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-manifest-files).

  >[!NOTE]
  >The `sqlserver.yaml` file describes the SQL Server containers and the persistent volume claims that are required for the storage for each SQL Server instance. 

  Apply the manifest to the Kubernetes cluster.

  ```azurecli
  kubectl apply -f sqlserver.yaml
  ```

  After you deploy the SQL Server manifest, the operator deploys the instances of SQL Server as pods in containers.

### Monitor the deployment

You can use [Kubernetes dashboard with Azure Kubernetes Service (AKS)](https://docs.microsoft.com/en-us/azure/aks/kubernetes-dashboard) to monitor the deployment.

Use `az aks browse` to launch the dashboard. 

## Create load balancer services to allow connection to replicas

The [`ag-services.yaml`](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-manifest-files/ag-services.yaml) from [sql-server-samples](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-manifest-files) example describes load-balancing services that can connect to availability group replicas. 

- `ag1-primary` connects to the primary replica.
- `ag1-secondary-sync` connects to a secondary replica in synchronous commit mode.
- `ag1-secondary-async` connects to a secondary replica in asynchronous commit mode.
- `ag1-secondary-config` connects to a configuration only replica.

When you apply the manifest file in the example, Kubernetes creates the load-balancing services for each type of replica. The load-balancing service includes an IP address. Use this IP address to connect to the type of replica you need.

>[!NOTE]
>If the specific replica type does not exist, connection attempts to that service fail. For example, if you create the load balancer for `ag1-secondary-async`, but do not have a secondary replica in asynchronous commit mode, the connection does not succeed.

To deploy the services, run the following command.

```azurecli
kubectl apply -f ag-services.yaml
```

Kubernetes creates a load balancer service for each replica type. The load balancer service stores an IP address for its replica type, as described in the manifest.

After you deploy the services, use `kubectl get services --namespace ag1` to identify the IP address for the services.

With the IP address, you can connect to the SQL Server instance that hosts each type of replica.

The following image shows:

1. The output from `kubectl get services` for the namespace `ag1`.
1. The connection to the replica, with `sqlcmd`, and the `sa` account.

![connect](./media/sql-server-linux-kubernetes-deploy/connect.png)

### Add a database to the availability group

After Kubernetes creates the SQL Server containers, complete the following steps to add a database to the availability group:

1. [Connect](sql-server-linux-kubernetes-connect.md) to a SQL Server instance in the cluster.

1. Create a database.

1. Take a full backup of the database to start the log chain.

1. Add the database to the availability group.

The availability group is created with automatic seeding so SQL Server will automatically create the secondary replicas.

## Next steps

[Connect to SQL Server availability group on Kubernetes cluster](sql-server-linux-kubernetes-connect.md)

[Manage SQL Server availability group on Kubernetes cluster](sql-server-linux-kubernetes-manage.md)

[SQL Server availability group on Kubernetes cluster](sql-server-ag-kubernetes.md)
