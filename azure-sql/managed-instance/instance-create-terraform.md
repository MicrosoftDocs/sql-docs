---
title: "Quickstart: Create Instance with Terraform"
description: Learn how to create and deploy Azure SQL Managed Instance with Terraform
author: vladai78
ms.author: vladiv
ms.reviewer: mathoma
ms.date: 08/26/2025
ms.service: azure-sql-managed-instance
ms.topic: quickstart
ms.custom:
  - devx-track-terraform
---

# Quickstart: Create instance with Terraform - Azure SQL Managed Instance

Article tested with the following Terraform and Terraform provider versions:

- [Terraform v1.3.5](https://releases.hashicorp.com/terraform/)
- [AzureRM Provider v.3.0.0](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs)

This article shows how use Terraform to deploy an Azure SQL Managed Instance in a virtual network and a subnet associated with a route table and a network security group.

[!INCLUDE [Terraform abstract](~/../azure-dev-docs-pr/articles/terraform/includes/abstract.md)]

In this article, you learn how to:

> [!div class="checklist"]
* Create all supporting services for SQL Managed Instance to run on
* Deploy SQL Managed Instance

> [!NOTE]  
> The example code in this article is located in the [Azure Terraform GitHub repo](https://github.com/Azure/terraform/tree/master/quickstart/101-managed-instance). See more [articles and sample code showing how to use Terraform to manage Azure resources](/azure/terraform)

## Prerequisites

[!INCLUDE [open-source-devops-prereqs-azure-subscription.md](~/../azure-dev-docs-pr/articles/includes/open-source-devops-prereqs-azure-subscription.md)]

- In the general case, your user needs to have the role [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor) assigned at subscription scope.
- If provisioning in a subnet that is already delegated to Azure SQL Managed Instance, your user only needs the **Microsoft.Sql/managedInstances/write** permission assigned at subscription scope.
- [Install and configure Terraform](/azure/developer/terraform/quickstart-configure)

[!INCLUDE [modifiable-config-note](../includes/sql-managed-instance/modifiable-config-note.md)]

## Implement the Terraform code

1. Create a directory in which to test and run the sample Terraform code and make it the current directory.

1. Create a file named `providers.tf` and insert the following code:
   [!code-terraform[master](~/../terraform_scripts/quickstart/101-managed-instance/providers.tf)]

1. Create a file named `main.tf` and insert the following code:
   [!code-terraform[master](~/../terraform_scripts/quickstart/101-managed-instance/main.tf)]

1. Create a file named `variables.tf` and insert the following code:
   [!code-terraform[master](~/../terraform_scripts/quickstart/101-managed-instance/variables.tf)]

## Initialize Terraform

[!INCLUDE [terraform-init.md](~/../azure-dev-docs-pr/articles/terraform/includes/terraform-init.md)]

## Create a Terraform execution plan

[!INCLUDE [terraform-plan.md](~/../azure-dev-docs-pr/articles/terraform/includes/terraform-plan.md)]

## Apply a Terraform execution plan

[!INCLUDE [terraform-apply-plan.md](~/../azure-dev-docs-pr/articles/terraform/includes/terraform-apply-plan.md)]

## Verify the results

To verify the results within the Azure portal, browse to the new resource group. The new instance will be in the new resource group after it's deployed. To see the deployment progress keep your PowerShell open or navigate to the Azure portal, search for SQL Managed Instance and then filter all instances by status.

## Clean up resources

[!INCLUDE [terraform-plan-destroy.md](~/../azure-dev-docs-pr/articles/terraform/includes/terraform-plan-destroy.md)]

## Troubleshoot Terraform on Azure

[Troubleshoot common problems when using Terraform on Azure](/azure/developer/terraform/troubleshoot)

## Next step

> [!div class="nextstepaction"]
> [Learn more about Azure SQL Managed Instance](index.yml)
