---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 06/02/2026
ms.topic: include
ms.custom:
  - ignite-2023
---
To determine the version of the current extension release, review the [release notes](../release-notes.md).

To check the version of your extension, use the following PowerShell command:

```PowerShell
azcmagent version
```

To simplify extension upgrades, be sure to enable [automatic updates](../update.md). You can also manually upgrade the extension by using the Azure portal, PowerShell and the Azure CLI. 

#### [Azure portal](#tab/azure-portal)

To upgrade the extension in the Azure portal, follow these steps:
1. In the Azure portal, go to [Machines - Azure Arc](https://portal.azure.com/#view/Microsoft_Azure_ArcCenterUX/ArcCenterMenuBlade/~/sqlServerInstances).
1. Select the name of the machine where SQL Server is installed to open the **Overview** pane for your server.
1. Under **Settings**, select **Extensions**.
1. Check the box for the `WindowsAgent.SqlServer` extension and then select **Update** from the navigation menu. 

   :::image type="content" source="media/manage-extension/update-extension.png" alt-text="Screenshot of the Extension pane for the Machine - Azure Arc pane in the Azure portal, with update highlighted.":::

1. Select **Yes** on the **Update extension** confirmation dialog box to complete the upgrade.


#### [PowerShell](#tab/powershell)

To upgrade the extension by using PowerShell, first install the `Az.ConnectedMachine` module with the following command (if you haven't already):

```PowerShell
Install-Module -Name Az.ConnectedMachine
```

Then, use the following command to upgrade the extension:

```PowerShell
# Variables 
$Subscription_Id = "<SubscriptionId>" 
$ResourceGroup = "<ResourceGroup>" 
$MachineName = "<MachineName>" 
$ExtensionName = "WindowsAgent.SqlServer" 
$Publisher = "Microsoft.AzureData" 
$TargetVersion = "<LatestVersion>"  # Example: 1.1.3211.337 

# Upgrade the extension 
Set-AzContext -Subscription $SubscriptionId 

$params = @{
  ResourceGroupName = $ResourceGroup
  MachineName = $MachineName
  Name = $ExtensionName
  Publisher = $Publisher
  Type = $ExtensionName
  TypeHandlerVersion = $TargetVersion
}

Update-AzConnectedMachineExtension @params
```


#### [Azure CLI](#tab/azure-cli)

To upgrade the extension by using the Azure CLI, first install the `connectedmachine` extension with the following command (if you haven't already):

```azurecli
az extension add --name connectedmachine 
```

Then, use the following command to upgrade the extension:

```azurecli
# Variables 
Subscription_Id="<SubscriptionId>" 
Resource_Group="<ResourceGroup>" 
Machine_Name="<ArcEnabledServerName>" 
Extension_Name="WindowsAgent.SqlServer" 
Publisher="Microsoft.AzureData" 
Target_Version="<LatestVersion>"  # Example: 1.1.3211.337 

# Upgrade the extension 

az account set --subscription $Subscription_Id 
az connectedmachine extension update \ 
    --resource-group $Resource_Group \ 
    --machine-name $Machine_Name \ 
    --name $Extension_Name \ 
    --publisher $Publisher \ 
    --type $Extension_Name \ 
    --type-handler-version $Target_Version 
```

---

For more information about upgrading the Azure extension for SQL Server, see [Upgrade extension](/azure/azure-arc/servers/manage-vm-extensions-cli#upgrade-extensions).


