---
title: Configure SLES shared disk cluster for SQL Server
description: Implement high availability by configuring SUSE Linux Enterprise Server (SLES) shared disk cluster for SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 08/23/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Configure SLES shared disk cluster for SQL Server

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This guide provides instructions to create a two-nodes shared disk cluster for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on SUSE Linux Enterprise Server (SLES). The clustering layer is based on SUSE [High Availability Extension (HAE)](https://www.suse.com/products/highavailability) built on top of [Pacemaker](https://clusterlabs.org/).

For more information on cluster configuration, resource agent options, management, best practices, and recommendations, see [SUSE Linux Enterprise High Availability Extension 12 SP2](https://www.suse.com/documentation/sle-ha-12/index.html).

## Prerequisites

To complete the following end-to-end scenario, you need two machines to deploy the two nodes cluster and another server to configure the NFS share. Below steps outline how these servers will be configured.

## Setup and configure the operating system on each cluster node

The first step is to configure the operating system on the cluster nodes. For this walk through, use SLES with a valid subscription for the HA add-on.

## Install and configure SQL Server on each cluster node

1. Install and setup [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on both nodes. For detailed instructions, see [Install SQL Server on Linux](sql-server-linux-setup.md).

1. Designate one node as primary and the other as secondary, for purposes of configuration. Use these terms for the following this guide.

1. On the secondary node, stop and disable [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. The following example stops and disables [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo systemctl stop mssql-server
   sudo systemctl disable mssql-server
   ```

   > [!NOTE]  
   > At setup time, a Server Master Key is generated for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance and placed at `/var/opt/mssql/secrets/machine-key`. On Linux, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] always runs as a local account called mssql. Because it's a local account, its identity isn't shared across nodes. Therefore, you need to copy the encryption key from primary node to each secondary node so each local mssql account can access it to decrypt the Server Master Key.

1. On the primary node, create a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] login for Pacemaker and grant the login permission to run `sp_server_diagnostics`. Pacemaker uses this account to verify which node is running [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

   ```bash
   sudo systemctl start mssql-server
   ```

   Connect to the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] `master` database with the 'sa' account and run the following:

   ```sql
   USE [master]
   GO
   CREATE LOGIN [<loginName>] with PASSWORD= N'<loginPassword>'
   GRANT VIEW SERVER STATE TO <loginName>
   ```

1. On the primary node, stop and disable [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

1. Follow the directions [in the SUSE documentation](https://www.suse.com/documentation/sles11/book_sle_admin/data/sec_basicnet_yast.html) to configure and update the hosts file for each cluster node. The 'hosts' file must include the IP address and name of every cluster node.

   To check the IP address of the current node, run:

   ```bash
   sudo ip addr show
   ```

   Set the computer name on each node. Give each node a unique name that is 15 characters or less. Set the computer name by adding it to `/etc/hostname` using [YAST](https://www.suse.com/documentation/sles11/book_sle_admin/data/sec_basicnet_yast.html) or [manually](https://www.suse.com/documentation/sled11book_sle_admin/data/sec_basicnet_manconf.html).

   The following example shows `/etc/hosts` with additions for two nodes named `SLES1` and `SLES2`.

   ```output
   127.0.0.1      localhost
   10.128.18.128  SLES1
   10.128.16.77   SLES2
   ```

   All cluster nodes must be able to access each other via SSH. Tools like hb_report or crm_report (for troubleshooting) and Hawk's History Explorer require passwordless SSH access between the nodes, otherwise they can only collect data from the current node. In case you use a non-standard SSH port, use the -X option ([see man page](https://www.suse.com/documentation/sle_ha/book_sleha/data/sec_ha_requirements_other.html)). For example, if your SSH port is 3479, invoke an crm_report with:

    ```bash
    crm_report -X "-p 3479" [...]
    ```

    For more information, see [the Administration Guide](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#sec.ha.troubleshooting.misc).

In the next section you'll configure shared storage and move your database files to that storage.

## Configure shared storage and move database files

There are various solutions for providing shared storage. This walk-through demonstrates configuring shared storage with NFS. We recommend following best practices and use Kerberos to secure NFS:

- [Sharing File Systems with NFS](https://documentation.suse.com/sles/12-SP5/single-html/SLES-admin/#cha-nfs)

If you don't follow this guidance, anyone who can access your network and spoof the IP address of a SQL node will be able to access your data files. As always, make sure that you threat model your system before using it in production.

Another storage option is to use SMB file share:

- [Samba section of SUSE documentation](https://documentation.suse.com/sles/12-SP5/single-html/SLES-admin/#cha-samba)

### Configure an NFS server

To configure an NFS server, see the following steps in the SUSE documentation: [Configuring NFS Server](https://documentation.suse.com/sles/12-SP5/single-html/SLES-admin/#sec-nfs-configuring-nfs-server).

### Configure all cluster nodes to connect to the NFS shared storage

Before configuring the client NFS to mount the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] database files path to point to the shared storage location, make sure you save the database files to a temporary location to be able to copy them later on the share:

1. **On the primary node only**, save the database files to a temporary location. The following script, creates a new temporary directory, copies the database files to the new directory, and removes the old database files. As [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] runs as local user mssql, you need to make sure that after data transfer to the mounted share, local user has read-write access to the share.

   ```bash
   su mssql
   mkdir /var/opt/mssql/tmp
   cp /var/opt/mssql/data/* /var/opt/mssql/tmp
   rm /var/opt/mssql/data/*
   exit
   ```

   Configure the NFS client on all cluster nodes:

   - [Configuring Clients](https://documentation.suse.com/sles/12-SP5/single-html/SLES-admin/#sec-nfs-configuring-nfs-clients)

   > [!NOTE]  
   > It's recommended to follow SUSE's best practices and recommendations regarding Highly Available NFS storage: [Highly Available NFS Storage with DRBD and Pacemaker](https://www.suse.com/documentation/sle-ha-12/book_sleha_techguides/data/art_ha_quick_nfs.html).

1. Validate that [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] starts successfully with the new file path. Do this on each node. At this point only one node should run [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] at a time. They can't both run at the same time because they will both try to access the data files simultaneously (to avoid accidentally starting [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on both nodes, use a File System cluster resource, to make sure the share isn't mounted twice by the different nodes). The following commands start [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], check the status, and then stop [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

    ```bash
    sudo systemctl start mssql-server
    sudo systemctl status mssql-server
    sudo systemctl stop mssql-server
    ```

At this point, both instances of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] are configured to run with the database files on the shared storage. The next step is to configure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] for Pacemaker.

## Install and configure Pacemaker on each cluster node

1. **On both cluster nodes, create a file to store the SQL Server username and password for the Pacemaker login**. The following command creates and populates this file:

    ```bash
    sudo touch /var/opt/mssql/secrets/passwd
    sudo echo '<loginName>' >> /var/opt/mssql/secrets/passwd
    sudo echo '<loginPassword>' >> /var/opt/mssql/secrets/passwd
    sudo chown root:root /var/opt/mssql/secrets/passwd
    sudo chmod 600 /var/opt/mssql/secrets/passwd
    ```

1. **All cluster nodes must be able to access each other via SSH**. Tools like hb_report or crm_report (for troubleshooting) and Hawk's History Explorer require passwordless SSH access between the nodes, otherwise they can only collect data from the current node. In case you use a non-standard SSH port, use the -X option (see man page). For example, if your SSH port is 3479, invoke an hb_report with:

    ```bash
    crm_report -X "-p 3479" [...]
    ```

    For more information, see [System Requirements and Recommendations in the SUSE documentation](https://www.suse.com/documentation/sle_ha/book_sleha/data/sec_ha_requirements_other.html).

1. **Install the High Availability extension**. To install the extension, follow the steps in the following SUSE article:

    [Installation and Setup Quick Start](https://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html)

1. **Install the FCI resource agent for SQL Server**. Run the following commands on both nodes:

    ```bash
    sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/mssql-server-2017.repo
    sudo zypper --gpg-auto-import-keys refresh
    sudo zypper install mssql-server-ha
    ```

1. **Automatically set up the first node**. The next step is to set up a running one-node cluster by configuring the first node, SLES1. Follow the instructions in the SUSE article, [Setting Up the First Node](https://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html#sec.ha.inst.quick.setup.1st-node).

    When finished, check the cluster status with `crm status`:

    ```bash
    crm status
    ```

    It should show that one node, SLES1, is configured.

1. **Add nodes to an existing cluster**. Next join the SLES2 node to the cluster. Follow the instructions in the SUSE article, [Adding the Second Node](https://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html#sec.ha.inst.quick.setup.2nd-node).

    When finished, check the cluster status with **crm status**. If you have successfully added a second node, the output is similar to the following:

    ```output
    2 nodes configured
    1 resource configured
    Online: [ SLES1 SLES2 ]
    Full list of resources:
    admin_addr     (ocf::heartbeat:IPaddr2):       Started SLES1
    ```

    > [!NOTE]  
    > **admin_addr** is the virtual IP cluster resource which is configured during initial one-node cluster setup.

1. **Removal procedures**. If you need to remove a node from the cluster, use the **ha-cluster-remove** bootstrap script. For more information, see [Overview of the Bootstrap Scripts](https://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html#sec.ha.inst.quick.bootstrap).

## Configure the cluster resources for SQL Server

The following steps explain how to configure the cluster resource for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. There are two settings that you need to customize.

- **SQL Server Resource Name**: A name for the clustered [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] resource.
- **Timeout Value**: The timeout value is the amount of time that the cluster waits while a resource is brought online. For [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], this is the time that you expect [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to take to bring the `master` database online.

Update the values from the following script for your environment. Run on one node to configure and start the clustered service.

```bash
sudo crm configure
primitive <sqlServerResourceName> ocf:mssql:fci op start timeout=<timeout_in_seconds>
colocation <constraintName> inf: <virtualIPResourceName> <sqlServerResourceName>
show
commit
exit
```

For example, the following script creates a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] clustered resource named `mssqlha`.

```bash
sudo crm configure
primitive mssqlha ocf:mssql:fci op start timeout=60s
colocation admin_addr_mssqlha inf: admin_addr mssqlha
show
commit
exit
```

After the configuration is committed, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] will start on the same node as the virtual IP resource.

For more information, see [Configuring and Managing Cluster Resources (Command Line)](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#cha.ha.manual_config).

### Verify that SQL Server is started

To verify that [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is started, run the **crm status** command:

```bash
crm status
```

The following example shows the results when Pacemaker has successfully started as clustered resource.

```output
2 nodes configured
2 resources configured

Online: [ SLES1 SLES2 ]

Full list of resources:

 admin_addr     (ocf::heartbeat:IPaddr2):       Started SLES1
 mssqlha        (ocf::mssql:fci):       Started SLES1
```

## Manage cluster resources

To manage your cluster resources, see the following SUSE article:
[Managing Cluster Resources](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#sec.ha.config.crm)

### Manual failover

Although resources are configured to automatically fail over (or migrate) to other nodes of the cluster in the event of a hardware or software failure, you can also manually move a resource to another node in the cluster using either the Pacemaker GUI or the command line.

Use the migrate command for this task. For example, to migrate the SQL resource to a cluster node names SLES2 execute:

```bash
crm resource
migrate mssqlha SLES2
```

## Related content

- [SUSE Linux Enterprise High Availability Extension - Administration Guide](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html)
