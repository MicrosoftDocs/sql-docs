---
title: "Lesson 3: Add Logging with SSIS | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
applies_to: 
  - "SQL Server 2016"
ms.assetid: 64cd24cc-ba8e-4bd7-b10b-6b80d8b04af6
caps.latest.revision: 25
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Lesson 3: Add Logging with SSIS
[!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] includes logging features that let you troubleshoot and monitor package execution by providing a trace of task and container events. The logging features are flexible, and can be enabled at the package level or on individual tasks and containers within the package. You can select which events you want to log, and create multiple logs against a single package.  
  
Logging is provided by a log provider. Each log provider can write logging information to different formats and destination types. [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides the following log providers:  
  
-   Text file  
  
-   [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)]  
  
-   Windows Event log  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]  
  
-   XML file  
  
In this lesson, you will create a copy of the package that you created in [Lesson 2: Adding Looping with SSIS](../integration-services/lesson-2-adding-looping-with-ssis.md). Working with this new package, you will then add and configure logging to monitor specific events during package execution. If you have not completed any of the previous lessons, you can also copy the completed Lesson 2 package that is included with the tutorial.  
  
> [!IMPORTANT]  
> This tutorial requires the **AdventureWorksDW2012** sample database. For more information about how to install and deploy **AdventureWorksDW2012**, [Reporting Services Product Samples on CodePlex](http://go.microsoft.com/fwlink/p/?LinkID=526910)  
  
## Lesson Tasks  
This lesson contains the following tasks:  
  
-   [Step 1: Copying the Lesson 2 Package](../integration-services/lesson-3-1-copying-the-lesson-2-package.md)  
  
-   [Step 2: Adding and Configuring Logging](../integration-services/lesson-3-2-adding-and-configuring-logging.md)  
  
-   [Step 3: Testing the Lesson 3 Tutorial Package](../integration-services/lesson-3-3-testing-the-lesson-3-tutorial-package.md)  
  
## Start the Lesson  
[Step 1: Copying the Lesson 2 Package](../integration-services/lesson-3-1-copying-the-lesson-2-package.md)  
  
  
  
