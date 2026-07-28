---
title: Configure service endpoint policies
description: Configure Azure Storage service endpoint policies to protect Azure SQL Managed Instance against exfiltration to unauthorized Azure Storage accounts.
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: mathoma
ms.date: 03/31/2026
ms.service: azure-sql-managed-instance
ms.subservice: security
ms.topic: how-to
ms.custom:
---


# Configure service endpoint policies for Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

By using [service endpoint policies](/azure/virtual-network/virtual-network-service-endpoint-policies-overview) for Azure Storage service endpoints on a virtual network (VNet) subnet, you can filter egress virtual network traffic to Azure Storage and restrict data transfers to specific storage accounts.

## Key benefits

Configuring Virtual Network Azure Storage service endpoint policies for your Azure SQL Managed Instance provides the following benefits:

- __Improved security for your Azure SQL Managed Instance traffic to Azure Storage__: Endpoint policies establish a security control that prevents erroneous or malicious exfiltration of business-critical data. You can limit traffic to only those storage accounts that are compliant with your data governance requirements.

- __Granular control over which storage accounts can be accessed__: Service endpoint policies can permit traffic to storage accounts at a subscription, resource group, and individual storage account level. Administrators can use service endpoint policies to enforce adherence to the organization's data security architecture in Azure.

- __System traffic remains unaffected__: Service endpoint policies never obstruct access to storage that is required for Azure SQL Managed Instance to function. This storage includes backups, data files, transaction log files, and other assets.

Service endpoint policies only control traffic that originates from the SQL Managed Instance subnet and terminates in Azure Storage. They don't affect other means of data egress. For example, they don't affect exporting the database to an on-premises BACPAC file, Azure Data Factory integration, data exfiltration to other cloud providers, or other mechanisms of data extraction that don't directly target Azure Storage. You can protect those pathways by using other means of traffic control, such as user-defined routes, network security groups, and Azure Firewall.

## Limitations

Service endpoint policies for Azure SQL Managed Instance have the following limitations:

- Service endpoint policies for Azure Storage in managed instance subnets are available in all Azure regions where SQL Managed Instance is supported except those listed in [Service endpoint policy regional availability](region-availability.md#service-endpoint-policy). 
- You can use this feature only with virtual networks deployed through the Azure Resource Manager deployment model.
- You can use this feature only in subnets that have [service endpoints](/azure/virtual-network/virtual-network-service-endpoints-overview) for Azure Storage enabled.
- Assigning a service endpoint policy to a service endpoint upgrades the endpoint from regional to global scope. In other words, all traffic to Azure Storage goes through the service endpoint regardless of the region in which the storage account resides.
- Allowing a storage account automatically allows access to its RA-GRS secondary, if it exists.

## Prepare storage inventory

Before you configure service endpoint policies on a subnet, create a list of storage accounts the managed instance needs access to in that subnet.

The following workflows might contact Azure Storage:

- [SQL Server Audit in Azure SQL Managed Instance](auditing.md) to Azure Storage.
- Performing a [copy-only backup](/sql/relational-databases/backup-restore/copy-only-backups-sql-server) to Azure Storage.
- [Restoring](restore-sample-database-quickstart.md) a database from Azure Storage.
- Importing data by using [BULK INSERT or OPENROWSET(BULK ...)](/sql/relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server).
- Logging [extended events](../database/xevent-db-diff-from-svr.md) to an Event File target on Azure Storage.
- [Azure DMS offline migration](/azure/dms/tutorial-sql-server-to-managed-instance) to Azure SQL Managed Instance.
- [Log Replay Service migration](log-replay-service-migrate.md) to Azure SQL Managed Instance.
- Synchronizing tables by using [transactional replication](replication-transactional-overview.md).

Note the account name, resource group, and subscription for any storage account that participates in these or any other workflows that access storage.

## Configure policies

First, create your service endpoint policy, and then associate it with the SQL Managed Instance subnet. Modify the workflow in this section to suit your business needs.

> [!NOTE]
> - SQL Managed Instance subnets require policies to contain the /Services/Azure/ManagedInstance service alias (see step 5). 

### Create a service endpoint policy

To create a service endpoint policy, follow these steps:

1. Sign in to the [Azure portal](https://portal.azure.com). 
1. Select **+ Create a resource**. 
1. In the search pane, enter *service endpoint policy*, select **Service endpoint policy**, and then select **Create**.

   :::image type="content" source="./media/service-endpoint-policies-configure/create-sep-add-resource.png" alt-text="Screenshot that shows creating a service endpoint policy." lightbox="./media/service-endpoint-policies-configure/create-sep-add-resource.png":::

1. Fill in the following values on the **Basics** page:

   - Subscription: Select the subscription for your policy from the drop-down.
   - Resource group: Select the resource group where your managed instance is located, or select **Create new** and fill in the name for a new resource group.
   - Name: Provide a name for your policy, such as **mySEP**.
   - Location: Select the region of the virtual network hosting the managed instance. 

   :::image type="content" source="./media/service-endpoint-policies-configure/create-sep-basics.png" alt-text="Screenshot that shows the Basics page for creating a service endpoint policy." lightbox="./media/service-endpoint-policies-configure/create-sep-basics.png":::

1. In **Policy definitions**, select **Add an alias** and enter the following information on the **Add an alias** pane:
   - Service Alias: Select /Services/Azure/ManagedInstance.
   - Select **Add** to finish adding the service alias.

   :::image type="content" source="./media/service-endpoint-policies-configure/add-an-alias.png" alt-text="Screenshot that shows adding an alias to a service endpoint policy." lightbox="./media/service-endpoint-policies-configure/add-an-alias.png":::

1. In **Policy definitions**, select **+ Add** under **Resources** and enter or select the following information in the **Add a resource** pane:
   - Service: Select **Microsoft.Storage**.
   - Scope: Select **All accounts in subscription**.
   - Subscription: Select a subscription containing the storage accounts to permit. Refer to your [inventory of Azure storage accounts](#prepare-storage-inventory) created earlier.
   - Select **Add** to finish adding the resource.
   - Repeat this step to add any additional subscriptions. 

   :::image type="content" source="./media/service-endpoint-policies-configure/add-a-resource.png" alt-text="Screenshot that shows adding a resource to a service endpoint policy." lightbox="./media/service-endpoint-policies-configure/add-a-resource.png":::

1. (Optional) Configure tags on the service endpoint policy under **Tags**.
1.    Select **Review + Create**. Validate the information and select **Create**. To make further edits, select **Previous**.

   > [!TIP]
   > First, configure policies to allow access to entire subscriptions. Validate the configuration by ensuring that all workflows operate normally. Then, optionally, reconfigure policies to allow individual storage accounts, or accounts in a resource group. To do so, select **Single account** or **All accounts in resource group** in the _Scope:_ field instead and fill in the other fields accordingly.

### Associate policy with subnet

After you create your service endpoint policy, associate the policy with your SQL Managed Instance subnet.

To associate your policy, follow these steps:

1. In the _All services_ box in the Azure portal, search for _virtual networks_. Select **Virtual networks**.
1. Locate and select the virtual network hosting your managed instance.
1. Select **Subnets** and choose the subnet dedicated to your managed instance. Enter the following information in the subnet pane:
    - Services: Select **Microsoft.Storage**. If this field is empty, you need to configure the service endpoint for Azure Storage on this subnet.
    - Service endpoint policies: Select any service endpoint policies you want to apply to the SQL Managed Instance subnet.

   :::image type="content" source="./media/service-endpoint-policies-configure/associate-service-endpoint-policy.png" alt-text="Screenshot that shows associating a service endpoint policy with a subnet." lightbox="./media/service-endpoint-policies-configure/associate-service-endpoint-policy.png":::

1. Select **Save** to finish configuring the virtual network.

> [!WARNING]
> If the policies on this subnet don't have the `/Services/Azure/ManagedInstance` alias, you might see the following error: 
> ` Failed to save subnet 'subnet'. Error: 'Found conflicts with NetworkIntentPolicy.`
> `Details: Service endpoint policies on subnet are missing definitions`
> To resolve this error, update all the policies on the subnet to include the `/Services/Azure/ManagedInstance` alias.

## Related content

- [Secure your Azure Storage accounts](/azure/storage/common/storage-network-security)
- [SQL Managed Instance security capabilities](../database/security-overview.md)
- [Connectivity architecture for Azure SQL Managed Instance](connectivity-architecture-overview.md)
- [Modifiable configuration reference for Azure SQL Managed Instance](modifiable-configuration-reference.md)
