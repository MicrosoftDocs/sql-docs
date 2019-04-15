---
title: What is the controller?
titleSuffix: SQL Server big data clusters
description: This article describes the controller of a SQL Server 2019 big data cluster (preview).
author: mihaelablendea 
ms.author: mihaelab
ms.reviewer: jroth
manager: craigg
ms.date: 03/27/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# What is the controller on a SQL Server big data cluster?

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The controller hosts the core logic for deploying and managing a big data cluster. It takes care of all interactions with Kubernetes, SQL Server instances that are part of the cluster and other components like HDFS and Spark. 

The controller service provides the following core functionality:

- Manage cluster lifecycle: cluster bootstrap & delete, update configurations
- Manage master SQL Server instances
- Manage compute, data, and storage pools
- Expose monitoring tools to observe the state of the cluster
- Expose troubleshooting tools to detect and repair unexpected issues
- Manage cluster security: ensure secure cluster endpoints, manage users and roles, configure credentials for intra-cluster communication
- Manage the workflow of upgrades so that they are implemented safely (not available in CTP 2.4)
- Manage high availability and DR for statefull services in the cluster (not available in CTP 2.4)

## Deploying the controller service

The controller is deployed and hosted in the same Kubernetes namespace where the customer wants to build out a big data cluster. This service is installed by a Kubernetes administrator during cluster bootstrap, using the mssqlctl command-line utility:

```bash
mssqlctl cluster create --name <name of your cluster>
```

The buildout workflow will layout on top of Kubernetes a fully functional SQL Server big data cluster that includes all the components described in the [Overview](big-data-cluster-overview.md) article. The bootstrap workflow first creates the controller service, and once this is deployed, the controller service will coordinate the installation and configuration of rest of the services part of master, compute, data, and storage pools.

## Managing the cluster through the controller service

You can manage the cluster purely through the controller service using either `mssqlctl` APIs or the cluster administration portal that is hosted within the cluster. If you deploy additional Kubernetes objects like pods into the same namespace, they are not managed or monitored by the controller service.

The controller and the Kubernetes objects (stateful sets, pods, secrets, etc.) created for a big data cluster reside in a dedicated Kubernetes namespace. The controller service will be granted permission by the Kubernetes cluster administrator to manage all resources within that namespace.  The RBAC policy for this scenario is configured automatically as part of initial cluster deployment using `mssqlctl`. 

### mssqlctl

`mssqlctl` is a command-line utility written in Python that enables cluster administrators to bootstrap and manage big data clusters via the REST APIs exposed by the controller service.

### Cluster Administration Portal

Once the controller service is up and running, cluster administrator can use the [Cluster Administration Portal](cluster-admin-portal.md) to monitor the deployment progress, detect, and troubleshoot issues with services within the cluster.

## Controller service security

All communication to the controller service is conducted via a REST API over HTTPS. A self-signed certificate will be automatically generated for you at bootstrap time. 

Authentication to the controller service endpoint is based on username and password. These credentials are provisioned at cluster bootstrap time using the input for environment variables `CONTROLLER_USERNAME` and `CONTROLLER_PASSWORD`.

> [!NOTE]
> You must provide a password that is in compliance with [SQL Server password complexity requirements](https://docs.microsoft.com/sql/relational-databases/security/password-policy?view=sql-server-2017).

## Next steps

To learn more about the SQL Server big data clusters, see the following resources:

- [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md)
- [Workshop: Microsoft SQL Server big data clusters Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
