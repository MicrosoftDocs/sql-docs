---
title: SQL Server Integration Services
description: Learn about SQL Server Integration Services, Microsoft's platform for building enterprise-level data integration and data transformations solutions.
ms.service: sql
ms.subservice: integration-services
ms.topic: overview
keywords: 
  - "SSIS"
helpviewer_keywords: 
  - "SSIS"
  - "DTS [Integration Services]"
  - "SQL Server Integration Services"
  - "Integration Services"
  - "DTS [Integration Services], about Integration Services"
  - "data integration [Integration Services]"
  - "Data Transformation Services"
author: chugugrace
ms.author: chugu
ms.reviewer: ""
ms.custom: FY22Q2Fresh
ms.date: 12/10/2021
---

# SQL Server Integration Services

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]

SQL Server [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] is a platform for building enterprise-level data integration and data transformations solutions. Use [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] to solve complex business problems by copying or downloading files, loading data warehouses, cleansing and mining data, and managing [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] objects and data.

## Capabilities

[!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] can [extract and transform data](./data-flow/data-flow.md) from a wide variety of sources such as XML data files, flat files, and relational data sources, and then load the data into one or more destinations.

[!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] includes:

- A rich set of built-in [tasks](./control-flow/integration-services-tasks.md) and [transformations](./data-flow/transformations/integration-services-transformations.md).
- [Graphical tools](ssis-designer.md) for building packages.
- An SSIS [Catalog database](./catalog/ssis-catalog.md) to store, run, and manage packages.

You can use the graphical [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] tools to create solutions without writing a single line of code. You can also program the extensive [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] object model to create packages programmatically and code custom tasks and other package objects.

## Get started

You can start by installing SQL Server Integration Services, which is part of SQL Server setup. 

For installation instructions and guidance, see [Install Integration Services](install-windows/install-integration-services.md).

## :::image type="icon" source="../includes/media/info-tip.svg" border="false"::: Get help

- [Get help in the SSIS forum](/answers/topics/sql-server-integration-services.html)
- [Get help on Stack Overflow](https://stackoverflow.com/questions/tagged/ssis)  
- [Follow the SSIS team blog](https://blogs.msdn.microsoft.com/ssis/)
- [Report issues & request features](https://feedback.azure.com/forums/908035-sql-server)
- [Get the docs on your PC](../sql-server/sql-server-offline-documentation.md)
