---
title: Deploy on Charmed Kubernetes
titleSuffix: SQL Server Big Data Clusters
description: How to deploy Microsoft Big Data Clusters on Charmed Kubernetes
author: evilnick
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Charmed Kubernetes

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article explains how to deploy a SQL Server Big Data Cluster (BDC) on [Charmed Kubernetes][]. 

> [!TIP]
> text text text.


text text text text text text xxxxx xxxx xx x xx x xxxx.

## Pre-requisites

-   **[Juju][]**: Juju provides full automation of software deployments and operations, significantly simplifying the entire process.
     
-   SSH keypair: Juju expects an SSH keypair for secure communication between the client and any units deployed. Almost all Linux distributions automatically generate SSH keys when installed. Windows 10 and Windows Server both now include the [OpenSSH][] software used to manage and use SSH keys, but you will need to manually generate the key by running the command `ssh-keygen` (more details [here][ssh-keygen]).
-   Cloud Credentials: Juju can operate and manage applications on a variety of hardware as well as major public clouds including Azure, AWS, Google, OpenStack, VMWare. For the purposes of this document we will assume an Azure public cloud.
  
## Prepare Juju
## Deploy Charmed Kubernetes

<!--LINKS-->
[Charmed Kubernetes]: https://ubuntu.com/kubernetes/docs/overview
[Juju]: https://juju.is
[ssh-keygen]: https://docs.microsoft.com/en-us/windows-server/administration/openssh/openssh_overview
[OpenSSH]: https://www.openssh.com/