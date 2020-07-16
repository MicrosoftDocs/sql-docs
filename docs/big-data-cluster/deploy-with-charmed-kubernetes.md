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

-   **[Juju][]**: Juju is a tool which provides full automation of software deployments and operations. **Charmed Kubernetes** uses Juju to deploy, configure and operate the applications which combine to make a running Kubernetes cluster. Juju is available for Ubuntu, Windows 10, macOS and various Linux variants.
    -   Ubuntu 18.04+: Run the command: `sudo snap install juju --classic`
    -   Windows 10: Download and run the installer [here][juju-win].
    -   For macOS and non-Ubuntu Linux systems, see the [install documentation][juju-install-docs]      
     
-   SSH keypair: Juju expects an SSH keypair for secure communication between the client and any units deployed. Almost all Linux distributions automatically generate SSH keys when installed. Windows 10 and Windows Server both now include the [OpenSSH][] software used to manage and use SSH keys, but you will need to manually generate the key by running the command `ssh-keygen` (more details [here][ssh-keygen]).
-   Cloud Credentials: Juju can operate and manage applications on a variety of hardware as well as major public clouds including Azure, AWS, Google, OpenStack, VMWare. To access these clouds it will be necessary to supply Juju with the appropriate credentials. This can be as simple as running the command:
`juju add-credential azure` and following the interactive prompt. For the purposes of this document we will assume an Azure public cloud, noting where there may be differences. For more details on credentials and using Juju with different clouds, see the [Juju documentation][juju-docs].
  

## Deploy Charmed Kubernetes


<!--LINKS-->
[Charmed Kubernetes]: https://ubuntu.com/kubernetes/docs/overview
[Juju]: https://juju.is
[ssh-keygen]: https://docs.microsoft.com/en-us/windows-server/administration/openssh/openssh_overview
[OpenSSH]: https://www.openssh.com/