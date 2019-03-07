---
title: "View Data Refresh History (Power Pivot for SharePoint) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: ppvt-sharepoint
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# View Data Refresh History (Power Pivot for SharePoint)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Data refresh history is a record of all data refresh activity for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data in an Excel workbook. Data refresh operations are performed on an Analysis Services server instance in a SharePoint farm on a schedule that you provide. By default, data refresh history is retained for one year. However, a farm administrator can specify a different retention policy for usage and event history that determines how long data refresh records are kept.  
  
 **[!INCLUDE[applies](../../includes/applies-md.md)]**  SharePoint 2013 | SharePoint 2010  
  
 **In this topic:**  
  
 [Prerequisites](#prereq)  
  
 [View Data Refresh History for an Individual Workbook](#viewhistory)  
  
 [View Data Refresh History for All Workbooks](#viewITOps)  
  
 [Use History Information](#pageelements)  
  
##  <a name="prereq"></a> Prerequisites  
 You must have Contribute permissions or greater to view the data refresh history.  
  
 You must have enabled and scheduled data refresh on a workbook that contains [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data. If you have not scheduled data refresh, you will see the schedule definition page instead of history information.  
  
##  <a name="viewhistory"></a> View Data Refresh History for an Individual Workbook  
  
1.  On a SharePoint site, open the library that contains an Excel workbook that contains [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data.  
  
     There is no visual indicator that identifies which workbooks in a SharePoint library contain [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data. You must know in advance which workbook contains refreshable [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data.  
  
2.  Select the workbook, and then click the down arrow that appears to the right.  
  
3.  Select **Manage [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Data Refresh**.  
  
 The history page appears, showing a complete record for all refresh activity for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data in the current Excel workbook.  
  
##  <a name="viewITOps"></a> View Data Refresh History for All Workbooks  
 Using the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard in Central administration, farm administrators and service application administrators can get a comprehensive view of data refresh history and status for all [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks. For more information, see [Power Pivot Management Dashboard and Usage Data](../../analysis-services/power-pivot-sharepoint/power-pivot-management-dashboard-and-usage-data.md).  
  
##  <a name="pageelements"></a> Use History Information  
 The data refresh history page provides detailed information about each refresh operation. You can use the information on this page to confirm whether refresh occurred or determine why it failed.  
  
|Item|Description|  
|----------|-----------------|  
|Name|Specifies the file name of the Excel workbook that contains [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data.|  
|Current status|Values include **Scheduled**, **Refreshing**, **Succeeded**, or **Failed**.<br /><br /> **Scheduled** appears when you first create the schedule. After data refresh runs the first time, this status message no longer appears.<br /><br /> **Refreshing** indicates that data refresh is in progress. A request is either in the process queue or actively running on the server.<br /><br /> **Succeeded** indicates that the last data refresh operation completed and the updated workbook is checked back into the SharePoint library.<br /><br /> **Failed** indicates that the last data refresh operation did not succeed. The refreshed data was not saved. The workbook contains the same data it had before data refresh began.|  
|Last successful refresh|Specifies the date on which the last data refresh completed successfully.|  
|Next schedule refresh|Specifies the date on which the next data refresh is scheduled to occur.<br /><br /> The **Configure schedule** link takes you to the schedule definition page. If you have Contribute permissions on the workbook, you can click the link to view and modify the schedule information that controls unattended data refresh for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data in the workbook.|  
|Started|Within the history details section, **Started** indicates the actual processing time. The actual processing time might be different from what you scheduled. Processing will begin when sufficient memory is available on the server. If the server is very busy, the processing might begin several hours after the start time you specified.|  
|Completed|Within the history details section, **Completed** indicates when the data refresh operation finished. The date and time indicates when the workbook was checked back into the library.<br /><br /> If data refresh fails, one or more error messages explain the cause of the failure. You can expand each record to view detailed status. Each data source is listed individually, alongside success or failure messages that explain why data refresh did not complete.|  
|Time|Provides the cumulative time from when data refresh started to when it was completed.|  
|Status|Provides a historical record of whether a refresh operation succeeded or failed.|  
  
## See Also  
 [Configure Usage Data Collection for &#40;Power Pivot for SharePoint](../../analysis-services/power-pivot-sharepoint/configure-usage-data-collection-for-power-pivot-for-sharepoint.md)   
 [Schedule a Data Refresh (Power Pivot for SharePoint)](http://msdn.microsoft.com/8571208f-6aae-4058-83c6-9f916f5e2f9b)   
 [Power Pivot Data Refresh](../../analysis-services/power-pivot-sharepoint/power-pivot-data-refresh.md)  
  
  
