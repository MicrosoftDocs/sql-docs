---
# required metadata

title: Configure Ubuntu Cluster for SQL Server Availability Group | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 02/09/2017
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

## Install mssql-server-ha package

## Create a SQL Server login for Pacemaker

[!INCLUDE [SLES-Create-SQL-Login](../includes/ss-linux-cluster-pacemaker-create-login.md)]

## Open Pacemaker firewall ports

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

## Install Pacemaker packages

On all nodes, run the following commands:

```bash
sudo apt-get install pacemaker pcs fence-agents resource-agents
```

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

## Enable monitoring on master

To enable monitoring, run the following commands on one node.

```bash
sudo pcs resource op add ag_cluster monitor interval=11s timeout=60s role=Master
sudo pcs resource op add ag_cluster monitor interval=12s timeout=60s role=Slave
```

## Create virtual IP resource

## Add colocation constraint

## Add ordering constraint

## Manual failover

## Next steps

[Create SQL Server Availability Group](sql-server-linux-availability-group-configure.md)

