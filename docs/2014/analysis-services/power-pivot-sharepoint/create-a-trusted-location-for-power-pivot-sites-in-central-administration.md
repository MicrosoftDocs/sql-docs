---
title: "Create a trusted location for PowerPivot sites in Central Administration | Microsoft Docs"
ms.custom: ""
ms.date: "04/28/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: a666f365-cd93-43a3-9d3d-e429dfc19b66
author: minewiskan
ms.author: owend
manager: craigg
---
# Create a trusted location for PowerPivot sites in Central Administration
  Excel Services lets you specify which locations are valid repositories for workbooks that you open on a SharePoint server. These locations are called 'trusted locations', and you can use different configuration settings for each trusted location you create. For a deployment of PowerPivot for SharePoint, you might consider creating a trusted location for sites that contain PowerPivot workbooks so that you can apply the settings that work best for PowerPivot data access, while preserving default settings for the rest of the farm.  
  
  
  
## Prerequisites  
 You must be a farm or service administrator to designate a URL as a trusted location.  
  
 You must know the URL address of the SharePoint site that contains the PowerPivot Gallery or other library that stores your workbooks. To get the address, open the site that contains the library, right-click **PowerPivot Gallery**, select **Properties**, and then copy the first part of the Address (URL) that contains the server name and site path.  
  
##  <a name="overview"></a> Overview  
 An initial installation of Excel Services specifies 'http://' as its trusted location, which means that workbooks from any site in the farm can be opened on the server. If you require tighter control over which locations are considered trustworthy, you can create new trusted locations that map to specific sites in your farm, and then vary the settings and permissions for each one.  
  
 Creating a new trusted location for sites that host PowerPivot workbooks is especially useful if you want to preserve default values for the rest of the farm, while applying different settings that work best for PowerPivot data access. For example, a trusted location that is optimized for PowerPivot workbooks could have a maximum workbook size of 50 MB, while the rest of the farm uses the default value of 10MB.  
  
 Creating a trusted location is recommended if you are using PowerPivot Gallery libraries to preview published workbooks and you encounter data refresh warnings instead of the preview image you expect. PowerPivot Gallery renders thumbnail images of reports and workbooks using data and presentation information within the document. If Warn on Data Refresh is enabled for a trusted location, PowerPivot Gallery might not have sufficient permissions to perform the refresh, causing an error to appear instead of the thumbnail image. Adding a site that contains PowerPivot Gallery as a new trusted location can eliminate this problem.  
  
##  <a name="create"></a> Create a Trusted Location for PowerPivot Data Access  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
2.  Click the Excel Services Service Application.  
  
3.  Click **Trusted File Locations**.  
  
4.  Click **Add Trusted File Location**.  
  
5.  Enter the URL of a site that contains a PowerPivot Gallery library.  
  
6.  In Location Type, select **Microsoft SharePoint Foundation**.  
  
    > [!IMPORTANT]  
    >  UNC and HTTP location types are not supported for PowerPivot data access.  
  
7.  Accept all of the default settings for properties in Session Management, Workbook Properties, and Calculation Behavior.  
  
8.  In Workbook Properties, set **Maximum Workbook Size** to **50**. This aligns the upper limit for workbook file size to the upper limit for file uploads to the parent web application. If your workbooks are larger than 50 megabytes, you must further increase the file size limit. For more information, see [Configure Maximum File Upload Size &#40;PowerPivot for SharePoint&#41;](configure-maximum-file-upload-size-power-pivot-for-sharepoint.md).  
  
9. In External Data, verify that Allow External Data is set to **Trusted data connection libraries and embedded**. This setting is required for PowerPivot data access in a workbook.  
  
10. Also in External Data, for Warn on Refresh, clear the checkbox for **Refresh warning enabled**. Clearing the checkbox allows PowerPivot Gallery to bypass routine warning messages and show preview images of a workbook instead.  
  
11. Click **OK**.  
  
## See Also  
 [PowerPivot Gallery](../../2014-toc/books-online-for-sql-server-2014.md)   
 [Create and Customize PowerPivot Gallery](create-and-customize-power-pivot-gallery.md)   
 [Use PowerPivot Gallery](use-power-pivot-gallery.md)  
  
  
