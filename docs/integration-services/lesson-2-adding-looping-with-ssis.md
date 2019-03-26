---
title: "Lesson 2: Add looping with SSIS | Microsoft Docs"
ms.custom: ""
ms.date: "01/03/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 01f2ed61-1e5a-4ec6-b6a6-2bd070c64077
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 2: Add looping with SSIS

In [Lesson 1: Create a project and basic package with SSIS](../integration-services/lesson-1-create-a-project-and-basic-package-with-ssis.md), you created a package that extracts data from a single flat file source. The data is then transformed using Lookup transformations. Finally, the package loads the data into a copy of the **FactCurrencyRate** fact table in the **AdventureWorksDW2012** sample database.  
  
An extract, transform, and load (ETL) process typically extracts data from multiple flat file sources. Extracting data from multiple sources requires an iterative control flow. [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] can easily add iteration or looping to packages.  
  
[!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides two types of containers for looping through packages: the Foreach Loop container and the For Loop container. The Foreach Loop container uses an enumerator for the looping, while the For Loop container typically uses a variable expression. This lesson uses the Foreach Loop container.  
  
The Foreach Loop container enables a package to repeat the control flow for each member of a specified enumerator. With the Foreach Loop container, you can enumerate:  
  
-   ADO recordset rows  
  
-   ADO .Net schema information  
  
-   File and directory structures  
  
-   System, package, and user variables  
  
-   Enumerable objects in a variable  
  
-   Items in a collection  
  
-   Nodes in an XML Path Language (XPath) expression  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Management Objects (SMO)  
  
In this lesson, you modify Lesson 1's example ETL package to use a Foreach Loop container, and set a user-defined package variable for the package. That variable is then used to iterate through the matching files in the sample folder.   
  
In this lesson, you won't modify the data flow, only the control flow.  
  
> [!NOTE]  
> If you haven't already, see the [Lesson 1 prerequisites](../integration-services/lesson-1-create-a-project-and-basic-package-with-ssis.md#prerequisites).

## Lesson tasks  
This lesson contains the following tasks:  
  
-   [Step 1: Copy the Lesson 1 package](../integration-services/lesson-2-1-copying-the-lesson-1-package.md)  
  
-   [Step 2: Add and configure the Foreach Loop container](../integration-services/lesson-2-2-adding-and-configuring-the-foreach-loop-container.md)  
  
-   [Step 3: Modify the Flat File connection manager](../integration-services/lesson-2-3-modifying-the-flat-file-connection-manager.md)  
  
-   [Step 4: Test the Lesson 2 tutorial package](../integration-services/lesson-2-4-testing-the-lesson-2-tutorial-package.md)  
  
## Start the lesson  
[Step 1: Copy the Lesson 1 package](../integration-services/lesson-2-1-copying-the-lesson-1-package.md)  
  
## See also  
[For Loop container](../integration-services/control-flow/for-loop-container.md)  
  
  
  
