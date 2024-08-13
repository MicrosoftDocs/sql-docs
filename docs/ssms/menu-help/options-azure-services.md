---
title: SQL Server Options page - Azure Services
description: "Options (Azure Services)"
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 08/13/2024
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "VS.ToolsOptionsPages.Azure_Services.Azure_Cloud"
dev_langs:
  - TSQL
---

# Options (Azure Services)

Use this page to specify options related to Azure cloud services. To access this dialog box, go to **Tools > Options > Azure Services** from the top menu bar.

[!INCLUDE [entra-id-hard-coded](../../includes/entra-id-hard-coded.md)]

## Miscellaneous

| Option | Information | Description |
|--------|-------------|-------------|
| Allow single sign-on to SQL using Microsoft Entra authentication | **True** <br> **False** | When true, removes the requirement to provide full credentials to connect to Azure SQL Database and SQL Server using Microsoft Entra ID if using the same account used to log in to Windows. |
| Azure Data Factory Portal URL | `https://adf.azure.com/` | Specifies the URL for the Azure Data Factory portal. |
| Gallery Endpoint | `https://gallery.azure.com/` | Specifies the endpoint for the Resource Manager gallery of deployment templates. |
| Graph Audience | `https://graph.microsoft.com/` | Graph Endpoint Resource ID. |
| Graph Endpoint | `https://graph.microsoft.com/` | Specifies the URL for Azure Active Directory Graph requests. |
| Management Portal URL | `https://portal.azure.com` | Specifies the URL for the Management Portal. |
| MSAL Output Window Trace Level | **Information** <br> **None** <br> **Verbose** <br> **Warning** | The level of Microsoft Authentication Library (MSAL) login tracing to send to the Output window. |
| Publish Setting File URL | `https://go.microsoft.com/fwlink/?LinkID=335839` | Specifies the URL from which the `.publishsettings` file can be downloaded. |
| SQL Database Service Principal Name | `https://database.windows.net/` | The Azure SQL Database SPN to obtain a token when using Azure AD authentication. Also the audience of the JSON Web Token (JWT) for server-side JSON Web Token (JWT) parsing/validation. |
| Use system default web browser | **True** <br> **False** | When true, SSMS will launch the user's default web browser to perform Azure logins.  When false, SSMS will use an embedded browser.  This option defaults to True with new installs starting with SSMS 19.1 |
| Use Web Account Manager | **True**<br />**False** | Enable this option if your enterprise uses FIDO keys or other advanced options not supported by a web browser. For example, a company might implement a conditional access policy, which prevents authentication to Azure using a browser. If you aren't sure of the appropriate value for your environment, contact your SQL Database Administrator or your IT team. |

## Resource Management

| Option | Information | Description |
|--------|-------------|-------------|
| Microsoft Entra Authority | `https://login.microsoftonline.com/` | Specifies the base authority for Microsoft Entra authentication. |
| Microsoft Entra Endpoint Resource ID | `https://management.core.windows.net/` | Specifies the audience for tokens that authenticate requests to Azure Resource Manager (ARM) or Service Management (RDFE) endpoints. |
| Resource Manager Endpoint | `https://management.azure.com/` | Specifies the URL for Resource Management requests. |
| Service Endpoint | `https://management.core.windows.net/` | Specifies the endpoint for Service Management requests. |

## Select an Azure Cloud

Select the Azure Cloud to which Management Studio connects to discover and manage resources.

| Option | Description |
|--------|-------------|
| **Azure China Cloud**  | International public cloud service generally available in the China market. For more information, see [Azure China documentation](/azure/china/). |
| **Azure Cloud**  | Default public cloud. |
| **Azure US Government** | Dedicated cloud for US government agencies and their partners. For more information, see [Azure Government documentation](/azure/azure-government/). |
| **Custom** | Select **Custom** to enter custom URLs and DNS suffixes.  The options listed on the page become editable. |

## Service Addresses

| Option | Information | Description |
|--------|-------------|-------------|
| Azure Data Lake Services Endpoint | `azuredatalakeanalytics.net` | Specifies the Azure Data Lake Analytics catalog and job endpoint suffix. |
| Azure Data Lake Store File System Endpoint | `azuredatalakestore.net` | Specifies the Azure Data Lake Store files system endpoint suffix. | 
| Azure Key Vault Audience | `https://vault.azure.net/` | Specifies the audience for access tokens that authorize requests for Key Vault services. |
| Azure Key Vault DNS Suffix | `vault.azure.net` | Specifies the domain name suffix for Azure Key Vault servers. |
| Azure Key Vault Managed HSM Audience | `https://managedhsm.azure.net/` | Specifies the audience for access token that authorize requests for Key Vault Managed HSM (Hardware Security Module) services. |
| SQL Database DNS Suffix | `.database.windows.net` | Specifies the domain name suffix for Azure SQL Database servers. |
| Storage Endpoint | `core.windows.net` | Specifies the endpoint for storage access. The option includes, blobs, tables, queues, and file storages. |
| Traffic Manager DNS Suffix | `trafficmanager.net` | Specifies the domain name suffix for Azure Traffic Manager services. |
