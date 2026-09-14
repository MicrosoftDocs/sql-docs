---
title: Configure SLES Shared Disk Cluster
titleSuffix: SQL Server on Linux
description: Implement high availability by configuring SUSE Linux Enterprise Server (SLES) shared disk cluster for SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 09/14/2026
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - linux-related-content
  - sfi-ropc-blocked
---
# Configure SLES shared disk cluster for SQL Server

[!INCLUDE [SQL Server - Linux](../../../includes/applies-to-version/sql-linux.md)]

This guide provides instructions to create a two-node shared disk cluster for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on SUSE Linux Enterprise Server (SLES). The clustering layer is based on SUSE [High Availability Extension (HAE)](https://www.suse.com/products/highavailability) built on top of [Pacemaker](https://clusterlabs.org/).

[!INCLUDE [sles-deprecated](../../includes/sles-deprecated.md)]

For more information on cluster configuration, resource agent options, management, best practices, and recommendations, see [SUSE Linux Enterprise High Availability Extension 15](https://documentation.suse.com/sle-ha/15-SP6/).

## Prerequisites

To complete the following end-to-end scenario, you need two machines to deploy the two nodes cluster and another server to configure the NFS share. The following steps outline how to configure these servers.

## Setup and configure the operating system on each cluster node

The first step is to configure the operating system on the cluster nodes. For this walkthrough, use SLES with a valid subscription for the HA add-on.

## Install and configure SQL Server on each cluster node

1. Install and set up [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on both nodes. For detailed instructions, see [Installation guidance for SQL Server on Linux](../../install-upgrade/setup.md).

1. Designate one node as primary and the other as secondary, for purposes of configuration. Use these terms for the following this guide.

1. On the secondary node, stop and disable [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]. The following example stops and disables [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]:

   ```bash
   sudo systemctl stop mssql-server
   sudo systemctl disable mssql-server
   ```

   > [!NOTE]  
   > At setup time, a server master key (SMK) is generated for the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance and placed at `/var/opt/mssql/secrets/machine-key`. On Linux, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] always runs as a local account called `mssql`. Because it's a local account, its identity isn't shared across nodes. You must copy the encryption key from the primary node to each secondary node so each local `mssql` account can access it to decrypt the SMK.

1. On the primary node, create a [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] login for Pacemaker and grant the login permission to run `sp_server_diagnostics`. Pacemaker uses this account to verify which node is running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].

   ```bash
   sudo systemctl start mssql-server
   ```

   Connect to the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] `master` database with the `sa` account and run the following script:

   ```sql
   USE master;
   GO

   CREATE LOGIN [<loginName>] with PASSWORD = N'<password>';
   GRANT VIEW SERVER STATE TO <loginName>;
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](../../includes/password-complexity.md)]

1. On the primary node, stop and disable [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].

1. Follow the directions [in the SUSE documentation](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/article-installation.html) to configure and update the hosts file for each cluster node. The `hosts` file must include the IP address and name of every cluster node.

   To check the IP address of the current node, run:

   ```bash
   sudo ip addr show
   ```

   Set the computer name on each node. Give each node a unique name that is 15 characters or less. Set the computer name by adding it to `/etc/hostname` using [YAST](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/article-installation.html) or [manually](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/article-installation.html).

   The following example shows `/etc/hosts` with additions for two nodes named `SLES1` and `SLES2`.

   ```output
   127.0.0.1      localhost
   10.128.18.128  SLES1
   10.128.16.77   SLES2
   ```

   All cluster nodes must have passwordless SSH access to each other. Otherwise, tools such as `hb_report`, `crm_report`, and Hawk's History Explorer can collect data only from the local node. If you use a non-standard SSH port, use the `-X` option (see [Other Requirements and Recommendations](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/article-installation.html#vl-ha-inst-quick-req-other)). For example, if your SSH port is 3479, invoke `crm_report` with:

   ```bash
   crm_report -X "-p 3479" [...]
   ```

   For more information, see [the Administration Guide](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/book-administration.html).

In the next section, you configure shared storage and move your database files to that storage.

## Configure shared storage and move database files

You can use various solutions for providing shared storage. This walkthrough demonstrates configuring shared storage with NFS. Follow best practices and use Kerberos to secure NFS:

- [Sharing File Systems with NFS](https://documentation.suse.com/sles/15-SP6/html/SLES-all/book-administration.html)

If you don't follow this guidance, anyone who can access your network and spoof the IP address of a SQL node can access your data files. As always, perform threat modeling on your system before you use it in production.

Another storage option is to use SMB file share:

- [Samba section of SUSE documentation](https://documentation.suse.com/sles/15-SP6/html/SLES-all/book-administration.html)

### Configure an NFS server

To configure an NFS server, see the following steps in the SUSE documentation: [Configuring NFS Server](https://documentation.suse.com/sles/15-SP6/html/SLES-all/book-administration.html).

### Configure all cluster nodes to connect to the NFS shared storage

Before configuring the client NFS to mount the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] database files path to point to the shared storage location, make sure you save the database files to a temporary location so you can copy them later to the share:

1. **On the primary node only**, save the database files to a temporary location. The following script creates a new temporary directory, copies the database files to the new directory, and removes the old database files. As [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] runs as local user `mssql`, you need to make sure that after data transfer to the mounted share, local user has read-write access to the share.

   ```bash
   su mssql
   mkdir /var/opt/mssql/tmp
   cp /var/opt/mssql/data/* /var/opt/mssql/tmp
   rm /var/opt/mssql/data/*
   exit
   ```

   Configure the NFS client on all cluster nodes:

   - [Configuring Clients](https://documentation.suse.com/sles/15-SP6/html/SLES-all/book-administration.html)

   > [!NOTE]  
   > For SUSE best practices and recommendations regarding Highly Available NFS storage, see [Highly Available NFS Storage with DRBD and Pacemaker](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/article-nfs-storage.html).

1. On each node, validate that [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] starts successfully with the new file path. At this point, only one node should run [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] at a time. They can't both run at the same time because they both try to access the data files simultaneously.

   To prevent [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] from starting on both nodes, use a File System cluster resource to ensure that the share is mounted by only one node at a time.

   The following commands start [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], check the status, and then stop [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].

   ```bash
   sudo systemctl start mssql-server
   sudo systemctl status mssql-server
   sudo systemctl stop mssql-server
   ```

At this point, both instances of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] are configured to run with the database files on the shared storage. The next step is to configure [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] for Pacemaker.

## Install and configure Pacemaker on each cluster node

1. **On both cluster nodes, create a file to store the SQL Server username and password for the Pacemaker login**. The following command creates and populates this file:

   ```bash
   sudo touch /var/opt/mssql/secrets/passwd
   echo '<loginName>' | sudo tee -a /var/opt/mssql/secrets/passwd
   echo '<password>' | sudo tee -a /var/opt/mssql/secrets/passwd
   sudo chown root:root /var/opt/mssql/secrets/passwd
   sudo chmod 600 /var/opt/mssql/secrets/passwd
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](../../includes/password-complexity.md)]

1. **All cluster nodes must access each other through SSH**. Tools like `hb_report` or `crm_report` (for troubleshooting) and Hawk's History Explorer require passwordless SSH access between the nodes. Otherwise, they can only collect data from the current node. If you use a non-standard SSH port, use the `-X` option (see `man` page). For example, if your SSH port is 3479, invoke an `hb_report` with:

   ```bash
   crm_report -X "-p 3479" [...]
   ```

   For more information, see [System Requirements and Recommendations in the SUSE documentation](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/cha-ha-requirements.html).

1. **Install the High Availability extension**. To install the extension, follow the steps in the following SUSE article:

   [Installation and Setup Quick Start](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/article-installation.html)

1. **Install the FCI resource agent for SQL Server**. Run the following commands on both nodes:

   ```bash
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/15/mssql-server-2022.repo
   sudo zypper --gpg-auto-import-keys refresh
   sudo zypper install mssql-server-ha
   ```

1. **Automatically set up the first node**. Set up a running one-node cluster by configuring the first node, SLES1. Follow the instructions in the SUSE article, [Setting Up the First Node](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/article-installation.html#sec-ha-inst-quick-setup-1st-node).

   When finished, check the cluster status with `crm status`:

   ```bash
   crm status
   ```

   It shows that one node, SLES1, is configured.

1. **Add nodes to an existing cluster**. Next, join the SLES2 node to the cluster. Follow the instructions in the SUSE article, [Adding the Second Node](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/article-installation.html#sec-ha-inst-quick-setup-2nd-node).

   When finished, check the cluster status with **crm status**. If you successfully add a second node, the output looks similar to the following example:

   ```output
   2 nodes configured
   1 resource configured
   Online: [ SLES1 SLES2 ]
   Full list of resources:
   admin_addr     (ocf::heartbeat:IPaddr2):       Started SLES1
   ```

   > [!NOTE]  
   > **admin_addr** is the virtual IP cluster resource that you configure during initial one-node cluster setup.

1. **Removal procedures**. If you need to remove a node from the cluster, use the **ha-cluster-remove** bootstrap script. For more information, see [Overview of the Bootstrap Scripts](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/article-installation.html#sec-ha-inst-quick-bootstrap).

## Configure the cluster resources for SQL Server

The following steps explain how to configure the cluster resource for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]. Customize the following two settings:

- **SQL Server Resource Name**: A name for the clustered [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] resource.
- **Timeout Value**: The timeout value is the amount of time that the cluster waits while a resource is brought online. For [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], this value represents the time that you expect [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] to take to bring the `master` database online.

Update the values in the following script for your environment. Run the script on one node to configure and start the clustered service.

```bash
sudo crm configure
primitive <sqlServerResourceName> ocf:mssql:fci op start timeout=<timeout_in_seconds>
colocation <constraintName> inf: <virtualIPResourceName> <sqlServerResourceName>
show
commit
exit
```

For example, the following script creates a [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] clustered resource named `mssqlha`.

```bash
sudo crm configure
primitive mssqlha ocf:mssql:fci op start timeout=60s
colocation admin_addr_mssqlha inf: admin_addr mssqlha
show
commit
exit
```

After you commit the configuration, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] starts on the same node as the virtual IP resource.

For more information, see [Configuring and Managing Cluster Resources (Command Line)](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/book-administration.html).

### Verify that SQL Server is started

To verify that [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] is started, run the **crm status** command:

```bash
crm status
```

The following example shows the results when Pacemaker successfully starts as clustered resource.

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
[Managing Cluster Resources](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/book-administration.html)

### Manual failover

Although resources are configured to automatically fail over or migrate to other cluster nodes on hardware or software failure, you can also move them manually using the Pacemaker GUI or the command line.

Use the `migrate` command for this task. For example, to migrate the SQL resource to a cluster node named `SLES2`, run:

```bash
crm resource
migrate mssqlha SLES2
```

## Related content

- [SUSE Linux Enterprise High Availability Extension - Administration Guide](https://documentation.suse.com/sle-ha/15-SP6/html/SLE-HA-all/article-installation.html#sec-ha-inst-quick-installation)
