---
title: Operate failover cluster instance - SQL Server on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: craigg
ms.date: 08/28/2017
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid:
---
# Operate failover cluster instance - SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article explains how to operate a SQL Server failover cluster instance (FCI) on Linux. If you have not created a SQL Server FCI on Linux, see [Configure failover cluster instance - SQL Server on Linux](sql-server-linux-shared-disk-cluster-configure.md). 

## Failover

Failover for FCIs is similar to a Windows Server failover cluster (WSFC). If the cluster node hosting the FCI experiences some sort of failure, the FCI should automatically fail over to another node. Unlike a WSFC, there is no way to set preferred owners, so Pacemaker picks the node that will be the new host for the FCI.

There are times you may want to manually fail the FCI to another node. The process is not the same as with FCIs on a WSFC. On a WSFC, you fail over resources at the role level. In Pacemaker, you choose a resource to move, and assuming all the constraints are correct, everything else will move as well. 

The way to failover depends on the Linux distribution. Follow the instructions for your linux distribution.

- [RHEL or Ubuntu](#rhelFailover)
- [SLES](#slesFailover)

## <a name = "#rhelFailover"></a> Manual Failover (RHEL or Ubuntu)

To perform a manual failover, onn Red Hat Enterprise Linux (RHEL) or Ubuntu servers execute the following steps.
1.	Issue the following command: 

   ```bash
   sudo pcs resource move <FCIResourceName> <NewHostNode> 
   ```

   \<FCIResourceName> is the Pacemaker resource name for the SQL Server FCI.

   \<NewHostNode> is the name of the cluster node that you want to host the FCI. 

   You will not get any acknowledgement.

2.	During a manual failover, Pacemaker creates a location constraint on the resource that was chosen to move manually. To see this constraint, run `sudo pcs constraint`.

3.	After the failover is complete, remove the constraint by issuing `sudo pcs resource clear <FCIResourceName>`. 

\<FCIResourceName> is the Pacemaker resource name for the FCI. 

## <a name = "#slesFailover"></a> Manual Failover (SLES)


In Suse Linux Enterprise Server (SLES), use the `migrate` command to manually failover a SQL Server FCI. For example:

```bash
crm resource migrate <FCIResourceName> <NewHostNode>
```

\<FCIResourceName> is the reource name for the failover cluster instance. 

\<NewHostNode> is the name of the new destination host. 


<!---
|Distribution |Topic 
|----- |-----
|**Red Hat Enterprise Linux with HA add-on** |[Configure](sql-server-linux-shared-disk-cluster-red-hat-7-configure.md)<br/>[Operate](sql-server-linux-shared-disk-cluster-red-hat-7-operate.md)
|**SUSE Linux Enterprise Server with HA add-on** |[Configure](sql-server-linux-shared-disk-cluster-sles-configure.md)
--->

## Next Steps

- [Configure failover cluster instance - SQL Server on Linux](sql-server-linux-shared-disk-cluster-configure.md)

<!--Image references-->
