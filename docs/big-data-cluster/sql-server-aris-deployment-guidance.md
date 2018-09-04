---
title: How to deploy SQL Server Aris on Kubernetes | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 08/27/2018
ms.topic: conceptual
ms.prod: sql
---

# How to deploy SQL Server Aris on Kubernetes

SQL Server vNext can be deployed as docker containers on a Kubernetes cluster. This is an overview of the setup and configuration steps:

- Setup Kubernetes cluster on a single VM, cluster of VMs or in Azure Container Service
- Deploy SQL Server vNext CTP 2.0 in a Kubernetes cluster
- Configure the SQL Server master instance

## Kubernetes prerequisistes

SQL Server vNext requires a minimum 1.10 version for Kubernetes, for both server and client. To install a specific version on kubectl client, see [Install kubectl binary via curl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl).  Latest versions of minikube and ACS are at least 1.10. For ACS you will need to use --orchestrator-version parameter to specify a version different than default.

Also, note that the client/server version skew that is supported is +/-1 minor version. The Kubernetes documentation states that  "a client should be skewed no more than one minor version from the master, but may lead the master by up to one minor version. For example, a v1.3 master should work with v1.1, v1.2, and v1.3 nodes, and should work with v1.2, v1.3, and v1.4 clients." For more information, see [Kubernetes supported releases and component skew](https://github.com/kubernetes/community/blob/master/contributors/design-proposals/release/versioning.md#supported-releases-and-component-skew).

## <a id="kubernetes"></a> Kubernetes cluster setup

If you already have a Kubernetes cluster, then you can skip directly to the [deployment step](#deploy). This section assumes a basic understanding of Kubernetes concepts.  For detailed information on Kubernetes, see the [Kubernetes documentation](https://kubernetes.io/docs/home).

You can choose to deploy Kubernetes in any of three ways:

| Deploy Kubernetes on: | Description |
|---|---|
| **Minikube** | A single-node Kubernetes cluster in a VM. |
| **Azure Container Services (ACS)** | A managed Kubernetes cluster in Azure. |
| **Multiple VMs** | Please refer to **CTP1.8/documentation/k8s-deployment-multiple-vms.docx** document in GitHub for instructions. |

For guidance on configuring one of these Kubernetes cluster options for SQL Server vNext, see one of the followin articles:

   - [Configure Minikube](sql-server-aris-deploy-on-minikube.md)
   - [Configure Kubernetes on Azure Container Service](sql-server-aris-deploy-on-acs.md)
   - [Configure Kubernetes on multiple VMs](sql-server-aris-deploy-on-vms.md)

## <a id="deploy"></a> Deploy SQL Server vNext

After you have configured your Kubernetes cluster, you can proceed with the deployment for SQL Server vNext. The deployment steps are described in the following article:

[Quickstart: Deploy SQL Server Aris on Kubernetes](quickstart-sql-server-aris-deploy.md)

## Next steps

After successfully deploying SQL Server vNext to Kubernetes, [install the big data tools](sql-server-aris-install-big-data-tools.md) and learn more in the [getting started quickstart](quickstart-sql-server-aris-get-started.md).