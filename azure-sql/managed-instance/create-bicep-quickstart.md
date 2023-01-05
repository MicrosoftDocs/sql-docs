---
title: "Bicep: Create an Azure SQL Managed Instance using Bicep"
description: Learn how to create an Azure SQL Managed Instance using Bicep.
author: niko-neugebauer
ms.author: nneugebauer
ms.date: 05/16/2022
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: quickstart
ms.custom:
  - subject-armqs
  - devx-track-azurepowershell
  - mode-arm
---

# Quickstart: Create an Azure SQL Managed Instance using Bicep

This quickstart focuses on the process of deploying a Bicep file to create an Azure SQL Managed Instance and vNet. [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) is an intelligent, fully managed, scalable cloud database, with almost 100% feature parity with the SQL Server database engine.

[Bicep](/azure/azure-resource-manager/bicep/overview?tabs=bicep) is a domain-specific language (DSL) that uses declarative syntax to deploy Azure resources. It provides concise syntax, reliable type safety, and support for code reuse. Bicep offers the best authoring experience for your infrastructure-as-code solutions in Azure.

## Prerequisites

If you don't have an Azure subscription, [create a free account](https://azure.microsoft.com/free/).

## Review the Bicep file

The Bicep file used in this quickstart is from [Azure Quickstart Templates](https://azure.microsoft.com/resources/templates/sqlmi-new-vnet/).

:::code language="bicep" source="~/../quickstart-templates/quickstarts/microsoft.sql/sqlmi-new-vnet/main.bicep":::

These resources are defined in the Bicep file:

- [**Microsoft.Network/networkSecurityGroups**](/azure/templates/microsoft.Network/networkSecurityGroups)
- [**Microsoft.Network/routeTables**](/azure/templates/microsoft.Network/routeTables)
- [**Microsoft.Network/virtualNetworks**](/azure/templates/microsoft.Network/virtualNetworks)
- [**Microsoft.Sql/managedinstances**](/azure/templates/microsoft.sql/managedinstances)

## Deploy the Bicep file

1. Save the Bicep file as **main.bicep** to your local computer.
1. Deploy the Bicep file using either Azure CLI or Azure PowerShell.

    # [CLI](#tab/CLI)

    ```azurecli
    az group create --name exampleRG --location eastus
    az deployment group create --resource-group exampleRG --template-file main.bicep --parameters managedInstanceName=<instance-name> administratorLogin=<admin-login>
    ```

    # [PowerShell](#tab/PowerShell)

    ```azurepowershell
    New-AzResourceGroup -Name exampleRG -Location eastus
    New-AzResourceGroupDeployment -ResourceGroupName exampleRG -TemplateFile ./main.bicep -managedInstanceName "<instance-name>" -administratorLogin "<admin-login>"
    ```

    ---

> [!NOTE]
> Replace **\<instance-name\>** with the name of the managed instance. Replace **\<admin-login\>** with the administrator username. You'll be prompted to enter **administratorLoginPassword**.

  When the deployment finishes, you should see a message indicating the deployment succeeded.

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

> [!div class="nextstepaction"]
> [Configure an Azure VM to connect to Azure SQL Managed Instance](connect-vm-instance-configure.md)
