---
title: "Quickstart: Create managed instance with Terraform"
description: Learn how to create and deploy SQL Managed Instance with Terraform
author: urosran
ms.author: urandjelovic
ms.reviewer: mathoma
ms.date: 12/06/2022
ms.service: sql-managed-instance
ms.topic: quickstart
ms.custom: devx-track-terraform
---

# Quickstart: Create instance with Terraform - Azure SQL Managed Instance


Article tested with the following Terraform and Terraform provider versions:

- [Terraform v1.3.5](https://releases.hashicorp.com/terraform/)
- [AzureRM Provider v.3.0.0](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs)


This article shows how to deploy an Azure SQL Managed Instance in a virtual network (vNet) and a subnet associated with a route table and a network security group by using Terraform.

[!INCLUDE [Terraform abstract](~/../azure-dev-docs-pr/articles/terraform/includes/abstract.md)]

In this article, you learn how to:

> [!div class="checklist"]
* Create all supporting services for SQL Managed Instance to run on
* Deploy SQL Managed Instance


> [!NOTE]
> The example code in this article is located in the [Azure Terraform GitHub repo](https://github.com/Azure/terraform/tree/master/quickstart/101-managed-instance). See more [articles and sample code showing how to use Terraform to manage Azure resources](/azure/terraform)

## Prerequisites

[!INCLUDE [open-source-devops-prereqs-azure-subscription.md](~/../azure-dev-docs-pr/articles/includes/open-source-devops-prereqs-azure-subscription.md)]

- [Install and configure Terraform](/azure/developer/terraform/quickstart-configure)

## Implement the Terraform code

1. Create a directory in which to test and run the sample Terraform code and make it the current directory.

2. Create a file named `providers.tf` and insert the following code:
   [!code-terraform[master](~/../terraform_scripts/quickstart/101-managed-instance/providers.tf)]

3. Create a file named `main.tf` and insert the following code:
   [!code-terraform[master](~/../terraform_scripts/quickstart/101-managed-instance/main.tf)]

4. Create a file named `variables.tf` and insert the following code:
   [!code-terraform[master](~/../terraform_scripts/quickstart/101-managed-instance/variables.tf)]
   

## Initialize Terraform

[!INCLUDE [terraform-init.md](~/../azure-dev-docs-pr/articles/terraform/includes/terraform-init.md)]

## Create a Terraform execution plan

[!INCLUDE [terraform-plan.md](~/../azure-dev-docs-pr/articles/terraform/includes/terraform-plan.md)]

## Apply a Terraform execution plan

[!INCLUDE [terraform-apply-plan.md](~/../azure-dev-docs-pr/articles/terraform/includes/terraform-apply-plan.md)]

## Verify the results

To verify the results within the Azure portal, browse to the new resource group. The new instance will be in the new resource group after it has been deployed. To see the deployment progress keep your PowerShell open or navigate to the Azure portal, search for SQL Managed Instance and then
filter all instances by status). 

## Clean up resources

[!INCLUDE [terraform-plan-destroy.md](~/../azure-dev-docs-pr/articles/terraform/includes/terraform-plan-destroy.md)]

## Troubleshoot Terraform on Azure

[Troubleshoot common problems when using Terraform on Azure](/azure/developer/terraform/troubleshoot)

## Next steps

> [!div class="nextstepaction"]
> [Learn more about Azure SQL Managed Instance](index.yml)
