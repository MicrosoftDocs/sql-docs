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

In this tutorial, you use a sample bash deployment script to deploy a single node Kubernetes cluster using  kubeadm and a SQL Server big data cluster on it.  

## Prerequisites

- A vanilla Ubuntu 18.04 or 16.04 **server** VM. All dependencies are set up by the script, and you run the script from within the VM.

  > [!NOTE]
  > Using Azure VMs is not yet supported.

- VM should have at least 8 CPUs, 64-GB RAM and 100 GB of disk space. After pulling all big data cluster Docker images, you will be left with 50 GB for data and logs to use across all components.

## Instructions

1. Download the script on the VM you are planning to use for the deployment.

   ```bash
   curl --output setup-bdc.sh https://raw.githubusercontent.com/microsoft/sql-server-samples/master/samples/features/sql-big-data-cluster/deployment/kubeadm/ubuntu-single-node-vm/setup-bdc.sh
   ```

2. Make the script executable with the following command.

   ```bash
   chmod +x setup-bdc.sh
   ```

3. Run the script with **sudo**.

   ```bash
   sudo ./setup-bdc.sh
   ```

   When prompted, provide your input for the password to use for the following external endpoints: controller, SQL Server master, and gateway. The controller username defaults to *admin*.

4. Set up an alias for the **azdata** tool.

   ```bash
   source ~/.bashrc
   ```

5. Validate that the alias works.

   ```bash
   azdata --version
   ```

## Next steps

Follow [this tutorial](tutorial-load-sample-data.md) to get started with using big data cluster.
