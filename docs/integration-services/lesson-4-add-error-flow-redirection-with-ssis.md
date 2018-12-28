---
title: "Lesson 4: Add Error Flow Redirection with SSIS | Microsoft Docs"
ms.custom: ""
ms.date: "12/27/2018"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 0c8dbda2-75e3-4278-9b4e-dcd220c92522
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Lesson 4: Add Error Flow Redirection with SSIS
To handle errors that may occur in the transformation process, [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] gives you the ability to decide on a per-component and per-column basis how to handle data that cannot be transformed. You can choose to ignore a failure in certain columns, redirect the entire failed row, or just fail the component. By default, all components in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] are configured to fail when errors occur. The failed component in turn causes the package to fail and processing then stops.  
  
Rather than letting failures stop package execution, you can configure and handle potential processing errors as they occur within the transformation. You can ignore failures, to ensure your package runs successfully, or you can redirect the failed row to another processing path where the data and the error can be persisted, examined, or reprocessed at a later time.  
  
In this lesson, you'll create a copy of the package that you developed in [Lesson 3: Add logging with SSIS](../integration-services/lesson-3-add-logging-with-ssis.md). Working with this new package, you'll create a corrupted version of one of the sample data files. The corrupted file will force a processing error to occur when you run the package.  
  
To handle the error data, you'll add and configure a Flat File destination that writes to a file any rows that fail to locate a lookup value in the Lookup Currency Key transformation.  
  
Before the error data is written to the file, you'll include a Script component that gets error descriptions. You'll then reconfigure the Lookup Currency Key transformation to redirect any data that could not be processed to the Script transformation.  
  
## Prerequisites

This tutorial uses a set of example packages and a sample database.

* To download all of the lesson packages for this tutorial:

    1.  Navigate to [Integration Services product samples](https://go.microsoft.com/fwlink/?LinkId=275027).

    2.  Select the **DOWNLOADS** tab.

    3.  Select the SQL2012.Integration_Services.Create_Simple_ETL_Tutorial.Sample.zip file.

* To install and deploy the **AdventureWorksDW2012** sample database, see [Reporting Services product samples on CodePlex](https://go.microsoft.com/fwlink/p/?LinkID=526910).

  
## Lesson task
This lesson contains the following tasks:  
  
-   [Step 1: Copy the Lesson 3 package](../integration-services/lesson-4-1-copying-the-lesson-3-package.md)  
  
-   [Step 2: Create a corrupted file](../integration-services/lesson-4-2-creating-a-corrupted-file.md)  
  
-   [Step 3: Add error flow redirection](../integration-services/lesson-4-3-adding-error-flow-redirection.md)  
  
-   [Step 4: Add a Flat File destination](../integration-services/lesson-4-4-adding-a-flat-file-destination.md)  
  
-   [Step 5: Test the Lesson 4 tutorial package](../integration-services/lesson-4-5-testing-the-lesson-4-tutorial-package.md)  
  
## Start the lesson  
[Step 1: Copy the Lesson 3 package](../integration-services/lesson-4-1-copying-the-lesson-3-package.md)  
  
  
  
