---
description: "Native to SharePoint Migration (SSRS)"
title: "Native to SharePoint Migration | Microsoft Docs"
ms.date: 08/17/2017
ms.service: reporting-services


ms.topic: conceptual
ms.assetid: c5b15bec-6fde-4174-bcde-d043307244dd
author: maggiesMSFT
ms.author: maggies
monikerRange: "= sql-server-2016"
---
# Native to SharePoint Migration (SSRS)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

  You cannot upgrade or convert from one [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] server mode to another. For example, you cannot upgrade or convert a Native mode report server to SharePoint mode. You cannot copy the report server databases between modes because they use different database schemas. You can migrate the content from one report server to another. The tools you use depend on the type of report server mode that is configured for the source and destination servers.  
  
##  <a name="bkmk_native_to_sharepoint"></a> Reporting Services Migration tool  
 The tool supports content migration from a native mode Deployment to a SharePoint mode deployment. The tool does not support migration from SharePoint mode to SharePoint mode or from SharePoint mode to Native mode.  
  
 For more information, see [Reporting Services Migration Tool](https://www.microsoft.com/download/details.aspx?id=29560) (https://www.microsoft.com/download/details.aspx?id=29560).  
  
## Use Script to migrate content  
 If the migration tool does not meet your needs, you can manually migrate the report server data. The following is a summary of the steps to complete to migrate report items from a one [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] deployment ot another. The approach supports either Native or SharePoint mode as the source or destination servers.  
  
1.  Backup and restore encryption keys. This is the key that is used to encrypt data. The encryption key is also used to encrypt passwords such as the passwords stored for data source connections. However, passwords cannot be migrated and you will need to enter them again in the destination environment.  
  
2.  **[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] RSS scripts:** Write a Visual Basic script that calls Report Server Web service SOAP methods to copy data between databases. Use the **RS.exe** utility to run the script. Rs.exe is installed with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
    -   [Sample Reporting Services rs.exe Script to Copy Content between Report Servers](../../reporting-services/tools/sample-reporting-services-rs-exe-script-to-copy-content-between-report-servers.md). The topics explains how to use the sample script you can download from CodePlex.  
  
    -   The sample rss script on CodePlex, [Reporting Services RS.exe script that migrates content from one report server to another](../tools/sample-reporting-services-rs-exe-script-to-copy-content-between-report-servers.md)  
  
    -   [Scripting and PowerShell with Reporting Services](../../reporting-services/tools/scripting-and-powershell-with-reporting-services.md)  
  
 The following table summarizes the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] objects you can migrate with scripts:  
  
|Object|Can be Scripted|Comments|  
|------------|---------------------|--------------|  
|Reports|Yes|Following migration, to re-enter passwords for datasources.|  
|Datasources|Yes|Following migration, Re-link reports to datasources.|  
|Models|Yes||  
|Datasets|Yes||  
|Report Parts||Following migration, verify or update the path to the report parts.|  
|Schedules|Yes|See the ListSchedules method [Subscription and Delivery Methods](../../reporting-services/report-server-web-service/methods/subscription-and-delivery-methods.md)|  
|Subscriptions|yes|See the List Subscriptions method [Subscription and Delivery Methods](../../reporting-services/report-server-web-service/methods/subscription-and-delivery-methods.md) and the <xref:ReportService2010.ReportingService2010.ChangeSubscriptionOwner%2A> method.|  
|Snapshots|||

[!INCLUDE [ssrs-report-parts-deprecated](../../includes/ssrs-report-parts-deprecated.md)] 

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)