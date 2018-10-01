---
title: Configure Minikube for SQL Server 2019 CTP 2.0 deployments | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 10/01/2018
ms.topic: conceptual
ms.prod: sql
---

# Configure Minikube for SQL Server 2019 CTP 2.0

Minikube is a tool that makes it easy to run Kubernetes on a single machine like a laptop or a desktop. Minikube runs a single-node Kubernetes cluster inside a VM on your laptop for users looking to try out Kubernetes or develop with it day-to-day. 

## Prerequisites

- To run a Minikube cluster for SQL Server 2019 CTP 2.0 in a SQL Big Data cluster configuration, it is recommended that your machine have at least 32 GB of RAM.

   > [!TIP] 
   > If the machine has insufficient memory, then modify the cluster configuration such that only 3 instances are created: one master instance and two compute instances.

- VT-x or AMD-v virtualization must be enabled in your computer’s BIOS.

## Install dependencies

1. If not already installed, install git locally on [Windows](https://git-for-windows.github.io/), [Linux, or Mac](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git).

1. Install [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/).

1. Install Python 3:
   - If pip is missing then download [get-clspip.py](https://bootstrap.pypa.io/get-pip.py) and run `python get-pip.py`.
   - Install requests package using `python -m pip install requests`.

1. If you do not already have a hypervisor installed, install one now.
   - For OS X, install [xhyve driver](https://git.k8s.io/minikube/docs/drivers.md), [VirtualBox](https://www.virtualbox.org/wiki/Downloads), or [VMware Fusion](https://www.vmware.com/products/fusion).
   - For Linux, install [VirtualBox](https://www.virtualbox.org/wiki/Downloads) or [KVM](http://www.linux-kvm.org/).
   - For Windows, install [VirtualBox](https://www.virtualbox.org/wiki/Downloads) or [Hyper-V](https://msdn.microsoft.com/virtualization/hyperv_on_windows/quick_start/walkthrough_install). If you do not have an external switch configured in hyper-v, then create one that has external network access.  See how to [create external switch in hyper-v for minikube](https://blogs.msdn.microsoft.com/wasimbloch/2017/01/23/setting-up-kubernetes-on-windows10-laptop-with-minikube/).

## Install Minikube

Install Minikube according to the instructions for the [v0.28.2 release](https://github.com/kubernetes/minikube/releases/tag/v0.28.2). The SQL Server 2019 CTP 2.0 Big Data Cluster only works with version v0.24.1 and up.

## Create a Minikube cluster

The command below creates a minikube cluster in a Hyper-V VM with 8 CPUs, 28 GB memory, and disk size of 100GB. The disk size is not reserved space.  It will grow to that size on disk as needed.  We recommend not changing the disk space to something less than 100GB as we ran into problems with this in testing. This also specifies the hyper-v switch with external access explicitly.

Change the parameters such as **--memory** as needed depending on your available hardware and which hypervisor you are using.  Make sure the **--hyper-v** virtual-switch parameter value matches the name you used when creating your virtual switch.

```bash
minikube start --vm-driver="hyperv" --cpus 8 --memory 28672 --disk-size 100g --hyperv-virtual-switch "External"
```

If you are using Minikube with VirtualBox the command would look like this:

```base
minikube start --cpus 8 --memory 28672 --disk-size 100g
```

## Disable automatic checkpoint with Hyper-V

On Windows 10, automatic checkpoint is enabled on a VM. Execute the command below in PowerShell to disable automatic checkpoint on the VM.

```PowerShell
Set-VM -Name minikube -CheckpointType Disabled -AutomaticCheckpointsEnabled $false
```

## Next steps

The steps in this article configured a Minikube cluster. The next step is to deploy SQL Server 2019 CTP 2.0 to the cluster.

[Deploy SQL Server 2019 CTP 2.0 on Kubernetes](quickstart-big-data-cluster-deploy.md)
