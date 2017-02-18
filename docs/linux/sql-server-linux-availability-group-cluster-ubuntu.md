---
# required metadata

title: Configure Ubuntu Cluster for SQL Server Availability Group | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 02/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: dd0d6fb9-df0a-41b9-9f22-9b558b2b2233


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

# Configure Ubuntu Cluster for SQL Server Availability Group

This document explains how to create a two-node availability group cluster for SQL Server on Ubuntu. 

> [!NOTE] 
> At this point, SQL Server's integration with Pacemaker on Linux is not as coupled as with WSFC on Windows. From within SQL, there is no knowledge about the presence of the cluster, all orchestration is outside in and the service is controlled as a standalone instance by Pacemaker. Also, virtual network name is specific to WSFC, there is no equivalent of the same in Pacemaker. It is expected Always On dmvs that query cluster information to return empty rows. You can still create a listener to use it for transparent reconnection after failover, but you will have to manually register the listener name in the  DNS server with the IP used to create the virtual IP resource (as explained below).

> [!NOTE] 
> This is not a production setup. This guide creates an architecture that is for high-level functional testing.

The following sections walk through the steps to set up a failover cluster solution. 

## Install and configure Pacemaker on each cluster node

1. On all nodes open the firewall ports. Open the port for the high-availability service, SQL Server, and the availability group endpoint. The default TCP port for SQL Server is 1433.  

   ```bash
   sudo ufw allow 2224/tcp
   sudo ufw allow 3121/tcp
   sudo ufw allow 21064/tcp
   sudo ufw allow 5405/udp
   		
   sudo ufw allow 1433/tcp # Replace with TDS endpoint
   sudo ufw allow 5022/tcp # Replace with DATA_MIRRORING endpoint
		
   sudo ufw reload
   ```
   
   Alternatively, you can just disable the firewall:
   		
   ```bash
   sudo ufw disable
   ```

1. Install Pacemaker packages. On all nodes, run the following commands:

   ```bash
   sudo apt-get install pacemaker pcs fence-agents resource-agents
   ```

2. Set the password for for the default user that is created when installing Pacemaker and Corosync packages. Use the same password on both nodes. 

   ```bash
   sudo passwd hacluster
   ```

3. Enable and start `pcsd` service and Pacemaker. This will allow nodes to rejoin the cluster after the reboot. Run the following command on both nodes.

   ```bash
   sudo systemctl enable pcsd
   sudo systemctl start pcsd
   sudo systemctl enable pacemaker
   ```

## Create a SQL Server login for Pacemaker

[!INCLUDE [SLES-Create-SQL-Login](../includes/ss-linux-cluster-pacemaker-create-login.md)]

## Enable and start pcsd service and Pacemaker

The following command enables and starts pcsd service and pacemaker. This allows the nodes to rejoin the cluster after reboot. 

```bash
sudo systemctl enable pcsd
sudo systemctl start pcsd
sudo systemctl enable pacemaker
```

## Create the Cluster

Run the following command on all nodes. 

```bash
sudo pcs cluster destroy # On all nodes
```

Run the following command the primary SQL Server. 

```bash
sudo pcs cluster auth nodeName1 nodeName2  -u hacluster -p <password for hacluster>
sudo pcs cluster setup --name <clusterName> <nodeName1> <nodeName2…> --force
sudo pcs cluster start --all
```

## Disable STONITH

Run the following command to disable STONITH

```bash
sudo pcs property set stonith-enabled=false
```

## Create availability group resource

To create the availability group resource, set properties as follows:

- **clone-max**: Number of AG replicas, including primary. For example, if you have one primary and one secondary, set this to 2.
- **clone-node-max**: Number of secondaries. For example, if you have one primary and one secondary, set this to 1.

The following script sets these properties.

```bash
sudo pcs resource create ag_cluster ocf:mssql:ag ag_name=ag1 \
--master meta master-max=1 master-node-max=1 clone-max=2 clone-node-max=1 
```

## Enable monitoring on primary

To enable monitoring, run the following commands on one node.

```bash
sudo pcs resource op add ag_cluster monitor interval=11s timeout=60s role=Master
sudo pcs resource op add ag_cluster monitor interval=12s timeout=60s role=Slave
```

## Create virtual IP resource

To create the virtual IP address resource, run the following command on one node. Use an available IP address from the network.

```bash
sudo pcs resource create virtualip ocf:heartbeat:IPaddr2 ip=<10.128.16.240>
```

## Add colocation constraint

To add colocation constraint, run the following command on one node.

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
>After you configure the cluster and add the availability group as a cluster resource, you cannot use Transact-SQL to fail over the availability group resources. SQL Server cluster resources on Linux are not coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). SQL Server is not aware of the presence of the cluster. All orchestration is done through the cluster management tools. In RHEL or Ubuntu use `pcs`. 

>[!IMPORTANT]
>If the availability group is a cluster resource, there is a known issue in current release where manual failover to an asynchronous replica does not work. This will be fixed in the upcoming release. Manual or automatic failover to a synchronous replica will succeed. 

Manually failover the availability group with `pcs`. Do not initiate failover with Transact-SQL.

To manually failover to cluster node2, run the following command.

```bash
sudo pcs resource move ag_cluster-master node2 --master
```
>[!NOTE]
>At this time manual failover to an asynchronous replica does not work properly. This will be fixed in a future release. 


## Next steps

[Create SQL Server Availability Group](sql-server-linux-availability-group-configure.md)
