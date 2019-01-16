---
title: "Step 2: Convert the project to the Project Deployment Model | Microsoft Docs"
ms.custom: ""
ms.date: "01/11/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 80964293-f1f5-4da7-b1fb-00ab8c30c1c5
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Lesson 6-2: Convert the project to the Project Deployment Model

In this task, you use the Integration Services Project Conversion Wizard to convert the project to the Project Deployment Model.  
  
1.  On the **Project** menu, select **Convert to Project Deployment Model**.  
  
2.  On the **Integration Services Project Conversion Wizard** **Introduction** page, review the steps and then select **Next**.  
  
3.  On the **Select Packages** page, in the **Packages** list, clear all checkboxes except **Lesson 6.dtsx**, and then select **Next**.  
  
4.  On the **Specify Project Properties** page, select **Next**.  
  
5.  On the **Update Execute Package Task** page, select **Next**.  
  
6.  On the **Select Configurations** page, make sure the **Lesson 6.dtsx** package is selected in the **Configurations** list, then select **Next**.  
  
7.  On the **Create Parameters** page, make sure the **Lesson 6.dtsx** package is selected.  Verify the **Scope** is **Package** in the **Configuration Properties** list, and then select **Next**.  
  
8.  On the **Configure Parameters** page, verify that the values for **Name** and **Value** are the same ones specified in Lesson 5 for the variable and configuration value, then select **Next**.  
  
9. On the **Review** page, in the **Summary** pane, notice that the wizard used the information from the configuration file to set the **Properties** that are converted.  
  
10. Select **Convert**.  
  
    After the conversion completes, you see a warning message that the changes aren't saved until you save the project. Select **OK** to close the warning dialog.  
  
11. On the **Integration Services Project Conversion Wizard**, select **Close**.  
  
12. In **SQL Server Data Tools**, select the **File** menu, then select **Save** to save the converted package.  
  
13. Select the **Parameters** tab and verify that the package now contains a parameter for **VarFolderName**. That parameter value is the same path specified for the **New Sample Data** folder in Lesson 5's configuration file.  
  
## Go to next task
[Step 3: Test the Lesson 6 package](../integration-services/lesson-6-3-testing-the-lesson-6-package.md)  
  
  
  
