---
title: How Windows Authentication for Azure SQL Managed Instance is implemented with Microsoft Entra ID and Kerberos
titleSuffix: Azure SQL Managed Instance
description: Learn how Windows Authentication for Azure SQL Managed Instance is implemented with Microsoft Entra ID and Kerberos.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: mathoma, bonova, urmilano, wiassaf
ms.date: 09/27/2023
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: conceptual
---

# How Windows Authentication for Azure SQL Managed Instance is implemented with Microsoft Entra ID and Kerberos 

[Windows Authentication for Azure SQL Managed Instance principals](winauth-azuread-overview.md) in Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) enables customers to move existing services to the cloud while maintaining a seamless user experience and provides the basis for security infrastructure modernization. To enable Windows Authentication for Microsoft Entra principals, you will turn your Microsoft Entra tenant into an independent Kerberos realm and create an incoming trust in the customer domain.

This configuration allows users in the customer domain to access resources in your Microsoft Entra tenant. It will not allow users in the Microsoft Entra tenant to access resources in the customer domain.

The following diagram gives an overview of how Windows Authentication is implemented for a managed instance using Microsoft Entra ID and Kerberos:

:::image type="complex" source="media/winauth-azuread/auth-kerberos.svg" alt-text="An overview of authentication: a client submits an encrypted Kerberos ticket as part of an authentication request to a managed instance. The managed instance submits the encrypted Kerberos ticket to Microsoft Entra I D, which exchanges it for a Microsoft Entra token that is returned the managed instance. The managed instance uses this token to authenticate the user.":::

[!INCLUDE [entra-id](../includes/entra-id.md)]

<a name='how-azure-ad-provides-kerberos-authentication'></a>

## How Microsoft Entra ID provides Kerberos authentication

To create an independent Kerberos realm for a Microsoft Entra tenant, customers install the [Azure AD Hybrid Authentication Management PowerShell module](https://www.powershellgallery.com/packages/AzureADHybridAuthenticationManagement) on any Windows server and run a cmdlet to create a Microsoft Entra Kerberos object in their cloud and Active Directory. Trust created in this way enables existing Windows clients to access Microsoft Entra ID with Kerberos.

Windows 10 21H1 clients and above have been enlightened for interactive mode and do not need configuration for interactive login flows to work. Clients running previous versions of Windows can be configured to use Kerberos Key Distribution Center (KDC) proxy servers to use Kerberos authentication.

Kerberos authentication in Microsoft Entra ID enables:

- Traditional on-premises applications to move to the cloud without changing their fundamental authentication scheme.

- Applications running on enlightened clients authenticate using Microsoft Entra ID directly.


<a name='how-azure-sql-managed-instance-works-with-azure-ad-and-kerberos'></a>

## How Azure SQL Managed Instance works with Microsoft Entra ID and Kerberos

Customers use the Azure portal to enable a system assigned service principal on each managed instance. The service principal allows managed instance users to authenticate using the Kerberos protocol.

## Next steps

Learn more about enabling Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance:

- [How to set up Windows Authentication for Microsoft Entra ID with the modern interactive flow](winauth-azuread-setup-modern-interactive-flow.md)
- [How to set up Windows Authentication for Microsoft Entra ID with the incoming trust-based flow](winauth-azuread-setup-incoming-trust-based-flow.md)
- [Configure Azure SQL Managed Instance for Windows Authentication for Microsoft Entra ID](winauth-azuread-kerberos-managed-instance.md)
- [Troubleshoot Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance](winauth-azuread-troubleshoot.md)
