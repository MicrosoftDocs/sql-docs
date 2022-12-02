---
title: 'Quickstart: Create and deploy SQL Managed Instance with Azure & Terraform'
description: Learn how to create and deploy SQL Managed Instance with Azure & Terraform
keywords: azure devops terraform sqlmi sql database
ms.topic: quickstart
ms.date: 11/22/2022
ms.custom: devx-track-terraform
author: urosran
ms.author: urandjelovic
---

# Quickstart: Create and deploy SQL Managed Instance with Azure & Terraform


Article tested with the following Terraform and Terraform provider versions:

- [Terraform v1.3.5](https://releases.hashicorp.com/terraform/)
- [AzureRM Provider v.3.0.0](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs)


This article shows how to deploy an Azure SQL Managed Instance 
in a virtual network (vNet) and a subnet associated with a route table and a network security group
using Terraform.

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

<!--
In the CRR (cross-repo reference) statements below, "terraform_samples" refers to the 
code repository. terraform_samples is defined in the repo's .openpublishing.publish.config.json
file. This definition exists in the azure-dev-docs-pr and azure-docs-pr repos. If you're
working in a different repo and need help creating this, reach out to the Terraform documentation 
team to have this created for you. In each CRR statement, the value after "terraform_samples"
is the directory on the code repo in which the file you want to copy exists. The examples
should help clarify how you need to enter these statements, but if you need assistance,
 the Terraform documentation team can help.

All Terraform procedural articles (quickstart, tutorial) should have a minimum of 4 files:

* providers.tf - declares the provider block
* main.tf -  declares resource group and your other code resources
* variables.tf - defines resource group name prefix, location, and any other vars you need
* outputs.tf - outputs randomly generated resource group name

-->

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

To verify the results within the Azure portal, browse to the new resource group. The new SQL MI instance will be in the new resource group after it has been deployed. (
to see the deployment progress keep your PowerShell open or navigate to Portal, search for SQL Managed Instance and once
opened filter by status)

## Clean up resources

[!INCLUDE [terraform-plan-destroy.md](~/../azure-dev-docs-pr/articles/terraform/includes/terraform-plan-destroy.md)]

## Troubleshoot Terraform on Azure


[Troubleshoot common problems when using Terraform on Azure](/azure/developer/terraform/troubleshoot)

## Next steps

[!div class="nextstepaction"]
[Learn more about <Azure service>"](https://aka.ms/learn-sqlmi)