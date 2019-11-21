---
title: "Deprecated features in SQL Server 2019 Reporting Services | Microsoft Docs"
ms.date: 11/20/2019
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: reporting-services

ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services, backward compatibility"
  - "deprecated features [Reporting Services]"
  - "HTML OWC rendering extension [Reporting Services]"
  - "Report Server Web service, endpoints"
ms.assetid: 3876c01e-f81d-4cce-9104-5106a8c369e6
author: maggiesMSFT
ms.author: maggies
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---

# Deprecated features in SQL Server 2019 Reporting Services

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2019-and-later](../includes/ssrs-appliesto-2019-and-later.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../includes/ssrs-appliesto-not-pbirs.md)]

When a feature is marked deprecated, it means:

- The feature is in maintenance mode only. No new changes will be done, including those related to interoperability with new features.
- We strive not to remove a deprecated feature from future releases, to make upgrades easier. However, in rare situations, we may choose to permanently remove the feature from Reporting Services if it limits future innovations.
- For new development work, we don't recommend using deprecated features.

**Features deprecated in a future version of SQL Server**

The following SQL Server Reporting Services features are supported in the next version of SQL Server but will be deprecated in a later version. The specific version of SQL Server hasn't been determined.

| **Category** | **Deprecated feature** | **Replacement** |
| --- | --- | --- |
| Report Server | Report Part Gallery | None |
| Report Server | Mobile Reports and Mobile Report Publisher | Power BI reports in Power BI Report Server offer mobile capabilities. |
| Report Server | XLS and DOC render formats | XLSX and DOCX formats are available and supported. |
| Report Server | Atom Data Feed | oData feed support is available for shared datasets in SSRS and Power BI Report Server. |

## See also

[Discontinued functionality in SQL Server 2019 Reporting Services (SSRS)](discontinued-functionality-sql-server-reporting-services-2019.md)

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
