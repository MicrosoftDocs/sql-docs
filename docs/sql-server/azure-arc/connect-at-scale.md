---
title: Connect SQL Servers on Azure Arc-enabled servers at scale
titleSuffix:
description: In this article, you learn different ways of connecting SQL Server instances to Azure Arc at scale.
author: anosov1960
ms.author: sashan 
ms.reviewer: mikeray
ms.date: 07/30/2021
ms.topic: conceptual
ms.prod: sql
---

# Connect SQL Server instances to Azure Arc at scale

This article describes how to connect multiple instances of SQL Server to Azure Arc as a single task. The easiest  way to do that is by using Azure policy. Alternatively, you can connect multiple SQL Server instances installed on multiple Windows or Linux machines to Azure Arc using a script.  

## Connecting at-scale using Azure policy

You can automatically register the SQL Server instances on multiple machines using a built-in Azure policy _Configure Arc-enabled machines running SQL Server to have SQL Server extension installed_. This policy is disabled by default. If you assign this policy to a scope of your choice, it will install the SQL Server extension (*WindowsAgent.SqlServer*) on all Azure Arc connected servers, and will assign `Azure Connected SQL Server Onboarding` role to Arc managed identity in the specified scope. Once installed, the extension will register the SQL Server instances on the machine with Azure. After that, the extension will run continuously to detect changes of the SQL Server configuration and synchronize them with Azure. For example, if a new SQL Server instance is installed on the machine, the extension automatically registers it with Azure. See [Azure Policy documentation](/azure/governance/policy) for instructions how to assign an Azure policy using Azure portal or an API of your choice.

> [!IMPORTANT]
>The __SQL Server - Azure Arc__ resources for the SQL Server instances will be created in the same region and the resource group as the corresponding __Server - Azure Arc__ resource. Because the SQL Serve extension synchronizes with Azure once an hour, it may take up to one hour before these resources are created.  

## Connecting multiple SQL Server instances using script

You can connect multiple SQL Server instances installed on multiple Windows or Linux machines to Azure Arc using the same [script your generated for a single machine](connect.md). The script will connect each machine and all installed SQL Server instances on it to Azure Arc.

### Use Azure Active Directory service principal

For the best experience, use an Azure Active Directory [service principal](/azure/active-directory/develop/app-objects-and-service-principals). A service principal is a special limited management identity that is granted only the minimum permission necessary to connect machines to Azure and to create the Azure resources for Azure Arc-enabled server and Azure Arc-enabled SQL Server. The service principal is safer than using a higher privileged account like a Tenant Administrator, and follows access control security best practices.  

The installation methods to install and configure the Connected Machine agent requires that the automated method you use has administrator permissions on the machines. On Linux, use the root account. Windows, use a member of the Local Administrators group.

Before you get started, be sure to review the [prerequisites](overview.md#prerequisites) and make sure that you have created a custom role that meets the required permissions.

### Connect multiple instances

# [Windows](#tab/windows)

Each machine must have [Azure PowerShell](/powershell/azure/install-az-ps) installed.

1. Create the service principal. Use the [`New-AzADServicePrincipal`](/powershell/module/az.resources/new-azadserviceprincipal) cmdlet. Make sure to store the output in a variable. Otherwise, you will not be able to retrieve the password needed later.

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

2. Give the service principle permissions to access Microsoft Graph:
    ```azurepowershell-interactive
    $sp = New-AzADServicePrincipal -DisplayName "Arc-for-servers" -Role <your custom role>
    ```
. 
1
1. Retrieve the password stored in the `$sp` variable:

   ```azurepowershell-interactive
   $credential = New-Object pscredential -ArgumentList "temp", $sp.Secret
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

7. Execute the script on each target machine

# [Linux](#tab/linux)

Each target machine must have the [Azure CLI installed](/cli/azure/install-azure-cli). The registration script will automatically sign in to Azure with the service principal credentials if they're provided and no other user is already signed in. Use the following steps to connect SQL Server instances on multiple Linux machines.

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

3. Replace the following variables in the script using the values returned by the 'az ad sp create-for-rbac' command. These variables are initially empty.

   ```console
   servicePrincipalAppId="{serviceprincipalAppID}"
   servicePrincipalSecret="{serviceprincipalPassword}"
   servicePrincipalTenant="{serviceprincipalTenant}"
   ```

3. Execute the script on each target machines
 
   ```console
   sudo chmod +x ./RegisterSqlServerArc.sh
   ./RegisterSqlServerArc.sh
   ```

---

## Validate successful onboarding

After you connected the SQL Server instances to Azure, go to the [Azure portal](https://aka.ms/azureportal) and view the newly created Azure Arc resources. You will see a new __Server - Azure Arc__ for each connected machine and a new __SQL Server - Azure Arc__ resource for each connected SQL Server instance within approximately 1 minute. If these resource are not created, it means something went wrong during the extension installation and activation process. See [Troubleshoot SQL Server extension](./connect-at-scale.md#troubleshoot-sql-server-extension) for the troubleshooting options.

![A successful onboard](./media/join-at-scale/successful-onboard.png)

## Troubleshoot SQL Server extension

Before you start, note the logs location. The extension log is created in this folder:
`C:\ProgramData\GuestConfig\extension_logs\Microsoft.AzureData.WindowsAgent.SqlServer\ExtensionLog_0.log`

The deployer logs is created in this folder:
`C:\ProgramData\GuestConfig\extension_logs\Microsoft.AzureData.WindowsAgent.SqlServer\1.1.0.0\deployer.log`

The failure to create the __SQL Server - Azure Arc__ resource could be caused by several issues.  

### Extension installation failed

Go to the connected server and check the deployer log. You should see the below messages. 

`[07/14/2021 18:56:45 UTC] [INFO]          Status of service 'SqlServerExtension' before attempting start: Stopped`
`[07/14/2021 18:56:45 UTC] [INFO]          Status of service 'SqlServerExtension' after attempting start: Stopped`

If you cannot see it means the extension did not install properly. Try the following steps.

1. Check event logs to see if anything preventing installation. Try installing SqlServerExtension.msi from the following folder `C:\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SqlServer{version}`. The installation UI may provide the error details.

2. Close services app, server manager and retry one of the [connection methods](./connect.md) to install the extension, and see if that helps.

### Extension installed but did not start

Check the log files for any application errors.

### The extension SQL Server is not present on the machine

Check if SQL server installed.

### Server - Azure Arc ARM resource was manually deleted

Check the extension log for the following record:
 `[7/14/2021 9:36:18 PM UTC] [ERROR]   [UploadServiceProvider]      [ExtensionHandlerStatusQueryError] ArcSqlInstancesRequest request is null, not sending data to RP.`

This means the machine is no longer recognized as a connected server. [Onboard the server to Azure Arc](/azure/azure-arc/servers/onboard-portal) and retry one of the [connection methods](./connect.md) to install the extension.

### Server managed identity has insufficient permissions

Check the extension log for the following record:
`[7/14/2021 5:16:14 PM UTC] [INFO] [UploadServiceProvider] [ExtensionHandlerArcUploadServicesNotifications] [AzureUpload] Arc post request failed with error: Forbidden message: {"ErrorDescription":{"ErrorCode":6,"Message":"The user has no access to the provided Azure resource."},"ResponseUrl":null}`

Make sure the machine's managed identity has been assigned the _Azure Connected SQL Server Onboarding_ role. See [Initiate the connection from Azure](./connect.md#initiate-the-connection-from-azure) for the role assignment instructions.

### The user did not migrate the __SQL Server - Azure Arc__ resource to the new resource provider

Check the extension log for the following record:
`[7/14/2021 5:35:04 PM UTC] [INFO] [UploadServiceProvider] [ExtensionHandlerArcUploadServicesNotifications] [AzureUpload] Arc for Sql Server upload response status: InternalServerError.`

Make sure to migrate the __SQL Server - Azure Arc__ resource to `Microsoft.AzureArcData` following [these steps](.\release-notes.md#breaking-change-1).

## Next steps

- Learn how to manage your machine using [Azure Policy](/azure/governance/policy/overview), for such things as VM [guest configuration](/azure/governance/policy/concepts/guest-configuration), verifying the machine is reporting to the expected Log Analytics workspace, enable monitoring with [Azure Monitor with VMs](/azure/azure-monitor/insights/vminsights-enable-policy), and much more.

- Learn more about the [Log Analytics agent](/azure/azure-monitor/platform/log-analytics-agent). The Log Analytics agent for Windows and Linux is required when you want to proactively monitor the OS and workloads running on the machine, manage it using Automation runbooks or solutions like Update Management, or use other Azure services like [Azure Security Center](/azure/security-center/security-center-intro).

- Learn how to [Configure your SQL Server instance for periodic environment health check using on-demand SQL assessment](assess.md)

- Learn how to [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
