---
title: "Create SQL Server VM with PowerShell script"
description: This article provides an end-to-end Azure PowerShell sample script to create SQL Server on Azure VMs.
author: bluefooted
ms.author: pamela
ms.reviewer: mathoma
ms.date: 05/29/2024
ms.service: virtual-machines-sql
ms.subservice: deployment
ms.topic: sample
ms.custom: devx-track-azurepowershell
ms.devlang: powershell
---
# Use PowerShell to create SQL Server on Azure VM

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqlmi.md)]

This PowerShell script example creates a Windows SQL Server virtual machine (VM) in Azure. 

[!INCLUDE [cloud-shell-try-it.md](../../includes/cloud-shell-try-it.md)]

If you choose to install and use PowerShell locally, this tutorial requires Azure PowerShell 1.4.0 or later. If you need to upgrade, see [Install Azure PowerShell module](/powershell/azure/install-az-ps). If you're running PowerShell locally, you also need to run `Connect-AzAccount` to create a connection with Azure.

## Set variables 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="SetVariables":::

## Sample script

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="FullScript":::

## Clean up deployment

Use the following command to remove the resource group and all resources associated with it.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="Cleanup":::

## Script explanation

The script in this article uses the following commands: 

| Command | Notes |
|---|---|
| [Get-AzVMImageOffer](/powershell/module/az.compute/get-azvmimageoffer) | Lists all Azure VM images. | 
| [Get-AzVMImageSku](/powershell/module/az.compute/get-azvmimagesku) | Lists the SKUs for a particular offer. | 
| [New-AzResourceGroup](/powershell/module/az.resources/new-azresourcegroup) | Creates a resource group in which all resources are stored. |
| [New-AzStorageAccount](/powershell/module/az.storage/new-azstorageaccount) | Creates a new Azure storage account. |
| [New-AzVirtualNetworkSubnetConfig](/powershell/module/az.network/new-azvirtualnetworksubnetconfig) | Creates and configures a new subnet. | 
| [New-AzVirtualNetwork](/powershell/module/az.network/new-azvirtualnetwork) | Creates a virtual network. |
| [New-AzPublicIpAddress](/powershell/module/az.network/new-azpublicipaddress) | Creates a public IP address. | 
| [New-AzNetworkSecurityGroup](/powershell/module/az.network/new-aznetworksecuritygroup) | Creates a new security group. | 
| [New-AzNetworkInterface](/powershell/module/az.network/new-aznetworkinterface) | Creates a network interface. | 
| [New-AzVMConfig](/powershell/module/az.compute/new-azvmconfig) | Creates a configurable virtual machine object. |  
| [Add-AzVMNetworkInterface](/powershell/module/az.compute/add-azvmnetworkinterface) | Adds a network interface to a virtual machine. | 
| [Register-AzResourceProvider](/powershell/module/az.resources/register-azresourceprovider) | Registers a resource provider. | 
| [New-AzSqlVM](/powershell/module/az.sqlvirtualmachine/new-azsqlvm) | Creates or updates a [SQL virtual machine](../windows/manage-sql-vm-portal.md) resource. | 
| [Stop-AzVM](/powershell/module/az.compute/stop-azvm) | Stops an Azure virtual machine. | 
| [Remove-AzResourceGroup](/powershell/module/az.resources/remove-azresourcegroup) | Removes a resource group in Azure. | 


## Related content

For more information on Azure PowerShell, see [Azure PowerShell documentation](/powershell/azure/).

