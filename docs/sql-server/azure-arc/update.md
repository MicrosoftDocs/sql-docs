---
title: Configure automatic updates
description: This article explains how to configure automatic updates for SQL Server enabled by Azure Arc.
author: AbdullahMSFT
ms.author: amamun
ms.reviewer: mikeray, randolphwest
ms.date: 09/09/2024
ms.topic: conceptual
---

# Configure automatic updates for SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

You can configure automatic updates for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]. Automatic updates:

- Establish a maintenance window for an Azure Arc-enabled SQL Server instance.

- Work at the level of the host operating system and apply to all installed SQL Server instances.

- Occur only during the maintenance window.

  This restriction ensures that system updates and any associated restarts happen at the best possible time for the SQL Server instances and their hosted databases.

- Currently work only on Windows hosts.

  They configure Windows Update and Microsoft Update, which are the services that ultimately update an Azure Arc-enabled SQL Server instance.

- Apply Windows and SQL Server updates marked as **Important** or **Critical**.

  You must manually install other SQL Server updates, such as service packs and cumulative updates that aren't marked as **Important** or **Critical**.

## Settings

You can configure automatic updates:

- By using the Azure portal.
- Programmatically or by policy.

The following table describes the options that you can configure for automatic updates.

| Setting | Possible values | Description |
| --- | --- | --- |
| **Automatic updates** | **Enable** \| **Disable** | Enables or disables automatic updates. |
| **Maintenance schedule** | **Daily** \| **Sunday** \| **Monday** \| **Tuesday** \| **Wednesday** \| **Thursday** \| **Friday** \| **Saturday** | The weekly schedule for downloading and installing Windows, SQL Server, and Microsoft updates. |
| **Maintenance start hour** | **0:00**-**23:00** | The local start time to apply updates. |

## Configure updates in the Azure portal

You can use the Azure portal to configure automatic updates for existing Azure Arc-enabled SQL Server instances:

1. In the portal, locate **Server - Azure Arc**.
1. Under **Operations**, select **SQL Server Configuration**.
1. Under **Update**, you can enable or disable automatic updates and set a maintenance schedule.

When you enable or configure automatic updates, Azure configures Azure Extension for SQL Server in the background.

Automatic updates apply only to servers covered with Software Assurance or the pay-as-you-go license type. If the server license type is license only, the option to automate updates is disabled.

To change the license type, follow these steps:

1. Unsubscribe from automatic updates and Extended Security Updates if they're enabled.
1. Save the change.
1. Wait approximately five minutes for the saved change to finish.
1. Set the new license type.

## Manage updates programmatically or by policy

To manage automatic updates programmatically or by policy, review the information in the following resources:

- [Manage updates by using the Azure REST API](/azure/update-manager/manage-arc-enabled-servers-programmatically?tabs=cli%2Crest#update-deployment)
- [Trigger an update assessment](/azure/update-manager/manage-arc-enabled-servers-programmatically?tabs=cli%2Crest#update-assessment)
- [Enable Microsoft updates (to enable SQL Server updates)](/azure/update-manager/configure-wu-agent#enable-updates-for-other-microsoft-products)
- [Enable Azure Update Manager via Azure policy](/azure/update-manager/tutorial-assessment-deployment-using-policy)

## Related content

- [Configure SQL Server enabled by Azure Arc](manage-configuration.md)
