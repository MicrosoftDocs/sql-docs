---
# required metadata

title: Configure RHEL Cluster for SQL Server Availability Group | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 06/14/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: b7102919-878b-4c08-a8c3-8500b7b42397

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

# Configure RHEL Cluster for SQL Server Availability Group

This document explains how to create a three-node availability group cluster for SQL Server on Red Hat Enterprise Linux. For high availability, an availability group on Linux requires three nodes - see [High availability and data protection for availability group configurations](sql-server-linux-availability-group-ha.md). The clustering layer is based on Red Hat Enterprise Linux (RHEL) [HA add-on](https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/6/pdf/High_Availability_Add-On_Overview/Red_Hat_Enterprise_Linux-6-High_Availability_Add-On_Overview-en-US.pdf) built on top of [Pacemaker](http://clusterlabs.org/). 

> [!NOTE] 
> Access to Red Hat full documentation requires a valid subscription. 

For more details on cluster configuration, resource agents options, and management, visit [RHEL reference documentation](http://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/7/html/High_Availability_Add-On_Reference/index.html).

> [!NOTE] 
> SQL Server is not as tightly integrated with Pacemaker on Linux as it is with Windows Server failover clustering. A SQL Server instance is not aware of the cluster. Pacemaker provides cluster resource orchestration. Also, the virtual network name is specific to Windows Server failover clustering - there is no equivalent in Pacemaker. Availability group dynamic management views (DMVs) that query cluster information return empty rows on Pacemaker clusters. To create a listener for transparent reconnection after failover, manually register the listener name in DNS with the IP used to create the virtual IP resource. 

The following sections walk through the steps to set up a Pacemaker cluster and add an availability group as resource in the cluster for high availability.

## Roadmap

The steps to create an availability group on Linux servers for high availability are different from the steps on a Windows Server failover cluster. The following list describes the high-level steps: 

1. [Configure SQL Server on the cluster nodes](sql-server-linux-setup.md).

2. [Create the availability group](sql-server-linux-availability-group-configure-ha.md). 

3. Configure a cluster resource manager, like Pacemaker. These instructions are in this document.
   
   The way to configure a cluster resource manager depends on the specific Linux distribution. 

   >[!IMPORTANT]
   >Production environments require a fencing agent, like STONITH for high availability. The demonstrations in this documentation do not use fencing agents. The demonstrations are for testing and validation only. 
   
   >A Linux cluster uses fencing to return the cluster to a known state. The way to configure fencing depends on the distribution and the environment. Currently, fencing is not available in some cloud environments. For more information, see [Support Policies for RHEL High Availability Clusters - Virtualization Platforms](https://access.redhat.com/articles/29440).

5. [Add the availability group as a resource in the cluster](sql-server-linux-availability-group-cluster-rhel.md#create-availability-group-resource).  

## Configure high availability for RHEL

To configure high availability for RHEL, enable the high availability subscription and then configure Pacemaker.

### Enable the high availability subscription for RHEL

Each node in the cluster must have an appropriate subscription for RHEL and the High Availability Add on. Review the requirements at [How to install High Availability cluster packages in Red Hat Enterprise Linux](http://access.redhat.com/solutions/45930). Follow these steps to configure the subscription and repos:

1. Register the system.

   ```bash
   #sudo subscription-manager register
   ```

   Provide your user name and password.   

1. List the available pools for registration.

   ```bash
   #sudo subscription-manager list --available
   ```

   From the list of available pools, note the pool ID for the high availability subscription.

1. Update the following script. Replace `<pool id>` with the pool ID for high availability from the preceding step. Run the script to attach the subscription.

   ```bash
   #sudo subscription-manager attach --pool=<pool id>
   ```

1. Enable the repository.

   ```bash
   #sudo subscription-manager repos --enable=rhel-ha-for-rhel-7-server-rpms
   ```

For more information, see [Pacemaker – The Open Source, High Availability Cluster](http://www.opensourcerers.org/pacemaker-the-open-source-high-availability-cluster/). 

After you have configured the subscription, complete the following steps to configure Pacemaker:

### Configure Pacemaker

After you register the subscription, complete the following steps to configure Pacemaker:
   
[!INCLUDE [RHEL-Configure-Pacemaker](../includes/ss-linux-cluster-pacemaker-configure-rhel.md)]

After Pacemaker is configured, use `pcs` to interact with the cluster. Execute all commands on one node from the cluster. 

## Configure fencing (STONITH)

Pacemaker cluster vendors require STONITH to be enabled and a fencing device configured for a supported cluster setup. STONITH stands for "shoot the other node in the head." When the cluster resource manager cannot determine the state of a node or of a resource on a node, fencing brings the cluster to a known state again.

Resource level fencing ensures that there is no data corruption in case of an outage by configuring a resource. For example, you can use resource level fencing to mark the disk on a node as outdated when the communication link goes down. 

Node level fencing ensures that a node does not run any resources. This is done by resetting the node. Pacemaker supports a great variety of fencing devices. Examples include an uninterruptible power supply or management interface cards for servers.

For information about STONITH, and fencing, see the following articles:

* [Pacemaker Clusters from Scratch](http://clusterlabs.org/doc/en-US/Pacemaker/1.1-plugin/html/Clusters_from_Scratch/ch05.html)
* [Fencing and STONITH](http://clusterlabs.org/doc/crm_fencing.html)
* [Red Hat High Availability Add-On with Pacemaker: Fencing](http://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/6/html/Configuring_the_Red_Hat_High_Availability_Add-On_with_Pacemaker/ch-fencing-HAAR.html)

Because the node level fencing configuration depends heavily on your environment, disable it for this tutorial (it can be configured later). The following script disables node level fencing:

```bash
sudo pcs property set stonith-enabled=false
```
  
>[!IMPORTANT]
>Disabling STONITH is just for testing purposes. If you plan to use Pacemaker in a production environment, you should plan a STONITH implementation depending on your environment and keep it enabled. RHEL does not provide fencing agents for any cloud environments (including Azure) or Hyper-V. Consequentially, the cluster vendor does not offer support for running production clusters in these environments. We are working on a solution for this gap that will be available in future releases.

## Set cluster property start-failure-is-fatal to false

`start-failure-is-fatal` indicates whether a failure to start a resource on a node prevents further start attempts on that node. When set to `false`, the cluster decides whether to try starting on the same node again based on the resource's current failure count and migration threshold. After failover occurs, Pacemaker retries starting the availability group resource on the former primary once the SQL instance is available. Pacemaker demotes the replica to secondary and it automatically rejoins the availability group. 

To update the property value to `false` run:

```bash
pcs property set start-failure-is-fatal=false
```

>[!WARNING]
>After an automatic failover, when `start-failure-is-fatal = true` the resource manager will attempt to start the resource. If it fails on the first attempt, manually run `pcs resource cleanup <resourceName>` to clean up the resource failure count and reset the configuration.

For information on Pacemaker cluster properties, see [Pacemaker Clusters Properties](http://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/7/html/High_Availability_Add-On_Reference/ch-clusteropts-HAAR.html).

## Create a SQL Server login for Pacemaker

[!INCLUDE [SQL-Create-SQL-Login](../includes/ss-linux-cluster-pacemaker-create-login.md)]

## Create availability group resource

To create the availability group resource, use `pcs resource create` command and set the resource properties. The following command creates a `ocf:mssql:ag` master/slave type resource for availability group with name `ag1`.

```bash
sudo pcs resource create ag_cluster ocf:mssql:ag ag_name=ag1 --master meta notify=true
```

[!INCLUDE [required-synchronized-secondaries-default](../includes/ss-linux-cluster-required-synchronized-secondaries-default.md)]

<a name="createIP"></a>

## Create virtual IP resource

To create the virtual IP address resource, run the following command on one node. Use an available static IP address from the network. Replace the IP address between `**<10.128.16.240>**` with a valid IP address.

```bash
sudo pcs resource create virtualip ocf:heartbeat:IPaddr2 ip=**<10.128.16.240>**
```

There is no virtual server name equivalent in Pacemaker. To use a connection string that points to a string server name instead of an IP address, register the virtual IP resource address and desired virtual server name in DNS. For DR configurations, register the desired virtual server name and IP address with the DNS servers on both primary and DR site.

## Add colocation constraint

Almost every decision in a Pacemaker cluster, like choosing where a resource should run, is done by comparing scores. Scores are calculated per resource. The cluster resource manager chooses the node with the highest score for a particular resource. If a node has a negative score for a resource, the resource cannot run on that node.

On a pacemaker cluster, you can manipulate the decisions of the cluster with constraints. Constraints have a score. If a constraint has a score lower than `INFINITY`, Pacemaker regards it as recommendation. A score of `INFINITY` is mandatory.

To ensure that primary replica and the virtual ip resources run on the same host, define a colocation constraint with a score of INFINITY. To add the colocation constraint, run the following command on one node.

```bash
sudo pcs constraint colocation add virtualip ag_cluster-master INFINITY with-rsc-role=Master
```

## Add ordering constraint

The colocation constraint has an implicit ordering constraint. It moves the virtual IP resource before it moves the availability group resource. By default the sequence of events is:

1. User issues `pcs resource move` to the availability group primary from node1 to node2.
1. The virtual IP resource stops on node 1.
1. The virtual IP resource starts on node 2.
  
   >[!NOTE]
   >At this point, the IP address temporarily points to node 2 while node 2 is still a pre-failover secondary. 
   
1. The availability group primary on node 1 is demoted to secondary.
1. The availability group secondary on node 2 is promoted to primary. 

To prevent the IP address from temporarily pointing to the node with the pre-failover secondary, add an ordering constraint. 

To add an ordering constraint, run the following command on one node:

```bash
sudo pcs constraint order promote ag_cluster-master then start virtualip
```

>[!IMPORTANT]
>After you configure the cluster and add the availability group as a cluster resource, you cannot use Transact-SQL to fail over the availability group resources. SQL Server cluster resources on Linux are not coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). SQL Server service is not aware of the presence of the cluster. All orchestration is done through the cluster management tools. In RHEL or Ubuntu use `pcs` and in SLES use `crm` tools. 

Manually fail over the availability group with `pcs`. Do not initiate failover with Transact-SQL. For instructions, see [Failover](sql-server-linux-availability-group-failover-ha.md#failover).

<!---[!INCLUDE [Pacemaker Concepts](..\includes\ss-linux-cluster-pacemaker-concepts.md)]--->

>additional line

## Next steps

[Operate HA availability group](sql-server-linux-availability-group-failover-ha.md)
