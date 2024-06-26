---
title: Enroll in November 2022 feature wave
titleSuffix: Azure SQL Managed Instance
description: Learn about rollout progress for new and existing instances of the November 2022 feature wave.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: mathoma
ms.date: 07/12/2023
ms.service: sql-managed-instance
ms.subservice: service-overview
ms.custom: ignite-2023
ms.topic: how-to
---
# Rollout of November 2022 feature wave for Azure SQL Managed Instance

[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes the November 2022 feature wave for Azure SQL Managed Instance, and details the current roll out phase. The benefits and features introduced by the November 2022 feature wave are now generally available. 

**Current phase: Rolling out the feature wave to subnets with existing SQL managed instances in production subscriptions.**

## New benefits and features

Once the November 2022 feature wave is enabled for a subnet, all the SQL managed instances in that subnet get access to the new benefits and features introduced in the feature wave. 

The benefits in the feature wave include:

- [**Fast instance provisioning**](management-operations-overview.md#fast-provisioning) -  It takes less time to deploy an instance.
- [**Improved network security**](connectivity-architecture-overview.md) - Internal service traffic is now isolated and secured by Microsoft. 
- [**Enhanced virtual cluster**](virtual-cluster-architecture.md) -  The functionality of the underlying virtual cluster is enhanced.

The features available in the wave are:

- [Instance stop/start](instance-stop-start-how-to.md): You can start and stop your instance at your discretion to save on billing costs for vCores and SQL Server licensing.
- [Zone redundancy for Business Critical tier](..//managed-instance/high-availability-sla-local-zone-redundancy.md): You can deploy your Business Critical tier managed instance across multiple availability zones to improve the availability of your service.
- [Managed DTC](distributed-transaction-coordinator-dtc.md): Run distributed transactions in mixed environments.

## Feature wave rollout

The following list describes the current state of the rollout: 

- *All instances* in subnets on **dev/test subscriptions** are enrolled in the feature wave and have access to the new benefits and features.
- *New instances* created after October 2023 within eligible subnets on **production subscriptions** in any region, are enrolled in the feature wave automatically and have access to the new benefits and features. 
- *Existing instances without the feature wave* on **production subscriptions** will be enrolled in the feature wave gradually and automatically in months following October 2023. Enrollment happens automatically as part of regular updates during configured maintenance windows, honoring [Azure Safe Deployment Practices](https://azure.microsoft.com/blog/advancing-safe-deployment-practices/). 

> [!NOTE]
> Benefits and features can't be enabled individually on managed instances that don't have the feature wave enabled at the subnet level. 

### Eligible subnets

All new instances on production subscriptions are enrolled in the feature wave by default if they are created in eligible subnets. The following subnet types are eligible:

- Newly created subnet (default)
- Existing subnets that are empty
- Existing subnets that already have the feature wave enabled, and contain only instances with the feature wave enabled

> [!IMPORTANT]
> If you deploy an instance to an existing subnet that contains instances **without** the feature wave, the feature wave is automatically disabled for the new instance, and can't be enabled in the future while the instance is in this subnet.

## New instances

Starting November 2023, both production and dev/test subscription types are eligible for the feature wave. 

Once a subscription is enrolled in the feature wave, new SQL managed instances deployed to an eligible subnet in a supported region automatically have the feature wave enabled. 

To deploy your SQL managed instance in the Azure portal with the feature wave enabled, follow these steps:

1. Go to [Azure SQL](https://ms.portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fazuresql) in the Azure portal.
1. Select **Create**.
1. On the **Select SQL deployment option** page, in the **SQL managed instances** dropdown, choose **Single instance** and then select **Create**.

   In a qualified subscription, the message **November 2022 feature wave is available for this subscription** appears on the **Create Azure SQL Managed Instance** page:

   :::image type="content" source="media/november-2022-feature-wave-enroll/create-new-instance-opt-in.png" alt-text="Screenshot that shows the Create Azure SQL Managed Instance pane, with the November 2022 feature dialog selected. ":::

1. Use the **Subscription** dropdown to choose a subscription that's listed under **Ready for November 2022 feature wave**.

   :::image type="content" source="media/november-2022-feature-wave-enroll/choose-subscription-from-drop-down.png" alt-text="Screenshot that shows the create a new managed instance pane and choosing a subscription that's ready for the feature wave.":::

1. Use the **Region** dropdown to choose a region that's listed under **Ready for November 2022 feature wave**.

   :::image type="content" source="media/november-2022-feature-wave-enroll/ready-region-drop-down.png" alt-text="Screenshot that shows the create a new managed instance pane and choosing a region that's ready for the feature wave.":::

1. On the **Networking** tab, select an eligible subnet from the dropdown under **Ready for November 2022 feature wave**:

   :::image type="content" source="media/november-2022-feature-wave-enroll/create-instance-subnet.png" alt-text="Screenshot that shows the create a new managed instance pane and a subnet that's ready for the feature wave selected." lightbox="media/november-2022-feature-wave-enroll/create-instance-subnet.png":::

1. Select **Review + Create** to validate your settings. Check the **Nov 2022 feature wave** section to confirm that all configuration options are compatible with the feature wave:

   :::image type="content" source="media/november-2022-feature-wave-enroll/review-and-create.png" alt-text="Screenshot that shows the Review + create pane in the Azure portal, with the November 2022 feature wave options highlighted.":::

## Existing instances

Only instances deployed to eligible subnets can enable features from the November 2022 feature wave. Existing instances deployed to ineligible subnets can't enable the feature wave until the subnet is eligible. Instances deployed to subnets that become eligible will be automatically enrolled during regular maintenance windows in months following October 2023. 

## Known issues

**I don't see the feature wave enabled when I create a new instance**

If you selected a subnet type that's not eligible for the feature wave, you might not see the feature wave enabled for a new managed instance. 

## Next steps

To learn about specific changes related to the feature wave, see these articles:

- [Simplified connectivity architecture](connectivity-architecture-overview.md)
- [Instance stop/start](instance-stop-start-how-to.md)
- [Zone redundancy for Business Critical tier](../database/high-availability-sla-local-zone-redundancy.md)
- [Managed DTC](distributed-transaction-coordinator-dtc.md)
- [Frequently asked questions](frequently-asked-questions-faq.yml#november-2022-feature-wave)
