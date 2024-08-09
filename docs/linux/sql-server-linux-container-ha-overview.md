---
title: High availability for SQL Server containers
description: Learn about high availability for SQL Server containers. Also learn about deploying a container with SQL Server on Kubernetes.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 07/15/2024
ms.service: sql
ms.subservice: linux
ms.topic: article
ms.custom:
  - linux-related-content
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# High availability for SQL Server containers

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

Create and manage your SQL Server instances natively in Kubernetes.

Deploy SQL Server to docker containers managed by [Kubernetes](https://kubernetes.io/). In Kubernetes, a container with a SQL Server instance can automatically recover in case a cluster node fails.

SQL Server 2017 introduces a Docker image that can deploy on Kubernetes. You can configure the image with a Kubernetes persistent volume claim (PVC). Kubernetes monitors the SQL Server process in the container. If the process, pod, container, or node fail, Kubernetes automatically bootstraps another instance and reconnects to the storage.

## Container with SQL Server instance on Kubernetes

Kubernetes 1.6 and later has support for [*storage classes*](https://kubernetes.io/docs/concepts/storage/storage-classes/), [*persistent volume claims*](https://kubernetes.io/docs/concepts/storage/storage-classes/#persistentvolumeclaims), and the [*Azure disk volume type*](https://github.com/kubernetes/examples/tree/master/staging/volumes/azure_disk).

In this configuration, Kubernetes plays the role of the container orchestrator.

:::image type="content" source="media/sql-server-linux-container-ha-overview/kubernetes-sql.png" alt-text="Diagram showing a Kubernetes SQL Server cluster.":::

In the preceding diagram, `mssql-server` is a SQL Server instance (container) in a [*pod*](https://kubernetes.io/docs/concepts/workloads/pods/pod/). A [replica set](https://kubernetes.io/docs/concepts/workloads/controllers/replicaset/) ensures that the pod is automatically recovered after a node failure. Applications connect to the service. In this case, the service represents a load balancer that hosts an IP address that stays the same after failure of the `mssql-server`.

Kubernetes orchestrates the resources in the cluster. When a node hosting a SQL Server instance container fails, it bootstraps a new container with a SQL Server instance and attaches it to the same persistent storage.

SQL Server on Linux supports containers on Kubernetes, OpenShift, and D2Hi.

## Related content

- [Deploy and connect to SQL Server Linux containers](sql-server-linux-docker-container-deployment.md)
- [Quickstart: Deploy a SQL Server container cluster on Azure](quickstart-sql-server-containers-azure.md)
- [Tutorial: Set up a three node Always On availability group with DH2i DxEnterprise](/azure/azure-sql/virtual-machines/linux/dh2i-high-availability-tutorial)
