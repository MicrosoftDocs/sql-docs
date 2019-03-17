---
title: "Create a Data-Driven Subscription (SSRS Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "subscriptions [Reporting Services], tutorials"
  - "walkthroughs [Reporting Services]"
  - "data-driven subscriptions"
ms.assetid: 79ab0572-43e9-4dc4-9b5a-cd8b627b8274
author: markingmyname
ms.author: maghan
manager: kfile
---
# Create a Data-Driven Subscription (SSRS Tutorial)
  [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] provides data-driven subscriptions so that you can customize the distribution of a report based on dynamic subscriber data. Data-driven subscriptions are intended for the following kinds of scenarios:  
  
-   Distributing reports to a large recipient pool whose membership may change from one distribution to the next. For example, distribute a monthly report to all current customers.  
  
-   Distributing reports to a specific group of recipients based on predefined criteria. For example, send a sales performance report to the top ten sales managers in an organization.  
  
## What You Will Learn  
 This tutorial shows you how to use data-driven subscriptions using a simple example to illustrate the concepts.  
  
 This tutorial is divided into three lessons:  
  
 [Lesson 1: Creating a Sample Subscriber Database](lesson-1-creating-a-sample-subscriber-database.md)  
 In this lesson you will learn how to create a local [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database that contains subscriber information.  
  
 [Lesson 2: Modifying the Report Data Source Properties](lesson-2-modifying-the-report-data-source-properties.md)  
 In this lesson, you will learn how to modify report data source properties so that the report can run unattended. Unattended processing requires stored credentials. You will also modify the report dataset to include a parameter that is supplied by the subscriber data.  
  
 [Lesson 3: Defining a Data-Driven Subscription](lesson-3-defining-a-data-driven-subscription.md)  
 In this lesson you will learn how to define a data-driven subscription. This lesson guides you through each page in the Data-Driven Subscription Wizard.  
  
## Requirements  
 Data-driven subscriptions are typically created and maintained by report server administrators. The ability to create data-driven subscriptions requires expertise in building queries, knowledge of data sources that contain subscriber data, and elevated permissions on a report server.  
  
 The tutorial will use the report created in the tutorial [Create a Basic Table Report &#40;SSRS Tutorial&#41;](create-a-basic-table-report-ssrs-tutorial.md) and data from [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)]  
  
 Your system must have the following installed to use this tutorial:  
  
-   An edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that supports data-driven subscriptions. For more information, see [Editions and Components of SQL Server 2014](../sql-server/editions-and-components-of-sql-server-2016.md).  
  
-   The report server must be running in native mode. The user interface described in this tutorial is based on a native mode report server. Subscriptions are supported on SharePoint mode report servers but the user interface will be different than what is described in this tutorial.  
  
-   SQL Server Agent service must be running.  
  
-   A report that includes parameters. This tutorial assumes the sample report, `Sales Orders` you create using the tutorial [Create a Basic Table Report &#40;SSRS Tutorial&#41;](create-a-basic-table-report-ssrs-tutorial.md).  
  
-   The [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] sample database, which provides data to the sample report.  
  
-   A role assignment that includes the Manage all subscriptions task on the sample report. This task is required for defining a data-driven subscription. If you are an administrator on the computer, the default role assignment for local administrators provides the permissions necessary for creating data-driven subscriptions. For more information, see [Granting Permissions on a Native Mode Report Server](security/granting-permissions-on-a-native-mode-report-server.md).  
  
-   A shared folder for which you have write permissions. The shared folder must be accessible over a network connection.  
  
 **Estimated time to complete the tutorial:** 30 minutes. An additional 30 minutes if you have not completed the basic report tutorial.  
  
## See Also  
 [Data-Driven Subscriptions](subscriptions/data-driven-subscriptions.md)   
 [Create a Basic Table Report &#40;SSRS Tutorial&#41;](create-a-basic-table-report-ssrs-tutorial.md)  
  
  
