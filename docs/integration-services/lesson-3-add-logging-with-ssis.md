---
title: "Lesson 3: Add Logging with SSIS | Microsoft Docs"
ms.custom: ""
ms.date: "12/26/2018"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 64cd24cc-ba8e-4bd7-b10b-6b80d8b04af6
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Lesson 3: Add Logging with SSIS
[!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] includes logging features that let you troubleshoot and monitor package execution by providing a trace of task and container events. The logging features are flexible, and can be enabled at the package level or on individual tasks and containers within the package. You select which events you want to log, and create multiple logs against a single package.  
  
Logging is provided by a log provider. Each log provider can write logging information to different formats and destination types. [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides the following log providers:  
  
-   Text file  
  
-   [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)]  
  
-   Windows Event log  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]  
  
-   XML file  
  
In this lesson, you'll create a copy of the package that you created in [Lesson 2: Add Looping with SSIS](../integration-services/lesson-2-adding-looping-with-ssis.md). Working with this new package, you'll then add and configure logging to monitor specific events during package execution. If you have not completed any of the previous lessons, you can also copy the completed Lesson 2 package that is included with the tutorial.  

## Prerequisites

This tutorial uses a set of example packages and a sample database.

* To download all of the lesson packages for this tutorial:

    1.  Navigate to [Integration Services product samples](https://go.microsoft.com/fwlink/?LinkId=275027)

    2.  Select the **DOWNLOADS** tab.

    3.  Select the SQL2012.Integration_Services.Create_Simple_ETL_Tutorial.Sample.zip file.

* To install and deploy the **AdventureWorksDW2012** sample database, see [Reporting Services product samples on CodePlex](https://go.microsoft.com/fwlink/p/?LinkID=526910).

## Lesson tasks  
This lesson contains the following tasks:  
  
-   [Step 1: Copy the Lesson 2 package](../integration-services/lesson-3-1-copying-the-lesson-2-package.md)  
  
-   [Step 2: Add and configure Logging](../integration-services/lesson-3-2-adding-and-configuring-logging.md)  
  
-   [Step 3: Test the Lesson 3 tutorial package](../integration-services/lesson-3-3-testing-the-lesson-3-tutorial-package.md)  
  
## Start the lesson  
[Step 1: Copy the Lesson 2 package](../integration-services/lesson-3-1-copying-the-lesson-2-package.md)  
  
