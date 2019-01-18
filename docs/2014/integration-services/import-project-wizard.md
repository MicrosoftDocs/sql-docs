---
title: "Import Project Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.importprojectwizard.f1"
ms.assetid: 9247ad6c-4bd1-43ab-b347-583181cb9917
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Import Project Wizard
  Use the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] Import Project Wizard create a new Integration Services project based on an existing one. Import projects that have already been deployed to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] catalog or import projects from a project deployment file (.ispac extension).  
  
### To create a project based on a project in .ispac file or in catalog  
  
1.  Click **File**, point to **New**, and click Project.  
  
2.  Expand **Business Intelligence**, and click **Integration Services**.  
  
3.  Select **Integration Services Import Wizard** in the middle pane, type a **name** for the solution, project, and specify the **folder** for the project, and then click **OK**.  
  
     You should see the **Integration Services Import Project Wizard**. This wizard creates a new Integration Services project based on an existing one  
  
4.  Click **Next** on the **Introduction** page to see the **Select Source** page.  
  
5.  Specify whether you want to import project from an .ispac file or from a catalog.  
  
    -   If you select **Project deployment file** option, specify the path to the .ispac file.  
  
    -   If you select **Integration Services Catalog** option, specify the name of database server instance on which the catalog exists, and the path to the project in the catalog.  
  
6.  Click **Next** on the **Select Source** page to see the **Review** page. Review the information displayed on the page about the import operation the wizard is going to perform.  
  
7.  Click **Import** to create a new Integration Services project based on the one you selected.  
  
8.  The **Results** page should display the results from import operation the wizard performed. Click **Save Report** to save the report to an XML file.  
  
9. Click **Close** to close the wizard.  
  
  
