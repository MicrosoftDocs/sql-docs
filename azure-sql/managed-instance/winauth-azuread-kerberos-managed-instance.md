---
title: Configure Windows Authentication with Microsoft Entra ID
titleSuffix: Azure SQL Managed Instance
description: Learn how to configure Azure SQL Managed Instance for Windows Authentication for Microsoft Entra ID.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: bonova, urmilano, wiassaf
ms.date: 08/26/2025
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: how-to
ms.update-cycle: 365-days
ms.custom:
  - has-azure-ad-ps-ref
  - azure-ad-ref-level-one-done
  - sfi-image-nochange
---

# Configure Azure SQL Managed Instance for Windows Authentication for Microsoft Entra ID

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes how to configure a SQL managed instance to support [Windows Authentication for principals](winauth-azuread-overview.md) in Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)). The steps to set up Azure SQL Managed Instance are the same for both the [Incoming trust-based authentication flow](winauth-azuread-setup-incoming-trust-based-flow.md) and the [Modern interactive authentication flow](winauth-azuread-setup-modern-interactive-flow.md).

## Prerequisites

The following prerequisites are required to configure a SQL managed instance for Windows Authentication for Microsoft Entra principals:

|Prerequisite  | Description  |
|---------|---------|
|Az.Sql PowerShell module | This PowerShell module provides management cmdlets for Azure SQL resources.<br /><br /> Install this module by running the following PowerShell command: `Install-Module -Name Az.Sql`   |
|Microsoft Graph PowerShell Module  | This module provides management cmdlets for Microsoft Entra administrative tasks such as user and service principal management.<br /><br /> Install this module by running the following PowerShell command: `Install-Module –Name Microsoft.Graph`  |
| A SQL managed instance | You might [Create a new SQL managed instance](instance-create-quickstart.md) or use an existing SQL managed instance. You must [Enable Microsoft Entra authentication](../database/authentication-aad-configure.md) on the SQL managed instance. |

<a id="configure-azure-ad-authentication-for-azure-sql-managed-instance"></a>

## Configure Microsoft Entra authentication for Azure SQL Managed Instance

To enable Windows Authentication for Microsoft Entra principals, you need to enable a system-assigned service principal on each SQL managed instance. The system-assigned service principal allows SQL managed instance users to authenticate using the Kerberos protocol. You also need to grant admin consent to each service principal.

### Enable a system assigned service principal

To enable a system assigned service principal for a SQL managed instance:

1. Sign in to the [Azure portal](https://portal.azure.com).
1. Navigate to your SQL managed instance.
1. Select **Identity**.
1. Set **System assigned service principal** to **On**.
   :::image type="content" source="media/winauth-azuread/azure-portal-managed-instance-identity-enable-system-assigned-service-principal.png" alt-text="Screenshot of the identity pane for a SQL managed instance in the Azure portal, with 'System assigned service principal' set to 'On'."  lightbox="media/winauth-azuread/azure-portal-managed-instance-identity-enable-system-assigned-service-principal.png":::
1. Select **Save**.

### Grant admin consent to a system assigned service principal

1. Sign in to the [Azure portal](https://portal.azure.com).
1. Open Microsoft Entra ID.
1. Select **App registrations**.
1. Select **All applications**.
   :::image type="content" source="media/winauth-azuread/azure-portal-azuread-app-registrations.png" alt-text="Screenshot of the Microsoft Entra ID resource in the Azure portal with App registrations selected in the left pane."  lightbox="media/winauth-azuread/azure-portal-azuread-app-registrations.png":::
1. Select the application with the display name matching your SQL managed instance. The name will be in the format: `<managedinstancename> principal`.
1. Select **API permissions**.
1. Select **Grant admin consent**.

   :::image type="content" source="media/winauth-azuread/azure-portal-configure-permissions-admin-consent.png" alt-text="Screenshot from the Azure portal of the configured permissions for applications. The status for the example application is 'Granted for aadsqlmi'."  lightbox="media/winauth-azuread/azure-portal-configure-permissions-admin-consent.png":::
1. Select **Yes** on the prompt to **Grant admin consent confirmation**.

## Connect to the SQL managed instance with Windows Authentication

If you have already implemented either the incoming [Trust-based authentication flow](winauth-azuread-setup-incoming-trust-based-flow.md) or the [Modern interactive authentication flow](winauth-azuread-setup-modern-interactive-flow.md), depending on the version of your client, you can now test connecting to your SQL managed instance with Windows Authentication.

To test the connection with [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms), follow the steps in [Quickstart: Use SSMS to connect to and query Azure SQL Database or Azure SQL Managed Instance](../database/connect-query-ssms.md). Select **Windows Authentication** as your authentication type.

:::image type="content" source="media/winauth-azuread/winauth-connect-to-managed-instance.png" alt-text="Dialog box from SQL Server Management Studio with a SQL managed instance name in the 'Server Name' area and 'Authentication' set to 'Windows Authentication'." :::

## Related content

- [Troubleshoot Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance](winauth-azuread-troubleshoot.md)
- [What is Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance?](winauth-azuread-overview.md)
- [How to set up Windows Authentication for Azure SQL Managed Instance using Microsoft Entra ID and Kerberos](winauth-azuread-setup.md)
