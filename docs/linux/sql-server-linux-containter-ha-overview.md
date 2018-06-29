---
title: High availability for SQL Server containers
description: This article introduces high availability for SQL Server containers
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
ms.date: 6/10/2018
ms.topic: article
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: linux
---

# High availability for SQL Server containers

SQL Server workloads on containers can provide high availability with container orchestration through [Kubernetes](http://kubernetes.io/).

Two deployment patterns on Kubernetes can provide high availability for SQL Server.

* [A SQL Server Always On availability group hosted on SQL Server containers in Kubernetes](tutorial-sql-server-ag-kubernetes.md).

* [A SQL Server container with peristent storage](tutorial-sql-server-containers-kubernetes.md).

## A SQL Server Always On availability group hosted on SQL Server containers in Kubernetes

SQL Server vNext supports availability groups on containers in a Kubernetes. For availability groups, deploy the SQL Server [Kubernetes operator](http://coreos.com/blog/introducing-operators.html) to your Kubernetes cluster. The operator helps package, deploy, and manage the availability group in a cluster.

![AG in Kubernetes Container](media/tutorial-sql-server-ag-containers-kubernetes/KubernetesCluster.png)

In the image above, a four-node kubernetes clusters host an availability group with three replicas. The solution includes the following components:

* A Kubernetes [*deployment*](http://kubernetes.io/docs/concepts/workloads/controllers/deployment/). The deployment includes the operator and a configuration map. These provide the container image, software, and instructions required to deploy SQL Server instances for the availability group.

* Three nodes, each hosting a [*StatefulSet*](http://kubernetes.io/docs/concepts/workloads/controllers/statefulset/). The StatefulSet contains a [*pod*](http://kubernetes.io/docs/concepts/workloads/pods/pod-overview/). Each pod contains:
  * A SQL Server container running one instance of SQL Server.
  * An availability group agent. 

* Two [*ConfigMaps*](http://kubernetes.io/docs/tasks/configure-pod-container/configure-pod-configmap/) related to the availability group. The ConfigMaps provide information about:
  * The deployment for the operator.
  * The availability group.

 * [*Persistent volumes*](http://kubernetes.io/docs/concepts/storage/persistent-volumes/) are pieces of storage. A *persistent volume claim* (PVC) is a request for storage by a user. Each container is affiliated with a PVC for the data and log storage. In Azure Kubernetes Service (AKS), you [create a persistent volume claim](http://docs.microsoft.com/azure/aks/azure-disks-dynamic-pv) to automatically provision storage based on a storage class.


In addition, the cluster stores [*secrets*](http://kubernetes.io/docs/concepts/configuration/secret/) for the passwords, certificates, keys, and other sensitive information.

## SQL Server container with persistent storage

Kubernetes 1.6 and later has support for [storage classes](http://kubernetes.io/docs/concepts/storage/storage-classes/), [persistent volume claims](http://kubernetes.io/docs/concepts/storage/storage-classes/#persistentvolumeclaims), and the [Azure disk volume type](https://github.com/kubernetes/examples/tree/master/staging/volumes/azure_disk). You can create and manage your SQL Server instances natively in Kubernetes. The example in this article shows how to create a [deployment](https://kubernetes.io/docs/concepts/workloads/controllers/deployment/) to achieve a high availability configuration similar to a shared disk failover cluster instance. In this configuration, Kubernetes plays the role of the cluster orchestrator. When a SQL Server instance in a container fails, the orchestrator bootstraps another instance of the container that attaches to the same persistent storage.

![Diagram of Kubernetes SQL Server cluster](media/tutorial-sql-server-containers-kubernetes/kubernetes-sql.png)

In the preceding diagram, `mssql-server` is a container in a [pod](http://kubernetes.io/docs/concepts/workloads/pods/pod/). Kubernetes orchestrates the resources in the cluster. A [replica set](http://kubernetes.io/docs/concepts/workloads/controllers/replicaset/) ensures that the pod is automatically recovered after a node failure. Applications connect to the service. In this case, the service represents a load balancer that hosts an IP address that stays the same after failure of the `mssql-server`.

In the following diagram, the `mssql-server` container has failed. As the orchestrator, Kubernetes guarantees the correct count of healthy instances in the replica set, and starts a new container according to the configuration. The orchestrator starts a new pod on the same node, and `mssql-server` reconnects to the same persistent storage. The service connects to the re-created `mssql-server`.

![Diagram of Kubernetes SQL Server cluster](media/tutorial-sql-server-containers-kubernetes/kubernetes-sql-after-pod-fail.png)

In the following diagram, the node hosting the `mssql-server` container has failed. The orchestrator starts the new pod on a different node, and `mssql-server` reconnects to the same persistent storage. The service connects to the re-created `mssql-server`.

![Diagram of Kubernetes SQL Server cluster](media/tutorial-sql-server-containers-kubernetes/kubernetes-sql-after-node-fail.png)

## Next steps

[Configure a SQL Server container in Kubernetes for high availability](tutorial-sql-server-containers-kubernetes.md)

[Configure a SQL Server Always On availability group on Docker containers in Kubernetes with Azure Kubernetes Service (AKS)](tutorial-sql-server-ag-kubernetes.md)