---
title: "Save a Report to a SharePoint Library (Report Builder) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 4daa1eee-78b7-43d0-8b22-4a98e8fa66ba
author: markingmyname
ms.author: maghan
manager: kfile
---
# Save a Report to a SharePoint Library (Report Builder)
  To save a report to a report server configured for SharePoint integration, you must browse to the SharePoint server and establish a connection to the report server. In the report definition, all references to items related to the report must use values that are specific to a SharePoint report server. Related items include subreports, drillthrough reports, and resources such as Web-based images. For more information, see [Specifying Paths to External Items &#40;Report Builder and SSRS&#41;](../report-design/specifying-paths-to-external-items-report-builder-and-ssrs.md).  
  
 You must have **Member** or **Owner** permission on the SharePoint site to set the properties on the project.  
  
### To save a report to a SharePoint site  
  
1.  From the Report Builder button, click **Save**. The **Save As**_\<Report Item>_ dialog box opens.  
  
    > [!NOTE]  
    >  If you are resaving a report, it is automatically resaved to its previous location. Use the **Save As** option to change location.  
  
2.  Optionally, click **Recent Sites and Servers** to show a list of recently used report servers and SharePoint sites.  
  
3.  Browse to the SharePoint site, and then click **Save**.  
  
    > [!NOTE]  
    >  If you leave a changed report for more than 10 hours without saving it, it is disconnected from the server without being saved. If that happens, in the lower-right status bar, click **Disconnect**, and then click **Connect**. The most recent server will be in the list of available servers. Select it and the report will reconnect.  
  
## See Also  
 [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](finding-viewing-and-managing-reports-report-builder-and-ssrs.md)  
  
  
