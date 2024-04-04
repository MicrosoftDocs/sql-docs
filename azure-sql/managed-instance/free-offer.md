---
title: Try for free (preview)
description: Guidance on how to deploy the Azure SQL Managed Instance free offer.
author: Urosran
ms.author: urandjelovic
ms.reviewer: mathoma, randolphwest
ms.date: 01/02/2024
ms.service: sql-managed-instance
ms.subservice: service-overview
ms.topic: how-to
ms.custom:
  - references_regions
monikerRange: "=azuresql || =azuresql-mi"
---
# Try Azure SQL Managed Instance for free (preview)

> [!div class="op_single_selector"]
> - [Azure SQL Database](../database/free-offer.md?view=azuresql&preserve-view=true)
> - [Azure SQL Managed Instance](free-offer.md?view=azuresql&preserve-view=true)

Try [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) free of charge for the first 12 months to get:

- a General Purpose instance with up to 100 databases
- 720 vCore hours of compute every month
- 64 GB of storage

> [!NOTE]  
> The free Azure SQL Managed Instance offer is currently in preview.

## Overview

The free SQL Managed Instance offer is designed for new Azure customers looking to get started with Azure SQL Managed Instance, as well as existing customers that might need a development database, such as for a proof of concept.

To get started, select **Apply free offer** from the banner when you try to create a new Azure SQL Managed Instance:

:::image type="content" source="media/free-offer/free-sql-managed-instance-banner.png" alt-text="Screenshot from the Azure portal of the Free Offer banner.":::

You know the offer has been applied when the **Cost summary** card on the right side of the page shows **Estimated total** as **Free**.

:::image type="content" source="media/free-offer/cost-summary-card.png" alt-text="Screenshot of the Free Offer Cost summary card. Included in the details are 'First 64 GB of storage free' and '720 vCore hours free'.":::

## Free offer limits

The free Azure SQL Managed Instance offer is the same fully managed platform-as-a-service (PaaS) instance you get with the paid version and includes handling all of the same database management functions (such as upgrading, patching, backups, and monitoring) without user involvement. The offer is available for one instance per Azure subscription.

The following table describes the limits of the free SQL Managed Instance: 

|Category  |Limit  |
|---------|---------|
|Compute | 4 or 8 vCore instances only | 
|vCore hours    | 720 vCore hours monthly         |
|Storage     |  64 GB of data<sup>1</sup>       |
|IOPS | Depends on file size / 300 for Next-gen General Purpose<sup>2</sup>
|Instances per subscription     |    1     |
|Service tiers | General purpose, and [Next-gen General Purpose (preview)](service-tiers-next-gen-general-purpose-use.md) | 
|Hardware | Standard only | 
|Backup retention     |  0-7 days for short term retention       |
|SQL License cost     |  None     |
|Guaranteed SLA| None| 

<sup>1</sup> Since system files can take up to 32 GB of storage, the available storage for user data could be less than 64 GB.   
<sup>2</sup> Free instances using the [Next-gen General Purpose (preview)](service-tiers-next-gen-general-purpose-use.md) service tier upgrade are limited to 300 IOPS, and can't use the IOPS slider. 

Additionally, the following limitations apply: 

- The following capabilities aren't supported: [Zone redundancy](high-availability-sla.md#zone-redundant-availability), [failover groups](failover-group-sql-mi.md), [long-term backup retention](../database/long-term-retention-overview.md) 
- Scaling up and down is possible only within 
- The free offer is currently available in the following region (which are subject to change): Australia East, East US, East US 2, North Europe, Sweden Central, Southeast Asia, South Central US, UK South, West Europe, West US 2, West US 3
- It's not currently possible to upgrade your free instance to the paid version. Create a new instance and [restore your database](point-in-time-restore.md?#restore-an-existing-database) to it to continue your business without the limits imposed by the free offer.


### Monthly vCore limits 

The monthly free limits include 720 vCore hours of compute. Your free month of credits starts when you create your instance and is renewed on the same day of the following month.

Once you reach the monthly free vCore limit, the instance is stopped with a status of `Stopped - Insufficient credit` and a banner on the **Overview** page of your instance in the Azure portal gives you the option to create a new paid instance. You can [restore your database](point-in-time-restore.md?#restore-an-existing-database) to the new instance to continue your business without the limits imposed by the free offer.

Once you have credits available again at the beginning of the next month, the instance is started automatically at the next scheduled start. You can also stop the instance manually at any time to avoid using up your free monthly vCore hours.

## Prerequisites

To try Azure SQL Managed Instance for free, you need:

- An Azure account with one of the following Azure subscriptions:
  - Pay-as-you-go (003P)
  - Azure in CSP (0145P)

## Create a free instance

Use the Azure portal to create the new free Azure SQL Managed Instance.

To create your free instance, follow these steps:

1. Go to the [provisioning page for Azure SQL Managed Instance](https://portal.azure.com/#create/Microsoft.SQLManagedInstance) in the Azure portal.
1. On the **Basics** tab, look for the **Want to try Azure SQL Managed Instance for free?** banner and select the **Apply offer** button. Check the **Estimated costs per month** to validate the free offer has been applied to your instance.
1. For **Resource group**, either select an existing resource group from the dropdown list or select **Create new** to create a new resource group. Enter the name of your resource group, such as `myFreeMIResourceGroup` and then select **OK**.
1. Instance details such as the name and region, are already populated with default values but you can choose to modify these values.
1. You can choose to leave the **Compute + storage** as default, or select **Configure Managed Instance** to update the number of vCores, and change the service tier from General Purpose to [Next-gen General Purpose (preview)](service-tiers-next-gen-general-purpose-use.md). 
1. Select your preferred authentication method. Select **Next : Networking**.
1. On the **Networking** tab, the public endpoint is enabled by default so you can connect to the instance from any application that can access the internet. You can choose to disable the public endpoint if you want to test a closed environment, but will then need to perform extra steps to [connect to your instance](#connect-to-your-instance).
1. Select **Next: Security**. You can choose to leave these options as default, or modify them as needed.
1. Select **Next: Additional settings**. You can choose to leave these options as default, or modify them as needed. The [instance schedule](#default-instance-schedule) is based on the time zone you configure on this page.
1. Select **Review + create** to review your settings and then use **Create** to create your free instance.

## Connect to your instance

The way to connect to your instance depends on whether or not you enabled the public endpoint when you created your instance.

### Public endpoint enabled

If you enabled the public endpoint, follow these steps to connect to your instance:

1. Go to your SQL Managed Instance in the [Azure portal](https://portal.azure.com).
1. Select **Overview** and then copy the value under **Host** in the **Essentials** section.
1. Open your preferred data tool, such as [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms) or [Azure Data Studio](/azure-data-studio/download-azure-data-studio).
1. Select **Connect** and then paste in the **Host** value for the SQL Managed Instance you copied from the Azure portal.
1. Select your preferred authentication method - either SQL administrator or Microsoft Entra and provide credentials, if necessary.
1. Select **Connect** to connect to your SQL Managed Instance.

### Public endpoint disabled

If your public endpoint is disabled, you can choose to either [Create an Azure VM within the same virtual network](connect-vm-instance-configure.md) or [Configure point-to-site](point-to-site-p2s-configure.md) to connect to your instance. Review [Connect your application to Azure SQL Managed Instance](connect-application-instance.md) for more information.

## Default instance schedule

To conserve credits, by default, the free instance is scheduled to be on from 9am to 5pm Monday through Friday in the time zone configured when you created the instance. You can [modify the schedule](instance-stop-start-how-to.md) to suit your business needs.

## Clean up resources

When you're finished using these resources, or if you want to start over again with a new free instance (limit one per subscription), you can delete the resource group you created, which also deletes the instance and any databases within it.

To delete `myFreeMIResourceGroup` and all its resources using the Azure portal:

1. In the Azure portal, search for and select **Resource groups**, and then select your resource group, such as `myFreeMIResourceGroup`, from the list.
1. On the **Resource group** page, select **Delete resource group**.
1. Under **Type the resource group name**, enter `myFreeMIResourceGroup`, and then select **Delete**.

## Related content

- [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md)
- [Azure SQL Database and Azure SQL Managed Instance connect and query articles](../database/connect-query-content-reference-guide.md)
- [Quickstart: Use SSMS to connect to and query Azure SQL Database or Azure SQL Managed Instance](../database/connect-query-ssms.md)
- [Connect and query using Azure Data Studio](/sql/azure-data-studio/quickstart-sql-database)
