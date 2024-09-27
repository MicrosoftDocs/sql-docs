---
title: "Cancel report server jobs (Management Studio)"
description: Learn how to use the options in the Cancel Report Server Jobs dialog box to view or cancel in-progress reports.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.swb.reportserver.cancelreportserverjobs.f1"
---
# Cancel report server jobs (Management Studio)
  Use the **Cancel Report Server Jobs** dialog box to view or cancel in-progress reports. This dialog box shows all jobs that are currently running on the report server. Although you can't pause or restart jobs that are currently processing, you can cancel all jobs or individual jobs if they're taking too long to complete.  
  
 You can cancel user jobs and system jobs.  
  
-   A user job is any job that an individual user initiates. This job includes running a report on-demand, manually creating a report history snapshot, or manually creating report execution snapshot. An in-progress standard subscription is also a user job.  
  
-   A system job is one that the report server initiates. System jobs include scheduled report processing.  
  
 To open this page, start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to a report server, right-click **Jobs**, and then select **Cancel All Jobs**. You can also open **Jobs**, right-click a job that is running on the report server, and select **Cancel Job(s)**.  
  
 Before canceling a job, you can view its properties to determine when the job started. For more information, see [Job properties &#40;Management Studio&#41;](../../reporting-services/tools/job-properties-management-studio.md).  
  
> [!NOTE]  
>  This feature is not supported in [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] with Advanced Services. The page does not appear when you are running [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)].  
  
## Options  
 **Name**  
 Shows the name of the report. You can identify subscriptions by their descriptions.  
  
 **Type**  
 Valid values are **User** and **System**.  
  
 **Start Time**  
 Shows when the job started.  
  
 **User Name**  
 For jobs that are initiated by a user, this column shows the name of the user.  
  
 **Status**  
 Shows the status of the job. Valid values are **New** and **Running**. Status is always **New** when the job begins. After 60 seconds, status changes to **Running**. You must refresh the page to pick up the change.  
  
 **OK**  
 Cancel a single job or multiple jobs. The jobs are canceled immediately and can't be resumed. If you mistakenly cancel a job, you must request the report or subscription again to start a new job.  
  
## Related content

- [Report server in Management Studio F1 hHelp](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)
- [Connect to a report server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)
- [Manage a running process](../../reporting-services/subscriptions/manage-a-running-process.md)
