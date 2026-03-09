---
title: Modernization Advisor (Preview)
description: This article explains the Modernization Advisor, which helps you assess whether Azure SQL Managed Instance is a more cost-effective option for your business than SQL Server on Azure VMs. 
author: danimir
ms.author: danil
ms.reviewer: mathoma
ms.date: 01/28/2025
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: how-to
ms.custom: devx-track-azurepowershell
tags: azure-resource-manager
---

# Modernization Advisor (Preview) - SQL Server on Azure VMs
[!INCLUDE[appliesto-sqlvm](../includes/appliesto-sqlvm.md)]

This article provides an overview of the Modernization Advisor, which assesses your SQL Server on Azure virtual machines (VMs) configuration to determine if you can save on cost, or optimize your performance, by migrating to [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md).  

> [!NOTE]
> Modernization Advisor is currently in [preview](windows/doc-changes-updates-release-notes-whats-new.md#preview) for SQL Server on Azure VMs.

## Overview 

Modernization Advisor is a tool built into the Azure portal that helps you assess whether Azure SQL Managed Instance is a more cost-effective option for your business than SQL Server on Azure VMs. Depending on your SQL Server configuration, you may save money, optimize performance, and simplify management by migrating from SQL Server on Azure VMs to Azure SQL Managed Instance. 

Modernization Advisor compares the configuration of your SQL Server on Azure VM, and then provides a list of benefits you can achieve by migrating to Azure SQL Managed Instance. 

By using the Modernization Advisor, you can: 

- Identify SQL Server configurations that are suitable for migration to Azure SQL Managed Instance.
- Easily match the configuration of Azure SQL Managed Instance to your SQL Server VM configuration.
- Determine a price range for modernizing your SQL Server workload to SQL Managed Instance for a reduced total cost of ownership (TCO).
- Get a detailed report of the benefits you can achieve by migrating to Azure SQL Managed Instance.

Modernization Advisor evaluates the following SQL Server VM resources to match with a suitable Azure SQL Managed Instance configuration:

- The number of vCores
- Memory used per vCore
- Storage size
- Storage type (Standard, Premium SSD, Premium SSD v2)

## Potential migration benefits 

Take advantage of the following benefits by migrating to Azure SQL Managed Instance:

- Fully managed Platform-as-a-Service (PaaS) offering with built-in high availability, and intelligent performance optimization.
- 24x7 service health monitoring and automitigation services, with Microsoft engineers monitoring and managing the service to ensure stability. 
- Easily and dynamically scale, add, or remove resources such as CPU, memory, IO, and storage as online operations without downtime. 
- No need to enable or configure backups or patching as both are fully automated in Azure SQL Managed Instance. 
- Zone-redundant storage increases the SLA of Azure SQL Managed Instance higher than the SLA for SQL Server on Azure VMs, since the SLA is for the virtual machine, and not the SQL service.
- Retain your backups up to 10 years with long-term retention.

## Prerequisites

To use the Modernization Advisor, your SQL Server VM must be registered with the [SQL IaaS Agent extension](windows/sql-server-iaas-agent-extension-automate-management.md). 

## View recommendations

Although the Modernization Advisor is available to all SQL Server VM customers, recommendations are only visible to customers with configurations suitable for migration to Azure SQL Managed Instance. 

If you don't see the **Compare** button on the **Modernization Advisor (preview)** pane, your workload isn't a suitable migration candidate. 

To view Modernization Advisor recommendations, go to your [SQL virtual machines](windows/manage-sql-vm-portal.md) resource in the Azure portal, and choose **Modernization Advisor (preview)** under **Settings** to open the **Modernization Advisor (preview)** pane. 

:::image type="content" source="media/modernization-advisor/modernization-advisor.png" alt-text="Screenshot of the Modernization Advisor in the Azure portal.":::

If your SQL Server VM qualifies for modernization, then you can also navigate to the **Modernization Advisor (preview)** pane from: 
- a recommendation on the **Recommendations** tab of the **Overview** pane of your [SQL virtual machine](windows/manage-sql-vm-portal.md) resource. Select the recommendation to learn more, and then select **Evaluate modernizing this SQL Server** to open the **Modernization Advisor (preview)** pane.
- a banner on the **Overview** pane of your [SQL virtual machine](windows/manage-sql-vm-portal.md) resource advising you to evaluate modernizing this SQL Server. Select the banner to open the **Modernization Advisor (preview)** pane.

The following options are available on the **Modernization Advisor (preview)** pane: 

- **Compare your SQL VM with Azure SQL Managed Instance**: use this option to compare your SQL Server VM configuration with a target Azure SQL Managed Instance. This option is only available if your SQL Server VM is a suitable migration candidate.
- **Discover benefits of Azure SQL Managed Instance**: use this option to open the **Discover Azure SQL Managed Instance** pane to then **Create a free or paid instance**. 

> [!NOTE]
> Running a comparison uses operation Azure VM telemetry without accessing any of your personal data. 

Selecting **Compare your SQL VM with Azure SQL Managed Instance** displays the **Comparison results** on the **Modernization Advisor (preview)** pane: 

:::image type="content" source="media/modernization-advisor/modernization-advisor-results.png" alt-text="Screenshot that shows the comparison results of Modernization Advisor in the Azure portal.":::

Selecting **Discover benefits of Azure SQL Managed Instance** opens the **Discover Azure SQL Managed Instance** pane, where you can navigate to the **Create a free or paid instance** page, and learn how you can restore your SQL Server database to Azure SQL Managed Instance. 

## Create instance 

After you decide to migrate to Azure SQL Managed Instance, [create your instance](../managed-instance/instance-create-quickstart.md) from the [Azure portal](https://portal.azure.com/#create/Microsoft.SQLManagedInstance), which you can [evaluate for free](../managed-instance/free-offer.md) for up to 12 months. 

## Restore database to Azure SQL Managed Instance

You can use the following methods to migrate a SQL Server database to Azure SQL Managed Instance: 
- [Backup and restore](../managed-instance/restore-sample-database-quickstart.md)
- [Log Replay Service](../managed-instance/log-replay-service-overview.md)
- [Managed Instance link](../managed-instance/managed-instance-link-migrate.md)

## Provide feedback

Use the **Feedback** button from the command bar of the **Modernization Advisor (preview)** pane to provide feedback for the Modernization Advisor directly to the product group: 

:::image type="content" source="media/modernization-advisor/modernization-advisor-feedback.png" alt-text="Screenshot highlighting the feedback button on the Modernization Advisor page in the Azure portal.":::

## Related content

- [Create an instance](../managed-instance/instance-create-quickstart.md)
- [Evaluate Azure SQL Managed Instance for free](../managed-instance/free-offer.md)