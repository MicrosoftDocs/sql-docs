---
title: "Configure General Properties for a Report (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "reports [Reporting Services], properties"
  - "properties [Reporting Services], general"
ms.assetid: 10b941b2-28e6-4408-9ee4-acebc63c8496
author: markingmyname
ms.author: maghan
manager: kfile
---
# Configure General Properties for a Report (Report Manager)
    
### To configure general report properties  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, navigate to the **Contents** page. Navigate to the report that you want to configure general properties for and open it.  
  
3.  Click the **Properties** tab.  
  
     Or, if the **Contents** page is in Details view, click the property page icon:  
  
     ![Property page icon](media/prop.gif "Property page icon")  
  
4.  The **General** properties page is displayed, and you can configure properties as follows:  
  
    -   In the **Properties** section, you can modify the report name and description.  
  
    -   You can select the **Hide in list view** checkbox to hide the item when the page is opened in the default page layout (list view) which arranges items across and down the page.  
  
    -   In the **Report Definition** section, click **Edit** to extract a copy of the report definition. Modifications that you make locally to the report definition are not saved on the report server.  
  
         Or, to update the report definition from an .rdl file, click **Update**.  
  
        > [!NOTE]  
        >  If you update a report definition, you must reset the data source settings after the update is completed.  
  
    -   Use the **Delete** or **Move** buttons to delete or move the report.  
  
    -   You can also create a linked report.  
  
5.  When you have finished configuring general properties for the report, click **Apply**.  
  
## See Also  
 [Move or Delete an Item &#40;Report Manager&#41;](report-server/move-or-delete-an-item-report-manager.md)   
 [Contents Page &#40;Report Manager&#41;](../../2014/reporting-services/contents-page-report-manager.md)   
 [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)   
 [General Properties Page, Reports &#40;Report Manager&#41;](../../2014/reporting-services/general-properties-page-reports-report-manager.md)  
  
  
