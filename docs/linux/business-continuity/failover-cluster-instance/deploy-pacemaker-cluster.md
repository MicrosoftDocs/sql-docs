---
title: Deploy a Pacemaker Cluster
titleSuffix: SQL Server on Linux
description: Learn to deploy a Linux Pacemaker cluster for a SQL Server Always On availability group (AG) or failover cluster instance (FCI).
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 09/14/2026
ms.service: sql
ms.subservice: linux
ms.topic: install-set-up-deploy
ms.custom:
  - intro-deployment
  - linux-related-content
  - sfi-image-nochange
ai-usage: ai-assisted
---
# Deploy a Pacemaker cluster for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../../../includes/applies-to-version/sql-linux.md)]

This tutorial describes the tasks required to deploy a Linux Pacemaker cluster for a [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Always On availability group (AG) or failover cluster instance (FCI). Unlike the tightly coupled Windows Server / [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] stack, you can create a Pacemaker cluster and configure an availability group (AG) on Linux before or after installing [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]. You configure the integration and resources for the Pacemaker portion of an AG or FCI deployment after the cluster is configured.

> [!IMPORTANT]  
> An AG with a cluster type of None doesn't require a Pacemaker cluster and can't be managed by Pacemaker.

> [!div class="checklist"]
> - Install the high availability add-on and install Pacemaker.
> - Prepare the nodes for Pacemaker (RHEL and Ubuntu only).
> - Create the Pacemaker cluster.
> - Install the SQL Server HA and SQL Server Agent packages.

> [!NOTE]  
> Starting in [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)], SUSE Linux Enterprise Server (SLES) isn't supported.

## Prerequisites

[Install SQL Server on Linux](../../install-upgrade/setup.md).

## Install the high availability add-on

Use the following syntax to install the packages that make up the high availability (HA) add-on for each distribution of Linux.

### [Red Hat Enterprise Linux (RHEL)](#tab/rhel)

[!INCLUDE [ss-linux-cluster-pacemaker-rhel-ha-subscription](../../includes/cluster-pacemaker-rhel-ha-subscription.md)]

1. Install Pacemaker.

   ```bash
   sudo yum install pacemaker pcs fence-agents-all resource-agents
   ```

### [SUSE Linux Enterprise Server (SLES)](#tab/sles)

Install the High Availability pattern in YaST, or install it as part of the main installation of the server. You can use an ISO or DVD as a source, or get it online.

> [!NOTE]  
> On SLES, the HA add-on is initialized when you create the cluster.

### [Ubuntu](#tab/ubuntu)

```bash
sudo apt-get install pacemaker pcs fence-agents resource-agents-base resource-agents-common resource-agents-extra
```

---

## Prepare the nodes for Pacemaker (RHEL and Ubuntu only)

Pacemaker uses a user named `hacluster` that you create on the distribution. On RHEL and Ubuntu, the HA add-on installation creates this user.

1. On each server that will serve as a node in the Pacemaker cluster, create the password for a user that the cluster uses. The examples use the name `hacluster`, but you can use any name. All nodes in the Pacemaker cluster must use the same name and password.

   ```bash
   sudo passwd hacluster
   ```

1. On each node that will be part of the Pacemaker cluster, enable and start the `pcsd` service with the following commands (RHEL and Ubuntu).

   ```bash
   sudo systemctl enable pcsd
   sudo systemctl start pcsd
   ```

   Then, run the following command to make sure that `pcsd` starts.

   ```bash
   sudo systemctl status pcsd
   ```

1. Enable the Pacemaker service on each possible node in the Pacemaker cluster.

   ```bash
   sudo systemctl start pacemaker
   ```

   On Ubuntu, you see the following error.

   ```output
   pacemaker Default-Start contains no runlevels, aborting.
   ```

   This error is a known issue. Despite the error, enabling the Pacemaker service is successful. This bug will be fixed in a future update.

1. Next, create and start the Pacemaker cluster. There's one difference between RHEL and Ubuntu at this step. While on both distributions, installing `pcs` configures a default configuration file for the Pacemaker cluster, on RHEL, running this command removes any existing configuration and creates a new cluster.

<a id="create"></a>

## Create the Pacemaker cluster

This section describes how to create and configure the cluster for each Linux distribution.

### [Red Hat Enterprise Linux (RHEL)](#tab/rhel)

1. Authorize the nodes. In these examples, `<NodeX>` is the name of each node. Manually enter the username and password for `hacluster` when prompted.

   ```bash
   sudo pcs host auth <Node1> <Node2> <Node3>
   ```

1. Create the cluster. In this example, `PMClusterName` is the name you assign to the Pacemaker cluster.

   ```bash
   sudo pcs cluster setup <PMClusterName> <Node1> <Node2> <Node3>
   ```

1. Start the cluster on all nodes.

   ```bash
   sudo pcs cluster start --all
   ```

1. Enable the cluster to start when the computer starts.

   ```bash
   sudo pcs cluster enable --all
   ```

1. Verify the cluster status.

   ```bash
   sudo pcs status
   ```

### [SUSE Linux Enterprise Server (SLES)](#tab/sles)

The process for creating a Pacemaker cluster is different on SLES than it is on RHEL and Ubuntu. The following steps show how to create a cluster with SLES.

1. Start the cluster configuration process by running the following command on one of the nodes:

   ```bash
   sudo ha-cluster-init
   ```

   You might be prompted that NTP isn't configured and that no watchdog device is found. This condition is fine for getting the cluster up and running. Watchdog is related to fencing a failed node. If you use SLES's built-in fencing, that fencing is storage-based. You can configure NTP and watchdog later.

1. You're prompted to configure Corosync. You're prompted for the network address to bind to, as well as the multicast address and port. The network address is the subnet that you're using (for example, `192.191.190.0`). You can accept the defaults at every prompt, or change if necessary.

1. Next, you're prompted to configure SBD, which is the disk-based fencing. You can choose to configure this setting later. If you don't configure SBD, unlike on RHEL and Ubuntu, the process sets `stonith-enabled` to `false` by default.

1. Finally, you're prompted to configure an IP address for administration. This IP address is optional, but functions similar to the IP address for a Windows Server failover cluster (WSFC). It creates an IP address in the cluster to be used for connecting to it via HA Web Konsole (HAWK). This configuration is also optional.

1. Ensure that the cluster is up and running.

   ```bash
   sudo crm status
   ```

1. Change the `hacluster` password.

   ```bash
   sudo passwd hacluster
   ```

1. If you configured an IP address for administration, you can test it in a browser. This test also checks the password change for `hacluster`.

   :::image type="content" source="media/deploy-pacemaker-cluster/pacemaker-web-interface-login.png" alt-text="Screenshot of the Pacemaker web UI login page with the hacluster username entered.":::

1. On another SLES server that will act as a node of the cluster, run the following command:

   ```bash
   sudo ha-cluster-join
   ```

1. When prompted, enter the name or IP address of the server that you configured as the first node of the cluster in the previous steps. The server is added as a node to the existing cluster.

1. Verify the node was added.

   ```bash
   sudo crm status
   ```

1. Change the `hacluster` password.

   ```bash
   sudo passwd hacluster
   ```

1. Repeat Steps 8-11 for all other servers to add to the cluster.

### [Ubuntu](#tab/ubuntu)

Configuring Ubuntu is similar to RHEL. However, there's one major difference: installing the Pacemaker packages creates a base configuration for the cluster, and enables and starts `pcsd`. If you try to configure the Pacemaker cluster by following the RHEL instructions exactly, you get an error. To fix this problem, perform the following steps.

1. Remove the default Pacemaker configuration from each node.

   ```bash
   sudo pcs cluster destroy
   ```

1. Authorize the nodes. In this example, `NodeX` is the name of the node.

   ```bash
   sudo pcs host auth <Node1 Node2 ... NodeN> -u hacluster
   ```

1. Create the cluster. In this example, `PMClusterName` is the name you assign to the Pacemaker cluster, and `Nodelist` is the list of node names separated by a space.

   ```bash
   sudo pcs cluster setup <PMClusterName Nodelist>
   ```

1. Enable the cluster to start when the computer starts.

   ```bash
   sudo pcs cluster enable --all
   ```

1. Verify the cluster status.

   ```bash
   sudo pcs status
   ```

---

## Install the SQL Server HA

Use the following commands to install the SQL Server HA package and [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Agent, if they aren't installed already. If you install the HA package after installing [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], you must restart [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] for the change to take effect. These instructions assume that the repositories for the Microsoft packages are already set up, since [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] should be installed at this point.

- If you don't use [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Agent for log shipping or any other use, you don't need to start or configure it.

- The other optional packages for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on Linux, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Full-Text Search (**mssql-server-fts**) and [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Integration Services (**mssql-server-is**), aren't required for high availability, either for an FCI or an AG.

### [Red Hat Enterprise Linux (RHEL)](#tab/rhel)

```bash
sudo yum install mssql-server-ha
sudo systemctl restart mssql-server
```

### [SUSE Linux Enterprise Server (SLES)](#tab/sles)

```bash
sudo zypper install mssql-server-ha
sudo systemctl restart mssql-server
```

### [Ubuntu](#tab/ubuntu)

```bash
sudo apt-get install mssql-server-ha
sudo systemctl restart mssql-server
```

---

## Next step

In this tutorial, you learned how to deploy a Pacemaker cluster for SQL Server on Linux. You learned how to:

> [!div class="checklist"]
> - Install the high availability add-on and install Pacemaker.
> - Prepare the nodes for Pacemaker (RHEL and Ubuntu only).
> - Create the Pacemaker cluster.
> - Install the SQL Server HA and SQL Server Agent packages.

To create and configure an availability group for SQL Server on Linux, see:

> [!div class="nextstepaction"]
> [Create and configure an availability group for SQL Server on Linux](../availability-groups/create.md).
