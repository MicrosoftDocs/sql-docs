---
title: Configure Resource Governor Using a Template
description: Learn how to configure resource governor by using a template that is provided in SQL Server Management Studio.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 01/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "Resource governor, templates"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Configure resource governor using a template

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

You can configure resource governor using a template that is provided in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

<a id="BeforeYouBegin"></a>

Use the following steps to open and modify a template that creates a resource pool and a workload group for the pool. In addition, this template enables you to create a classifier function that assigns new connections to either the default group or the workload group that you create.

<a id="Permissions"></a>

## Permissions

The resource governor [!INCLUDE[tsql](../../includes/tsql-md.md)] statements in the template require the `CONTROL SERVER` permission.

<a id="ConfRGTemplate"></a>

## Configure resource governor using a template

To configure resource governor using a template in [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms):

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open the **View** menu and select **Template Explorer**.
1. In **Template Explorer**, expand **Resource governor**, and then select **Configure Resource governor**.
1. In **Connect to Database Engine**, enter the required information, and then select **OK**. the template `Configure Resource governor.sql` opens in the query editor. Use this template to create and configure a resource pool, a workload group, and a classifier function.
1. To change the values in the template, use the `Control+Shift+M` key combination. In the **Specify Values for Template Parameters** window, enter the values that you want to use.
1. To populate the template with the values you entered, select **OK**.
1. To execute the query, select **Execute**.

## Related content

- [Resource governor](resource-governor.md)
- [Enable resource governor](enable-resource-governor.md)
- [Resource governor resource pool](resource-governor-resource-pool.md)
- [Resource governor workload group](resource-governor-workload-group.md)
- [Resource governor classifier function](resource-governor-classifier-function.md)
- [View and modify resource governor properties](view-resource-governor-properties.md)
- [CREATE RESOURCE POOL (Transact-SQL)](../../t-sql/statements/create-resource-pool-transact-sql.md)
- [CREATE WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/create-workload-group-transact-sql.md)
- [CREATE FUNCTION (Transact-SQL)](../../t-sql/statements/create-function-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../../t-sql/statements/alter-resource-governor-transact-sql.md)
