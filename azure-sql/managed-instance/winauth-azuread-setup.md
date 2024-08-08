---
title: How to set up Windows Authentication for Azure SQL Managed Instance using Microsoft Entra ID and Kerberos
titleSuffix: Azure SQL Managed Instance
description: Learn how to set up Windows Authentication access to Azure SQL Managed Instance using Microsoft Entra ID and Kerberos.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: mathoma, bonova, urmilano, wiassaf
ms.date: 09/27/2023
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.custom: has-azure-ad-ps-ref, azure-ad-ref-level-one-done
ms.topic: conceptual
---


# How to set up Windows Authentication for Azure SQL Managed Instance using Microsoft Entra ID and Kerberos 

This article gives an overview of how to set up infrastructure and managed instances to implement [Windows Authentication for principals on Azure SQL Managed Instance](winauth-azuread-overview.md) with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)).

There are two phases to set up Windows Authentication for Azure SQL Managed Instance using Microsoft Entra ID and Kerberos.

- **One-time infrastructure setup.**
    - Synchronize Active Directory (AD) and Microsoft Entra ID, if this hasn't already been done.
    - Enable the modern interactive authentication flow, when available. The modern interactive flow is recommended for organizations with [Microsoft Entra joined](/azure/active-directory/devices/concept-directory-join) or [hybrid joined](/azure/active-directory/devices/concept-hybrid-join) clients running Windows 10 20H1 / Windows Server 2022 and higher.
    - Set up the incoming trust-based authentication flow. This is recommended for customers who can't use the modern interactive flow, but who have AD joined clients running Windows 10 / Windows Server 2012 and higher.
- **Configuration of Azure SQL Managed Instance.**
    - Create a system assigned service principal for each managed instance.

[!INCLUDE [entra-id](../includes/entra-id.md)]

## One-time infrastructure setup

The first step in infrastructure setup is to synchronize AD with Microsoft Entra ID, if this hasn't already been completed.

Following this, a system administrator configures authentication flows. Two authentication flows are available to implement Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance: the incoming trust-based flow supports AD joined clients running Windows server 2012 or higher, and the modern interactive flow supports Microsoft Entra joined clients running Windows 10 21H1 or higher.

<a name='synchronize-ad-with-azure-ad'></a>

### Synchronize AD with Microsoft Entra ID

Customers should first implement [Microsoft Entra Connect](/azure/active-directory/hybrid/whatis-azure-ad-connect) to integrate on-premises directories with Microsoft Entra ID.

### Select which authentication flow(s) you will implement

The following diagram shows eligibility and the core functionality of the modern interactive flow and the incoming trust-based flow:

:::image type="complex" source="media/winauth-azuread/decision-authentication.svg" alt-text="A decision tree showing criteria to select authentication flows." :::
"A decision tree showing that the modern interactive flow is suitable for clients running Windows 10 20H1 or Windows Server 2022 or higher, where clients are Microsoft Entra joined or Microsoft Entra hybrid joined. The incoming trust-based flow is suitable for clients running Windows 10 or Windows Server 2012 or higher where clients are AD joined."
:::image-end:::

The modern interactive flow works with enlightened clients running Windows 10 21H1 and higher that are Microsoft Entra joined or Microsoft Entra hybrid joined. In the modern interactive flow, users can access Azure SQL Managed Instance without requiring a line of sight to Domain Controllers (DCs). There is no need for a trust object to be created in the customer's AD. To enable the modern interactive flow, an administrator will set group policy for Kerberos authentication tickets (TGT) to be used during login.

The incoming trust-based flow works for clients running Windows 10 or Windows Server 2012 and higher. This flow requires that clients be joined to AD and have a line of sight to AD from on-premises. In the incoming trust-based flow, a trust object is created in the customer's AD and is registered in Microsoft Entra ID. To enable the incoming trust-based flow, an administrator will set up an incoming trust with Microsoft Entra ID and set up Kerberos Proxy via group policy.

### Modern interactive authentication flow

The following prerequisites are required to implement the modern interactive authentication flow:

|Prerequisite  |Description  |
|---------|---------|
|Clients must run Windows 10 20H1, Windows Server 2022, or a higher version of Windows. |         |
|Clients must be [Microsoft Entra joined](/azure/active-directory/devices/concept-directory-join) or [Microsoft Entra hybrid joined](/azure/active-directory/devices/concept-hybrid-join). | You can determine if this prerequisite is met by running the [dsregcmd command](/azure/active-directory/devices/troubleshoot-device-dsregcmd): `dsregcmd.exe /status` |
|Application must connect to the managed instance via an interactive session. | This supports applications such as SQL Server Management Studio (SSMS) and web applications, but won't work for applications that run as a service. |
|Microsoft Entra tenant. |         |
|Azure subscription under the same Microsoft Entra tenant you plan to use for authentication.|         |
|[Microsoft Entra Connect](/azure/active-directory/hybrid/whatis-azure-ad-connect) installed. | Hybrid environments where identities exist both in Microsoft Entra ID and AD. |


See [How to set up Windows Authentication for Microsoft Entra ID with the modern interactive flow](winauth-azuread-setup-modern-interactive-flow.md) for steps to enable this authentication flow.

### Incoming trust-based authentication flow

The following prerequisites are required to implement the incoming trust-based authentication flow:

|Prerequisite  |Description  |
|---------|---------|
|Client must run Windows 10, Windows Server 2012, or a higher version of Windows. |         |
|Clients must be joined to AD. The domain must have a functional level of Windows Server 2012 or higher. |  You can determine if the client is joined to AD by running the [dsregcmd command](/azure/active-directory/devices/troubleshoot-device-dsregcmd): `dsregcmd.exe /status`  |
|Azure AD Hybrid Authentication Management Module. | This PowerShell module provides management features for on-premises setup. |
|Microsoft Entra tenant.  |         |
|Azure subscription under the same Microsoft Entra tenant you plan to use for authentication.|         |
|[Microsoft Entra Connect](/azure/active-directory/hybrid/whatis-azure-ad-connect) installed. | Hybrid environments where identities exist both in Microsoft Entra ID and AD. |


See [How to set up Windows Authentication for Microsoft Entra ID with the incoming trust based flow](winauth-azuread-setup-incoming-trust-based-flow.md) for instructions on enabling this authentication flow.


## Configure Azure SQL Managed Instance

The steps to set up Azure SQL Managed Instance are the same for both the incoming trust-based authentication flow and the modern interactive authentication flow.

#### Prerequisites to configure a managed instance

The following prerequisites are required to configure a managed instance for Windows Authentication for Microsoft Entra principals:

|Prerequisite  | Description  |
|---------|---------|
|Az.Sql PowerShell module | This PowerShell module provides management cmdlets for Azure SQL resources. Install this module by running the following PowerShell command: `Install-Module -Name Az.Sql`   |
|Microsoft Graph PowerShell Module  | This module provides management cmdlets for Microsoft Entra ID administrative tasks such as user and service principal management. Install this module by running the following PowerShell command: `Install-Module –Name Microsoft.Graph`  |
| A managed instance | You may [create a new managed instance](instance-create-quickstart.md) or use an existing managed instance. |

#### Configure each managed instance

See [Configure Azure SQL Managed Instance for Windows Authentication for Microsoft Entra ID](winauth-azuread-kerberos-managed-instance.md) for steps to configure each managed instance.

## Limitations

The following limitations apply to Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance:

###	Not available for Linux clients

Windows Authentication for Microsoft Entra principals is currently supported only for client machines running Windows.

###	Microsoft Entra ID cached logon

Windows limits how often it connects to Microsoft Entra ID, so there is a potential for user accounts to not have a refreshed Kerberos Ticket Granting Ticket (TGT) within 4 hours of an upgrade or fresh deployment of a client machine.  User accounts who do not have a refreshed TGT results in failed ticket requests from Microsoft Entra ID.  

As an administrator, you can trigger an online logon immediately to handle upgrade scenarios by running the following command on the client machine, then locking and unlocking the user session to get a refreshed TGT:

```dos
dsregcmd.exe /RefreshPrt
```

## Next steps

Learn more about implementing Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance:

- [What is Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance?](winauth-azuread-overview.md)
- [How Windows Authentication for Azure SQL Managed Instance is implemented with Microsoft Entra ID and Kerberos](winauth-implementation-aad-kerberos.md)
- [How to set up Windows Authentication for Microsoft Entra ID with the modern interactive flow](winauth-azuread-setup-modern-interactive-flow.md)
- [How to set up Windows Authentication for Microsoft Entra ID with the incoming trust-based flow](winauth-azuread-setup-incoming-trust-based-flow.md)
- [Configure Azure SQL Managed Instance for Windows Authentication for Microsoft Entra ID](winauth-azuread-kerberos-managed-instance.md)
