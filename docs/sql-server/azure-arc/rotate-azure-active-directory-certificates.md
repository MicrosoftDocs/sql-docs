---
title: Azure Active Directory certificate rotation 
titleSuffix: Azure Arc-enabled SQL Server
description: Explains how Azure Arc automatically rotates certificates for Azure Active Directory on Azure Arc-enabled SQL Server.
author: ntakru
ms.author: nikitatakru
ms.reviewer: mikeray
ms.date: 08/01/2023
ms.topic: conceptual
---

# Azure Active Directory certificate rotation

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

On Azure Arc-enabled SQL Server, Azure automatically rotates certificates for Azure Active Directory. This article explains how the automatic process works and identifies the process specifics for Windows and Linux operating systems.

Certificate management depends on whether you manage your own certificates (*customer managed certificates*), or the service manages the certificates (*service managed certificates*).

## Prerequisite

The functionality described in this article applies to an instance of Azure Arc-enabled SQL Server configured for Azure Active Directory. For instructions to configure such an instance, see:

- [Azure Active Directory authentication for SQL Server](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md)

## Customer managed certificate rotation

For customer managed certificate rotation, you create a new version of the certificate in Azure Key Vault. If you don't create the new version yourself, Azure Key Vault automatically rotates the certificate after the certificate lifetime has been met. In Azure Key Vault, you can pick, configure, and choose any percentage for the certificate lifetime period.

After you create the new version, you can download the new certificate in `.cer` format and upload it to the app registration in place of the old certificate.

> [!NOTE]
> For Linux, you need to restart the SQL Server service manually so the new certificate is used for authentication.  

## Service managed certificate rotation

For service managed certificate rotation, Azure Key Vault automatically rotates the certificate for you. By default, the certificate are rotated after the certificate lifetime has been met. If the certificate has expired, then the automatic rotation fails.

> [!NOTE]
> For Linux, the old certificate will not be deleted from the app registration used for Azure Active Directory authentication and the SQL server running on the Linux machine will need to be manually restarted.

## Next steps

- [Automatically connect your SQL Server to Azure Arc](automatically-connect.md)
- You can further investigate the security alerts and attacks using [Azure Sentinel](/azure/sentinel/overview). For details, see [on-board Azure Sentinel](/azure/sentinel/connect-data-sources).
