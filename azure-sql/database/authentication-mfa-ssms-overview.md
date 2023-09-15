---
title: Using multi-factor Azure Active Directory authentication
titleSuffix: Azure SQL Database & SQL Managed Instance & Azure Synapse Analytics
description: Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics support connections from SQL Server Management Studio (SSMS) using Active Directory Universal Authentication.
author: GithubMirek
ms.author: mireks
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 12/15/2021
ms.service: sql-db-mi
ms.subservice: security
ms.topic: conceptual
ms.custom:
  - sqldbrb=1
tags: azure-synapse
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi || >=sql-server-ver16 || >= sql-server-linux-ver16"
---

# Using Azure Active Directory Multi-Factor Authentication
[!INCLUDE[appliesto-sqldb-sqlmi-asa-sqlvm-arc](../includes/appliesto-sqldb-sqlmi-asa-sqlvm-arc.md)]

[Azure Active Directory (Azure AD) Multi-Factor Authentication](/azure/active-directory/authentication/concept-mfa-howitworks) is a security feature provided by Microsoft's cloud-based identity and access management service. Multifactor authentication (MFA) enhances the security of user sign-ins by requiring users to provide extra verification steps beyond a password. MFA is a supported authentication method for [Azure SQL Database](sql-database-paas-overview.md), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md), [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is), and [SQL Server 2022 (16.x)](/sql/relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview) and later versions.

This article provides a brief overview of the benefits of multifactor authentication, explains how to configure it with Azure AD, and shows how to use it to connect to a database with [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms).

> [!IMPORTANT]
> Databases in Azure SQL Database, Azure SQL Managed Instance, Azure Synapse, and SQL Server 2022 are referred to collectively in the remainder of this article as databases, and the server is referring to the [server](logical-servers.md) that hosts databases for Azure SQL Database and Azure Synapse.

## Benefits of MFA

Azure AD Multi-Factor Authentication helps safeguard access to data and applications while meeting user demand for a simple sign-in process. MFA adds an extra layer of security to user sign-ins by requiring users to provide two or more authentication factors. These factors typically include something the user knows (password), something the user possesses (smartphone or hardware token), and/or something the user is (biometric data). By combining multiple factors, MFA significantly reduces the likelihood of unauthorized access.

Azure AD Multi-Factor Authentication provides all the benefits of Azure AD authentication described in the [Azure AD authentication overview](./authentication-aad-overview.md#overview).

For the full list of authentication methods available, see [What authentication and verification methods are available in Azure Active Directory?](/azure/active-directory/authentication/concept-authentication-methods)

## Configuration steps

1. **Configure an Azure Active Directory** - For more information, see [Administering your Azure AD directory](/previous-versions/azure/azure-services/hh967611(v=azure.100)), [Integrating your on-premises identities with Azure Active Directory](/azure/active-directory/hybrid/whatis-hybrid-identity), [Add your own domain name to Azure AD](/azure/active-directory/fundamentals/add-custom-domain), [Federation with Azure AD](/azure/active-directory/hybrid/connect/whatis-fed), and [Manage Azure AD using Windows PowerShell](/previous-versions/azure/jj151815(v=azure.100)).
2. **Configure MFA** - For step-by-step instructions, see [What is Azure AD Multi-Factor Authentication?](/azure/active-directory/authentication/concept-mfa-howitworks), [Conditional Access (MFA) with Azure SQL Database and Data Warehouse](conditional-access-configure.md). (Full Conditional Access requires a Premium Azure Active Directory. Limited MFA is available with a standard Azure AD.)
3. **Configure Azure AD Authentication** - For step-by-step instructions, see [Connecting to SQL Database, SQL Managed Instance, or Azure Synapse using Azure Active Directory Authentication](authentication-aad-overview.md).
4. **Download SSMS** - On the client computer, download the latest SSMS, from [Download SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms).

[!INCLUDE[ssms-connect-azure-ad](../includes/ssms-connect-azure-ad.md)]

## Azure AD B2B support

Azure AD multifactor authentication also supports [Azure AD B2B scenarios](/azure/active-directory/external-identities/what-is-b2b), which enable businesses to invite guest users to collaborate with their organization. Guest users can connect to databases either as individual users or members of an Azure AD group. For more information, see [Create guest user in SQL Database, Azure Synapse, and SQL Managed Instance](/azure/azure-sql/database/authentication-aad-guest-users#create-guest-user-in-sql-database-and-azure-synapse).

## Connect using MFA in SSMS

The following steps show how to connect using multifactor authentication in the latest SSMS.

1. To connect using MFA, on the **Connect to Server** dialog box in SSMS select **Active Directory - Universal with MFA**.

   ![Screenshot of the Connect to Server dialog in SSMS. "Azure Active Directory - Universal with MFA" is selected from the authentication dropdown window.](./media/authentication-mfa-ssms-overview/1-mfa-connect-authentication-method-dropdown.png)

2. Fill the **Server name** box with your server's name. Fill the **User name** box with your Azure AD credentials, in the format `user_name@domain.com`.
    
    ![Screenshot of the Connect to Server dialog settings in SSMS, with all fields filled in.](./media/authentication-mfa-ssms-overview/2-mfa-connect-to-server.png)

3. Click **Connect**.
4. When the **Sign in to your account** dialog box appears, it should be prepopulated with the **User name** you provided in step 2. No password is required if a user is part of a domain federated with Azure AD.

    ![Screenshot of the Sign in to your account dialog for Azure SQL Database and Data Warehouse. the account name is filled in.](./media/authentication-mfa-ssms-overview/3-mfa-sign-in.png)

5. You'll be prompted to authenticate using one of the methods configured based on the MFA administrator setting.
6. When verification is complete, SSMS connects normally, presuming valid credentials and firewall access.

Azure AD multifactor authentication is a supported authentication method for all [SQL tools](/sql/tools/overview-sql-tools). For information on programmatically authenticating with Azure AD, see the [Overview of the Microsoft Authentication Library (MSAL)](/azure/active-directory/develop/msal-overview).

## Next steps

- Grant others access to your database: [SQL Database Authentication and Authorization: Granting Access](logins-create-manage.md)
- Make sure others can connect through the firewall: [Configure a server-level firewall rule using the Azure portal](firewall-configure.md)
- [Configure and manage Azure Active Directory authentication with SQL Database or Azure Synapse](authentication-aad-configure.md)
- [Create Azure AD guest users and set as an Azure AD admin](authentication-aad-guest-users.md)
- C# interface [IUniversalAuthProvider Interface](/dotnet/api/microsoft.sqlserver.dac.iuniversalauthprovider)
- [Tutorial: Set up Azure Active Directory authentication for SQL Server](/sql/relational-databases/security/authentication-access/azure-ad-authentication-sql-server-setup-tutorial)