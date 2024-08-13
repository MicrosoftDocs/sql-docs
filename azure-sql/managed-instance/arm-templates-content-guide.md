---
title: Azure Resource Manager templates
description: Use Azure Resource Manager templates to create and configure Azure SQL Managed Instance.
author: urosmil
ms.author: urmilano
ms.reviewer: wiassaf, mathoma
ms.date: 04/30/2024
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: conceptual
ms.custom: overview-samples, azure-sql-split, devx-track-arm-template
monikerRange: "= azuresql || = azuresql-mi"
---

# Azure Resource Manager templates for Azure SQL Managed Instance
[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/arm-templates-content-guide.md?view=azuresql&preserve-view=true)
> * [Azure SQL Managed Instance](arm-templates-content-guide.md?view=azuresql&preserve-view=true)

Azure Resource Manager templates enable you to define your infrastructure as code and deploy your solutions to the Azure cloud for Azure SQL Managed Instance.


## Templates

The following table includes links to Azure Resource Manager templates for Azure SQL Managed Instance.

|Link|Description|
|---|---|
| [SQL Managed Instance in a new virtual network](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sql/sqlmi-new-vnet) | This Azure Resource Manager template creates and configures a new Azure virtual network and creates a new managed instance in the virtual network. |
| [Network environment for SQL Managed Instance](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sql/sql-managed-instance-azure-environment) | This deployment creates and configures an Azure virtual network with two subnets, one that is dedicated to your managed instances and another where you can place other resources (such as VMs, App Service environments). This template creates a properly configured networking environment where you can deploy managed instances. |
| [SQL Managed Instance with P2S connection](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sql/sqlmi-new-vnet-w-point-to-site-vpn) | This deployment creates an Azure virtual network with two subnets, `ManagedInstance` and `GatewaySubnet`. SQL Managed Instance is deployed in the `ManagedInstance` subnet. A virtual network gateway is created in the `GatewaySubnet` subnet and configured for a Point-to-Site VPN connection. |
| [SQL Managed Instance with a virtual machine](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sql/sqlmi-new-vnet-w-jumpbox) | This deployment creates an Azure virtual network with two subnets, `ManagedInstance` and `Management`. SQL Managed Instance is deployed in the `ManagedInstance` subnet. A virtual machine with the latest version of SQL Server Management Studio (SSMS) is deployed in the `Management` subnet. |
| [SQL Managed Instance with diagnostic logs enabled](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sql/sqlmi-new-vnet-w-diagnostic-settings) | This deployment creates an Azure Virtual Network with a `ManagedInstance` subnet and deploys a SQL Managed Instance with diagnostic logs enabled. It also deploys event hub, diagnostic workspace, and the storage account for the purpose of sending and storing instance diagnostic logs. |


## Related content

- [ARM templates](/azure/azure-resource-manager/templates/overview)