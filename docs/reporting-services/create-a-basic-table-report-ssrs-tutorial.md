---
title: "Create a Basic Table Report (SSRS Tutorial) | Microsoft Docs"
description: Use the Report Designer tool in Visual Studio / SQL Server Data Tools (SSDT) and then create a SQL Server Reporting Services (SSRS) paginated report. 
ms.date: 04/16/2019
ms.service: reporting-services
ms.subservice: reporting-services

ms.topic: conceptual
helpviewer_keywords: 
  - "walkthroughs [Reporting Services]"
  - "tutorials [Reporting Services]"
  - "reports [Reporting Services], creating"
ms.assetid: 3b539b4b-26f2-4c0b-b506-80f175679a46
author: maggiesMSFT
ms.author: maggies
---
# Create a Basic Table Report (SSRS Tutorial)

In this tutorial, you use the *Report Designer* tool in Visual Studio / SQL Server Data Tools (SSDT). You create a SQL Server Reporting Services (SSRS) paginated report. The report contains a query table, created from data in the AdventureWorks2016 database.

As you progress in this tutorial, you're going to learn how to:
  
- create a report project.
- set up a data connection.
- define a query.
- add a table data region.
- format the report.
- group and total fields.
- preview the report.
- optionally publish the report.

## Requirements

Your system must have the following components installed to take this tutorial:

- [!INCLUDE[msconame-md](../includes/msconame-md.md)] SQL Server database engine.  
- SQL Server 2016 Reporting Services or later (SSRS).
- The AdventureWorks2016 database.  For more information, see [Adventure Works Sample Databases](https://github.com/Microsoft/sql-server-samples/releases).
- [SQL Server Data Tools](../ssdt/download-sql-server-data-tools-ssdt.md) for Visual Studio along with the Reporting Services extension installed to enable access to the *Report Designer*.
  
You must also have read-only permissions to retrieve data from the AdventureWorks2016 database.

**Estimated time to complete the tutorial:** 30 minutes.

## Next steps

[Lesson 1: Creating a Report Server Project &#40;Reporting Services&#41;](lesson-1-creating-a-report-server-project-reporting-services.md)

[Lesson 2: Specifying Connection Information &#40;Reporting Services&#41;](lesson-2-specifying-connection-information-reporting-services.md)

[Lesson 3: Defining a Dataset for the Table Report &#40;Reporting Services&#41;](lesson-3-defining-a-dataset-for-the-table-report-reporting-services.md)

[Lesson 4: Adding a Table to the Report &#40;Reporting Services&#41;](lesson-4-adding-a-table-to-the-report-reporting-services.md)

[Lesson 5: Formatting a Report &#40;Reporting Services&#41;](lesson-5-formatting-a-report-reporting-services.md)

[Lesson 6: Adding Grouping and Totals &#40;Reporting Services&#41;](lesson-6-adding-grouping-and-totals-reporting-services.md)

## See also

[Reporting Services Tutorials](reporting-services-tutorials-ssrs.md)
More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)