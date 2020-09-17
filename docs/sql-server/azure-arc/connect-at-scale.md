---
title: Connect SQL Server instances to Azure Arc at scale
titleSuffix:
description: In this article, you learn how to connect SQL Server instances as Azure Arc enabled SQL Servers (preview) using a service principal.
author: anosov1960
ms.author: sashan 
ms.reviewer: mikeray
ms.date: 09/10/2020
ms.topic: conceptual
ms.prod: sql
---

# Connect SQL Server instances to Azure Arc at scale

You can connect multiple SQL Server instances installed on multiple Windows or Linux machines to Azure Arc using the same [script your generated for a single machine](connect.md). The script will connect and register each machine and the installed SQL Server instances on it to Azure Arc. For the best experience, we recommend using an Azure Active Directory [service principal](https://docs.microsoft.com/azure/active-directory/develop/app-objects-and-service-principals). A service principal is a special limited management identity that is granted only the minimum permission necessary to connect machines to Azure and to create the Azure resources for Azure Arc enabled server and Azure Arc enabled SQL Server. This is safer than using a higher privileged account like a Tenant Administrator, and follows our access control security best practices.  

The installation methods to install and configure the Connected Machine agent requires that the automated method you use has  administrator permissions on the machines. On Linux, by using the root account and on Windows, as a member of the Local Administrators group.

Before you get started, be sure to review the [prerequisites](overview.md#prerequisites) and make sure that you have created a custom role that meets the required permissions.

If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

At the end of this process, you will have successfully connected your hybrid machines to Azure Arc enabled servers (preview).

## Connecting multiple SQL Server instances on Windows using Azure PowerShell

Each machine must have [Azure PowerShell](/powershell/azure/install-az-ps) installed.

1. Create the service principal using PowerShell using the [`New-AzADServicePrincipal`](/powershell/module/az.resources/new-azadserviceprincipal) cmdlet. Make sure to store the output in a variable. Otherwise, you will not be able to retrieve the password needed later.

    ```azurepowershell-interactive
    $sp = New-AzADServicePrincipal -DisplayName "Arc-for-servers" -Role <your custom role>
    $sp
    ```

    ```output
    Secret                : System.Security.SecureString
    ServicePrincipalNames : {ad9bcd79-be9c-45ab-abd8-80ca1654a7d1, https://Arc-for-servers}
    ApplicationId         : ad9bcd79-be9c-45ab-abd8-80ca1654a7d1
    ObjectType            : ServicePrincipal
    DisplayName           : Hybrid-RP
    Id                    : 5be92c87-01c4-42f5-bade-c1c10af87758
    Type                  :
    ```

> [!NOTE]
> When you create a service principal, your account must be an Owner or User Access Administrator in the subscription that you want to use for onboarding. If you don't have sufficient permissions to create role assignments, the service principal might be created, but it won't be able to onboard machines. The instructions on how to create a custom role are provided in [Required permissions](overview.md#required-permissions).

2. Retrieve the password stored in the `$sp` variable:

   ```azurepowershell-interactive
   $credential = New-Object pscredential -ArgumentList "temp", $sp.Secret
   $credential.GetNetworkCredential().password
   ```

3. In the output, find the password value under the field **password** and copy it. Also find the value under the field **ApplicationId** and copy it also. Save them for later in a secure place. If you forget or lose your service principal password, you can reset it using the [`New-AzADSpCredential`](/powershell/module/azurerm.resources/new-azurermadspcredential) cmdlet.

> [!NOTE]
> Note that Azure Arc for servers doesn’t currently support signing in with a certificate, so the service principal must have a secret to authenticate with.

4. Retrieve the value of the service principal’s tenant ID:
 
   ```azurepowershell-interactive
   $tenantId= (Get-AzContext).Tenant.Id
   ```
5. Download the Linux shell script from the Portal following the instructions in [Connect your SQL Server to Azure Arc](connect.md).

6. Open the script in an admin instance of PowerShell ISE and replace the following environment variables using the values generated during the service principal provisioning described earlier. These variable are initially empty.

   ```azurepowershell-interactive
   $servicePrincipalAppId="{serviceprincipalAppID}"
   $servicePrincipalSecret="{serviceprincipalPassword}"
   $servicePrincipalTenantId="{serviceprincipalTenantId}"
   ```

7. Execute the script on each target machine

## Connecting multiple SQL Server instances on Linux using Azure CLI

Each target machine must have the [Azure CLI installed](/cli/azure/install-azure-cli). The registration script will automatically sign in to azure with the service principal credentials if they’re provided and no other user is already signed in. Use the following steps to connect SQL Server instances on multiple Linux machines.

1. Create the service principal using the ['az ad sp create-for-rbac'](/cli/azure/ad/sp#az_ad_sp_create_for_rbac) command. 

   ```azurecli-interactive
   az ad sp create-for-rbac --name <your service principal name> --role <your custom role name>    
   ```

   ```output
   { "appId": "d2ff754a-e10a-4eb6-9cdc-ce6e7a4687db",
     "displayName": "Arc-for-servers",
     "name": "http://Arc-for-servers",
     "password": {SomeRandomlyGeneratedPassword}",
     "tenant": "2530e75f-673b-4841-8270-47ca6a34ef4f"
   }
   ```

> [!NOTE]
> When you create a service principal, your account must be an Owner or User Access Administrator in the subscription that you want to use for onboarding. If you don't have sufficient permissions to create role assignments, the service principal might be created, but it won't be able to onboard machines. The instructions on how to create a custom role are provided in [Required permissions](overview.md#required-permissions).

2. Download the Linux shell script from the Portal following the instructions in [Connect your SQL Server to Azure Arc](connect.md).

3. Replace the following variables in the script using the values returned by the 'az ad sp create-for-rbac' command. These variable are initially empty.

   ```bash
   servicePrincipalAppId="{serviceprincipalAppID}"
   servicePrincipalSecret="{serviceprincipalPassword}"
   servicePrincipalTenant="{serviceprincipalTenant}"
   ```

3. Execute the script on each target machines
 
   ```bash
   sudo chmod +x ./RegisterSqlServerArc.sh
   ./RegisterSqlServerArc.sh
   ```

## Validate successful onboarding

After you register SQL Server instances with Azure Arc enabled SQL Server (preview), go to the [Azure portal](https://aka.ms/azureportal) and view the newly created Azure Arc resources. You will see a new __Machine - Azure Arc__ for each connected machine and a new __SQL Server - Azure Arc__ resource for each registered SQL Server instance. 

![A successful onboard](./media/join-at-scale/successful-onboard.png)

## Next steps

- Learn how to manage your machine using [Azure Policy](/azure/governance/policy/overview), for such things as VM [guest configuration](/azure/governance/policy/concepts/guest-configuration), verifying the machine is reporting to the expected Log Analytics workspace, enable monitoring with [Azure Monitor with VMs](/azure/azure-monitor/insights/vminsights-enable-policy), and much more.

- Learn more about the [Log Analytics agent](/azure/azure-monitor/platform/log-analytics-agent). The Log Analytics agent for Windows and Linux is required when you want to proactively monitor the OS and workloads running on the machine, manage it using Automation runbooks or solutions like Update Management, or use other Azure services like [Azure Security Center](/azure/security-center/security-center-intro).

- Learn how to [Configure your SQL Server instance for periodic environment health check using on-demand SQL assessment](assess.md)

- Learn how to [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
