---
title: Enroll in November 2022 feature wave
titleSuffix: Azure SQL Managed Instance
description: Learn how to enroll new and existing instances in the November 2022 feature wave.
author: MashaMSFT
ms.author: mathoma
ms.date: 07/12/2023
ms.service: sql-managed-instance
ms.subservice: service-overview
ms.topic: how-to
---
# Enroll in November 2022 feature wave for Azure SQL Managed Instance

[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqlmi.md)]

Learn how to enroll new and existing managed instances in the November 2022 feature wave for Azure SQL Managed Instance.

The November 2022 feature wave is being released in a phased manner. The current phase makes managed instances belonging to Dev/Test subscriptions eligible for the feature wave. 

Existing eligible instances that were created before November 2022 can enroll in the feature wave immediately to unlock new benefits and features.

## New benefits and features

The November 2022 feature wave introduced new features and automatic benefits for Azure SQL Managed Instance.

The benefits in the feature wave include:

- **Fast instance provisioning**. It takes less time to deploy an instance.
- [**Improved network security**](connectivity-architecture-overview.md). Internal service traffic is now isolated and secured by Microsoft. 
- [**Enhanced virtual cluster**](connectivity-architecture-overview.md?tab=current#virtual-cluster-connectivity-architecture). The functionality of the underlying virtual cluster is enhanced.

These new preview features were introduced in the wave:

- [Instance stop/start](instance-stop-start-how-to.md): You can start and stop your instance at your discretion to save on billing costs for vCores and SQL Server licensing.
- [Zone redundancy for Business Critical tier](../database/high-availability-sla.md): You can deploy your Business Critical tier managed instance across multiple availability zones to improve the availability of your service.
- [Managed DTC](distributed-transaction-coordinator-dtc.md): Run distributed transactions in mixed environments.

For more information, see [Frequently asked questions](frequently-asked-questions-faq.yml#november-2022-feature-wave).

## November 2022 feature wave rollout

The November 2022 feature wave is being released in a phased manner. The current phase makes managed instances belonging to [Dev/Test subscriptions](frequently-asked-questions-faq.yml#what-azure-offers-and-subscription-types-are-enrolled-in-the-november-2022-feature-wave) eligible for the feature wave. 

The enrollment experience is different for new instances and existing instances:

- *New instances* created after November 2022 in eligible subnets will be enrolled in the feature wave automatically and get access to the new benefits and features.
- *Existing instances* will be enrolled in the November 2022 feature wave gradually and automatically. Eligible existing instances that were created before November 2022 can enroll in the feature wave immediately to unlock the new benefits and features.

> [!NOTE]
> Benefits and features can't be enabled individually on managed instances that haven't enrolled in the feature wave.

## Enroll an existing instance

Existing eligible instances that were created before November 2022 eventually will be automatically enrolled in the feature wave. However, you can choose to enroll your existing instance immediately if your instance meets the following conditions:

- Your instance is hosted in a subscription type that's ready for the November 2022 feature wave. Currently, Dev/Test subscription type is supported.
- You can update the virtual network subnet to one of the supported types:

  - A new subnet
  - An empty subnet
  - A subnet that hosts only instances that have already enrolled in the feature wave

To enroll an existing instance in the feature wave by using the Azure portal:

1. In the [Azure portal](https://portal.azure.com), go to **Overview** for your instance of SQL Managed Instance.
1. In the left menu or in the instance information box, select **November 2022 feature wave**.

   :::image type="content" source="media/november-2022-feature-wave-enroll/existing-instance-overview-page.png" alt-text="Screenshot that shows the managed instance overview pane, with November 2022 feature wave information highlighted." lightbox="media/november-2022-feature-wave-enroll/existing-instance-overview-page.png":::

1. In **November 2022 feature wave status**, if the existing instance is eligible, select the **Open Virtual network** option that appears:

   :::image type="content" source="media/november-2022-feature-wave-enroll/feature-wave-details.png" alt-text="Screenshot that shows the November 2022 feature wave information pane.":::

   If your subscription is eligible but your instance isn't, select the **Create new instance** option to go to the instance deployment page. Skip to the [Enroll new instances](#enroll-a-new-instance) section.

1. In **Virtual network / subnet**, either select **Create new** to create a new subnet, or choose an existing eligible subnet from the dropdown under **Ready for November 2022 feature wave**. Select **Save** to move your managed instance to the subnet you selected.

   :::image type="content" source="media/november-2022-feature-wave-enroll/virtual-network-subnet.png" alt-text="Screenshot that shows the virtual network / subnet pane, with a subnet selected in the dropdown.":::

1. When the subnet update is finished, the message **November 2022 feature wave is ready to use for this instance** message appears. Your instance is now enrolled in the feature wave, and it has access to all the benefits and features.

## Enroll a new instance

After your subscription is enrolled in the feature wave, you can deploy a new instance with the feature wave already enabled if you choose an enrolled subscription, supported region, and eligible subnet type during deployment.

Use the Azure portal to deploy your instance. Then, confirm that it's enrolled in the feature wave.

The following subnet types are eligible:

- Newly created subnet (default)
- Existing subnets that are empty
- Existing subnets that contain only instances that have already enrolled in the feature wave

To confirm that your new instance is enrolled in the feature wave during deployment in the Azure portal:

1. In the [Azure portal](https://portal.azure.com), go to [Azure SQL](https://ms.portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fazuresql).
1. In the command bar, select **Create**.
1. In **Select SQL deployment option**, in the **SQL managed instances** dropdown, select **Single instance**. Select **Create**.

   In a qualified subscription, the message **This subscription is ready to opt-in to November 2022 feature wave** appears in the **Create Azure SQL Managed Instance** pane:

   :::image type="content" source="media/november-2022-feature-wave-enroll/create-new-instance-opt-in.png" alt-text="Screenshot that shows the Create Azure SQL Managed Instance pane, with the November 2022 feature dialog selected. ":::

1. Use the **Subscription** dropdown to choose a subscription that's listed under **Ready for November 2022 feature wave**.

   :::image type="content" source="media/november-2022-feature-wave-enroll/choose-subscription-from-drop-down.png" alt-text="Screenshot that shows the create a new managed instance pane and choosing a subscription that's ready for the feature wave.":::

1. Use the **Region** dropdown to choose a region that's listed under **Ready for November 2022 feature wave**.

   :::image type="content" source="media/november-2022-feature-wave-enroll/ready-region-drop-down.png" alt-text="Screenshot that shows the create a new managed instance pane and choosing a region that's ready for the feature wave.":::

1. On the **Networking** tab, select an eligible subnet from the dropdown under **Ready for November 2022 Feature Wave**:

   :::image type="content" source="media/november-2022-feature-wave-enroll/create-instance-subnet.png" alt-text="Screenshot that shows the create a new managed instance pane and a subnet that's ready for the feature wave selected." lightbox="media/november-2022-feature-wave-enroll/create-instance-subnet.png":::

1. Select **Review + Create** to validate your settings. Check the **Nov 2022 feature wave** section to confirm that all configuration options are compatible with the feature wave:

   :::image type="content" source="media/november-2022-feature-wave-enroll/review-and-create.png" alt-text="Screenshot that shows the Review + create pane in the Azure portal, with the November 2022 feature wave options highlighted.":::

## Known issues

For specifics, check out the [Frequently asked questions](frequently-asked-questions-faq.yml#november-2022-feature-wave).

**I don't see the feature wave option when I open the overview page for my instance**

If your subscription isn't enrolled in the feature wave yet, you might not see the feature wave option for your managed instance. Check back soon.

**I don't see the feature wave enabled when I create a new instance**

If you selected a subscription, region, or subnet type that's not eligible for the feature wave, you might not see the feature wave enabled for a new managed instance. Update these parameters and try again.

## Next steps

To learn about specific changes related to the feature wave, see these articles:

- [Simplified connectivity architecture](connectivity-architecture-overview.md)
- [Instance stop/start](instance-stop-start-how-to.md)
- [Zone redundancy for Business Critical tier](../database/high-availability-sla.md)
- [Managed DTC](distributed-transaction-coordinator-dtc.md)
- [Frequently asked questions](frequently-asked-questions-faq.yml#november-2022-feature-wave)
