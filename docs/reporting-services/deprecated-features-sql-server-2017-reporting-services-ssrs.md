---
title: "Deprecated features in SQL Server 2017 Reporting Services | Microsoft Docs"
description: This article describes features that will be deprecated in the next version of SQL Server Reporting Services.
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
monikerRange: ">= sql-server-2017 || = sqlallproducts-allversions"
---

# Deprecated features in SQL Server 2017 Reporting Services

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2017](../includes/ssrs-appliesto-2017.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../includes/ssrs-appliesto-not-pbirs.md)]

When we mark a feature as deprecated, it means:

- The feature is in maintenance mode only. We'll make no new changes, including changes related to interoperability with new features.
- We strive not to remove a deprecated feature from future releases, to make upgrades easier. However, in rare situations, we may choose to permanently remove the feature from Reporting Services if it limits future innovations.
- For new development work, we don't recommend using deprecated features.

## Features deprecated in the next version of SQL Server

The following SQL Server 2017 Reporting Services features will be deprecated in the next version of SQL Server. Don't use these features in new development work, and modify applications that currently use these features as soon as possible.

> [!NOTE]
> This list is identical to the SQL Server 2016 Reporting Services (13.x) list. There are no new deprecated or discontinued features announced for SQL Server 2017 Reporting Services (14.x).


| **Category** | **Deprecated feature** | **Replacement** |
| --- | --- | --- |
| Report Server | HTML 4.0 Renderer. | HTML 5 renderer |

## See also

[Discontinued functionality in SQL Server Reporting Services (SSRS)](discontinued-functionality-to-sql-server-reporting-services-in-sql-server.md)

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
