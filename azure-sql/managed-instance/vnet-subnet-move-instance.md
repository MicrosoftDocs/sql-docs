---
title: Move Instance to Another Subnet
titleSuffix: Azure SQL Managed Instance
description: Learn how to move an Azure SQL Managed Instance to another subnet with only a short downtime during failover - typically up to 10 seconds.
author: urosmil
ms.author: urmilano
ms.reviewer: mathoma, bonova, srbozovi, wiassaf`
ms.date: 12/12/2025
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: how-to
ms.custom:
  - ignite-2023
  - sfi-image-nochange
---
# Move Azure SQL Managed Instance across subnets

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article explains how to move Azure SQL Managed Instance from one subnet to another subnet in the same virtual network or a different virtual network. The operation is similar to scaling vCores or changing the instance service tier. During the move, SQL Managed Instance remains available, except for a short downtime when the failover happens - typically lasting up to 10 seconds, even if long-running transactions are interrupted.

Moving the instance to another subnet triggers the following virtual cluster operations:

- The virtual cluster builds out or resizes the underlying infrastructure in the destination subnet.
- The virtual cluster is removed or defragmented in the source subnet.

## Requirements and limitations

You must deploy SQL Managed Instance inside a dedicated subnet within an Azure [virtual network](/azure/virtual-network/virtual-networks-overview). The size of the subnet (subnet range) determines how many SQL managed instances you can deploy within the subnet. To deploy a SQL managed instance or move it to another subnet, the destination subnet must meet certain [network requirements](connectivity-architecture-overview.md#service-aided-subnet-configuration).

Before moving your instance to another subnet, consider familiarizing yourself with the following concepts:

- [Determine required subnet size and range for Azure SQL Managed Instance](vnet-subnet-determine-size.md).

- Choose between moving the instance to a [new subnet](virtual-network-subnet-create-arm-template.md) or [using an existing subnet](vnet-existing-add-subnet.md).

- Use [management operations](management-operations-overview.md) to automatically deploy new managed instances, update instance properties, or delete instances. You can [monitor](management-operations-monitor.md) these management operations.

Before initiating the subnet move, verify that connectivity is allowed between the source and destination subnets on ports 5022 and 11000-11999. This verification includes validating the entire network path and ensuring that any user-defined routes, network virtual appliances (NVAs), firewalls, or other network devices aren't blocking traffic on these ports.

### Subnet readiness

Before you move your SQL managed instance, confirm the subnet is marked as **Ready for Managed Instance**.

In the **Virtual network** UI of the Azure portal, virtual networks that meet the prerequisites for a SQL managed instance are categorized as **Ready for Managed Instance**. Virtual networks that have subnets with SQL managed instances already deployed to them display a SQL Managed Instance icon before the virtual network name. Empty subnets that are ready for a SQL managed instance display a Virtual network subnet icon.

Subnets that are marked as **Not ready** don't fulfill all the requirements for SQL Managed Instance deployment. To learn why the subnet isn't ready and if the subnet can meet [network requirements](connectivity-architecture-overview.md#service-aided-subnet-configuration), use the info icon on the right of the subnet name. These requirements include:

- delegating to the `Microsoft.Sql/managedInstances` resource provider
- attaching a route table
- attaching a network security group

If the subnet is part of another virtual network, you need:

- [Bi-directional peering](/azure/virtual-network/virtual-network-peering-overview) between current and destination virtual network.
- Current and destination subnets use separate route tables and network security groups.

After you satisfy these requirements, the subnet moves from the **Not ready** to the **Ready for Managed Instance** category and can be used for a SQL managed instance.

Subnets that are already in use (subnets used for instance deployments can't contain other resources), or subnets that have a different DNS zone (a cross-subnet instance move limitation), are always part of the **Not ready** category.

:::image type="content" source="media/vnet-subnet-move-instance/subnet-grouping-per-state.png" alt-text="Screenshot of the Azure SQL Managed Instance subnet options." lightbox="media/vnet-subnet-move-instance/subnet-grouping-per-state.png":::

Depending on the subnet state and designation, the following adjustments might be made to the destination subnet:

- **Ready for Managed Instance (contains existing SQL Managed Instance)**

  No adjustments are made. These subnets already contain SQL managed instances, and making any change to the subnet could affect existing instances.

- **Ready for Managed Instance (empty)**

  The workflow validates all the required rules in the network security group and route table, and adds any rules that are necessary but missing. Custom rules that you add to the source subnet configuration aren't copied to the destination subnet. You must manually replicate any customization of the source subnet configuration to the destination subnet. One way to achieve this replication is by using the same route table and network security group for the source and destination subnet.

### Destination subnet limitations

Consider the following limitations when choosing a destination subnet for an existing instance:

- You can move SQL Managed Instance to a subnet that is either:

  - In the same virtual network as the currently used one, or
  - In a [peered virtual network](/azure/virtual-network/tutorial-connect-virtual-networks-portal), if moving to a subnet in another virtual network.

- The DNS zone of the instances in the destination subnet must match the DNS zone of the instance being moved. This limitation applies if you plan to move to a nonempty subnet.

    You can specially prepare the destination subnet to retain the DNS zone of the SQL managed instance that you're moving. Prepare the subnet by creating a new SQL managed instance in an empty subnet and providing the `dnsZonePartner` parameter in the create request. This [parameter as a value accepts the ID of SQL Managed Instance](api-references-create-manage-instance.md), and in this case you can use the instance that would later be moved to the new subnet.

    Apart from this approach, there's no other way to dictate the DNS zone of SQL Managed Instance since it's randomly generated. Currently, there's no way to update the DNS zone of an existing SQL managed instance.

If you want to migrate a SQL Managed Instance with a [failover group](failover-group-sql-mi.md), the following prerequisites apply:

- The target subnet must allow the same replication traffic required for the failover group as the source subnet:

  Open both inbound and outbound ports 5022 and the range 11000-11999 in the Network Security Group (NSG) for connections from the other SQL managed instance subnet (the one that holds the failover group replica) to allow replication traffic between the two instances. Make sure the full network path also allows this traffic. A user-defined route, Network Virtual Appliance, firewall, or other intermediate device can still block the ports even when the NSG rules are correct.

- The target subnet can't have an overlapping address range with the subnet that holds the secondary instance replica of the failover group.

  For example, if MI1 is in subnet S1, the secondary instance in the failover group is MI2 in subnet S2. You want to move MI1 to subnet S3. Subnet S3 can't have an overlapping address range with subnet S2.

To learn more about configuring the network for failover groups, review [Enable geo-replication between SQL managed instances](failover-group-configure-sql-mi.md#establish-connectivity-between-the-instances).

## Operation steps

Moving an instance from one subnet to another involves many steps. Depending on how your SQL managed instance is configured, the move operation can take anywhere from 30 minutes to 6 hours.

The following table details the operation steps that occur during the instance move operation:

| Step name | Step description |
| --- | --- |
| Request validation | Validates the submitted parameters. If a misconfiguration is detected, the operation fails with an error. |
| Virtual cluster resizing or creation | Depending on the state of the destination subnet, the virtual cluster is either created or resized. |
| New instance startup | The SQL process starts on the deployed virtual cluster in the destination subnet. |
| Seeding database files or attaching database files | Depending on the service tier, either the database is seeded or the database files are attached. |
| Preparing failover and failover | After data is seeded or database files are reattached, the system prepares for failover. When everything is ready, the system performs a failover **with a short downtime**, that's typically less than 10 seconds. |
| Old SQL instance cleanup | Removes the old SQL process from the source virtual cluster. |
| Virtual cluster deletion | If it's the last instance within the source subnet, the final step deletes the virtual cluster synchronously. Otherwise, the virtual cluster is asynchronously defragmented. |

For a detailed explanation of the operation steps, see [Duration of management operations in Azure SQL Managed Instance](management-operations-duration.md).

## Move the instance

A cross-subnet instance move is part of the instance update operation. Existing instance update API, Azure PowerShell, and Azure CLI commands are enhanced with a subnet ID property.

In the Azure portal, use the subnet field on the **Networking** pane to move the instance to the destination subnet. When using Azure PowerShell or the Azure CLI, provide a different subnet ID in the update command to move the instance from an existing subnet to the destination subnet.

For a full reference of instance management commands, see [Managed API reference for Azure SQL Managed Instance](api-references-create-manage-instance.md).

# [Portal](#tab/azure-portal)

You can choose the instance subnet on the **Networking** pane of the Azure portal. Once you select a subnet and save your changes, the instance move operation starts.

The move operation first prepares the destination subnet for deployment, which can take several minutes. After the subnet is ready, the instance move management operation starts and appears in the Azure portal.

:::image type="content" source="media/vnet-subnet-move-instance/how-to-select-subnet.png" alt-text="Screenshot of How to select subnet on SQL Managed Instance networking pane." lightbox="media/vnet-subnet-move-instance/how-to-select-subnet.png":::

You can monitor instance move operations from the **Overview** pane of the Azure portal. Select the notification to open another pane that contains information about the current step, the total steps, and a button to cancel the operation.

:::image type="content" source="media/vnet-subnet-move-instance/monitor-subnet-move-operation.png" alt-text="Screenshot shows the Overview page where you can monitor the move operation and cancel it." lightbox="media/vnet-subnet-move-instance/monitor-subnet-move-operation.png":::

# [PowerShell](#tab/azure-powershell)

Use the Azure PowerShell command [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance) to move an instance after you create your subnet in the same virtual network as your destination subnet. To use an existing subnet, provide that subnet name in the PowerShell command.

The example PowerShell commands in this section prepare the destination subnet for instance deployment, and move the SQL managed instance.

Use the following PowerShell command to specify your parameters:

```powershell-interactive
### PART 1 - DEFINE PARAMETERS

#Generating basic parameters
$currentSubscriptionID = 'subscription-id'
$sqlMIResourceGroupName = 'resource-group-name-of-sql-mi'
$sqlMIName = 'sql-mi-name'
$sqlMIResourceVnetName = 'vnet-name-of-sql-mi'
$destinationSubnetName = 'name-of-the-destination-subnet-for-sql-mi'
```

Skip this command if your subnet already has instances deployed to it. If you're using a new subnet, use the following Azure PowerShell command to prepare your subnet:

```powershell-interactive
### PART 2 - PREPARE DESTINATION SUBNET

#Loading the url of script used for preparing the subnet for SQL MI deployment
$scriptUrlBase = 'https://raw.githubusercontent.com/Microsoft/sql-server-samples/master/samples/manage/azure-sql-db-managed-instance/delegate-subnet'

#Generating destination subnet parameters
$parameters = @{
    subscriptionId = $currentSubscriptionID
    resourceGroupName = $sqlMIResourceGroupName
    virtualNetworkName = $sqlMIResourceVnetName
    subnetName = $destinationSubnetName
}

#Initiating subnet preparation script
Invoke-Command -ScriptBlock ([Scriptblock]::Create((Invoke-WebRequest ($scriptUrlBase + '/delegateSubnet.ps1?t=' + [DateTime]::Now.Ticks)).Content)) -ArgumentList $parameters
```

> [!NOTE]  
> To learn more about the script that prepares the subnet, see [Configure an existing virtual network for Azure SQL Managed Instance](vnet-existing-add-subnet.md).

The following Azure PowerShell command moves the instance to the source subnet:

```powershell-interactive
### PART 3 - MOVE INSTANCE TO THE NEW SUBNET

Set-AzSqlInstance -Name $sqlMIName -ResourceGroupName $sqlMIResourceGroupName `
    -SubnetId "/subscriptions/$currentSubscriptionID/resourceGroups/$sqlMIResourceGroupName/providers/Microsoft.Network/virtualNetworks/$sqlMIResourceVnetName/subnets/$destinationSubnetName"
```

The following Azure PowerShell command moves the instance, and also provides a way to monitor progress:

```powershell-interactive
### PART 3 EXTENDED - MOVE INSTANCE AND MONITOR PROGRESS

# Extend the Set-AzSqlInstance command with -AsJob -Force parameters to be able to monitor the progress or proceed with script execution as moving the instance to another subnet is long running operation
Set-AzSqlInstance -Name $sqlMIName -ResourceGroupName $sqlMIResourceGroupName `
    -SubnetId "/subscriptions/$currentSubscriptionID/resourceGroups/$sqlMIResourceGroupName/providers/Microsoft.Network/virtualNetworks/$sqlMIResourceVnetName/subnets/$destinationSubnetName" -AsJob -Force

$operationProgress = Get-AzSqlInstanceOperation -ManagedInstanceName $sqlMIName -ResourceGroupName $sqlMIResourceGroupName
#checking the operation step status
Write-Host "Checking the ongoing step" -ForegroundColor Yellow
$operationProgress.OperationSteps.StepsList
```

# [Azure CLI](#tab/azure-cli)

Use the Azure CLI [az sql mi update](/cli/azure/sql/mi#az-sql-mi-update) command to move your instance to another subnet.

Provide the destination by either specifying the subnet ID as the `--subnet` property, or by specifying the virtual network name as the `--vnet-name` property, and subnet name as the `--subnet` property.

The following example moves the SQL managed instance to another subnet by specifying the subnet ID:

```azurecli-interactive
az sql mi update -g myResourceGroup -n mySqlManagedInstance --subnet /subscriptions/xxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/myResourceGroup/providers/Microsoft.Network/virtualNetworks/myVirtualNetworkName/subnets/destinationSubnetName
```

The following example moves the SQL managed instance to another subnet by specifying the virtual network name and subnet name:

```azurecli-interactive
az sql mi update -g myResourceGroup -n mySqlManagedInstance --vnet-name myVirtualNetworkName --subnet destinationSubnetName
```

Use the following command to monitor the progress of the management operation:

```azurecli-interactive
az sql mi op list -g myResourceGroup --mi mySqlManagedInstance
```

---

## Related content

- [Quickstart: Create Azure SQL Managed Instance](instance-create-quickstart.md)
- [Features comparison: Azure SQL Database and Azure SQL Managed Instance](../database/features-comparison.md)
- [Connectivity architecture for Azure SQL Managed Instance](connectivity-architecture-overview.md)
- [SQL Managed Instance migration using Database Migration Service](/azure/dms/tutorial-sql-server-to-managed-instance)
- [Modifiable configuration reference for Azure SQL Managed Instance](modifiable-configuration-reference.md)
