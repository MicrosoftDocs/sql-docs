---
title: Connect to SQL Server Always On Availability Group on Kubernetes cluster
description: This article explains how to connect to an Always On Availability Group
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

# Connect to a SQL Server Always On Availability Group on Kubernetes

To connect to SQL Server instances in containers on a Kubernetes cluster, create a [load balancer service](https://kubernetes.io/docs/concepts/services-networking/service/#loadbalancer). The load balancer is an endpoint. It holds an IP address and forwards requests for the IP address to the pod running the SQL Server instance.

To connect to an availability group replica, create a service for different replica types. You can see examples of services for different types of replicas in [sql-server-samples/ag-services.yaml](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-manifest-files).

* `ag1-primary` points to the primary replica.
* `ag1-secondary` points to any secondary replica.

If more than one secondary replica, Kubernetes routes your connection to the different replicas in a round-robin fashion.

## Create a load balancer service

To create load balancer services for the primary and replicas, copy [`ag1-services.yaml`](https://github.com/Microsoft/sql-server-samples/blob/master/samples/features/high%20availability/Kubernetes/sample-manifest-files/ag-services.yaml) from [sql-server-samples](https://github.com/Microsoft/sql-server-samples/blob/master/samples/features/high%20availability/Kubernetes/sample-manifest-file) and update it for your availability group.

The following command applies the configuration from the `.yaml` file to cluster:

```kubectl
kubectl apply -f ag1-services.yaml --namespace ag1
```

## Get the IP address for your load balancer service

To get the load balancer IP address for your load balancer service, run

```kubectl
kubectl get services
```

Identify the IP address of the service you want to connect to.

## Connect to primary replica

To connect to the primary replica with SQL authentication, use the `sa` account, the value for `sapassword` from the secret you created, and this IP address.

For example:

```cmd
sqlcmd -S <0.0.0.0> -U sa -P "<MyC0m9l&xP@ssw0rd>"
```

## Next steps

[Manage SQL Server availability group on Kubernetes cluster](sql-server-linux-kubernetes-manage.md)

[SQL Server availability group on Kubernetes cluster](sql-server-ag-kubernetes.md)
