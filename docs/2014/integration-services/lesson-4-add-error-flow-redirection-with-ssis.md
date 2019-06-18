---
title: "Lesson 4: Adding Error Flow Redirection | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 0c8dbda2-75e3-4278-9b4e-dcd220c92522
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 4: Adding Error Flow Redirection
  To handle errors that may occur in the transformation process, [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] gives you the ability to decide on a per component and per column basis how to handle data that cannot be transformed. You can choose to ignore a failure in certain columns, redirect the entire failed row, or just fail the component. By default, all components in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] are configured to fail when errors occur. Failing a component, in turn, causes the package to fail and all subsequent processing to stop.  
  
 Instead of letting failures stop package execution, it is good practice to configure and handle potential processing errors as they occur within the transformation. While you might choose to ignore failures to ensure your package runs successfully, it is often better to redirect the failed row to another processing path where the data and the error can be persisted, examined and reprocessed at a later time.  
  
 In this lesson, you will create a copy of the package that you developed in [Lesson 3: Adding Logging](lesson-3-add-logging-with-ssis.md). Working with this new package, you will create a corrupted version of one of the sample data files. The corrupted file will force a processing error to occur when you run the package.  
  
 To handle the error data, you will add and configure a Flat File destination that will write any rows that fail to locate a lookup value in the Lookup Currency Key transformation to a file.  
  
 Before the error data is written to the file, you will include a Script component that uses script to get error descriptions. You will then reconfigure the Lookup Currency Key transformation to redirect any data that could not be processed to the Script transformation.  
  
> [!IMPORTANT]  
>  This tutorial requires the **AdventureWorksDW2012** sample database. For more information about how to install and deploy **AdventureWorksDW2012**, see [Reporting Services Product Samples Project on CodePlex](https://go.microsoft.com/fwlink/p/?LinkId=526910).  
  
## Tasks in Lesson  
 This lesson contains the following tasks:  
  
-   [Step 1: Copying the Lesson 3 Package](lesson-4-1-copying-the-lesson-3-package.md)  
  
-   [Step 2: Creating a Corrupted File](lesson-4-2-creating-a-corrupted-file.md)  
  
-   [Step 3: Adding Error Flow Redirection](lesson-4-3-adding-error-flow-redirection.md)  
  
-   [Step 4: Adding a Flat File Destination](lesson-4-4-adding-a-flat-file-destination.md)  
  
-   [Step 5: Testing the Lesson 4 Tutorial Package](lesson-4-5-testing-the-lesson-4-tutorial-package.md)  
  
## Start the Lesson  
 [Step 1: Copying the Lesson 3 Package](lesson-4-1-copying-the-lesson-3-package.md)  
  
  
