---
title: 'Quickstart: Create an Azure SQL Database server and database using Terraform'
description: In this article, you create an Azure SQL Database server and database using Terraform.
ms.topic: quickstart
ms.service: azure-sql-database
ms.subservice: deployment-configuration
ms.custom: devx-track-terraform
author: TomArcherMsft
ms.author: tarcher
ms.reviewer: mathoma
ms.date: 5/03/2023
content_well_notification: 
  - AI-contribution
ai-usage: ai-assisted
---

# Quickstart: Create an Azure SQL Database server and database using Terraform
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Creating a [single database](single-database-overview.md) is the quickest and simplest option to create a database in Azure SQL Database. This quickstart shows you how to create a single database using Terraform.

[!INCLUDE [Terraform abstract](~/../azure-dev-docs-pr/articles/terraform/includes/abstract.md)]

In this article, you learn how to:

> [!div class="checklist"]
> * Create a random value for the Azure resource group name using [random_pet](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/pet).
> * Create an Azure resource group using [azurerm_resource_group](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/resource_group).
> * Create a random value for the [logical server in Azure](logical-servers.md) using [random_pet](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/pet).
> * Create a random password for the [logical server in Azure](logical-servers.md) using [random_password](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/password).
> * Create a [logical server in Azure](logical-servers.md) using [azurerm_mssql_server](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/mssql_server).
> * Create a database in Azure SQL Database using [azurerm_mssql_database](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/mssql_database).

## Prerequisites

- [Install and configure Terraform](/azure/developer/terraform/quickstart-configure)

## Implement the Terraform code

> [!NOTE]
> The sample code for this article is located in the [Azure Terraform GitHub repo](https://github.com/Azure/terraform/tree/master/quickstart/101-sql-database). You can view the log file containing the [test results from current and previous versions of Terraform](https://github.com/Azure/terraform/tree/master/quickstart//101-sql-database/TestRecord.md).
> 
> See more [articles and sample code showing how to use Terraform to manage Azure resources](/azure/terraform)

1. Create a directory in which to test and run the sample Terraform code and make it the current directory.

1. Create a file named `providers.tf` and insert the following code:

    [!code-terraform[master](~/../terraform_samples/quickstart/101-sql-database//providers.tf)]

1. Create a file named `main.tf` and insert the following code:

    [!code-terraform[master](~/../terraform_samples/quickstart/101-sql-database//main.tf)]

1. Create a file named `variables.tf` and insert the following code:

    [!code-terraform[master](~/../terraform_samples/quickstart/101-sql-database//variables.tf)]

1. Create a file named `outputs.tf` and insert the following code:

    [!code-terraform[master](~/../terraform_samples/quickstart/101-sql-database//outputs.tf)]

## Initialize Terraform

[!INCLUDE [terraform-init.md](~/../azure-dev-docs-pr/articles/terraform/includes/terraform-init.md)]

## Create a Terraform execution plan

[!INCLUDE [terraform-plan.md](~/../azure-dev-docs-pr/articles/terraform/includes/terraform-plan.md)]

## Apply a Terraform execution plan

[!INCLUDE [terraform-apply-plan.md](~/../azure-dev-docs-pr/articles/terraform/includes/terraform-apply-plan.md)]

## Verify the results

#### [Azure CLI](#tab/azure-cli)

1. Get the Azure resource group name.

    ```console
    resource_group_name=$(terraform output -raw resource_group_name)
    ```

1. Get the new logical server name.

    ```console
   sql_server_name=$(terraform output -raw sql_server_name)
    ```

1. Run [az sql db list](/cli/azure/sql/db#az-sql-db-list) to display the names of all the databases in your server.

    ```azurecli
    az sql db list \
    --resource-group $resource_group_name \
    --server $sql_server_name \
    --output table
    ```

#### [Azure PowerShell](#tab/azure-powershell)

1. Get the Azure resource group name.

    ```console
    $resource_group_name=$(terraform output -raw resource_group_name)
    ```

1. Get the new logical server name.

    ```console
    $sql_server_name=$(terraform output -raw sql_server_name)
    ```

1. Run [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) to display the names of all the databases in your server.

    ```azurepowershell
     Get-AzSqlDatabase -ResourceGroupName $resource_group_name `
                       -ServerName $sql_server_name `
                       | Format-Table
    ```

---

## Clean up resources

[!INCLUDE [terraform-plan-destroy.md](~/../azure-dev-docs-pr/articles/terraform/includes/terraform-plan-destroy.md)]

## Troubleshoot Terraform on Azure

[Troubleshoot common problems when using Terraform on Azure](/azure/developer/terraform/troubleshoot)

## Next steps

> [!div class="nextstepaction"]
> [Create a server-level firewall rule](firewall-create-server-level-portal-quickstart.md)
