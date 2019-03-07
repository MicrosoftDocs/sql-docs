---
title: "Saving Reports (Report Builder) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-builder


ms.topic: conceptual
ms.assetid: 59ddc4b8-9517-4d3f-9c88-a07e9907cecb
author: markingmyname
ms.author: maghan
---
# Saving Reports (Report Builder)
  In Report Builder you can save a paginated report to a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] report server, SharePoint library, file share where you have write permission, or your computer. 
  
When you save a report, what you are really saving is the report definition, which describes the report layout. You are not saving the data. Every time you run the report, the report data is refreshed and is likely to be different than the previous time you ran the report.  
  
 If you want to save the report to a different format or save the report definition with the data, use one of the following [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features:  
  
-   Export a rendered report to a different file format such as comma separated files (CSV) or Excel workbooks and save the report in that format. You can also generate data feeds from reports and save the report data.  
  
-   Create report subscriptions to deliver and save reports to a file share.  
  
-   Use report history to save versions of rendered reports as historical copies.  
  
 To learn more about viewing and managing reports directly on the report server, see [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md) and [Reporting Services Report Server &#40;Native Mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md).  
  
##  <a name="SavingReportDefinitions"></a> Saving Reports to a Report Server  
  Saving a report to a report server is also known as publishing a report. Although you can save reports to your computer, saving reports to a report server offers many advantages:  
  
-   Reports become available to others who have permission to access the folder in which you saved the report.  
  
-   Reports can be managed and viewed on the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal.  
  
-   Report resources such as data sources, images, and subreports are stored in one place for easier access.  
  
-   Reports can be delivered to others by subscriptions.  
  
-   Reports are securely stored in the report server database.  
  
-   Report runs can be logged and provide performance and auditing information.  
  
##  <a name="ExportingAndSavingReports"></a> Exporting and Saving Reports  
 If you have a small number of reports to archive, consider exporting a report and saving it as a file. After you export a report to an application (such as PDF or Excel), you can save it as a file and place it in a protected shared directory on the network. Alternatively, you can upload a saved PDF or Excel file as a resource item if you want to keep all copies of a report, regardless of the format, in the report server database. For more information about exporting a report, see [Export Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md) and [Upload a File or Report](../../reporting-services/reports/upload-a-file-or-report-report-manager.md).  
  
##  <a name="UsingFileShareDelivery"></a> Using File-Share Delivery  
 If you have a large number of reports to archive, create a subscription that delivers the report directly to the file system. For this approach, you must create a subscription for each report, choose a shared folder to store the reports, and define a schedule that determines when the file is created. Once you define a subscription, the report server can run the report unattended and add report files to the archive using the schedule that you provide. You can also create single-use schedules if you want to archive reports on an occasional basis. For more information about subscriptions and file share delivery, see [File Share Delivery in Reporting Services](../../reporting-services/subscriptions/file-share-delivery-in-reporting-services.md).  
  
##  <a name="UsingReportHistory"></a> Using Report History  
 You can also use the report history feature to create historical copies. You can then back up the report server database and store the backup in a safe location for future use. All report history (along with reports, shared data source items, folders, subscriptions, and shared schedules) is stored in the report server database. You can create a backup to maintain a permanent copy of report history and metadata such as subscription information that indicates the recipients of a report. For more information, see [Create, Modify, and Delete Snapshots in Report History](../../reporting-services/report-server/create-modify-and-delete-snapshots-in-report-history.md).  
 
##  <a name="HowTo"></a> How-To Topics  
  
-   [Save Reports to a Report Server &#40;Report Builder&#41;](../../reporting-services/report-builder/save-reports-to-a-report-server-report-builder.md)  
  
-   [Save a Report to a SharePoint Library &#40;Report Builder&#41;](../../reporting-services/report-builder/save-a-report-to-a-sharepoint-library-report-builder.md)  
   
## See Also  
 [Reports, Report Parts, and Report Definitions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/reports-report-parts-and-report-definitions-report-builder-and-ssrs.md)   
 [Install and Uninstall Report Builder](https://msdn.microsoft.com/library/2c9a5814-17bf-4947-8fb3-6269e7caa416)   
 [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)   
 [Export Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md)   
 [Print Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-builder/print-reports-report-builder-and-ssrs.md)  
  
  
