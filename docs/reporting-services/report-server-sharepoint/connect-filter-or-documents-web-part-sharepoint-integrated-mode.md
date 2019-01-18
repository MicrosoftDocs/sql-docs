---
title: "Connect Filter or Documents web part with a Reporting Services Report Viewer web part | Microsoft Docs"
ms.date: 11/26/2018
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server-sharepoint


ms.topic: conceptual
author: markingmyname
ms.author: maghan
---
# Connect Filter or Documents web part with a Reporting Services Report Viewer web part

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2016-2019](../../includes/ssrs-appliesto-sharepoint-2016-2019.md)] [!INCLUDE[ssrs-appliesto-not-sharepoint-online](../../includes/ssrs-appliesto-not-sharepoint-online.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

If you are using a SharePoint product, you can create a dashboard or web part Page that includes a Filter web part or Documents web part and a Report Viewer web part. Supported versions are [!INCLUDE[SPF2010](../../includes/spf2010-md.md)] or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)]. Also supported are [!INCLUDE[winSPServ3](../../includes/winspserv3-md.md)] or [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] 2007. By connecting a Filter web part, users who select filter values in a Filter web part can send the value to a parameterized report on the same page. By connecting a Documents web part, users who click on reports in the Documents library can view the report in an adjacent Report Viewer web part.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

 The Filter web part is used to send values to one or more parameters on a report. To use a Filter web part, the report must have parameters defined for it that are compatible with the values, data type, and format sent by the web part.  
  
 The Documents web part is associated with the Documents library of the Home site. To view, add, or remove items from the Documents library, click **View All Site Content**. In Libraries, click **Documents**. You can use the **New**, **Upload**, and **Actions** menu to manage the items in the Documents library.  
  
## Connect a Filter web part
  
1.  Open or create the web part page or dashboard.  
  
2.  On the **Site Actions** menu, click **Edit Page**.  
  
3.  Click **Add a web part**.  
  
4.  In **All web parts**, in the **Miscellaneous** category, select **SQL Server Reporting Services Report Viewer**.  
  
5.  Click **Add**. The web part is added at the top of the zone.  
  
6.  On another zone in the same web part page or dashboard, click **Add a web part**.  
  
7.  In **All web parts**, in the **Filters** section, select a web part.  
  
8.  Click **Add**. The web part is added at the top of the zone.  
  
9. In the zone that contains the web part, click the web part **edit** menu, point to **Connections**, point to **Send Filter Values To**, and then select **Report Viewer** - *report name*.  
  
10. Check in your changes and save the page.  
  
## Connect a Documents web part  
  
1.  Open or create the web part page or dashboard.  
  
2.  On the **Site Actions** menu, click **Edit Page**.  
  
3.  Click **Add a web part**.  
  
4.  In **All web parts**, in the **Lists and Library** section, select **Documents.**  
  
5.  Click **Add**. The web part is added at the top of the zone.  
  
6.  Click **Apply** at the bottom of the tool pane, and then click **OK** to close the pane.  
  
7.  On another zone in the same web part page or dashboard, click **Add a web part**.  
  
8.  In **All web parts**, in the **Miscellaneous** category, select **SQL Server Reporting Services Report Viewer.**  
  
9. Click **Add**. The web part is added at the top of the zone.  
  
10. In the zone that contains the web part, click the web part **edit** menu, point to **Connections**, point to **Get report definitions from**, and then select **Documents**.  
  
11. Check in your changes and save the page.  
  
## See also

 [Add the Report Viewer web part to a web page](../../reporting-services/report-server-sharepoint/add-the-report-viewer-web-part-to-a-web-page.md)   
 [Report Viewer web part on a SharePoint Site](../../reporting-services/report-server-sharepoint/report-viewer-web-part-on-a-sharepoint-site.md)   
 [Customize the Report Viewer web part](../../reporting-services/report-server-sharepoint/customize-the-report-viewer-web-part.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
