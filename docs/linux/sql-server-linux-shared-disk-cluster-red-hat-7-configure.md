---
title: Configure Red Hat Enterprise Linux shared cluster for SQL Server | Microsoft Docs
description: Implement high availability by configuring Red Hat Enterprise Linux shared disk cluster for SQL Server.
author: MikeRayMSFT 
ms.author: mikeray 
manager: craigg
ms.date: 03/17/2017
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: dcc0a8d3-9d25-4208-8507-a5e65d2a9a15
---
# Configure Red Hat Enterprise Linux shared disk cluster for SQL Server

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This guide provides instructions to create a two-node shared disk cluster for SQL Server on Red Hat Enterprise Linux. The clustering layer is based on Red Hat Enterprise Linux (RHEL) [HA add-on](https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/6/pdf/High_Availability_Add-On_Overview/Red_Hat_Enterprise_Linux-6-High_Availability_Add-On_Overview-en-US.pdf) built on top of [Pacemaker](https://clusterlabs.org/). The SQL Server instance is active on either one node or the other.

> [!NOTE] 
> Access to Red Hat HA add-on and documentation requires a subscription. 

As the following diagram shows, storage is presented to two servers. Clustering components - Corosync and Pacemaker - coordinate communications and resource management. One of the servers has the active connection to the storage resources and the SQL Server. When Pacemaker detects a failure the clustering components manage moving the resources to the other node.  

![Red Hat Enterprise Linux 7 Shared Disk SQL Cluster](./media/sql-server-linux-shared-disk-cluster-red-hat-7-configure/LinuxCluster.png) 

For more information on cluster configuration, resource agents options, and management, visit [RHEL reference documentation](https://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/7/html/High_Availability_Add-On_Reference/index.html).


> [!NOTE] 
> At this point, SQL Server's integration with Pacemaker is not as coupled as with WSFC on Windows. From within SQL, there is no knowledge about the presence of the cluster, all orchestration is outside in and the service is controlled as a standalone instance by Pacemaker. Also for example, cluster dmvs sys.dm_os_cluster_nodes and sys.dm_os_cluster_properties will no records.
To use a connection string that points to a string server name and not use the IP, they will have to register in their DNS server the IP used to create the virtual IP resource (as explained in the following sections) with the chosen server name.

The following sections walk through the steps to set up a failover cluster solution. 

## Prerequisites

To complete the following end-to-end scenario, you need two machines to deploy the two nodes cluster and another server to configure the NFS server. Below steps outline how these servers will be configured.

## Setup and configure the operating system on each cluster node

The first step is to configure the operating system on the cluster nodes. For this walk through, use RHEL with a valid subscription for the HA add-on. 

## Install and configure SQL Server on each cluster node

1. Install and setup SQL Server on both nodes.  For detailed instructions, see [Install SQL Server on Linux](sql-server-linux-setup.md).

1. Designate one node as primary and the other as secondary, for purposes of configuration. Use these terms for the following this guide.  

1. On the secondary node, stop and disable SQL Server.

   The following example stops and disables SQL Server: 

   ```bash
   sudo systemctl stop mssql-server
   sudo systemctl disable mssql-server
   ```
> [!NOTE] 
> At setup time, a Server Master Key is generated for the SQL Server instance and placed at `/var/opt/mssql/secrets/machine-key`. On Linux, SQL Server always runs as a local account called mssql. Because it's a local account, its identity isn't shared across nodes. Therefore, you need to copy the encryption key from primary node to each secondary node so each local mssql account can access it to decrypt the Server Master Key. 

1. On the primary node, create a SQL server login for Pacemaker and grant the login permission to run `sp_server_diagnostics`. Pacemaker uses this account to verify which node is running SQL Server. 

   ```bash
   sudo systemctl start mssql-server
   ```

   Connect to the SQL Server `master` database with the sa account and run the following:

   ```bashsql
   USE [master]
   GO
   CREATE LOGIN [<loginName>] with PASSWORD= N'<loginPassword>'

   ALTER SERVER ROLE [sysadmin] ADD MEMBER [<loginName>]
   ```
   Alternatively, you can set the permissions at a more granular level. The Pacemaker login requires `VIEW SERVER STATE` to query health    status with sp_server_diagnostics, `setupadmin` and `ALTER ANY LINKED SERVER` to update the FCI instance name with the resource name    by running sp_dropserver and sp_addserver. 

1. On the primary node, stop and disable SQL Server. 

1. Configure the hosts file for each cluster node. The host file must include the IP address and name of every cluster node. 

    Check the IP address for each node. The following script shows the IP address of your current node. 

   ```bash
   sudo ip addr show
   ```

   Set the computer name on each node. Give each node a unique name that is 15 characters or less. Set the computer name by adding it to `/etc/hosts`. The following script lets you edit `/etc/hosts` with `vi`. 

   ```bash
   sudo vi /etc/hosts
   ```
   The following example shows `/etc/hosts` with additions for two nodes named `sqlfcivm1` and `sqlfcivm2`.

   ```bash
   127.0.0.1   localhost localhost4 localhost4.localdomain4
   ::1       localhost localhost6 localhost6.localdomain6
   10.128.18.128 sqlfcivm1
   10.128.16.77 sqlfcivm2
   ```

In the next section you will configure shared storage and move your database files to that storage. 

## Configure shared storage and move database files 

There are a variety of solutions for providing shared storage. This walk-through demonstrates configuring shared storage with NFS. We recommend to follow best practices and use Kerberos to secure NFS (you can find an example here: https://www.certdepot.net/rhel7-use-kerberos-control-access-nfs-network-shares/). 

>[!Warning]
>If you do not secure NFS, then anyone who can access your network and spoof the IP address of a SQL node will be able to access your data files. As always, make sure you threat model your system before using it in production. Another storage option is to use SMB fileshare.

### Configure shared storage with NFS

> [!IMPORTANT] 
> Hosting database files on a NFS server with version <4 is not supported in this release. This includes using NFS for shared disk failover clustering as well as databases on non-clustered instances. We are working on enabling other NFS server versions in the upcoming releases. 

On the NFS Server do the following:

1. Install `nfs-utils`

   ```bash
   sudo yum -y install nfs-utils
   ```

1. Enable and start `rpcbind`

   ```bash
   sudo systemctl enable rpcbind && sudo systemctl start rpcbind
   ```

1. Enable and start `nfs-server`
 
   ```bash
   sudo systemctl enable nfs-server && sudo systemctl start nfs-server
   ```
 
1.	Edit `/etc/exports` to export the directory you want to share. You need 1 line for each share you want. For example: 

   ```bash
   /mnt/nfs  10.8.8.0/24(rw,sync,no_subtree_check,no_root_squash)
   ```

1. Export the shares

   ```bash
   sudo exportfs -rav
   ```

1. Verify that the paths are shared/exported, run from the NFS server

   ```bash
   sudo showmount -e
   ```

1. Add exception in SELinux

   ```bash
   sudo setsebool -P nfs_export_all_rw 1
   ```
   
1. Open the firewall the server.

   ```bash 
   sudo firewall-cmd --permanent --add-service=nfs
   sudo firewall-cmd --permanent --add-service=mountd
   sudo firewall-cmd --permanent --add-service=rpc-bind
   sudo firewall-cmd --reload
   ```

### Configure all cluster nodes to connect to the NFS shared storage

Do the following steps on all cluster nodes.

1.	Install `nfs-utils`

   ```bash
   sudo yum -y install nfs-utils
   ```

1. Open up the firewall on clients and NFS server

   ```bash
   sudo firewall-cmd --permanent --add-service=nfs
   sudo firewall-cmd --permanent --add-service=mountd
   sudo firewall-cmd --permanent --add-service=rpc-bind
   sudo firewall-cmd --reload
   ```

1. Verify that you can see the NFS shares on client machines

   ```bash
   sudo showmount -e <IP OF NFS SERVER>
   ```

1. Repeat these steps on all cluster nodes.

For more information about using NFS, see the following resources:

* [NFS servers and firewalld | Stack Exchange](https://unix.stackexchange.com/questions/243756/nfs-servers-and-firewalld)
* [Mounting an NFS Volume | Linux Network Administrators Guide](https://www.tldp.org/LDP/nag2/x-087-2-nfs.mountd.html)
* [NFS server configuration | Red Hat Customer Portal](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/storage_administration_guide/nfs-serverconfig)

### Mount database files directory to point to the shared storage

1.  **On the primary node only**, save the database files to a temporary location.The following script, creates a new temporary directory, copies the database files to the new directory, and removes the old database files. As SQL Server runs as local user mssql, you need to make sure that after data transfer to the mounted share, local user has read-write access to the share. 

   ``` 
   $ sudo su mssql
   $ mkdir /var/opt/mssql/tmp
   $ cp /var/opt/mssql/data/* /var/opt/mssql/tmp
   $ rm /var/opt/mssql/data/*
   $ exit
   ``` 

1.  On all cluster nodes edit `/etc/fstab` file to include the mount command.  

   ```bash
   <IP OF NFS SERVER>:<shared_storage_path> <database_files_directory_path> nfs timeo=14,intr 
   ```
   
   The following script shows an example of the edit.  

   ``` 
   10.8.8.0:/mnt/nfs /var/opt/mssql/data nfs timeo=14,intr 
   ``` 
> [!NOTE] 
>If using a File System (FS) resource as recommended here, there is no need to preserve the mounting command in /etc/fstab. Pacemaker will take care of mounting the folder when it starts the FS clustered resource. With the help of fencing, it will ensure the FS is never mounted twice. 

1.  Run `mount -a` command for the system to update the mounted paths.  

1.  Copy the database and log files that you saved to `/var/opt/mssql/tmp` to the newly mounted share `/var/opt/mssql/data`. This only needs to be done **on the primary node**. Make sure that you give read write permissions to 'mssql' local user.

   ``` 
   $ sudo chown mssql /var/opt/mssql/data
   $ sudo chgrp mssql /var/opt/mssql/data
   $ sudo su mssql
   $ cp /var/opt/mssql/tmp/* /var/opt/mssql/data/
   $ rm /var/opt/mssql/tmp/*
   $ exit
   ``` 
 
1.  Validate that SQL Server starts successfully with the new file path. Do this on each node. At this point only one node should run SQL Server at a time. They cannot both run at the same time because they will both try to access the data files simultaneously (to avoid accidentally starting SQL Server on both nodes, use a File System cluster resource to make sure the share is not mounted twice by the different nodes). The following commands start SQL Server, check the status, and then stop SQL Server.
 
   ```bash
   sudo systemctl start mssql-server
   sudo systemctl status mssql-server
   sudo systemctl stop mssql-server
   ```
 
At this point both instances of SQL Server are configured to run with the database files on the shared storage. The next step is to configure SQL Server for Pacemaker. 

## Install and configure Pacemaker on each cluster node


2. On both cluster nodes, create a file to store the SQL Server username and password for the Pacemaker login. The following command creates and populates this file:

   ```bash
   sudo touch /var/opt/mssql/secrets/passwd
   echo '<loginName>' | sudo tee -a /var/opt/mssql/secrets/passwd
   echo '<loginPassword>' | sudo tee -a /var/opt/mssql/secrets/passwd
   sudo chown root:root /var/opt/mssql/secrets/passwd 
   sudo chmod 600 /var/opt/mssql/secrets/passwd    
   ```

3. On both cluster nodes, open the Pacemaker firewall ports. To open these ports with `firewalld`, run the following command:

   ```bash
   sudo firewall-cmd --permanent --add-service=high-availability
   sudo firewall-cmd --reload
   ```

   > If you're using another firewall that doesn't have a built-in high-availability configuration, the following ports need to be opened for Pacemaker to be able to communicate with other nodes in the cluster
   >
   > * TCP: Ports 2224, 3121, 21064
   > * UDP: Port 5405

1. Install Pacemaker packages on each node.

   ```bash
   sudo yum install pacemaker pcs fence-agents-all resource-agents
   ```

    

2. Set the password for the default user that is created when installing Pacemaker and Corosync packages. Use the same password on both nodes. 

   ```bash
   sudo passwd hacluster
   ```

    

3. Enable and start `pcsd` service and Pacemaker. This will allow nodes to rejoin the cluster after the reboot. Run the following command on both nodes.

   ```bash
   sudo systemctl enable pcsd
   sudo systemctl start pcsd
   sudo systemctl enable pacemaker
   ```

4. Install the FCI resource agent for SQL Server. Run the following commands on both nodes. 

   ```bash
   sudo yum install mssql-server-ha
   ```

## Create the cluster 

1. On one of the nodes, create the cluster.

   ```bash
   sudo pcs cluster auth <nodeName1 nodeName2 ...> -u hacluster
   sudo pcs cluster setup --name <clusterName> <nodeName1 nodeName2 ...>
   sudo pcs cluster start --all
   ```

   > RHEL HA add-on has fencing agents for VMWare and KVM. Fencing needs to be disabled on all other hypervisors. Disabling fencing agents is not recommended in production environments. As of timeframe, there are no fencing agents for HyperV or cloud environments. If you are running one of these configurations, you need to disable fencing. \**This is NOT recommended in a production system!**

   The following command disables the fencing agents.

   ```bash
   sudo pcs property set stonith-enabled=false
   sudo pcs property set start-failure-is-fatal=false
   ```

2. Configure the cluster resources for SQL Server, File System and virtual IP resources and push the configuration to the cluster. You need the following information:

   - **SQL Server Resource Name**: A name for the clustered SQL Server resource. 
   - **Floating IP Resource Name**: A name for the virtual IP address resource.
   - **IP Address**: The IP address that clients will use to connect to the clustered instance of SQL Server. 
   - **File System Resource Name**: A name for the File System resource.
   - **device**: The NFS share path
   - **device**: The local path that it's mounted to the share
   - **fstype**: File share type (i.e. nfs)

   Update the values from the following script for your environment. Run on one node to configure and start the clustered service.  

   ```bash
   sudo pcs cluster cib cfg 
   sudo pcs -f cfg resource create <sqlServerResourceName> ocf:mssql:fci
   sudo pcs -f cfg resource create <floatingIPResourceName> ocf:heartbeat:IPaddr2 ip=<ip Address>
   sudo pcs -f cfg resource create <fileShareResourceName> Filesystem device=<networkPath> directory=<localPath>         fstype=<fileShareType>
   sudo pcs -f cfg constraint colocation add <virtualIPResourceName> <sqlResourceName>
   sudo pcs -f cfg constraint colocation add <fileShareResourceName> <sqlResourceName> 
   sudo pcs cluster cib-push cfg
   ```

   For example, the following script creates a SQL Server clustered resource named `mssqlha`, and a floating IP resources with IP address `10.0.0.99`. It also creates a Filesystem resource and adds constraints so all resources are colocated on same node as SQL resource. 

   ```bash
   sudo pcs cluster cib cfg
   sudo pcs -f cfg resource create mssqlha ocf:mssql:fci
   sudo pcs -f cfg resource create virtualip ocf:heartbeat:IPaddr2 ip=10.0.0.99
   sudo pcs -f cfg resource create fs Filesystem device="10.8.8.0:/mnt/nfs" directory="/var/opt/mssql/data" fstype="nfs"
   sudo pcs -f cfg constraint colocation add virtualip mssqlha
   sudo pcs -f cfg constraint colocation add fs mssqlha
   sudo pcs cluster cib-push cfg
   ```

   After the configuration is pushed, SQL Server will start on one node. 

3. Verify that SQL Server is started. 

   ```bash
   sudo pcs status 
   ```

   The following examples shows the results when Pacemaker has successfully started a clustered instance of SQL Server. 

   ```
   fs     (ocf::heartbeat:Filesystem):    Started sqlfcivm1
   virtualip     (ocf::heartbeat:IPaddr2):      Started sqlfcivm1
   mssqlha  (ocf::mssql:fci): Started sqlfcivm1
   
   PCSD Status:
    slqfcivm1: Online
    sqlfcivm2: Online
   
   Daemon Status:
    corosync: active/disabled
    pacemaker: active/enabled
    pcsd: active/enabled
   ```

## Additional resources

* [Cluster from Scratch](https://clusterlabs.org/doc/Cluster_from_Scratch.pdf) guide from Pacemaker

## Next steps

[Operate SQL Server on Red Hat Enterprise Linux shared disk cluster](sql-server-linux-shared-disk-cluster-red-hat-7-operate.md)
