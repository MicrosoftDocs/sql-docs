---
title: "Lesson 2: Generate Classes from the RDL Schema using the xsd Tool | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services-native
ms.topic: conceptual
ms.assetid: a81c87f1-7977-4b30-b6ac-b38b3e2b6398
author: markingmyname
ms.author: maghan
manager: kfile
---
# Lesson 2: Generate Classes from the RDL Schema using the xsd Tool
  After you have created your [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] project, the next step is to retrieve a local copy of the report definition schema and run the XML Schema Definition Tool (Xsd.exe).  
  
### To generate the RDL classes  
  
1.  Open an instance of [!INCLUDE[msCoName](../includes/msconame-md.md)] Internet Explorer (or equivalent Web browser) and navigate to the following URL:  
  
    ```  
    https://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition/ReportDefinition.xsd  
    ```  
  
2.  Once the RDL schema has been opened in the browser, browse to the **File** menu, and select **Save As**.  
  
3.  Browse to the location where you created your [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] project and save the schema with the file name ReportDefinition.xsd.  
  
4.  After the file has been saved, open an instance of the [!INCLUDE[vs_dev10_long](../includes/vs-dev10-long-md.md)] command prompt. To open an instance of the command prompt, click the Start menu, point to **All Programs**, point to **Microsoft Visual Studio 2010**, point to **Visual Studio Tools** and click **Visual Studio Command Prompt (2010)**.  
  
5.  Change the current path to the location where you saved the ReportDefinition.xsd file:  
  
     `CD\<ReportDefinition.xsd Path>`  
  
6.  Generate the ReportDefinition.cs file that contains the classes for the RDL schema with the following command:  
  
     `xsd /c /n:SampleRDLSchema ReportDefinition.xsd`  
  
     To generate a ReportDefinition.vb file use this command:  
  
     `xsd /c /l:VB /n:SampleRDLSchema ReportDefinition.xsd`  
  
7.  Add ReportDefinition.xsd your project. From the **Project** menu, click **Add Existing Item**. Browse to the location of the ReportDefinition.xsd file, select ReportDefinition.xsd, and click **Add**.  
  
    > [!NOTE]  
    >  After you have added the ReportDefinition.xsd file to the project you will notice in **Solution Explorer** that the ReportDefinition.cs (.vb) file is not there. To display the file, click the expand/collapse button next to the ReportDefinition.xsd file.  
  
## Next Lesson  
 In the next lesson, you will write code to load a report definition from a report server using the classes you generated from the RDL schema. See [Lesson 3: Load a Report Definition from the Report Server](../../2014/tutorials/lesson-3-load-a-report-definition-from-the-report-server.md).  
  
## See Also  
 [Updating Reports Using Classes Generated from the RDL Schema &#40;SSRS Tutorial&#41;](../../2014/tutorials/updating-reports-using-classes-generated-from-the-rdl-schema-ssrs-tutorial.md)   
 [Report Definition Language &#40;SSRS&#41;](../reporting-services/reports/report-definition-language-ssrs.md)  
  
  
