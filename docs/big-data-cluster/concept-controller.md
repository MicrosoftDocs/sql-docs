---
title: What is the controller?
titleSuffix: SQL Server big data clusters
description: This article describes the controller of a SQL Server big data cluster.
author: mihaelablendea 
ms.author: mihaelab
ms.reviewer: mikeray
ms.date: 11/04/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# What is the controller on a SQL Server big data cluster?

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

The controller hosts the core logic for deploying and managing a big data cluster. It takes care of all interactions with Kubernetes, SQL Server instances that are part of the cluster and other components like HDFS and Spark.

The controller service provides the following core functionality:

- Manage cluster lifecycle: cluster bootstrap & delete, update configurations
- Manage master SQL Server instances
- Manage compute, data, and storage pools
- Expose monitoring tools to observe the state of the cluster
- Expose troubleshooting tools to detect and repair unexpected issues
- Manage cluster security:
  - Ensure secure cluster endpoints
  - Manage users and roles
  - Configure credentials for intra-cluster communication

## Deploying the controller service

The controller is deployed and hosted in the same Kubernetes namespace where the customer wants to build out a big data cluster. This service is installed by a Kubernetes administrator during cluster bootstrap, using the **azdata** command-line utility. For more information, see [Get started with [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](deploy-get-started.md).

The buildout workflow will layout on top of Kubernetes a fully functional SQL Server big data cluster that includes all the components described in the [Overview](big-data-cluster-overview.md) article. The bootstrap workflow first creates the controller service, and once this is deployed, the controller service will coordinate the installation and configuration of rest of the services part of master, compute, data, and storage pools.

## Managing the cluster through the controller service

You can manage the cluster through the controller service using either **azdata** commands. If you deploy additional Kubernetes objects like pods into the same namespace, they are not managed or monitored by the controller service. You can also use **kubectl** commands to manage the cluster at the Kubernetes level. For more information, see [Monitoring and troubleshoot [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](cluster-troubleshooting-commands.md).

The controller and the Kubernetes objects (stateful sets, pods, secrets, etc.) created for a big data cluster reside in a dedicated Kubernetes namespace. The controller service will be granted permission by the Kubernetes cluster administrator to manage all resources within that namespace.  The RBAC policy for this scenario is configured automatically as part of initial cluster deployment using **azdata**.

### azdata

**azdata** is a command-line utility written in Python that enables cluster administrators to bootstrap and manage big data clusters via the REST APIs exposed by the controller service.

## Controller service security

All communication to the controller service is conducted via a REST API over HTTPS. A self-signed certificate will be automatically generated for you at bootstrap time. 

Authentication to the controller service endpoint is either using an Active Directory identity or based on username and password. These credentials are provisioned at cluster bootstrap time using the input for environment variables `AZDATA_USERNAME` and `AZDATA_PASSWORD`.

> [!NOTE]
> You must provide a password that is in compliance with [SQL Server password complexity requirements](../relational-databases/security/password-policy.md?view=sql-server-2017).

## Next steps

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following resources:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/microsoft/sqlworkshops-bdc)