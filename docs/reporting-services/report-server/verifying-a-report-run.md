---
title: "Verify a report run"
description: Learn how to use log files or refer to status information that is displayed with a report to verify a report run in Report Manager.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "auditing [Reporting Services]"
  - "verifying report execution"
  - "logs [Reporting Services], verifying report run"
  - "report execution data [Reporting Services]"
  - "status information [Reporting Services]"
  - "report processing [Reporting Services], verifying execution"
  - "checking report execution"
---
# Verify a report run
  To view information about the status of report processing, you can use log files or refer to status information that is displayed with the report in Report Manager.  
  
## Sources of report execution data  
 The report execution logs provide comprehensive information about report execution. The logs include the name of the report, the name of the user who ran the report, report execution time, and the delivery extension used to distribute the report. To view and analyze this data, you can copy the report execution log into database tables. These tables are easy to query and report on.  
  
 Log files contain many entries about report execution and other server activity. Because log files contain so much data, you might want to use Report Manager if you only want to verify when the report last ran. If you require additional information, you must view the log files.  
  
> [!NOTE]  
>  Report Manager doesn't show the processing times of reports that run on demand.  
  
 The following table describes how to view the processing date and time for various types of reports.  
  
|For this type of report|Date and time information is located here|To view the information, do the following|  
|-----------------------------|-----------------------------------------------|-----------------------------------------------|  
|A report that runs as a report snapshot.|On the **Contents** page. For more information, see [Contents page &#40;Report Manager&#41;](/previous-versions/sql/sql-server-2016/ms186470(v=sql.130)).|1) Locate the folder that contains the report.<br /><br /> 2) Set the folder in Details view.<br /><br /> 3) Note the date and time in the **When Run** column.|  
|A snapshot in report history.|On the **History Properties** page. For more information, see [Snapshot Options Properties Page &#40;Report Manager&#41;](/previous-versions/sql/sql-server-2016/ms189952(v=sql.130)).|1) Open the report.<br /><br /> 2) Select the **Properties** page.<br /><br /> 3) Select the **History** tab.<br /><br /> 4) Note the date and time in the **When Run** column.|  
|A cached report.|In the schedule used to create and refresh the cached report.|1) Open the report.<br /><br /> 2) Select the **Properties** page.<br /><br /> 3) Select the **Execution** tab.<br /><br /> 4) Open the schedule.|  
  
## Related content 
 [Reporting Services log files and sources](../../reporting-services/report-server/reporting-services-log-files-and-sources.md)   
 [Set report processing properties](../../reporting-services/report-server/set-report-processing-properties.md)   
 [Report Manager  &#40;SSRS native mode&#41;](../web-portal-ssrs-native-mode.md)  
  
