---
# required metadata

title: Configure Red Hat Enterprise Linux 7.2 shared disk cluster for SQL Server | SQL Server vNext CTP1
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 10-31-2016
ms.topic: article
ms.prod: sql-linux 
ms.service: 
ms.technology: 
ms.assetid: 

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

# Configure Red Hat Enterprise Linux 7.2 shared disk cluster for SQL Server

This guide provides instructions to create a two-node shared disk cluster for SQL Server on Red Hat Enterprise Linux 7.2. The clustering layer is based on Red Hat Enterprise Linux (RHEL) [HA add-on](https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/6/pdf/High_Availability_Add-On_Overview/Red_Hat_Enterprise_Linux-6-High_Availability_Add-On_Overview-en-US.pdf) built on top of [Pacemaker](http://clusterlabs.org/). Corosync and Pacemaker coordinate cluster communications and resource management. The SQL Server instance is active on either one node or the other.

The following diagram illustrates the components in a Linux cluster with SQL Server. 

![Red Hat Enterprise Linux 7 Shared Disk SQL Cluster](./media/sql-server-linux-shared-disk-cluster-red-hat-7-configure/LinuxCluster.png) 

For more details on cluster configuration, resource agents options, and management, visit [RHEL reference documentation](http://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/7/html/High_Availability_Add-On_Reference/index.html).

> Access to Red Hat documentation may require a subscription. 

The following sections walk through the steps to set up a failover cluster solution for demonstration purposes. 

## Setup and configure the operating system on each cluster node

The first step is to configure the operating system on the cluster nodes. For this walk through, use RHEL 7.2 with a valid subscription for the HA add-on. 

## Install and configure SQL Server on each cluster node

1. Install and setup SQL Server on both nodes. 

   To install SQL Server on RHEL, run the following commands: 

   ```
   $ sudo su 
   # yum install wget
   # curl -O https://private-repo.microsoft.com/tools/configure-mssql-repo-2.sh 
   # chmod a+x configure-mssql-repo-2.sh 
   # ./configure-mssql-repo-2.sh  <--!URL Provided in Email.-->
   # exit 

   $ sudo yum update 
   $ sudo yum install -y mssql-server 
   $ cd /opt/mssql/bin 
   $ sudo ./sqlservr-setup 
   $ sudo firewall-cmd --zone=public --add-port=1433/tcp--permanent 
   $ sudo firewall-cmd --reload 
   ```

   For detailed instructions see [Install SQL Server on Linux](sql-server-linux-setup.md).



1. Configure the hosts file for each cluster node. On each node, the host file must include the IP address and name of every cluster node. 

    Check the IP address for each node. The following script shows the IP address of your current node. 

    ```bash
    # ip addr show
    ```

    Give each node a unique name that is 15 characters or less. By default in Red Hat Linux the computer name is `localhost.localdomain`. This default name may not be unique and is too long. Set the computer name on each node. Set the computer name by adding it to `/etc/hosts`. The following script lets you edit `/etc/hosts` with `vi`. 

    ```
    # vi /etc/hosts
    ```

    The following example shows `/etc/hosts` with additions for two nodes named `sqlfcivm1` and `sqlfcivm2`.

    ```
    127.0.0.1   localhost localhost4 localhost4.localdomain4
    ::1         localhost localhost6 localhost6.localdomain6
    10.128.18.128 fcivm1
    10.128.16.77 fcivm2
    ```

At this point, SQL Server should be stopped on both nodes. On one node, you have copied the SQL Server database files to a temporary directory and deleted the files from the original directory. The next step is to configure shared storage. 

## Configure shared storage and move database files 

To configure shared storage, you need to create a network share and mount it to the database file path on both nodes. In the following steps, you will move the SQL Server database files, install `cifs-utils`, configure the credentials for the share, mount the share, and move the SQL Server database files to the newly mounted share. To complete these steps, chose one node as the primary node. This node is only the primary node for the purpose of configuration. After the cluster service configuration is complete, either node can host the SQL Server service. 

1.   **On the primary node only**, save the database files to a temporary location. 

    > [AZURE.NOTE] The database files contain the login information for the “sa” user.  We will later copy them to the share so that a SQL server instance running on any node in the cluster can access them.

    The following script creates a new temporary directory, copies the database files to the new directory, and removes the old database files. 

    ```bash
    # mkdir /var/opt/mssql/tmp
    # cp /var/opt/mssql/data/* /var/opt/mssql/tmp
    # rm /var/opt/mssql/data/*
    ```
 
2.  Install `cifs-utils` on both nodes. The following command installs `cifs-utils`.

    ```
    # sudo yum install cifs-utils
    ```

3.  **On the primary node only**, save the database files to a temporary location. 

    > [AZURE.NOTE] The database files contain the login information for the “sa” user.  We will later copy them to the share so that a SQL server instance running on any node in the cluster can access them.

    The following script, creates a new temporary directory, copies the database files to the new directory, and removes the old database files. 

    ```bash
    # mkdir /var/opt/mssql/tmp
    # cp /var/opt/mssql/data/* /var/opt/mssql/tmp
    # rm /var/opt/mssql/data/*
    ```

5.  Create a file that contains credentials for mounting the share on both nodes. The file needs to identify the username, password and domain as follows:

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

1. Configure the operating system to mount the shared file. In the following line, update `//<storage server>/<share>` with the name of the file server and the shared disk. Update `<file>` with the name of the credential file. Update `<mssql uid>` with the SQL Server User ID, and `<gid>` with the Group ID. Update the following line and append it to `/etc/fstab` to instruct the operating system where and how to mount the file for SQL Server:

    ```bash
    //<storage server>/<share> /var/opt/mssql/data  cifs  credentials=<file>, uid=<mssql uid>, gid=<mssql gid> 0  0
    ```
    
    For example, the following line adds the `\\StorageServer\SQL` share to the `/var/opt/mssql/data` with credentials for Linux cluster file with the SQL Server UID and gid. 

    ```bash
    //machine/share /var/opt/mssql/data cifs credentials=/.cifscredfile,uid=995,gid=996 0  0
    ```

    If the `/etc/fstab` file was edited correctly, the share is mounted to`/var/opt/mssql/data` and will be automatically re-mounted when the node restarts.

7.  Copy the database and log files that you saved to `/var/opt/mssql/tmp` to the newly mounted share `/var/opt/mssql/data`. This only needs to be done **on the primary node**.
 
8.  Validate that SQL Server starts successfully with the new file path. Do this on each node. At this point only one node should run SQL Server at a time. They cannot both run at the same time because they will both try to access the data files simultaneously.  The following commands start SQL Server, check the status, and then stop SQL Server.
 
    ```
    # systemctl start mssql-server
    # systemctl status mssql-server
    # systemctl stop mssql-server
    ```
 
At this point both instances of SQL Server are configured to run with the database files on the shared storage. The next step is to configure SQL Server for Pacemaker. 

## Install and configure Pacemaker on each cluster node


1. Create a SQL server login for Pacemaker and grant the login permission to run `sp_server_diagnostics`. Pacemaker will use this account to verify which node is running SQL Server. 

    On the primary node, start SQL Server.

    ```bash
    # systemctl start mssql-server
    ```

    On the node that is running, connect to the SQL Server `master` database with the sa account and run the following:

    ```sql
    USE [master]
    GO
    CREATE LOGIN [<loginName>] with PASSWORD= N'<loginPassword>'

    GRANT VIEW SERVER STATE TO <loginName>
    ```

2. On both cluster nodes, create a file to store the SQL Server username and password for the Pacemaker login. The following command creates and populates this file:

    ```bash
    # touch /var/opt/mssql/passwd
    # echo "<loginName>" >> /var/opt/mssql/secrets/passwd
    # echo "<loginPassword>" >> /var/opt/mssql/secrets/passwd
    # chown root:root /var/opt/mssql/passwd
    # chmod 600 /var/opt/mssql/passwd
    ```

3. On both cluster nodes, open the Pacemaker firewall ports. To open these ports with `firewalld`, run the following command:

    ```bash
    # firewall-cmd --permanent --add-service=high-availability
    # firewall-cmd --reload
    ```

    > If you’re using another firewall that doesn’t have a built-in high-availability configuration, the following ports need to be opened for Pacemaker to be able to communicate with other nodes in the cluster
    >
    > * TCP: Ports 2224, 3121, 21064
    > * UDP: Port 5405

1. Install Pacemaker packages on each node.

    ```bash
    # yum install pacemaker pcs fence-agents-all resource-agents
    ```

   ​

2. Set the password for for the default user that is created when installing Pacemaker and Corosync packages. Use the same password for on both nodes. 

    ```bash
    # passwd hacluster
    ```

   ​

3. Enable and start `pcsd` service and Pacemaker. This will allow nodes to rejoin the cluster after the reboot. Run the following command on both nodes.

    ```bash
    # systemctl enable pcsd
    # systemctl start pcsd
    # systemctl enable pacemaker
   ```

4. Install the FCI resource agent for SQL Server. Run the following commands on both nodes. 

    ```bash
    # yum install mssql-server-ha
    ```

## Create the cluster 

1. One one of the nodes, create the cluster.

    ```bash
    # pcs cluster auth <nodeName1 nodeName2 …> -u hacluster
    # pcs cluster setup --name <clusterName> <nodeName1 nodeName2 …>
    # pcs cluster start --all
    ```

    > RHEL HA add-on has fencing agents for VMWare and KVM. Fencing needs to be disabled on all other hypervisors. Disabling fencing agents is not recommended in production environments. As of CTP1 timeframe, there are no fencing agents for HyperV or cloud environments. If you are running one of these configurations, you need to disable fencing. \**This is NOT recommended in a production system!**

    The following command disables the fencing agents.

    ```bash
    # pcs property setstonith-enabled=false
    # pcs property setstart-failure-is-fatal=false
    ```

1. Stop and disable SQL Server on each node with the following commands. 

    ```bash
    # systemctl stop mssql-server
    # systemctl disable mssql-server
    ```

2. Configure the cluster resources for SQL Server and virtual IP resources and push the configuration to the cluster. You will need the following information:

    - **SQL Server Resource Name**: A name for the clustered SQL Server resource. 
    - **Timeout Value**: The timeout value is the amount of time that the cluster waits while a a resource is brought online. For SQL Server, this is the time that you expect SQL Server to take to bring the `master` database online.  
    - **Floating IP Resource Name**: A name for the IP address.
    - **IP Address**: THe IP address that clients will use to connect to the clustered instance of SQL Server.  

   Update the values from the script below for your environment. Run on one node to configure and start the clustered service.  

   ```bash
   # pcs cluster cib cfg 
   # pcs -f cfg resource create <sqlServerResourceName> ocf:sql:fci timeout=<timeout_in_seconds>
   # pcs -f cfg resource create <floatingIPResourceName> ocf:heartbeat:IPAddr2 ip=<ipAddress>
   # pcs -f cfg constraint colocation add <sqlResourceName> <virtualIPResourceName>
   # pcs cluster cib-push cfg
   ```

   For example, the following script creates a SQL Server clustered resource named `MyAppSQL`, and a floating IP resources with IP address `10.0.0.99`. It also starts the failover cluster instance on one node of the cluster. 

   ```bash
   # pcs cluster cib cfg
   # pcs -f cfg resource create MyAppSQL ocf:sql:fci timeout=60s
   # pcs -f cfg resource create virtualip ocf:heartbeat:IPAddr2 ip=10.0.0.99
   # pcs -f cfg constraint colocation add mssql virtualip
   # pcs cluster cib-push cfg
   ```

    After the configuration is pushed, SQL Server will start on one node. 

3. Verify that SQL Server is started. 

   ```bash
   # pcs status 
   ```

   The following examples shows the results when Pacemaker has succesfully started a clustered instance of SQL Server. 

   ```
   virtualip      (ocf::heartbeat:IPaddr2):       Started vm1
    mssql  (ocf::sql:fci): Started vm1sss
    
   PCSD Status:
     slqfcivm1: Online
     sqlfcivm2: Online
    
   Daemon Status:
     corosync: active/disabled
     pacemaker: active/enabled
     pcsd: active/enabled
   ```

## Troubleshooting shared disk cluster 

It may help troubleshoot the cluster to understand the three daemons work together cluster resources.

| Daemon | Description 
| ----- | -----
| Corosync | Provides quorum membership and messaging between cluster nodes.
| Pacemaker | Resides on top of Corosync and provides state machines for resources. 
| PCSD | Manages both Pacemaker and Corosync through the `pcs` tools

PCSD must be running in order to use `pcs`. 

### Current cluster status 

`# pcs status` returns basic information about the cluster, quorum, nodes, resources, and daemon status for each node. 

An example of a healthy pacemaker quorum output would be:

```
Cluster name: MyAppSQL 
Last updated: Wed Oct 31 12:00:00 2016  Last change: Wed Oct 31 11:00:00 2016 by root via crm_resource on sqlvmnode1 
Stack: corosync 
Current DC: sqlvmnode1  (version 1.1.13-10.el7_2.4-44eb2dd) - partition with quorum 
3 nodes and 1 resource configured 

Online: [ sqlvmnode1 sqlvmnode2 sqlvmnode3] 

Full list of resources: 

mssql (ocf::sql:fci): Started sqlvmnode1 

PCSD Status: 
sqlvmnode1: Online 
sqlvmnode2: Online 
sqlvmnode3: Online 

Daemon Status: 
corosync: active/disabled 
pacemaker: active/enabled 
```

In the example, `partition with quorum` means that a majority quorum of nodes is online. If the cluster loses a majority quorum of nodes , `pcs status` will return `partition WITHOUT quorum` and all resources will be stopped. 

`online: [sqlvmnode1 sqlvmnode2 sqlvmnode3]` returns the name of all nodes currently participating in the cluster. If any nodes are not participating, `pcs status` returns `OFFLINE: [*nodename*]`.

`PCSD Status` shows the cluster status for each node.

### Reasons why a node may be offline

Check the following items when a node is offline.

- **Firewall**

    The following ports need to be open on all nodes for Pacemaker to be able to communicate.
    
    - **TCP: 2224, 3121, 21064

- **Pacemaker or corosync services running**

- **Node communication**

- **Node name mappings**


## Next steps

[Operate SQL Server on Red Hat Enterprise Linux 7 shared disk cluster](sql-server-linux-shared-disk-cluster-red-hat-7-operate.md)

