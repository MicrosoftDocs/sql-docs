---
title: Move resources to a new resource group or subscription
description: This article explains how to move resources to a new resource group or subscription for SQL Server enabled by Azure Arc.
author: ntakru 
ms.author: nikitatakru
ms.reviewer: mikeray
ms.date: 04/04/2024
ms.topic: conceptual
---


# Move SQL Server enabled by Azure Arc resources to a new resource group or subscription (preview)

This article describes how you can move resources to a new resource group or subscription by using [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]. The capability applies to both SQL Server instances and databases.

At this time, this feature is available for preview.

[!INCLUDE [azure-arc-sql-preview](includes/azure-arc-sql-preview.md)]

## Prerequsites

To complete this task, make sure that the *Machine - Azure Arc* resource and all SQL Server instances are in the same resource group.

In addition:

- If Microsoft Purview is enabled, you must disable it in the compliance portal before the move.
- If best practices assessment is enabled, you must disable it before the move.

After the move, you can reenable any features that you disabled.

## Move resources to a new resource group or subscription

1. In the Azure portal, locate the resource group.

1. Select the server.

   The server resource type is **Machine - Azure Arc**. Don't select any other types of resources.

   :::image type="content" source="media/move-resources/machine-azure-arc.png" alt-text="Screenshot a list of Azure Arc-enabled resources in the portal.":::

   If the SQL Server instance is a failover cluster instance, select all of the **Machine - Azure Arc** server resources for the active and inactive nodes.

1. Select **Move** > **Move to another resource group** or **Move to another subscription**.
1. Under **Move resources**, provide the required information. Select **Next**.
1. Review the information and select **Move**.

Wait for the resource move to finish. It takes resources up to one hour to move items and reflect the new location in the portal.

## Verify that the move is complete

Verify that the Azure Arc-enabled SQL Server instances and associated databases are in the new resource group or subscription.

## Enable features

Enable any features that you disabled before the move.

The following table identifies features that remain enabled after the resource move, along with features that you need to reenable.

|Feature |Before move |After move |
|:----|:----|:----|
|Patching  |Enabled |Enabled |
|Extended Security Updates |Enabled |Enabled |
|License type |Enabled |Enabled |
|Viewing databases  |Enabled |Enabled |
|Viewing availability groups |Enabled |Enabled |
|Viewing failover cluster instances  |Enabled |Enabled |
|Automated backups |Enabled |Enabled |
|Monitoring dashboards |Enabled |Enabled |
|Microsoft Entra ID |Enabled |Enabled |
|Microsoft Purview |Enabled |Disabled. To enable, review [Connect to and manage Azure Arc-enabled SQL Server in Microsoft Purview](/purview/register-scan-azure-arc-enabled-sql-server).|
|Best practices assessment |Enabled |Disabled. Review the following section about enabling best practices assessment.|
|Microsoft Defender |Enabled |Disabled. To enable, review [Protect SQL Server with Microsoft Defender for Cloud](configure-advanced-data-security.md).|

## Enable best practices assessment

The way to enable best practices assessment depends on whether the resource moved resource groups or subscriptions.

### Resources moved within the same subscription

1. Before the move, disable best practices assessment.
2. Move the resource.
3. [Enable best practices assessment](assess.md#enable-best-practices-assessment).

Alternatively, use an Azure policy to [enable best practices assessment at scale](assess.md#enable-best-practices-assessment-at-scale-by-using-azure-policy).

### Resources moved to a different subscription

The following steps explain how to reconfigure best practices assessment after a move to a different subscription, to a different resource group, or within the same Log Analytics workspace.

1. Before the move, disable best practices assessment.
2. Move the resource.
3. Update the Log Analytics workspace with one from the new subscription and then enable best practices assessment.

Moving to a different subscription requires manual reconfiguration with the preceding steps for all SQL Server instances affected for best practices assessment.
