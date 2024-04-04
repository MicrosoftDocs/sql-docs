---
title: Manually fail an FCI over - SQL Server on Linux
description: Learn to manually fail a failover cluster instance (FCI) on SQL Server on Linux, specifically Red Hat Linux Enterprise, Ubuntu, and SUSE Linux Enterprise Server.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 11/16/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Operate failover cluster instance - SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article explains how to operate a SQL Server failover cluster instance (FCI) on Linux. If you haven't created a SQL Server FCI on Linux, see [Configure failover cluster instance - SQL Server on Linux (RHEL)](sql-server-linux-shared-disk-cluster-configure.md).

## Failover

Failover for FCIs is similar to a Windows Server failover cluster (WSFC). If the cluster node hosting the FCI experiences some sort of failure, the FCI should automatically fail over to another node. Unlike a WSFC, there's no way to set preferred owners, so Pacemaker picks the node that will be the new host for the FCI.

There are times you might want to manually fail the FCI to another node. The process isn't the same as with FCIs on a WSFC. On a WSFC, you fail over resources at the role level. In Pacemaker, you choose a resource to move, and assuming all the constraints are correct, everything else will move as well.

The way to failover depends on the Linux distribution. Follow the instructions for your linux distribution.

- [RHEL or Ubuntu](#manual-failover-rhel-or-ubuntu)
- [SLES](#manual-failover-sles)

## Manual Failover (RHEL or Ubuntu)

To perform a manual failover on Red Hat Enterprise Linux (RHEL) or Ubuntu servers, execute the following steps.

1. Issue the following command:

   ```bash
   sudo pcs resource move <FCIResourceName> <NewHostNode>
   ```

   \<FCIResourceName> is the Pacemaker resource name for the SQL Server FCI.

   \<NewHostNode> is the name of the cluster node that you want to host the FCI.

   You'll not get any acknowledgment.

1. During a manual failover, Pacemaker creates a location constraint on the resource that was chosen to move manually. To see this constraint, run `sudo pcs constraint`.

1. After the failover is complete, remove the constraint by issuing `sudo pcs resource clear <FCIResourceName>`.

\<FCIResourceName> is the Pacemaker resource name for the FCI.

## Manual Failover (SLES)

In SUSE Linux Enterprise Server (SLES), use the `migrate` command to manually fail over a SQL Server FCI. For example:

```bash
crm resource migrate <FCIResourceName> <NewHostNode>
```

\<FCIResourceName> is the resource name for the failover cluster instance.

\<NewHostNode> is the name of the new destination host.

## Related content

- [Configure failover cluster instance - SQL Server on Linux (RHEL)](sql-server-linux-shared-disk-cluster-configure.md)
