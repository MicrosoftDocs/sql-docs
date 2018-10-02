---
title: "New Linked Report Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: fefb46e8-6901-4d50-a3f8-7c49ad72e7b1
author: markingmyname
ms.author: maghan
manager: craigg
---
# New Linked Report Page (Report Manager)
  Use the New Linked Report page to create a linked report. A linked report is a report with settings and properties of its own, but links to the report definition of another report. Linked reports are useful when you have a base report that you want to vary for specific groups or users; for example, a regional report that returns different data based on a regional code that you specify as a parameter. A linked report is typically created from a parameterized report when you want to vary and then save different parameter values with each report instance. However, you can create a linked report from any report to which you have access.  
  
 A linked report can have its own name, description, location, parameter properties, report execution properties, report history properties, permissions, and subscriptions. However, a linked report must use the data source properties and layout of the base report that provides the report definition.  
  
## Navigation  
 Use the following procedures to navigate to this location in the user interface (UI).  
  
###### To open the New Linked Report page from the Contents page  
  
1.  Open Report Manager, and locate a report for which you want to create a linked report.  
  
2.  Hover over the report, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Create Linked Report**.  
  
###### To open the New Linked Report page from the General properties page of a report  
  
1.  Open Report Manager, and locate a report for which you want to create a linked report.  
  
2.  Hover over the report, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the General properties page for the report.  
  
4.  In the item toolbar, click **Create Linked Report**.  
  
## Options  
 **Name**  
 Specify the name of the linked report. A name must contain at least one alphanumeric character. It can also include spaces and certain symbols. However, you must not use the characters ; ? : \@ & = + , $ / * \< > | " or / when specifying a name.  
  
 **Description**  
 Type a description of the report contents. This description appears in the Contents page to users who have permission to access the report.  
  
 **Location**  
 Specify the folder path that contains the report. By default, linked reports are created as siblings to the base report. Click **Change Location** to put the linked report in a different folder.  
  
 **OK**  
 Click **OK** to save your changes and return to the General properties page of the base report.  
  
## See Also  
 [Create a Linked Report](reports/create-a-linked-report.md)   
 [General Properties Page, Reports &#40;Report Manager&#41;](../../2014/reporting-services/general-properties-page-reports-report-manager.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
