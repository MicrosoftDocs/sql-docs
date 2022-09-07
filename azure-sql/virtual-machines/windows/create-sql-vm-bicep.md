---
title: Create SQL Server VM using Bicep
description: Learn how to create a SQL Server on Azure Virtual Machine (VM) using Bicep.
author: adbadram
ms.author: adbadram
ms.date: 06/17/2022
ms.service: virtual-machines-sql
ms.subservice: deployment
ms.topic: quickstart
ms.custom:
  - subject-armqs
  - devx-track-azurepowershell
  - mode-arm
---

# Quickstart: Create SQL Server VM using Bicep

This quickstart shows you how to use Bicep to create an SQL Server on Azure Virtual Machine (VM).

[Bicep](/azure/azure-resource-manager/bicep/overview) is a domain-specific language (DSL) that uses declarative syntax to deploy Azure resources. It provides concise syntax, reliable type safety, and support for code reuse. Bicep offers the best authoring experience for your infrastructure-as-code solutions in Azure. 

## Prerequisites

The SQL Server VM Bicep file requires the following:

- The latest version of the [Azure CLI](/cli/azure/install-azure-cli) and/or [PowerShell](/powershell/scripting/install/installing-powershell).
- A pre-configured [resource group](/azure/azure-resource-manager/management/manage-resource-groups-portal#create-resource-groups) with a prepared [virtual network](/azure/virtual-network/quick-create-portal) and [subnet](/azure/virtual-network/virtual-network-manage-subnet#add-a-subnet).
- An Azure subscription. If you don't have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

## Review the Bicep file

The Bicep file used in this quickstart is from [Azure Quickstart Templates](https://azure.microsoft.com/resources/templates/sql-vm-new-storage/).

:::code language="bicep" source="~/../quickstart-templates/quickstarts/microsoft.sqlvirtualmachine/sql-vm-new-storage/main.bicep":::

Five Azure resources are defined in the Bicep file:

- [Microsoft.Network/publicIpAddresses](/azure/templates/microsoft.network/publicipaddresses): Creates a public IP address.
- [Microsoft.Network/networkSecurityGroups](/azure/templates/microsoft.network/networksecuritygroups): Creates a network security group.
- [Microsoft.Network/networkInterfaces](/azure/templates/microsoft.network/networkinterfaces): Configures the network interface.
- [Microsoft.Compute/virtualMachines](/azure/templates/microsoft.compute/virtualmachines): Creates a virtual machine in Azure.
- [Microsoft.SqlVirtualMachine/SqlVirtualMachines](/azure/templates/microsoft.sqlvirtualmachine/sqlvirtualmachines): registers the virtual machine with the SQL IaaS Agent extension.

## Deploy the Bicep file

1. Save the Bicep file as **main.bicep** to your local computer.
1. Deploy the Bicep file using either Azure CLI or Azure PowerShell.

    # [CLI](#tab/CLI)

    ```azurecli
    az deployment group create --resource-group exampleRG --template-file main.bicep --parameters existingSubnetName=<subnet-name> adminUsername=<admin-user> adminPassword=<admin-pass>
    ```

    # [PowerShell](#tab/PowerShell)

    ```azurepowershell
    New-AzResourceGroupDeployment -ResourceGroupName exampleRG -TemplateFile ./main.bicep -administratorLogin "<admin-login>" -adminUsername "<admin-user>" -adminPassword "<admin-pass>"
    ```

    ---

Make sure to replace the resource group name, *exampleRG*, with the name of your pre-configured resource group.

You're required to enter the following parameters:

- **existingSubnetName**: Replace **\<subnet-name\>** with the name of the subnet.
- **adminUsername**: Replace **\<admin-user\>** with the admin username of the VM.

You'll also be prompted to enter **adminPassword**.

> [!NOTE]
> When the deployment finishes, you should see a message indicating the deployment succeeded.

## Review deployed resources

Use the Azure portal, Azure CLI, or Azure PowerShell to list the deployed resources in the resource group.

# [CLI](#tab/CLI)

```azurecli-interactive
az resource list --resource-group exampleRG
```

# [PowerShell](#tab/PowerShell)

```azurepowershell-interactive
Get-AzResource -ResourceGroupName exampleRG
```

---

## Clean up resources

When no longer needed, use the Azure portal, Azure CLI, or Azure PowerShell to delete the resource group and its resources.

# [CLI](#tab/CLI)

```azurecli-interactive
az group delete --name exampleRG
```

# [PowerShell](#tab/PowerShell)

```azurepowershell-interactive
Remove-AzResourceGroup -Name exampleRG
```

---

## Next steps

For a step-by-step tutorial that guides you through the process of creating a Bicep file with Visual Studio Code, see:

> [!div class="nextstepaction"]
> [Quickstart: Create Bicep files with Visual Studio Code](/azure/azure-resource-manager/bicep/quickstart-create-bicep-use-visual-studio-code)

For other ways to deploy a SQL Server VM, see:

- [Azure portal](create-sql-vm-portal.md)
- [PowerShell](create-sql-vm-powershell.md)

To learn more, see [an overview of SQL Server on Azure VMs](sql-server-on-azure-vm-iaas-what-is-overview.md).
