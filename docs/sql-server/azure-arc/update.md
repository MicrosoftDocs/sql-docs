---
title: Configure automatic updates
description: This article explains how to configure SQL Server enabled by Azure Arc automatic updates.
author: AbdullahMSFT
ms.author: amamun 
ms.reviewer: mikeray
ms.date: 07/29/2024
ms.topic: conceptual 
---

# Configure automatic updates for SQL Server instances enabled for Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

You can configure automatic updates for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)].

Automatic updates:

- Establishes a maintenance window for an Arc-enabled SQL Server instance
- Works at the host operating system level and applies to all installed SQL Server instances
- Only installs automated updates during this maintenance window 

   For SQL Server, this restriction ensures that system updates and any associated restarts occur at the best possible time for the SQL Server instances and their hosted databases.

- Currently only works on Windows hosts

   It configures Windows Update/Microsoft Update which is the service that ultimately updates the service.

- Applies Windows and SQL Server updates marked as **Important** or **Critical**

> [!IMPORTANT]
> Only Windows and SQL Server updates marked as **Important** or **Critical** are installed. Other SQL Server updates, such as service packs and cumulative updates that are not marked as **Important** or **Critical**, must be installed manually.

## Settings

To configure automatic updates, use:

- Azure portal
- Programmatically or by policy

The following table describes the options that you may configure for automatic updates. 

| Setting | Possible values | Description |
| --- | --- | --- |
| **Automatic updates** |Enable \| **Disable** | Enables or disables automatic updates. |
| **Maintenance schedule** | Daily \| Sunday \| Monday \| Tuesday \| Wednesday \| Thursday \| Friday \| Saturday |The weekly schedule for downloading and installing Windows, SQL Server, and Microsoft updates. |
| **Maintenance start hour** |0:00-23:00 |The local start time to apply updates. |

## Configure in the Azure portal

Use the Azure portal to configure automatic updates for existing Arc-enabled SQL Server instances.

1. In the portal, locate the **Server - Azure Arc**.
1. Under **Operations**, select **SQL Server Configuration**.
1. Under **Update**, you can enable or disable automatic updates and set a maintenance schedule.

When you enable or configure automatic updates, Azure configures the Azure Extension for SQL Server in the background.

> [!NOTE]
> Automatic updates only applies to servers covered with Software Assurance or pay-as-you-go license type. If the server license type is license only, the option to automate updates is disabled.
>
> To change the license type to license only, follow these steps:
>
> 1. Unsubscribe from automatic updates and ESU if it is enabled.
> 1. Save the change.
> 1. Wait approximately 5 minutes for the saved change to complete.
> 1. Set the new license type.
>

## Manage programmatically or by policy

To manage automatic updates programmatically or by policy, review the information in the following resources:

- [Manage updates using Azure REST API](/azure/update-manager/manage-arc-enabled-servers-programmatically?tabs=cli%2Crest#update-deployment)
- [Trigger an update assessment](/azure/update-manager/manage-arc-enabled-servers-programmatically?tabs=cli%2Crest#update-assessment)
- [Enable Microsoft updates (to enable SQL updates)](/azure/update-manager/configure-wu-agent#enable-updates-for-other-microsoft-products)
- [Enable Azure Update Manager via Azure policy](/azure/update-manager/tutorial-assessment-deployment-using-policy)

## Next steps

[Manage SQL Server license and billing options](manage-configuration.md)