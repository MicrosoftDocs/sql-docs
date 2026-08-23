---
title: Managed identity overview
description: Learn about managed identity with SQL Server 2025 enabled by Azure Arc.
author: PratimDasgupta
ms.author: prdasgu
ms.reviewer: randolphwest, mathoma, vanto
ms.date: 11/18/2025
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
# CustomerIntent: As a database engineer I need to understand what managed identity is and when to use it with SQL Server 2025.
monikerRange: ">=sql-server-ver17"
---
# Managed identity for SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025.md)]

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] includes managed identity support for SQL Server on Windows. Use a managed identity to interact with resources in Azure by using Microsoft Entra authentication.

## Overview

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] introduces support for [Microsoft Entra managed identities](/entra/identity/managed-identities-azure-resources/overview). Use managed identities to authenticate to Azure services without needing to manage credentials. Managed identities are automatically managed by Azure and can be used to authenticate to any service that supports Microsoft Entra authentication. With [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], you can use managed identities both to authenticate inbound connections, and also to authenticate outbound connections to Azure services.

When you connect your SQL Server instance to Azure Arc, a system-assigned managed identity is automatically created for the SQL Server hostname. After the managed identity is created, you must associate the identity with the SQL Server instance and the Microsoft Entra tenant ID by updating the registry.

For step-by-step setup instructions, see [Set up managed identity for SQL Server enabled by Azure Arc](microsoft-entra-authentication-with-managed-identity.md).

When using managed identity with SQL Server enabled by Azure Arc, consider the following:

- The managed identity is assigned at the Azure Arc server level.
- Only system-assigned managed identities are supported.
- SQL Server uses this Azure Arc server level managed identity as the **primary managed identity**.
- SQL Server can use this primary managed identity in either `inbound` and/or `outbound` connections.
  - `Inbound connections` are logins and users connecting to SQL Server. Inbound connections can also be achieved by using [App registration](entra-authentication-setup-tutorial.md), starting in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].
  - `Outbound connections` are SQL Server connections to Azure resources, like backup to URL, or connecting to Azure Key Vault.
- App Registration **can't** enable a SQL Server to make outbound connections. Outbound connections need a primary managed identity assigned to the SQL Server.
- For SQL Server 2025 and later, we recommend that you use managed identity based Microsoft Entra setup, as detailed in this article. Alternatively, you can configure an [app registration for SQL Server 2025](../../relational-databases/security/authentication-access/microsoft-entra-authentication-sql-server-enable-without-arc.md).

## Prerequisites

Before you can use a managed identity with SQL Server enabled by Azure Arc, ensure that you meet the following prerequisites:

- [Connect your SQL Server to Azure Arc](connect.md).
- The latest version of the [Azure Extension for SQL Server](release-notes.md).

For detailed setup instructions, see [Set up managed identity for SQL Server enabled by Azure Arc](microsoft-entra-authentication-with-managed-identity.md).

## Limitations

Consider the following limitations when using a managed identity with SQL Server 2025:

- The managed identity setup for Microsoft Entra authentication is only supported with Azure Arc-enabled SQL Server 2025, running on Windows Server.
- SQL Server needs access to Azure public cloud to use [Microsoft Entra authentication](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md).
- Using Microsoft Entra authentication with failover cluster instances isn't supported.
- Once Microsoft Entra authentication is enabled, disabling isn't advisable. Disabling Microsoft Entra authentication forcefully by deleting registry entries can result in unpredictable behavior with SQL Server 2025.
- Authenticating to SQL Server on Arc machines through Microsoft Entra authentication using the [FIDO2 method](/azure/active-directory/authentication/howto-authentication-passwordless-faqs) isn't currently supported.
- [OPENROWSET BULK](../../t-sql/functions/openrowset-bulk-transact-sql.md) operations can also read the tokens folder `C:\ProgramData\AzureConnectedMachineAgent\Tokens\`. The `BULK` option requires either `ADMINISTER BULK OPERATIONS` or `ADMINISTER DATABASE BULK OPERATIONS` permissions. These permissions should be treated as equivalent to **[sysadmin](../../relational-databases/security/authentication-access/server-level-roles.md)**.

## Related content

- [Set up managed identity and Microsoft Entra authentication for SQL Server enabled by Azure Arc](microsoft-entra-authentication-with-managed-identity.md)
- [Microsoft Entra authentication for SQL Server](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md)
- [What is managed identities for Azure resources?](/entra/identity/managed-identities-azure-resources/overview)
- [Enable Microsoft Entra authentication for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm)
