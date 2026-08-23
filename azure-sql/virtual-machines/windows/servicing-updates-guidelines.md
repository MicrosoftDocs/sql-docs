---
title: Updating SQL Server
description: Learn the supported methods to keep SQL Server up to date for SQL Server on Azure VMs, including Azure Update Manager, Automated Patching, custom images, winget for client tools, and post-deployment automation. SQL Server Marketplace images deploy at RTM and require post-deployment servicing.
author: dplessMSFT
ms.author: dpless
ms.reviewer: mathoma
ms.date: 03/31/2026
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: best-practice
tags: azure-service-management
---
# Updating SQL Server on Azure VMs

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article provides an overview of the different methods you can use to keep SQL Server up to date for [SQL Server on Azure virtual machines (VMs)](sql-server-on-azure-vm-iaas-what-is-overview.md).

## Overview

Azure Marketplace SQL Server images for SQL Server on Azure VMs are intentionally deployed at the Release to Manufacturing (RTM) version. These images provide a stable and predictable starting point and aren't intended to function as an ongoing patching or servicing mechanism.

Marketplace images aren't updated over time, and they don't remain current. You're responsible for updating the SQL Server instance after deploying the SQL Server VM, based on your organization's servicing and compliance requirements. RTM is a supported baseline for all currently supported versions of SQL Server. Microsoft delivers all security and stability fixes after RTM through supported servicing mechanisms, including Cumulative Updates (CUs) and General Distribution Releases (GDRs).

The rest of the article provides supported methods to keep your SQL Server VM updated. Choose the approach that best fits your operational model and governance requirements.

## Azure Update Manager (Recommended)

[Azure Update Manager](../azure-update-manager-sql-vm.md) is the recommended, first-party, Azure-wide service for patching Azure virtual machines, including SQL Server on Azure VMs. You can enable Azure Update Manager after you register your SQL Server VM with the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md).

By using Azure Update Manager, you can:
- Automatically install SQL Server Cumulative Updates (CUs)
- Automatically install security and critical OS updates
- Schedule updates within defined maintenance windows
- Patch SQL Server instances at scale across multiple VMs
- Perform on-demand or scheduled updates
- Monitor update compliance centrally

Azure Update Manager provides a consistent and scalable approach for keeping both the operating system and SQL Server instance current after deployment.

## Automated patching

[Automated patching](automated-patching.md) is available through the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md), but it's scheduled for retirement in September 2027. Don't use automated patching for new SQL Server VM deployments or in conjunction with Azure Update Manager. Use Azure Update Manager for a more robust, scalable, and long-term servicing solution.

Automated patching installs only Windows and SQL Server updates marked as **Important** or **Critical**. You must manually install other SQL Server updates that aren't marked as **Important** or **Critical**. To automatically install Cumulative Updates, use the integrated [Azure Update Manager](../azure-update-manager-sql-vm.md) experience.

## Customer-built or custom images

If your environment requires specific patches, build and maintain custom VM images that include:

- A specific SQL Server CU level
- Desired OS patch levels
- Additional tooling, such as SSMS or monitoring agents

This approach gives you full control over the deployed patch state but requires you to maintain and update the image lifecycle over time.

## Using Windows Package Manager (winget) for client tools and supporting software

Use [Windows Package Manager (winget)](/windows/package-manager/winget/) to install and update SQL Server client tools and supporting software for SQL Server on Azure VMs. This approach commonly automates post-deployment configuration or maintains tooling on custom images.

However, winget isn't meant to update the SQL Server Database Engine, including installing Cumulative Updates (CUs) or General Distribution Releases (GDRs). You must apply SQL Server engine updates by using supported SQL Server servicing mechanisms such as Azure Update Manager.

The following list shows supported use cases for winget:
- Installing or updating SQL Server Management Studio (SSMS)
- Installing command-line tools, SDKs, and utilities, such as Azure CLI or PowerShell modules
- Installing monitoring, diagnostics, or supporting agents available in the winget catalog
- Automating client-tool installation as part of:
  - Post-deployment scripts
  - Infrastructure-as-code workflows
  - Custom VM image pipelines

Use the following winget command to install SQL Server Management Studio (SSMS):

```PowerShell
winget install --id Microsoft.SQLServerManagementStudio -e
```

Use the following winget command to upgrade SSMS if a newer version is available:

```PowerShell
winget upgrade --id Microsoft.SQLServerManagementStudio
```

SQL Server Database Engine updates require supported SQL Server update mechanisms and are intentionally kept separate from general package-management tooling.

## Recommended pattern

The following pattern is a common and recommended servicing pattern for SQL Server on Azure VMs:

1. Deploy a Marketplace image as a clean RTM baseline.
1. Use Azure Update Manager to service the operating system and SQL Server engine.
1. Use winget to install and update client tools and supporting software.

This separation ensures SQL Server servicing remains fully supported, predictable, and compliant while still enabling flexible and automated tooling management.

## Post-deployment automation

You can apply SQL Server updates immediately after VM provisioning by using:
- PowerShell
- Azure CLI
- Desired State Configuration (DSC)
- Configuration management tools, such as Ansible, Chef, or Puppet

By using this method, you can retain the Marketplace image as a clean baseline while enforcing a required SQL Server version during deployment.

## Manual updates

You can also install SQL Server updates manually by downloading and applying updates directly from Microsoft. A manual approach is supported but is generally recommended only for small-scale or ad hoc scenarios.

## Related content

For detailed guidance on each optimization area:

- **[Quick checklist](performance-guidelines-best-practices-checklist.md)** - Review the full best practices checklist
- **[VM size](performance-guidelines-best-practices-vm-size.md)** - Choose the right VM series and configuration
- **[Storage](performance-guidelines-best-practices-storage.md)** - Optimize disk configuration and performance
- **[Security](security-considerations-best-practices.md)** - Implement security best practices
- **[HADR settings](hadr-cluster-best-practices.md)** - Configure high availability and disaster recovery
- **[Collect baseline](performance-guidelines-best-practices-collect-baseline.md)** - Establish performance baselines

For more information:

- [Azure Update Manager for SQL Server on Azure VMs](../azure-update-manager-sql-vm.md)
- [What is SQL Server on Azure VMs?](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [Provision a SQL Server VM in the Azure portal](create-sql-vm-portal.md)
- [What is the SQL Server IaaS Agent extension?](sql-server-iaas-agent-extension-automate-management.md)
- [Latest updates for SQL Server](/sql/database-engine/install-windows/latest-updates-for-microsoft-sql-server)

