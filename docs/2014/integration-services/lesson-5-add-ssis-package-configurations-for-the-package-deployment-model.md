---
title: "Lesson 5: Adding Package Configurations for the Package Deployment Model | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 1c10dd54-67cb-4b63-9e4d-aa6ff0452ecb
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Lesson 5: Adding Package Configurations for the Package Deployment Model
  Package configurations let you set run-time properties and variables from outside of the development environment. Configurations allow you to develop packages that are flexible and easy to both deploy and distribute. [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] offers the following configuration types:  
  
-   XML configuration file  
  
-   Environment variable  
  
-   Registry entry  
  
-   Parent package variable  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] table  
  
 In this lesson, you will modify the simple [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package that you created in [Lesson 4: Adding Error Flow Redirection](lesson-4-add-error-flow-redirection-with-ssis.md) to use the Package Deployment Model and take advantage of package configurations. You can also copy the completed Lesson 4 package that is included with the tutorial. Using the Package Configuration Wizard, you will create an XML configuration that updates the `Directory` property of the Foreach Loop container by using a package-level variable mapped to the Directory property. Once you have created the configuration file, you will modify the value of the variable from outside of the development environment and point the modified property to a new sample data folder. When you run the package again, the configuration file populates the value of the variable, and the variable in turn updates the `Directory` property. As a result, the package iterates through the files in the new data folder, rather than iterating through the files in the original folder that was hard-coded in the package.  
  
> [!IMPORTANT]  
>  This tutorial requires the **AdventureWorksDW2012** sample database. For more information about how to install and deploy **AdventureWorksDW2012**, see [Reporting Services Product Samples on CodePlex](https://go.microsoft.com/fwlink/?LinkID=526910).  
  
## Lesson Tasks  
 This lesson contains the following tasks:  
  
-   [Step 1: Copying the Lesson 4 Package](lesson-5-1-copying-the-lesson-4-package.md)  
  
-   [Step 2: Enabling and Configuring Package Configurations](lesson-5-2-enabling-and-configuring-package-configurations.md)  
  
-   [Step 3: Modifying the Directory Property Configuration Value](lesson-5-3-modifying-the-directory-property-configuration-value.md)  
  
-   [Step 4: Testing the Lesson 5 Tutorial Package](lesson-5-4-testing-the-lesson-5-tutorial-package.md)  
  
## Start the Lesson  
  
-   [Step 1: Copying the Lesson 4 Package](lesson-5-1-copying-the-lesson-4-package.md)  
  
  
