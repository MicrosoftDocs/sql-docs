---
title: Enroll in November 2022 feature wave 
titleSuffix: Azure SQL Managed Instance
description: Learn how to enroll new and existing instances into the November 2022 feature wave
author: MashaMSFT
ms.author: mathoma
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: service-overview
ms.topic: how-to
ms.custom:
---
# Enroll in November 2022 feature wave - Azure SQL Managed Instance

[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqlmi.md)]

Learn how to enroll new and existing managed instances in the November 2022 feature wave for Azure SQL Managed Instance.

The November 2022 feature wave is rolling out over several months. The initial rollout phase focuses on managed instances that belong to Dev/Test subscription types. Other subscription types will enroll in later months.

Eligible existing instances created before November 2022 can enroll in the feature wave immediately to unlock new benefits and features.

## New benefits and features

The November 2022 feature wave introduced new features and automatic benefits for Azure SQL Managed Instance.

The benefits in the feature wave include:

- Fast instance provisioning. It takes less time to deploy an instance.
- [Simplified connectivity architecture](connectivity-architecture-overview.md). The connectivity architecture of SQL Managed Instance is simplified by removing the management endpoint and reducing the number of mandatory rules.
- [Enhanced virtual cluster](connectivity-architecture-overview.md?tab=current#virtual-cluster-connectivity-architecture). The functionality of the underlying virtual cluster is enhanced.

These new preview features were introduced in the wave:

- [Instance stop/start](instance-stop-start-how-to.md): You can start and stop your instance at your discretion to save on billing costs for vCores and SQL Server licensing.
- [Zone redundancy for Business Critical tier](../database/high-availability-sla.md): You can deploy your Business Critical tier managed instance across multiple availability zones to improve the availability of your service.
- [Managed DTC](distributed-transaction-coordinator-dtc.md): Run distributed transactions in mixed environments.

For more information, see [Frequently asked questions](frequently-asked-questions-faq.yml#november-2022-feature-wave).

## Feature wave rollout

The November 2022 feature wave is rolling out over several months. The initial rollout phase focuses on instances that belong to [Dev/Test subscriptions](frequently-asked-questions-faq.yml#what-azure-offers-and-subscription-types-are-enrolled-in-the-november-2022-feature-wave). Other subscription types will be enrolled in subsequent months. Unsupported subscription types can't be enabled individually.

The enrollment experience is different for new instances and existing instances:

- New instances created after November 2022 in eligible subnets will be enrolled in the feature wave automatically and get access to the new benefits and features.
- Existing instances will be enrolled in the November 2022 feature wave gradually and automatically. Eligible existing instances that were created before November 2022 can enroll in the feature wave immediately to unlock the new benefits and features.

> [!NOTE]
> Benefits and features can't be enabled individually on instances that haven't enrolled in the feature wave.

## Enroll an existing instance

Existing eligible instances that were created before November 2022 will eventually be enrolled in the feature wave automatically. However, you can choose to enroll your existing instance immediately if the following conditions are met:

- Your instance is hosted in a subscription type that's ready for the November 2022 feature wave. Currently, only the Dev/Test subscription type is supported.
- You can update the virtual network subnet to one of the supported types: a *new* subnet, an *empty* subnet, or a subnet that *hosts only instances that have already enrolled in the feature wave*.

To enroll an existing instance to the feature wave by using the Azure portal:

1. In the [Azure portal](https://portal.azure.com), go to **Overview** for your instance of SQL Managed Instance.
1. In the left menu or in the instance information box, select **November 2022 feature wave** either from the menu.

   :::image type="content" source="media/november-2022-feature-wave-enroll/existing-instance-overview-page.png" alt-text="Screenshot of the Azure portal showing the MI overview page, with November 2022 feature wave info highlighted." lightbox="media/november-2022-feature-wave-enroll/existing-instance-overview-page.png":::

1. In **November 2022 feature wave status**, if the existing instance is eligible, select **Open Virtual Network**:

   :::image type="content" source="media/november-2022-feature-wave-enroll/feature-wave-details.png" alt-text="Screenshot of the Azure portal showing the November 2022 feature wave info page.":::

  If your subscription is eligible but your instance isn't, select **Create new instance** to go to the instance deployment page. Skip to the [Enroll new instances](#enroll-new-instances) section.

1. In **Virtual network / subnet**, either select **Create new** to create a new subnet, or choose an existing eligible subnet from the dropdown under **Ready for November 2022 feature wave**. Select **Save** to move your managed instance to the chosen subnet.

   :::image type="content" source="media/november-2022-feature-wave-enroll/virtual-network-subnet.png" alt-text="Screenshot of the Azure portal, virtual network / subnet page, with a subnet selected in the dropdown.":::

1. When the subnet update is finished, a green **November 2022 feature wave is ready to use for this instance** message appears. Your instance is now enrolled in the feature wave, and it has access to all the benefits and features.

## Enroll a new instance

Once your subscription has been enrolled in the feature wave, you can deploy a new instance with the feature wave already enabled as long as you choose an enrolled subscription, supported region, and eligible subnet type during deployment.

Use the Azure portal to deploy your instance, and confirm that it's enrolled in the feature wave.

The following subnet types are eligible:

- Newly created subnet (this is the default selection)
- Existing subnets that are empty
- Existing subnets that only contain instances that have already enrolled in the feature wave

To confirm that your new instance is enrolled in the feature wave during deployment in the Azure portal:

1. In the [Azure portal](https://portal.azure.com), go to [Azure SQL](https://ms.portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fazuresql).
1. In the command bar, select **Create**.
1. In **Select SQL deployment option**, in the **SQL managed instances** dropdown, select **Single instance**. Select **Create**. Qualified subscriptions will see **This subscription is ready to opt-in to November 2022 feature wave** on the **Create Azure SQL Managed Instance page** as the following screenshot shows:

   :::image type="content" source="media/november-2022-feature-wave-enroll/create-new-instance-opt-in.png" alt-text="Screenshot of the Azure portal, Create Azure SQL Managed Instance page, with November 2022 feature dialog selected. ":::

1. Use the **Subscription** dropdown to choose a subscription listed under **Ready for November 2022 feature wave**.

   :::image type="content" source="media/november-2022-feature-wave-enroll/choose-subscription-from-drop-down.png" alt-text="Screenshot of the Azure portal, create new MI page, basics tab, choosing a ready for feature wave subscription from the drop-down.":::

1. Use the **Region** dropdown to choose a region listed under **Ready for November 2022 feature wave**.

   :::image type="content" source="media/november-2022-feature-wave-enroll/ready-region-drop-down.png" alt-text="Screenshot of the Azure portal, create new MI page, basics tab, showing choosing a ready for feature wave region from the drop-down.":::

1. On the **Networking** tab, select an eligible subnet from the dropdown listed under **Ready for November 2022 Feature Wave**:

   :::image type="content" source="media/november-2022-feature-wave-enroll/create-instance-subnet.png" alt-text="Screenshot of the Azure portal, create new MI page, networking tab, with a subnet selected from the drop-down that's ready for the feature wave." lightbox="media/november-2022-feature-wave-enroll/create-instance-subnet.png":::

1. Select **Review + Create** to validate your settings. Check the **Nov 2022 feature wave** section to confirm that all configuration options are compatible with the feature wave:

   :::image type="content" source="media/november-2022-feature-wave-enroll/review-and-create.png" alt-text="Screenshot of the Azure portal, Review + create page for SQL MI, showing the Nov 22 feature wave options.":::

## Known issues

For specifics, check out the [Frequently asked questions](frequently-asked-questions-faq.yml#november-2022-feature-wave).

**I don't see the feature wave option when I open the overview page for my instance**

This is likely because your subscription hasn't been enrolled in the feature wave yet. Check back soon.

**I don't see the feature wave enabled when I create a new instance**

This is likely because you've selected a subscription, region, or subnet type that is ineligible for the feature wave. Update these parameters and try again.

## Next steps

To learn about specific changes related to the feature wave, see these articles:

- [Simplified connectivity architecture](connectivity-architecture-overview.md)
- [Instance stop / start](instance-stop-start-how-to.md)
- [Zone redundancy for Business Critical tier](../database/high-availability-sla.md)
- [Managed DTC](distributed-transaction-coordinator-dtc.md)
- [Frequently asked questions](frequently-asked-questions-faq.yml#november-2022-feature-wave)
