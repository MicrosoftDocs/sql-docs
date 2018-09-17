---
title: What is the SQL Server Big Data Clusters controller? | Microsoft Docs
description:
author: mihaelablendea 
ms.author: mihaelab 
manager: craigg
ms.date: 09/24/2018
ms.topic: conceptual
ms.prod: sql
---

# What is the SQL Server Big Data Clusters controller?

The Controller hosts the core logic for building and managing the cluster. It takes care of all interactions with Kubernetes, SQL Servers instances that are part of the cluster as well as Hadoop components like HDFS. 

The Controller service provides the following core functionality:

- Manage cluster lifecycle: cluster bootstrap & delete, update configurations and upgrade (upgrade not available in CTP 2.0)
- Manage master SQL Server instances
- Manage compute, data and storage pools
- Expose monitoring tools to observe the state of the cluster
- Expose troubleshooting tools to detect and repair unexpected issues
- Manage cluster security: ensure secure cluster endpoints, manage users and roles, configure credentials for intra-cluster communication
- Manage the workflow of upgrades so that they are implemented safely (not available in CTP 2.0)
- Manage high availability and DR for statefull services in the cluster (not in CTP 2.0)

## Deploying the Controller service

The Controller is hosted in the same Kubernetes environment, in the same namespace where the customer wants to build out the cluster. This service is installed by a Kubernetes administrator during cluster bootstrap, using the mssqlctl command-line utility:

```bash
python mssqlctl.py create cluster <name of your cluster>
```

The buildout workflow will layout on top of Kubernetes a fully functional SQL Server Big Data Cluster that includes all the components described in the [Overview](big-data-cluster-overview.md) article. The bootstrap workflow creates first the Controller service, and once this is deployed, the Controller will coordinate the installation and configuration of rest of the services part of Master, Compute, Data and Storage pools.

## Managing the cluster through the Controller

Customers are expected to manage the cluster purely through the Controller using either `mssqlctl` APIs or the [administration portal](manage-troubleshooting.md) that is hosted within the cluster. If customers deploy additional Kubernetes objects (pods) into the same namespace, they are not managed or monitored by the Controller.

The Controller and the Kubernetes objects (stateful sets, pods, secrets, etc.) created for the cluster reside in a dedicated Kubernetes namespace. The Controller service will be granted permission by the Kubernetes cluster administrator to manage all resources within that namespace.  The RBAC policy this scenario is configured automatically as part of initial cluster deployment. 

### mssqlctl

`mssqlctl` is a command-line utility written in Python that enables cluster administrators to bootstrap and manage the Big Data Cluster via REST APIs.
You can find details on how to install and use `mssqlctl` in the [Deployment guidance](deployment-guidance.md) and [Install Big Data Tools](deploy-big-data-tools.md) topics.

### Cluster Admin Portal

Once the Controller service is up and running, cluster administrator can use the [Cluster Admin Portal](manage-monitoring.md) to monitor the deployment progress, detect, and troubleshoot issues with services within the cluster. 

## Monitoring and troubleshooting Controller service

Controller 

## Controller service security
All communication to the Controller is conducted via a REST API over HTTPS. A self-signed certificate will be automatically generated for at bootstrap time. 

Authentication to Controller endpoint is based on username and password. These credentials are provisioned at cluster bootstrap using the input for environment variables `CONTROLLER_USERNAME` and `CONTROLLER_PASSWORD`. For example, from a Linux client machine, you can run below script to set the environment variables:

```bash
export CONTROLLER_USERNAME=<your controller user name>
export CONTROLLER_PASSWORD=<your controller password>
```

> [!NOTE]
> You must provide a password that is in compliance with [SQL Server password complexity requirements](https://docs.microsoft.com/en-us/sql/relational-databases/security/password-policy?view=sql-server-2017).

## Next steps

To learn more about the SQL Server Big Data Clusters, see the following articles:

- [What is SQL Server 2019 Big Data Clusters?](big-data-cluster-overview.md)
- [Quickstart: Deploy SQL Server Big Data Cluster on Kubernetes](quickstart-big-data-cluster-deploy.md)