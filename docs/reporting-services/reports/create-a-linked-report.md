---
title: "Create a Linked Report | Microsoft Docs"
description: Learn how to create a linked report so that you can create additional versions of an existing report.
ms.date: 05/30/2019
ms.service: reporting-services
ms.subservice: reports


ms.topic: conceptual
helpviewer_keywords: 
  - "linked reports [Reporting Services], creating"
ms.assetid: 12be8341-cb57-45e8-a421-2bf66b50234d
author: maggiesMSFT
ms.author: maggies
---
# Create a Linked Report
  A linked report is a report server item that provides an access point to an existing report. Conceptually, it is similar to a program shortcut that you use to run a program or open a file.  
  
 A linked report is derived from an existing report and retains the original's report definition. A linked report always inherits report layout and data source properties of the original report. All other properties and settings can be different from those of the original report, including security, parameters, location, subscriptions, and schedules.  
  
 You can create a linked report when you want to create additional versions of an existing report. For example, you could use a single regional sales report to create region-specific reports for all of your sales territories.  
  
 Although linked reports are typically based on parameterized reports, a parameterized report is not required. You can create linked reports whenever you want to deploy an existing report with different settings.  
  
## To create a linked report  
  
1. In the web portal, navigate to the desired report, right-click on it and  select **Manage** from the drop down menu.

2. On the **Manage \<reportname\>** page, select **Create linked report**.  
  
3. Enter a name for the new linked report. Optionally enter a description.  
  
4. To select a different folder for the report, select the ellipsis button (...) to the right of ***Location***.  Navigate to the new folder for the report and select **Select**. If you do not select a different folder, the linked report is created in the current folder.  
  
5. Select **Create**. The linked report is created.  

6. Under **Advanced**, select a different **Report timeout** value if desired, and select **Apply** to save the changes.
  
     A linked report's icon differs from other items managed by a report server. The following icon indicates a linked report:  
  
     ![Linked report icon](../../reporting-services/report-server/media/hlp-16linked.gif "Linked report icon")  
  
## See also  

 [Reporting Services Concepts &#40;SSRS&#41;](../../reporting-services/reporting-services-concepts-ssrs.md)  
 [The web portal of a report server (SSRS Native Mode)](../../reporting-services/web-portal-ssrs-native-mode.md)
  
