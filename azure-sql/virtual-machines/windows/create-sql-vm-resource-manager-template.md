---
title: Create SQL Server VM using an ARM template
description: Learn how to create a SQL Server on Azure Virtual Machine (VM) by using an Azure Resource Manager template (ARM template).
author: dplessMSFT
ms.author: dpless
ms.reviewer: vanto
ms.date: 06/29/2020
ms.service: azure-vm-sql-server
ms.subservice: deployment
ms.topic: quickstart
ms.custom: subject-armqs, mode-arm, devx-track-arm-template
---

# Quickstart: Create SQL Server VM using an ARM template

Use this Azure Resource Manager template (ARM template) to deploy a SQL Server on Azure Virtual Machine (VM). 

[!INCLUDE [About Azure Resource Manager](../../includes/resource-manager-quickstart-introduction.md)]

If your environment meets the prerequisites and you're familiar with using ARM templates, select the **Deploy to Azure** button. The template will open in the Azure portal.

[![Deploy to Azure](../../media/template-deployments/deploy-to-azure.svg)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FAzure%2Fazure-quickstart-templates%2Fmaster%2Fquickstarts%2Fmicrosoft.sqlvirtualmachine%2Fsql-vm-new-storage%2Fazuredeploy.json)

## Prerequisites

The SQL Server VM ARM template requires the following:

- The latest version of the [Azure CLI](/cli/azure/install-azure-cli) and/or [PowerShell](/powershell/scripting/install/installing-powershell). 
- A preconfigured [resource group](/azure/azure-resource-manager/management/manage-resource-groups-portal#create-resource-groups) with a prepared [virtual network](/azure/virtual-network/quick-create-portal) and [subnet](/azure/virtual-network/virtual-network-manage-subnet#add-a-subnet).
- An Azure subscription. If you don't have one, create a [free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn) before you begin.


## Review the template

The template used in this quickstart is from [Azure Quickstart Templates](https://azure.microsoft.com/resources/templates/sql-vm-new-storage/).

:::code language="json" source="~/../quickstart-templates/quickstarts/microsoft.sqlvirtualmachine/sql-vm-new-storage/azuredeploy.json":::

Five Azure resources are defined in the template: 

- [Microsoft.Network/publicIpAddresses](/azure/templates/microsoft.network/publicipaddresses): Creates a public IP address. 
- [Microsoft.Network/networkSecurityGroups](/azure/templates/microsoft.network/networksecuritygroups): Creates a network security group. 
- [Microsoft.Network/networkInterfaces](/azure/templates/microsoft.network/networkinterfaces): Configures the network interface. 
- [Microsoft.Compute/virtualMachines](/azure/templates/microsoft.compute/virtualmachines): Creates a virtual machine in Azure. 
- [Microsoft.SqlVirtualMachine/sqlVirtualMachines](/azure/templates/microsoft.sqlvirtualmachine/sqlvirtualmachines): Registers the virtual machine with the SQL IaaS Agent extension. 

More SQL Server on Azure VM templates can be found in the [quickstart template gallery](https://azure.microsoft.com/resources/templates/?resourceType=Microsoft.Sqlvirtualmachine&pageNumber=1&sort=Popular).


## Deploy the template

1. Select the following image to sign in to Azure and open a template. The template creates a virtual machine with the intended SQL Server version installed on it and registers the VM with the SQL IaaS Agent extension.

   [![Deploy to Azure](../../media/template-deployments/deploy-to-azure.svg)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FAzure%2Fazure-quickstart-templates%2Fmaster%2Fquickstarts%2Fmicrosoft.sqlvirtualmachine%2Fsql-vm-new-storage%2Fazuredeploy.json)

2. Complete the deployment form fields in order:

   - **Subscription** – Select an Azure subscription.  
   - **Resource group** – Select the prepared resource group for the SQL Server VM.  
   - **Region** – Select a region (for example, Central US).  
   - **Virtual machine name** – Enter a name for the SQL Server VM.  
   - **Virtual machine size** – Choose an appropriate VM size.  
   - **Virtual network (VNet)** – Specify the existing virtual network and subnet.  
   - **SQL Server configuration** – Select the image offer, SQL SKU, and storage workload settings.  
   - **Administrator credentials** – Enter the admin username and password.  

3. Review the deployment parameters:

   | Parameter | Type / Default | Allowed values / Constraints | Description |
   |-----------|----------------|------------------------------|-------------|
   | Subscription | Required | Existing Azure subscription | Azure subscription used for deployment. |
   | Resource group | Required | Existing resource group | Prepared resource group for the SQL Server VM. |
   | Region | Default: resourceGroup().location | Azure regions | Azure region for the VM (for example, Central US). |
   | virtualMachineName | String | 1–15 characters | Name of the SQL Server virtual machine. |
   | virtualMachineSize | String | Azure VM sizes | Size of the virtual machine. |
   | existingVirtualNetworkName | String | Existing virtual network (VNet) | Name of the prepared virtual network (VNet). |
   | existingVnetResourceGroup | String | Existing resource group | Resource group containing the virtual network. |
   | existingSubnetName | String | Existing subnet | Name of the prepared subnet. |
   | imageOffer | String | SQL Server and Windows Server images | SQL Server and Windows Server image offer. |
   | sqlSku | String | Developer, Express, Standard, Enterprise | SQL Server edition SKU. |
   | adminUsername | String | — | Administrator username for the VM. |
   | adminPassword | Secure string | Complexity enforced | Administrator password for the VM. |
   | storageWorkloadType | String | OLTP, DW, General | Storage workload type (OLTP = Online Transaction Processing, DW = Data Warehouse). |
   | sqlDataDisksCount | Int | Min/Max per SKU | Number of disks used for SQL Server data files. |
   | dataPath | String | Valid path | Path for SQL Server data files. |
   | sqlLogDisksCount | Int | Min/Max per SKU | Number of disks used for SQL Server log files. |
   | logPath | String | Valid path | Path for SQL Server log files. |
   | location | Default: resourceGroup().location | Azure regions | Location for all resources. |

4. Select **Review + create**, then select **Create**.

5. Verify deployment success:
   - **Portal**: Confirm the deployment status shows **Succeeded**.
   - **Azure CLI**:
     ```azurecli
     az resource show --resource-group <resource-group> --name <vm-name> --resource-type Microsoft.Compute/virtualMachines
     ```

The Azure portal is used to deploy the template. In addition to the Azure portal, you can also use Azure PowerShell, the Azure CLI, and the REST API. To learn about other deployment methods, see [Deploy templates](/azure/azure-resource-manager/templates/deploy-powershell).

## Review deployed resources

You can use the Azure CLI to check deployed resources. 

```azurecli-interactive
echo "Enter the resource group where your SQL Server VM exists:" &&
read resourcegroupName &&
az resource list --resource-group $resourcegroupName 
```

## Clean up resources

When no longer needed, delete the resource group by using Azure CLI or Azure PowerShell:

# [CLI](#tab/CLI)

```azurecli-interactive
echo "Enter the Resource Group name:" &&
read resourceGroupName &&
az group delete --name $resourceGroupName &&
echo "Press [ENTER] to continue ..."
```

# [PowerShell](#tab/PowerShell)

```azurepowershell-interactive
$resourceGroupName = Read-Host -Prompt "Enter the Resource Group name"
Remove-AzResourceGroup -Name $resourceGroupName
Write-Host "Press [ENTER] to continue..."
```

---

## Next steps

For a step-by-step tutorial that guides you through the process of creating a template, see:

> [!div class="nextstepaction"]
> [Tutorial: Create and deploy your first ARM template](/azure/azure-resource-manager/templates/template-tutorial-create-first-template)

For other ways to deploy a SQL Server VM, see: 
- [Azure portal](create-sql-vm-portal.md)
- [PowerShell](create-sql-vm-powershell.md)

To learn more, see [an overview of SQL Server on Azure VMs](sql-server-on-azure-vm-iaas-what-is-overview.md).
