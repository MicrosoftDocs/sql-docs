---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 08/28/2025
ms.service: sql
ms.topic: include
---

Certain operations, including configuring server (instance level) settings, or manually adding a database to an availability group, require a connection to the SQL Server instance. Operations like `sp_configure`, `RESTORE DATABASE`, or any DDL command in a database belonging to an availability group require a connection to the SQL Server instance. By default, a big data cluster doesn't include an endpoint that enables a connection to the instance. You must expose this endpoint manually.

For instructions, see [Connect to databases on the primary replica](../big-data-cluster/deployment-high-availability.md#instance-connect).
