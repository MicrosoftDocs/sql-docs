---
description: "Lesson 3: Add Logging with SSIS"
title: "Lesson 3: Add Logging with SSIS | Microsoft Docs"
ms.custom: ""
ms.date: "01/04/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: tutorial
ms.assetid: 64cd24cc-ba8e-4bd7-b10b-6b80d8b04af6
author: chugugrace
ms.author: chugu
---
# Lesson 3: Add logging with SSIS

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]



[!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] includes logging features that let you troubleshoot and monitor package execution by providing a trace of task and container events. The logging features are flexible. You can enable logging at the package level, or on individual tasks or containers within the package. You select which events you want to log, and create multiple logs against a single package.  
  
Log providers create the logs. Each log provider can write logging information to different formats and destination types. [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides the following log providers:  
  
-   Text file  
  
-   [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)]  
  
-   Windows Event log  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]  
  
-   XML file  
  
In this lesson, you create a copy of the package that you created in [Lesson 2: Add Looping with SSIS](../integration-services/lesson-2-adding-looping-with-ssis.md). Working with this new package, you then add and configure logging to monitor specific events during package execution. If you haven't completed either of the previous lessons, you can also copy the completed Lesson 2 package included with the tutorial.  

> [!NOTE]
> If you haven't already, see the [Lesson 1 prerequisites](../integration-services/lesson-1-create-a-project-and-basic-package-with-ssis.md#prerequisites).

## Lesson tasks  
This lesson contains the following tasks:  
  
-   [Step 1: Copy the Lesson 2 package](../integration-services/lesson-3-1-copying-the-lesson-2-package.md)  
  
-   [Step 2: Add and configure logging](../integration-services/lesson-3-2-adding-and-configuring-logging.md)  
  
-   [Step 3: Test the Lesson 3 package](../integration-services/lesson-3-3-testing-the-lesson-3-tutorial-package.md)  
  
## Start the lesson  
[Step 1: Copy the Lesson 2 package](../integration-services/lesson-3-1-copying-the-lesson-2-package.md)  
  
