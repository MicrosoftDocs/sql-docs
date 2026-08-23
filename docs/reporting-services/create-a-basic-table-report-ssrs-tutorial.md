---
title: "Create a basic table report (SSRS tutorial)"
description: Use the Report Designer tool in Visual Studio / SQL Server Data Tools (SSDT) and then create a SQL Server Reporting Services (SSRS) paginated report.
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: tutorial
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "walkthroughs [Reporting Services]"
  - "tutorials [Reporting Services]"
  - "reports [Reporting Services], creating"
# customer intent: As a SQL server user, I want to use SQL Server Data Tools (SSDT) to create a data connection so that I can create and publish a basic report.
---
# Create a basic table report (SSRS tutorial)

This tutorial is a six-step process that helps you create a SQL Server Reporting Services (SSRS) paginated report. You learn how to use the Report Designer tool in Visual Studio/SQL Server Data Tools (SSDT) to create a query table from data in the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database.

The six parts in this tutorial show you how to:
  
- Create a report project.
- Set up a data connection.
- Define a query.
- Add a table data region.
- Format the report.
- Group and total fields.
- Preview the report.
- Publish the report.

## Prerequisites

Install or configure the following components and permissions:

- [!INCLUDE[msconame-md](../includes/msconame-md.md)] SQL Server database engine.  
- SQL Server 2016 Reporting Services or later (SSRS).
- The [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database. For more information, see [Adventure Works Sample Databases](https://github.com/Microsoft/sql-server-samples/releases).
- [SQL Server Data Tools](../ssdt/download-sql-server-data-tools-ssdt.md) for Visual Studio and the Reporting Services extension for access to the *Report Designer*.
- Read-only permissions to retrieve data from the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database.

**Estimated time to complete the tutorial**: 30 minutes.

## Related content

- [Tutorial: Create a report server project](tutorial-step-01-create-report-server-project-reporting-services.md)
- [Tutorial: Specify connection information (Reporting Services)](tutorial-step-02-specify-connection-information-reporting-services.md)
- [Tutorial: Define a dataset for the table report (Reporting Services)](tutorial-step-03-define-dataset-table-report-reporting-services.md)
- [Tutorial: Add a table to the report (Reporting Services)](tutorial-step-04-add-table-report-reporting-services.md)
- [Tutorial: Format a report (Reporting Services)](tutorial-step-05-format-report-reporting-services.md)
- [Tutorial: Add grouping and totals (Reporting Services)](tutorial-step-06-add-grouping-totals-reporting-services.md)
- [Reporting Services tutorials (SSRS)](reporting-services-tutorials-ssrs.md)
- [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&so.rt=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
