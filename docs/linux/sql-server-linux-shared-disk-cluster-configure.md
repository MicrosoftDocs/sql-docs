---
title: Configure failover cluster instance - SQL Server on Linux (RHEL) | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: craigg
ms.date: 08/28/2017
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: 31c8c92e-12fe-4728-9b95-4bc028250d85 
---
# Configure failover cluster instance - SQL Server on Linux (RHEL)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

A SQL Server two-node shared disk failover cluster instance provides server-level redundancy for high availability. In this tutorial, you learn how to create a two-node failover cluster instance of SQL Server on Linux. The specific steps that you will complete include:

> [!div class="checklist"]
> * Set up and configure Linux
> * Install and configure SQL Server
> * Configure the hosts file
> * Configure shared storage and move the database files
> * Install and configure Pacemaker on each cluster node
> * Configure the failover cluster instance

This article explains how to create a two-node shared disk failover cluster instance (FCI) for SQL Server. The article includes instructions and script examples for Red Hat Enterprise Linux (RHEL). Ubuntu distributions are similar to RHEL so the script examples will normally also work on Ubuntu. 

For conceptual information, see [SQL Server Failover Cluster Instance (FCI) on Linux](sql-server-linux-shared-disk-cluster-concepts.md).

## Prerequisites

To complete the following end-to-end scenario, you need two machines to deploy the two nodes cluster and another server for storage. Below steps outline how these servers will be configured.

## Set up and configure Linux

The first step is to configure the operating system on the cluster nodes. On each node in the cluster, configure a linux distribution. Use the same distribution and version on both nodes. Use either one or the other of the following distributions:
    
* RHEL with a valid subscription for the HA add-on

## Install and configure SQL Server

1. Install and set up SQL Server on both nodes.  For detailed instructions, see [Install SQL Server on Linux](sql-server-linux-setup.md).
1. Designate one node as primary and the other as secondary, for purposes of configuration. Use these terms for the following this guide.  
1. On the secondary node, stop and disable SQL Server.
    The following example stops and disables SQL Server: 
    ```bash
    sudo systemctl stop mssql-server
    sudo systemctl disable mssql-server
    ```

    > [!NOTE] 
    > At set up time, a Server Master Key is generated for the SQL Server instance and placed at `var/opt/mssql/secrets/machine-key`. On Linux, SQL Server always runs as a local account called mssql. Because it's a local account, its identity isn't shared across nodes. Therefore, you need to copy the encryption key from primary node to each secondary node so each local mssql account can access it to decrypt the Server Master Key. 

1.  On the primary node, create a SQL server login for Pacemaker and grant the login permission to run `sp_server_diagnostics`. Pacemaker uses this account to verify which node is running SQL Server. 

    ```bash
    sudo systemctl start mssql-server
    ```
   
   Connect to the SQL Server `master` database with the sa account and run the following:

   ```sql
   USE [master]
   GO
   CREATE LOGIN [<loginName>] with PASSWORD= N'<loginPassword>'
   ALTER SERVER ROLE [sysadmin] ADD MEMBER [<loginName>]
   ```

   Alternatively, you can set the permissions at a more granular level. The Pacemaker login requires `VIEW SERVER STATE` to query health status with sp_server_diagnostics, `setupadmin` and `ALTER ANY LINKED SERVER` to update the FCI instance name with the resource name by running sp_dropserver and sp_addserver. 

1. On the primary node, stop and disable SQL Server. 

## Configure the hosts file

On each cluster node, configure the hosts file. The hosts file must include the IP address and name of every cluster node.

1. Check the IP address for each node. The following script shows the IP address of your current node. 

    ```bash
    sudo ip addr show
    ```

1. Set the computer name on each node. Give each node a unique name that is 15 characters or less. Set the computer name by adding it to `/etc/hosts`. The following script lets you edit `/etc/hosts` with `vi`. 

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

## Configure storage & move database files  

You need to provide storage that both nodes can access. You can use iSCSI, NFS, or SMB. Configure storage, present the storage to the cluster nodes, and then move the database files to the new storage. The following articles explain the steps for each storage type:

- [Configure failover cluster instance - iSCSI - SQL Server on Linux](sql-server-linux-shared-disk-cluster-configure-iscsi.md)
- [Configure failover cluster instance - NFS - SQL Server on Linux](sql-server-linux-shared-disk-cluster-configure-nfs.md)
- [Configure failover cluster instance - SMB - SQL Server on Linux](sql-server-linux-shared-disk-cluster-configure-smb.md)

## Install and configure Pacemaker on each cluster node

1. On both cluster nodes, create a file to store the SQL Server username and password for the Pacemaker login. 

    The following command creates and populates this file:

    ```bash
    sudo touch /var/opt/mssql/secrets/passwd
    sudo echo '<loginName>' >> /var/opt/mssql/secrets/passwd
    sudo echo '<loginPassword>' >> /var/opt/mssql/secrets/passwd
    sudo chown root:root /var/opt/mssql/secrets/passwd 
    sudo chmod 600 /var/opt/mssql/secrets/passwd    
    ```

1. On both cluster nodes, open the Pacemaker firewall ports. To open these ports with `firewalld`, run the following command:

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
1. Set the password for the default user that is created when installing Pacemaker and Corosync packages. Use the same password on both nodes. 

   ```bash
   sudo passwd hacluster
   ```
1. Enable and start `pcsd` service and Pacemaker. This will allow nodes to rejoin the cluster after the reboot. Run the following command on both nodes.

   ```bash
   sudo systemctl enable pcsd
   sudo systemctl start pcsd
   sudo systemctl enable pacemaker
   ```

1. Install the FCI resource agent for SQL Server. Run the following commands on both nodes. 

   ```bash
   sudo yum install mssql-server-ha
   ```

## Configure the failover cluster instance

The FCI will be created in a resource group. This is a little bit easier since the resource group alleviates the need for constraints. However, add the resources into the resource group in the order they should start. The order they should start is: 

1. Storage resource
2. Network resource
3. Application resource

This example will create an FCI in the group NewLinFCIGrp. The name of the resource group must be unique from any resource created in Pacemaker.

1.	Create the disk resource. You will get no response back if there is not a problem. The way to create the disk resource depends on the storage type. The following is an example for each storage type. Use the example that applies to the storage type for your clustered storage.

    **iSCSI**

    ```bash
    sudo pcs resource create <iSCSIDiskResourceName> Filesystem device="/dev/<VolumeGroupName>/<LogicalVolumeName>" directory="<FolderToMountiSCSIDisk>" fstype="<FileSystemType>" --group RGName
    ```

    \<iSCSIDIskResourceName> is the name of the resource associated with the iSCSI disk

    \<VolumeGroupName> is the name of the volume group  

    \<LogicalVolumeName> is the name of the logical volume that was created  

    \<FolderToMountiSCSIDIsk> is the folder to mount the disk (for system databases and the default location, it would be /var/opt/mssql/data)

    \<FileSystemType> would be EXT4 or XFS depending on how things were formatted and what the distribution supports. 

    **NFS**

    ```bash
    sudo pcs resource create <NFSDiskResourceName> Filesystem device="<IPAddressOfNFSServer>:<FolderOnNFSServer>" directory="<FolderToMountNFSShare>" fstype=nfs4 options=" nfsvers=4.2,timeo=14,intr" --group RGName
    mount -t nfs4 IPAddressOfNFSServer:FolderOnNFSServer /var/opt/mssql/data -o 
    ```

    \<NFSDIskResourceName> is the name of the resource associated with the NFS share

    \<IPAddressOfNFSServer> is the IP address of the NFS server that you are going to use

    \<FolderOnNFSServer> is the name of the NFS share

    \<FolderToMountNFSShare> is the folder to mount the disk (for system databases and the default location, it would be /var/opt/mssql/data)

    An example is shown here:

    ```bash
    mount -t nfs4 200.201.202.63:/var/nfs/fci1 /var/opt/mssql/data -o nfsvers=4.2,timeo=14,intr
    ```

    **SMB**

    ```bash
    sudo pcs resource create SMBDiskResourceName Filesystem device="//<ServerName>/<ShareName>" directory="<FolderName>" fstype=cifs options="vers=3.0,username=<UserName>,password=<Password>,domain=<ADDomain>,uid=<mssqlUID>,gid=<mssqlGID>,file_mode=0777,dir_mode=0777" --group <RGName>
    ```

    \<ServerName> is the name of the server with the SMB share

    \<ShareName> is the name of the share

    \<FolderName> is the name of the folder created in the last step
    
    \<UserName> is the name of the user to access the share

    \<Password> is the password for the user

    \<ADDomain> is the AD DS domain (if applicable when using a Windows Server-based SMB share)

    \<mssqlUID> is the UID of the mssql user

    \<mssqlGID> is the GID of the mssql user

    \<RGName> is the name of the resource group
 
2.	Create the IP address that will be used by the FCI. You will get no response back if there is not a problem.

    ```bash
    sudo pcs resource create <IPResourceName> ocf:heartbeat:IPaddr2 ip=<IPAddress> nic=<NetworkCard> cidr_netmask=<NetMask> --group <RGName>
    ```

    \<IPResourceName> is the name of the resource associated with the IP address

    \<IPAddress> is the IP address for the FCI

    \<NetworkCard> is the network card associated with the subnet (i.e. eth0)

    \<NetMask> is the netmask of the subnet (i.e. 24)

    \<RGName> is the name of the resource group
 
3.	Create the FCI resource. You will get no response back if there is not a problem.

    ```bash
    sudo pcs resource create FCIResourceName ocf:mssql:fci op defaults timeout=60s --group RGName
    ```

    \<FCIResourceName> is not only the name of the resource, but the friendly name that is associated with the FCI. This is what users and applications will use to connect. 

    \<RGName> is the name of the resource group.
 
4.	Run the command `sudo pcs resource`. The FCI should be online.
 
5.	Connect to the FCI with SSMS or sqlcmd using the DNS/resource name of the FCI.

6.	Issue the statement `SELECT @@SERVERNAME`. It should return the name of the FCI.

7.	Issue the statement `SELECT SERVERPROPERTY('ComputerNamePhysicalNetBIOS')`. It should return the name of the node that the FCI is running on.

8.	Manually fail the FCI to the other node(s). See the instructions under [Operate failover cluster instance - SQL Server on Linux](sql-server-linux-shared-disk-cluster-operate.md).

9.	Finally, fail the FCI back to the original node and remove the colocation constraint.

<!---
|Distribution |Topic 
|----- |-----
|**Red Hat Enterprise Linux with HA add-on** |[Configure](sql-server-linux-shared-disk-cluster-red-hat-7-configure.md)<br/>[Operate](sql-server-linux-shared-disk-cluster-red-hat-7-operate.md)
|**SUSE Linux Enterprise Server with HA add-on** |[Configure](sql-server-linux-shared-disk-cluster-sles-configure.md)
-->
## Summary

In this tutorial you completed the following tasks.

> [!div class="checklist"]
> * Set up and configure Linux
> * Install and configure SQL Server
> * Configure the hosts file
> * Configure shared storage and move the database files
> * Install and configure Pacemaker on each cluster node
> * Configure the failover cluster instance

## Next steps

- [Operate failover cluster instance - SQL Server on Linux](sql-server-linux-shared-disk-cluster-operate.md)

<!--Image references-->
