---
title: Azure Private Link for Azure SQL Managed Instance
titleSuffix: Azure SQL Managed Instance
description: Connect your Azure SQL Managed Instance to virtual networks and Azure services via private endpoints.
author: ZoranRilakMSFT
ms.author: zoran.rilak
ms.reviewer: mathoma, srbozovi
ms.date: 11/30/2022
ms.service: sql-managed-instance
ms.subservice: backup-restore
ms.topic: how-to
---
# Azure Private Link for Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

[Private Link](https://learn.microsoft.com/azure/private-link/private-link-overview) is an Azure technology that makes supporting Azure SaaS and PaaS services accessible in a virtual network of your choice.

With Azure SQL Managed Instance, you deploy a [private endpoint](https://learn.microsoft.com/azure/private-link/private-endpoint-overview) in another virtual network, which can then be used by clients and services in that virtual network to communicate with your managed instance.

Private endpoints can also be used to make your Azure SQL Managed Instance available to SaaS and PaaS services, as long as they support the creation and management of private endpoints on your behalf.

##### What this article does

##### When to use private endpoints
Private endpoints allow you to implement several scenarios more easily and securely than by using [VNet-local endpoints]()
###### Managed private endpoints
##### When to use other endpoints

##### Create a private endpoint (Portal)
##### Approve an endpoint request (Portal)

##### Ditto for PowerShell/Azure CLI

## Next steps

Learn about []().
(use relative links to docs) https://learn.microsoft.com/en-us/azure/private-link/private-endpoint-overview
(use relative links to docs; also update this) https://learn.microsoft.com/en-us/azure/private-link/availability