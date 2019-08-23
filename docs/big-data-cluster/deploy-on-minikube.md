---
title: Configure minikube
titleSuffix: SQL Server big data clusters
description: Learn how to configure minikube for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] deployments on a single machine.
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/21/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Configure minikube for SQL Server big data cluster deployments

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes how to configure **minikube** on a single machine for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] deployments. Minikube is a tool that makes it easy to run Kubernetes on a single machine like a laptop or a desktop. Minikube runs a single-node Kubernetes cluster inside a VM on your laptop for users looking to try out Kubernetes or develop with it day-to-day. 

## Prerequisites

- 64 GB of memory.

- If the machine has only the minimum recommended memory, then configure the deployment of the cluster to have only 1 compute pool instance, 1 data pool instance, and 1 storage pool instance. This configuration should only be used for evaluation environments where the durability and availability of the data is unimportant. See the [deployment documentation](deployment-guidance.md#configfile) for more information on the environment variables to set to configure the number of replicas for data pools, compute pools, and storage pools.

- VT-x or AMD-v virtualization must be enabled in your computer's BIOS.

## Install dependencies

1. Install [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/).

1. If you do not already have a hypervisor installed, install one now.
   - For OS X, install [xhyve driver](https://git.k8s.io/minikube/docs/drivers.md), [VirtualBox](https://www.virtualbox.org/wiki/Downloads), or [VMware Fusion](https://www.vmware.com/products/fusion).
   - For Linux, install [VirtualBox](https://www.virtualbox.org/wiki/Downloads) or [KVM](https://www.linux-kvm.org/).
   - For Windows, install [VirtualBox](https://www.virtualbox.org/wiki/Downloads) or [Hyper-V](https://msdn.microsoft.com/virtualization/hyperv_on_windows/quick_start/walkthrough_install). If you do not have an external switch configured in Hyper-V, create one that has external network access. Learn how to [create an external switch in Hyper-V for minikube](https://blogs.msdn.microsoft.com/wasimbloch/2017/01/23/setting-up-kubernetes-on-windows10-laptop-with-minikube/).

## Install minikube

Install minikube release according to the instructions for the [v1.3.0 release](https://github.com/kubernetes/minikube/releases/tag/v1.3.0). The [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] only works with version v1.0.0 and up.

## Create a minikube cluster

The command below creates a minikube cluster in a Hyper-V VM with 8 CPUs, 64 GB of memory, and disk size of 100 GB. The disk size is not reserved space.  It grows to that size on disk as needed.  We recommend not changing the disk space to something less than 100 GB as we ran into problems with this in testing. This also specifies the Hyper-V switch with external access explicitly.

Change the parameters such as **--memory** as needed depending on your available hardware and which hypervisor you are using.  Make sure the **--hyper-v** virtual-switch parameter value matches the name you used when creating your virtual switch.

```bash
minikube start --vm-driver="hyperv" --cpus 8 --memory 65536 --disk-size 100g --hyperv-virtual-switch "External"
```

If you are using minikube with VirtualBox the command would look like this:

```base
minikube start --cpus 8 --memory 65536 --disk-size 100g
```

## Disable automatic checkpoint with Hyper-V

On Windows 10, automatic checkpoint is enabled on a VM. Execute the command below in PowerShell to disable automatic checkpoint on the VM and set memory to static.

```PowerShell
Set-VM -Name minikube -CheckpointType Disabled -AutomaticCheckpointsEnabled $false -StaticMemory
```

## Next steps

The steps in this article configured a minikube cluster. The next step is to deploy SQL Server 2019 big data cluster. For instructions, see the following article:

[Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] on Kubernetes](deployment-guidance.md#deploy)
