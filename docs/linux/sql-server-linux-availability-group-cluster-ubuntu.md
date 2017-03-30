---
# required metadata

title: Configure Ubuntu Cluster for SQL Server Availability Group | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 03/17/2017
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

# Configure Ubuntu Cluster and Availability Group Resource

This document explains how to create a two-node cluster on Ubuntu and add a previously created availability group as a resource in the cluster. 

> [!NOTE] 
> At this point, SQL Server's integration with Pacemaker on Linux is not as coupled as with WSFC on Windows. From within SQL, there is no knowledge about the presence of the cluster, all orchestration is outside in and the service is controlled as a standalone instance by Pacemaker. Also, virtual network name is specific to WSFC, there is no equivalent of the same in Pacemaker. Always On dynamic management views that query cluster information will return empty rows. You can still create a listener to use it for transparent reconnection after failover, but you will have to manually register the listener name in the  DNS server with the IP used to create the virtual IP resource (as explained below).

The following sections walk through the steps to set up a failover cluster solution. 

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

2. Set the password for the default user that is created when installing Pacemaker and Corosync packages. Use the same password on both nodes. 

   ```bash
   sudo passwd hacluster
   ```

## Enable and start pcsd service and Pacemaker

The following command enables and starts pcsd service and pacemaker. This allows the nodes to rejoin the cluster after reboot. 

```bash
sudo systemctl enable pcsd
sudo systemctl start pcsd
sudo systemctl enable pacemaker
```
>[!NOTE]
>Enable pacemaker command will complete with the error 'pacemaker Default-Start contains no runlevels, aborting.' This is harmless, cluster configuration can continue. We are following up with cluster vendors for fixing this issue.

## Create the Cluster

1. Remove any existing cluster configuration. 

   Running 'sudo apt-get install pcs' installs pacemaker, corosync, and pcs at the same time and starts running all 3 of the services.  Starting corosync generates a template '/etc/cluster/corosync.conf' file.  To have next steps succeed this file should not exist – so the workaround is to stop pacemaker / corosync and delete '/etc/cluster/corosync.conf', and then next steps will complete successfully. 'pcs cluster destroy' does the same thing, and you can use it as a one time initial cluster setup step.
   
   The following command removes any existing cluster configuration files and stops all cluster services. This permanently destroys the cluster. Run it as a first step in a pre-production environment. Run the following command on all nodes. Note that 'pcs cluster destroy' disabled the pacemaker service and needs to be reenabled.
   
   >[!WARNING]
   >The command will destroy any existing cluster resources.

   ```bash
   sudo pcs cluster destroy # On all nodes
   sudo systemctl enable pacemaker
   ```

1. Create the cluster. 

   >[!WARNING]
   >Due to a known issue that the clustering vendor is investigating, starting the cluster ('pcs cluster start') will fail with below error. This is because the log file configured in /etc/corosync/corosync.conf is wrong. To workaround this issue, change the log file to: /var/log/corosync/corosync.log. Alternatively you could create the /var/log/cluster/corosync.log file.
 
 ```bash
 Job for corosync.service failed because the control process exited with error code. 
 See "systemctl status corosync.service" and "journalctl -xe" for details.
  ```
  
The following command creates a two node cluster. Before you run the script, replace the values between `**< ... >**`. Run the following command on the primary SQL Server. 

   ```bash
   sudo pcs cluster auth **<nodeName1>** **<nodeName2>**  -u hacluster -p **<password for hacluster>**
   sudo pcs cluster setup --name **<clusterName>** **<nodeName1>** **<nodeName2…>** 
   sudo pcs cluster start --all
   ```
   
   >[!NOTE]
   >If you previously configured a cluster on the same nodes, you need to use '--force' option when running 'pcs cluster setup'. Note this is equivalent to running 'pcs cluster destroy' and pacemaker service needs to be reenabled using 'sudo systemctl enable pacemaker'.

## Install SQL Server resource agent for integration with Pacemaker

Run the following commands on all nodes. 

```bash
sudo apt-get install mssql-server-ha
```

## Configure fencing (STONITH)
Pacemaker cluster vendors require STONITH to be enabled and a fencing device configured for a supported cluster setup. Fencing configuration is needed to allow a surviving cluster node to forcibly remove a non-responsive node from the cluster. For details, see [Pacemaker Clusters from Scratch](http://clusterlabs.org/doc/en-US/Pacemaker/1.1-plugin/html/Clusters_from_Scratch/ch05.html)

To continue the configuration and validate the cluster setup , disable STONITH (it can be configured at a later time):

```bash
sudo pcs property set stonith-enabled=false
```

>[!IMPORTANT]
>This is not supported by the clustering vendors in a production setup. For details, see [Pacemaker Clustersf from Scratch](http://clusterlabs.org/doc/en-US/Pacemaker/1.1-plugin/html/Clusters_from_Scratch/ch05.html) and
[Red Hat High Availability Add-On with Pacemaker: Fencing](http://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/6/html/Configuring_the_Red_Hat_High_Availability_Add-On_with_Pacemaker/ch-fencing-HAAR.html).

## Create a SQL Server login for Pacemaker

[!INCLUDE [SLES-Create-SQL-Login](../includes/ss-linux-cluster-pacemaker-create-login.md)]

## Create availability group resource

To create the availability group resource, set properties as follows:

- **clone-max**: Number of AG replicas, including primary. For example, if you have one primary and one secondary, set this to 2. Default is number of nodes in the cluster.
- **clone-node-max**: Number of replicas that can be started on a node. Set this to 1 (or use the default which is 1).

The following script sets these properties.

```bash
sudo pcs resource create ag_cluster ocf:mssql:ag ag_name=ag1 \
--master meta master-max=1 master-node-max=1 clone-max=2 clone-node-max=1 notify=true
```

## Create virtual IP resource

To create the virtual IP address resource, run the following command on one node. Use an available static IP address from the network. Before you run the script, replace the values between `**< ... >**` with a valid IP address.

```bash
sudo pcs resource create virtualip ocf:heartbeat:IPaddr2 ip=**<10.128.16.240>**
```

There is no virtual server name equivalent in Pacemaker. To use a connection string that points to a string server name and not use the IP address, register the IP resource address and desired virtual server name in DNS. For DR configurations, register the desired virtual server name and IP address with the DNS servers on both primary and DR site.

## Add colocation constraint

To add colocation constraint, run the following command on one node.

```bash
sudo pcs constraint colocation add virtualip ag_cluster-master INFINITY with-rsc-role=Master
```

## Add ordering constraint

The colocation constraint has an implicit ordering constraint. It moves the virtual IP resource before it moves the availability group resource. By default the sequence of events is:

1. User issues `pcs resource move` to the availability group primary from node1 to node2.
1. The virtual IP resource stops on nodeName1.
1. The virtual IP resource starts on nodeName2.

   >[!NOTE]
   >At this point, the IP address temporarily points to nodeName2 while nodeName2 is still a pre-failover secondary. 
   
1. The availability group primary on nodeName1 is demoted to secondary.
1. The availability group secondary on nodeName2 is promoted to primary. 

To prevent the IP address from temporarily pointing to the node with the pre-failover secondary, add an ordering constraint. 

To add an ordering constraint, run the following command on one node:

```bash
sudo pcs constraint order promote ag_cluster-master then start virtualip
```

## Manual failover

>[!IMPORTANT]
>After you configure the cluster and add the availability group as a cluster resource, you cannot use Transact-SQL to fail over the availability group resources. SQL Server cluster resources on Linux are not coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). SQL Server service is not aware of the presence of the cluster. All orchestration is done through the cluster management tools. In RHEL or Ubuntu use `pcs`. 

>[!IMPORTANT]
>If the availability group is a cluster resource, there is a known issue in current release where forced failover to an asynchronous replica does not work. This will be fixed in the upcoming release. Manual or automatic failover to a synchronous replica will succeed. 

Manually failover the availability group with `pcs`. Do not initiate failover with Transact-SQL.

To manually failover to cluster nodeName2, run the following command.

```bash
sudo pcs resource move ag_cluster-master nodeName2 --master
```

[!INCLUDE [Move-Resource](../includes/ss-linux-cluster-pacemaker-configure-rhel-ubuntu-move-resource.md)]

<a name="sync-commit"></a>
[!INCLUDE [Manage-Sync-Commit](../includes/ss-linux-cluster-availability-group-manage-sync-commit.md)]
