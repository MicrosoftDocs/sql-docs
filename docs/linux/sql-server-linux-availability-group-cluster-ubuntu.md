---
title: Configure Ubuntu Cluster for SQL Server Availability Group
titleSuffix: SQL Server
description: Learn about creating availability group clusters for Ubuntu
author: MikeRayMSFT 
ms.author: mikeray 
manager: craigg
ms.date: 04/30/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux, seodec18"
ms.technology: linux
ms.assetid: dd0d6fb9-df0a-41b9-9f22-9b558b2b2233
---
# Configure Ubuntu Cluster and Availability Group Resource

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This document explains how to create a three-node cluster on Ubuntu and add a previously created availability group as a resource in the cluster. 
For high availability, an availability group on Linux requires three nodes - see [High availability and data protection for availability group configurations](sql-server-linux-availability-group-ha.md).

> [!NOTE] 
> At this point, SQL Server's integration with Pacemaker on Linux is not as coupled as with WSFC on Windows. From within SQL, there is no knowledge about the presence of the cluster, all orchestration is outside in, and the service is controlled as a standalone instance by Pacemaker. Also, virtual network name is specific to WSFC, there is no equivalent of the same in Pacemaker. Always On dynamic management views that query cluster information return empty rows. You can still create a listener to use it for transparent reconnection after failover, but you have to manually register the listener name in the  DNS server with the IP used to create the virtual IP resource (as explained in the following sections).

The following sections walk through the steps to set up a failover cluster solution. 

## Roadmap

The steps to create an availability group on Linux servers for high availability are different from the steps on a Windows Server failover cluster. The following list describes the high-level steps: 

1. [Configure SQL Server on the cluster nodes](sql-server-linux-setup.md).

2. [Create the availability group](sql-server-linux-availability-group-configure-ha.md). 

3. Configure a cluster resource manager, like Pacemaker. These instructions are in this document.
   
   The way to configure a cluster resource manager depends on the specific Linux distribution. 

   >[!IMPORTANT]
   >Production environments require a fencing agent, like STONITH for high availability. The demonstrations in this documentation do not use fencing agents. The demonstrations are for testing and validation only. 
   
   >A Linux cluster uses fencing to return the cluster to a known state. The way to configure fencing depends on the distribution and the environment. At this time, fencing is not available in some cloud environments. See [Support Policies for RHEL High Availability Clusters - Virtualization Platforms](https://access.redhat.com/articles/29440) for more information.

5.  [Add the availability group as a resource in the cluster](sql-server-linux-availability-group-cluster-ubuntu.md#create-availability-group-resource). 

## Install and configure Pacemaker on each cluster node

1. On all nodes open the firewall ports. Open the port for the Pacemaker high-availability service, SQL Server instance, and the availability group endpoint. The default TCP port for server running SQL Server is 1433.  

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

2. Set the password for the default user that is created when installing Pacemaker and Corosync packages. Use the same password on all nodes. 

   ```bash
   sudo passwd hacluster
   ```

## Enable and start pcsd service and Pacemaker

The following command enables and starts pcsd service and pacemaker. Run on all nodes. This allows the nodes to rejoin the cluster after reboot. 

```bash
sudo systemctl enable pcsd
sudo systemctl start pcsd
sudo systemctl enable pacemaker
```
>[!NOTE]
>Enable pacemaker command may complete with the error 'pacemaker Default-Start contains no runlevels, aborting.' This is harmless, cluster configuration can continue. 

## Create the Cluster

1. Remove any existing cluster configuration from all nodes. 

   Running 'sudo apt-get install pcs' installs pacemaker, corosync, and pcs at the same time and starts running all 3 of the services.  Starting corosync generates a template '/etc/cluster/corosync.conf' file.  To have next steps succeed this file should not exist - so the workaround is to stop pacemaker / corosync and delete '/etc/cluster/corosync.conf', and then next steps complete successfully. 'pcs cluster destroy' does the same thing, and you can use it as a one time initial cluster setup step.
   
   The following command removes any existing cluster configuration files and stops all cluster services. This permanently destroys the cluster. Run it as a first step in a pre-production environment. Note that 'pcs cluster destroy' disabled the pacemaker service and needs to be reenabled. Run the following command on all nodes.
   
   >[!WARNING]
   >The command destroys any existing cluster resources.

   ```bash
   sudo pcs cluster destroy 
   sudo systemctl enable pacemaker
   ```

1. Create the cluster. 

   >[!WARNING]
   >Due to a known issue that the clustering vendor is investigating, starting the cluster ('pcs cluster start') fails with following error. This is because the log file configured in /etc/corosync/corosync.conf which is created when the cluster setup command is run, is wrong. To work around this issue, change the log file to: /var/log/corosync/corosync.log. Alternatively you could create the /var/log/cluster/corosync.log file.
 
   ```Error
   Job for corosync.service failed because the control process exited with error code. 
   See "systemctl status corosync.service" and "journalctl -xe" for details.
   ```
  
The following command creates a three-node cluster. Before you run the script, replace the values between `< ... >`. Run the following command on the primary node. 

   ```bash
   sudo pcs cluster auth <node1> <node2> <node3> -u hacluster -p <password for hacluster>
   sudo pcs cluster setup --name <clusterName> <node1> <node2...> <node3>
   sudo pcs cluster start --all
   sudo pcs cluster enable --all
   ```
   
   >[!NOTE]
   >If you previously configured a cluster on the same nodes, you need to use '--force' option when running 'pcs cluster setup'. Note this is equivalent to running 'pcs cluster destroy' and pacemaker service needs to be reenabled using 'sudo systemctl enable pacemaker'.


## Configure fencing (STONITH)

Pacemaker cluster vendors require STONITH to be enabled and a fencing device configured for a supported cluster setup. When the cluster resource manager cannot determine the state of a node or of a resource on a node, fencing is used to bring the cluster to a known state again. 
Resource level fencing ensures mainly that there is no data corruption in case of an outage by configuring a resource. You can use resource level fencing, for instance, with DRBD (Distributed Replicated Block Device) to mark the disk on a node as outdated when the communication link goes down. 
Node level fencing ensures that a node does not run any resources. This is done by resetting the node and the Pacemaker implementation of it is called STONITH (which stands for "shoot the other node in the head"). Pacemaker supports a great variety of fencing devices, e.g. an uninterruptible power supply or management interface cards for servers. 
For more information, see [Pacemaker Clusters from Scratch](https://clusterlabs.org/pacemaker/doc/en-US/Pacemaker/1.1/html/Clusters_from_Scratch/) and [Fencing and Stonith](https://clusterlabs.org/doc/crm_fencing.html) 

Because the node level fencing configuration depends heavily on your environment, we disable it for this tutorial (it can be configured at a later time). Run the following script on the primary node: 

```bash
sudo pcs property set stonith-enabled=false
```

>[!IMPORTANT]
>Disabling STONITH is just for testing purposes. If you plan to use Pacemaker in a production environment, you should plan a STONITH implementation depending on your environment and keep it enabled. Note that at this point there are no fencing agents for any cloud environments (including Azure) or Hyper-V. Consequentially, the cluster vendor does not offer support for running production clusters in these environments. 

## Set cluster property cluster-recheck-interval

`cluster-recheck-interval` indicates the polling interval at which the cluster checks for changes in the resource parameters, constraints or other cluster options. If a replica goes down, the cluster tries to restart the replica at an interval that is bound by the `failure-timeout` value and the `cluster-recheck-interval` value. For example, if `failure-timeout` is set to 60 seconds and `cluster-recheck-interval` is set to 120 seconds, the restart is tried at an interval that is greater than 60 seconds but less than 120 seconds. We recommend that you set failure-timeout to 60s and cluster-recheck-interval to a value that is greater than 60 seconds. Setting cluster-recheck-interval to a small value is not recommended.

To update the property value to `2 minutes` run:

```bash
sudo pcs property set cluster-recheck-interval=2min
```

> [!IMPORTANT] 
> If you already have an availability group resource managed by a Pacemaker cluster, note that all distributions that use the latest available Pacemaker package 1.1.18-11.el7 introduce a behavior change for the start-failure-is-fatal cluster setting when its value is false. This change affects the failover workflow. If a primary replica experiences an outage, the cluster is expected to failover to one of the available secondary replicas. Instead, users will notice that the cluster keeps trying to start the failed primary replica. If that primary never comes online (because of a permanent outage), the cluster never fails over to another available secondary replica. Because of this change, a previously recommended configuration to set start-failure-is-fatal is no longer valid and the setting needs to be reverted back to its default value of `true`. 
> Additionally, the AG resource needs to be updated to include the `failover-timeout` property. 
>
>To update the property value to `true` run:
>
>```bash
>sudo pcs property set start-failure-is-fatal=true
>```
>
>Update your existing AG resource property `failure-timeout` to `60s` run (replace `ag1` with the name of your availability group resource):
>
>```bash
>pcs resource update ag1 meta failure-timeout=60s
>```

## Install SQL Server resource agent for integration with Pacemaker

Run the following commands on all nodes. 

```bash
sudo apt-get install mssql-server-ha
```

## Create a SQL Server login for Pacemaker

[!INCLUDE [SLES-Create-SQL-Login](../includes/ss-linux-cluster-pacemaker-create-login.md)]

## Create availability group resource

To create the availability group resource, use `pcs resource create` command and set the resource properties. Below command creates a `ocf:mssql:ag` master/slave type resource for availability group with name `ag1`. 

```bash
sudo pcs resource create ag_cluster ocf:mssql:ag ag_name=ag1 meta failure-timeout=30s --master meta notify=true

```

[!INCLUDE [required-synchronized-secondaries-default](../includes/ss-linux-cluster-required-synchronized-secondaries-default.md)]

## Create virtual IP resource

To create the virtual IP address resource, run the following command on one node. Use an available static IP address from the network. Before you run the script, replace the values between `< ... >` with a valid IP address.

```bash
sudo pcs resource create virtualip ocf:heartbeat:IPaddr2 ip=<10.128.16.240>
```

There is no virtual server name equivalent in Pacemaker. To use a connection string that points to a string server name and not use the IP address, register the IP resource address and desired virtual server name in DNS. For DR configurations, register the desired virtual server name and IP address with the DNS servers on both primary and DR site.

## Add colocation constraint

Almost every decision in a Pacemaker cluster, like choosing where a resource should run, is done by comparing scores. Scores are calculated per resource, and the cluster resource manager chooses the node with the highest score for a particular resource. (If a node has a negative score for a resource, the resource cannot run on that node.) 
Use constraints to configure the decisions of the cluster. Constraints have a score. If a constraint has a score lower than INFINITY, it is only a recommendation. A score of INFINITY means it is mandatory. 
To ensure that primary replica and the virtual ip resource are on the same host, define a colocation constraint with a score of INFINITY. To add the colocation constraint, run the following command on one node. 

```bash
sudo pcs constraint colocation add virtualip ag_cluster-master INFINITY with-rsc-role=Master
```

## Add ordering constraint

The colocation constraint has an implicit ordering constraint. It moves the virtual IP resource before it moves the availability group resource. By default the sequence of events is:

1. User issues `pcs resource move` to the availability group primary from node1 to node2.
1. The virtual IP resource stops on node1.
1. The virtual IP resource starts on node2.

   >[!NOTE]
   >At this point, the IP address temporarily points to node2 while node2 is still a pre-failover secondary. 
   
1. The availability group primary on node1 is demoted to secondary.
1. The availability group secondary on node2 is promoted to primary. 

To prevent the IP address from temporarily pointing to the node with the pre-failover secondary, add an ordering constraint. 

To add an ordering constraint, run the following command on one node:

```bash
sudo pcs constraint order promote ag_cluster-master then start virtualip
```

>[!IMPORTANT]
>After you configure the cluster and add the availability group as a cluster resource, you cannot use Transact-SQL to fail over the availability group resources. SQL Server cluster resources on Linux are not coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). SQL Server service is not aware of the presence of the cluster. All orchestration is done through the cluster management tools. In RHEL or Ubuntu use `pcs`. 

<!---[!INCLUDE [Pacemaker Concepts](..\includes\ss-linux-cluster-pacemaker-concepts.md)]--->

## Next steps

[Operate HA availability group](sql-server-linux-availability-group-failover-ha.md)

