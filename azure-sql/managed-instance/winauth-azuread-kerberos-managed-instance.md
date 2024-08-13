---
title: Configure Windows Authentication with Microsoft Entra ID
titleSuffix: Azure SQL Managed Instance
description: Learn how to configure Azure SQL Managed Instance for Windows Authentication for Microsoft Entra ID.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: mathoma, bonova, urmilano, wiassaf
ms.date: 09/27/2023
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.custom: has-azure-ad-ps-ref, azure-ad-ref-level-one-done
ms.topic: how-to
---

# Configure Azure SQL Managed Instance for Windows Authentication for Microsoft Entra ID
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes how to configure a managed instance to support [Windows Authentication for principals](winauth-azuread-overview.md) in Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)). The steps to set up Azure SQL Managed Instance are the same for both the [incoming trust-based authentication flow](winauth-azuread-setup-incoming-trust-based-flow.md) and the [modern interactive authentication flow](winauth-azuread-setup-modern-interactive-flow.md). 

## Prerequisites

The following prerequisites are required to configure a managed instance for Windows Authentication for Microsoft Entra principals:

|Prerequisite  | Description  |
|---------|---------|
|Az.Sql PowerShell module | This PowerShell module provides management cmdlets for Azure SQL resources.<BR/><BR/> Install this module by running the following PowerShell command: `Install-Module -Name Az.Sql`   |
|Microsoft Graph PowerShell Module  | This module provides management cmdlets for Microsoft Entra administrative tasks such as user and service principal management.<BR/><BR/> Install this module by running the following PowerShell command: `Install-Module –Name Microsoft.Graph`  |
| A managed instance | You may [create a new managed instance](instance-create-quickstart.md) or use an existing managed instance. You must [enable Microsoft Entra authentication](../database/authentication-aad-configure.md) on the managed instance. |

<a name='configure-azure-ad-authentication-for-azure-sql-managed-instance'></a>

## Configure Microsoft Entra authentication for Azure SQL Managed Instance

To enable Windows Authentication for Microsoft Entra principals, you need to enable a system-assigned service principal on each managed instance. The system-assigned service principal allows managed instance users to authenticate using the Kerberos protocol. You also need to grant admin consent to each service principal.

### Enable a system assigned service principal

To enable a system assigned service principal for a managed instance:

1. Sign in to the [Azure portal](https://portal.azure.com).
1. Navigate to your managed instance.
1. Select **Identity**.
1. Set **System assigned service principal** to **On**.
    :::image type="content" source="media/winauth-azuread/azure-portal-managed-instance-identity-enable-system-assigned-service-principal.png" alt-text="Screenshot of the identity pane for a managed instance in the Azure portal, with 'System assigned service principal' set to 'On'."  lightbox="media/winauth-azuread/azure-portal-managed-instance-identity-enable-system-assigned-service-principal.png":::
1. Select **Save**.

### Grant admin consent to a system assigned service principal

1. Sign in to the [Azure portal](https://portal.azure.com).
1. Open Microsoft Entra ID.
1. Select **App registrations**.
1. Select **All applications**.
   :::image type="content" source="media/winauth-azuread/azure-portal-azuread-app-registrations.png" alt-text="Screenshot of the Microsoft Entra ID resource in the Azure portal with App registrations selected in the left pane."  lightbox="media/winauth-azuread/azure-portal-azuread-app-registrations.png":::
1. Select the application with the display name matching your managed instance. The name will be in the format: `<managedinstancename> principal`.
1. Select **API permissions**.
1. Select **Grant admin consent**.

    :::image type="content" source="media/winauth-azuread/azure-portal-configure-permissions-admin-consent.png" alt-text="Screenshot from the Azure portal of the configured permissions for applications. The status for the example application is 'Granted for aadsqlmi'."  lightbox="media/winauth-azuread/azure-portal-configure-permissions-admin-consent.png":::
1. Select **Yes** on the prompt to **Grant admin consent confirmation**.

## Connect to the managed instance with Windows Authentication

If you have already implemented either the incoming [trust-based authentication flow](winauth-azuread-setup-incoming-trust-based-flow.md) or the [modern interactive authentication flow](winauth-azuread-setup-modern-interactive-flow.md), depending on the version of your client, you can now test connecting to your managed instance with Windows Authentication.

To test the connection with [SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms) (SSMS), follow the steps in [Quickstart: Use SSMS to connect to and query Azure SQL Database or Azure SQL Managed Instance](../database/connect-query-ssms.md). Select **Windows Authentication** as your authentication type.

:::image type="content" source="media/winauth-azuread/winauth-connect-to-managed-instance.png" alt-text="Dialog box from SQL Server Management Studio with a managed instance name in the 'Server Name' area and 'Authentication' set to 'Windows Authentication'." :::

## Next steps

Learn more about implementing Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance:

- [Troubleshoot Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance](winauth-azuread-troubleshoot.md)
- [What is Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance?](winauth-azuread-overview.md)
- [How to set up Windows Authentication for Azure SQL Managed Instance using Microsoft Entra ID and Kerberos](winauth-azuread-setup.md)
