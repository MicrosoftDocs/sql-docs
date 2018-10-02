---
title: "Link a Report to a Model as a Clickthrough Report | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "customizing clickthrough reports"
  - "clickthrough reports, customizing"
  - "clickthrough reports, templates"
ms.assetid: 3af42de3-67ef-41c2-bc8a-7045baec6f63
author: markingmyname
ms.author: maghan
manager: craigg
---
# Link a Report to a Model as a Clickthrough Report
  Instead of using the default clickthrough report templates, you can create a Report Builder report and then link it to a specific entity in the report model. When the person viewing the report clicks the interactive data in the main report, this report is displayed as a clickthrough report. To link a report to an entity, use [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Report Manager.  
  
> [!IMPORTANT]  
>  The primary, or base, entity that is used in the report must to be the same entity to which the report is linked.  
  
### To start Report Manager from a browser  
  
1.  Open [!INCLUDE[msCoName](../includes/msconame-md.md)] Internet Explorer 6.0 or later.  
  
2.  In the address bar of the Web browser, type the Report Manager URL. By default, the URL is http://\<*ComputerName*>/reports.  
  
### To create a customized clickthrough report  
  
1.  Navigate to the report model to which you want to add the customized clickthrough report.  
  
2.  Double-click the report model.  
  
3.  Click **Clickthrough**.  
  
4.  Select the entity to which you want to attach the customized clickthrough report.  
  
    > [!NOTE]  
    >  This entity must be the same as the base entity of the customized clickthrough report.  
  
5.  To display the customized report when a single instance of the selected entity is clicked, click the Single instance report **Browse** button.  
  
     -or-  
  
     To display the customized report when a multiple instance of the selected entity is clicked, click the Multiple instance report **Browse** button.  
  
6.  Select the report and then click **OK**.  
  
7.  Click **Apply**.  
  
## See Also  
 [Clickthrough Reports &#40;SSRS&#41;](reports/clickthrough-reports-ssrs.md)  
  
  
