---
title: Deploy with a bash script to a single node kubeadm cluster
titleSuffix: SQL Server big data clusters
description: Use a bash deployment script to deploy a SQL Server 2019 big data cluster (preview) to a single node kubeadm cluster.
author: mihaelablendea 
ms.author: mihaelab
ms.reviewer: mikeray
ms.date: 07/24/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Deploy with a bash script to a single node kubeadm cluster

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

In this tutorial, you use a sample bash deployment script to deploy a single node Kubernetes cluster using  kubeadm and a SQL Server big data cluster on top of it.  

## Prerequisites

1. A vanilla Ubuntu 18.04 or 16.04 **server** VM. All dependencies are going to be setup by the script. You will run the script from within the VM.

**NOTE: Using Azure VMs is not yet supported.**

1. VM should have at least 8CPUs, 64GB RAM and 100GB disk space. After pulling all big data cluster Docker images you will be left with 50GB for data/logs to use across all components.

## Instructions

1. Download the script on the VM you are planning to use for the deployment
```
curl --output setup-bdc.sh https://github.com/microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/deployment/kubeadm/ubuntu-single-node-vm/setup-bdc.sh
```

2. Make the script executable
```
chmod +x setup-bdc.sh
```

3. Run the script (make sure you are running with sudo)
```
sudo ./setup-bdc.sh
```

When prompted, provide your input for the password that will be used for all external endpoints: controller, SQL Server master and gateway. Note that controller username is hardcoded to *admin*.

4. Setup an alias for azdata tool
```
source ~/.bashrc
```

5. Validate alias works
```
azdata --version
```

## Next steps

Follow [this tutorial](tutorial-load-sample-data.md) to get you started on using big data cluster.
