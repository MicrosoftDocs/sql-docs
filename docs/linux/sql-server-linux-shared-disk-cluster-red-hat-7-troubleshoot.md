---
# required metadata

title: Troubleshoot Red Hat Enterprise Linux 7.3 shared disk cluster for SQL Server - SQL Server vNext | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 11/16/2016
ms.topic: article
ms.prod: sql-linux 
ms.technology: database-engine
ms.assetid: 158f6861-d551-4753-b1c9-2b62e2901d67

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

# Troubleshoot Red Hat Enterprise Linux 7.3 shared disk cluster for SQL Server

In troubleshooting the cluster it may help to understand how the three daemons work together to manage cluster resources. 

| Daemon | Description 
| ----- | -----
| Corosync | Provides quorum membership and messaging between cluster nodes.
| Pacemaker | Resides on top of Corosync and provides state machines for resources. 
| PCSD | Manages both Pacemaker and Corosync through the `pcs` tools

PCSD must be running in order to use `pcs` tools. 

### Current cluster status 

`sudo pcs status` returns basic information about the cluster, quorum, nodes, resources, and daemon status for each node. 

An example of a healthy pacemaker quorum output would be:

```
Cluster name: MyAppSQL 
Last updated: Wed Oct 31 12:00:00 2016  Last change: Wed Oct 31 11:00:00 2016 by root via crm_resource on sqlvmnode1 
Stack: corosync 
Current DC: sqlvmnode1  (version 1.1.13-10.el7_2.4-44eb2dd) - partition with quorum 
3 nodes and 1 resource configured 

Online: [ sqlvmnode1 sqlvmnode2 sqlvmnode3] 

Full list of resources: 

mssqlha (ocf::mssql:fci): Started sqlvmnode1 

PCSD Status: 
sqlvmnode1: Online 
sqlvmnode2: Online 
sqlvmnode3: Online 

Daemon Status: 
corosync: active/disabled 
pacemaker: active/enabled 
```

In the example, `partition with quorum` means that a majority quorum of nodes is online. If the cluster loses a majority quorum of nodes , `pcs status` will return `partition WITHOUT quorum` and all resources will be stopped. 

`online: [sqlvmnode1 sqlvmnode2 sqlvmnode3]` returns the name of all nodes currently participating in the cluster. If any nodes are not participating, `pcs status` returns `OFFLINE: [<nodename>]`.

`PCSD Status` shows the cluster status for each node.

### Reasons why a node may be offline

Check the following items when a node is offline.

- **Firewall**

    The following ports need to be open on all nodes for Pacemaker to be able to communicate.
    
    - **TCP: 2224, 3121, 21064

- **Pacemaker or Corosync services running**

- **Node communication**

- **Node name mappings**

## Additional resources

* [Cluster from Scratch](http://clusterlabs.org/doc/Cluster_from_Scratch.pdf) guide from Pacemaker

## Next steps

[Operate SQL Server on Red Hat Enterprise Linux 7.3 shared disk cluster](sql-server-linux-shared-disk-cluster-red-hat-7-operate.md)

