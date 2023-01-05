---
title: Operate RHEL FCI for SQL Server on Linux
description: Learn how to operate a Red Hat Enterprise Linux (RHEL) shared disk failover cluster instance (FCI) for SQL Server for high availability, such as manually failover the FCI, and add or remove nodes to the cluster.
ms.custom: seo-lt-2019
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto
ms.date: 03/17/2017
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
ms.assetid: 075ab7d8-8b68-43f3-9303-bbdf00b54db1
---
# Operate RHEL failover cluster instance (FCI) for SQL Server

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This document describes how to do the following tasks for SQL Server on a shared disk failover cluster with Red Hat Enterprise Linux.

- Manually failover the cluster
- Monitor a failover cluster SQL Server service
- Add a cluster node
- Remove a cluster node
- Change the SQL Server resource monitoring frequency

## Architecture description

The clustering layer is based on Red Hat Enterprise Linux (RHEL) [HA add-on](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/pdf/high_availability_add-on_overview/red_hat_enterprise_linux-7-high_availability_add-on_overview-en-us.pdf) built on top of [Pacemaker](https://clusterlabs.org/). Corosync and Pacemaker coordinate cluster communications and resource management. The SQL Server instance is active on either one node or the other.

The following diagram illustrates the components in a Linux cluster with SQL Server. 

![Red Hat Enterprise Linux 7 Shared Disk SQL Cluster](./media/sql-server-linux-shared-disk-cluster-red-hat-7-configure/LinuxCluster.png) 

For more information on cluster configuration, resource agents options, and management, visit [RHEL reference documentation](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/high_availability_add-on_reference/index).

## <a name = "failManual"></a>Failover cluster manually

The `resource move` command creates a constraint forcing the resource to start on the target node.  After executing the `move` command, executing resource `clear` will remove the constraint so it is possible to move the resource again or have the resource automatically fail over. 

```bash
sudo pcs resource move <sqlResourceName> <targetNodeName>  
sudo pcs resource clear <sqlResourceName> 
```

The following example moves the **mssqlha** resource to a node named **sqlfcivm2**, and then removes the constraint so that the resource can move to a different node later.  

```bash
sudo pcs resource move mssqlha sqlfcivm2 
sudo pcs resource clear mssqlha 
```

## Monitor a failover cluster SQL Server service

View the current cluster status:

```bash
sudo pcs status  
```

View live status of cluster and resources:

```bash
sudo crm_mon 
```

View the resource agent logs at `/var/log/cluster/corosync.log`

## Add a node to a cluster

1. Check the IP address for each node. The following script shows the IP address of your current node. 

   ```bash
   ip addr show
   ```

3. The new node needs a unique name that is 15 characters or less. By default in Red Hat Linux the computer name is `localhost.localdomain`. This default name may not be unique and is too long. Set the computer name the new node. Set the computer name by adding it to `/etc/hosts`. The following script lets you edit `/etc/hosts` with `vi`. 

   ```bash
   sudo vi /etc/hosts
   ```

   The following example shows `/etc/hosts` with additions for three nodes named `sqlfcivm1`, `sqlfcivm2`, and`sqlfcivm3`.

   ```
   127.0.0.1   localhost localhost4 localhost4.localdomain4
   ::1         localhost localhost6 localhost6.localdomain6
   10.128.18.128 fcivm1
   10.128.16.77 fcivm2
   10.128.14.26 fcivm3
    ```
    
   The file should be the same on every node. 

1. Stop the SQL Server service on the new node.

1. Follow the instructions to mount the database file directory to the shared location:

   From the NFS server, install `nfs-utils`

   ```bash
   sudo yum -y install nfs-utils 
   ``` 

   Open up the firewall on clients and NFS server 

   ```bash
   sudo firewall-cmd --permanent --add-service=nfs
   sudo firewall-cmd --permanent --add-service=mountd
   sudo firewall-cmd --permanent --add-service=rpc-bind
   sudo firewall-cmd --reload
   ```

   Edit /etc/fstab file to include the mount command: 

   ```bash
   <IP OF NFS SERVER>:<shared_storage_path> <database_files_directory_path> nfs timeo=14,intr
   ```

   Run `mount -a` for the changes to take effect.
   
1. On the new node, create a file to store the SQL Server username and password for the Pacemaker login. The following command creates and populates this file:

   ```bash
   sudo touch /var/opt/mssql/passwd
   sudo echo "<loginName>" >> /var/opt/mssql/secrets/passwd
   sudo echo "<loginPassword>" >> /var/opt/mssql/secrets/passwd
   sudo chown root:root /var/opt/mssql/passwd
   sudo chmod 600 /var/opt/mssql/passwd
   ```

3. On the new node, open the Pacemaker firewall ports. To open these ports with `firewalld`, run the following command:

   ```bash
   sudo firewall-cmd --permanent --add-service=high-availability
   sudo firewall-cmd --reload
   ```

   > [!NOTE]
   > If you're using another firewall that doesn't have a built-in high-availability configuration, the following ports need to be opened for Pacemaker to be able to communicate with other nodes in the cluster
   >
   > * TCP: Ports 2224, 3121, 21064
   > * UDP: Port 5405

1. Install Pacemaker packages on the new node.

   ```bash
   sudo yum install pacemaker pcs fence-agents-all resource-agents
   ```
 
2. Set the password for the default user that is created when installing Pacemaker and Corosync packages. Use the same password as the existing nodes. 

   ```bash
   sudo passwd hacluster
   ```
 
3. Enable and start `pcsd` service and Pacemaker. This will allow the new node to rejoin the cluster after the reboot. Run the following command on the new node.

   ```bash
   sudo systemctl enable pcsd
   sudo systemctl start pcsd
   sudo systemctl enable pacemaker
   ```

4. Install the FCI resource agent for SQL Server. Run the following commands on the new node. 

   ```bash
   sudo yum install mssql-server-ha
   ```

1. On an existing node from the cluster, authenticate the new node and add it to the cluster:

    ```bash
    sudo pcs    cluster auth <nodeName3> -u hacluster 
    sudo pcs    cluster node add <nodeName3> 
    ```

    The following example adds a node named **vm3** to the cluster.

    ```bash
    sudo pcs    cluster auth  
    sudo pcs    cluster start 
    ```

## Remove nodes from a cluster

To remove a  node from a cluster run the following command:

```bash
sudo pcs    cluster node remove <nodeName>  
```

## Change the frequency of sqlservr resource monitoring interval

```bash
sudo pcs    resource op monitor interval=<interval>s <sqlResourceName> 
```

The following example sets the monitoring interval to 2 seconds for the mssql resource:

```bash
sudo pcs    resource op monitor interval=2s mssqlha 
```
## Troubleshoot Red Hat Enterprise Linux shared disk cluster for SQL Server

In troubleshooting the cluster it may help to understand how the three daemons work together to manage cluster resources. 

| Daemon | Description 
| ----- | -----
| Corosync | Provides quorum membership and messaging between cluster nodes.
| Pacemaker | Resides on top of Corosync and provides state machines for resources. 
| PCSD | Manages both Pacemaker and Corosync through the `pcs` tools

PCSD must be running in order to use `pcs` tools. 

### Current cluster status 

`sudo pcs status` returns basic information about the cluster, quorum, nodes, resources, and daemon status for each node. 

An example of a healthy pacemaker quorum output would be:

```
Cluster name: MyAppSQL 
Last updated: Wed Oct 31 12:00:00 2016  Last change: Wed Oct 31 11:00:00 2016 by root via crm_resource on sqlvmnode1 
Stack: corosync 
Current DC: sqlvmnode1  (version 1.1.13-10.el7_2.4-44eb2dd) - partition with quorum 
3 nodes and 1 resource configured 

Online: [ sqlvmnode1 sqlvmnode2 sqlvmnode3] 

Full list of resources: 

mssqlha (ocf::sql:fci): Started sqlvmnode1 

PCSD Status: 
sqlvmnode1: Online 
sqlvmnode2: Online 
sqlvmnode3: Online 

Daemon Status: 
corosync: active/disabled 
pacemaker: active/enabled 
```

In the example, `partition with quorum` means that a majority quorum of nodes is online. If the cluster loses a majority quorum of nodes , `pcs status` will return `partition WITHOUT quorum` and all resources will be stopped. 

`online: [sqlvmnode1 sqlvmnode2 sqlvmnode3]` returns the name of all nodes currently participating in the cluster. If any nodes are not participating, `pcs status` returns `OFFLINE: [<nodename>]`.

`PCSD Status` shows the cluster status for each node.

### Reasons why a node may be offline

Check the following items when a node is offline.

- **Firewall**

    The following ports need to be open on all nodes for Pacemaker to be able to communicate.
    
    - **TCP: 2224, 3121, 21064

- **Pacemaker or Corosync services running**

- **Node communication**

- **Node name mappings**

## Additional resources

* [Cluster from Scratch](https://clusterlabs.org/doc/Cluster_from_Scratch.pdf) guide from Pacemaker

## Next steps

[Configure Red Hat Enterprise Linux shared disk cluster for SQL Server](sql-server-linux-shared-disk-cluster-red-hat-7-configure.md)

