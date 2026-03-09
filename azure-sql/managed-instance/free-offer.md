---
title: Try for Azure SQL Managed Instance for Free
description: Learn how to deploy a free Azure SQL Managed Instance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, randolphwest, vladiv, strrodic
ms.date: 02/24/2026
ms.service: azure-sql-managed-instance
ms.subservice: service-overview
ms.topic: how-to
ms.custom:
  - ignite-2025
monikerRange: "=azuresql || =azuresql-mi"
---

# Try Azure SQL Managed Instance for free

> [!div class="op_single_selector"]
> - [Azure SQL Database](../database/free-offer.md?view=azuresql&preserve-view=true)
> - [Azure SQL Managed Instance](free-offer.md?view=azuresql&preserve-view=true)

This article describes the free offer of [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md), which provides for the first 12 months a free instance with up to 500 databases, 720 vCore hours of compute, and 64 GB of storage.

To get started, select the **Try for free** link on the [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub).

   :::image type="content" source="media/free-offer/try-for-free.png" alt-text="Screenshot from the Azure SQL hub of the Try for free button for Azure SQL Managed Instance.":::

## Prerequisites

To create a free Azure SQL Managed Instance, you need:

- An Azure account.
- A [supported Azure subscription](#supported-subscription-types). There's a limit of 1 free offer SQL managed instance per subscription.

<a id="create-a-free-instance"></a>

## Create a free SQL managed instance

Use the Azure portal to create a free Azure SQL Managed Instance. To create your free SQL managed instance, follow these steps:

1. Go to [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub).
1. On the **Azure SQL Managed Instance** tile, select **Try for free** to open the **Create Azure SQL Managed Instance** page, with the free offer already applied:

   - Alternatively, you can go directly to the [Create Azure SQL Managed Instance](https://portal.azure.com/#view/SqlAzureExtension/CreateManagedInstanceBladeV2/_provisioningContext~/) page in the Azure portal. Then select **Apply free offer** from the banner at the top of the page. Proceed with the steps to [Create Azure SQL Managed Instance in the Azure portal](instance-create-quickstart.md).

1. On the **Basics** tab:
    1. Under **Project details**:
        - Select your **Subscription**. Your current subscription is already selected.
        - A new **Resource Group** with a default name is provided. You can use an existing group or create a new resource group with a different name.
    1. Under **Managed Instance details**: 
        - Use the automatically created unique **Managed Instance name**, or provide a new name.
        - Choose a **Region**.

      That's all you need! The portal creates the new SQL managed instance with default settings.

   You know the offer is applied when the **Cost summary** card on the right side of the page shows **Estimated Cost/Month** zero cost.

   :::image type="content" source="media/free-offer/cost-summary-card.png" alt-text="Screenshot from the Azure portal of the Free Offer Cost summary card.":::

1. Select **Review + create**.
1. Review and select **Create**.

## Connect to your instance

How you connect to your instance depends on whether you disabled the public endpoint when you created your instance. For more information about endpoints, see [Connectivity architecture](connectivity-architecture-overview.md#communication-overview).

### Public endpoint enabled

When you apply the free offer to your instance, the public endpoint is enabled by default so you can connect to your instance from any application that can access the internet.

If your public endpoint is enabled, follow these steps to connect to your instance:

1. Go to your SQL Managed Instance in the [Azure portal](https://portal.azure.com/#view/HubsExtension/ServiceMenuBlade/~/SingleInstance/extension/SqlAzureExtension/menuId/AzureSqlHub/itemId/SingleInstance).
1. Under **Security**, select **Networking** and then copy the value from the **Endpoint** field, such as: 

   `contoso-free-sqlmi.public.123456789.database.windows.net,3342`

   :::image type="content" source="media/free-offer/public-instance-endpoint.png" alt-text="Screenshot of the highlighted instance public endpoint in the Azure portal on the networking page." lightbox="media/free-offer/public-instance-endpoint.png":::  

   You can also find connection strings to connect to your instance under **Settings** and then **Connection strings**.

1. Open your preferred data tool, such as [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms).
1. Select **Connect** and then paste the **Endpoint** value for the SQL Managed Instance you copied from the Azure portal into the server name field:

   :::image type="content" source="media/free-offer/connect-to-instance.png" alt-text="Screenshot of the Connect to Server dialog box in SQL Server Management Studio.":::

1. Select your preferred authentication method - either SQL Server Authentication or Microsoft Entra and provide credentials, if necessary.
1. Select **Connect** to connect to your SQL Managed Instance.

### Public endpoint disabled

If you disable the public endpoint, you can choose to either [Create an Azure VM within the same virtual network](connect-vm-instance-configure.md) or [Configure point-to-site](point-to-site-p2s-configure.md) to connect to your instance. Review [Connect your application to Azure SQL Managed Instance](connect-application-instance.md) for more information.

## Default instance schedule

To conserve credits, the free instance is scheduled to be on from 9 a.m. to 5 p.m. Monday through Friday in the time zone configured when you created the instance. If you don't configure a time zone, the default time zone is UTC. You can't modify the time zone after you create the instance.

You can [modify the schedule](instance-stop-start-how-to.md) to suit your business needs.

## Restore sample database

You can use your preferred query tool, such as [SQL Server Management Studio (SSMS)](/ssms/install/install), to restore the sample [Wide World Importers](/sql/samples/wide-world-importers-what-is) database to your free SQL Managed Instance.

To restore your database by using Transact-SQL in SQL Server Management Studio, follow these steps:

1. Connect to your free instance and open a new query window.
1. Run the following command to create a credential that accesses a publicly available storage account:

   ```sql
   CREATE CREDENTIAL [https://mitutorials.blob.core.windows.net/examples/WideWorldImporters-Standard.bak]
   WITH IDENTITY = 'SHARED ACCESS SIGNATURE';
   ```

1. Run the following command to check that your credential has access to the storage account:

   ```sql
   RESTORE FILELISTONLY 
   FROM URL = 'https://mitutorials.blob.core.windows.net/examples/WideWorldImporters-Standard.bak';
   ```

1. Run the following statement to restore the sample Wide World Importers database:

   ```sql
   RESTORE DATABASE [WideWorldImportersExample] FROM URL =
     'https://mitutorials.blob.core.windows.net/examples/WideWorldImporters-Standard.bak';
   ```

1. Run the following statement to track the status of your restore process:

   ```sql
   SELECT session_id as SPID, command, a.text AS Query, start_time, percent_complete
      , dateadd(second,estimated_completion_time/1000, getdate()) as estimated_completion_time
   FROM sys.dm_exec_requests r
   CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) a
   WHERE r.command in ('BACKUP DATABASE','RESTORE DATABASE');
   ```

## Upgrade to paid instance

To use an unlimited paid Azure SQL Managed Instance, or if you run out of vCore hours, upgrade your free instance to a paid instance directly from the Azure portal.

To upgrade your instance, follow these steps:

1. Go to your [SQL managed instance](https://portal.azure.com/#view/HubsExtension/ServiceMenuBlade/~/SingleInstance/extension/SqlAzureExtension/menuId/AzureSqlHub/itemId/SingleInstance) in the Azure portal.
1. Select the free instance you want to upgrade to navigate to the **Overview** page for the SQL Managed Instance.
1. Under **Settings**, select **Compute + storage** to open the **Compute + storage** page. Alternatively, you can select **Upgrade to a paid offer** from the banner on the **Overview** page.
1. On the **Compute + storage** page:
   1. Choose the **Paid offer** under **Offer type** to upgrade your instance to the paid version.
   1. Review the **Estimated costs per month**.
   1. Select **Apply** to confirm the upgrade.

   :::image type="content" source="media/free-offer/upgrade-to-paid-offer.png" alt-text="Screenshot of the paid offer selected on the compute + storage page for your instance in the Azure portal." lightbox="media/free-offer/upgrade-to-paid-offer.png":::

> [!NOTE]  
> When you change a free SQL managed instance to a paid offer, the free offer isn't available for that instance. To continue using the free offer, you must create a new instance and apply the free offer again.

## Clean up resources

When you're done using your free Azure SQL Managed Instance or want to start fresh with a new one, you can delete the entire resource group or the individual instance. This action removes the instance and any associated databases.

To delete your resource group and all its resources by using the Azure portal, follow these steps:

1. In the [Azure portal](https://portal.azure.com), search for **Resource groups** in the top search bar and select it.
1. Select your resource group from the list - such as `myFreeMIResourceGroup`.
1. On the Resource group overview page, select **Delete resource group**.
1. When prompted, type the name of the resource group to confirm.
1. Select **Delete** to remove the group and **all its resources**.

Use a similar procedure to remove the individual free SQL managed instance resource.

> [!WARNING]  
> Deleting a resource group is irreversible. Make sure you no longer need any of the resources it contains.

## What's included in the free offer

The free Azure SQL Managed Instance offer is designed for:

- New customers who want to explore SQL Managed Instance and get started with cloud-native SQL Server capabilities.
- Existing customers who need a development environment to build proof-of-concept applications.
- Testing existing SQL Server workloads in Azure before committing to a paid plan.

The free SQL Managed Instance offer includes:

- One General Purpose, Standard-series (Gen 5) SQL managed instance per subscription.
- 4 vCores and **720 vCore hours** every month for 12 months.
- Creating up to **500 databases** in the Next-gen General Purpose service tier, or up to **100 databases** in the General Purpose service tier.
- 64 GB of data storage.
- SQL license for the instance.
- Automatically backed up databases retained for up to seven days with locally redundant backup storage in Azure.
- **Standard workday** [start/stop schedule enabled by default](#default-instance-schedule) to ensure efficient use of credits.
- The instance automatically stops when you reach the monthly vCore limit. When you set the start/stop schedule on the instance, the next scheduled start succeeds when credits are available again.
- No extra charges are applied until you decide to upgrade to paid version.

### Free offer limits

The free Azure SQL Managed Instance offer is the same fully managed platform as a service (PaaS) instance you get with the paid version. It handles all of the same database management functions, such as upgrading, patching, backups, and monitoring, without user involvement. You can get one free instance per Azure subscription.

The following table describes the limits of the free SQL Managed Instance:

|Category  |Limit  |
|---------|---------|
|Compute | 4 or 8 vCore instances only |
|Databases    | 100 (General Purpose) / 500 (Next-gen General Purpose)         |
|vCore hours    | 720 vCore hours monthly         |
|Storage     |  64 GB of data<sup>1</sup>       |
|IOPS | Depends on file size / 300 for Next-gen General Purpose<sup>2</sup> |
|Instances per subscription     |    1     |
|Service tiers | General purpose, and [Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) |
|Hardware | Standard only |
|Backup retention     |  1-7 days for short term retention       |
|SQL License cost     |  None     |
|Guaranteed SLA| None|

<sup>1</sup> Since system files can take up to 32 GB of storage, the available storage for user data could be less than 64 GB.
<sup>2</sup> Free instances that use the [Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) service tier upgrade are limited to 300 IOPS, and the IOPS slider is unavailable.

Additionally, the following limitations apply:

- The offer is valid for 12 months from the day you activate the offer. You can apply it to only one instance at a time, per subscription.
- After 12 months, the free SQL managed instance stops. If you don't upgrade the instance to a paid version within 30 days, the instance and all databases are deleted and no longer recoverable.
- The following capabilities aren't supported: [Zone redundancy](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability), [Failover groups](failover-group-sql-mi.md), [Long-term backup retention](../database/long-term-retention-overview.md), modifying the backup storage redundancy.
- You can only scale up and down within the free offer limits.
- The free offer is currently available in the [following regions](region-availability.md#free-offer).
- If you delete a free SQL managed instance, you delete all its databases and can't restore them.

### Monthly vCore limits

The monthly free limits include 720 vCore hours of compute. Your free month of credits starts when you create your instance and renews on the same day of the following month.

When you reach the monthly free vCore limit, the instance stops with a status of `Stopped - Insufficient credit`. A banner on the **Overview** page of your instance in the Azure portal gives you the option to create a new paid instance. You can [restore your database](point-in-time-restore.md#restore-an-existing-database) to the new instance to continue your business without the limits imposed by the free offer.

The free vCore hours renew on the same date each month, starting the day the offer is applied. If your instance stops due to insufficient credits, then after you have available credits again, the instance starts automatically at the next scheduled start. You can also stop the instance manually at any time to avoid using up your free monthly vCore hours.

On the **Overview** pane of your free instance in the Azure portal, you can identify:
- **Pricing tier**: the free offer applied to your instance.
- **Free vCore hours renew in**: the number of days until your free vCore hours recycle.
- **Free vCore hours**: the remaining free vCore hours for the current 30-day cycle.

:::image type="content" source="media/free-offer/remaining-credits.png" alt-text="Screenshot showing the remaining vCore hours for a free instance in the Azure portal." lightbox="media/free-offer/remaining-credits.png":::

> [!NOTE]  
> If you delete your original free SQL Managed Instance and create a new one, your free credits don't reset. The remaining vCore hours for the current month carry over to the new instance and your full 720 vCore hours are available again on your original monthly renewal date.

## Supported subscription types

The free Azure SQL Managed Instance offer is available on the following subscription types:

:::row:::
    :::column:::
      - [Pay-as-you-go (0003P)](https://azure.microsoft.com/pricing/offers/ms-azr-0003p/)
      - [Azure in CSP (0145P)](https://azure.microsoft.com/pricing/offers/ms-azr-0145p/)
      - [Azure Plan (0017G)](https://azure.microsoft.com/pricing/offers/ms-azr-0017g/)
      - [Enterprise Agreement Support](https://azure.microsoft.com/pricing/offers/enterprise-agreement-support/)
      - [Microsoft Azure EA Sponsorship (0136P)](https://azure.microsoft.com/pricing/offers/ms-azr-0136p/)
      - [Visual Studio Professional subscribers (0059P)](https://azure.microsoft.com/pricing/offers/ms-azr-0059p/)
    :::column-end:::
    :::column:::
      - [Visual Studio Test Professional subscribers (0059P)](https://azure.microsoft.com/pricing/offers/ms-azr-0060p/)
      - [Visual Studio Enterprise subscribers (0063P)](https://azure.microsoft.com/pricing/offers/ms-azr-0063p/)
      - [Pay-As-You-Go Dev/Test (0023P)](https://azure.microsoft.com/pricing/offers/ms-azr-0023p/)
      - [Enterprise Dev/Test (0148P)](https://azure.microsoft.com/pricing/offers/ms-azr-0148p/)
      - [Azure in Open Licensing (0111P)](https://azure.microsoft.com/pricing/offers/ms-azr-0111p?cid=msft_learn)
    :::column-end:::
:::row-end:::

## Related content

- [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md)
- [Azure SQL Database and Azure SQL Managed Instance connect and query articles](../database/connect-query-content-reference-guide.md)
- [Quickstart: Use SSMS to connect to and query Azure SQL Database or Azure SQL Managed Instance](../database/connect-query-ssms.md)
