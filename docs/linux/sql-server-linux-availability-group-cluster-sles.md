---
# required metadata

title: Configure SLES Cluster for SQL Server Availability Group | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 02/09/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 85180155-6726-4f42-ba57-200bf1e15f4d

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

# Configure SLES Cluster for SQL Server Availability Group

This guide provides instructions to create a two-node shared disk cluster for SQL Server on SUSE Linux Enterprise Server (SLES). The clustering layer is based on SUSE [High Availability Extension (HAE)](https://www.suse.com/products/highavailability) built on top of [Pacemaker](http://clusterlabs.org/). 

For more details on cluster configuration, resource agent options, management, best practices, and recommendations, see [SUSE Linux Enterprise High Availability Extension 12 SP2](https://www.suse.com/documentation/sle-ha-12/index.html).

> [!NOTE]
> At this point, SQL Server's integration with Pacemaker on Linux is not as coupled as with WSFC on Windows. From within SQL, there is no knowledge about the presence of the cluster, all orchestration is outside in and the service is controlled as a standalone instance by Pacemaker. Also, virtual network name is specific to WSFC, there is no equivalent of the same in Pacemaker. It is expected that @@servername and sys.servers to return the node name, while the cluster dmvs sys.dm_os_cluster_nodes and sys.dm_os_cluster_properties will no records. To use a connection string that points to a string server name and not use the IP, they will have to register in their DNS server the IP used to create the virtual IP resource (as explained below) with the chosen server name.

## Install and configure Pacemaker on each cluster node
 
[!INCLUDE [SLES-Configure-Pacemaker](../includes/ss-linux-cluster-pacemaker-configure-sles.md)]

## Create a SQL Server login for Pacemaker

[!INCLUDE [SLES-Create-SQL-Login](../includes/ss-linux-cluster-pacemaker-create-login.md)]

## Open Pacemaker firewall ports

For instructions, see [SuSEFirewall2](https://www.suse.com/documentation/sles-12/book_security/data/sec_security_firewall_suse.html).

## Install Pacemaker packages

For instructions, see [Installing SUSE Linux Enterprise Server and High Availability Extension](https://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html#sec.ha.inst.quick.installation)

## Set password for default user

Set the password for the default user that is created when installing pacemaker and corosync packages. Use the same password on all nodes. The following command sets the password.

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

## Create the Cluster

To create the cluster, run `ha-cluster-init` on the first node. For instructions, see [Setting up the first node](http://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html#sec.ha.inst.quick.setup.1st-node).

To add additional nodes, run `ha-cluster-join` on secondary nodes. See [Adding the second node](http://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html#sec.ha.inst.quick.setup.1st-node)

## Disable STONITH

On SLES, STONITH is disabled during `ha-cluster-init` on the first node, and `ha-cluster-join` on secondary nodes. You can also manually disable it by running the following commands. 

```bash
sudo crm configure property stonith-enabled=false
sudo crm configure property start-failure-is-fatal=false
```

## Create availability group resource

The following command creates and configures the availabiltiy group resource.

```bash
crm configure
#primitive ag_cluster \
   ocf:mssql:ag \
   params ag_name="ag1" \
   op monitor interval="15" role="Master" \
   op monitor interval="30" role="Slave"
#ms ms-ag_cluster ag_cluster \
   meta master-max="1" master-node-max="1" clone-max="2" \
   clone-node-max="1"
commit
```

## Create virtual IP resource

If you did not create the virtual IP resource when you ran `ha-cluster-init` you can create this resource now. The following command creates a virtual IP resource. Run on one node.

```bash
crm configure
# primitive admin_addr \
   ocf:heartbeat:IPaddr2 \
   params ip=<10.9.9.180> \
      cidr_netmask=<24> \
   op monitor interval="30s"
```

## Add colocation constraint

To set colocation constraint, run the following command on one node.

```bash
crm configure
colocation vip_on_master inf: \
    admin_addr ms-ag_cluster:Master
commit
```



## Add ordering constraint

The colocation constraint has an implicit ordering constraint. It moves the virtual IP resource before it moves the availability group resource. By default the sequence of events is:

1. User issues `pcs resource move` to the availability group master from node1 to node2.
1. The virtual IP resource stops on node 1.
1. The virtual IP resource starts on node 2. 
   >[!NOTE]
   >At this point, the IP address temporarily points to node 2 while node 2 is still a pre-failover secondary. 
1. The availability group master on node 1 is demoted to slave.
1. The availability group slave on node 2 is promoted to master. 

To prevent the IP address from temporarily pointing to the node with the pre-failover secondary, add an ordering constraint. 

To add an ordering constraint, run the following command on one node:

```bash
crm configure
   order ag_first inf: ms-ag_cluster:promote admin_addr:start
commit
```

For more information, see [Show Cluster Resources](http://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#sec.ha.manual_config.show).

## Manual failover

Manage failover of the availability group with `crm`. Do not initiate failover with Transact-SQL. To manually failover to cluster node2, run the following command.

```bash
crm resource migrate ms-ag_cluster sles1
```

>[!NOTE]
>At this time manual failover to an asynchronous replica does not work properly. This will be fixed in a future release.

## Next steps

[Create SQL Server Availability Group](sql-server-linux-availability-group-configure.md)

