---
title: "Cancel Report Server Jobs (Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.reportserver.cancelreportserverjobs.f1"
ms.assetid: 1c5b4975-49e9-4d0b-b298-2638e81edbfd
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Cancel Report Server Jobs (Management Studio)
  Use the **Cancel Report Server Jobs** dialog box to view or cancel in-progress reports. This dialog box shows all jobs that are currently running on the report server. Although you cannot pause or restart jobs that are currently processing, you can cancel all jobs or individual jobs if they are taking too long to complete.  
  
 You can cancel user jobs and system jobs.  
  
-   A user job is any job that is initiated by an individual user. This includes running a report on-demand, manually creating a report history snapshot, or manually creating report execution snapshot. An in-progress standard subscription is also a user job.  
  
-   A system job is one that is initiated by the report server. System jobs include scheduled report processing.  
  
 To open this page, start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to a report server, right-click **Jobs**, and then click **Cancel All Jobs**. You can also open **Jobs**, right-click a job that is running on the report server, and select **Cancel Job(s)**.  
  
 Before cancelling a job, you can view its properties to determine when the job started. For more information, see [Job Properties &#40;Management Studio&#41;](job-properties-management-studio.md).  
  
> [!NOTE]  
>  This feature is not supported in [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] with Advanced Services. The page does not appear when you are running [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)].  
  
## Options  
 **Name**  
 Shows the name of the report. Subscriptions are identified by their descriptions.  
  
 **Type**  
 Valid values are **User** and **System**.  
  
 **Start Time**  
 Shows when the job started.  
  
 **User Name**  
 For jobs that are initiated by a user, this column shows the name of the user.  
  
 **Status**  
 Shows the status of the job. Valid values are **New** and **Running**. Status is always **New** when the job begins. After 60 seconds, status changes to **Running**. You must refresh the page to pick up the change.  
  
 **OK**  
 Cancel a single job or multiple jobs. The jobs are cancelled immediately and cannot be resumed. If you mistakenly cancel a job, you must request the report or subscription again to start a new job.  
  
## See Also  
 [Report Server in Management Studio F1 Help](report-server-in-management-studio-f1-help.md)   
 [Connect to a Report Server in Management Studio](connect-to-a-report-server-in-management-studio.md)   
 [Manage a Running Process](../subscriptions/manage-a-running-process.md)  
  
  
