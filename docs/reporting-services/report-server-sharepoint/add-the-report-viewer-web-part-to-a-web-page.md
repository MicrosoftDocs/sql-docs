---
title: "Add the Report Viewer web part to a web page | Microsoft Docs"
ms.date: 10/05/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server-sharepoint


ms.topic: conceptual
author: markingmyname
ms.author: maghan
---
# Add the Report Viewer web part to a web page

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

You can use the Report Viewer web part to view reports that run on report server that is configured to run in SharePoint integrated mode. You can use the web part to display report definition (.rdl) files that you created in Report Builder or Report Designer and uploaded to a library.

You can add the Report Viewer web part to a web page if you want to embed a report on that page.

> [!NOTE]
> This article is specific to the Report Viewer web part that shipped with the Reporting Services Add-in for SharePoint products. Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

To add a web part to a web page, you must have the Add and Customize Pages permission at the site level. If you are using default security settings, this permission is granted to members of the **Owners** group who have the Full Control level of permission.

## To embed a report in a web page

1.  Open or create the web part page or dashboard.  
  
2.  On **Site Actions**, click **Edit Page**.  
  
3.  Click **Add a web part**.  
  
4.  In the list of web part categories, select the **Miscellaneous** category, and then select **SQL Server Reporting Services Report Viewer**.  
  
5.  Click **Add**. The web part is added at the top of the zone. You can drag it to a different location in the zone.  
  
6.  Within the viewer, click **Click here to open the tool pane**.  
  
7.  Select a report from any library in the current site collection by clicking the browse (**...**) button. You can also type the report URL. To determine the URL for any report, right-click the report and select **Properties**. Do not click the down arrow next to the report; the report URL is not indicated in the View Properties page of the item. If you copy and paste the URL from the **Properties** dialog box, replace the "%20" URL encoding with a space (for example, "Company%20Sales" should be "Company Sales").  
  
    > [!NOTE]  
    >  Each Report Viewer web part contains a single report. The URL must be the fully qualified path to a report that is on the current SharePoint site, or on a site within the same Web application or farm. The URL must resolve to a document library or to a folder within a document library that contains the report. The report URL must include the .rdl file extension. If the report depends on a model or shared data source files, you do not need to specify those files in the URL. The report contains references to the files it needs.  
  
8.  While the tool pane is open, you can set properties to modify the default appearance and layout.  
  
9. Click **Apply** at the bottom of the tool pane, and then click **OK** to close the pane.  
  
## See also

 [Report Viewer web part on a SharePoint Site](../../reporting-services/report-server-sharepoint/report-viewer-web-part-on-a-sharepoint-site.md)   
 [Customize the Report Viewer web part](../../reporting-services/report-server-sharepoint/customize-the-report-viewer-web-part.md)   
 [Granting Permissions on Report Server Items on a SharePoint Site](../../reporting-services/security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md)   
 [Install or Uninstall the Reporting Services Add-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)  
