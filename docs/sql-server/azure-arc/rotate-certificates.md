---
title: Microsoft Entra ID certificate rotation 
titleSuffix: Azure Arc-enabled SQL Server
description: Explains how Azure Arc automatically rotates certificates for Microsoft Entra ID on Azure Arc-enabled SQL Server.
author: ntakru
ms.author: nikitatakru
ms.reviewer: mikeray
ms.date: 08/02/2023
ms.topic: conceptual
---

# Rotate certificates

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

On Azure Arc-enabled SQL Server, Azure extension for SQL Server can automatically rotate certificates for Microsoft Entra ID for service managed certificates. For customer managed certificates, you can follow the steps to rotate the certificate used for Microsoft Entra ID.

[!INCLUDE [entra-id](../../includes/entra-id.md)]

This article explains how automatic certificate rotation and customer managed certificate rotation works and identifies the process specifics for Windows and Linux operating systems.

Certificate management depends on whether you manage your own certificates (*customer managed certificates*), or the service manages the certificates (*service managed certificates*).

Azure Key Vault automatically rotates the certificate for you. Key vault rotates certificates by default, after the certificate lifetime is at 80%. You can configure this setting. For instructions, review [Configure certificate auto-rotation in Key Vault](/azure/key-vault/certificates/tutorial-rotate-certificates). If the certificate has expired, then the automatic rotation fails.

## Prerequisite

The functionality described in this article applies to an instance of Azure Arc-enabled SQL Server configured for authentication with Microsoft Entra ID. For instructions to configure such an instance, see:

- [Azure Active Directory authentication for SQL Server](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md)

## Customer managed certificate rotation

For customer managed certificate rotation:

1. Create a new version of the certificate in Azure Key Vault.

   In Azure Key Vault, you can set any percentage for the certificate lifetime period.

   When you configure a certificate with Azure Key Vault, you define its lifecycle attributes. For example:

   - Validity period - when the certificate expires.
   - Lifetime action type - what happens when the expiration approaches, including: automatic renewal, and alerting. 

   For details about certificate configuration options, see [Update certificate lifecycle attributes at the time of creation](/azure/key-vault/certificates/tutorial-rotate-certificates#update-certificate-lifecycle-attributes-at-the-time-of-creation).

1. Download the new certificate in `.cer` format and upload it to the app registration in place of the old certificate.

> [!NOTE]
> For Linux, you need to restart the SQL Server service manually so the new certificate is used for authentication.  

Once a new certificate is created in Azure Key Vault, the Azure extension for SQL Server checks for a new certificate daily. If the new certificate is available, the extension installs the new certificate on the server and deletes the old certificate.

After the new certificate is installed, you can delete older certificates from app registration because they won't be used.

It can take up to 24 hours for a new certificate to be installed on the server.  The recommended time to delete the old certificate from app registration is after 24 hours from the time you create the new version of the certificate.

If the new version of the certificate is created and installed on the server, but not uploaded to app registration, the portal displays an error message on the **SQL Server - Azure Arc** resource under **Microsoft Entra ID**.

## Service managed certificate rotation

With service managed certificate rotation, the Azure Extension for SQL Server rotates the certificates.

To allow the service to manage the certificate, add an access policy for the service principal with permission to sign keys. See [Assign a Key Vault access policy (legacy)](/azure/key-vault/general/assign-access-policy?tabs=azure-portal). The access policy assignment needs to explicitly reference the service principal of the Arc server.

For instructions, see [Create and assign a certificate](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-setup-tutorial.md#create-and-assign-a-certificate).

Once a new certificate is discovered, it is uploaded to app registration automatically.

> [!NOTE]
> For Linux, the old certificate will not be deleted from the app registration used for Microsoft Entra ID and the SQL Server running on the Linux machine will need to be manually restarted.

## Next steps

- [Automatically connect your SQL Server to Azure Arc](automatically-connect.md)
- You can further investigate the security alerts and attacks using [Azure Sentinel](/azure/sentinel/overview). For details, see [on-board Azure Sentinel](/azure/sentinel/connect-data-sources).
