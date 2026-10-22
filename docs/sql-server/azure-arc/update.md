---
title: Configure Automatic Updates
description: This article explains how to configure automatic updates for SQL Server enabled by Azure Arc.
author: AbdullahMSFT
ms.author: amamun
ms.reviewer: vanto
ms.date: 07/03/2025
ms.topic: how-to
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

## Check automatic extension upgrade history 

You can use the Azure activity log to identify when automatic updates were applied to an Azure Arc-enabled SQL Server instance. To view the activity log, follow these steps:

1. Go to your [SQL Server instance enabled by Azure Arc resource](https://portal.azure.com/#view/Microsoft_Azure_ArcCenterUX/ArcCenterMenuBlade/~/sqlServerInstances) in the Azure portal.
1. Select **Activity log** from the resource menu:

   :::image type="content" source="media/migrate-to-azure-sql-managed-instance-troubleshoot/activity-log.png" alt-text="Screenshot of the activity log highlighted for a SQL Server instance resource in the Azure portal.":::

1. Use **+ Add filter** to add a filter for the **Operation** of `Upgrade Extensions on Azure Arc machines` to identify when the extension was automatically updated.

You can also access the subscription-level activity log for a broader view of events across all resources in your subscription by selecting the notification bell icon of the top navigation bar and then selecting **More events in the activity log**:

:::image type="content" source="media/migrate-to-azure-sql-managed-instance-troubleshoot/notification-bell.png" alt-text="Screenshot of the notification bell icon highlighted in the Azure portal.":::

Filter by the `Upgrade Extensions on Azure Arc machines` **Operation** to identify when the extension was automatically updated across all resources in your subscription.

## Related content

- [Configure SQL Server enabled by Azure Arc](manage-configuration.md)
