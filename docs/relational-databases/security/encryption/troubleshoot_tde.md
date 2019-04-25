---
title: "Troubleshoot Transparent Data Encryption (TDE) with customer-managed keys in Azure Key Vault (AKV) | Microsoft Docs"
description: "Troubleshoot the Transparent Data Encryption (TDE) with Azure Key Vault configuration."
helpviewer_keywords: 
  - "troublshooting, tde akv"
  - "tde akv configuration, troubleshooting"
  - "tde troubleshooting"
documentationcenter:
author: "aliceku"
manager: craigg
editor:
ms.prod: 
ms.reviewer: ""
ms.suite: sql
ms.prod_service: sql-database, sql-data-warehouse
ms.service: "sql-database"
ms.tgt_pltfrm:
ms.devlang: 
ms.topic: conceptual
ms.date: "25/04/2019"
ms.author: "aliceku"
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# Transparent Data Encryption (TDE) with customer-managed keys in Azure Key Vault (AKV) Troubleshooting
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic provides information about the following issues:  
  
-   Troubleshooting Overview  
   
-   How to resolve the most common errors.
  


## Troubleshooting Overview
In order to troubleshoot the [TDE with customer-managed TDE Protector in AKV configuration](https://docs.microsoft.com/azure/sql-database/transparent-data-encryption-byok-azure-sql#guidelines-for-configuring-tde-with-azure-key-vault), it is recommended to confirm the
following requirements:
- The logical SQL server and the key vault need to be in the same region
- The key vault needs to be up and running, learn about [Azure Resource Health](https://docs.microsoft.com/azure/service-health/resource-health-overview) to check on the key vault status 
- The logical server needs to have an AAD identity (APPID) in order to authenticate to the key vault
- The APPID needs to have access to the key vault and wrap, unwrap, and get permissions to the keys selected as TDE Protectors

Most issues encountered are due to one of the following:

### Key vault unavailable or doesn't exist?
- Key vault unavailable
- Key vault accidentally deleted
- Firewall configured for Azure Key Vault without allowing access to Microsoft services
- Permissions for SQL APPID revoked
- SQL APPID accidentally deleted

### No permissions to access the key or key doesn't exist?
- Key accidentally deleted
- Permissions on key not sufficient

In the next section we are going to list the most common errors and troubleshooting steps.


## How to identify and resolve the most common errors

Identify the key uri/key vault: 
Use the following command to get the key uri of a given logical SQL server and then use the key uri to identify the keyvault. 
[Powershell](https://docs.microsoft.com/powershell/module/azurerm.sql/get-azurermsqlserverkeyvaultkey?view=azurermps-6.13.0) 
[Cli](https://docs.microsoft.com/cli/azure/sql/server/tde-key?view=azure-cli-latest#az-sql-server-tde-key-show) 
 
 
### Missing permissions 
"401 AzureKeyVaultMissingPermissions - The server is missing required permissions on the Azure Key Vault."
Identify the key vault  
In the Azure portal, browse to the key vault, go to Access policies and locate the Sql Server APPID:  
- If the APPID is not present, add it using Add new button. 
- If the APPID is present, ensure that it has the following key permissions: Get, Wrap and Unwrap 
 
### Missing key 
"404 ServerKeyNotFound - The requested server key was not found on the current subscription."
"409 ServerKeyDoesNotExists - The server key does not exist."
Identify the key uri added to the logical SQL server using the Get-AzSqlServerKeyVaultKey cmdlet to return the list of keys.
Identify the key vault 
In the Azure portal, browse to the key vault
Ensure that the key identified by key uri is present 
 
### Missing key vault
"503 AzureKeyVaultConnectionFailed - The operation could not be completed on the server because attempts to connect to Azure Key Vault have failed"
Identify the key uri and key vault 
Go to Azure portal and ensure that the key vault identified in the previous step is present 
If the key vault is behind a firewall, ensure the checkbox to allow Microsoft services to access the key vault is checked

### Missing Identity
"401 AzureKeyVaultNoServerIdentity - The server identity is not correctly configured on server. Please contact support."
Use the following command to ensure that an identity has been assigned to the logical SQL server: 
[Powershell](https://docs.microsoft.com/powershell/module/AzureRM.Sql/Get-AzureRmSqlServer?view=azurermps-6.13.0) 
[Cli](https://docs.microsoft.com/cli/azure/sql/server?view=azure-cli-latest#az-sql-server-show)

How to configure an AD identity for the logical SQL server:

Use the following command and use option [-AssignIdentity](https://docs.microsoft.com/powershell/module/azurerm.sql/set-azurermsqlserver?view=azurermps-6.13.0) [--assign_identity](https://docs.microsoft.com/cli/azure/sql/server?view=azure-cli-latest#az-sql-server-update) 
[Learn more](https://docs.microsoft.com/azure/sql-database/transparent-data-encryption-byok-azure-sql-configure?view=sql-server-2017&viewFallbackFrom=azuresqldb-current#step-1-assign-an-azure-ad-identity-to-your-server)

> [!IMPORTANT]
> If the logical SQL server has been moved to a new subscription after the initial configuration of TDE with AKV, the step to configure the AAD identity has to be repeated to create the new APPID.  The new APPID then needs to get registered with they key vault and the correct permissions have to be reassigned. 
>
