---
title: How to set up Windows authentication for Microsoft Entra ID with the modern interactive flow
titleSuffix: Azure SQL Managed Instance
description: Learn how to set up Windows Authentication for Microsoft Entra ID with the modern interactive flow.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: mathoma, bonova, urmilano, wiassaf
ms.date: 09/27/2023
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: how-to
---

# How to set up Windows Authentication for Microsoft Entra ID with the modern interactive flow 

This article describes how to implement the modern interactive authentication flow to allow clients running Windows 10 20H1, Windows Server 2022, or a higher version of Windows to authenticate to Azure SQL Managed Instance using Windows Authentication. Clients must be joined to Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) or [Microsoft Entra hybrid joined](/azure/active-directory/devices/how-to-hybrid-join).

Enabling the modern interactive authentication flow is one step in [setting up Windows Authentication for Azure SQL Managed Instance using Microsoft Entra ID and Kerberos](winauth-azuread-setup.md). The [incoming trust-based flow](winauth-azuread-setup-incoming-trust-based-flow.md) is available for AD joined clients running Windows 10 / Windows Server 2012 and higher.

With this feature, Microsoft Entra ID is now its own independent Kerberos realm. Windows 10 21H1 clients are already enlightened and will redirect clients to access Microsoft Entra Kerberos to request a Kerberos ticket. The capability for clients to access Microsoft Entra Kerberos is switched off by default and can be enabled by modifying group policy. Group policy can be used to deploy this feature in a staged manner by choosing specific clients you want to pilot on and then expanding it to all the clients across your environment.

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Prerequisites

There is no Active Directory to Microsoft Entra ID set up required to enable running software on Microsoft Entra-joined VMs to access Azure SQL Managed Instance using Windows Authentication. The following prerequisites are required to implement the modern interactive authentication flow:

|Prerequisite  |Description  |
|---------|---------|
|Clients must run Windows 10 20H1, Windows Server 2022, or a higher version of Windows. |         |
|Clients must be Microsoft Entra joined or Microsoft Entra hybrid joined. |  You can determine if this prerequisite is met by running the [dsregcmd command](/azure/active-directory/devices/troubleshoot-device-dsregcmd): `dsregcmd.exe /status` |
|Application must connect to the managed instance via an interactive session. | This supports applications such as SQL Server Management Studio (SSMS) and web applications, but won't work for applications that run as a service. |
|Microsoft Entra tenant. |         |
|Azure subscription under the same Microsoft Entra tenant you plan to use for authentication.|         |
| [Microsoft Entra Connect](/azure/active-directory/hybrid/whatis-azure-ad-connect) installed. | Hybrid environments where identities exist both in Microsoft Entra ID and AD. |



## Configure group policy

Enable the following group policy setting `Administrative Templates\System\Kerberos\Allow retrieving the cloud Kerberos ticket during the logon`:

1. Open the group policy editor.
1. Navigate to `Administrative Templates\System\Kerberos\`.
1. Select the **Allow retrieving the cloud kerberos ticket during the logon** setting.

    :::image type="content" source="media/winauth-azuread/policy-allow-retrieving-cloud-kerberos-ticket-during-logon.png" alt-text="A list of kerberos policy settings in the Windows policy editor. The 'Allow retrieving the cloud kerberos ticket during the logon' policy is highlighted with a red box."  lightbox="media/winauth-azuread/policy-allow-retrieving-cloud-kerberos-ticket-during-logon.png":::

1. In the setting dialog, select **Enabled**.
1. Select **OK**.

    :::image type="content" source="media/winauth-azuread/policy-enable-cloud-kerberos-ticket-during-logon-setting.png" alt-text="Screenshot of the 'Allow retrieving the cloud kerberos ticket during the logon' dialog. Select 'Enabled' and then 'OK' to enable the policy setting."  lightbox="media/winauth-azuread/policy-enable-cloud-kerberos-ticket-during-logon-setting.png":::
    
## Refresh PRT (optional)

Users with existing logon sessions may need to refresh their Microsoft Entra [Primary Refresh Token (PRT)](/azure/active-directory/devices/concept-primary-refresh-token) if they attempt to use this feature immediately after it has been enabled. It can take up to a few hours for the PRT to refresh on its own.

To refresh PRT manually, run this command from a command prompt:

``` dos
dsregcmd.exe /RefreshPrt
```

## Next steps

Learn more about implementing Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance:

- [What is Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance?](winauth-azuread-overview.md)
- [How Windows Authentication for Azure SQL Managed Instance is implemented with Microsoft Entra ID and Kerberos](winauth-implementation-aad-kerberos.md)
- [How to set up Windows Authentication for Microsoft Entra ID with the incoming trust-based flow](winauth-azuread-setup-incoming-trust-based-flow.md)
- [Configure Azure SQL Managed Instance for Windows Authentication for Microsoft Entra ID](winauth-azuread-kerberos-managed-instance.md)
- [Troubleshoot Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance](winauth-azuread-troubleshoot.md)
