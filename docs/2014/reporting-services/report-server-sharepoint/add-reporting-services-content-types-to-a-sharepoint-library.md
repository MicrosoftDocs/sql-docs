---
title: "Add the Report Viewer Web Part to a Web Page (Reporting Services in SharePoint Integrated Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "SharePoint integration [Reporting Services], viewing reports"
  - "Web Parts [Reporting Services]"
  - "SharePoint integration [Reporting Services], Web Parts"
  - "Report Viewer Web Part [Reporting Services]"
ms.assetid: cac75345-2380-467d-a394-0a2140908a5a
author: markingmyname
ms.author: maghan
manager: kfile
---
# Add the Report Viewer Web Part to a Web Page (Reporting Services in SharePoint Integrated Mode)
  You can use the Report Viewer Web Part to view reports that run on report server that is configured to run in SharePoint integrated mode. You can use the Web Part to display report definition (.rdl) files that you created in Report Builder or Report Designer and uploaded to a library.  
  
 You can add the Report Viewer Web Part to a Web page if you want to embed a report on that page.  
  
> [!NOTE]  
>  Although they have identical names, the Report Viewer Web Part that is installed through the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in is different from the Report Viewer Web Part that is included in the RSWebParts.cab file. The instructions in this topic are specifically for the Report Viewer Web Part that is installed through the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in.  
  
 To add a Web Part to a Web page, you must have the Add and Customize Pages permission at the site level. If you are using default security settings, this permission is granted to members of the **Owners** group who have the Full Control level of permission.  
  
### To embed a report in a Web page  
  
1.  Open or create the Web Part page or dashboard.  
  
2.  On **Site Actions**, click **Edit Page**.  
  
3.  Click **Add a Web Part**.  
  
4.  In the list of web part categories, select the **Miscellaneous** category, and then select **SQL Server Reporting Services Report Viewer**.  
  
5.  Click **Add**. The Web Part is added at the top of the zone. You can drag it to a different location in the zone.  
  
6.  Within the viewer, click **Click here to open the tool pane**.  
  
7.  Select a report from any library in the current site collection by clicking the browse (**...**) button. You can also type the report URL. To determine the URL for any report, right-click the report and select **Properties**. Do not click the down arrow next to the report; the report URL is not indicated in the View Properties page of the item. If you copy and paste the URL from the **Properties** dialog box, replace the "%20" URL encoding with a space (for example, "Company%20Sales" should be "Company Sales").  
  
    > [!NOTE]  
    >  Each Report Viewer Web Part contains a single report. The URL must be the fully qualified path to a report that is on the current SharePoint site, or on a site within the same Web application or farm. The URL must resolve to a document library or to a folder within a document library that contains the report. The report URL must include the .rdl file extension. If the report depends on a model or shared data source files, you do not need to specify those files in the URL. The report contains references to the files it needs.  
  
8.  While the tool pane is open, you can set properties to modify the default appearance and layout.  
  
9. Click **Apply** at the bottom of the tool pane, and then click **OK** to close the pane.  
  
## See Also  
 [Report Viewer Web Part on a SharePoint Site](../report-viewer-web-part-on-a-sharepoint-site.md)   
 [Customize the Report Viewer Web Part](../customize-the-report-viewer-web-part.md)   
 [Granting Permissions on Report Server Items on a SharePoint Site](../security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md)   
 [Install or Uninstall the Reporting Services Add-in for SharePoint &#40;SharePoint 2010 and SharePoint 2013&#41;](../install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)  
  
  
