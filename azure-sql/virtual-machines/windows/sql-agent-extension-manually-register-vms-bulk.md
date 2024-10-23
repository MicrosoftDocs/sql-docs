---
title: Register multiple SQL VMs in Azure with the SQL IaaS Agent extension
description: Bulk register SQL Server VMs with the SQL IaaS Agent extension to improve manageability.
author: adbadram
ms.author: adbadram
ms.reviewer: mathoma
ms.date: 10/26/2021
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: how-to
ms.custom:
tags: azure-resource-manager
---
# Register multiple SQL VMs in Azure with the SQL IaaS Agent extension
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article describes how to register your SQL Server virtual machines (VMs) in bulk in Azure with the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md) by using the `Register-SqlVMs`Azure PowerShell cmdlet. 


Alternatively, you can register [all SQL Server VMs automatically](sql-agent-extension-automatic-registration-all-vms.md) or [individual SQL Server VMs manually](sql-agent-extension-manually-register-single-vm.md). 

[!INCLUDE [SQL VM feature benefits](../../includes/sql-vm-iaas-extension-permissions.md)]

## Overview

The `Register-SqlVMs` cmdlet can be used to register all virtual machines in a given list of subscriptions, resource groups, or a list of specific virtual machines. The cmdlet will register the virtual machines and then generate both a [report and a log file](#output-description). 

The registration process carries no risk, has no downtime, and will not restart the SQL Server service or the virtual machine. 

By default, Azure VMs with SQL Server 2016 or later are automatically registered with the SQL IaaS Agent extension when detected by the [CEIP service](/sql/sql-server/usage-and-diagnostic-data-configuration-for-sql-server). You can use bulk registration to register any SQL Server VMs that are not detected by the CEIP service. 

For information about privacy, see the [SQL IaaS Agent extension privacy statements](sql-server-iaas-agent-extension-automate-management.md#in-region-data-residency). 

## Prerequisites

To register your SQL Server VM with the extension, you'll need the following: 

- An [Azure subscription](https://azure.microsoft.com/free/) that has been [registered with the **Microsoft.SqlVirtualMachine** resource provider](sql-agent-extension-manually-register-single-vm.md#register-subscription-with-rp) and contains unregistered SQL Server virtual machines.
- Ensure the Azure VM is running.
- The client credentials used to register the virtual machines exist in any of the following Azure roles: **Virtual Machine contributor**, **Contributor**, or **Owner**. 
- [Az PowerShell 5.0](/powershell/azure/new-azureps-module-az) - versions higher than 5.0 currently only support MFA and are not compatible with the script to register multiple VMs. 


## Get started

Before proceeding, you must first create a local copy of the script, import it as a PowerShell module, and connect to Azure. 

### Create the script

To create the script, copy the [full script](#full-script) from the end of this article and save it locally as `RegisterSqlVMs.psm1`. 

### Import the script

After the script is created, you can import it as a module in the PowerShell terminal. 

Open an administrative PowerShell terminal and navigate to where you saved the `RegisterSqlVMs.psm1` file. Then, run the following PowerShell cmdlet to import the script as a module: 

```powershell-interactive
Import-Module .\RegisterSqlVMs.psm1
```

### Connect to Azure

Use the following PowerShell cmdlet to connect to Azure:

```powershell-interactive
Connect-AzAccount
```


## All VMs in a list of subscriptions 

Use the following cmdlet to register all SQL Server virtual machines in a list of subscriptions:

```powershell-interactive
Register-SqlVMs -SubscriptionList SubscriptionId1,SubscriptionId2
```

Example output: 

```
Number of subscriptions registration failed for 
because you do not have access or credentials are wrong: 1
Total VMs Found: 10
VMs Already registered: 1
Number of VMs registered successfully: 4
Number of VMs failed to register due to error: 1
Number of VMs skipped as VM or the guest agent on VM is not running: 3
Number of VMs skipped as they are not running SQL Server On Windows: 1

Please find the detailed report in file RegisterSqlVMScriptReport1571314821.txt
Please find the error details in file VMsNotRegisteredDueToError1571314821.log
```

## All VMs in a single subscription

Use the following cmdlet to register all SQL Server virtual machines in a single subscription: 

```powershell-interactive
Register-SqlVMs -Subscription SubscriptionId1
```

Example output:

```
Total VMs Found: 10
VMs Already registered: 1
Number of VMs registered successfully: 5
Number of VMs failed to register due to error: 1
Number of VMs skipped as VM or the  guest agent on VM is not running: 2
Number of VMs skipped as they are not running SQL Server On Windows: 1

Please find the detailed report in file RegisterSqlVMScriptReport1571314821.txt
Please find the error details in file VMsNotRegisteredDueToError1571314821.log
```

## All VMs in multiple resource groups

Use the following cmdlet to register all SQL Server virtual machines in multiple resource groups within a single subscription:

```powershell-interactive
Register-SqlVMs -Subscription SubscriptionId1 -ResourceGroupList ResourceGroup1,ResourceGroup2
```

Example output:

```
Total VMs Found: 4
VMs Already registered: 1
Number of VMs registered successfully: 1
Number of VMs failed to register due to error: 1
Number of VMs skipped as they are not running SQL Server On Windows: 1

Please find the detailed report in file RegisterSqlVMScriptReport1571314821.txt
Please find the error details in file VMsNotRegisteredDueToError1571314821.log
```

## All VMs in a resource group

Use the following cmdlet to register all SQL Server virtual machines in a single resource group: 

```powershell-interactive
Register-SqlVMs -Subscription SubscriptionId1 -ResourceGroupName ResourceGroup1
```

Example output:

```
Total VMs Found: 4
VMs Already registered: 1
Number of VMs registered successfully: 1
Number of VMs failed to register due to error: 1
Number of VMs skipped as VM or the guest agent on VM is not running: 1

Please find the detailed report in file RegisterSqlVMScriptReport1571314821.txt
Please find the error details in file VMsNotRegisteredDueToError1571314821.log
```

## Specific VMs in a single resource group

Use the following cmdlet to register specific SQL Server virtual machines within a single resource group:

```powershell-interactive
Register-SqlVMs -Subscription SubscriptionId1 -ResourceGroupName ResourceGroup1 -VmList VM1,VM2,VM3
```

Example output:

```
Total VMs Found: 3
VMs Already registered: 0
Number of VMs registered successfully: 1
Number of VMs skipped as VM or the guest agent on VM is not running: 1
Number of VMs skipped as they are not running SQL Server On Windows: 1

Please find the detailed report in file RegisterSqlVMScriptReport1571314821.txt
Please find the error details in file VMsNotRegisteredDueToError1571314821.log
```

## A specific VM

Use the following cmdlet to register a specific SQL Server virtual machine: 

```powershell-interactive
Register-SqlVMs -Subscription SubscriptionId1 -ResourceGroupName ResourceGroup1 -Name VM1
```

Example output:

```
Total VMs Found: 1
VMs Already registered: 0
Number of VMs registered successfully: 1

Please find the detailed report in  file RegisterSqlVMScriptReport1571314821.txt
```


## Output description

Both a report and a log file are generated every time the `Register-SqlVMs` cmdlet is used. 

### Report

The report is generated as a `.txt` file named `RegisterSqlVMScriptReport<Timestamp>.txt` where the timestamp is the time when the cmdlet was started. The report lists the following details:

| **Output value** | **Description** |
| :--------------  | :-------------- | 
| Number of subscriptions registration failed for because you do not have access or credentials are incorrect | This provides the number and list of subscriptions that had issues with the provided authentication. The detailed error can be found in the log by searching for the subscription ID. | 
| Number of subscriptions that could not be tried because they are not registered to the resource provider | This section contains the count and list of subscriptions that have not been registered to the SQL IaaS Agent extension. |
| Total VMs found | The count of virtual machines that were found in the scope of the parameters passed to the cmdlet. | 
| VMs already registered | The count of virtual machines that were skipped as they were already registered with the extension. |
| Number of VMs registered successfully | The count of virtual machines that were successfully registered after running the cmdlet. Lists the registered virtual machines in the format `SubscriptionID, Resource Group, Virtual Machine`. | 
| Number of VMs failed to register due to error | Count of virtual machines that failed to register due to some error. The details of the error can be found in the log file. | 
| Number of VMs skipped as the VM or the gust agent on VM is not running | Count and list of virtual machines that could not be registered as either the virtual machine or the guest agent on the virtual machine were not running. These can be retried once the virtual machine or guest agent has been started. Details can be found in the log file. |
| Number of VMs skipped as they are not running SQL Server on Windows | Count of virtual machines that were skipped as they are not running SQL Server or are not a Windows virtual machine. The virtual machines are listed in the format `SubscriptionID, Resource Group, Virtual Machine`. | 


### Log 

Errors are logged in the log file named `VMsNotRegisteredDueToError<Timestamp>.log`, where timestamp is the time when the script started. If the error is at the subscription level, the log contains the comma-separated Subscription ID and the error message. If the error is with the virtual machine registration, the log contains the Subscription ID, Resource group name, virtual machine name, error code, and message separated by commas. 

## Remarks

When you register SQL Server VMs with the extension by using the provided script, consider the following:

- Registration with the extension requires a guest agent running on the SQL Server VM. Windows Server 2008 images do not have a guest agent, so these virtual machines will fail and must be [registered manually](sql-agent-extension-manually-register-single-vm.md#register-with-extension)  with [limited functionality](sql-server-iaas-agent-extension-automate-management.md#feature-benefits). 
- There is retry logic built-in to overcome transparent errors. If the virtual machine is successfully registered, then it is a rapid operation. However, if the registration fails, then each virtual machine will be retried.  As such, you should allow significant time to complete the registration process -  though actual time requirement is dependent on the type and number of errors. 


## Full script

For the full script on GitHub, see [Bulk register SQL Server VMs with Az PowerShell](https://github.com/Azure/azure-docs-powershell-samples/blob/master/sql-virtual-machine/register-sql-vms/RegisterSqlVMs.psm1). 

Copy the full script and save it as `RegisterSqLVMs.psm1`.

## Next steps

- Review the benefits provided by the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md).
- [Manually register a single VM](sql-agent-extension-manually-register-single-vm.md)
- [Automatically register all VMs in a subscription](sql-agent-extension-automatic-registration-all-vms.md).
- [Troubleshoot known issues with the extension](sql-agent-extension-troubleshoot-known-issues.md).
- Review the [SQL IaaS Agent extension privacy statements](sql-server-iaas-agent-extension-automate-management.md#in-region-data-residency).
- Review the [best practices checklist](performance-guidelines-best-practices-checklist.md) to optimize for performance and security. 

To learn more, review the following articles:

* [Overview of SQL Server on Windows VMs](sql-server-on-azure-vm-iaas-what-is-overview.md)
* [FAQ for SQL Server on Windows VMs](frequently-asked-questions-faq.yml)
* [Pricing guidance for SQL Server on Azure VMs](../windows/pricing-guidance.md)
* [What's new for SQL Server on Azure VMs](../windows/doc-changes-updates-release-notes-whats-new.md)
