---
title: View Always On failover cluster instances (preview)
description: In this article, you learn how you can view and manage SQL Server instances enabled by Azure Arc that are configured as a failover cluster.
author: AbdullahMSFT
ms.author: amamun 
ms.reviewer: mikeray, randolphwest
ms.date: 10/06/2023
ms.topic: conceptual
---

# View Always On failover cluster instances in Azure Arc (preview)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Always On failover clustering is a high availability technology that leverages Windows Server failover clustering to provide local redundancy at an instance level.

[!INCLUDE [azure-arc-sql-preview](includes/azure-arc-sql-preview.md)]

If the [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] is configured as a failover cluster instance, additional information about the host Windows failover cluster is also shown in the Azure portal. The Azure SQL extension agent on each of the nodes participating in the Windows cluster recognizes the installed SQL binaries as an installation of SQL Server. The agent projects the installation into Azure as a [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] resource. In addition to each of these [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] resources, the clustered resource also gets projected into Azure, also as [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] resource type. This means that a cluster with `n` nodes projects `n+1` [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] resources.

## View cluster configuration, SQL Server, and database properties

All the information about the SQL Server configuration such as server configuration, database inventory and metadata, backups, cluster configuration, Defender status etc. is available on the clustered resource. The clustered resource can be differentiated from the other resources in the resource group by one of two ways:

 - The name of the Azure Arc-enabled SQL resource following the pattern `<NetworkName>_<InstanceName>`.
 - The `Always On role` property in the Essentials pane in Azure portal would show `Failover cluster instance`.

In addition, the database resources are nested under the clustered resource. For example, `<DatabaseName> (<NetworkName>_<InstanceName>/<DatabaseName>)`.

### Cluster configuration

To view the cluster configuration:

1. Browse to the [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] resource named `<NetworkName>_<InstanceName>`
1. Select **Overview**
1. Select **Properties**

The portal displays the host cluster configuration under `Properties`.

### View databases

To view the cluster configuration:

1. Browse to the [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] resource named `<NetworkName>_<InstanceName>`
1. Select on `Databases` tab

The portal displays the databases on the SQL Server instance.

## Limitations

- All the Windows and SQL Server instances that are part of the failover clustering should be in same resource group.
- Always On availability groups on a failover cluster instance aren't supported for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] at this time.
- Currently, best practices assessment isn't supported with Always On failover cluster instance.
- Automated backups and point-in-time restore isn't supported for failover cluster instances at this time.
- SQL failover cluster instances with multiple network names are not supported at this time.
- For extension versions older than `1.1.2620.127`, SQL failover cluster instances which use a different network name than the one configured during installation are not supported.

## Related tasks

- [View SQL Server databases - Azure Arc](view-databases.md)
