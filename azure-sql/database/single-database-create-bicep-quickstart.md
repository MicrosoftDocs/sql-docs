---
title: "Bicep: Create a single database in Azure SQL Database"
description: Create a single database in Azure SQL Database using Bicep.
author: dimitri-furman
ms.author: dfurman
ms.date: 05/16/2022
ms.service: sql-database
ms.subservice: deployment-configuration
ms.topic: quickstart
ms.custom:
  - "subject-armqs sqldbrb=1"
  - "devx-track-azurepowershell"
  - "mode-arm"
---

# Quickstart: Create a single database in Azure SQL Database using Bicep

Creating a [single database](single-database-overview.md) is the quickest and simplest option for creating a database in Azure SQL Database. This quickstart shows you how to create a single database using Bicep.

[Bicep](/azure/azure-resource-manager/bicep/overview?tabs=bicep) is a domain-specific language (DSL) that uses declarative syntax to deploy Azure resources. It provides concise syntax, reliable type safety, and support for code reuse. Bicep offers the best authoring experience for your infrastructure-as-code solutions in Azure.

## Prerequisites

If you don't have an Azure subscription, [create a free account](https://azure.microsoft.com/free/).

## Review the Bicep file

A single database has a defined set of compute, memory, IO, and storage resources using one of two [purchasing models](purchasing-models.md). When you create a single database, you also define a [server](logical-servers.md) to manage it and place it within [Azure resource group](/azure/active-directory-b2c/overview) in a specified region.

The Bicep file used in this quickstart is from [Azure Quickstart Templates](https://azure.microsoft.com/resources/templates/sql-database/).

:::code language="bicep" source="~/../quickstart-templates/quickstarts/microsoft.sql/sql-database/main.bicep":::

The following resources are defined in the Bicep file:

- [**Microsoft.Sql/servers**](/azure/templates/microsoft.sql/servers)
- [**Microsoft.Sql/servers/databases**](/azure/templates/microsoft.sql/servers/databases)

## Deploy the Bicep file

1. Save the Bicep file as **main.bicep** to your local computer.
1. Deploy the Bicep file using either Azure CLI or Azure PowerShell.

    # [CLI](#tab/CLI)

    ```azurecli
    az group create --name exampleRG --location eastus
    az deployment group create --resource-group exampleRG --template-file main.bicep --parameters administratorLogin=<admin-login>
    ```

    # [PowerShell](#tab/PowerShell)

    ```azurepowershell
    New-AzResourceGroup -Name exampleRG -Location eastus
    New-AzResourceGroupDeployment -ResourceGroupName exampleRG -TemplateFile ./main.bicep -administratorLogin "<admin-login>"
    ```

    ---

> [!NOTE]
> Replace **\<admin-login\>** with the administrator username of the SQL logical server. You'll be prompted to enter **administratorLoginPassword**.

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

- Create a server-level firewall rule to connect to the single database from on-premises or remote tools. For more information, see [Create a server-level firewall rule](firewall-create-server-level-portal-quickstart.md).
- After you create a server-level firewall rule, [connect and query](connect-query-content-reference-guide.md) your database using several different tools and languages.
  - [Connect and query using SQL Server Management Studio](connect-query-ssms.md)
  - [Connect and query using Azure Data Studio](/sql/azure-data-studio/quickstart-sql-database?toc=%2fazure%2fsql-database%2ftoc.json)
- To create a single database using the Azure CLI, see [Azure CLI samples](az-cli-script-samples-content-guide.md).
- To create a single database using Azure PowerShell, see [Azure PowerShell samples](powershell-script-content-guide.md).
- To learn how to create Bicep files, see [Create Bicep files with Visual Studio Code](/azure/azure-resource-manager/bicep/quickstart-create-bicep-use-visual-studio-code).
