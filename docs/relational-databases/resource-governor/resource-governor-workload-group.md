---
title: Resource Governor Workload Group
description: Resource governor uses a workload group as a container for requests that are subject to common policies.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 01/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: concept-article
helpviewer_keywords:
  - "Resource Governor, workload group"
  - "workload groups [SQL Server]"
  - "workload groups [SQL Server], overview"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Resource governor workload group

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

A resource governor workload group is a container for requests that are subject to common policies. A workload group supports aggregate monitoring of the sessions and requests, and defines request policies. Each workload group is in a [resource pool](resource-governor-resource-pool.md), which represents a subset of the physical resources of a [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance.

When a session is started, it is classified into a specific workload group. Workload group policies govern the requests executing on a session. Requests use the resources from the underlying resource pool.

## Workload group concepts

A workload group serves as a container for sessions that are similar according to the classification criteria applied to each session. A workload group allows the aggregate monitoring of resource consumption and the use of common policies to all requests in the group. For example, you can limit the degree of parallelism or the maximum size of a memory grant for each query executing in a workload group.

Resource governor has two built-in workload groups: the `internal` group and the `default` group. You can't change the classification policies of the `internal` group, but you can monitor it.

Sessions are classified into the `default` group if:

- A [classifier function](resource-governor-classifier-function.md) doesn't exist.
- There is an attempt to classify the session into a nonexistent group.
- There is a general classification failure.

## Workload group tasks

Resource pools can govern a variety of system resources. For more information, see [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md).

For more samples and a complete walkthrough, see [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md).

| Task description | Article |
|:--|:--|
| Create a workload group | [Create a workload group](create-a-workload-group.md) |
| Change workload group settings | [Change workload group settings](change-workload-group-settings.md) |
| Delete a workload group | [Delete a workload group](delete-a-workload-group.md) |
| Move a workload group | [Move a workload group](move-a-workload-group.md) |

Resource governor provides DDL statements for creating, modifying, and deleting workload groups.

For more information, including the policies that can be specified for a workload group, see:

- [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md)
- [ALTER WORKLOAD GROUP](../../t-sql/statements/alter-workload-group-transact-sql.md)
- [DROP WORKLOAD GROUP](../../t-sql/statements/drop-workload-group-transact-sql.md)

## Related content

- [Resource governor](resource-governor.md)
- [Enable resource governor](enable-resource-governor.md)
- [Resource governor resource pool](resource-governor-resource-pool.md)
- [Resource governor classifier function](resource-governor-classifier-function.md)
- [Configure resource governor using a template](configure-resource-governor-using-a-template.md)
- [View and modify resource governor properties](view-resource-governor-properties.md)
