---
title: Configure SLES Cluster for SQL Server Availability Group | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 05/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 85180155-6726-4f42-ba57-200bf1e15f4d
---
# Configure SLES Cluster for SQL Server Availability Group

[!INCLUDE[tsql-appliesto-sslinx-only_md](../../docs/includes/tsql-appliesto-sslinx-only_md.md)]

This guide provides instructions to create a three-node cluster for SQL Server on SUSE Linux Enterprise Server (SLES) 12 SP2. For high availability, an availability group on Linux requires three nodes - see [High availability and data protection for availability group configurations](sql-server-linux-availability-group-ha.md). The clustering layer is based on SUSE [High Availability Extension (HAE)](https://www.suse.com/products/highavailability) built on top of [Pacemaker](http://clusterlabs.org/). 

For more details on cluster configuration, resource agent options, management, best practices, and recommendations, see [SUSE Linux Enterprise High Availability Extension 12 SP2](https://www.suse.com/documentation/sle-ha-12/index.html).

>[!NOTE]
>At this point, SQL Server's integration with Pacemaker on Linux is not as coupled as with WSFC on Windows. SQL Server service on Linux is not cluster aware. Pacemaker controls all of the orchestration of the cluster resources, including the availability group resource. On Linux, you should not rely on Always On Availability Group Dynamic Management Views (DMVs) that provide cluster information like sys.dm_hadr_cluster. Also, virtual network name is specific to WSFC, there is no equivalent of the same in Pacemaker. You can still create a listener to use it for transparent reconnection after failover, but you will have to manually register the listener name in the  DNS server with the IP used to create the virtual IP resource (as explained below).


## Roadmap

The steps to create an availability group on Linux servers for high availability are different from the steps on a Windows Server failover cluster. The following list describes the high level steps: 

1. [Configure SQL Server on the cluster nodes](sql-server-linux-setup.md).

2. [Create the availability group](sql-server-linux-availability-group-failover-ha.md). 

3. Configure a cluster resource manager, like Pacemaker. These instructions are in this document.
   
   The way to configure a cluster resource manager depends on the specific Linux distribution. 

   >[!IMPORTANT]
   >Production environments require a fencing agent, like STONITH for high availability. The demonstrations in this documentation do not use fencing agents. The demonstrations are for testing and validation only. 
   
   >A Pacemaker cluster uses fencing to return the cluster to a known state. The way to configure fencing depends on the distribution and the environment. At this time, fencing is not available in some cloud environments. See [SUSE Linux Enterprise High Availability Extension](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#cha.ha.fencing).

5. [Add the availability group as a resource in the cluster](sql-server-linux-availability-group-cluster-sles.md#configure-the-cluster-resources-for-sql-server). 

## Prerequisites

To complete the end-to-end scenario below you need three machines to deploy the three nodes cluster. The steps below outline how to configure these servers.

## Setup and configure the operating system on each cluster node 

The first step is to configure the operating system on the cluster nodes. For this walk through, use SLES 12 SP2 with a valid subscription for the HA add-on.

### Install and configure SQL Server service on each cluster node

1. Install and setup SQL Server service on all nodes. For detailed instructions see [Install SQL Server on Linux](sql-server-linux-setup.md).

1. Designate one node as primary and other nodes as secondaries. Use these terms throughout this guide.

1. Make sure nodes that are going to be part of the cluster can communicate to each other.

   The following example shows `/etc/hosts` with additions for three nodes named SLES1, SLES2 and SLES3.

   ```
   127.0.0.1   localhost
   10.128.16.33 SLES1
   10.128.16.77 SLES2
   10.128.16.22 SLES3
   ```

   All cluster nodes must be able to access each other via SSH. Tools like `hb_report` or `crm_report` (for troubleshooting) and Hawk's History Explorer require passwordless SSH access between the nodes, otherwise they can only collect data from the current node. In case you use a non-standard SSH port, use the -X option (see `man` page). For example, if your SSH port is 3479, invoke a `crm_report` with:

   ```bash
   sudo crm_report -X "-p 3479" [...]
   ```

   For additional information, see the [SLES Administration Guide - Miscellaneous section](http://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#sec.ha.troubleshooting.misc).


## Create a SQL Server login for Pacemaker

[!INCLUDE [SLES-Create-SQL-Login](../includes/ss-linux-cluster-pacemaker-create-login.md)]

## Configure an Always On Availability Group

On Linux servers configure the availability group and then configure the cluster resources. To configure the availability group, see [Configure Always On Availability Group for SQL Server on Linux](sql-server-linux-availability-group-configure-ha.md)

## Install and configure Pacemaker on each cluster node

1. Install the High Availability extension

   For reference, see [Installing SUSE Linux Enterprise Server and High Availability Extension](https://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html#sec.ha.inst.quick.installation)

1. Install SQL Server resource agent package on both nodes.

   ```bash
   sudo zypper install mssql-server-ha
   ```

## Set up the first node

   Refer to [SLES installation instructions](http://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html#sec.ha.inst.quick.setup.1st-node)

1. Log in as `root` to the physical or virtual machine you want to use as cluster node.
2. Start the bootstrap script by executing:
   ```bash
   sudo ha-cluster-init
   ```

   If NTP has not been configured to start at boot time, a message appears. 

   If you decide to continue anyway, the script will automatically generate keys for SSH access and for the Csync2 synchronization tool and start the services needed for both. 

3. To configure the cluster communication layer (Corosync): 

   a. Enter a network address to bind to. By default, the script will propose the network address of eth0. Alternatively, enter a different network address, for example the address of bond0. 

   b. Enter a multicast address. The script proposes a random address that you can use as default. 

   c. Enter a multicast port. The script proposes 5405 as default. 

   d. To configure `SBD ()`, enter a persistent path to the partition of your block device that you want to use for SBD. The path must be consistent across all nodes in the cluster. 
   Finally, the script will start the Pacemaker service to bring the one-node cluster online and enable the Web management interface Hawk2. The URL to use for Hawk2 is displayed on the screen. 

4. For any details of the setup process, check `/var/log/sleha-bootstrap.log`. You now have a running one-node cluster. Check the cluster status with crm status:

   ```bash
   sudo crm status
   ```

   You can also see cluster configuration with `crm configure show xml`  or `crm configure show`.

5. The bootstrap procedure creates a Linux user named hacluster with the password linux. Replace the default password with a secure one as soon as possible: 

   ```bash
   sudo passwd hacluster
   ```

## Add nodes to the existing cluster

If you have a cluster running with one or more nodes, add more cluster nodes with the ha-cluster-join bootstrap script. The script only needs access to an existing cluster node and will complete the basic setup on the current machine automatically. Follow the steps below:

If you have configured the existing cluster nodes with the `YaST` cluster module, make sure the following prerequisites are fulfilled before you run `ha-cluster-join`:
- The root user on the existing nodes has SSH keys in place for passwordless login. 
- `Csync2` is configured on the existing nodes. For details, refer to Configuring Csync2 with YaST. 

1. Log in as root to the physical or virtual machine supposed to join the cluster. 
2. Start the bootstrap script by executing: 

   ```bash
   sudo ha-cluster-join
   ```

   If NTP has not been configured to start at boot time, a message appears. 

3. If you decide to continue anyway, you will be prompted for the IP address of an existing node. Enter the IP address. 

4. If you have not already configured a passwordless SSH access between both machines, you will also be prompted for the root password of the existing node. 

   After logging in to the specified node, the script will copy the Corosync configuration, configure SSH and `Csync2`, and will bring the current machine online as new cluster node. Apart from that, it will start the service needed for Hawk. If you have configured shared storage with `OCFS2`, it will also automatically create the mountpoint directory for the `OCFS2` file system. 

5. Repeat the steps above for all machines you want to add to the cluster. 

6. For details of the process, check `/var/log/ha-cluster-bootstrap.log`. 

1. Check the cluster status with `sudo crm status`. If you have successfully added a second node, the output will be similar to the following:

   ```bash
   sudo crm status
   
   3 nodes configured
   1 resource configured
   Online: [ SLES1 SLES2 SLES3]
   Full list of resources:   
   admin_addr     (ocf::heartbeat:IPaddr2):       Started node1
   ```

   >[!NOTE]
   >`admin_addr` is the virtual IP cluster resource which is configured during initial one-node cluster setup.

After adding all nodes, check if you need to adjust the no-quorum-policy in the global cluster options. This is especially important for two-node clusters. For more information, refer to Section 4.1.2, Option no-quorum-policy. 

## Set cluster property start-failure-is-fatal to false

`Start-failure-is-fatal` indicates whether a failure to start a resource on a node prevents further start attempts on that node. When set to `false`, the cluster will decide whether to try starting on the same node again based on the resource's current failure count and migration threshold. So, after failover occurs, Pacemaker will retry starting the availability group resource on the former primary once the SQL instance is available. Pacemaker will take care of demoting the replica to secondary and it will automatically rejoin the availability group. Also, if `start-failure-is-fatal` is set to `false`, the cluster will fall back to the configured failcount limits configured with migration-threshold so you need to make sure default for migration threshold is updated accordingly.

To update the property value to false run:
```bash
sudo crm configure property start-failure-is-fatal=false
sudo crm configure rsc_defaults migration-threshold=5000
```
If the property has the default value of `true`, if first attempt to start the resource fails, user intervention is required after an automatic failover to cleanup the resource failure count and reset the configuration using: `sudo crm resource cleanup <resourceName>` command.

For more details on Pacemaker cluster properties see [Configuring Cluster Resources](https://www.suse.com/documentation/sle_ha/book_sleha/data/sec_ha_config_crm_resources.html).

# Configure fencing (STONITH)
Pacemaker cluster vendors require STONITH to be enabled and a fencing device configured for a supported cluster setup. When the cluster resource manager cannot determine the state of a node or of a resource on a node, fencing is used to bring the cluster to a known state again.
Resource level fencing ensures mainly that there is no data corruption in case of an outage by configuring a resource. You can use resource level fencing, for instance, with DRBD (Distributed Replicated Block Device) to mark the disk on a node as outdated when the communication link goes down.
Node level fencing ensures that a node does not run any resources. This is done by resetting the node and the Pacemaker implementation of it is called STONITH (which stands for "shoot the other node in the head"). Pacemaker supports a great variety of fencing devices, e.g. an uninterruptible power supply or management interface cards for servers.
For more details, see [Pacemaker Clusters from Scratch](http://clusterlabs.org/doc/en-US/Pacemaker/1.1-plugin/html/Clusters_from_Scratch/ch05.html), [Fencing and Stonith](http://clusterlabs.org/doc/crm_fencing.html) and [SUSE HA documentation: Fencing and STONITH](https://www.suse.com/documentation/sle_ha/book_sleha/data/cha_ha_fencing.html).

At cluster initialization time, STONITH is disabled if no configuration is detected. It can be enabled later by running below command

```bash
sudo crm configure property stonith-enabled=true
```
  
>[!IMPORTANT]
>Disabling STONITH is just for testing purposes. If you plan to use Pacemaker in a production environment, you should plan a STONITH implementation depending on your environment and keep it enabled. Note that SUSE does not provide fencing agents for any cloud environments (including Azure) or Hyper-V. Consequentially, the cluster vendor does not offer support for running production clusters in these environments. We are working on a solution for this gap that will be available in future releases.


## Configure the cluster resources for SQL Server

Refer to [SLES Administration Guid](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#cha.ha.manual_config)

### Create availability group resource

The following command creates and configures the availability group resource for 3 replicas of availability group [ag1]. The monitor operations and timeouts have to be specified explicitly in SLES based on the fact that timeouts are highly workload dependent and need to be carefully adjusted for each deployment.
Run the command on one of the nodes in the cluster:

1. Run `crm configure` to open the crm prompt:

   ```bash
   sudo crm configure 
   ```

1. In the crm prompt, run the command below to configure the resource properties.

   ```bash
primitive ag_cluster \
   ocf:mssql:ag \
   params ag_name="ag1" \
   op start timeout=60s \
   op stop timeout=60s \
   op promote timeout=60s \
   op demote timeout=10s \
   op monitor timeout=60s interval=10s \
   op monitor timeout=60s interval=11s role="Master" \
   op monitor timeout=60s interval=12s role="Slave" \
   op notify timeout=60s
ms ms-ag_cluster ag_cluster \
   meta master-max="1" master-node-max="1" clone-max="3" \
  clone-node-max="1" notify="true" \
commit
   ```

[!INCLUDE [required-synchronized-secondaries-default](../includes/ss-linux-cluster-required-synchronized-secondaries-default.md)]

### Create virtual IP resource

If you did not create the virtual IP resource when you ran `ha-cluster-init` you can create this resource now. The following command creates a virtual IP resource. Replace `<**0.0.0.0**>` with an available address from your network and `<**24**>` with the number of bits in the CIDR subnet mask. Run on one node.

```bash
crm configure \
primitive admin_addr \
   ocf:heartbeat:IPaddr2 \
   params ip=<**0.0.0.0**> \
      cidr_netmask=<**24**>
```

### Add colocation constraint
Almost every decision in a Pacemaker cluster, like choosing where a resource should run, is done by comparing scores. Scores are calculated per resource, and the cluster resource manager chooses the node with the highest score for a particular resource. (If a node has a negative score for a resource, the resource cannot run on that node.) We can manipulate the decisions of the cluster with constraints. Constraints have a score. If a constraint has a score lower than INFINITY, it is only a recommendation. A score of INFINITY means it is a must. We want to ensure that primary of the availability group and the virtual ip resource are run on the same host, so we will define a colocation constraint with a score of INFINITY. 

To set colocation constraint for the virtual IP to run on same node as the master, run the following command on one node:

```bash
crm configure
colocation vip_on_master inf: \
    admin_addr ms-ag_cluster:Master
commit
```

### Add ordering constraint
The colocation constraint has an implicit ordering constraint. It moves the virtual IP resource before it moves the availability group resource. By default the sequence of events is: 

1. User issues resource migrate to the availability group master from node1 to node2.
2. The virtual IP resource stops on node 1.
3. The virtual IP resource starts on node 2. At this point, the IP address temporarily points to node 2 while node 2 is still a pre-failover secondary. 
4. The availability group master on node 1 is demoted to slave.
5. The availability group slave on node 2 is promoted to master. 

To prevent the IP address from temporarily pointing to the node with the pre-failover secondary, add an ordering constraint. 
To add an ordering constraint, run the following command on one node: 

```bash
crm crm configure \
   order ag_first inf: ms-ag_cluster:promote admin_addr:start
```


>[!IMPORTANT]
>After you configure the cluster and add the availability group as a cluster resource, you cannot use Transact-SQL to fail over the availability group resources. SQL Server cluster resources on Linux are not coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). SQL Server service is not aware of the presence of the cluster. All orchestration is done through the cluster management tools. In SLES use `crm`. 

Manually fail over the availability group with `crm`. Do not initiate failover with Transact-SQL. For instructions, see [Failover](sql-server-linux-availability-group-failover-ha.md#failover).


For additional details see:
- [Managing cluster resources](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#sec.ha.config.crm).   
- [HA Concepts](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#cha.ha.concepts)
- [Pacemaker Quick Reference](https://github.com/ClusterLabs/pacemaker/blob/master/doc/pcs-crmsh-quick-ref.md) 

<!---[!INCLUDE [Pacemaker Concepts](..\includes\ss-linux-cluster-pacemaker-concepts.md)]--->

## Next steps

[Operate HA availability group](sql-server-linux-availability-group-failover-ha.md)