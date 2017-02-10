---
# required metadata

title: Configure RHEL Cluster for SQL Server Availability Group | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 02/09/2017
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

## Configure Pacemaker for RHEL

## Install mssql-server-ha package

## Create a SQL Server login for Pacemaker

[!INCLUDE [SLES-Create-SQL-Login](../includes/ss-linux-cluster-pacemaker-create-login.md)]

## Open Pacemaker firewall ports

On all nodes open the firewall ports. Open the port for the high-availability service, SQL Server, and the availability group endpoint. If firewalld is installed, run the following commands: 

```bash
sudo firewall-cmd --permanent --add-service=high-availability
sudo firewall-cmd --permanent --add-port=1433/tcp
sudo firewall-cmd --permanent --add-port=5022/tcp
		
sudo firewall-cmd --reload
```

## Install Pacemaker packages

On all nodes, run the following commands to install the pacemaker packages:

```bash
sudo yum install pacemaker pcs fence-agents-all resource-agents
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

## Disable STONITH

## Create AG resource

## Enable monitoring on master

## Create virtual IP resource

## Add colocation constraint

## Add ordering constraint

## Manual failover

## Next steps

[Create SQL Server Availability Group](sql-server-linux-availability-group-configure.md)

