---
title: Connect SQL Server instances on Azure Arc-enabled servers at scale with a script
description: In this article, you learn different ways of connecting SQL Server instances to Azure Arc at scale with a script.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 03/08/2024
ms.topic: conceptual
---

# Connect SQL Server instances to Azure at scale with a script

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [automatic](includes/if-manual.md)]

This article describes how to connect SQL Server instances installed on multiple Windows or Linux machines to Azure Arc as a single task using a script. After the machines are connected to Azure, Azure Arc automatically installs the Azure extension for SQL Server. For information, see [Automatically connect your SQL Server to Azure Arc](automatically-connect.md).

For the best experience, use Microsoft Entra ID. With Microsoft Entra ID, you define a [service principal](/azure/active-directory/develop/app-objects-and-service-principals). The service principal is a special limited management identity that is granted only the minimum permissions necessary to connect machines to Azure and to create the Azure resources for Azure Arc-enabled server and [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)].

Before you get started, be sure to review the [prerequisites](prerequisites.md) and make sure that you've created a [custom role](/azure/role-based-access-control/custom-roles-portal) that meets the required permissions.

### Connect multiple instances

# [Windows](#tab/windows)

Each machine must have [Azure PowerShell](/powershell/azure/install-az-ps) installed.

1. Create the service principal. Use the [`New-AzADServicePrincipal`](/powershell/module/az.resources/new-azadserviceprincipal) cmdlet. Make sure to store the output in a variable. Otherwise, you won't be able to retrieve the password needed later.

    ```azurepowershell-interactive
    $sp = New-AzADServicePrincipal -DisplayName "Arc-for-servers" -Role <your custom role>
    $sp
    ```

2. Give the service principal permissions to access Microsoft Graph.

   > [!NOTE]
   >
   > * When you create a service principal, your account must be an Owner or User Access Administrator in the subscription that you want to use for onboarding. If you don't have sufficient permissions to create role assignments, the service principal might be created, but it won't be able to onboard machines. The instructions on how to create a custom role are provided in [prerequisites](prerequisites.md).
   > 

1. Retrieve the password stored in the `$sp` variable:

   ```azurepowershell-interactive
   $credential = New-Object pscredential -ArgumentList "temp", $sp.PasswordCredentials.SecretText 
   $credential.GetNetworkCredential().password
   ```

3. Retrieve the value of the service principal's tenant ID:

   ```azurepowershell-interactive
   $tenantId= (Get-AzContext).Tenant.Id
   ```

4. Copy and save the password, application ID, and tenant ID values using the appropriate security practices. If you forget or lose your service principal password, you can reset it using the [`New-AzADSpCredential`](/powershell/module/azurerm.resources/new-azurermadspcredential) cmdlet.

   > [!NOTE]
   > Note that Azure Arc for servers doesn't currently support signing in with a certificate, so the service principal must have a secret to authenticate with.

5. Download the PowerShell script from the Portal following the instructions in [Connect your SQL Server to Azure Arc](connect.md).

6. Open the script in an administrator instance of PowerShell ISE and replace the following environment variables using the values generated during the service principal provisioning described earlier. These variables are initially empty.

   ```azurepowershell-interactive
   $servicePrincipalAppId="{serviceprincipalAppID}"
   $servicePrincipalSecret="{serviceprincipalPassword}"
   $servicePrincipalTenantId="{serviceprincipalTenantId}"
   ```

7. Execute the script on each target machine.


# [Linux](#tab/linux)

Each target machine must have the [Azure CLI installed](/cli/azure/install-azure-cli). The registration script will automatically sign in to Azure with the service principal credentials if they're provided and no other user is already signed in. Use the following steps to connect SQL Server instances on multiple Linux machines.

1. Create the service principal using the ['az ad sp create-for-rbac'](/cli/azure/ad/sp#az-ad-sp-create-for-rbac) command.

   ```azurecli-interactive
   az ad sp create-for-rbac --name <your service principal name> --role <your custom role name> --scopes /subscriptions/<subscription id>
   ```

2. Download the Linux shell script from the Portal following the instructions in [Connect your SQL Server to Azure Arc](connect.md).

   > [!NOTE]
   > When you create a service principal, your account must be an Owner or User Access Administrator in the subscription that you want to use for onboarding. If you don't have sufficient permissions to create role assignments, the service principal might be created, but it won't be able to onboard machines. The instructions on how to create a custom role are provided in [prerequisites](prerequisites.md).

3. Replace the following variables in the script using the values returned by the 'az ad sp create-for-rbac' command. These variables are initially empty.

   ```console
   servicePrincipalAppId="{serviceprincipalAppID}"
   servicePrincipalSecret="{serviceprincipalPassword}"
   servicePrincipalTenant="{serviceprincipalTenant}"
   ```

1. Execute the script on each target machine

   ```console
   sudo chmod +x ./RegisterSqlServerArc.sh
   ./RegisterSqlServerArc.sh
   ```

---

## Validate successful onboarding

After you connected the SQL Server instances to Azure, go to the [Azure portal](https://aka.ms/azureportal) and view the newly created Azure Arc resources. You'll see a new `Server - Azure Arc` resource for each connected machine and a new `SQL Server - Azure Arc` resource for each connected SQL Server instance within approximately 1 minute. If these resources aren't created, it means something went wrong during the extension installation and activation process. See [Troubleshoot Azure extension for SQL Server](troubleshoot-deployment.md) for the troubleshooting options.

:::image type="content" source="./media/join-at-scale/successful-onboard.png" alt-text="Screenshot showing a successful onboard.":::



## Next steps

- Learn how to [Configure your SQL Server instance for periodic environment health check using best practices assessment](assess.md)

- Learn how to [use Microsoft Defender for Cloud to protect [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]](configure-advanced-data-security.md)
