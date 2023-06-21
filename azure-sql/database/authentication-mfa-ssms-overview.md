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
  - seoapril2019
  - sqldbrb=1
tags: azure-synapse
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Using Azure Active Directory Multi-Factor Authentication
[!INCLUDE[appliesto-sqldb-sqlmi-asa](../includes/appliesto-sqldb-sqlmi-asa.md)]

[Azure Active Directory (Azure AD) Multi-Factor Authentication](/azure/active-directory/authentication/concept-mfa-howitworks) is a security feature provided by Microsoft's cloud-based identity and access management service. Multi-factor authentication (MFA) enhances the security of user sign-ins by requiring users to provide additional verification steps beyond a password. Azure AD MFA is a supported authentication method for [Azure SQL Database](sql-database-paas-overview.md), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md) and [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is).

This article provides a brief overview of the benefits of Azure AD MFA, explains how to configure it, and shows how to connect to a database using MFA with [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms).

> [!IMPORTANT]
> Databases in Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse are referred to collectively in the remainder of this article as databases, and the server is referring to the [server](logical-servers.md) that hosts databases for Azure SQL Database and Azure Synapse.

## How Azure AD MFA works

Azure AD MFA helps safeguard access to data and applications while meeting user demand for a simple sign-in process. Azure AD MFA adds an extra layer of security to user sign-ins by requiring users to provide two or more authentication factors. These factors typically include something the user knows (password), something the user possesses (smartphone or hardware token), and/or something the user is (biometric data). By combining multiple factors, the likelihood of unauthorized access is significantly reduced.

Azure AD MFA provides all the benefits of Azure AD authentication described in the [Azure AD authentication overview](./authentication-aad-overview.md#overview)

For the full list of authentication methods available, see [What authentication and verification methods are available in Azure Active Directory?](/azure/active-directory/authentication/concept-authentication-methods)

## Configuration steps

1. **Configure an Azure Active Directory** - For more information, see [Administering your Azure AD directory](/previous-versions/azure/azure-services/hh967611(v=azure.100)), [Integrating your on-premises identities with Azure Active Directory](/azure/active-directory/hybrid/whatis-hybrid-identity), [Add your own domain name to Azure AD](/azure/active-directory/fundamentals/add-custom-domain), [Federation with Azure AD](/azure/active-directory/hybrid/connect/whatis-fed), and [Manage Azure AD using Windows PowerShell](/previous-versions/azure/jj151815(v=azure.100)).
2. **Configure MFA** - For step-by-step instructions, see [What is Azure AD Multi-Factor Authentication?](/azure/active-directory/authentication/concept-mfa-howitworks), [Conditional Access (MFA) with Azure SQL Database and Data Warehouse](conditional-access-configure.md). (Full Conditional Access requires a Premium Azure Active Directory. Limited MFA is available with a standard Azure AD.)
3. **Configure Azure AD Authentication** - For step-by-step instructions, see [Connecting to SQL Database, SQL Managed Instance, or Azure Synapse using Azure Active Directory Authentication](authentication-aad-overview.md).
4. **Download SSMS** - On the client computer, download the latest SSMS, from [Download SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms).

[!INCLUDE[ssms-connect-azure-ad](../includes/ssms-connect-azure-ad.md)]

## Azure AD B2B support

Azure AD users that are supported for Azure AD B2B scenarios as guest users (see [What is Azure B2B collaboration](/azure/active-directory/external-identities/what-is-b2b)) can connect to SQL Database and Azure Synapse as individual users or members of an Azure AD group created in the associated Azure AD, and mapped manually using the [CREATE USER (Transact-SQL)](/sql/t-sql/statements/create-user-transact-sql) statement in a given database. 

For example, if `steve@gmail.com` is invited to Azure AD `contosotest` (with the Azure AD domain `contosotest.onmicrosoft.com`), a user `steve@gmail.com` must be created for a specific database (such as **MyDatabase**) by an Azure AD SQL administrator or Azure AD DBO by executing the Transact-SQL `create user [steve@gmail.com] FROM EXTERNAL PROVIDER` statement. If `steve@gmail.com` is part of an Azure AD group, such as `usergroup` then this group must be created for a specific database (such as **MyDatabase**) by an Azure AD SQL administrator, or Azure AD DBO by executing the Transact-SQL statement `create user [usergroup] FROM EXTERNAL PROVIDER` statement. 

After the database user or group is created, then the user `steve@gmail.com` can sign into `MyDatabase` using the SSMS authentication option `Azure Active Directory – Universal with MFA`. By default, the user or group only has connect permission. Any further data access will need to be [granted](/sql/t-sql/statements/grant-transact-sql) in the database by a user with enough privilege.

## Connect using Azure AD MFA in SSMS

The following steps show how to connect using multi-factor authentication in the latest SSMS.

1. To connect using Azure AD MFA, on the **Connect to Server** dialog box in SSMS select **Active Directory - Universal with MFA support**.

   ![Screenshot of the Connect to Server dialog settings in S S M S, showing Server type, Server name, Authentication, and User name. The Authentication setting "Azure Active Directory - Universal with MFA" is selected from a dropdown window.](./media/authentication-mfa-ssms-overview/1mfa-connect-authentication-method-dropdown.png)

2. Fill the **Server name** box with your server's name. Fill the **User name** box with your Azure AD credentials, in the format `user_name@domain.com`.
    
    ![Screenshot of the Connect to Server dialog settings in S S M S, with all fields filled in.](./media/authentication-mfa-ssms-overview/2mfa-connect-to-server.png)

3. Click **Connect**.
4. When the **Sign in to your account** dialog box appears, it should be pre-filled with the **User name** you provided in step 2. No password is required if a user is part of a domain federated with Azure AD.

    ![Screenshot of the Sign in to your account dialog for Azure SQL Database and Data Warehouse. the account name is filled in.](./media/authentication-mfa-ssms-overview/3mfa-sign-in.png)

5. You will be prompted to authenticate using one of the methods configured based on the MFA administrator setting.
6. When verification is complete, SSMS connects normally, presuming valid credentials and firewall access.

Azure AD MFA is supported as an authentication method for all [SQL tools](/sql/tools/overview-sql-tools). For information on programmatically authenticating with Azure AD, see the [Overview of the Microsoft Authentication Library (MSAL)](/azure/active-directory/develop/msal-overview).

## Next steps

- Grant others access to your database: [SQL Database Authentication and Authorization: Granting Access](logins-create-manage.md)
- Make sure others can connect through the firewall: [Configure a server-level firewall rule using the Azure portal](firewall-configure.md)
- [Configure and manage Azure Active Directory authentication with SQL Database or Azure Synapse](authentication-aad-configure.md)
- [Create Azure AD guest users and set as an Azure AD admin](authentication-aad-guest-users.md)
- C# interface [IUniversalAuthProvider Interface](/dotnet/api/microsoft.sqlserver.dac.iuniversalauthprovider)
