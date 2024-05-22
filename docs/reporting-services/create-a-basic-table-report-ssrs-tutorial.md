---
title: "Create a basic table report (SSRS tutorial)"
description: Use the Report Designer tool in Visual Studio / SQL Server Data Tools (SSDT) and then create a SQL Server Reporting Services (SSRS) paginated report.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/21/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "walkthroughs [Reporting Services]"
  - "tutorials [Reporting Services]"
  - "reports [Reporting Services], creating"
#customer intent: As a SQL server user, I want to use SQL Server Data Tools (SSDT) to create a data connection so that I can create and publish a basic report.
---
# Create a basic table report (SSRS tutorial)

The tutorial helps you create a SQL Server Reporting Services (SSRS) paginated report. As you progress through the tutorial, you learn how to use the Report Designer tool in Visual Studio / SQL Server Data Tools (SSDT) to create a query table from data in the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database.

As you progress in this tutorial, you're going to learn how to:
  
- Create a report project
- Set up a data connection
- Define a query
- Add a table data region
- Format the report
- Group and total fields
- Preview the report
- Publish the report

## Prerequisites

Your system must have the following components installed to take this tutorial:

- [!INCLUDE[msconame-md](../includes/msconame-md.md)] SQL Server database engine.  
- SQL Server 2016 Reporting Services or later (SSRS).
- The [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database. For more information, see [Adventure Works Sample Databases](https://github.com/Microsoft/sql-server-samples/releases).
- [SQL Server Data Tools](../ssdt/download-sql-server-data-tools-ssdt.md) for Visual Studio along with the Reporting Services extension installed to enable access to the *Report Designer*.
  
You must also have read-only permissions to retrieve data from the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database.

**Estimated time to complete the tutorial:** 30 minutes.

## Related content

- [Lesson 1: Create a report server project &#40;Reporting Services&#41;](lesson-1-creating-a-report-server-project-reporting-services.md)
- [Lesson 2: Specify connection information &#40;Reporting Services&#41;](lesson-2-specifying-connection-information-reporting-services.md)
- [Lesson 3: Define a dataset for the table report &#40;Reporting Services&#41;](lesson-3-defining-a-dataset-for-the-table-report-reporting-services.md)
- [Lesson 4: Add a table to the report &#40;Reporting Services&#41;](lesson-4-adding-a-table-to-the-report-reporting-services.md)
- [Lesson 5: Format a report &#40;Reporting Services&#41;](lesson-5-formatting-a-report-reporting-services.md)
- [Lesson 6: Add grouping and totals &#40;Reporting Services&#41;](lesson-6-adding-grouping-and-totals-reporting-services.md)
[Reporting Services tutorials](reporting-services-tutorials-ssrs.md)

More questions? [Try asking the Reporting Services forum.](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
