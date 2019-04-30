---
title: "Common errors and resolutions with Transparent Data Encryption (TDE) with customer-managed keys in Azure Key Vault (AKV) | Microsoft Docs"
description: "Troubleshoot the Transparent Data Encryption (TDE) with Azure Key Vault configuration."
helpviewer_keywords: 
  - "troublshooting, tde akv"
  - "tde akv configuration, troubleshooting"
  - "tde troubleshooting"
author: "aliceku"
manager: craigg
ms.prod: sql
ms.technology: security
ms.reviewer: vanto
ms.topic: conceptual
ms.date: "04/26/2019"
ms.author: "aliceku"
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# Transparent Data Encryption (TDE) with customer-managed keys in Azure Key Vault (AKV) Troubleshooting

[!INCLUDE[appliesto-xx-asdb-asdw-xxx-md.md](../../../includes/appliesto-xx-asdb-asdw-xxx-md.md)]
This topic provides information about the following issues:  
  
- Requirements  
- How to identify and resolve the most common errors

## Requirements
To troubleshoot the [TDE with customer-managed TDE Protector in AKV configuration](https://docs.microsoft.com/azure/sql-database/transparent-data-encryption-byok-azure-sql#guidelines-for-configuring-tde-with-azure-key-vault), let's get started with confirming the following requirements:
- The logical SQL server and the key vault need to be located in the same region.
- The logical SQL server identity provided by Azure Active Directory (APPID in Azure Key Vault) is limited to a tenant in the original subscription.  If the server was moved to another subscription, the server identity (APPID) has to be recreated.
- The key vault needs to be up and running, learn about [Azure Resource Health](https://docs.microsoft.com/azure/service-health/resource-health-overview) to check on the key vault status and [Action Groups](https://docs.microsoft.com/en-us/azure/azure-monitor/platform/action-groups) to sign up for notifications.
- In the Geo-DR scenario, both key vaults have to contain the same key material for a failover to work.
- The logical server needs to have an Azure Active Directory (AAD) identity (APPID) in order to authenticate to the key vault.
- The APPID needs to have access to the key vault and wrap, unwrap, and get permissions to the keys selected as TDE Protectors.

Most issues encountered when using TDE with AKV are due to one of the following misconfigurations:

### Key vault unavailable or doesn't exist?
- Key vault accidentally deleted
- Firewall configured for Azure Key Vault without allowing access to Microsoft services
- Permissions for SQL APPID revoked
- SQL APPID accidentally deleted
- SQL was moved to a different subscription, which requires a new APPID

### No permissions to access the key or key doesn't exist?
- Key accidentally deleted
- Permissions granted to APPID for keys not sufficient (wrap, unwrap, get)

In the next section, we are going to list the troubleshooting steps for the most common errors.


## How to identify and resolve the most common errors

## Missing server identity
Error Message: "401 AzureKeyVaultNoServerIdentity - The server identity is not correctly configured on server. Please contact support."

Detection: Use the following command to ensure that an identity has been assigned to the logical SQL server:

- [Azure PowerShell Get-AzureRMSqlServer](https://docs.microsoft.com/powershell/module/AzureRM.Sql/Get-AzureRmSqlServer?view=azurermps-6.13.0) 
- [Azure CLI az-sql-server-show](https://docs.microsoft.com/cli/azure/sql/server?view=azure-cli-latest#az-sql-server-show)

Mitigation: Configure an Azure Active Directory (Azure AD) identity (APPID) for the logical SQL server

For PowerShell: Use the command Set-AzureRmSqlServer with option [-AssignIdentity](https://docs.microsoft.com/powershell/module/azurerm.sql/set-azurermsqlserver?view=azurermps-6.13.0) 

For CLI: Use the command az sql server update with option [--assign_identity](https://docs.microsoft.com/cli/azure/sql/server?view=azure-cli-latest#az-sql-server-update) 

In the Azure portal, browse to the key vault and go to Access policies:  
 - Using the Add New button, add the APPID for the server created in the previous step. 
 - Assign the following key permissions: Get, Wrap, and Unwrap 

[Learn more](https://docs.microsoft.com/azure/sql-database/transparent-data-encryption-byok-azure-sql-configure?view=sql-server-2017&viewFallbackFrom=azuresqldb-current#step-1-assign-an-azure-ad-identity-to-your-server)

> [!IMPORTANT]
> If the logical SQL server has been moved to a new subscription after the initial configuration of TDE with AKV, the step to configure the AAD identity has to be repeated to create the new APPID.  The new APPID then needs to get added to the key vault, and the correct permissions have to be reassigned. 
>

## Missing key vault
Error message: "503 AzureKeyVaultConnectionFailed - The operation could not be completed on the server because attempts to connect to Azure Key Vault have failed"

Detection: How to identify the key uri and key vault 

Step 1: Use the following command to get the key uri of a given logical SQL server: 
-[Azure PowerShell get-azurermsqlserverkeyvaultkey](https://docs.microsoft.com/powershell/module/azurerm.sql/get-azurermsqlserverkeyvaultkey?view=azurermps-6.13.0)

-[Azure CLI az-sql-server-tde-key-show](https://docs.microsoft.com/cli/azure/sql/server/tde-key?view=azure-cli-latest#az-sql-server-tde-key-show) 

Step 2: Use the key uri to identify the key vault

PowerShell: You can inspect the properties of $MyServerKeyVaultKey to get details about the key vault

CLI: Inspect the returned server encryption protector for details about the key vault

Mitigation: Confirm the key vault is available
- Ensure the key vault is available and the logical SQL Server has access
- If the key vault is behind a firewall, ensure the checkbox to allow Microsoft services to access the key vault is checked
- If the key vault has been accidentally deleted, the configuration has to be completed from the start


## Missing key 
Error message: "404 ServerKeyNotFound - The requested server key was not found on the current subscription."
"409 ServerKeyDoesNotExists - The server key does not exist."

Detection: How to identify they key uri and key vault
- Identify the key uri added to the logical SQL server using the cmdlets from the Missing key vault section above to return the list of keys.

Mitigation: Confirm the TDE protector is present in AKV
- Identify the key vault and browse to it in the Azure portal
- Ensure that the key identified by the key uri is present

## Missing permissions 
Error message: "401 AzureKeyVaultMissingPermissions - The server is missing required permissions on the Azure Key Vault."

Detection: How to identify the key uri and key vault
- Identify the key vault used by the logical SQL server using the cmdlets from the missing key vault section above.

Mitigation: Confirm the logical sql server has permissions to the key vault and the correct permissions to access the key
- In the Azure portal, browse to the key vault, go to Access policies, and locate the Sql Server APPID:  
  - If the APPID is not present, add it using the Add New button. 
  - If the APPID is present, ensure that it has the following key permissions: Get, Wrap, and Unwrap.
