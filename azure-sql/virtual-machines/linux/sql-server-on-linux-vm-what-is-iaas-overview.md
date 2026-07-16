---
title: Overview of SQL Server on Azure Virtual Machines for Linux
description: Learn about how to run full SQL Server editions on Azure Virtual Machines for Linux. Get direct links to all Linux SQL Server VM images and related content.
author: MashaMSFT
ms.author: mathoma
ms.date: 04/02/2026
ms.service: azure-vm-sql-server
ms.subservice: service-overview
ms.topic: overview
ms.custom:
  - linux-related-content
tags: azure-service-management
---
# Overview of SQL Server on Linux Azure Virtual Machines

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
>
> * [Windows](../windows/sql-server-on-azure-vm-iaas-what-is-overview.md)
> * [Linux](sql-server-on-linux-vm-what-is-iaas-overview.md)

SQL Server on Azure Virtual Machines enables you to use full versions of SQL Server in the cloud without having to manage any on-premises hardware. SQL Server VMs also simplify licensing costs when you pay as you go.

Azure virtual machines run in many different [geographic regions](https://azure.microsoft.com/regions/) around the world. They also offer a variety of [machine sizes](/azure/virtual-machines/sizes). The virtual machine image gallery allows you to create a SQL Server VM with the right version, edition, and operating system. This makes virtual machines a good option for many different SQL Server workloads.

If you're new to Azure SQL, check out the *SQL Server on Azure VM Overview* video from our in-depth [Azure SQL video series](/shows/Azure-SQL-for-Beginners?WT.mc_id=azuresql4beg_azuresql-ch9-niner):
> [!VIDEO https://learn.microsoft.com/shows/Azure-SQL-for-Beginners/SQL-Server-on-Azure-VM-Overview-4-of-61/player]

## Get started with SQL Server on Azure Linux VMs

To get started with SQL Server on Azure Linux VMs, follow these steps:

1. [Provision a Linux VM on Azure](/azure/virtual-machines/linux/create-upload-generic)
1. [Install SQL Server 2017 or later on Linux](/sql/linux/install-upgrade/setup).
1. Register with the [SQL IaaS Agent extension for SQL Server on Azure Linux Virtual Machines](sql-iaas-agent-extension-register-vm-linux.md) to enable management features.

> [!NOTE]  
> SQL Server on Azure Linux VMs is transitioning to a script-based model that offers greater flexibility, automation, and control. To learn more, review the [Upcoming changes to SQL Server on Linux VMs blog](https://techcommunity.microsoft.com/blog/sqlserver/announcement-upcoming-changes-to-sql-server-on-linux-virtual-machine-vm-provisio/4457324).

## Related products and services

### AI capabilities and features

- [Intelligent applications and AI in SQL Server](/sql/sql-server/ai/artificial-intelligence-intelligent-applications?view=azuresqldb-mi-current&preserve-view=true)
- [Multi-model capabilities in Azure SQL](../../multi-model-features.md)
- [Connect to REST API endpoints for a SQL database](/azure/data-api-builder/concept/rest/overview)
- [Connect to GraphQL endpoints for a SQL database](/azure/data-api-builder/concept/graphql/overview)
- [SQL MCP Server](/azure/data-api-builder/mcp/overview)

### Linux virtual machines

- [Azure Virtual Machines overview](/azure/virtual-machines/linux/overview)

### Storage

- [Introduction to Microsoft Azure Storage](/azure/storage/common/storage-introduction)

### Networking

- [Virtual Network overview](/azure/virtual-network/virtual-networks-overview)
- [IP addresses in Azure](/azure/virtual-network/ip-services/public-ip-addresses)
- [Create a Fully Qualified Domain Name in the Azure portal](/azure/virtual-machines/create-fqdn)

### SQL

- [SQL Server on Linux documentation](/sql/linux)
- [Azure SQL Database comparison](../../azure-sql-iaas-vs-paas-what-is-overview.md)

## Related content

- [Provision a Linux virtual machine running SQL Server in the Azure portal](sql-vm-create-portal-quickstart.md)
- [SQL Server on Azure Virtual Machines FAQ](frequently-asked-questions-faq.yml)
