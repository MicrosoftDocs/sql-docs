---
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 09/14/2026
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
---
In [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] with Cumulative Update (CU) 3 and later versions, a new Pacemaker HA agent v2 is available for Red Hat Enterprise Linux (RHEL) and Ubuntu in the `mssql-server-ha` package.

Pacemaker HA agent v2 introduces reliability and performance improvements over the previous agent, including:

- Improved failover performance to reduce both planned and unplanned failover times.

- Support for flexible automatic failover policies, including configuration of [health-check timeout](../../database-engine/availability-groups/windows/configure-flexible-automatic-failover-policy.md#HCtimeout) and [failure-condition level](../../database-engine/availability-groups/windows/configure-flexible-automatic-failover-policy.md#failure-condition-level).

- Support for TLS 1.3 for communication between the Pacemaker cluster and SQL Server.

Pacemaker HA agent v2 is currently in preview. The existing Pacemaker HA agent (v1) remains fully supported for production deployments.
