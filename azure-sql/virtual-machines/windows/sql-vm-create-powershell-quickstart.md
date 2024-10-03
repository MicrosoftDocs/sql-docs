---
title: "Quickstart: Create SQL Server VM with PowerShell"
description: This tutorial shows how to use Azure PowerShell to create a Windows virtual machine running SQL Server 2022.
author: bluefooted
ms.author: pamela
ms.reviewer: mathoma
ms.date: 05/29/2024
ms.service: virtual-machines-sql
ms.subservice: deployment
ms.topic: quickstart
ms.custom:
  - devx-track-azurepowershell
  - mode-api
tags: azure-resource-manager
---

# Quickstart: Create SQL Server on Azure VM with PowerShell

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This quickstart steps through creating a Windows SQL Server on Azure Virtual Machine (VM) with Azure PowerShell.

> [!TIP]
> - This quickstart provides a path to quickly provision and connect to a SQL Server VM. For more information about other Azure PowerShell options to create SQL Server VMs, see the [Provisioning guide for SQL Server VMs with Azure PowerShell](create-sql-vm-powershell.md).
> - If you have questions about SQL Server virtual machines, see the [Frequently Asked Questions](frequently-asked-questions-faq.yml).

## Prerequisites 

To complete this quickstart, you should have the following: 

- An Azure subscription. If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- The latest version of Azure PowerShell

[!INCLUDE [updated-for-az.md](../../includes/updated-for-az.md)]


## Connect to Azure

1. Open PowerShell and establish access to your Azure account by running the [Connect-AzAccount](/powershell/module/az.accounts/connect-azaccount) command, and set your subscription context with [Set-AzContext](/powershell/module/az.accounts/set-azcontext).

   ```powershell
   Connect-AzAccount
   Set-AzContext -subscription <Subscription ID>
   ```

1. When you see the sign-in window, enter your credentials. Use the same email and password that you use to sign in to the Azure portal.

## Create a resource group


Define variables for a unique resource group name and provide a location of a target Azure region for all VM resources. Then use [New-AzResourceGroup](/powershell/module/az.resources/new-azresourcegroup) to create your resource group. To simplify the rest of the quickstart, the remaining commands use this name as a basis for other resource names.

   ```powershell
   $ResourceGroupName = "sqlvm1"
   $Location = "East US"
   $ResourceGroupParams = @{
      Name = $ResourceGroupName
      Location = $Location
      Tag = @{Owner="SQLDocs-Samples"}
   }
   New-AzResourceGroup @ResourceGroupParams
   ```

## Configure network settings

1. Use [New-AzVirtualNetworkSubnetConfig](/powershell/module/az.network/new-azvirtualnetworksubnetconfig),  [New-AzVirtualNetwork](/powershell/module/az.network/new-azvirtualnetwork), and [New-AzPublicIpAddress](/powershell/module/az.network/new-azpublicipaddress) to create a virtual network, subnet, and a public IP address. These resources are used to provide network connectivity to the virtual machine and connect it to the internet.

   ``` PowerShell
   $SubnetName = $ResourceGroupName + "subnet"
   $VnetName = $ResourceGroupName + "vnet"
   $PipName = $ResourceGroupName + $(Get-Random)

   # Create a subnet configuration
   $SubnetConfig = New-AzVirtualNetworkSubnetConfig -Name $SubnetName -AddressPrefix 192.168.1.0/24

   # Create a virtual network
   $Vnet = New-AzVirtualNetwork -ResourceGroupName $ResourceGroupName -Location $Location `
      -Name $VnetName -AddressPrefix 192.168.0.0/16 -Subnet $SubnetConfig

   # Create a public IP address and specify a DNS name
   $Pip = New-AzPublicIpAddress -ResourceGroupName $ResourceGroupName -Location $Location `
      -AllocationMethod Static -IdleTimeoutInMinutes 4 -Name $PipName
   ```

1. Use [New-AzNetworkSecurityGroup](/powershell/module/az.network/new-aznetworksecuritygroup) to create a network security group after you've configured rules to allow remote desktop (RDP) and SQL Server connections with [New-AzNetworkSecurityRuleConfig](/powershell/module/az.network/new-aznetworksecurityruleconfig).

   ```powershell
   # Rule to allow remote desktop (RDP)
   $NsgRuleRDP = New-AzNetworkSecurityRuleConfig -Name "RDPRule" -Protocol Tcp `
      -Direction Inbound -Priority 1000 -SourceAddressPrefix * -SourcePortRange * `
      -DestinationAddressPrefix * -DestinationPortRange 3389 -Access Allow

   #Rule to allow SQL Server connections on port 1433
   $NsgRuleSQL = New-AzNetworkSecurityRuleConfig -Name "MSSQLRule"  -Protocol Tcp `
      -Direction Inbound -Priority 1001 -SourceAddressPrefix * -SourcePortRange * `
      -DestinationAddressPrefix * -DestinationPortRange 1433 -Access Allow

   # Create the network security group
   $NsgName = $ResourceGroupName + "nsg"
   $Nsg = New-AzNetworkSecurityGroup -ResourceGroupName $ResourceGroupName `
      -Location $Location -Name $NsgName `
      -SecurityRules $NsgRuleRDP,$NsgRuleSQL
   ```

1. Create the network interface with [New-AzNetworkInterface](/powershell/module/az.network/new-aznetworkinterface).

   ```powershell
   $InterfaceName = $ResourceGroupName + "int"
   $Interface = New-AzNetworkInterface -Name $InterfaceName `
      -ResourceGroupName $ResourceGroupName -Location $Location `
      -SubnetId $VNet.Subnets[0].Id -PublicIpAddressId $Pip.Id `
      -NetworkSecurityGroupId $Nsg.Id
   ```

## Create the SQL VM

1. Define your credentials to sign in to the VM. The username is "azureadmin". Make sure you change \<password> before running the command.

   ``` PowerShell
   # Define a credential object
    $userName = "azureadmin"
    $SecurePassword = ConvertTo-SecureString '<strong password>' `
       -AsPlainText -Force
    $Cred = New-Object System.Management.Automation.PSCredential ($userName, $securePassword)
   ```

1. Create a virtual machine configuration object with [New-AzVMConfig](/powershell/module/az.compute/new-azvmconfig) and then create the VM with [New-AzVM](/powershell/module/az.compute/new-azvm). The following command creates a SQL Server 2022 Developer Edition VM on Windows Server 2022.

   ```powershell
   # Create a virtual machine configuration
   $VMName = $ResourceGroupName + "VM"
   $VMConfig = New-AzVMConfig -VMName $VMName -VMSize Standard_DS13_V2 |
      Set-AzVMOperatingSystem -Windows -ComputerName $VMName -Credential $Cred -ProvisionVMAgent -EnableAutoUpdate |
      Set-AzVMSourceImage -PublisherName "MicrosoftSQLServer" -Offer "sql2022-ws2022" -Skus "sqldev-gen2" -Version "latest" |
      Add-AzVMNetworkInterface -Id $Interface.Id
   
   # Create the VM
   New-AzVM -ResourceGroupName $ResourceGroupName -Location $Location -VM $VMConfig
   ```

   > [!TIP]
   > It takes several minutes to create the VM.

## Register with SQL VM RP 

To get portal integration and SQL VM features, you must [register](sql-agent-extension-manually-register-single-vm.md#register-with-extension) with the [SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md).

To do so, first register your subscription with the resource provider by using [Register-AzResourceProvider](/powershell/module/az.resources/register-azresourceprovider):

```powershell-interactive
# Register the SQL IaaS Agent extension to your subscription
Register-AzResourceProvider -ProviderNamespace Microsoft.SqlVirtualMachine
```

Next, register your SQL Server VM with the SQL IaaS Agent extension by using [New-AzSqlVM](/powershell/module/az.sqlvirtualmachine/new-azsqlvm): 

```powershell-interactive
$License = 'PAYG'

# Register SQL Server VM with the extension
New-AzSqlVM -Name $VMName -ResourceGroupName $ResourceGroupName -Location $Location `
-LicenseType $License
```

## Remote desktop into the VM

1. Use [Get-AzPublicIpAddress](/powershell/module/az.network/get-azpublicipaddress) to retrieve the public IP address for the new VM.

   ```powershell
   Get-AzPublicIpAddress -ResourceGroupName $ResourceGroupName | Select IpAddress
   ```

1. Pass the returned IP address as a command-line parameter to **mstsc** to start a Remote Desktop session into the new VM.

   ```
   mstsc /v:<publicIpAddress>
   ```

1. When prompted for credentials, choose to enter credentials for a different account. Enter the username with a preceding backslash (for example, `\azureadmin`), and the password that you set previously in this quickstart.

## Connect to SQL Server

1. After signing in to the Remote Desktop session, launch **SQL Server Management Studio 2017** from the start menu.

1. In the **Connect to Server** dialog box, keep the defaults. The server name is the name of the VM. Authentication is set to **Windows Authentication**. Select **Connect**.

You're now connected to SQL Server locally. If you want to connect remotely, you must [configure connectivity](ways-to-connect-to-sql.md) from the Azure portal or manually.

## Clean up resources

If you don't need the VM to run continuously, you can avoid unnecessary charges by stopping it when not in use. The following command stops the VM but leaves it available for future use.

```powershell
Stop-AzVM -Name $VMName -ResourceGroupName $ResourceGroupName
```

You can also permanently delete all resources associated with the virtual machine with the [Remove-AzResourceGroup](/powershell/module/az.resources/remove-azresourcegroup) command. Doing so permanently deletes the virtual machine as well, so use this command with care.

## Next steps

In this quickstart, you created a SQL Server 2022 virtual machine using Azure PowerShell. To learn more about how to migrate your data to the new SQL Server, see the following article.

> [!div class="nextstepaction"]
> [Migration guide: SQL Server to SQL Server on Azure Virtual Machines](../../migration-guides/virtual-machines/sql-server-to-sql-on-azure-vm-individual-databases-guide.md)
