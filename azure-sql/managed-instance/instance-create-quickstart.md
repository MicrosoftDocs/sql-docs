---
title: "Quickstart: Create Azure SQL Managed Instance"
description: Create Azure SQL Managed Instance by using the Azure portal, PowerShell, and the Azure CLI. 
author: urosran
ms.author: urandjelovic
ms.reviewer: mathoma
ms.date: 02/26/2024
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: quickstart
ms.custom: mode-ui, devx-track-azurecli
---
# Quickstart: Create Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This quickstart teaches you to create a deployment of [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) by using the Azure portal, PowerShell, and the Azure CLI. 

[!INCLUDE [azure-sql-managed-instance-free-offer-note](../includes/azure-sql-managed-instance-free-offer-note.md)]

## Prerequisites

- An Azure subscription. If you don't have an Azure subscription, [create a free account](https://azure.microsoft.com/free/)
- The latest version of the [Az.SQL](https://www.powershellgallery.com/packages/Az.Sql) PowerShell module or the latest version of the [Azure CLI](/cli/azure/install-azure-cli-windows). 

For limitations, see [Supported regions](resource-limits.md#supported-regions) and [Supported subscription types](resource-limits.md#supported-subscription-types).

## Create Azure SQL Managed Instance

You can create a deployment of Azure SQL Managed Instance by using the Azure portal, PowerShell, and the Azure CLI. 

Consider the following:

- You can [cancel the provisioning process](management-operations-cancel.md) through Azure portal, or via PowerShell or the Azure CLI or other tooling using the REST API.
- Instance deployment delayed if it's [impacted by other operations](management-operations-overview.md#management-operations-cross-impact) in the same subnet, such as a long-running restore or scaling an instance. 
- **Read** permissions for the resource group are required to see the managed instance in your resource group. 

> [!IMPORTANT]
> Deploying a managed instance is a [long-running operation](management-operations-overview.md#duration). Deploying the first instance to a subnet typically takes much longer than deploying to a subnet with existing instances. 



### [Portal](#tab/azure-portal)

### Sign in to the Azure portal

To create your instance in the Azure portal, you'll first need to sign into the Azure portal, and then fill out the information on the **Create Azure SQL Managed Instance** page. 

To create your instance, follow these steps: 

1. Sign in to the [Azure portal](https://portal.azure.com/).
1. Select **Azure SQL** on the left menu of the Azure portal. If **Azure SQL** isn't in the list, select **All services**, and then enter **Azure SQL** in the search box.
1. Select **+ Create** to open the **Select SQL deployment option** page. You can view additional information about Azure SQL Managed Instance by selecting **Show details** on the **SQL managed instances** tile.
1. Choose *Singe instance* from the dropdown and then select **Create** to open the **Create Azure SQL Managed Instance** page.

   :::image type="content" source="./media/instance-create-quickstart/select-sql-deploment-page.png" alt-text="Screenshot of the select SQL deployment page in the Azure portal.":::


### Basics tab

Fill out mandatory information required on the **Basics** tab, which is the minimum requirement to provision a SQL Managed Instance. 

The following table provides details for the required information on the **Basics** tab: 

   | Setting| Suggested value | Description |
   | ------ | --------------- | ----------- |
   | **Subscription** | Your subscription. | A subscription that gives you permission to create new resources. |
   | **Resource group** | A new or existing resource group.|For valid resource group names, see [Naming rules and restrictions](/azure/architecture/best-practices/resource-naming).|
   | **Managed Instance name** | Any valid name.|For valid names, see [Naming rules and restrictions](/azure/architecture/best-practices/resource-naming).|
   | **Region** |The region in which you want to create the managed instance.|For information about regions, see [Azure regions](https://azure.microsoft.com/regions/).|
   | **Belongs to an instance pool?** | Select **Yes** if you want this instance to be created inside an [instance pool](instance-pools-configure.md). | 
   | **Authentication method** | Use SQL authentication | For the purpose of this quickstart, use SQL authentication. But you can also choose to use both SQL and [Microsoft Entra](../database/authentication-aad-overview.md) authentication. | 
   | **Managed instance admin login** | Any valid username. | For valid names, see [Naming rules and restrictions](/azure/architecture/best-practices/resource-naming). Don't use `serveradmin` because that's a reserved server-level role.|
   | **Password** | Any valid password.| The password must be at least 16 characters long and meet the [defined complexity requirements](/azure/virtual-machines/windows/faq#what-are-the-password-requirements-when-creating-a-vm-).|

Under **Managed Instance details**, select **Configure Managed Instance** in the **Compute + storage** section to open the **Compute + storage** page. 

:::image type="content" source="media/instance-create-quickstart/open-compute-storage-page.png" alt-text="Screenshot of the Create Azure SQL Managed Instance page with Configure Managed Instance selected.":::

The following table provides recommendations for the compute and storage for your sample SQL Managed Instance: 

| Setting| Suggested value | Description |
| ------ | --------------- | ----------- |
| **Service Tier** | General Purpose | The **General Purpose** tier is suitable for most production workloads, and is the default option. The improved [Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) service tier is also a great choice for most workloads. For more information, review [resource limits](resource-limits.md).|
| **Hardware generation** | Standard-series (Gen5) | Standard-series (gen5) is the default [hardware generation](resource-limits.md#hardware-configuration-characteristics), which defines compute and memory limits. **Standard-series (Gen5)** is the default.|
| **vCores** | Designate a value. | vCores represent the exact amount of compute resources that are always provisioned for your workload. **Eight vCores** is the default.|
| **Storage in GB** | Designate a value. | Storage size in GB, select based on expected data size. |
| **SQL Server License** | Select applicable licensing model. | Either pay as you go, use an existing SQL license with the [Azure Hybrid Benefit](../azure-hybrid-benefit.md), or enable the [Hybrid failover rights](managed-instance-link-feature-overview.md#license-free-passive-dr-replica) |
| **Backup storage redundancy** | **Geo-redundant backup storage**. | [Storage redundancy](automated-backups-overview.md#backup-storage-redundancy) inside Azure for backup storage. Geo-redundant backup storage is default and recommended, though Geo-zone, Zone and Local redundancy allow for greater cost flexibility and single region data residency.|

Once you've designated your **Compute + Storage** configuration, use **Apply** to save your settings, and navigate back to the **Create Azure SQL Managed Instance** page. Select **Next** to move to the **Networking tab**

### Networking tab

Fill out optional information on the **Networking** tab. If you omit this information, the portal will apply default settings.

The following table provides details for information on the **Networking** tab: 

   | Setting| Suggested value | Description |
   | ------ | --------------- | ----------- |
   | **Virtual network / subnet** | Create new, or use an existing virtual network |  If a network or subnet is unavailable, it must be [modified to satisfy the network requirements](vnet-existing-add-subnet.md) before you select it as a target for the new managed instance.  |
   | **Connection type** | Choose a suitable connection type.|For more information, see [connection types](../database/connectivity-architecture.md#connection-policy).|
   | **Public endpoint**  | Select **Disable**. | For a managed instance to be accessible through the public data endpoint, you need to enable this option. | 
   | **Allow access from** (if **Public endpoint** is enabled) | Select **No Access**  |The portal configures the security group with a public endpoint. </br> </br> Based on your scenario, select one of the following options: </br> <ul> <li>**Azure services**: We recommend this option when you're connecting from Power BI or another multitenant service. </li> <li> **Internet**: Use for test purposes when you want to quickly spin up a managed instance. Not recommended for production environments. </li> <li> **No access**: This option creates a **Deny** security rule. Modify this rule to make a managed instance accessible through a public endpoint. </li> </ul> </br> For more information on public endpoint security, see [Using Azure SQL Managed Instance securely with a public endpoint](public-endpoint-overview.md).|

Select **Review + create** to review your choices before you create a managed instance. Or, configure security settings by selecting **Next: Security settings**.

### Security tab

For the purpose of this quickstart, leave the settings on the **Security** tab at their default values. 

Select **Review + create** to review your choices before you create a managed instance.  Or, configure more custom settings by selecting **Next: Additional settings**.

### Additional settings

Fill out optional information on the **Additional settings** tab. If you omit this information, the portal applies default settings.

The following table provides details for information on the **Additional settings** tab: 

   | Setting| Suggested value | Description |
   | ------ | --------------- | ----------- |
   | **Collation** | Choose the collation that you want to use for your managed instance. If you migrate databases from SQL Server, check the source collation by using `SELECT SERVERPROPERTY(N'Collation')` and use that value.| For information about collations, see [Set or change the server collation](/sql/relational-databases/collations/set-or-change-the-server-collation).|   
   | **Time zone** | Select the time zone that managed instance will observe.|For more information, see [Time zones](timezones-overview.md).|
   | **Geo-Replication** | Select **No**. | Only enable this option if you plan to use the managed instance as a failover group secondary.|
   | **Maintenance window** | Choose a suitable maintenance window. | Designate a[schedule for when your instance is [maintained](../database/maintenance-window.md) by the service. | 

Select **Review + create** to review your choices before you create a managed instance. Or, configure Azure Tags by selecting **Next: Tags** (recommended).

### Tags

Add tags to resources in your Azure Resource Manager template (ARM template). [Tags](/azure/azure-resource-manager/management/tag-resources) help you logically organize your resources. The tag values show up in cost reports and allow for other management activities by tag.  Consider at least tagging your new SQL Managed Instance with the Owner tag to identify who created, and the Environment tag to identify whether this system is Production, Development, etc. For more information, see [Develop your naming and tagging strategy for Azure resources](/azure/cloud-adoption-framework/ready/azure-best-practices/naming-and-tagging).
 
Select **Review + create** to proceed.

### Review + create

On the **Review + create** tab, review your choices, and then select **Create** to deploy your managed instance. 

### Monitor deployment progress

1. Select the **Notifications** icon to view the status of the deployment.

   :::image type="content" source="./media/instance-create-quickstart/azure-sql-managed-instance-create-deployment-in-progress.png" alt-text="Screenshot of the Deployment progress of a SQL Managed Instance deployment.":::

1. Select **Deployment in progress** in the notification to open the SQL Managed Instance window and further monitor the deployment progress. 

Once deployment completes, navigate to your resource group to view your managed instance: 

   :::image type="content" source="./media/instance-create-quickstart/azure-sql-managed-instance-resources.png" alt-text="Screenshot of the SQL Managed Instance resources in the Azure portal.":::

> [!TIP]
> If you closed your web browser or moved away from the deployment progress screen, you can [monitor the provisioning operation](management-operations-monitor.md#monitor-operations) via the managed instance's **Overview** page in the Azure portal, PowerShell or the Azure CLI. 

### [PowerShell](#tab/powershell)

Use PowerShell to create your instance. For details, review [Create SQL Managed Instance with PowerShell](scripts/create-configure-managed-instance-powershell.md).

First, set your variables: 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/create-and-configure-managed-instance.ps1" id="SetVariables":::

Next, create your resource group: 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/create-and-configure-managed-instance.ps1" id="CreateResourceGroup":::

After that, create your virtual network: 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/create-and-configure-managed-instance.ps1" id="CreateVirtualNetwork":::

And finally, create your instance: 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/create-and-configure-managed-instance.ps1" id="CreateManagedInstance":::


### [Azure CLI](#tab/cli)

Use the Azure CLI to create your instance. For details, review [Create SQL Managed Instance with the Azure CLI](scripts/create-configure-managed-instance-cli.md).

First, set your variables: 

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/managed-instance/create-managed-instance.sh"id="SetVariables":::

Next, create your resource group: 

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/managed-instance/create-managed-instance.sh" id="CreateResourceGroup":::

After that, create your virtual network: 

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/managed-instance/create-managed-instance.sh" id="CreateVirtualNetwork":::

And finally, create your instance: 

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/managed-instance/create-managed-instance.sh" id="CreateManagedInstance":::

---

## Review network settings 

Select the **Route table** resource in your resource group to review the default [user-defined route table object and entries to route traffic](subnet-service-aided-configuration-enable.md#mandatory-security-rules-and-routes) from, and within, the SQL Managed Instance virtual network. To change or add routes, open the **Routes** in the Route table settings.

:::image type="content" source="./media/instance-create-quickstart/azure-sql-managed-instance-route-table-user-defined-route.png" alt-text="Screenshot of the Entry for a SQL Managed Instance subnet to local in the Azure portal. ":::

Select the **Network security group** object to review the inbound and outbound security rules. To change or add rules, open the **Inbound Security Rules** and **Outbound security rules** in the Network security group settings.

:::image type="content" source="./media/instance-create-quickstart/azure-sql-managed-instance-security-rules.png" alt-text="Screenshot of the Security rules for your instance in the Azure portal.":::


> [!IMPORTANT]
> If you [enabled public endpoint](public-endpoint-configure.md#allow-public-endpoint-traffic-in-the-network-security-group) for SQL Managed Instance, open ports to allow network traffic connections to SQL Managed Instance from the public internet. 

## Create database 

You can create a new database by using the Azure portal, PowerShell, or the Azure CLI. 

### [Portal](#tab/azure-portal)

To create a new database for your instance in the Azure portal, follow these steps:

1. Go to your [SQL managed instance](https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2FmanagedInstances) in the Azure portal. 
1. Select **+ New database** on the **Overview** page for your SQL managed instance to open the **Create Azure SQL Managed Database** page.  

   :::image type="content" source="media/instance-create-quickstart/create-new-database-portal.png" alt-text="Screenshot of creating a new database in the Azure portal. ":::

1. Provide a name for the database on the **Basics** tab. 
1. On the **Data source** tab, select **None** for an empty database, or restore a database from backup. 
1. Configure the remaining settings on the remaining tabs, and then select **Review + create** to validate your choices. 
1. Use **Create** to deploy your database. 

### [PowerShell](#tab/powershell)

Use PowerShell to create your database: 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/create-and-configure-managed-instance.ps1" id="CreateDatabase":::

### [Azure CLI](#tab/cli)

Use the Azure CLI to create your database: 

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/managed-instance/create-managed-instance.sh"id="CreateDatabase":::

---


## Retrieve connection details to SQL Managed Instance

To connect to SQL Managed Instance, follow these steps to retrieve the host name and fully qualified domain name (FQDN):

1. Return to the resource group and select the SQL managed instance object that was created.

2. On the **Overview** tab, locate the **Host** property. Copy the host name to your clipboard for the managed instance for use in the next quickstart by clicking the **Copy to clipboard** button.

   :::image type="content" source="./media/instance-create-quickstart/azure-sql-managed-instance-host-name.png" alt-text="Screenshot of the Overview page for the instance in the Azure portal with the hostname selected.":::

   The value copied represents a fully qualified domain name (FQDN) that can be used to connect to SQL Managed Instance. It's similar to the following address example: *your_host_name.a1b2c3d4e5f6.database.windows.net*.

## Related content 

Review the following related content: 

- [Configure an Azure virtual machine connection](connect-vm-instance-configure.md)
- [Migration overview: SQL Server to SQL Managed Instance](../migration-guides/managed-instance/sql-server-to-managed-instance-overview.md)
- [Configure a point-to-site connection](point-to-site-p2s-configure.md)
- [Monitor Azure SQL Managed Instance by using Azure SQL Analytics](/azure/azure-monitor/insights/azure-sql)

To restore an existing SQL Server database from on-premises to SQL Managed Instance: 
- Use the [Azure Database Migration Service](/azure/dms/tutorial-sql-server-managed-instance-online-ads) to restore from a database backup file.
- Use the [T-SQL RESTORE command](restore-sample-database-quickstart.md) to restore from a database backup file.
 
## Next steps

[Connect your applications to SQL Managed Instance](connect-application-instance.md).
