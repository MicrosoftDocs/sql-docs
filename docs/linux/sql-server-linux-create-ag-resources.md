---
title: Create availability group resources in Pacemaker for SQL Server on Linux | Microsoft Docs
description: How to use Pacemaker to create availability group resources for SQL Server on Linux.
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 12/4/2017
ms.topic: article
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "linux"
ms.suite: "sql"
ms.custom: ""
ms.technology: database-engine
ms.workload: "On Demand"
---

# Create availability group resources in a Pacemaker cluster (External only)

[!INCLUDE[tsql-appliesto-sslinux-only](../includes/tsql-appliesto-sslinux-only.md)]

Once an availability group (AG) is created in SQL Server, the corresponding resources must be created in Pacemaker, when a cluster type of External is specified. There are two resources associated with an AG: the AG itself and an IP address. Configuring the IP address resource is optional if you are not using the listener functionality, but is recommended.

The AG resource that is created is a special kind of resource called a clone. The AG resource essentially has copies on each node, and there is one controlling resource called the master. The master is associated with the server hosting the primary replica. The secondary replicas (regular or configuration-only) are considered to be slaves and can be promoted to master in a failover.

## Prerequisite
Create the Pacemaker cluster as documented in [Deploy a Pacemaker cluster for SQL Server on Linux](sql-server-linux-deploy-pacemaker-cluster.md).

## Create the resources
1.  Create the AG resource with the following syntax:

    **Red Hat Enterprise Linux (RHEL) and Ubuntu**
    
    ```bash
    sudo pcs resource create <NameForAGResource> ocf:mssql:ag ag_name=<AGName> --master meta notify=true
    ```
    
    **SUSE Linux Enterprise Server (SLES)**
    
    primitive <NameForAGResource> \
    ocf:mssql:ag \
    params ag_name="<AGName>" \
    op start timeout=60s \
    op stop timeout=60s \
    op promote timeout=60s \
    op demote timeout=10s \
    op monitor timeout=60s interval=10s \
    op monitor timeout=60s interval=11s role="Master" \
    op monitor timeout=60s interval=12s role="Slave" \
    op notify timeout=60s
    ms ms-ag_cluster <NameForAGResource> \
    meta master-max="1" master-node-max="1" clone-max="3" \
    clone-node-max="1" notify="true" \
    commit
    ```
    
    where *NameForAGResource* is the unique name given to this cluster resource for the AG, and *AGName* is the name of the AG that was created.
 
2.  Create the IP address resource for the AG that will be associated with the listener functionality.

    **RHEL and Ubuntu**
    
    ```bash
    sudo pcs resource create <NameForIPResource> ocf:heartbeat:IPaddr2 ip=<IPAddress> cidr_netmask=<Netmask>
    ```
    
    **SLES**
    
    ```bash
    crm configure \
    primitive <NameForIPResource> \
       ocf:heartbeat:IPaddr2 \
       params ip=<IPAddress> \
          cidr_netmask=<Netmask>
    ```
    
    where *NameForIPResource* is the unique name for the IP resource, and *IPAddress* is the static IP address assigned to the resource. On SLES, you also need to provide the netmask. For example, 255.255.255.0 would have a value of 24 for *Netmask.*
    
3.  To ensure that the IP address and the AG resource are running on the same node, a colocation constraint must be configured.

    **RHEL and Ubuntu**
    
    ```bash
    sudo pcs constraint colocation add <NameForIPResource> <NameForAGResource>-master INFINITY with-rsc-role=Master
    ```
    
    **SLES**
    
    ```bash
    crm configure <NameForConstraint> inf: \
    <NameForIPResource> <NameForAGResource>:Master 
    commit
    ```
    
    where *NameForIPResource* is the name for the IP resource, *NameForAGResource* is the name for the AG resource, and on SLES, *NameForConstraint* is the name for the constraint.

4.  Create an ordering constraint to ensure that the AG resource is up and running before the IP address. While the colocation constraint implies an ordering constraint, this enforces it.

    **RHEL and Ubuntu**
    
    ```bash
    sudo pcs constraint order promote <NameForAGResource>-master then start <NameForIPResource>
    ```
    
    **SLES**
    
    ```bash
    crm configure \
    order <NameForConstraint> inf: <NameForAGResource>:promote <NameForIPResource>:start
    commit
    ```
    
    where *NameForIPResource* is the name for the IP resource, *NameForAGResource* is the name for the AG resource, and on SLES, *NameForConstraint* is the name for the constraint.

## Next steps

For most AG administration tasks, including upgrades and failing over, see [Operate HA availability group for SQL Server on Linux](sql-server-linux-availability-group-failover-ha.md).
