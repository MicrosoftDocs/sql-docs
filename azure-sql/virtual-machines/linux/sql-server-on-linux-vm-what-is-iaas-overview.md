---
title: Overview of SQL Server on Azure Virtual Machines for Linux
description: Learn about how to run full SQL Server editions on Azure Virtual Machines for Linux. Get direct links to all Linux SQL Server VM images and related content.
author: MashaMSFT
ms.author: mathoma
ms.date: 09/23/2026
ms.service: azure-vm-sql-server
ms.subservice: service-overview
ms.topic: overview
ms.custom:
  - linux-related-content
tags: azure-service-management
ai-usage: ai-assisted
---
# Overview of SQL Server on Linux Azure Virtual Machines

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
>
> * [Windows](../windows/sql-server-on-azure-vm-iaas-what-is-overview.md)
> * [Linux](sql-server-on-linux-vm-what-is-iaas-overview.md)

SQL Server on Azure Virtual Machines enables you to use full versions of SQL Server in the cloud without having to manage any on-premises hardware. SQL Server VMs also simplify licensing costs when you pay as you go.

Azure virtual machines run in many different [geographic regions](https://azure.microsoft.com/regions/) around the world. They also offer a variety of [machine sizes](/azure/virtual-machines/sizes). When you create a Linux VM, you can choose the Linux distribution, SQL Server version, edition, and licensing model that fit your workload. This flexibility makes virtual machines a good option for many different SQL Server workloads.

If you're new to Azure SQL, check out the *SQL Server on Azure VM Overview* video from our in-depth [Azure SQL video series](/shows/Azure-SQL-for-Beginners?WT.mc_id=azuresql4beg_azuresql-ch9-niner):
> [!VIDEO https://learn.microsoft.com/shows/Azure-SQL-for-Beginners/SQL-Server-on-Azure-VM-Overview-4-of-61/player]

## Azure portal deployment

To create a SQL Server on Linux Azure VM, use the Azure portal virtual machine deployment experience. You start from a supported Linux base image, and Azure installs and configures SQL Server while it provisions the VM.

> [!NOTE]  
> The new script-based Azure portal virtual machine deployment for SQL Server on Linux Azure VMs is currently in preview.

> [!IMPORTANT]  
> Precreated SQL Server on Linux Azure Marketplace images are deprecated. They're no longer available for new deployments through the Azure portal, the Azure SQL hub, the Azure CLI, or Azure PowerShell.

The script-based SQL Server on Azure VM portal deployment experience offers the following benefits:

- **Flexibility and control**: Choose your Linux distribution, SQL Server version, edition, and features. You can also upload your own `mssql.conf` file to customize the SQL Server configuration.
- **Faster access to new versions**: New Linux versions and Azure capabilities become available without waiting for a new Marketplace image.
- **Automatic registration**: Azure registers every VM with the [SQL Server IaaS Agent extension](sql-server-iaas-agent-extension-linux.md) by default, which gives you licensing flexibility, compliance, and license visibility.
- **Guided configuration**: The Azure portal only shows valid combinations of operating system, SQL Server version, and licensing options.

The deployment follows these stages:

1. You choose a supported Linux base image and VM size.
1. You enable and configure SQL Server, including the version, edition, features, and licensing model.
1. Azure installs and configures SQL Server on the VM.
1. Azure registers the VM with the SQL Server IaaS Agent extension, with either the pay-as-you-go or Azure Hybrid Benefit license type.

You can start a deployment from either the **Create a virtual machine** page in the Azure portal or from the [Azure SQL hub](https://aka.ms/azuresqlhub). Both options lead to the same guided SQL Server configuration. For steps, see [Provision a Linux virtual machine running SQL Server in the Azure portal](sql-vm-create-portal-quickstart.md).

### Supported Linux distributions

Azure portal deployment supports the following Linux distributions. Azure automatically adds support for later versions as they become supported.

| Distribution | Supported versions |
| --- | --- |
| Red Hat Enterprise Linux (RHEL) | RHEL 9, RHEL 10 |
| Ubuntu | Ubuntu 22.04, Ubuntu 24.04, Ubuntu 26.04 |

Azure portal deployment doesn't support SUSE Linux Enterprise Server (SLES). To run SQL Server on SLES, create a SLES VM and [install SQL Server manually](#install-sql-server-manually).

### Install SQL Server manually

If you prefer to install SQL Server yourself, you can also:

1. [Provision a Linux VM on Azure](/azure/virtual-machines/linux/quick-create-portal).
1. [Install SQL Server on Linux](/sql/linux/install-upgrade/setup).
1. [Register the VM with the SQL IaaS Agent extension](sql-iaas-agent-extension-register-vm-linux.md) to enable management features.

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
- [SQL Server IaaS Agent extension for Linux](sql-server-iaas-agent-extension-linux.md)
- [SQL Server on Azure Virtual Machines FAQ](frequently-asked-questions-faq.yml)
