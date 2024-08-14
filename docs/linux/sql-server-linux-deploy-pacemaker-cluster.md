---
title: Deploy a Pacemaker cluster for SQL Server on Linux
description: Learn to deploy a Linux Pacemaker cluster for a SQL Server Always On availability group (AG) or failover cluster instance (FCI).
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 07/15/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - intro-deployment
  - linux-related-content
---
# Deploy a Pacemaker cluster for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This tutorial documents the tasks required to deploy a Linux Pacemaker cluster for a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Always On availability group (AG) or failover cluster instance (FCI). Unlike the tightly coupled Windows Server / [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] stack, Pacemaker cluster creation and availability group (AG) configuration on Linux, can be done before or after installation of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. The integration and configuration of resources for the Pacemaker portion of an AG or FCI deployment is done after the cluster is configured.
> [!IMPORTANT]  
> An AG with a cluster type of None does *not* require a Pacemaker cluster, nor can it be managed by Pacemaker.

> [!div class="checklist"]
> - Install the high availability add-on and install Pacemaker.
> - Prepare the nodes for Pacemaker (RHEL and Ubuntu only).
> - Create the Pacemaker cluster.
> - Install the SQL Server HA and SQL Server Agent packages.

## Prerequisites

[Install SQL Server on Linux](sql-server-linux-setup.md).

## Install the high availability add-on

Use the following syntax to install the packages that make up the high availability (HA) add-on for each distribution of Linux.

### [Red Hat Enterprise Linux (RHEL)](#tab/rhel)

1. Register the server using the following syntax. You're prompted for a valid username and password.

    ```bash
    sudo subscription-manager register
    ```

1. List the available pools for registration.

    ```bash
    sudo subscription-manager list --available
    ```

1. Run the following command to associate RHEL high availability with the subscription

    ```bash
    sudo subscription-manager attach --pool=<PoolID>
    ```

    where *PoolId* is the pool ID for the high availability subscription from the previous step.

1. Enable the repository to be able to use the high availability add-on.

    ```bash
    sudo subscription-manager repos --enable=rhel-ha-for-rhel-7-server-rpms
    ```

1. Install Pacemaker.

    ```bash
    sudo yum install pacemaker pcs fence-agents-all resource-agents
    ```

### [SUSE Linux Enterprise Server (SLES)](#tab/sles)

Install the High Availability pattern in YaST or do it as part of the main installation of the server. The installation can be done with an ISO/DVD as a source or by getting it online.

> [!NOTE]  
> On SLES, the HA add-on gets initialized when the cluster is created.

### [Ubuntu](#tab/ubuntu)

```bash
sudo apt-get install pacemaker pcs fence-agents resource-agents
```

---

## Prepare the nodes for Pacemaker (RHEL and Ubuntu only)

Pacemaker itself uses a user created on the distribution named *hacluster*. The user gets created when the HA add-on is installed on RHEL and Ubuntu.

1. On each server that will serve as a node of the Pacemaker cluster, create the password for a user to be used by the cluster. The name used in the examples is *hacluster*, but any name can be used. The name and password must be the same on all nodes participating in the Pacemaker cluster.

   ```bash
   sudo passwd hacluster
   ```

1. On each node that will be part of the Pacemaker cluster, enable and start the `pcsd` service with the following commands (RHEL and Ubuntu):

   ```bash
   sudo systemctl enable pcsd
   sudo systemctl start pcsd
   ```

   Then execute the following command to ensure that `pcsd` is started.

   ```bash
   sudo systemctl status pcsd
   ```

1. Enable the Pacemaker service on each possible node of the Pacemaker cluster.

   ```bash
   sudo systemctl start pacemaker
   ```

   On Ubuntu, you see an error:

   ```output
   pacemaker Default-Start contains no runlevels, aborting.
   ```

   This error is a known issue. Despite the error, enabling the Pacemaker service is successful, and this bug will be fixed at some point in the future.

1. Next, create and start the Pacemaker cluster. There's one difference between RHEL and Ubuntu at this step. While on both distributions, installing `pcs` configures a default configuration file for the Pacemaker cluster, on RHEL, executing this command destroys any existing configuration and creates a new cluster.

## <a id="create"></a> Create the Pacemaker cluster

This section documents how to create and configure the cluster for each distribution of Linux.

### [Red Hat Enterprise Linux (RHEL)](#tab/rhel)

1. Authorize the nodes

   ```bash
   sudo pcs cluster auth <Node1 Node2 ... NodeN> -u hacluster
   ```

   where *NodeX* is the name of the node.

1. Create the cluster

   ```bash
   sudo pcs cluster setup --name <PMClusterName Nodelist> --start --all --enable
   ```

   where `PMClusterName` is the name assigned to the Pacemaker cluster and `Nodelist` is the list of names of the nodes separated by a space.

### [SUSE Linux Enterprise Server (SLES)](#tab/sles)

The process for creating a Pacemaker cluster is different on SLES than it's on RHEL and Ubuntu. The following steps document how to create a cluster with SLES.

1. Start the cluster configuration process by running the following command on one of the nodes:

   ```bash
   sudo ha-cluster-init
   ```

   You might be prompted that NTP isn't configured and that no watchdog device is found. That is fine for getting things up and running. Watchdog is related to fencing a failed node, if you use SLES's built-in fencing that is storage-based. NTP and watchdog can be configured later.

1. You're prompted to configure Corosync. You're prompted for the network address to bind to, as well as the multicast address and port. The network address is the subnet that you're using; for example, 192.191.190.0. You can accept the defaults at every prompt, or change if necessary.

1. Next, you're prompted to configure SBD, which is the disk-based fencing. This configuration can be done later if desired. If SBD isn't configured, unlike on RHEL and Ubuntu, `stonith-enabled` will by default be set to false.

1. Finally, you're prompted to configure an IP address for administration. This IP address is optional, but functions similar to the IP address for a Windows Server failover cluster (WSFC): it creates an IP address in the cluster to be used for connecting to it via HA Web Konsole (HAWK). This configuration, too, is optional.

1. Ensure that the cluster is up and running by issuing

   ```bash
   sudo crm status
   ```

1. Change the *hacluster* password with

   ```bash
   sudo passwd hacluster
   ```

1. If you configured an IP address for administration, you can test it in a browser, which also tests the password change for *hacluster*.

   :::image type="content" source="media/sql-server-linux-deploy-pacemaker-cluster/image2.png" alt-text="Screenshot of hacluster." lightbox="media/sql-server-linux-deploy-pacemaker-cluster/image2.png":::

1. On another SLES server that will be a node of the cluster, run

   ```bash
   sudo ha-cluster-join
   ```

1. When prompted, enter the name or IP address of the server that was configured as the first node of the cluster in the previous steps. The server is added as a node to the existing cluster.

1. Verify the node was added by issuing

   ```bash
   sudo crm status
   ```

1. Change the *hacluster* password with

   ```bash
   sudo passwd hacluster
   ```

1. Repeat Steps 8-11 for all other servers to be added to the cluster.

### [Ubuntu](#tab/ubuntu)

Configuring Ubuntu is similar to RHEL. However, there's one major difference: installing the Pacemaker packages creates a base configuration for the cluster, and enables and starts `pcsd`. If you try to configure the Pacemaker cluster by following the RHEL instructions exactly, you get an error. To fix this problem, perform the following steps:

1. Remove the default Pacemaker configuration from each node.

   ```bash
   sudo pcs cluster destroy
   ```

1. Create the cluster

   ```bash
   sudo pcs cluster setup --name <PMClusterName Nodelist> --start --all --enable
   ```

   where `PMClusterName` is the name assigned to the Pacemaker cluster and `Nodelist` is the list of names of the nodes separated by a space.

---

## Install the SQL Server HA and SQL Server Agent packages

Use the following commands to install the SQL Server HA package and [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Agent, if they aren't installed already. Installing the HA package after installing [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] requires a restart of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] for it to be used. These instructions assume that the repositories for the Microsoft packages are already set up, since [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] should be installed at this point.

- If you won't use [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Agent for log shipping or any other use, it doesn't have to be installed, so package **mssql-server-agent** can be skipped.

- The other optional packages for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Full-Text Search (**mssql-server-fts**) and [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Integration Services (**mssql-server-is**), aren't required for high availability, either for an FCI or an AG.

### [Red Hat Enterprise Linux (RHEL)](#tab/rhel)

```bash
sudo yum install mssql-server-ha mssql-server-agent
sudo systemctl restart mssql-server
```

### [SUSE Linux Enterprise Server (SLES)](#tab/sles)

```bash
sudo zypper install mssql-server-ha mssql-server-agent
sudo systemctl restart mssql-server
```

### [Ubuntu](#tab/ubuntu)

```bash
sudo apt-get install mssql-server-ha mssql-server-agent
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
> [Create and configure an availability group for SQL Server on Linux](sql-server-linux-create-availability-group.md).
