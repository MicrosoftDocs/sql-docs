---
title: Configure Ubuntu cluster for availability group
titleSuffix: SQL Server on Linux
description: Learn to create a three-node cluster on Ubuntu and add a previously created availability group resource to the cluster. 
ms.custom: seo-lt-2019
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto, randolphwest
ms.date: 08/25/2022
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
---
# Configure Ubuntu Cluster and Availability Group Resource

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article describes how to create a three-node cluster on Ubuntu 20.04 and add a previously created availability group as a resource in the cluster. For high availability, an availability group on Linux requires three nodes - see [High availability and data protection for availability group configurations](sql-server-linux-availability-group-ha.md).

SQL Server's integration with Pacemaker on Linux isn't as coupled as with WSFC on Windows. From within SQL Server, there's no knowledge about the presence of the cluster, all orchestration is from outside in, and the service is controlled as a standalone instance by the cluster manager. Also, the virtual network name is specific to WSFC, and there's no equivalent of the same in Pacemaker. Always On dynamic management views that query cluster information return empty rows.

You can still [create a listener](sql-server-linux-availability-group-overview.md#the-listener-under-linux) for transparent reconnection after failover, but you have to manually register the listener name in the DNS server with the IP used to create the virtual IP resource (as explained in the following sections).

The following sections walk through the steps to set up a failover cluster solution.

## Roadmap

The steps to create an availability group on Linux servers for high availability are different from the steps on a Windows Server failover cluster. The following list describes the high-level steps:

1. [Configure SQL Server on the cluster nodes](sql-server-linux-setup.md).

1. [Create the availability group](sql-server-linux-availability-group-configure-ha.md). 

1. Configure a cluster resource manager, like Pacemaker. These instructions are in this document.

   The way to configure a cluster resource manager depends on the specific Linux distribution.

   Production environments require a fencing agent, like [STONITH](#stonith) for high availability. The demonstrations in this documentation don't use fencing agents. The demonstrations are for testing and validation only.

   A Linux cluster uses fencing to return the cluster to a known state. The way to configure fencing depends on the distribution and the environment. At this time, fencing isn't available in some cloud environments. For more information, see [Support Policies for RHEL High Availability Clusters - Virtualization Platforms](https://access.redhat.com/articles/29440).

   Fencing is normally implemented at the operating system and is dependent on the environment. Find instructions for fencing in the operating system distributor documentation.

1. [Add the availability group as a resource in the cluster](sql-server-linux-availability-group-cluster-ubuntu.md#create-availability-group-resource).

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

1. Set the password for the default user that is created when installing Pacemaker and Corosync packages. Use the same password on all nodes.

   ```bash
   sudo passwd hacluster
   ```

## Enable and start pcsd service and Pacemaker

The following command enables and starts the `pcsd` service and Pacemaker. Run on all nodes. This allows the nodes to rejoin the cluster after reboot.

```bash
sudo systemctl enable pcsd
sudo systemctl start pcsd
sudo systemctl enable pacemaker
```

The `enable pacemaker` command may complete with the following error:

```error
pacemaker Default-Start contains no runlevels, aborting.
```

The error is harmless, and cluster configuration can continue.

## Create the cluster

1. Remove any existing cluster configuration from all nodes.

   Running `sudo apt-get install pcs` installs **pacemaker**, **corosync**, and **pcs** at the same time and starts running all 3 of the services.  Starting **corosync** generates a template `/etc/cluster/corosync.conf` file.  To have next steps succeed, this file shouldn't exist, so the workaround is to stop **pacemaker** or **corosync** and delete `/etc/cluster/corosync.conf`, and then next steps complete successfully. The command `pcs cluster destroy` does the same thing, and you can use it as a one time initial cluster setup step.

   The following command removes any existing cluster configuration files and stops all cluster services, permanently destroying the cluster. Run it as a first step in a pre-production environment. Note that `pcs cluster destroy` disables the Pacemaker service and needs to be reenabled. Run the following command on all nodes.

   > [!WARNING]
   >  
   > The command destroys any existing cluster resources.

   ```bash
   sudo pcs cluster destroy 
   sudo systemctl enable pacemaker
   ```

1. Create the cluster.

   Starting the cluster (`pcs cluster start`) may fail with following error, because the log file configured in `/etc/corosync/corosync.conf`, which is created when the cluster setup command is run, is wrong. To work around this issue, change the log file to `/var/log/corosync/corosync.log`. Alternatively you can create the `/var/log/cluster/corosync.log` file yourself.

   ```Error
   Job for corosync.service failed because the control process exited with error code. 
   See "systemctl status corosync.service" and "journalctl -xe" for details.
   ```

   The following command creates a three-node cluster. Before you run the script, replace the values between `< ... >`. Run the following command on the primary node.

   ```bash
   sudo pcs host auth <node1> <node2> <node3> -u hacluster -p <password for hacluster>
   sudo pcs cluster setup <clusterName> <node1> <node2> <node3>
   sudo pcs cluster start --all
   sudo pcs cluster enable --all
   ```

   In the current implementation of the SQL Server resource agent, the node name must match the `ServerName` property from your instance. For example, if your node name is `node1`, make sure `SERVERPROPERTY('ServerName')` returns `node1` in your SQL Server instance. If there's a mismatch, your replicas will go into a resolving state after the Pacemaker resource is created.

   A scenario where this rule is important is when using fully qualified domain names (FQDN). For example, if you use `node1.yourdomain.com` as the node name during cluster setup, make sure `SERVERPROPERTY('ServerName')` returns `node1.yourdomain.com`, and not just `node1`. The possible workarounds for this problem are:

   - Rename your host name to the FQDN, and use the `sp_dropserver` and `sp_addserver` stored procedures to ensure the metadata in SQL Server matches the change.
   - Use the `addr` option in the `pcs cluster auth` command to match the node name to the `SERVERPROPERTY('ServerName')` value and use a static IP as the node address.

   If you previously configured a cluster on the same nodes, you need to use the `--force` option when running `pcs cluster setup`. This is equivalent to running `pcs cluster destroy`, and the Pacemaker service needs to be reenabled using `sudo systemctl enable pacemaker`.

[!INCLUDE [Considerations for multiple NICs](../includes/linux/sql-server-linux-availability-group-multiple-network-interfaces.md)]

## <a id="stonith"></a> Configure fencing (STONITH)

Pacemaker cluster vendors require STONITH to be enabled and a fencing device configured for a supported cluster setup. (STONITH stands for "shoot the other node in the head".) When the cluster resource manager can't determine the state of a node or of a resource on a node, fencing is used to bring the cluster to a known state again.

Resource level fencing ensures that no data corruption occurs if there's an outage. You can use resource level fencing, for instance, with DRBD (Distributed Replicated Block Device) to mark the disk on a node as outdated when the communication link goes down.

Node level fencing ensures that a node doesn't run any resources. This is done by resetting the node, and the Pacemaker implementation of it is called STONITH. Pacemaker supports a great variety of fencing devices, for example, an uninterruptible power supply or management interface cards for servers.

For more information, see [Pacemaker Clusters from Scratch](https://clusterlabs.org/pacemaker/doc/en-US/Pacemaker/1.1/html/Clusters_from_Scratch/) and [Fencing and Stonith](https://clusterlabs.org/doc/crm_fencing.html).

Because the node level fencing configuration depends heavily on your environment, we disable it for this tutorial (it can be configured at a later time). Run the following script on the primary node:

```bash
sudo pcs property set stonith-enabled=false
```

In this example, disabling STONITH is just for testing purposes. If you plan to use Pacemaker in a production environment, you should plan a STONITH implementation depending on your environment and keep it enabled. Contact the operating system vendor for information about fencing agents for any specific distribution.

## Set cluster property cluster-recheck-interval

The `cluster-recheck-interval` property indicates the polling interval at which the cluster checks for changes in the resource parameters, constraints or other cluster options. If a replica goes down, the cluster tries to restart the replica at an interval that is bound by the `failure-timeout` value and the `cluster-recheck-interval` value. For example, if `failure-timeout` is set to 60 seconds and `cluster-recheck-interval` is set to 120 seconds, the restart is tried at an interval that is greater than 60 seconds but less than 120 seconds. You should set `failure-timeout` to 60 seconds, and `cluster-recheck-interval` to a value that is greater than 60 seconds. Setting `cluster-recheck-interval` to a smaller value isn't recommended.

To update the property value to `2 minutes` run:

```bash
sudo pcs property set cluster-recheck-interval=2min
```

If you already have an availability group resource managed by a Pacemaker cluster, note that all distributions that use the Pacemaker package 1.1.18-11.el7 or later, introduce a behavior change for the `start-failure-is-fatal` cluster setting when its value is `false`. This change affects the failover workflow. If a primary replica experiences an outage, the cluster is expected to fail over to one of the available secondary replicas. Instead, users will notice that the cluster keeps trying to start the failed primary replica. If that primary never comes online (because of a permanent outage), the cluster never fails over to another available secondary replica. Because of this change, a previously recommended configuration to set `start-failure-is-fatal` is no longer valid and the setting needs to be reverted back to its default value of `true`

Additionally, the AG resource needs to be updated to include the `failover-timeout` property.

To update the property value to `true` run:

```bash
sudo pcs property set start-failure-is-fatal=true
```

Update your existing AG resource property `failure-timeout` to `60s` run (replace `ag1` with the name of your availability group resource):

```bash
pcs resource update ag1 meta failure-timeout=60s
```

## Install SQL Server resource agent for integration with Pacemaker

Run the following commands on all nodes.

```bash
sudo apt-get install mssql-server-ha
```

## Create a SQL Server login for Pacemaker

[!INCLUDE [SLES-Create-SQL-Login](../includes/linux/ss-linux-cluster-pacemaker-create-login.md)]

## Create availability group resource

To create the availability group resource, use `pcs resource create` command and set the resource properties. Below command creates a `ocf:mssql:ag` primary/replica type resource for availability group with name `ag1`.

```bash
sudo pcs resource create ag_cluster ocf:mssql:ag ag_name=ag1 meta failure-timeout=30s promotable notify=true
```

[!INCLUDE [required-synchronized-secondaries-default](../includes/linux/ss-linux-cluster-required-synchronized-secondaries-default.md)]

## Create virtual IP resource

To create the virtual IP address resource, run the following command on one node. Use an available static IP address from the network. Before you run the script, replace the values between `< ... >` with a valid IP address.

```bash
sudo pcs resource create virtualip ocf:heartbeat:IPaddr2 ip=<10.128.16.240>
```

There's no virtual server name equivalent in Pacemaker. To use a connection string that points to a string server name and not use the IP address, register the IP resource address and desired virtual server name in DNS. For DR configurations, register the desired virtual server name and IP address with the DNS servers on both primary and DR site.

## Add colocation constraint

Almost every decision in a Pacemaker cluster, like choosing where a resource should run, is done by comparing scores. Scores are calculated per resource, and the cluster resource manager chooses the node with the highest score for a particular resource. (If a node has a negative score for a resource, the resource can't run on that node.)

Use constraints to configure the decisions of the cluster. Constraints have a score. If a constraint has a score lower than INFINITY, it's only a recommendation. A score of INFINITY means it's mandatory.

To ensure that primary replica and the virtual ip resource are on the same host, define a colocation constraint with a score of INFINITY. To add the colocation constraint, run the following command on one node. 

```bash
sudo pcs constraint colocation add virtualip with master ag_cluster-clone INFINITY
```

## Add ordering constraint

The colocation constraint has an implicit ordering constraint. It moves the virtual IP resource before it moves the availability group resource. By default the sequence of events is:

1. User issues `pcs resource move` to the availability group primary from `node1` to `node2`.

1. The virtual IP resource stops on `node1`.

1. The virtual IP resource starts on `node2`.

   At this point, the IP address temporarily points to `node2` while `node2` is still a pre-failover secondary.

1. The availability group primary on `node1` is demoted to secondary.

1. The availability group secondary on `node2` is promoted to primary.

To prevent the IP address from temporarily pointing to the node with the pre-failover secondary, add an ordering constraint.

To add an ordering constraint, run the following command on one node:

```bash
sudo pcs constraint order promote ag_cluster-clone then start virtualip
```

After you configure the cluster and add the availability group as a cluster resource, you can't use Transact-SQL to fail over the availability group resources. SQL Server cluster resources on Linux aren't coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). The SQL Server service isn't aware of the presence of the cluster. All orchestration is done through the cluster management tools. In RHEL or Ubuntu, you should use `pcs`.

## Next steps

- [Operate HA availability group](sql-server-linux-availability-group-failover-ha.md)
