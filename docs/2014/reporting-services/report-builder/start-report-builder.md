---
title: "Start Report Builder (Report Builder) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Report Builder, launching"
  - "launching Report Builder"
  - "SharePoint integration [Reporting Services], starting Report Builder"
  - "starting Report Builder"
ms.assetid: 8c8c7d2e-b315-418d-bf65-90e7685e4259
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Start Report Builder (Report Builder)
  [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] includes stand-alone and [!INCLUDE[ndptecclick](../../includes/ndptecclick-md.md)] versions of Report Builder. The [!INCLUDE[ndptecclick](../../includes/ndptecclick-md.md)] version can be used with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installed in native or SharePoint integrated mode.  
  
> [!NOTE]  
>  Report Builder cannot be installed on Itanium 64-based computers. This applies to the [!INCLUDE[ndptecclick](../../includes/ndptecclick-md.md)] and stand-alone versions of Report Builder.  
  
 If the [!INCLUDE[ndptecclick](../../includes/ndptecclick-md.md)] version of Report Builder that opens is an earlier version of Report Builder, contact your administrator who can update Report Manager and the SharePoint site to use the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of Report Builder  
  
 You can also use the [!INCLUDE[ndptecclick](../../includes/ndptecclick-md.md)] version of Report Builder to create reports on a [!INCLUDE[ssGeminiClient](../../includes/ssgeminiclient-md.md)] workbook that has been published to SharePoint. For more information about using Report Builder with [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)], see [Create a Reporting Services Report with PowerPivot Data](https://go.microsoft.com/fwlink/?LinkId=185238) on technet.microsoft.com.  
  
 To start Report Builder stand-alone from the **Start** menu on your local computer, you or an administrator must install Report Builder directly on your computer before the tool is available for you to use. The stand-alone version is not installed when you install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]; you must download and install it separately. To download Report Builder, see [Microsoft® SQL Server® 2012 Report Builder](https://go.microsoft.com/fwlink/?LinkId=401502).  
  
### To start Report Builder ClickOnce from Report Manager  
  
1.  In your Web browser, type the URL for your report server in the address bar. By default, the URL is http://\<*servername*>/reports. Report Manager opens.  
  
2.  Click **Report Builder**.  
  
     Report Builder opens and you can create a report or open a report on the report server.  
  
### To start Report Builder ClickOnce using a URL  
  
1.  In your Web browser, type the following URL in the address bar:  
  
     http://\<servername>/reportserver/reportbuilder/ReportBuilder_3_0_0_0.application  
  
2.  Press ENTER.  
  
     Report Builder opens and you can create a report or open a report on the report server.  
  
### To start Report Builder ClickOnce in SharePoint integrated mode  
  
1.  Navigate to the SharePoint site that contains the library you want.  
  
2.  Open the library.  
  
3.  Click **Documents**.  
  
4.  On the **New Document** menu, click **Report Builder Report**.  
  
     Report Builder opens and you can create a report or open a report on the report server.  
  
     **Note** If the **New Document** menu does not list the **Report Builder Report**, **Report Builder Model**, and **Report Data Source** options, their content types need to be added to the SharePoint library. For more information, see [Add Report Server Content Types to a Library &#40;Reporting Services in SharePoint Integrated Mode&#41;](../add-reporting-services-content-types-to-a-sharepoint-library.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?LinkId=154888) on msdn.microsoft.com.  
  
### To start Report Builder stand-alone from the Start menu  
  
1.  On the Start menu, click **All Programs**, and then click [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)] **Report Builder**.  
  
2.  Click **Report Builder** .  
  
     Report Builder opens and you can create or open a report.  
  
3.  To create a new report, click the SQL Server icon in the upper left-hand corner of Report Builder, and then click New.  
  
4.  To open an existing report stored on your local machine or a report server, click the SQL Server icon in the upper left-hand corner, and then click Open.  
  
     If you don't see the report server in the list of existing servers, close the **Open Report** dialog box and then click **Connect** at the bottom of Report Builder to connect to the server.  
  
## See Also  
 [Report Builder in SQL Server 2014](report-builder-in-sql-server-2016.md)  
  
  
