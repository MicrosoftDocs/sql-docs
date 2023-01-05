---
title: "Azure Active Directory (Azure AD) authentication for SQL Server overview"
description: Learn about Azure Active Directory authentication support for SQL Server
author: GithubMirek
ms.author: mireks
ms.reviewer: vanto, randolphwest
ms.date: 10/20/2022
ms.service: sql
ms.subservice: security
ms.topic: conceptual
ms.custom: event-tier1-build-2022
monikerRange: ">=sql-server-ver15||>= sql-server-linux-ver16"
---

# Azure Active Directory authentication for SQL Server

[!INCLUDE [SQL Server 2022](../../../includes/applies-to-version/sqlserver2022.md)]

[!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] introduces support for [Azure Active Directory (Azure AD) authentication](/azure/active-directory/authentication/overview-authentication), on both Windows and Linux on-premises, and [SQL Server on Windows Azure VMs](/azure/azure-sql/virtual-machines/windows/security-considerations-best-practices#azure-ad-authentication-preview).

## Overview

You can now connect to SQL Server using the following authentication methods using Azure AD identities:

- Azure Active Directory Password
- Azure Active Directory Integrated
- Azure Active Directory Universal with Multi-Factor Authentication
- Azure Active Directory access token

The current authentication modes, such as [SQL authentication and Windows authentication](../choose-an-authentication-mode.md) remain unchanged.

As a central authentication repository used by Azure, Azure AD allows you to store objects such as users, groups, or service principals as identities. Azure AD also allows you to use those identities to authenticate with different Azure services. Azure AD authentication is supported for Azure SQL Database, Azure SQL Managed Instance, SQL Server on Windows Azure VMs, Azure Synapse Analytics, and SQL Server. For more information, see [Use Azure Active Directory authentication](/azure/azure-sql/database/authentication-aad-overview) and [Configure and manage Azure AD authentication with Azure SQL](/azure/azure-sql/database/authentication-aad-configure).

If your Windows Server Active Directory is federated with Azure AD, users can authenticate with SQL Server using their Windows credentials, either as a Windows logins or an Azure AD login. Azure AD doesn't support all AD features, such as service accounts or complex networking forest architecture that is supported for Windows Server Active Directory.

## Connect SQL Server to Azure with Azure AD

For SQL Server to communicate with Azure, both SQL Server and the Windows or Linux host it runs on must be registered with [Azure Arc](../../../sql-server/azure-arc/overview.md). To do this, you'll need to install the [Azure Arc Agent](/azure/azure-arc/servers/overview) and [Azure extension for SQL Server](../../../sql-server/azure-arc/overview.md). This will facilitate SQL Server's  communication with Azure.

To get started, see [Connect your SQL Server to Azure Arc](../../../sql-server/azure-arc/connect.md).

## Authentication methods

The following authentication methods are available when connecting to SQL Server using Azure AD authentication.

### Azure Active Directory Password

Allows specifying the username and password to the client and driver, but this is disabled on many tenants for security reasons. When possible, we recommend avoiding this as it requires sending passwords over the network. These connections are encrypted, but it's best practice to never send them in the first place.

### Azure Active Directory Integrated

When the Windows domain is synchronized with Azure AD, and a user is logged into the Windows domain, the user's Windows credentials are used for Azure AD authentication.

### Azure Active Directory Universal with Multi-Factor Authentication

This is the standard interactive method with multi-factor authentication option for Azure AD accounts. This will work in most scenarios.

### Azure Active Directory access token

Some non-GUI clients such as [Invoke-sqlcmd](/powershell/module/sqlserver/invoke-sqlcmd) allow providing an access token. The scope or audience of the access token must be `https://database.windows.net/`.

## Remarks

- Only [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] on-premises with a supported Windows or Linux operating system, or [SQL Server 2022 on Windows Azure VMs](/azure/azure-sql/virtual-machines/windows/security-considerations-best-practices#azure-ad-authentication-preview), is supported for Azure AD authentication.
- To connect SQL Server to Azure Arc, the Azure AD account needs the following permissions:
  - Member of the *Azure Connected Machine Onboarding* group or *Contributor* role in the resource group.
  - Member of the *Azure Connected Machine Resource Administrator* role in the resource group.
  - Member of the *Reader* role in the resource group.

## See also

- [Tutorial: Set up Azure Active Directory authentication for SQL Server](azure-ad-authentication-sql-server-setup-tutorial.md)
- [Azure Active Directory (Azure AD) authentication](/azure/active-directory/authentication/overview-authentication)
- [Linked server for SQL Server with Azure Active Directory authentication](azure-ad-authentication-sql-server-linked-server.md)
- [Tutorial: Using automation to set up the Azure Active Directory admin for SQL Server](azure-ad-authentication-sql-server-automation-setup-tutorial.md)

## Next steps

- [Connect your SQL Server to Azure Arc](../../../sql-server/azure-arc/connect.md)
