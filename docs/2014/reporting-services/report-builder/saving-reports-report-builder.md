---
title: "Saving Reports (Report Builder) | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 59ddc4b8-9517-4d3f-9c88-a07e9907cecb
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Saving Reports (Report Builder)
  In Report Builder you can save a report to a report server, SharePoint library, file share on which you have write permission, or your computer. You can save a report to the same location from which you opened it, save it to a different location, or save it with a new name to the same or different location. By default, a report is resaved to the location from which you opened it. When you save the report, what you are really saving is the report definition, which describes the report layout. You are not saving the data. Every time you run the report, the report data is refreshed and is likely to be different than the previous time you ran the report.  
  
 If you want to save the report to a different format or save the report definition with the data, use one of the following [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features:  
  
-   Export a rendered report to a different file format such as comma separated files (CSV) or Excel workbooks and save the report in that format. You can also generate data feeds from reports and save the report data.  
  
-   Create report subscriptions to deliver and save reports to a file share.  
  
-   Use report history to save versions of rendered reports as historical copies.  
  
 To learn more about viewing and managing reports directly on the report server, see [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](finding-viewing-and-managing-reports-report-builder-and-ssrs.md) and [Reporting Services Report Server &#40;Native Mode&#41;](../report-server/reporting-services-report-server-native-mode.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?LinkId=154888) on msdn.microsoft.com.  
  
##  <a name="SavingReportDefinitions"></a> Saving Report Definitions  
 Although you can save reports to your computer, saving reports to a report server offers many advantages.  
  
 Saving a report to a report server offers the following advantages:  
  
-   Reports become available to others who have permission to access the folder in which you saved the report.  
  
-   Reports can be managed and viewed from Report Manager.  
  
-   Report resources such as data sources, images, and subreports are stored in one place for easier access.  
  
-   Reports can be delivered to others by subscriptions.  
  
-   Reports are securely stored in the report server database.  
  
-   Report runs can be logged and provide performance and auditing information.  
  
 Saving a report to a report server is also known as publishing a report. When you save the report you save the report definition only. Every time you run the report, the report data is refreshed and likely different that the previous time you ran the report. If you want to save the report definition with the data you should use the report history feature. Using this feature, you save a copy of the report with its data.  
  

  
##  <a name="ExportingAndSavingReports"></a> Exporting and Saving Reports  
 If you have a small number of reports to archive, consider exporting a report and saving it as a file. After you export a report to an application (such as PDF or Excel), you can save it as a file and place it in a protected shared directory on the network. Alternatively, you can upload a saved PDF or Excel file as a resource item if you want to keep all copies of a report, regardless of the format, in the report server database. For more information about exporting a report, see [Exporting Reports &#40;Report Builder and SSRS&#41;](export-reports-report-builder-and-ssrs.md) and [Upload a File or Report &#40;Report Manager&#41;](../reports/upload-a-file-or-report-report-manager.md).  
  

  
##  <a name="UsingFileShareDelivery"></a> Using File-Share Delivery  
 If you have a large number of reports to archive, create a subscription that delivers the report directly to the file system. For this approach, you must create a subscription for each report, choose a shared folder to store the reports, and define a schedule that determines when the file is created. Once you define a subscription, the report server can run the report unattended and add report files to the archive using the schedule that you provide. You can also create single-use schedules if you want to archive reports on an occasional basis. For more information about subscriptions and file share delivery, see "File Delivery in Reporting Services" in the [Reporting Services documentation](https://go.microsoft.com/fwlink/?linkid=121312) in SQL Server Books Online.  
  

  
##  <a name="UsingReportHistory"></a> Using Report History  
 You can also use the report history feature to create historical copies. You can then back up the report server database and store the backup in a safe location for future use. All report history (along with reports, shared data source items, folders, subscriptions, and shared schedules) is stored in the report server database. You can create a backup to maintain a permanent copy of report history and metadata such as subscription information that indicates the recipients of a report. For more information, see "Managing Report History" in the [Reporting Services documentation](https://go.microsoft.com/fwlink/?linkid=121312) in SQL Server Books Online.  
  

  
##  <a name="HowTo"></a> How-To Topics  
  
-   [Save Reports to a Report Server &#40;Report Builder&#41;](save-reports-to-a-report-server-report-builder.md)  
  
-   [Save a Report to a SharePoint Library &#40;Report Builder&#41;](save-a-report-to-a-sharepoint-library-report-builder.md)  
  
-   [Save Reports to Your Computer &#40;Report Builder&#41;](../save-reports-to-your-computer-report-builder.md)  
  

  
## See Also  
 [Reports, Report Parts, and Report Definitions &#40;Report Builder and SSRS&#41;](../report-design/reports-report-parts-and-report-definitions-report-builder-and-ssrs.md)   
 [Install, Uninstall, and Report Builder Support](../install-uninstall-and-report-builder-support.md)   
 [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](finding-viewing-and-managing-reports-report-builder-and-ssrs.md)   
 [Exporting Reports &#40;Report Builder and SSRS&#41;](export-reports-report-builder-and-ssrs.md)   
 [Print Reports &#40;Report Builder and SSRS&#41;](print-reports-report-builder-and-ssrs.md)  
  
  
