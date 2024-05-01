---
title: "Microsoft Entra authentication for SQL Server overview"
description: Learn about Microsoft Entra authentication support for SQL Server
author: GithubMirek
ms.author: mireks
ms.reviewer: vanto, randolphwest
ms.date: 02/21/2024
ms.service: sql
ms.subservice: security
ms.topic: conceptual
monikerRange: ">=sql-server-ver15||>= sql-server-linux-ver16"
---

# Microsoft Entra authentication for SQL Server

[!INCLUDE [SQL Server 2022](../../../includes/applies-to-version/sqlserver2022.md)]

[!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] introduces support for [authentication](/azure/active-directory/authentication/overview-authentication) with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)), on both Windows and Linux on-premises, and [SQL Server on Azure Windows VMs](/azure/azure-sql/virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm).

## Overview

You can now connect to SQL Server using the following Microsoft Entra authentication methods:

- Default authentication
- Username and password
- Integrated
- Universal with multifactor authentication
- Service principal
- Managed identity
- Access token

The existing authentication modes, [SQL authentication and Windows authentication](../choose-an-authentication-mode.md) remain unchanged.

Microsoft Entra ID is Azure's cloud-based identity and access management service. Microsoft Entra ID is conceptually similar to Active Directory, providing a centralized repository for managing access to your organization's resources. Identities are objects in Microsoft Entra ID that represent users, groups, or applications. They can be assigned permissions through role-based access control and be used for authentication to Azure resources. Microsoft Entra authentication is supported for:

- Azure SQL Database
- Azure SQL Managed Instance
- SQL Server on Windows Azure VMs
- Azure Synapse Analytics
- SQL Server

For more information, see [Use Microsoft Entra authentication with Azure SQL](/azure/azure-sql/database/authentication-aad-overview) and [Configure and manage Microsoft Entra authentication with Azure SQL](/azure/azure-sql/database/authentication-aad-configure).

If your Windows Server Active Directory is [federated](/entra/identity/hybrid/connect/whatis-fed) with Microsoft Entra ID, users can authenticate with SQL Server using their Windows credentials, either as Windows logins or Microsoft Entra logins. While Microsoft Entra ID doesn't support all AD features supported by Windows Server Active Directory, such as service accounts or complex networking forest architecture. There are other capabilities of Microsoft Entra ID such as multifactor authentication that isn't available with Active Directory. [Compare Microsoft Entra ID with Active Directory](/entra/fundamentals/compare) to learn more.

## <a id="connect-sql-server-to-azure-with-azure-ad"></a> Connect SQL Server to Azure with Microsoft Entra ID

For SQL Server to communicate with Azure, both SQL Server and the Windows or Linux host it runs on must be registered with [Azure Arc](../../../sql-server/azure-arc/overview.md). To enable SQL Server's communication with Azure, you need to install the [Azure Arc Agent](/azure/azure-arc/servers/overview) and [Azure extension for SQL Server](../../../sql-server/azure-arc/overview.md).

To get started, see [Connect your SQL Server to Azure Arc](../../../sql-server/azure-arc/connect.md).

> [!Note]
> If you are running SQL Server on an Azure VM, you don't need to register the VM with Azure Arc, you must instead register the VM with the [SQL IaaS Agent extension](/azure/azure-sql/virtual-machines/windows/sql-agent-extension-manually-register-single-vm). Once the VM is registered, see [Enable Azure AD authentication for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm) for more details.

## <a id="azure-active-directory-default"></a> Default authentication

The default authentication option with Microsoft Entra ID that enables authentication through password-less and non-interactive mechanisms including managed identities, Visual Studio, Visual Studio Code, the Azure CLI, and more.

## <a id="azure-active-directory-password"></a> Username and password

Allows specifying the username and password to the client and driver. The username and password method is commonly disabled on many tenants for security reasons. Although the connections are encrypted, it's best practice/recommended to avoid the use of username and password when possible as it requires sending passwords over the network.

## <a id="azure-active-directory-integrated"></a> Integrated

With [Integrated Windows authentication (IWA)](/azure/active-directory/develop/msal-authentication-flows#integrated-windows-authentication-iwa), Microsoft Entra ID provides a solution to organizations with both on-premises and cloud infrastructures. On-premises Active Directory domains can be synchronized with Microsoft Entra ID through [federation](/azure/active-directory/hybrid/connect/whatis-fed), allowing management and access control to be handled within Microsoft Entra ID, while user authentication remains on-premises. With IWA, the user's Windows credentials are authenticated against Active Directory, and upon success the user's authentication token from Microsoft Entra ID is returned to SQL.

### <a id="azure-active-directory-universal-with-multi-factor-authentication"></a> Universal with multifactor authentication

This is the standard interactive method with multifactor authentication option for Microsoft Entra accounts. This works in most scenarios.

## <a id="azure-active-directory-service-principal"></a> Service principal

A service principal is an identity that can be created for use with automated tools, jobs and applications. With the service principal authentication method, you can connect to your SQL Server instance using the client ID and secret of a service principal identity.

## <a id="azure-active-directory-managed-identity"></a> Managed identity

Managed identities are special forms of service principals. There are two types of managed identities: system-assigned and user-assigned. System-assigned managed identities are enabled directly on an Azure resource, whereas user-assigned managed identities are a standalone resource that can be assigned to one or more Azure resources.

> [!NOTE]
> In order to use a managed identity to connect to a SQL resource through GUI clients such as SSMS and ADS, the machine running the client application must have a Microsoft Entra client running with the identity's certificate stored in it. This is most commonly achieved through an Azure VM, as the identity can be easily assigned to the machine through the VM's portal pane.

For tools that use Azure identity libraries such as SQL Server Management Studio (SSMS), when connecting with a managed identity you need to use the GUID for the login such as `abcd1234-abcd-1234-abcd-abcd1234abcd1234`. For more information, see ([ManagedIdentityCredential](/dotnet/api/azure.identity.managedidentitycredential.-ctor). If you incorrectly pass the username, an error occurs such as:

```output
ManagedIdentityCredential authentication unavailable. The requested identity has not been assigned to this resource.
Status: 400 (Bad Request)
Content:
{"error":"invalid_request","error_description":"Identity not found"}
```

### <a id="azure-active-directory-access-token"></a> Access token

Some non-GUI clients such as [Invoke-sqlcmd](/powershell/module/sqlserver/invoke-sqlcmd) allow providing an access token. The scope or audience of the access token must be `https://database.windows.net/`.

## Remarks

- Only [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] on-premises with a supported Windows or Linux operating system, or [SQL Server 2022 on Windows Azure VMs](/azure/azure-sql/virtual-machines/windows/security-considerations-best-practices#azure-ad-authentication-preview), is supported for Microsoft Entra authentication.
- To connect SQL Server to Azure Arc, the Microsoft Entra account needs the following permissions:
  - Member of the *Azure Connected Machine Onboarding* group or *Contributor* role in the resource group.
  - Member of the *Azure Connected Machine Resource Administrator* role in the resource group.
  - Member of the *Reader* role in the resource group.

## Related content

- [Microsoft Entra authentication](/azure/active-directory/authentication/overview-authentication)
- [Linked server for SQL Server with Microsoft Entra authentication](azure-ad-authentication-sql-server-linked-server.md)
- [Tutorial: Using automation to set up the Microsoft Entra admin for SQL Server](azure-ad-authentication-sql-server-automation-setup-tutorial.md)
- [Tutorial: Set up Microsoft Entra authentication for SQL Server](azure-ad-authentication-sql-server-setup-tutorial.md)
- [Rotate certificates](../../../sql-server/azure-arc/rotate-certificates.md)
- [Connect your SQL Server to Azure Arc](../../../sql-server/azure-arc/connect.md)
