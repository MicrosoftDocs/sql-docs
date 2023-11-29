---
title: "Microsoft Entra authentication for SQL Server overview"
description: Learn about Microsoft Entra authentication support for SQL Server
author: GithubMirek
ms.author: mireks
ms.reviewer: vanto, randolphwest
ms.date: 10/20/2022
ms.service: sql
ms.subservice: security
ms.topic: conceptual
monikerRange: ">=sql-server-ver15||>= sql-server-linux-ver16"
---

# Microsoft Entra authentication for SQL Server

[!INCLUDE [SQL Server 2022](../../../includes/applies-to-version/sqlserver2022.md)]

[!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] introduces support for [authentication](/azure/active-directory/authentication/overview-authentication) with Microsoft Entra ID ([formerly Azure Active Directory](/azure/active-directory/fundamentals/new-name)), on both Windows and Linux on-premises, and [SQL Server on Windows Azure VMs](/azure/azure-sql/virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm).

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

Microsoft Entra ID is Azure's cloud-based identity and access management service. Microsoft Entra ID is conceptually similar to Active Directory, providing a centralized repository for managing access to your organization's resources. Identities are objects in Microsoft Entra ID that represent users, groups, or applications. They can be assigned permissions through role-based access control and be used for authentication to Azure resources. Microsoft Entra authentication is supported for Azure SQL Database, Azure SQL Managed Instance, SQL Server on Windows Azure VMs, Azure Synapse Analytics, and SQL Server. For more information, see [Use Microsoft Entra authentication with Azure SQL](/azure/azure-sql/database/authentication-aad-overview) and [Configure and manage Microsoft Entra authentication with Azure SQL](/azure/azure-sql/database/authentication-aad-configure).

If your Windows Server Active Directory is [federated](/azure/active-directory/hybrid/connect/whatis-fed) with Microsoft Entra ID, users can authenticate with SQL Server using their Windows credentials, either as Windows logins or Microsoft Entra logins. While Microsoft Entra ID doesn't support all AD features supported by Windows Server Active Directory, such as service accounts or complex networking forest architecture, there are other capabilities of Microsoft Entra ID, such as multifactor authentication, that aren't available with Active Directory. [Compare Microsoft Entra ID with Active Directory](/azure/active-directory/fundamentals/compare) to learn more. 

<a name='connect-sql-server-to-azure-with-azure-ad'></a>

## Connect SQL Server to Azure with Microsoft Entra ID

For SQL Server to communicate with Azure, both SQL Server and the Windows or Linux host it runs on must be registered with [Azure Arc](../../../sql-server/azure-arc/overview.md). To do this, you'll need to install the [Azure Arc Agent](/azure/azure-arc/servers/overview) and [Azure extension for SQL Server](../../../sql-server/azure-arc/overview.md). This will facilitate SQL Server's  communication with Azure.

To get started, see [Connect your SQL Server to Azure Arc](../../../sql-server/azure-arc/connect.md).

> [!Note]
> If you are running SQL Server on an Azure VM, you don't need to register the VM with Azure Arc, you must instead register the VM with the [SQL IaaS Agent extension](/azure/azure-sql/virtual-machines/windows/sql-agent-extension-manually-register-single-vm). Once the VM is registered, see [Enable Azure AD authentication for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm) for more details.

<a name='azure-active-directory-default'></a>

## Default authentication

The default authentication option with Microsoft Entra ID enables authentication that's performed through password-less and non-interactive mechanisms including managed identities, Visual Studio, Visual Studio Code, the Azure CLI, and more. 

<a name='azure-active-directory-password'></a>

## Username and password

Allows specifying the username and password to the client and driver, but this is disabled on many tenants for security reasons. When possible, we recommend avoiding this as it requires sending passwords over the network. These connections are encrypted, but it's best practice to never send them in the first place.

<a name='azure-active-directory-integrated'></a>

## Integrated

With [Integrated Windows authentication (IWA)](/azure/active-directory/develop/msal-authentication-flows#integrated-windows-authentication-iwa), Microsoft Entra ID provides a solution to organizations with both on-premises and cloud infrastructures. On-premises Active Directory domains can be synchronized with Microsoft Entra ID through [federation](/azure/active-directory/hybrid/connect/whatis-fed), allowing management and access control to be handled within Microsoft Entra ID, while user authentication remains on-premises. With IWA, the user's Windows credentials are authenticated against Active Directory, and upon success the user's authentication token from Microsoft Entra ID is returned to SQL.

<a name='azure-active-directory-universal-with-multi-factor-authentication'></a>

### Universal with multifactor authentication

This is the standard interactive method with multifactor authentication option for Microsoft Entra accounts. This will work in most scenarios.

<a name='azure-active-directory-service-principal'></a>

## Service principal

A service principal is an identity that can be created for use with automated tools, jobs and applications. With the service principal authentication method, you can connect to your SQL Server instance using the client ID and secret of a service principal identity.

<a name='azure-active-directory-managed-identity'></a>

## Managed identity

Managed identities are special forms of service principals. There are two types of managed identities: system-assigned and user-assigned. System-assigned managed identities are enabled directly on an Azure resource, whereas user-assigned managed identities are a standalone resource that can be assigned to one or more Azure resources.

> [!Note]
> In order to use a managed identity to connect to a SQL resource through GUI clients such as SSMS and ADS, the machine running the client application must have a Microsoft Entra client running with the identity's certificate stored in it. This is most commonly achieved through an Azure VM, as the identity can be easily assigned to the machine through the VM's portal blade.

<a name='azure-active-directory-access-token'></a>

### Access token

Some non-GUI clients such as [Invoke-sqlcmd](/powershell/module/sqlserver/invoke-sqlcmd) allow providing an access token. The scope or audience of the access token must be `https://database.windows.net/`.

## Remarks

- Only [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] on-premises with a supported Windows or Linux operating system, or [SQL Server 2022 on Windows Azure VMs](/azure/azure-sql/virtual-machines/windows/security-considerations-best-practices#azure-ad-authentication-preview), is supported for Microsoft Entra authentication.
- To connect SQL Server to Azure Arc, the Microsoft Entra account needs the following permissions:
  - Member of the *Azure Connected Machine Onboarding* group or *Contributor* role in the resource group.
  - Member of the *Azure Connected Machine Resource Administrator* role in the resource group.
  - Member of the *Reader* role in the resource group.

## See also

- [Microsoft Entra authentication](/azure/active-directory/authentication/overview-authentication)
- [Linked server for SQL Server with Microsoft Entra authentication](azure-ad-authentication-sql-server-linked-server.md)
- [Tutorial: Using automation to set up the Microsoft Entra admin for SQL Server](azure-ad-authentication-sql-server-automation-setup-tutorial.md)
- [Tutorial: Set up Microsoft Entra authentication for SQL Server](azure-ad-authentication-sql-server-setup-tutorial.md)
- [Rotate certificates](../../../sql-server/azure-arc/rotate-certificates.md)

## Next steps

- [Connect your SQL Server to Azure Arc](../../../sql-server/azure-arc/connect.md)
