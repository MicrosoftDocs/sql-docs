---
# required metadata

title: Configure RHEL Cluster for SQL Server Availability Group | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 03/17/2017
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

This document explains how to create a two-node availability group cluster for SQL Server on Red Hat Enterprise Linux. The clustering layer is based on Red Hat Enterprise Linux (RHEL) [HA add-on](https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/6/pdf/High_Availability_Add-On_Overview/Red_Hat_Enterprise_Linux-6-High_Availability_Add-On_Overview-en-US.pdf) built on top of [Pacemaker](http://clusterlabs.org/). 

> [!NOTE] 
> Access to Red Hat documentation requires a subscription. 

For more details on cluster configuration, resource agents options, and management, visit [RHEL reference documentation](http://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/7/html/High_Availability_Add-On_Reference/index.html).

> [!NOTE] 
> At this point, SQL Server service's integration with Pacemaker on Linux is not as coupled as with WSFC on Windows. From within SQL, there is no knowledge about the presence of the cluster, all orchestration is outside in and the service is controlled as a standalone instance by Pacemaker. Also, virtual network name is specific to WSFC, there is no equivalent of the same in Pacemaker. It is expected Always On dmvs that query cluster information to return empty rows. You can still create a listener to use it for transparent reconnection after failover, but you will have to manually register the listener name in the  DNS server with the IP used to create the virtual IP resource (as explained below).

The following sections walk through the steps to set up a failover cluster solution. 

## Configure Pacemaker for RHEL

[!INCLUDE [RHEL-Configure-Pacemaker](../includes/ss-linux-cluster-pacemaker-configure-rhel.md)]

From now on, we will interact with the cluster via `pcs` cluster management tools, so all commands need to be executed only on one host that is a node in the cluster, it does not matter which one.

## Configure fencing (STONITH)
Pacemaker cluster vendors require STONITH to be enabled and a fencing device configured for a supported cluster setup. When the cluster resource manager cannot determine the state of a node or of a resource on a node, fencing is used to bring the cluster to a known state again.
Resource level fencing ensures mainly that there is no data corruption in case of an outage by configuring a resource. You can use resource level fencing, for instance, with DRBD (Distributed Replicated Block Device) to mark the disk on a node as outdated when the communication link goes down.
Node level fencing ensures that a node does not run any resources. This is done by resetting the node and the Pacemaker implementation of it is called STONITH (which stands for "shoot the other node in the head"). Pacemaker supports a great variety of fencing devices, e.g. an uninterruptible power supply or management interface cards for servers.
For more details, see [Pacemaker Clusters from Scratch](http://clusterlabs.org/doc/en-US/Pacemaker/1.1-plugin/html/Clusters_from_Scratch/ch05.html), [Fencing and Stonith](http://clusterlabs.org/doc/crm_fencing.html) and [Red Hat High Availability Add-On with Pacemaker: Fencing](http://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/6/html/Configuring_the_Red_Hat_High_Availability_Add-On_with_Pacemaker/ch-fencing-HAAR.html).

Because the node level fencing configuration depends heavily on your environment, we will disable it for this tutorial (it can be configured at a later time):

```bash
sudo pcs property set stonith-enabled=false
```
  
>[!IMPORTANT]
>Disabling STONITH is just for testing purposes. If you plan to use Pacemaker in a production environment, you should plan a STONITH implementation depending on your environment and keep it enabled.

## Create a SQL Server login for Pacemaker

[!INCLUDE [SQL-Create-SQL-Login](../includes/ss-linux-cluster-pacemaker-create-login.md)]

## Create availability group resource

To create the availability group resource, use `pcs resource create` command and set the resource properties. Below command creates a `ocf:mssql:ag` master/slave type resource for availability group with name `ag1`.

```bash
sudo pcs resource create ag_cluster ocf:mssql:ag ag_name=ag1 --master meta notify=true
```

## Create virtual IP resource

To create the virtual IP address resource, run the following command on one node. Use an available static IP address from the network. Replace the IP address between `**<...>**'

```bash
sudo pcs resource create virtualip ocf:heartbeat:IPaddr2 ip=**<10.128.16.240>**
```

There is no virtual server name equivalent in Pacemaker. To use a connection string that points to a string server name and not use the IP address, register the virtual IP resource address and desired virtual server name in DNS. For DR configurations, register the desired virtual server name and IP address with the DNS servers on both primary and DR site.

## Add colocation constraint

Almost every decision in a Pacemaker cluster, like choosing where a resource should run, is done by comparing scores. Scores are calculated per resource, and the cluster resource manager chooses the node with the highest score for a particular resource. (If a node has a negative score for a resource, the resource cannot run on that node.)
We can manipulate the decisions of the cluster with constraints. Constraints have a score. If a constraint has a score lower than INFINITY, it is only a recommendation. A score of INFINITY means it is a must.
We want to ensure that primary of the availability group and the virtual ip resource are run on the same host, so we will define a colocation constraint with a score of INFINITY. To add the colocation constraint, run the following command on one node.

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

## Manual failover

>[!IMPORTANT]
>After you configure the cluster and add the availability group as a cluster resource, you cannot use Transact-SQL to fail over the availability group resources. SQL Server cluster resources on Linux are not coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). SQL Server service is not aware of the presence of the cluster. All orchestration is done through the cluster management tools. In RHEL or Ubuntu use `pcs` and in SLES use 'crm' tools. 

>[!IMPORTANT]
>If the availability group is a cluster resource, there is a known issue in current release where manual failover to an asynchronous replica does not work. This will be fixed in the upcoming release. Manual or automatic failover to a synchronous replica will succeed. 

Manually failover the availability group with `pcs`. Do not initiate failover with Transact-SQL.

To manually failover to cluster node2, run the following command.

```bash
sudo pcs resource move ag_cluster-master node2 --master
```

[!INCLUDE [Move-Resource](../includes/ss-linux-cluster-pacemaker-configure-rhel-ubuntu-move-resource.md)]

<a name="sync-commit"></a>
[!INCLUDE [Manage-Sync-Commit](../includes/ss-linux-cluster-availability-group-manage-sync-commit.md)]
