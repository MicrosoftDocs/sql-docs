---
title: SQL Server Options page - Azure Services
description: Options (Azure Services)
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords: 
  - VS.ToolsOptionsPages.Azure_Services.Azure_Cloud
dev_langs: 
  - TSQL
author: markingmyname
ms.author: maghan
ms.date: 01/15/2021
---

# Options (Azure Services)

Use this page to specify options related to Azure cloud services. To access this dialog box, go to **Tools > Options > Azure Services** from the top menu bar.

## Miscellaneous

| Option | Information | Description |
|--------|-------------|-------------|
| ADAL Output Window Trace Level | **Information** <br> **None** <br> **Verbose** <br> **Warning** | The level of Azure Active Directory (AAD) login tracing to send to the Output window. |
| Azure Data Factory Portal URL | `https://adf.azure.com` | Specifies the URL for the Azure Data Factory portal. |
| Enable conditional access to Azure SQL Database (EXPERIMENTAL) | **True** <br> **False** | EXPERIMENTAL: If true, requests for access tokens for Azure SQL Database that includes a claim for access to the selected server. Setting may require a restart of SSMS to take effect. |
| Gallery Endpoint | `https://gallery.azure.com` | Specifies the endpoint for the Resource Manager gallery of deployment templates. |
| Graph Audience | `https://graph.windows.net` | Graph Endpoint Resource ID. |
| Graph Endpoint | `https://graph.windows.net` | Specifies the URL for Azure Active Directory Graph requests. |
| Management Portal URL | `https://portal.azure.com` | Specifies the URL for the Management Portal. |
| Publish Setting File URL | `https://go.microsoft.com/fwlink/?LinkID=335839` | Specifies the URL from which the `.publishsettings` file can be downloaded. |
| SQL Database Service Principal Name | `https://database.windows.net/` | The Azure SQL Database SPN to obtain a token when using AAD authentication. Also the audience of the JSON Web Token (JWT) for server-side JSON Web Token (JWT) parsing/validation. |

## Resource Management

| Option | Information | Description |
|--------|-------------|-------------|
| Active Directory Authority | `https://login.microsoftonline.com` | Specifies the base authority for Azure Active Directory (AAD) authentication. |
| Active Directory Service Endpoint Resource ID | `https://management.core.windows.net` | Specifies the audience for tokens that authenticate requests to Azure Resource Manager (ARM) or Service Management (RDFE) endpoints. |
| Resource Manager Endpoint | `https://management.azure.com` | Specifies the URL for REsource Management requests. |
| Service Endpoint | `https://management.core.windows.net` | Specifies the endpoint for Service Management requests. |

## Select an Azure Cloud

| Option | Information | Description |
|--------|-------------|-------------|
| Name | **Azure China Cloud** <br><br> **Azure Cloud** <br><br> **Azure German Cloud** <br><br> **Azure US Government** <br><br> **Custom** | Select the Azure Cloud to which Management Studio connects to discover and manage resources. Select **Custom** to provide your own URLs and DNS suffixes. |

## Service Addresses

| Option | Information | Description |
|--------|-------------|-------------|
| Azure Data Lake Services Endpoint | `azuredatalakeanalytics.net` | Specifies the Azure Data Lake Analytics catalog and job endpoint suffix. |
| Azure Data Lake Store File System Endpoint | `azuredatalakestore.net` | Specifies the Azure Data Lake Store files system endpoint suffix. | 
| Azure Key Vault Audience | `https://vault.azure.net` | Specifies the audience for access tokens that authorize requests for Key Vault services. |
| Azure Key Vault DNS Suffix | `vault.azure.net` | Specifies the domain name suffix for Azure Key Vault servers. |
| SQL Database DNS Suffix | `database.windows.net` | Specifies the domain name suffix for Azure SQL Database servers. |
| Storage Endpoint | `core.windows.net` | Specifies the endpoint for storage access. The option includes, blobs, tables, queues, and file storages. |
| Traffic Manager DNS Suffix | `trafficmanager.net` | Specifies the domain name suffix for Azure Traffic Manager services. |