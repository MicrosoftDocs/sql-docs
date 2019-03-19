---
title: "Lesson 5: Add SSIS package configurations for the Package Deployment Model | Microsoft Docs"
ms.custom: ""
ms.date: "01/08/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 1c10dd54-67cb-4b63-9e4d-aa6ff0452ecb
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Lesson 5: Add SSIS package configurations for the Package Deployment Model

Package configurations let you set run-time properties and variables from outside the development environment. Configurations allow you to develop packages that are flexible and easy to both deploy and distribute. [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] offers the following configuration types:  
  
-   XML configuration file  
  
-   Environment variable  
  
-   Registry entry  
  
-   Parent package variable  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] table  
  
In this lesson, you modify the example [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package that you created in [Lesson 4: Add error flow redirection with SSIS](../integration-services/lesson-4-add-error-flow-redirection-with-ssis.md) to use the Package Deployment Model and take advantage of package configurations. You can also copy the completed Lesson 4 package  included with this tutorial. 

Using the Package Configuration Wizard, you create an XML configuration that updates the **Directory** property of the Foreach Loop container. You use a package-level variable mapped to the **Directory** property. After you create the configuration file, you modify the value of the variable from outside the development environment to a new sample data folder path. When you run the package again, the configuration file populates the value of the variable, and the variable in turn updates the **Directory** property. The package then iterates through the files in the new data folder, rather than in the original hard-coded folder.  
  
> [!NOTE]
> If you haven't already, see the [Lesson 1 prerequisites](../integration-services/lesson-1-create-a-project-and-basic-package-with-ssis.md#prerequisites).
  
## Lesson tasks  
This lesson contains the following tasks:  
  
-   [Step 1: Copy the Lesson 4 package](../integration-services/lesson-5-1-copying-the-lesson-4-package.md)  
  
-   [Step 2: Enable and configure package configurations](../integration-services/lesson-5-2-enabling-and-configuring-package-configurations.md)  
  
-   [Step 3: Modify the directory property configuration value](../integration-services/lesson-5-3-modifying-the-directory-property-configuration-value.md)  
  
-   [Step 4: Test the Lesson 5 package](../integration-services/lesson-5-4-testing-the-lesson-5-tutorial-package.md)  
  
## Start the lesson  
  
-   [Step 1: Copy the Lesson 4 package](../integration-services/lesson-5-1-copying-the-lesson-4-package.md)  
  
  
  
