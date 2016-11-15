---
# required metadata

title: Operate Red Hat Enterprise Linux 7.2 shared disk cluster for SQL Server - SQL Server vNext CTP1 | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 11/08/2016
ms.topic: article
ms.prod: sql-linux 
ms.technology: database-engine
ms.assetid: 075ab7d8-8b68-43f3-9303-bbdf00b54db1

# optional metadata
# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---

# Operate Red Hat Enterprise Linux 7.2 shared disk cluster for SQL Server

This document describes how to do the following tasks for SQL Server on a shared disk failover cluster with Red Hat Enterprise Linux 7.0.

- Manually failover the cluster
- Monitor a failover cluster SQL Server service
- Add a cluster node
- remove a cluster node
- Change the SQL Server resource monitoring frequency

## Architecture description

The clustering layer is based on Red Hat Enterprise Linux (RHEL) [HA add-on](https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/6/pdf/High_Availability_Add-On_Overview/Red_Hat_Enterprise_Linux-6-High_Availability_Add-On_Overview-en-US.pdf) built on top of [Pacemaker](http://clusterlabs.org/). Corosync and Pacemaker coordinate cluster communications and resource management. The SQL Server instance is active on either one node or the other.

The following diagram illustrates the components in a Linux cluster with SQL Server. 

![Red Hat Enterprise Linux 7 Shared Disk SQL Cluster](./media/sql-server-linux-shared-disk-cluster-red-hat-7-configure/LinuxCluster.png) 

For more details on cluster configuration, resource agents options, and management, visit [RHEL reference documentation](http://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/7/html/High_Availability_Add-On_Reference/index.html).

## Failover cluster manually

The `resource move` command creates a constraint forcing the resource to remain on the target node.  After executing the `move` command, executing resource `clear` will remove the constraint so it is possible to move the resource again or have the resource automatically fail over. 

```bash
# pcs resource move <sqlResourceName> <targetNodeName>  
# pcs resource clear <sqlResourceName> 
```

The following example moves the **mssql** resource to a node named **vm2**, and then removes the constraint so that the resource can move to a different node later.  

```bash
# pcs resource move mssql vm2 
# pcs resource clear mssql 
```

## Monitor a failover cluster SQL Server service

View the current cluster status:

```bash
# pcs status  
```

View live status of cluster and resources:

```bash
# crm_mon 
```

View the resource agent logs at `/var/log/cluster/corosync.log`

## Add a node to a cluster

1. Check the IP address for each node. The following script shows the IP address of your current node. 

   ```bash
   # ip addr show
   ```

3. The new node needs a unique name that is 15 characters or less. By default in Red Hat Linux the computer name is `localhost.localdomain`. This default name may not be unique and is too long. Set the computer name the new node. Set the computer name by adding it to `/etc/hosts`. The following script lets you edit `/etc/hosts` with `vi`. 

    ```bash
   # vi /etc/hosts
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

1. Install `cifs-utils` on the new node. 

1. Create a file that contains credentials for mounting the share. The file needs to identify the username, password and domain as follows:

    ```bash
    username=<username>
    password=<password>
    domain=<domain>
    ```

    For example, the credential file may contain the following values:

    ```bash
    username=sqlfci
    password=KD(YE8e937!0008x
    domain=CORP
    ```
6.  Get the SQL Server user ID (uid), and group ID (gid). To get the SQL Server uid and gid, run the following command **from the primary node**.

    ```bash
    # id mssql
    ```

1.  On the new node, configure the operating system to mount the shared file. In the following line, update `//<storage server>/<share>` with the name of the file server and the shared disk. Update `<file>` with the name of the credential file. Update `<mssql uid>` with the SQL Server User ID, and `<gid>` with the Group ID. Update the following line and append it to `/etc/fstab` to instruct the operating system where and how to mount the file for SQL Server:

    ```bash
    //<storage server>/<share> /var/opt/mssql/data  cifs  credentials=<file>, uid=<mssql uid>, gid=<mssql gid> 0  0
    ```
    
    For example, the following line adds the `\\StorageServer\SQL` share to the `/var/opt/mssql/data` with credentials for Linux cluster file with the SQL Server UID and gid. 

    ```bash
    //machine/share /var/opt/mssql/data cifs credentials=/.cifscredfile,uid=995,gid=996 0  0
    ```

    If the `/etc/fstab` file was edited correctly, the share is mounted to`/var/opt/mssql/data` and will be automatically re-mounted when the node restarts. 

1. On the new node, create a file to store the SQL Server username and password for the Pacemaker login. The following command creates and populates this file:

   ```bash
   # touch /var/opt/mssql/passwd
   # echo "<loginName>" >> /var/opt/mssql/secrets/passwd
   # echo "<loginPassword>" >> /var/opt/mssql/secrets/passwd
   # chown root:root /var/opt/mssql/passwd
   # chmod 600 /var/opt/mssql/passwd
   ```

3. On the new node, open the Pacemaker firewall ports. To open these ports with `firewalld`, run the following command:

   ```bash
   # firewall-cmd --permanent --add-service=high-availability
   # firewall-cmd --reload
   ```

   > [AZURE.NOTE]If you’re using another firewall that doesn’t have a built-in high-availability configuration, the following ports need to be opened for Pacemaker to be able to communicate with other nodes in the cluster
   >
   > * TCP: Ports 2224, 3121, 21064
   > * UDP: Port 5405

1. Install Pacemaker packages on the new node.

   ```bash
   # yum install pacemaker pcs fence-agents-all resource-agents
   ```
 
2. Set the password for for the default user that is created when installing Pacemaker and Corosync packages. Use the same password as the existing nodes. 

   ```bash
   # passwd hacluster
   ```
 
3. Enable and start `pcsd` service and Pacemaker. This will allow the new node to rejoin the cluster after the reboot. Run the following command on the new node.

   ```bash
   # systemctl enable pcsd
   # systemctl start pcsd
   # systemctl enable pacemaker
   ```

4. Install the FCI resource agent for SQL Server. Run the following commands on the new node. 

   ```bash
   # yum install mssql-server-ha
   ```

1. On an existing node from the cluster, authenticate the new node and add it to the cluster:

    ```bash
    # pcs cluster auth <nodeName3> -u hacluster 
    # pcs cluster node add <nodeName3> 
    ```

    The following example ads a node named **vm3** to the cluster.

    ```bash
    # pcs cluster auth  
    # pcs cluster start 
    ```

## Remove nodes from a cluster

To remove a  node from a cluster run the following command:

```bash
# pcs cluster node remove <nodeName>  
```

## Change the frequency of sqlservr resource monitoring interval

```bash
# pcs resource op monitor interval=<interval>s <sqlResourceName> 
```

The following example sets the monitoring interval to 2 seconds for the mssql resource:

```bash
# pcs resource op monitor interval=2s mssql 
```

## Next Steps

[Configure Red Hat Enterprise Linux 7.0 shared disk cluster for SQL Server](sql-server-linux-shared-disk-cluster-red-hat-7-configure.md)
