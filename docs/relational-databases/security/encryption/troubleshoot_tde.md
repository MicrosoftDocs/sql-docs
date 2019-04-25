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
  
-   Basic troubleshooting steps.  
   
-   How to resolve the most common errors.
  
-   How to resolve Azure Insights.

-   How to reconfigure TDE with AKV after moving subscriptions

## Basic Troubleshooting Steps
In order to troubleshoot the [TDE with customer-managed TDE Protector in AKV configuration](https://docs.microsoft.com/en-us/azure/sql-database/transparent-data-encryption-byok-azure-sql#guidelines-for-configuring-tde-with-azure-key-vault), it is recommended to confirm the
following requirements:
- The logical server and AKV need to be in the same region
- The AKV needs to be up and running 
- The logical server needs to have an identity (APPID) in order to receive permissions to AKV
- The APPID needs to have current wrap, unwrap, and get permissions in AKV
- The TDE Protector selected for the logical server needs to be present and accessible in the key vault at all times


## How to resolve the most common errors
### Key vault unavailable or doesn't exist?
- Key vault accidentally deleted
- Firewall configured for Azure Key Vault without allowing access to Microsoft services
- Permissions for SQL APPID revoked
- SQL APPID accidentally deleted
### No permissions to access the key or key doesn't exist?
- Key accidentally deleted
- Permissions on key no sufficient
- Permissions on key revoked


## How to resolve Azure Insights

### Missing permissions 
Identify the key vault  
Go to Azure portal and browse to the key vault identified in the previous step 
Then go to Access policies and locate the Sql Server APPID  
If the APPID is not present, add it using Add new button. 
If the APPID is present, ensure that it has the following key permissions Get, Wrap and Unwrap 
 
 
### Missing key 
Identify the key uri added to the logical SQL server using the Get-AzSqlServerKeyVaultKey cmdlet to return the list of keys.
Identify the key vault 
Go to Azure portal and browse to the key vault identified in the previous step 
Ensure that the key identified by key uri is present 
 
 
### Missing key vault 
Identify the key uri and key vault 
Go to Azure portal and ensure that the key vault identified in the previous step is present 
If the key vault is behind a firewall, ensure the checkbox to allow Microsoft services to access the key vault is checked
 
 
 
Identify the key uri/key vault: 
Use the following command to get the key uri of a given server and then use the key uri to identify the keyvault. 
[Powershell](https://docs.microsoft.com/en-us/powershell/module/azurerm.sql/get-azurermsqlserverkeyvaultkey?view=azurermps-6.13.0) 
[Cli](https://docs.microsoft.com/en-us/cli/azure/sql/server/tde-key?view=azure-cli-latest#az-sql-server-tde-key-show) 
 
 
Ensure identity is present: 
Use the following command to get the server information and ensure that an identity has been assigned to the logical SQL server 
[Powershell](https://docs.microsoft.com/en-us/powershell/module/AzureRM.Sql/Get-AzureRmSqlServer?view=azurermps-6.13.0) 
[Cli](https://docs.microsoft.com/en-us/cli/azure/sql/server?view=azure-cli-latest#az-sql-server-show) 
 
 
Configure AD identity for the logical SQL server: 
Use the following command and use option [-AssignIdentity](https://docs.microsoft.com/en-us/powershell/module/azurerm.sql/set-azurermsqlserver?view=azurermps-6.13.0) [--assign_identity](https://docs.microsoft.com/en-us/cli/azure/sql/server?view=azure-cli-latest#az-sql-server-update) 
https://docs.microsoft.com/en-us/azure/sql-database/transparent-data-encryption-byok-azure-sql-configure?view=sql-server-2017&viewFallbackFrom=azuresqldb-current#step-1-assign-an-azure-ad-identity-to-your-server 

### How to reconfigure TDE with AKV after moving subscriptions
Configure identity for the server: 
Use the following command and use option -AssignIdentity(powershell) --assign_identity(cli) 
https://docs.microsoft.com/en-us/azure/sql-database/transparent-data-encryption-byok-azure-sql-configure?view=sql-server-2017&viewFallbackFrom=azuresqldb-current#step-1-assign-an-azure-ad-identity-to-your-server
