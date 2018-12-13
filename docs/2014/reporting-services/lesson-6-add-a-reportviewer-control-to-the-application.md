---
title: "Lesson 6: Add a ReportViewer Control to the Application | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: f9492a97-5609-4059-ae76-0fba111d4968
author: markingmyname
ms.author: maghan
manager: craigg
---
# Lesson 6: Add a ReportViewer Control to the Application
  After you design the child report by using the Report Wizard, your next step is to add a ReportViewer control to the website application.  
  
### To add a ReportViewer control to the application  
  
1.  In **Solution Explorer**, right-click **Default.aspx**, and then click **View Designer**.  
  
2.  From the **AJAX Extensions** group in the **Toolbox** window, drag a **ScriptManager** control to the design surface.  
  
3.  From the **Reporting** group, drag a **ReportViewer** control to the design surface below the **ScriptManager** control.  
  
4.  Open the **ReportViewer Tasks** window by clicking the arrow in the top right-hand corner of the **ReportViewer** control.  
  
5.  In the **Choose Report** box, select Parent report you created.  
  
     When you select a report, instances of data sources used in the report are created automatically. Code is generated to instantiate each DataTable (and its [DataSet](https://msdn.microsoft.com/library/system.data.dataset\(v=vs.100\).aspx) container). An [ObjectDataSource](https://msdn.microsoft.com/library/system.web.ui.webcontrols.objectdatasource\(v=vs.100\).aspx) control is added to the design surface, corresponding to each data source used in the report. This data source control is configured automatically.  
  
     If you're using Microsoft Visual Studio 2012, make sure that the ObjectDataSource control is bound with DataSet1 that is fully qualified with the project namespace, if the fully qualified name is listed in the **Choose your business object** drop-down list box (for example, Projectnamespace.DataSet1TableAdapters.ProductTableAdapter). You access the list box by right-clicking ObjectDataSource, and then clicking **Configure Data Source**.  
  
6.  On the Build menu, click Build website.  
  
     The report is compiled and any errors such as a syntax error in a report expression appear in the **Error List** area. Click **Error List** at the bottom of the Visual Studio window to display the **Error List** area.  
  
## Next Task  
 You have successfully added a ReportViewer control to the website application. Next, you will add a drillthrough action on the parent report.  
  
  
