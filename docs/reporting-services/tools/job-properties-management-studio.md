---
title: "Job properties (Management Studio)"
description: Learn about the Job Properties page in SQL Server Management Studio where you view information about jobs on the report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.swb.reportserver.jobproperties.f1"
---
# Job properties (Management Studio)
  Use the **Job Properties** page to view information about an in-progress report or subscription before you cancel it.  
  
 To open this page, start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to a report server, and open the **Jobs** folder. Right-click a job that is running, and then select **Properties**.  
  
> [!NOTE]  
>  This feature is not supported in [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] with Advanced Services. The page does not appear when you are running [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)].  
  
## Tasks  
 Before you can view information about a job, refresh the page to retrieve information about jobs that are currently running on the report server:  
  
1.  Open the report server folder.  
  
2.  Right-click **Jobs**, and then select **Refresh**.  
  
3.  If a job is listed, right-click the job, and then select **Properties**.  
  
## Options  
 **Job ID**  
 A GUID that is assigned to a job while it's processing. The value is randomly generated each time a report or subscription runs.  
  
 **Job Status**  
 Valid values are **New** and **Running**. Status is always **New** when the job begins. After 60 seconds, status changes to **Running**. You must refresh the page to pick up the change.  
  
 **Job Type**  
 Valid values are **User** and **System**. A user job is any job that an individual user initiates. This job includes running a report on-demand, manually generating a report history snapshot, or manually creating a report execution snapshot. An in-progress standard subscription is also a user job. A system job is a job that the report server initiates. System jobs include report processing that a schedule triggers.  
  
 **Job Action**  
 For reports, this column shows the report execution processes that are underway. This value is always **Render**.  
  
 **Job Description**  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] doesn't provide job descriptions by default.  
  
 **Server Name**  
 Shows the name of the report server that is processing the job. If you configured a scale-out deployment, this value shows which server is processing the job.  
  
 **Report Name**  
 Shows the name of the report. You can identify subscriptions by their descriptions.  
  
 **Report Path**  
 Shows the path of the report in the report server folder hierarchy.  
  
 **Start Time**  
 Shows when the process started.  
  
 **User Name**  
 For processes initiated by a user, this column shows the name of the user. For system jobs, the user name is the name of the report server.  
  
## Related content  
 [Report server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)   
 [Connect to a report server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)   
 [Manage a running process](../../reporting-services/subscriptions/manage-a-running-process.md)  
  
  
