---
title: "Configure Maximum File Upload Size (PowerPivot for SharePoint) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: ac516c63-1e79-4ae8-bca6-32d3c1a09c00
author: minewiskan
ms.author: owend
manager: craigg
---
# Configure Maximum File Upload Size (PowerPivot for SharePoint)
  PowerPivot workbooks often contain large amounts of data that result in files that exceed the maximum file size allowed for SharePoint uploads. When you try to upload a file that exceeds the upper limit, you will get the following error on SharePoint:  
  
-   "The specified file is larger than the maximum supported file size."  
  
 To increase the file size, start by adjusting the Maximum Workbook Size for Excel Services to 50 megabytes. This aligns the maximum file size limits for Excel to those of SharePoint web applications (set to 50 megabytes by default in SharePoint 2010 and 200 in SharePoint 2013). If your files exceed 50 megabytes in size, increase the file size limits for both Excel Services and your web application.  
  
 You must be a SharePoint administrator to change the maximum file upload size.  
  
### Configure maximum file size for Excel Services  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
2.  Click the name of your Excel Services Application.  
  
3.  Click **Trusted File Locations**.  
  
4.  Click the location to edit the properties. By default, Excel Services considers the default web application a trusted site. If you are using the default web application, click **http://** to open the configuration page for this location.  
  
5.  Scroll to **Workbook Properties**.  
  
6.  In Maximum Workbook Size, increase the file size from 10 (the default value) to 50 or a larger size that accommodates the files you are working with.  
  
     By default, 50 megabytes is the maximum file upload size for SharePoint web applications. If you set Maximum Workbook Size to a number larger than 50 megabytes, follow the steps in the next procedure to increase the Maximum Upload Size for your SharePoint web application to the same value.  
  
     The maximum value that you can specify is 2 gigabytes (or 2047 megabytes as specified in Central Administration).  
  
7.  Click **OK**.  
  
### Configure maximum file size for a SharePoint web application  
  
1.  In Central Administration, in Application Management, click **Manage web applications**.  
  
    > [!NOTE]  
    >  Perform the following steps only if you increased the Maximum Workbook Size in Excel Services.  
  
2.  Select the application (for example, **SharePoint - 80**).  
  
3.  On the Web Applications ribbon, click the down arrow on the General Settings button.  
  
4.  Click **General Settings**.  
  
5.  Scroll to **Maximum Upload Size**.  
  
6.  Set the property to the same number, or larger as the Maximum Workbook Size in Excel Services.  
  
7.  Click **OK**.  
  
  
