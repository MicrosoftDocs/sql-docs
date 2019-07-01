---
title: Common errors and resolutions for transparent data encryption with customer-managed keys in Azure Key Vault | Microsoft Docs
description: Troubleshoot transparent data encryption (TDE) with an Azure Key Vault configuration.
helpviewer_keywords: 
  - "troublshooting, tde akv"
  - "tde akv configuration, troubleshooting"
  - "tde troubleshooting"
author: aliceku
manager: craigg
ms.prod: sql
ms.technology: security
ms.reviewer: vanto
ms.topic: conceptual
ms.date: 04/26/2019
ms.author: aliceku
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# Common errors and resolutions for transparent data encryption with customer-managed keys in Azure Key Vault

[!INCLUDE[appliesto-xx-asdb-asdw-xxx-md.md](../../../includes/appliesto-xx-asdb-asdw-xxx-md.md)]
This article describes the requirements for using transparent data encryption (TDE) with customer-managed keys in Azure Key Vault and how to identify and resolve common errors.

## Requirements

To troubleshoot [TDE with a customer-managed TDE protector in Key Vault](https://docs.microsoft.com/azure/sql-database/transparent-data-encryption-byok-azure-sql#guidelines-for-configuring-tde-with-azure-key-vault), these requirements must be met:
- The logical SQL Server instance and the key vault must be in the same region.
- The logical SQL Server instance identity provided by Azure Active Directory (the AppId in Azure Key Vault) must be a tenant in the original subscription. If the server was moved to a different subscription than where it was created, the server identity (AppId) must be re-created.
- The key vault is up and running. Learn about [Azure Resource Health](https://docs.microsoft.com/azure/service-health/resource-health-overview) to check the key vault status. To sign up for notifications, read about [action groups](https://docs.microsoft.com/azure/azure-monitor/platform/action-groups).
- In a geo-disaster recovery scenario, both key vaults contain the same key material for a failover to work.
- The logical server has an Azure Active Directory (Azure AD) identity (AppId) to authenticate to the key vault.
- The AppId has access to the key vault and the Get, Wrap, and Unwrap permissions to the keys that were selected as TDE protectors.

## Common misconfigurations

Most issues that occur when you use TDE with Key Vault are caused by one of the following misconfigurations:

### The key vault is unavailable or doesn't exist

- The key vault was accidentally deleted.
- The firewall was configured for Azure Key Vault, but it didn't allow access to Microsoft services.

### No permissions to access the key vault or the key doesn't exist

- The key was accidentally deleted.
- The logical SQL Server instance AppId was accidentally deleted.
- The logical SQL Server instance was moved to a different subscription. A new AppId if the logical server is moved to a different subscription.
- Permissions granted to AppId for keys weren't sufficient (they didn't include Get, Wrap, and Unwrap)
- Permissions for the logical SQL Server instance AppId were revoked.

## Identify and resolve common errors

In this section, we list troubleshooting steps for the most common errors.

### Missing server identity

**Error Message**: _401 AzureKeyVaultNoServerIdentity - The server identity is not correctly configured on server. Please contact support._

**Detection**: Use the following cmdlet or command to ensure that an identity has been assigned to the logical SQL Server instance:

- Azure PowerShell: [Get-AzureRMSqlServer](https://docs.microsoft.com/powershell/module/AzureRM.Sql/Get-AzureRmSqlServer?view=azurermps-6.13.0) 

- Azure CLI: [`az-sql-server-show`](https://docs.microsoft.com/cli/azure/sql/server?view=azure-cli-latest#az-sql-server-show)

**Mitigation**: Use the following cmdlet or command to configure an Azure AD identity (AppId) for the logical SQL Server instance:

- Azure PowerShell: **Set-AzureRmSqlServer** with the [-AssignIdentity](https://docs.microsoft.com/powershell/module/azurerm.sql/set-azurermsqlserver?view=azurermps-6.13.0) option.

- Azure CLI: `az sql server update` with the [--assign_identity](https://docs.microsoft.com/cli/azure/sql/server?view=azure-cli-latest#az-sql-server-update) option.

In the Azure portal, go to the key vault, and then go to **Access policies**:  
 - Use the **Add New** button to add the AppId for the server created in the preceding step. 
 - Assign the following key permissions: Get, Wrap, and Unwrap 

To learn more, see [Assign an Azure AD identity to your server](https://docs.microsoft.com/azure/sql-database/transparent-data-encryption-byok-azure-sql-configure?view=sql-server-2017&viewFallbackFrom=azuresqldb-current#step-1-assign-an-azure-ad-identity-to-your-server).

> [!IMPORTANT]
> If the logical SQL Server instance has been moved to a new subscription after the initial configuration of TDE with Key Vault, repeat the step to configure the Azure AD identity to create a new AppId. Then, add the AppId to the key vault and assign the correct permissions to the key. 
>

### Missing key vault

**Error message**: _503 AzureKeyVaultConnectionFailed - The operation could not be completed on the server because attempts to connect to Azure Key Vault have failed._

**Detection**: How to identify the key URI and the key vault:

1. Use the following cmdlet or command to get the key URI of a specific logical SQL Server instance:

    - Azure PowerShell: [Get-AzureRmSqlServerKeyVaultKey](https://docs.microsoft.com/powershell/module/azurerm.sql/get-azurermsqlserverkeyvaultkey?view=azurermps-6.13.0)

    - Azure CLI: [`az-sql-server-tde-key-show`](https://docs.microsoft.com/cli/azure/sql/server/tde-key?view=azure-cli-latest#az-sql-server-tde-key-show) 

2. Use the key URI to identify the key vault

    - Azure PowerShell: You can inspect the properties of the $MyServerKeyVaultKey variable to get details about the key vault.

    - Azure CLI: Inspect the returned server encryption protector for details about the key vault.

**Mitigation**: Confirm that the key vault is available:

- Ensure that the key vault is available and that the logical SQL Server instance has access.
- If the key vault is behind a firewall, ensure that the checkbox to allow Microsoft services to access the key vault is selected.
- If the key vault has been accidentally deleted, you must complete the configuration from the start.


### Missing key

**Error message**: _404 ServerKeyNotFound - The requested server key was not found on the current subscription._ or _409 ServerKeyDoesNotExists - The server key does not exist._

**Detection**: How to identify the key URI and the key vault:

- Identify the key URI that's added to the logical SQL Server instance by using the cmdlets [Missing key vault](#missing-key-vault). Running the commands returns the list of keys.

**Mitigation**: Confirm that the TDE protector is present in Key Vault:

- Identify the key vault and go to the key vault in the Azure portal.
- Ensure that the key identified by the key URI is present.

### Missing permissions

**Error message**: _401 AzureKeyVaultMissingPermissions - The server is missing required permissions on the Azure Key Vault._

**Detection**: How to identify the key URI and key vault: 

- Identify the key vault that the logical SQL Server instance uses by using the cmdlets in [Missing key vault](#missing-key-vault).

**Mitigation**: Confirm that the logical SQL Server instance has permissions to the key vault and the correct permissions to access the key:

- In the Azure portal, go to the key vault > **Access policies**. Find the logical SQL Server instance AppId:  
  - If the AppId isn't present, add it by using the **Add New** button. 
  - If the AppId is present, ensure that it has the following key permissions: Get, Wrap, and Unwrap.

## Next steps

- Review the [Guidelines for configuring TDE with Azure Key Vault](https://docs.microsoft.com/azure/sql-database/transparent-data-encryption-byok-azure-sql#guidelines-for-configuring-tde-with-azure-key-vault).
- Learn about [Azure Resource Health](https://docs.microsoft.com/azure/service-health/resource-health-overview).
- Get a refresh of how to [assign an Azure AD identity to your server](https://docs.microsoft.com/azure/sql-database/transparent-data-encryption-byok-azure-sql-configure?view=sql-server-2017&viewFallbackFrom=azuresqldb-current#step-1-assign-an-azure-ad-identity-to-your-server).
