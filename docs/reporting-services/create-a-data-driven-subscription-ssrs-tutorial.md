---
title: "Create a data-driven subscription (SSRS Tutorial)"
description: Learn about data-driven subscriptions through an example that creates a data-driven subscription used to generate and save filtered report output to a file share.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/10/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "subscriptions [Reporting Services], tutorials"
  - "walkthroughs [Reporting Services]"
  - "data-driven subscriptions"
#customer intent: As a SQL Server user, I want to create data-driven subscriptions using SQL Server Reporting Services (SSRS) to automate and customize report distribution based on dynamic subscriber data.
---
# Create a data-driven subscription (SSRS Tutorial)

This [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] tutorial is a three-step process that teaches you the concepts of data-driven subscriptions. You walk through a simple example that creates a data-driven subscription to generate and save filtered report output to a file share. 
[!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] data-driven subscriptions allow you to customize and automate the distribution of a report based on dynamic subscriber data. [Data-driven subscriptions](../reporting-services/subscriptions/data-driven-subscriptions.md) are intended for the following kinds of scenarios:  
  
-   Distributing reports to a large recipient pool whose membership might change from one distribution to the next. For example, email a monthly report to all current customers.  
-   Distributing reports to a specific group of recipients based on predefined criteria. For example, send a sales performance report to all of the sales managers in an organization.
- Automate the generation of reports in a wide variety of formats, for example `.xlsx` and `.pdf`.  
  
The following diagram illustrates the basic workflow of the tutorial:

| Step    | Description |
| --------|------------ |
| (1)     | The subscription configuration notes the source report, schedule, and the field mapping to the subscribers Database. |
| (2)     | The OrderInfo table contains four order numbers to use for filtering, 1 per file. The table also contains the file formats for the generated reports. |
| (3)     | Information from the Adventureworks database is filtered and return in the report. |
| (4)     | The reports are created in the file formats specified in the Orderinfo table. |

  :::image type="content" source="../reporting-services/media/ssrs-tutorial-datadriven-flow.png" alt-text="Diagram that shows the basic workflow of the tutorial.":::
  
## Prerequisites 
  
- Knowledge of building queries, knowledge of data sources that contain subscriber data, and elevated permissions on a report server.
- The *Sales order* report created in the tutorial [Create a basic table report &#40;SSRS tutorial&#41;](../reporting-services/create-a-basic-table-report-ssrs-tutorial.md) and data from the sample database **AdventureWorks2022**.
- An edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] installed on your local computer that supports data-driven subscriptions. For more information, see [Editions and supported features of SQL Server 2022](../sql-server/editions-and-components-of-sql-server-2022.md).
- The report server must be running in native mode. The user interface described in this tutorial is based on a native mode report server. Subscriptions are supported on SharePoint mode report servers but the user interface is different than what is described in this tutorial.  
- SQL Server Agent service must be running.    
- A report that includes parameters. This tutorial assumes the sample report, `Sales Orders` you create using the tutorial [Create a basic table report &#40;SSRS tutorial&#41;](../reporting-services/create-a-basic-table-report-ssrs-tutorial.md).  
- The [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] sample database, which provides data to the sample report.  
- A [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] role assignment that includes the Manage all subscriptions task on the sample report. This task is required for defining a data-driven subscription. If you're an administrator on the computer, the default role assignment for local administrators provides the permissions necessary for creating data-driven subscriptions. For more information, see [Grant permissions on a native mode report server](../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md).  
- A shared folder for which you have write permissions. The shared folder must be accessible over a network connection.  
  
**Estimated time to complete the tutorial:** 30 minutes. An extra 30 minutes if you need to complete the basic report tutorial.  
  
## Related content

- [Step 1: Create a sample subscriber database](../reporting-services/lesson-1-creating-a-sample-subscriber-database.md)
- [Step 2: Configure report data source properties](../reporting-services/lesson-2-modifying-the-report-data-source-properties.md)
- [Step 3: Define a data-driven subscription](../reporting-services/lesson-3-defining-a-data-driven-subscription.md)