---
title: Configure public endpoint
description: "Learn how to configure a public endpoint for Azure SQL Managed Instance by using the Azure portal, Azure PowerShell, or the Azure CLI." 
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: vanto, mathoma
ms.date: 07/14/2023
ms.service: azure-sql-managed-instance
ms.subservice: security
ms.topic: how-to
ms.custom: sqldbrb=1, devx-track-azurepowershell, devx-track-azurecli
---
# Configure public endpoints in Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

Public endpoints for [Azure SQL Managed Instance](./sql-managed-instance-paas-overview.md) enable data access to your managed instance from outside the [virtual network](/azure/virtual-network/virtual-networks-overview). You're able to access your managed instance from multitenant Azure services like Power BI, Azure App Service, or an on-premises network. By using the public endpoint on a managed instance, you don't need to use a VPN, which can help avoid VPN throughput issues.

In this article, you learn how to:

> [!div class="checklist"]
>
> - Enable or disable a public endpoint for your managed instance
> - Configure your managed instance network security group (NSG) to allow traffic to the managed instance public endpoint
> - Obtain the managed instance public endpoint connection string

## Permissions

Due to the sensitivity of data that in a managed instance, the configuration to enable managed instance public endpoint requires a two-step process. This security measure adheres to separation of duties (SoD):

- The managed instance admin needs to enable the public endpoint on the managed instance. The managed instance admin can be found on the **Overview** page for your managed instance resource.
- A network admin needs to allow traffic to the managed instance using a network security group (NSG). For more information, review [network security group permissions](/azure/virtual-network/manage-network-security-group#permissions).

## Enable public endpoint

You can enable the public endpoint for your SQL Managed Instance by using the Azure portal, Azure PowerShell, or the Azure CLI. 

### [Azure portal](#tab/azure-portal)

To enable the public endpoint for your SQL Managed Instance in the Azure portal, follow these steps: 

1. Go to the [Azure portal](https://portal.azure.com).
1. Open the resource group with the managed instance, and select the **SQL managed instance** that you want to configure public endpoint on.
1. On the **Security** settings, select the **Networking** tab.
1. In the Virtual network configuration page, select **Enable** and then the **Save** icon to update the configuration.

:::image type="content" source="./media/public-endpoint-configure/mi-vnet-config.png" alt-text="Screenshot shows the Virtual network page of SQL Managed Instance with the Public endpoint enabled.":::

### [Azure PowerShell](#tab/azure-powershell)

To enable the public endpoint with PowerShell, set `-PublicDataEndpointEnabled` to _true_ when you update instance properties with [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance). 

Use the sample PowerShell script to enable the public endpoint for your SQL Managed Instance. Replace the following values: 
-  **subscription-id** with your subscription ID
-  **rg-name** with the resource group of your managed instance
-  **mi-name** with the name of your managed instance

To enable the public endpoint by using PowerShell, run the following script: 

```powershell
Install-Module -Name Az

Import-Module Az.Accounts
Import-Module Az.Sql

Connect-AzAccount

# Use your subscription ID in place of subscription-id below

Select-AzSubscription -SubscriptionId {subscription-id}

# Replace rg-name with the resource group for your managed instance, and replace mi-name with the name of your managed instance

$mi = Get-AzSqlInstance -ResourceGroupName {rg-name} -Name {mi-name}

$mi = $mi | Set-AzSqlInstance -PublicDataEndpointEnabled $true -force
```

### [Azure CLI](#tab/azure-cli)

To enable the public endpoint with the Azure CLI,  set `--public-data-endpoint-enabled` to _true_ when you update instance properties with [az sql mi update](/cli/azure/sql/mi#az-sql-mi-update).

Use the sample Azure CLI command to enable the public endpoint for your SQL Managed Instance. Replace the following values: 
- **subscription** with your subscription ID
- **rg-name** with the resource group of your managed instance
- **mi-name** with the name of your managed instance

To enable the public endpoint by using the Azure CLI, run the following command: 

```azurecli
az sql mi update --subscription {subscription-id} --resource-group {rg-name} --name {mi-name} --public-data-endpoint-enabled true
```

---

## Disable public endpoint 

You can disable the public endpoint for your SQL Managed Instance by using the Azure portal, Azure PowerShell, and the Azure CLI. 

### [Azure portal](#tab/azure-portal)

To disable the public endpoint by using the Azure portal, follow these steps: 

1. Go to the [Azure portal](https://portal.azure.com).
1. Open the resource group with the managed instance, and select the **SQL managed instance** that you want to configure public endpoint on.
1. On the **Security** settings, select the **Networking** tab.
1. In the Virtual network configuration page, select **Disable** and then the **Save** icon to update the configuration.

### [Azure PowerShell](#tab/azure-powershell)

To disable the public endpoint with PowerShell, set `-PublicDataEndpointEnabled` to _false_ when you update instance properties with [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance). 

Use Azure PowerShell to disable the public endpoint for your SQL Managed Instance. Remember to also close the inbound security rule for port 3342 in your network security group (NSG) if you've [configured it](#allow-public-endpoint-traffic-in-the-network-security-group).

To disable the public endpoint, use the following command: 

```powershell
Set-AzSqlInstance -PublicDataEndpointEnabled $false -force
```

### [Azure CLI](#tab/azure-cli)

To disable the public endpoint with the Azure CLI,  set `--public-data-endpoint-enabled` to _false_ when you update instance properties with [az sql mi update](/cli/azure/sql/mi#az-sql-mi-update).

Use the Azure CLI to disable the public endpoint for your SQL Managed Instance. Replace the following values: 
- **subscription** with your subscription ID
- **rg-name** with the resource group of your managed instance
-  **mi-name** with the name of your managed instance.

Remember to also close the inbound security rule for port 3342 in your network security group (NSG) if you've [configured it](#allow-public-endpoint-traffic-in-the-network-security-group).

To disable the public endpoint, use the following command: 

```azurecli
az sql mi update --subscription {subscription-id} --resource-group {rg-name} --name {mi-name} --public-data-endpoint-enabled false
```

---


## Allow public endpoint traffic in the network security group

Use the Azure portal to allow public traffic within the network security group. Follow these steps: 

1. Go to the **Overview** page for your SQL Managed Instance in the [Azure portal](https://portal.azure.com). 
1. Select the **Virtual network/subnet** link, which takes you to the **Virtual network configuration** page.

    :::image type="content" source="./media/public-endpoint-configure/mi-overview.png" alt-text="Screenshot shows the Virtual network configuration page where you can find your Virtual network/subnet value.":::

1. Select the **Subnets** tab on the configuration pane of your Virtual network, and make note of the **SECURITY GROUP** name for your managed instance.

    :::image type="content" source="./media/public-endpoint-configure/mi-vnet-subnet.png" alt-text="Screenshot shows the Subnet tab, where you can get the SECURITY GROUP for your managed instance.":::

1. Go back to the resource group that contains your managed instance. You should see the **Network security group** name noted previously. Select the **Network security group** name to open the **Network Security Group** configuration page.

1. Select the **Inbound security rules** tab, and **Add** a rule that has higher priority than the **deny_all_inbound** rule with the following settings: </br> </br>

    |Setting  |Suggested value  |Description  |
    |---------|---------|---------|
    |**Source**     |Any IP address or Service tag         |<ul><li>For Azure services like Power BI, select the Azure Cloud Service Tag</li> <li>For your computer or Azure virtual machine, use NAT IP address</li></ul> |
    |**Source port ranges**     |* |Leave this to * (any) as source ports are typically dynamically allocated and as such, unpredictable |
    |**Destination**     |Any         |Leaving destination as Any to allow traffic into the managed instance subnet |
    |**Destination port ranges**     |3342         |Scope destination port to 3342, which is the managed instance public TDS endpoint |
    |**Protocol**     |TCP         |SQL Managed Instance uses TCP protocol for TDS |
    |**Action**     |Allow         |Allow inbound traffic to managed instance through the public endpoint |
    |**Priority**     |1300         |Make sure this rule is higher priority than the **deny_all_inbound** rule |

    :::image type="content" source="./media/public-endpoint-configure/mi-nsg-rules.png" alt-text="Screenshot shows the Inbound security rules with your new public_endpoint_inbound rule above the deny_all_inbound rule.":::

    > [!NOTE]
    > Port 3342 is used for public endpoint connections to managed instance, and can't be changed currently.


## Confirm that routing is properly configured

A route with the 0.0.0.0/0 address prefix instructs Azure how to route traffic destined for an IP address that is not within the address prefix of any other route in a subnet's route table. When a subnet is created, Azure creates a default route to the 0.0.0.0/0 address prefix, with the **Internet** next hop type.

Overriding this default route without adding the necessary route(s) to ensure the public endpoint traffic is routed directly to **Internet** may cause asymmetric routing issues since incoming traffic does not flow via the Virtual appliance/Virtual network gateway. Ensure that all traffic reaching the managed instance over public internet goes back out over public internet as well by either adding specific routes for each source or setting the default route to the 0.0.0.0/0 address prefix back to **Internet** as next hop type.

See more the details about impact of changes on this default route at [0.0.0.0/0 address prefix](/azure/virtual-network/virtual-networks-udr-overview#default-route).

## Obtain the public endpoint connection string

1. Navigate to the managed instance configuration page that has been enabled for public endpoint. Select the **Connection strings** tab under the **Settings** configuration.
1. The public endpoint host name comes in the format <mi_name>.**public**.<dns_zone>.database.windows.net and that the port used for the connection is 3342. Here's an example of a server value of the connection string denoting the public endpoint port that can be used in SQL Server Management Studio or Azure Data Studio connections: `<mi_name>.public.<dns_zone>.database.windows.net,3342`

    :::image type="content" source="./media/public-endpoint-configure/mi-public-endpoint-conn-string.png" alt-text="Screenshot shows the connection strings for your public and VNet-local endpoints." lightbox="./media/public-endpoint-configure/mi-public-endpoint-conn-string.png":::

## Next steps

Learn about using [Azure SQL Managed Instance securely with public endpoint](public-endpoint-overview.md).
