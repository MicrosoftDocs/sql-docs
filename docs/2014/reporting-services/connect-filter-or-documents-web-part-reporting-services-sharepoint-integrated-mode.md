---
title: "Connect Filter or Documents Web Part (Reporting Services in SharePoint Integrated Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Filter Web Part [Reporting Services]"
  - "SharePoint integration [Reporting Services], Web Parts"
  - "Report Viewer Web Part [Reporting Services]"
  - "Documents Web Part [Reporting Services]"
ms.assetid: 6a303135-c0ef-44cd-a423-1cea8df3dcf3
author: markingmyname
ms.author: maghan
manager: kfile
---
# Connect Filter or Documents Web Part (Reporting Services in SharePoint Integrated Mode)
  If you are using a SharePoint product, you can create a dashboard or Web Part Page that includes a Filter Web Part or Documents Web part and a Report Viewer Web Part. Supported versions are [!INCLUDE[SPF2010](../includes/spf2010-md.md)] or [!INCLUDE[SPS2010](../includes/sps2010-md.md)]. Also supported are [!INCLUDE[winSPServ3](../includes/winspserv3-md.md)] or [!INCLUDE[offSPServ](../includes/offspserv-md.md)] 2007. By connecting a Filter Web Part, users who select filter values in a Filter Web Part can send the value to a parameterized report on the same page. By connecting a Documents Web Part, users who click on reports in the Documents library can view the report in an adjacent Report Viewer Web Part.  
  
 The Filter Web Part is used to send values to one or more parameters on a report. To use a Filter Web Part, the report must have parameters defined for it that are compatible with the values, data type, and format sent by the Web Part.  
  
 The Documents Web Part is associated with the Documents library of the Home site. To view, add, or remove items from the Documents library, click **View All Site Content**. In Libraries, click **Documents**. You can use the **New**, **Upload**, and **Actions** menu to manage the items in the Documents library.  
  
### To connect a Filter Web Part  
  
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
  
### To connect a Documents Web Part  
  
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
  
## See Also  
 [Add the Report Viewer Web Part to a Web Page &#40;Reporting Services in SharePoint Integrated Mode&#41;](report-server-sharepoint/add-reporting-services-content-types-to-a-sharepoint-library.md)   
 [Report Viewer Web Part on a SharePoint Site](../../2014/reporting-services/report-viewer-web-part-on-a-sharepoint-site.md)   
 [Customize the Report Viewer Web Part](../../2014/reporting-services/customize-the-report-viewer-web-part.md)  
  
  
