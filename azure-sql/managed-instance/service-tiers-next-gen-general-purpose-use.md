---
title: Use Next-gen General Purpose service tier (preview)
description: Learn about the Next-gen General Purpose service tier (preview) in Azure SQL Managed Insatnce, which is an architectural upgrade to the existing General Purpose service tier that can be used for new and existing instances. 
author: urosmil
ms.author: urmilano
ms.reviewer: wiassaf, mathoma
ms.date: 05/27/2024
ms.service: azure-sql-managed-instance
ms.subservice: service-overview
ms.topic: conceptual
---
# Use Next-gen General Purpose service tier (preview) - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you how to use the Next-gen General Purpose service tier upgrade for [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md). The Next-gen General Purpose service tier is an architectural upgrade to the existing General Purpose service tier that can be used for new and existing instances. 

> [!NOTE]
> The Next-gen General Purpose service tier upgrade is currently in preview. 

## Overview

[!INCLUDE [azure-sql-managed-instance-compare-service-tiers](../includes/sql-managed-instance/azure-sql-managed-instance-next-gen-general-purpose-upgrade.md)]

## Upgrade existing instances 

To upgrade an existing instance to the Next-gen General Purpose service tier in the Azure portal, follow these steps:

1. Go to your SQL managed instance resource in the [Azure portal](https://portal.azure.com). 
1. Select **Compute + storage** under **Settings** to open the **Compute + storage** pane. 
1. On the **Compute + storage** pane, select **Enable** for *Next-gen General Purpose (preview)*: 
   
   :::image type="content" source="media/service-tiers-next-gen-general-purpose-use/existing-instance.png" alt-text="Screenshot of the compute and storage page for your instance in the Azure portal, with next-gen general purpose selected.":::

1. After *Next-gen General Purpose (preview)* is enabled, you can use sliders to modify the IOPS for the instance, and review the *Cost per IOPS* in the **Estimated costs per month** box. 
1. Select **Apply** to save your changes. 

## New instances 

You can use the Next-gen General Purpose tier upgrade for new instances when you deploy them in the Azure portal.  To do so, follow these steps:

1. Go to the [Create Azure SQL Managed Instance](https://portal.azure.com/#create/Microsoft.SQLManagedInstance) pane in the Azure portal. 
1. On the **Networking** tab, under **Virtual network / subnet**, select a subnet from the dropdown that is **Ready for November 2022 feature wave**. 
1. On the **Basics** tab, select **Configure Managed Instance** under **Compute + storage** to open the **Compute + storage** pane: 

   :::image type="content" source="media/service-tiers-next-gen-general-purpose-use/new-instance-configure.png" alt-text="Screenshot of the Create Azure SQL Managed Instance page in the Azure portal, with Configure Managed Instance selected.":::

1. Select **Enabled** for *Next-gen General Purpose (preview)* and then use the slider to modify IOPS for your instance. Review the *Cost per IOPS* in the **Estimated costs per month** box: 

   :::image type="content" source="media/service-tiers-next-gen-general-purpose-use/new-instance-configure-managed-instance.png" alt-text="Screenshot of the compute + storage page when you configure your new Azure SQL Managed in the Azure portal.":::

1. Select **Apply** to save your instance configuration and go back to the **Create Azure SQL Managed Instance** pane. 
1. Fill out the rest of the values to configure your new instance. 
1. Select **Review + create** to review your settings, and then select **Create** to deploy your new instance using the Next-gen General purpose tier upgrade. 

## Next steps

- To get started, see [Creating a SQL Managed Instance using the Azure portal](instance-create-quickstart.md)
- For pricing details, see 
    - [Azure SQL Managed Instance single instance pricing page](https://azure.microsoft.com/pricing/details/azure-sql-managed-instance/single/)
    - [Azure SQL Managed Instance pools pricing page](https://azure.microsoft.com/pricing/details/azure-sql-managed-instance/pools/)
- For details about the specific compute and storage sizes available in the General Purpose and Business Critical service tiers, see [vCore-based resource limits for Azure SQL Managed Instance](resource-limits.md).
- [SLA for Azure SQL Managed Instance](https://azure.microsoft.com/support/legal/sla/azure-sql-sql-managed-instance/)
- [Frequently asked questions](frequently-asked-questions-faq.yml#next-gen-general-purpose-service-tier-upgrade)
