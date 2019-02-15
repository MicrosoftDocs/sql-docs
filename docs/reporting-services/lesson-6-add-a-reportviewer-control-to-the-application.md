---
title: "Lesson 6: Add a ReportViewer Control to the Application | Microsoft Docs"
ms.date: 05/18/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: reporting-services

ms.topic: conceptual
ms.assetid: f9492a97-5609-4059-ae76-0fba111d4968
author: markingmyname
ms.author: maghan
---
# Lesson 6: Add a ReportViewer Control to the Application
After you design the child report by using the Report Wizard, your next step is to add a ReportViewer control to the website application. If you are using the ASP.NET Reports Web Site, it will have added the ReportViewer control to the default.aspx page.   
  
### To add a ReportViewer control to the application  
  
1.  In **Solution Explorer**, right-click **Default.aspx**, and then click **View Designer**.  
  
2.  If default.aspx already has the ReportViewer Control on it, skip to **Step 4**. Otherwise, From the **AJAX Extensions** group in the **Toolbox** window, drag a **ScriptManager** control to the design surface.  
  
3.  From the **Reporting** group, drag a **ReportViewer** control to the design surface below the **ScriptManager** control.  
  
4.  Open the **ReportViewer Tasks** window by clicking the arrow in the top right-hand corner of the **ReportViewer** control.  
  
5.  In the **Choose Report** box, select the parent report you created.  
  
    When you select a report, instances of data sources used in the report are created automatically. Code is generated to instantiate each DataTable (and its [DataSet](https://msdn.microsoft.com/library/system.data.dataset.aspx) container). An [ObjectDataSource](https://msdn.microsoft.com/library/system.web.ui.webcontrols.objectdatasource.aspx) control is added to the design surface, corresponding to each data source used in the report. This data source control is configured automatically.  
  
6.  On the Build menu, click Build website.  
  
    The report is compiled and any errors such as a syntax error in a report expression appear in the **Error List** area. Click **Error List** at the bottom of the Visual Studio window to display the **Error List** area.  
  
## Next Task  
You have successfully added a ReportViewer control to the website application. Next, you will add a drillthrough action on the parent report. See [Lesson 7: Add Drillthrough Action on Parent Report](../reporting-services/lesson-7-add-drillthrough-action-on-parent-report.md).  
  

