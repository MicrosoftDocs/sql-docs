---
title: How to evaluate Azure Arc-enabled SQL Server instance with an Azure VM
description: Learn how to evaluate Azure Arc-enabled SQL Server instance using an Azure virtual machine.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 04/20/2023
ms.topic: conceptual
---

# Evaluate Azure Arc-enabled SQL Server instance on an Azure virtual machine

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Azure Arc-enabled SQL Server is designed to help you connect servers running on-premises or in other clouds to Azure. Normally, you wouldn't use Azure Arc-enabled SQL Server on an Azure virtual machine. All the Azure capabilities are natively available for these VMs, for example:

- Representation of the VM in Azure Resource Manager
- VM extensions
- Managed identities
- Azure Policy

While you can't install Azure Arc-enabled SQL Server on an Azure VM for production scenarios, it's possible to configure Azure Arc-enabled servers to run on an Azure VM for *evaluation and testing purposes only*. This article explains how to set up an Azure SQL VM before you can enable Azure Arc-enabled SQL Server on it.

> [!NOTE]
> The steps in this article are intended for virtual machines hosted in the Azure cloud. Azure Arc-enabled SQL Server is not supported on virtual machines running on Azure Stack Hub or Azure Stack Edge.

When you follow this article, you will:

1. Create an Azure SQL VM.
1. Remove the Azure extensions.
1. Disable the Azure VM Guest Agent.
1. Connect the SQL Server to Azure Arc (Arc-enabled the SQL Server instance).

## Create evaluation server

Create an Azure SQL Virtual Machine. Use an [available Azure SQL VM image](/azure/azure-sql/virtual-machines/windows/sql-vm-create-portal-quickstart).

## Remove any VM extensions on the Azure VM

1. In the Azure portal, navigate to your Azure VM resource and from the left-hand pane, select **Extensions + applications**.
1. Notice any extensions. Because this VM is an Azure SQL VM, it has *SQLIaasExtension*.
1. Select **SQLIaasExtension**, and select **Uninstall**.
1. If there are any other extensions installed on the VM, select each extension individually, and then select **Uninstall**.
1. Wait for all extensions to finish uninstalling before you proceed.

## Disable the Azure VM Guest agent

To disable the Azure VM Guest agent:

1. Connect to the virtual machine.
1. On the virtual machine, run the following PowerShell.

   ```powershell
   Set-Service WindowsAzureGuestAgent -StartupType Disabled -Verbose
   Stop-Service WindowsAzureGuestAgent -Force -Verbose
   ```

1. Block access to the Azure IMDS endpoint

   While still connected to the server, run the following commands to block access to the Azure IMDS endpoint. For Windows, run the following PowerShell command:

   ```powershell
   New-NetFirewallRule -Name BlockAzureIMDS -DisplayName "Block access to Azure IMDS" -Enabled True -Profile Any -Direction Outbound -Action Block -RemoteAddress 169.254.169.254
   ```

## Connect the SQL Server to Azure Arc

Connect the instance of SQL Server to Azure Arc. Follow the steps in [Quickstart: Connect SQL Server machines to Azure Arc](quick-enabled-sql-server.md).

## Clean up your evaluation environment

After you have evaluated Arc-enabled SQL Server on an Azure Virtual Machine, to avoid charges, delete your resource groups.

## Next steps
- [Automatically connect your SQL Server to Azure Arc](automatically-connect.md)
- [Configure SQL best practices assessment](assess.md)