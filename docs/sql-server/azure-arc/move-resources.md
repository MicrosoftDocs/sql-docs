---
title: Move Resources to a New Resource Group or Subscription
description: This article explains how to move resources to a new resource group or subscription for SQL Server enabled by Azure Arc.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: sashan
ms.date: 07/08/2025
ms.topic: how-to
ms.custom:
  - build-2025
---

# Move SQL Server enabled by Azure Arc resources to a new resource group or subscription

This article describes how you can move resources to a new resource group or subscription with SQL Server enabled by Azure Arc. The capability applies to both:

- SQL Server instances
- Databases

Before you begin, review [Known limitations](#known-limitations).

## Requirements

To complete this task, make sure that:

- The *Machine - Azure Arc* resource and all SQL Server instances are in the same resource group.
- The new subscription or resource group needs to meet all [prerequisites](prerequisites.md).

In addition:

- If Microsoft Purview is enabled, you must disable it in the compliance portal before the move.
- If best practices assessment is enabled, you must disable it before the move.
- SQL Server license and SQL Server extended security update license aren't moved automatically.

After the move:

1. Reenable any features that you disabled.
1. Configure SQL Server license in the new location.
1. Configure ESU in the new location.

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

| Feature | Before move | After move |
| :--- | :--- | :--- |
| Patching | Enabled | Enabled |
| Extended Security Updates | Enabled | Enabled |
| License type | Enabled | Enabled |
| Viewing databases | Enabled | Enabled |
| Viewing availability groups | Enabled | Enabled |
| Viewing failover cluster instances | Enabled | Enabled |
| Automated backups | Enabled | Enabled |
| Monitoring dashboards | Enabled | Enabled |
| Microsoft Entra ID | Enabled | Enabled |
| Microsoft Purview | Enabled | Disabled. To enable, review [Connect to and manage Azure Arc-enabled SQL Server in Microsoft Purview](/purview/register-scan-azure-arc-enabled-sql-server). |
| Best practices assessment | Enabled | Disabled. Review the following section about enabling best practices assessment. |
| Microsoft Defender | Enabled | Disabled. To enable, review [Protect SQL Server with Microsoft Defender for Cloud](configure-advanced-data-security.md). |

## Enable best practices assessment

The way to enable best practices assessment depends on whether the resource moved resource groups or subscriptions.

### Resources moved within the same subscription

1. Before the move, disable best practices assessment.
1. Move the resource.
1. [Enable best practices assessment](assess.md#enable-best-practices-assessment).

Alternatively, use an Azure policy to [enable best practices assessment at scale](assess.md#enable-best-practices-assessment-at-scale-by-using-azure-policy).

### Resources moved to a different subscription

The following steps explain how to reconfigure best practices assessment after a move to a different subscription, to a different resource group, or within the same Log Analytics workspace.

1. Before the move, disable best practices assessment.
1. Move the resource.
1. Update the Log Analytics workspace with one from the new subscription and then enable best practices assessment.

Moving to a different subscription requires manual reconfiguration with the preceding steps for all SQL Server instances affected for best practices assessment.

## Known limitations

Moving the SQL Server p-core license to the new subscription or resource group is currently not supported. You'll need to:

1. Manually delete the old license
1. Re-create and reconfigure the license in the new location

  For details, review [Manage the unlimited virtualization benefit for SQL Server](manage-configuration.md#manage-pcore-license).

Moving the SQL Server ESU p-core license to the new subscription or resource group is currently not supported. If you create a license in the location, it will be treated as a new ESU subscription with a new back-billing charge.

> [!IMPORTANT]  
> While moving the VMs with ESU enabled to a new subscription or resource group is supported, if they used to be covered by a SQL Server ESU p-core license in the old location and not billed individually, the move will result in these VMs being billed for ESU individually as they will no longer be covered by a p-core license.

## Related content

- [Manage inventory of SQL Server resources with Azure Arc](view-inventory.md)
