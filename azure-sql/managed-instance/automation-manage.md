---
title: Azure Automation and Azure SQL Managed Instance
description: Learn about how the Azure Automation service can be used to manage databases in Azure SQL Managed Instance at scale.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest, mathoma
ms.date: 01/16/2024
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: conceptual
monikerRange: "=azuresql||=azuresql-mi"
---

# Manage databases in Azure SQL Managed Instance by using Azure Automation

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/automation-manage.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](automation-manage.md?view=azuresql-mi&preserve-view=true)

This guide introduces you to the Azure Automation service, and how it can be used to simplify the management of databases in Azure SQL Managed Instance.

## About Azure Automation

[Azure Automation](https://azure.microsoft.com/services/automation/) is an Azure service for simplifying cloud management through process automation. Using Azure Automation, long-running, manual, error-prone, and frequently repeated tasks can be automated to increase reliability, efficiency, and time to value for your organization. For information on getting started, see [Azure Automation intro](/azure/automation/automation-intro).

Azure Automation provides a workflow execution engine with high reliability and high availability, and that scales to meet your needs as your organization grows. In Azure Automation, processes can be kicked off manually, by third-party systems, or at scheduled intervals so that tasks happen exactly when needed.

Lower operational overhead and free up IT / DevOps staff to focus on work that adds business value by moving your cloud management tasks to be run automatically by Azure Automation.

## How Azure Automation can help manage your SQL managed instances

With Azure Automation, you can manage databases in Azure SQL Managed Instance by using the [latest Az PowerShell cmdlets]/powershell/azure/install-azure-powershell) that are available in [Azure Az PowerShell](/powershell/azure/new-azureps-module-az). Azure Automation has these Azure Az PowerShell cmdlets immediately available, so that you can perform all of your management tasks within the service. You can also pair these cmdlets in Azure Automation with the cmdlets for other Azure services, to automate complex tasks across Azure services and across third-party systems.

Azure Automation also has the ability to communicate with SQL managed instances directly, by issuing SQL commands using PowerShell.

The runbook and module galleries for [Azure Automation](/azure/automation/automation-runbook-gallery) offer a variety of runbooks from Microsoft and the community that you can import into Azure Automation. To use one, download a runbook from the gallery, or you can directly import runbooks from the gallery, or from your Automation account in the Azure portal.

>[!NOTE]
> The Automation runbook might run from a range of IP addresses at any datacenter in an Azure region. To learn more, see [Automation region DNS records](/azure/automation/how-to/automation-region-dns-records).

## Authentication via managed identities

On 30 September 2023, Azure Automation Classic Run As accounts will be retired. Instead, use managed identities for authentication for existing and new runbooks. Managed identities provide the same functionality as Run As accounts, plus:

- Secure authentication to any Azure service that supports authentication with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)).
- Minimized management overhead with easy access to resources.
- Simplified runbooks with no requirement to use multi-line code.

Since April 2023, the creation of new Run As accounts in Azure Automation is no longer possible.

For more information on this required action, visit [Migrate from an existing Run As account to Managed Identities](/azure/automation/migrate-run-as-accounts-managed-identity?tabs=run-as-account).

## Other automation methods

Azure SQL Managed Instance has near complete compatibility with SQL Agent in the latest version of SQL Server. For more information, see [Automate management tasks using SQL Agent jobs in Azure SQL Managed Instance](job-automation-managed-instance.md).

## Related content
Now that you've learned the basics of Azure Automation and how it can be used to manage SQL managed instances, follow these links to learn more about Azure Automation.

- [Azure Automation Overview](/azure/automation/automation-intro)
- [My first runbook](/azure/automation/learn/powershell-runbook-managed-identity)
