---
title: "Job Activity Monitor (Filter Settings)"
description: Job Activity Monitor (Filter Settings)
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.jobactivitymon.filter.f1"
ms.assetid: 89cb0055-5262-447f-8464-7203d4caba78
---
# Job Activity Monitor (Filter Settings)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use this page to reduce the number of rows visible in the Job Activity Monitor. Enter criteria in one or several of the available boxes, to show only the rows that meet the specified values. Some of the boxes, such as **Status** or **Blocking Type** offer a finite number of possible values, provided by a drop-down list. Others, such as **Application,** allow you to enter whatever value and as many values as you wish, as a comma separated list. Toolbar icons allow you to sort the available boxes by category or alphabetically. Click the criteria to show a short description of the each one.  
  
 To filter the Job Activity Monitor, provide as many filter criteria as you want, click **Apply filter**, and then click **OK**.  
  
## All Jobs  
 This group of filter criteria is available when filtering the Job Activity Monitor.  
  
 **Name**  
 Filter jobs by name.  
  
 **Next Run**  
 Filter by the date next scheduled to run.  
  
 **Runnable**  
 View jobs that can be run, or jobs that cannot be run. Select **Yes** to view only jobs that can be run, select **No** to view only jobs that cannot be run, or select **All** to view both jobs that can be run and those that cannot.  
  
 **Last Run**  
 Filter by the date last run.  
  
 **Last Run Outcome**  
 Filter jobs by the status last time the jobs were run.  
  
 **Enabled**  
 View only enabled or not enabled jobs  
  
 **Category**  
 Filter jobs by the job category.  
  
 **Scheduled**  
 View all jobs with schedules, or without schedules.  
  
 **Status**  
 Filter jobs by the status.  
  
## Description Area  
 **Description Box**  
 This unnamed box provides a short description of the criteria as they are selected.  
  
 **Apply filter**  
 To apply the filter, click **Applyfilter** and then click **OK**. To retain the filter settings in the **FilterSettings** dialog box, but not apply them, uncheck **Applyfilter**, and then click **OK**, to display all rows.  
  
 **Clear**  
 Returns the filter settings back to the default settings.  
  
## See Also  
 [Monitor Job Activity](../../ssms/agent/monitor-job-activity.md)  
  
  
