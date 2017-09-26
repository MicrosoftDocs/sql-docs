---
title: "Connect Filter or Documents web part | Microsoft Docs"
ms.custom: ""
ms.date: "09/25/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-sharepoint"
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---
# Connect Filter or Documents web part

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

If you are using a SharePoint product, you can create a dashboard or Web Part Page that includes a Filter Web Part or Documents Web part and a Report Viewer Web Part. Supported versions are [!INCLUDE[SPF2010](../../includes/spf2010-md.md)] or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)]. Also supported are [!INCLUDE[winSPServ3](../../includes/winspserv3-md.md)] or [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] 2007. By connecting a Filter Web Part, users who select filter values in a Filter Web Part can send the value to a parameterized report on the same page. By connecting a Documents Web Part, users who click on reports in the Documents library can view the report in an adjacent Report Viewer Web Part.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

 The Filter Web Part is used to send values to one or more parameters on a report. To use a Filter Web Part, the report must have parameters defined for it that are compatible with the values, data type, and format sent by the Web Part.  
  
 The Documents Web Part is associated with the Documents library of the Home site. To view, add, or remove items from the Documents library, click **View All Site Content**. In Libraries, click **Documents**. You can use the **New**, **Upload**, and **Actions** menu to manage the items in the Documents library.  
  
## Connect a Filter web part
  
1.  Open or create the Web Part page or dashboard.  
  
2.  On the **Site Actions** menu, click **Edit Page**.  
  
3.  Click **Add a Web Part**.  
  
4.  In **All Web Parts**, in the **Miscellaneous** category, select **SQL Server Reporting Services Report Viewer**.  
  
5.  Click **Add**. The Web Part is added at the top of the zone.  
  
6.  On another zone in the same Web Part page or dashboard, click **Add a Web Part**.  
  
7.  In **All Web Parts**, in the **Filters** section, select a Web Part.  
  
8.  Click **Add**. The Web Part is added at the top of the zone.  
  
9. In the zone that contains the Web Part, click the Web Part **edit** menu, point to **Connections**, point to **Send Filter Values To**, and then select **Report Viewer** - *report name*.  
  
10. Check in your changes and save the page.  
  
## Connect a Documents web part  
  
1.  Open or create the Web Part page or dashboard.  
  
2.  On the **Site Actions** menu, click **Edit Page**.  
  
3.  Click **Add a Web Part**.  
  
4.  In **All Web Parts**, in the **Lists and Library** section, select **Documents.**  
  
5.  Click **Add**. The Web Part is added at the top of the zone.  
  
6.  Click **Apply** at the bottom of the tool pane, and then click **OK** to close the pane.  
  
7.  On another zone in the same Web Part page or dashboard, click **Add a Web Part**.  
  
8.  In **All Web Parts**, in the **Miscellaneous** category, select **SQL Server Reporting Services Report Viewer.**  
  
9. Click **Add**. The Web Part is added at the top of the zone.  
  
10. In the zone that contains the Web Part, click the Web Part **edit** menu, point to **Connections**, point to **Get report definitions from**, and then select **Documents**.  
  
11. Check in your changes and save the page.  
  
## See also

 [Add the Report Viewer Web Part to a Web Page &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../reporting-services/report-server-sharepoint/add-the-report-viewer-web-part-to-a-web-page.md)   
 [Report Viewer Web Part on a SharePoint Site](../../reporting-services/report-server-sharepoint/report-viewer-web-part-on-a-sharepoint-site.md)   
 [Customize the Report Viewer Web Part](../../reporting-services/report-server-sharepoint/customize-the-report-viewer-web-part.md)  

More questions? [Try asking the Reporting Services forum](http://go.microsoft.com/fwlink/?LinkId=620231)