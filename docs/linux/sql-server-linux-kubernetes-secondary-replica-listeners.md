---
title: Create SQL Server Always On availability group secondary replica listeners for Kubernetes cluster
description: This article explains the parameters for the SQL Server Kubernetes Always On availability group specification
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
# Create service type load balancer to connect to secondary replicas

In a Kubernetes cluster, create a service type of load balancer to connect to secondary replica. [Configure a SQL Server Always On availability group on Docker containers in Kubernetes for high availability](tutorial-sql-server-ag-kubernetes.md) tutorial creates a listener for the primary replica. This article shows how to create a listener for secondary replicas.

## Requirements

To complete the steps in this article you need a Kubernetes cluster with a SQL Server availablity group on it. The steps and scripts in this article work for the availability group created by [the tutorial](tutorial-sql-server-ag-kubernetes.md). 

This article also requires `kubectl` with a connection to the Kubernetes cluster hosting the availability group.

## Create the services

To create services for secondary replica listeners, create a manifest file named `listenerServices.yaml`.

The manifest file describes four Kubernetes services. Each service creates a load balancer. The following table shows the difference between each load balancer:

|Name | Role | Purpose
|:-----|:-----|:-----
|`ag1-primary`| `primary`| Connect to primary replica
|`ag1-secondary-sync`| `secondary-sync`| Connect to secondary synchronous replica
|`ag1-secondary-async`| `secondary-async`| Connect to secondary asynchronous replica
|`ag1-ag1-secondary-config`| `ag1-secondary-config`| Connect to secondary configuration only replica

Copy the manifest below into the file.

```yaml
---
apiVersion: v1
kind: Service
metadata:
  name: ag1-primary
spec:
  ports:
  - name: tds
    port: 1433
  selector:
    type: sqlservr
    role.ag.mssql.microsoft.com/ag1: primary
  type: LoadBalancer

---
apiVersion: v1
kind: Service
metadata:
  name: ag1-secondary-sync
spec:
  ports:
  - name: tds
    port: 1433
  selector:
    type: sqlservr
    role.ag.mssql.microsoft.com/ag1: secondary-sync
  type: LoadBalancer


---
apiVersion: v1
kind: Service
metadata:
  name: ag1-secondary-async
spec:
  ports:
  - name: tds
    port: 1433
  selector:
    type: sqlservr
    role.ag.mssql.microsoft.com/ag1: secondary-async
  type: LoadBalancer


---
apiVersion: v1
kind: Service
metadata:
  name: ag1-secondary-config
spec:
  ports:
  - name: tds
    port: 1433
  selector:
    type: sqlservr
    role.ag.mssql.microsoft.com/ag1: secondary-config
  type: LoadBalancer
  ```

To create the Kubernetes services and for the secondary replicas, run the following command.

```azurecli
kubectl apply -f secondaryListeners.yaml
```

After you run the command, Kubernetes creates a load balancer for each of the replica types.

## Next steps

[SQL Server availability group on Kubernetes cluster](sql-server-ag-kubernetes.md)