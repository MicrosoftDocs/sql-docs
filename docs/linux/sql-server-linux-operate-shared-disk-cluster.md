---
# required metadata

title: Operate shared disk cluster | SQL Server vNext CTP1
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 10-31-2016
ms.topic: article
ms.prod: sql-non-specified
ms.service: 
ms.technology: 
ms.assetid: 

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

# Operate shared disk cluster

## Failover cluster manually

The `resource move` command creates a constraint forcing the resource to remain on the target node.  After executing the `move` command, executing resource `clear` will remove the constraint so it is possible to move the resource again or have the resource automatically fail over. 

```bash
# pcs resource move <sqlResourceName> <targetNodeName>  
# pcs resource clear <sqlResourceName> 
```

The following example moves the **mssql** resource to a node named **vm2**, and then removes the constraint so that the resource can move to a different node later.  

```bash
# pcs resource move mssql vm2 
# pcs resource clear mssql 
```

## Monitor and troubleshoot failover cluster

View the current cluster status:

```bash
# pcs status  
```

View live status of cluster and resources:

```bash
# crm_mon 
```

View the resource agent logs at `/var/log/cluster/corosync.log`

## Add a node to a cluster

1. Check the IP address for each node. The following script shows the IP address of your current node. 

   ```bash
   # ip addr show
   ```

3. The new node needs a unique name that is 15 characters or less. By default in Red Hat Linux the computer name is `localhost.localdomain`. This default name may not be unique and is too long. Set the computer name the new node. Set the computer name by adding it to `/etc/hosts`. The following script lets you edit `/etc/hosts` with `vi`. 

    ```
   # vi /etc/hosts
    ```

   The following example shows `/etc/hosts` with additions for three nodes named `sqlfcivm1`, `sqlfcivm2`, and`sqlfcivm3`.

    ```
    127.0.0.1   localhost localhost4 localhost4.localdomain4
    ::1         localhost localhost6 localhost6.localdomain6
    10.128.18.128 fcivm1
    10.128.16.77 fcivm2
    10.128.14.26 fcivm3
    ```
    
    The file should be the same on every node. 

1. Stop the SQL Server service.

1. Install `cifs-utils` on both nodes. 
    
## Remove nodes from a cluster

