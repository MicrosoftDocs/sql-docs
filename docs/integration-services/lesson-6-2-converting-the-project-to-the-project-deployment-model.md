---
title: "Step 2: Converting the Project to the Project Deployment Model | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
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
# Lesson 6-2 - Converting the Project to the Project Deployment Model
In this task, you will use the Integration Services Project Conversion Wizard to convert the project to the Project Deployment Model.  
  
### Converting the Project to the Project Deployment Model  
  
1.  On the Project Menu, click Convert to Project Deployment Model.  
  
2.  On the Integration Services Project Conversion Wizard Introduction page, review the steps then click Next.  
  
3.  On the Select Packages page, in the Packages list, clear all checkboxes except Lesson 6.dtsx then click Next.  
  
4.  On the Specify Project Properties page, click Next.  
  
5.  On the Update Execute Package Task page click Next.  
  
6.  On the Select Configurations page, make sure the Lesson 6.dtsx package is selected in the Configurations list, then click Next.  
  
7.  On the Create Parameters page make sure the Lesson 6.dtsx package is selected, and the Scope is set to Package, in the Configuration Properties List, then Click Next.  
  
8.  On the Configure Parameters page verify that the values for Name and Value are the same name and value specified in Lesson 5 for the variable and configuration value, then click Next.  
  
9. On the Review page, in the Summary pane, notice that the wizard has used the information from the configuration file to set the Properties to be converted.  
  
10. Click Convert.  
  
    When the conversion completes a message is displayed warning that the changes are not saved until the project is saved in Visual Studio. Click OK on the warning dialog.  
  
11. On the Integration Services Project Conversion Wizard click Close.  
  
12. In SQL Server Data Tools, click the File menu, then click Save to save the converted package.  
  
13. Click the Parameters Tab and verify that the package now contains a parameter for VarFolderName and that the value is the same path specified for the New Sample Data folder from the Lesson 5 configuration file.  
  
## Next Task in Lesson  
[Step 3: Testing the Lesson 6 Package](../integration-services/lesson-6-3-testing-the-lesson-6-package.md)  
  
  
  
