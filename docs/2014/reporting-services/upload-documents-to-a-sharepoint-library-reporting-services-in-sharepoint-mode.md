---
title: "Upload Documents to a SharePoint Library (Reporting Services in SharePoint Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "SharePoint integration [Reporting Services], viewing reports"
  - "SharePoint integration [Reporting Services], content management"
  - "uploading reports [Reporting Services]"
ms.assetid: 93bd1b19-061b-409f-8dc2-ec416b2f4b39
author: markingmyname
ms.author: maghan
manager: kfile
---
# Upload Documents to a SharePoint Library (Reporting Services in SharePoint Mode)
  You can upload report definitions and report models to a SharePoint library. When uploading a report server item, you must select a library or a folder within a library. You cannot upload a report server item to a list or page.  
  
 You cannot upload a data source (.rds) file. However, you can publish .rds files from a design tool, such as Report Designer, to a SharePoint library. During publication, a new .rsds file is created from the original .rds file in the solution. You can also create a new .rsds file in a SharePoint library and then set data source connection properties in the uploaded reports and models to use the new connection.  
  
> [!NOTE]  
>  The report server must be configured for SharePoint mode, and the instance of the SharePoint product must have the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Add-in that provides program files for storing and accessing report server items from a SharePoint site.  
  
 To upload a document to a library, you must have the "Add Items" permission at the site level. If you are using default security settings, this permission is granted to members of the **Owners** group who have the Full Control level of permission and to the **Members** group who have the Contribute level of permission.  
  
### To add a report definition or report model to a library  
  
1.  Open the library or a folder within a library. If the library is not already open, click its name on the Quick Launch. If the name of your library does not appear, click **View All Site Content**, and then click the name of your library.  
  
2.  On the **Upload** menu, click **Upload document**.  
  
3.  To upload a single report or report model file, select a report definition (.rdl) or report model (.smdl) file and then click **OK**.  
  
     If the report definition uses a shared data source (.rsds) file to store connection information to an external data source, you can upload the .rdl and the .rsds file at the same time. To do this, click **Upload Multiple Documents**, specify both files, and then click **OK**.  
  
 If you upload a report that contains references to shared data sources, report models, or subreports, the references will be broken in the report when the files are uploaded. For more information about how to reset the references, see [Create and Manage Shared Data Sources &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../2014/reporting-services/create-manage-shared-data-sources-reporting-services-sharepoint-integrated-mode.md).  
  
 When you upload a report, it runs on demand when you open it, retrieving live data from the data source. You can configure the report to retrieve data on a schedule or use cached data. For more information, see [Set Processing Options &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../2014/reporting-services/set-processing-options-reporting-services-in-sharepoint-integrated-mode.md).  
  
 A report can contain parameters so that users can filter data. You can configure the parameters to use specific values or change how they appear to the user. For more information, see [Set Parameters on a Published Report &#40;Reporting Services in SharePoint Integrated Mode&#41;](report-design/set-parameters-on-a-published-report-sharepoint-integrated-mode.md).  
  
## See Also  
 [Publish a Report to a SharePoint Library](reports/publish-a-report-to-a-sharepoint-library.md)   
 [Publish a Shared Data Source to a SharePoint Library](reports/publish-a-shared-data-source-to-a-sharepoint-library.md)   
 [Granting Permissions on Report Server Items on a SharePoint Site](security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md)  
  
  
