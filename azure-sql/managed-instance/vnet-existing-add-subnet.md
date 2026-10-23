---
title: Configure an Existing Virtual Network
titleSuffix: Azure SQL Managed Instance
description: This article describes how to configure an existing virtual network and subnet where you can deploy Azure SQL Managed Instance.
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: bonova
ms.date: 08/26/2025
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: how-to
ms.update-cycle: 1825-days
ms.custom:
  - sqldbrb=1
---
# Configure an existing virtual network for Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

Azure SQL Managed Instance must be deployed within an Azure [virtual network](/azure/virtual-network/virtual-networks-overview) and the subnet dedicated for managed instances only. You can use the existing virtual network and subnet if they're configured according to the [SQL Managed Instance virtual network requirements](connectivity-architecture-overview.md#network-requirements).

If one of the following cases applies to you, you can validate and modify your network by using the script explained in this article:

- You have a new subnet that's still not configured.
- You're not sure that the subnet is aligned with the [requirements](connectivity-architecture-overview.md#network-requirements).
- You want to check that the subnet still complies with the [network requirements](connectivity-architecture-overview.md#network-requirements) after you made changes.

> [!NOTE]  
> Calculate subnet size by following the guidelines in the [Determine the size of subnet for SQL Managed Instance](vnet-subnet-determine-size.md) article. You can't resize the subnet after you deploy the resources inside.
>
> After the SQL managed instance is created, you can [Move the instance to another subnet inside the same vNet or across vNets](vnet-subnet-move-instance.md), but moving the instance or VNet to another resource group or subscription isn't supported.

## Validate and modify an existing virtual network

If you want to create a SQL managed instance inside an existing subnet, we recommend the following PowerShell script to prepare the subnet:

```powershell
$scriptUrlBase = 'https://raw.githubusercontent.com/Microsoft/sql-server-samples/master/samples/manage/azure-sql-db-managed-instance/delegate-subnet'

$parameters = @{
    subscriptionId = '<subscriptionId>'
    resourceGroupName = '<resourceGroupName>'
    virtualNetworkName = '<virtualNetworkName>'
    subnetName = '<subnetName>'
    }

Invoke-Command -ScriptBlock ([Scriptblock]::Create((iwr ($scriptUrlBase+'/delegateSubnet.ps1?t='+ [DateTime]::Now.Ticks)).Content)) -ArgumentList $parameters
```

The script prepares the subnet in three steps:

1. **Validate**: It validates the selected virtual network and subnet for SQL Managed Instance networking requirements.
1. **Confirm**: It shows the user a set of changes that need to be made to prepare the subnet for SQL Managed Instance deployment. It also asks for consent.
1. **Prepare**: It properly configures the virtual network and subnet.

## Related content

- [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md)
- [Quickstart: Create Azure SQL Managed Instance](instance-create-quickstart.md)
- [Resolve private domain names in Azure SQL Managed Instance](resolve-private-domain-names.md)
