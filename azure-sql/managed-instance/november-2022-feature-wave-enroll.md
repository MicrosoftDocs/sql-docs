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

This article explains how to enroll new and existing instances to the November 2022 feature wave for Azure SQL Managed Instance. 

Rollout of the November 2022 Feature Wave is happening over the course of several months.  The initial rollout phase targets instances that belong to Dev/Test subscriptions, with other subscription types enrolling in subsequent months. 

Eligible existing instances created prior to November 2022 can enroll into the feature wave immediately to unlock the new benefits and features. 

## Overview

November 2022 introduced a wave of new features and automatic benefits for Azure SQL Managed Instance. 

The benefits in the feature wave include: 

- Fast Instance provisioning: reduces the time it takes to deploy an instance. 
- [Simplified connectivity architecture ](connectivity-architecture-overview.md): simplifies the connectivity architecture of SQL Managed Instance by removing the management endpoint, and reducing the number of mandatory rules. 
- [Enhanced virtual cluster](connectivity-architecture-overview.md?tab=current#virtual-cluster-connectivity-architecture): improves the functionality of the underlying virtual cluster. 

The new preview features introduced in the wave are: 

- [Instance stop / start](instance-stop-start-how-to.md):  allows you to start and stop your instance at your discretion to save on billing costs for vCores and SQL licensing. 
- [Zone redundancy for BC](../database/high-availability-sla.md): lets you deploy your Business Critical managed instance across multiple availability zones to improve the availability of your service. 
- [Managed DTC](distributed-transaction-coordinator-dtc.md): lets you run distributed transactions in mixed environments. 

For specifics, check out the [Frequently asked questions](frequently-asked-questions-faq.yml#november-2022-feature-wave). 

## Feature wave rollout 

Rollout of the November 2022 Feature Wave is happening over the course of several months.  The initial rollout phase targets instances that belong to [Dev/Test subscriptions](frequently-asked-questions-faq.yml#what-azure-offers-and-subscription-types-are-enrolled-in-the-november-2022-feature-wave), with other subscription types enrolled in subsequent months. Unsupported subscription types cannot be enabled individually.

The enrollment experience is different for new instances and existing instances:

- New instances created after November 2022 in eligible subnets will automatically be enrolled in the feature wave and get access to the new benefits and features.
- Enrolling existing instances to the November 2022 feature wave will happen gradually and automatically. However, existing eligible instances created prior to November 2022 can enroll into the feature wave immediately to unlock the new benefits and features. 

> [!NOTE]
> Benefits and features cannot be enabled individually on instances that have not enrolled into the feature wave. 

## Enroll existing instances 

Existing eligible instances that were created before November 2022 will eventually be enrolled in the feature wave automatically. However, you can choose to enroll your existing instance immediately if the following conditions are met: 

- Your instance is hosted in a subscription type that is ready for the November 2022 feature wave - currently only Dev/Test subscriptions are supported. 
- You're able to update the virtual network subnet to one of the supported types: **new subnet**, **empty subnet** or a subnet that's **only hosting instances that have already enrolled in the feature wave**. 

To enroll an existing instance to the feature wave by using the Azure portal, follow these steps: 

1. Go to the **Overview** page for your eligible SQL Managed Instance in the [Azure portal](https://portal.azure.com). 
1. Select **November 2022 feature wave** either from the menu, or from the information box to open the **November 2022 feature wave** info page. 

   :::image type="content" source="media/november-2022-feature-wave-enroll/existing-instance-overview-page.png" alt-text="Screenshot of the Azure portal showing the MI overview page, with November 2022 feature wave info highlighted." lightbox="media/november-2022-feature-wave-enroll/existing-instance-overview-page.png":::

1. If the existing instance is eligible, you'll see select **Open Virtual Network** and be able to open the **Virtual network / subnet** page: 

   :::image type="content" source="media/november-2022-feature-wave-enroll/feature-wave-details.png" alt-text="Screenshot of the Azure portal showing the November 2022 feature wave info page.":::

   However, if your subscription is eligible but your instance isn't, you'll see **Create new instance** on the *feature wave* page instead. Select **Create new instance** to go to the instance deployment page and skip to the [Enroll new instances](#enroll-new-instances) section. 

1. From the **Virtual network / subnet** page, either select **Create new** to create a new subnet, or choose an existing eligible subnet from the drop-down under **Ready for November 2022 feature wave**. Selecting **Save** moves your managed instance to the chosen subnet. 

   :::image type="content" source="media/november-2022-feature-wave-enroll/virtual-network-subnet.png" alt-text="Screenshot of the Azure portal, virtual network / subnet page, with a subnet selected in the drop-down.":::

1. After the subnet update is complete, you'll see a green **November 2022 feature wave is ready to use for this instance** message. Your instance is now enrolled in the feature wave, and gets access to all the benefits and features.

## Enroll new instances 

Once your subscription has been enrolled in the feature wave, you can deploy a new instance with the feature wave already enabled as long as you choose an enrolled subscription, supported region, and eligible subnet type during deployment. 

Use the Azure portal to deploy your instance, and confirm that it's enrolled in the feature wave. 


The following subnet types are eligible: 

- Newly created subnet (this is the default selection)
- Existing subnets that are empty
- Existing subnets that only contain instances that have already enrolled in the feature wave 

To confirm that your new instance is enrolled in the feature wave during deployment in the Azure portal, follow these steps: 

1. Go to the [Azure SQL](https://ms.portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fazuresql) page in the [Azure portal](https://portal.azure.com). 
1. Select **+ Create** to open the **Select SQL deployment option**. 
1. Choose **Single instance** from the **SQL managed instances** drop-down and then select **Create**. Qualified subscriptions will see **This subscription is ready to opt-in to November 2022 feature wave** on the **Create Azure SQL Managed Instance page** as the following screenshot shows: 

   :::image type="content" source="media/november-2022-feature-wave-enroll/create-new-instance-opt-in.png" alt-text="Screenshot of the Azure portal, Create Azure SQL Managed Instance page, with November 2022 feature dialog selected. ":::

1. Use the **Subscription** drop-down to choose a subscription listed under **Ready for November 2022 feature wave**. 

   :::image type="content" source="media/november-2022-feature-wave-enroll/choose-subscription-from-drop-down.png" alt-text="Screenshot of the Azure portal, create new MI page, basics tab, choosing a ready for feature wave subscription from the drop-down.":::

1. Use the **Region** drop-down to choose a region listed under **Ready for November 2022 feature wave**. 

   :::image type="content" source="media/november-2022-feature-wave-enroll/ready-region-drop-down.png" alt-text="Screenshot of the Azure portal, create new MI page, basics tab, showing choosing a ready for feature wave region from the drop-down.":::

1. On the **Networking** tab, select an eligible subnet from the drop-down listed under **Ready for November 2022 Feature Wave**: 

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

To learn about specific changes related to the feature wave, review: 

- [Simplified connectivity architecture](connectivity-architecture-overview.md)
- [Instance stop / start](instance-stop-start-how-to.md)
- [Zone redundancy for BC](../database/high-availability-sla.md)
- [Managed DTC](distributed-transaction-coordinator-dtc.md)
- [Frequently asked questions](frequently-asked-questions-faq.yml#november-2022-feature-wave)

