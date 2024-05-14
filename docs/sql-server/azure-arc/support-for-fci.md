---
title: View Always On failover cluster instances
description: In this article, you learn how you can view and manage SQL Server instances enabled by Azure Arc that are configured as a failover cluster.
author: AbdullahMSFT
ms.author: amamun 
ms.reviewer: mikeray, randolphwest
ms.date: 05/08/2024
ms.topic: conceptual
---

# View Always On failover cluster instances in Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

An Always On failover cluster is a high availability technology that uses Windows Server failover clustering to provide local redundancy at an instance level. A failover cluster instance is a logical resource that may run on any one of the clustered server resources.

If the [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] is configured as a failover cluster instance, additional information about the host Windows failover cluster is also shown in the Azure portal. The Azure SQL extension agent on each of the nodes participating in the Windows cluster recognizes the installed SQL binaries as an installation of SQL Server. The agent projects the installation into Azure as a [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] resource.

[!INCLUDE [latest-features](includes/latest-features.md)]

## View cluster configuration, SQL Server, and database properties

In Azure portal, **Azure Arc | SQL Server instances** lists all instances of SQL Server that are enabled by Azure Arc.

### List failover cluster instances

To list failover cluster instances:

1. Select **Add filter**
1. Set **Filter** to *SQL Server instance type* equals **Failover Cluster Instance**
1. Select **Apply**

   :::image type="content" source="media/support-for-fci/filter-portal.png" alt-text="Screenshot of Azure portal for Azure Arc SQL Server add filter control." lightbox="media/support-for-fci/filter-portal-expanded.png":::

The portal returns only the failover cluster instances.

The portal presents the instance names as:

- Default instance `<NetworkName>` 
- Named instance `<NetworkName>_<InstanceName>`

### View failover cluster instance

To view the properties of a failover cluster instance:

1. Select a **SQL Server - Azure Arc** resource that is a failover cluster instance type
1. Select **Overview**
1. Review the properties under **Essentials**

   :::image type="content" source="media/support-for-fci/essentials.png" alt-text="Screenshot of Azure portal for failover cluster instance enabled by Azure Arc." lightbox="media/support-for-fci/essentials-expanded.png":::

The portal describes the failover cluster instance. For example:

- Instance name
- Instance type
- Network name
- Active nodes
- Passive nodes

Active or passive node information is current as of **Inventory upload** time. A change in active node after this time may not be reflected in this view.

The clustered resource is distinct from other resources in the resource group in two ways:

- In **Overview** > **Essentials**, the **SQL Server Instance type** property is `Failover cluster instance`
- The name of the SQL resource is either:
  - `<NetworkName>`
  - `<NetworkName>_<InstanceName>`

In addition, the database resources are nested under the clustered resource. For example, `<DatabaseName> (<NetworkName>_<InstanceName>/<DatabaseName>)`.

### View databases

To view the databases on a failover cluster instance:

1. Browse to the resource - either `<NetworkName>` or `<NetworkName>_<InstanceName>`
1. Select **Data management** > **Database**

The portal displays the databases on the SQL Server instance.

Note You can see all the resoursces like Network name, databases and all the nodes in the corresponding resource group.  

## Limitations

- All the Windows and SQL Server instances that are part of the failover clustering should be in same resource group.
- Always On availability groups on a failover cluster instance aren't supported for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] at this time.
- Currently, best practices assessment isn't supported with Always On failover cluster instance.
- Automated backups and point-in-time restore isn't supported for failover cluster instances at this time.
- SQL failover cluster instances with multiple network names aren't supported at this time.
- For extension versions older than `1.1.2620.127`, SQL failover cluster instances with a different network name than the one configured during installation aren't supported.

## Related tasks

- [View SQL Server databases - Azure Arc](view-databases.md)
