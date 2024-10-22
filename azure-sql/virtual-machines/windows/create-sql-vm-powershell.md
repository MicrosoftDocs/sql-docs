---
title: Create SQL Server VM with PowerShell (Guide)
description: Provides detailed steps and PowerShell commands to create a Windows SQL Server on Azure Virtual Machine. 
author: bluefooted
ms.author: pamela
ms.reviewer: mathoma
ms.date: 05/29/2024
ms.service: azure-vm-sql-server
ms.subservice: deployment
ms.topic: how-to
ms.custom: devx-track-azurepowershell
tags: azure-resource-manager
---
# Guide to creating SQL Server VM with PowerShell

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This guide covers options for using PowerShell to create a SQL Server on Azure Virtual Machine (VM). For a streamlined Azure PowerShell example that relies on default values, see the [SQL Server on Azure VM PowerShell quickstart](sql-vm-create-powershell-quickstart.md), or for an end-to-end script, see [Create SQL Server VM with PowerShell script](../scripts/create-sql-vm-powershell.md).

## Prerequisites 

To complete this guide, you should have the following: 

- An Azure subscription. If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- The latest version of Azure PowerShell

[!INCLUDE [updated-for-az.md](../../includes/updated-for-az.md)]


## Define variables

To reuse values and simplify script creation, start by defining a number of variables. Change the parameter values as you want, but be aware of naming restrictions related to name lengths and special characters when modifying the values provided.

Start by defining the parameters to use throughout the script, such as the location, name of the resource group, the SQL Server image and storage account you want to use, as well as the network and virtual machine properties. 

### Location and resource group

Define the data region, resource group, and subscription where you want to create your SQL Server VM and associated resources. 

Modify as you want and then run these cmdlets to initialize these variables.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="GlobalVariables":::


### Storage properties

Define the storage account and the type of storage to be used by the virtual machine.

Modify as you want, and then run the following cmdlet to initialize these variables. We recommend using [premium SSDs](/azure/virtual-machines/disks-types#premium-ssds) for production workloads.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="StorageVariables":::

> [!NOTE]
> The storage account name must be between 3 and 24 characters in length and use numbers and lower-case letters only, so make sure your resource group name doesn't have any special characters, or modify the name of the storage account to use a different name than $ResourceGroupName. 

### Network properties

Define the properties to be used by the network in the virtual machine. 

- Network interface
- TCP/IP allocation method
- Virtual network name
- Virtual subnet name
- Range of IP addresses for the virtual network
- Range of IP addresses for the subnet
- Public domain name label

Modify as you want and then run this cmdlet to initialize these variables.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="NetworkVariables":::

### Virtual machine properties

Define the following properties:

- Virtual machine name
- Computer name
- Virtual machine size
- Operating system disk name for the virtual machine

Modify as you want and then run this cmdlet to initialize these variables.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="ComputeVariables":::

### Choose a SQL Server image

It's possible to deploy an older image of SQL Server that isn't available in the Azure portal by using PowerShell.

Use the following variables to define the SQL Server image to use for the virtual machine. 

1. List all of the SQL Server image offerings with the [Get-AzVMImageOffer](/powershell/module/az.compute/get-azvmimageoffer) command to list the current available images in the Azure portal as well as older images that you can only deploy with PowerShell: 

   ```powershell
   Get-AzVMImageOffer -Location $Location -Publisher 'MicrosoftSQLServer'
   ```

   [!INCLUDE[appliesto-sqlvm](../../includes/virtual-machines-2008-end-of-support.md)]

1. List the available editions for your offer with the [Get-AzVMImageSku](/powershell/module/az.compute/get-azvmimagesku).

   ```powershell
   Get-AzVMImageSku -Location $Location -Publisher 'MicrosoftSQLServer' -Offer $OfferName | Select Skus
   ```

For this tutorial, use the SQL Server 2022 Developer edition (**SQLDEV-GEN2**) on Windows Server 2022. The Developer edition is freely licensed for testing and development, and you only pay for the cost of running the VM: 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="ImageVariables":::


## Create a resource group

Open PowerShell and establish access to your Azure account by running the [Connect-AzAccount](/powershell/module/az.accounts/connect-azaccount) command, and set your subscription context with [Set-AzContext](/powershell/module/az.accounts/set-azcontext). When prompted, enter your credentials. Use the same email and password that you use to sign in to the Azure portal.

After you've established subscription context, the first object that you create is the resource group.  Use the [Connect-AzAccount](/powershell/module/az.accounts/connect-azaccount) command to connect to Azure, and set your subscription context with [Set-AzContext](/powershell/module/az.accounts/set-azcontext).  Use the [New-AzResourceGroup](/powershell/module/az.resources/new-azresourcegroup) cmdlet to create an Azure resource group and its resources. Specify the variables that you previously initialized for the resource group name and location.

Run this cmdlet to connect to Azure, establish subscription context and create your new resource group: 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="CreateResourceGroup":::

## Create a storage account

The virtual machine requires storage resources for the operating system disk and for the SQL Server data and log files. For simplicity, you'll create a single disk for both. You can attach additional disks later using the [Add-Azure Disk](/powershell/module/servicemanagement/azure/add-azuredisk) cmdlet to place your SQL Server data and log files on dedicated disks. Use the [New-AzStorageAccount](/powershell/module/az.storage/new-azstorageaccount) cmdlet to create a standard storage account in your new resource group. Specify the variables that you previously initialized for the storage account name, storage SKU name, and location.

Run this cmdlet to create your new storage account: 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="CreateStorageAccount":::

> [!TIP]
> Creating the storage account can take a few minutes.

## Create network resources

The virtual machine requires a number of network resources for network connectivity.

* Each virtual machine requires a virtual network.
* A virtual network must have at least one subnet defined.
* A network interface must be defined with either a public or a private IP address.

### Create a virtual network subnet configuration

Start by creating a subnet configuration for your virtual network. For this tutorial, create a default subnet using the [New-AzVirtualNetworkSubnetConfig](/powershell/module/az.network/new-azvirtualnetworksubnetconfig) cmdlet. Specify the variables that you previously initialized for the subnet name and address prefix.

> [!NOTE]
> You can define additional properties of the virtual network subnet configuration using this cmdlet, but that is beyond the scope of this tutorial.

Run this cmdlet to create your virtual subnet configuration.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="CreateSubnetConfig":::

### Create a virtual network

Next, create your virtual network in your new resource group using the [New-AzVirtualNetwork](/powershell/module/az.network/new-azvirtualnetwork) cmdlet. Specify the variables that you previously initialized for the name, location, and address prefix. Use the subnet configuration that you defined in the previous step.

Run this cmdlet to create your virtual network:

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="CreateVirtualNetwork":::

### Create the public IP address

Now that your virtual network is defined, you must configure an IP address to connect to the virtual machine. For this tutorial, create a public IP address using dynamic IP addressing to support Internet connectivity. Use the [New-AzPublicIpAddress](/powershell/module/az.network/new-azpublicipaddress) cmdlet to create the public IP address in your new resource group. Specify the variables that you previously initialized for the name, location, allocation method, and DNS domain name label.

> [!NOTE]
> You can define additional properties of the public IP address using this cmdlet, but that is beyond the scope of this initial tutorial. You could also create a private address or an address with a static address, but that is also beyond the scope of this tutorial.

Run this cmdlet to create your public IP address.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="CreatePublicIP":::

### Create the network security group

To secure the VM and SQL Server traffic, create a network security group.

1. Create two network security group rules by using [New-AzNetworkSecurityRuleConfig](/powershell/module/az.network/new-aznetworksecurityruleconfig), a rule for remote desktop (RDP) to allow RDP connections, and a rule that allows traffic on TCP port 1433. Doing so enables connections to SQL Server over the internet.

   :::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="CreateNetworkSecurityGroupRules":::

1. Create the network security group by using [New-AzNetworkSecurityGroup](/powershell/module/az.network/new-aznetworksecuritygroup).

   :::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="CreateNetworkSecurityGroup":::

### Create the network interface

Now you're ready to create the network interface for your virtual machine. Use the [New-AzNetworkInterface](/powershell/module/az.network/new-aznetworkinterface) cmdlet to create the network interface in your new resource group. Specify the name, location, subnet, and public IP address previously defined.

Run this cmdlet to create your network interface.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="CreateNetworkInterface":::

## Configure a VM object

Now that storage and network resources are defined, you're ready to define compute resources for the virtual machine.

- Specify the virtual machine size and various operating system properties.
- Specify the network interface that you previously created.
- Define blob storage.
- Specify the operating system disk.

## Create the SQL Server VM

To create your SQL Server VM, first create a credential object, and then create the VM. 

### Create a credential object to hold the name and password for the local administrator credentials

Before you can set the operating system properties for the virtual machine, you must supply the credentials for the local administrator account as a secure string. To accomplish this, use the [Get-Credential](/powershell/module/microsoft.powershell.security/get-credential) cmdlet.

Run the following cmdlet. You'll need to type the VM's local administrator name and password into the PowerShell credential request window.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="CreateCredential":::

### Define properties and create the VM

Now you're ready to set the virtual machine's operating system properties with [New-AzVMConfig](/powershell/module/az.compute/new-azvmconfig), create the VM with [New-AzVM](/powershell/module/az.compute/new-azvm), and use [Add-AzVMNetworkInterface](/powershell/module/az.compute/add-azvmnetworkinterface) cmdlet to add the network interface using the variable that you defined earlier. 

The sample script does the following: 

- Require the [virtual machine agent](/azure/virtual-machines/extensions/agent-windows) to be installed.
- Specifies that the cmdlet enables auto update.
- Specifies the variables that you previously initialized for the virtual machine name, the computer name, and the credential.

Run this cmdlet to set the operating system properties for your virtual machine.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="CreateVirtualMachine":::

The virtual machine is created.

> [!NOTE]
> If you get an error about boot diagnostics, you can ignore it. A standard storage account is created for boot diagnostics because the specified storage account for the virtual machine's disk is a premium storage account.

## Install the SQL IaaS Agent extension

SQL Server virtual machines support automated management features with the [SQL Server IaaS Agent Extension](sql-server-iaas-agent-extension-automate-management.md). To register your SQL Server with the extension run the [New-AzSqlVM](/powershell/module/az.sqlvirtualmachine/new-azsqlvm) command after the virtual machine is created. Specify the license type for your SQL Server VM, choosing between pay-as-you-go (`PAYG`), bring-your-own-license via the [Azure Hybrid Benefit](https://azure.microsoft.com/pricing/hybrid-benefit/) (`AHUB`), disaster recovery (`DR`) to activate the [free DR replica license](business-continuity-high-availability-disaster-recovery-hadr-overview.md#free-dr-replica-in-azure). For more information about licensing, see [licensing model](licensing-model-azure-hybrid-benefit-ahb-change.md). 


To register your SQL Server VM with the SQL IaaS Agent extension, first register your subscription with the resource provider by using [Register-AzResourceProvider](/powershell/module/az.resources/register-azresourceprovider), and then register your SQL Server VM with the SQL IaaS Agent extension by using [New-AzSqlVM](/powershell/module/az.sqlvirtualmachine/new-azsqlvm): 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/virtual-machine/create-sql-server-vm.ps1" id="RegisterSQLIaaSExtension":::

There are three ways to register with the extension: 
- [Automatically for all current and future VMs in a subscription](sql-agent-extension-automatic-registration-all-vms.md)
- [Manually for a single VM](sql-agent-extension-manually-register-single-vm.md)
- [Manually for multiple VMs in bulk](sql-agent-extension-manually-register-vms-bulk.md)


## Stop or remove a VM

If you don't need the VM to run continuously, you can avoid unnecessary charges by stopping it when not in use. The following command stops the VM but leaves it available for future use.

```powershell
Stop-AzVM -Name $VMName -ResourceGroupName $ResourceGroupName
```

You can also permanently delete all resources associated with the virtual machine with the [Remove-AzResourceGroup command](/powershell/module/az.resources/remove-azresourcegroup). Doing so permanently deletes the virtual machine as well, so use this command with care.

## Full script

For a full PowerShell script that provides an end-to-end experience, see [Deploy SQL Server on Azure VM with PowerShell](../scripts/create-sql-vm-powershell.md). 


## Related content

After the virtual machine is created, you can:

- Connect to the virtual machine using RDP
- Configure SQL Server settings in the portal for your VM, including:
   - [Storage settings](storage-configuration.md) 
   - [Automated management tasks](sql-server-iaas-agent-extension-automate-management.md)
- [Configure connectivity](ways-to-connect-to-sql.md)
- Connect clients and applications to the new SQL Server instance
