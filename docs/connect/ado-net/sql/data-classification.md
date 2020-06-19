---
title: "Data discovery and classification in SqlClient"
description: "Describes how to check if a SQL Server database supports data classification and how to access data classification information through a SqlDataReader object."
ms.date: "06/15/2020"
dev_langs: 
  - "csharp"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: johnnypham
ms.author: v-jopha
ms.reviewer: 
---
# Data discovery and classification in SqlClient

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

[Data Discovery & Classification](https://docs.microsoft.com/sql/relational-databases/security/sql-data-discovery-and-classification?view=sql-server-2017) is a set of advanced services for discovering, classifying, labeling & reporting the sensitive data in your databases. SqlClient provides an API exposing read-only Data Discovery and Classification information when the underlying source supports the feature. This information is accessed through SqlDataReader.

This sample application demonstrates how to access the Data Classification properties of SqlDataReader.

[!code-csharp [SqlDataReader_DataDiscoveryAndClassification#1](~/../sqlclient/doc/samples/SqlDataReader_DataDiscoveryAndClassification.cs#1)]

**See also**  

 [SQL Server features and ADO.NET](sql-server-features-adonet.md)   
