---
description: "Lesson 4: Add Error Flow Redirection with SSIS"
title: "Lesson 4: Add Error Flow Redirection with SSIS | Microsoft Docs"
ms.custom: ""
ms.date: "01/07/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: tutorial
ms.assetid: 0c8dbda2-75e3-4278-9b4e-dcd220c92522
author: chugugrace
ms.author: chugu
---
# Lesson 4: Add error flow redirection with SSIS

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]



To handle errors that may occur in the transformation process, [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] lets you decide on a per-component and per-column basis how to handle data that [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] can't transform. You can choose to ignore a failure in certain columns, redirect the entire failed row, or fail the component. By default, components in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] are configured to fail when errors occur. The failed component in turn causes the package to fail and processing then stops.  
  
Rather than letting failures stop package execution, you may configure and handle potential processing errors as they occur. One option is to ignore failures altogether so your package always runs successfully. You can also redirect the failed row to another processing path, where the data and the error can be persisted, examined, or reprocessed.  
  
In this lesson, you create a copy of the package that you developed in [Lesson 3: Add logging with SSIS](../integration-services/lesson-3-add-logging-with-ssis.md). Working with this new package, you create a corrupted version of one of the sample data files. The corrupted file causes a processing error to occur when you run the package.  
  
To handle the error data, you add and configure a Flat File destination that writes any failed rows to an error file. 
  
Before [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] writes error data to the file, you include a Script component that gets error descriptions. You then reconfigure the Lookup Currency Key transformation to redirect any data that couldn't be processed to the Script transformation.  
  
## Prerequisites

> [!NOTE]
> If you haven't already, see the [Lesson 1 prerequisites](../integration-services/lesson-1-create-a-project-and-basic-package-with-ssis.md#prerequisites).
 
## Lesson task
This lesson contains the following tasks:  
  
-   [Step 1: Copy the Lesson 3 package](../integration-services/lesson-4-1-copying-the-lesson-3-package.md)  
  
-   [Step 2: Create a corrupted file](../integration-services/lesson-4-2-creating-a-corrupted-file.md)  
  
-   [Step 3: Add error flow redirection](../integration-services/lesson-4-3-adding-error-flow-redirection.md)  
  
-   [Step 4: Add a Flat File destination](../integration-services/lesson-4-4-adding-a-flat-file-destination.md)  
  
-   [Step 5: Test the Lesson 4 tutorial package](../integration-services/lesson-4-5-testing-the-lesson-4-tutorial-package.md)  
  
## Start the lesson  
[Step 1: Copy the Lesson 3 package](../integration-services/lesson-4-1-copying-the-lesson-3-package.md)  
  
  
  
