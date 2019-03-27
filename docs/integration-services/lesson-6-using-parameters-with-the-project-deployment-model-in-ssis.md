---
title: "Lesson 6: Use parameters with the Project Deployment Model in SSIS | Microsoft Docs"
ms.custom: ""
ms.date: "01/11/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 9216f18c-1762-4f2d-8c22-bd0ab7107555
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 6: Use parameters with the Project Deployment Model in SSIS

SQL Server 2012 introduced a new deployment model where you can deploy your projects to the Integration Services server. The Integration Services server enables you to manage and run packages, and to configure runtime values for packages.  
  
In this lesson, you modify the package that you created in [Lesson 5: Add SSIS package configurations for the Package Deployment Model](../integration-services/lesson-5-add-ssis-package-configurations-for-the-package-deployment-model.md) to use the Project Deployment Model. You replace the configuration value with a parameter to specify the sample data location. You can also copy the completed Lesson 5 package that is included with the tutorial.  
  
By using the Integration Services Project Configuration Wizard, you convert the project to the Project Deployment Model. This model uses a parameter rather than a configuration value to set the Directory property. This lesson partially covers the steps you would follow to convert existing SSIS packages to the new Project Deployment Model.  
  
When you run the package again, the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server uses the parameter to populate the value of the variable. The variable in turn updates the Directory property. The package iterates through the files in the data folder specified by the new parameter.  
  
> [!NOTE]
> If you haven't already, see the [Lesson 1 prerequisites](../integration-services/lesson-1-create-a-project-and-basic-package-with-ssis.md#prerequisites).
    
## Lesson tasks  
This lesson contains the following tasks:  
  
1.  [Step 1: Copy the Lesson 5 package](../integration-services/lesson-6-1-copying-the-lesson-5-package.md)  
  
2.  [Step 2: Convert the project to the Project Deployment Model](../integration-services/lesson-6-2-converting-the-project-to-the-project-deployment-model.md)  
  
3.  [Step 3: Test the Lesson 6 package](../integration-services/lesson-6-3-testing-the-lesson-6-package.md)  
  
4.  [Step 4: Deploy the Lesson 6 package](../integration-services/lesson-6-4-deploying-the-lesson-6-package.md)  
  
## Start the lesson  
[Step 1: Copy the Lesson 5 package](../integration-services/lesson-6-1-copying-the-lesson-5-package.md)  
  
