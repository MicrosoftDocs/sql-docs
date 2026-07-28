---
title: Configure Minimal TLS Version - SQL Managed Instance
description: "Learn how to configure minimal TLS version for a managed instance"
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: mathoma, vanto
ms.date: 07/17/2025
ms.service: azure-sql-managed-instance
ms.subservice: security
ms.topic: how-to
ms.custom:
  - devx-track-azurecli
ms.devlang: azurecli
---
# Configure minimal TLS version in Azure SQL Managed Instance

> [!IMPORTANT]  
> **Retirement changes**
>
> Azure has announced that support for older TLS versions (TLS 1.0, and 1.1) ends August 31, 2025. For more information, see [TLS 1.0 and 1.1 deprecation](https://azure.microsoft.com/updates/azure-support-tls-will-end-by-31-october-2024-2/).
> Starting November 2024, you'll no longer be able to set the minimal TLS version for Azure SQL Managed Instance client connections below TLS 1.2.

The minimum [Transport Layer Security (TLS)](/troubleshoot/sql/database-engine/connect/tls-1-2-support-microsoft-sql-server) version setting allows customers to control the version of TLS used by their Azure SQL Managed Instance.

Setting **Minimum TLS version** to 1.2 is currently enforced for SQL Managed Instance. Setting a minimum TLS version ensures that subsequent, newer TLS versions are supported. Only connections using TLS 1.2 or above are accepted.

For more information, see [TLS considerations for SQL Database connectivity](../database/connect-query-content-reference-guide.md#tls-considerations-for-database-connectivity).

After setting the Minimum TLS version, login attempts from clients that are using a TLS version lower than the minimum TLS version of the server will fail with following error:

```output
Error 47072
Login failed with invalid TLS version
```

> [!NOTE]
> - When you configure a minimum TLS version, that minimum version is enforced at the application layer. Tools that attempt to determine TLS support at the protocol layer might return TLS versions in addition to the minimum required version when run directly against the managed instance endpoint.
> - TLS 1.0 and 1.1 is [retired](#upcoming-tls-10-and-11-retirement-changes-faq) and no longer available. 

## Set minimal TLS version via PowerShell

[!INCLUDE [updated-for-az](../includes/updated-for-az.md)]

> [!IMPORTANT]
> The PowerShell Azure Resource Manager (AzureRM) module was deprecated on February 29, 2024. All future development should use the Az.Sql module. Users are advised to migrate from AzureRM to the Az PowerShell module to ensure continued support and updates. The AzureRM module is no longer maintained or supported. The arguments for the commands in the Az PowerShell module and in the AzureRM modules are substantially identical. For more about their compatibility, see [Introducing the new Az PowerShell module](/powershell/azure/new-azureps-module-az).

The following script requires the [Azure PowerShell module](/powershell/azure/install-az-ps).

The following PowerShell script shows how to `Get` and `Set` the **Minimal TLS Version** property at the instance level:

```powershell
#Get the Minimal TLS Version property
(Get-AzSqlInstance -Name sql-instance-name -ResourceGroupName resource-group).MinimalTlsVersion

# Update Minimal TLS Version Property
Set-AzSqlInstance -Name sql-instance-name -ResourceGroupName resource-group -MinimalTlsVersion "1.2"
```

## Set Minimal TLS Version via Azure CLI

> [!IMPORTANT]
> All scripts in this section require [Azure CLI](/cli/azure/install-azure-cli).

### Azure CLI in a bash shell

The following CLI script shows how to change the **Minimal TLS Version** setting in a bash shell:

```azurecli-interactive
# Get current setting for Minimal TLS Version
az sql mi show -n sql-instance-name -g resource-group --query "minimalTlsVersion"

# Update setting for Minimal TLS Version
az sql mi update -n sql-instance-name -g resource-group --set minimalTlsVersion="1.2"
```

## Upcoming TLS 1.0 and 1.1 retirement changes FAQ

[!INCLUDE [tls-deprecation](../includes/tls-deprecation.md)]

## Related content

- [Connectivity architecture for Azure SQL Managed Instance](connectivity-architecture-overview.md)
- [Modifiable configuration reference for Azure SQL Managed Instance](modifiable-configuration-reference.md)
