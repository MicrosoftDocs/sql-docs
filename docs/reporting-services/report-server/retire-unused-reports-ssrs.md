---
title: "Find and retire unused reports | Microsoft Docs"
description: You don't want to migrate unused reports to the Power BI service. As you plan for a migration to the cloud, we suggest doing some housekeeping to get rid of unused SSRS reports.
ms.date: 11/21/2022
ms.service: reporting-services
ms.prod_service: "reporting-services-native"
ms.subservice: report-server
ms.topic: conceptual
author: maggiesMSFT
ms.author: maggies
---

# Find and retire unused reports (SSRS)

Your company may deal with hundreds of SQL Server Reporting Services (SSRS) reports. Some of those reports may become obsolete and unused and need to be retired. As a report author or administrator, you don't want to migrate unused reports to the [Power BI service](https://app.powerbi.com). As you plan for a migration to the cloud, we suggest doing some housekeeping to get rid of unused SSRS reports. This best practice supports retention governance and allows your organization to make use of a retention schedule and data policy.  

There are two processes for checking unused reports. We extend the cleanup to unused objects, as well as to get rid of unused database tables that could have potentially stale data.  

## Run an audit (optional)  

First, we suggest that you create a [server audit and database audit specification](/sql/relational-databases/security/auditing/create-a-server-audit-and-database-audit-specification). Auditing an instance of the SQL Server Database Engine or an individual database involves tracking and logging events that occur on the Database Engine. SQL Server audit lets you create server audits, which can contain server audit specifications for server level events, and database audit specifications for database level events. Audited events can be written to the event logs or to audit files.

Once you've filled your audit log with tables and stored procedures used for reports, you can export those objects to an Excel file and share them with stakeholders. Let them know you are preparing to deprecate unused objects.  

> [!NOTE]
> Some important reports may run only rarely, so be sure to ask for feedback on database objects that are infrequently used. By deprecating an object, you can alter the object name by placing a *zdel* in front of it, so the object drops to the bottom of the Object Explorer. This way, if you decide later that you need the *zdel* object, you can alter the name back to the original. Once you know you are ready to remove them from your database, you can create a cadence to delete unused objects. 

## Create a Reports Usage metrics list

Second, you will want to create a SSRS Reports Usage metrics list by querying Report Server DB. Use the T-SQL below to derive the usage counts. If your report server is configured to store one year of report execution history, you can use a specific date to filter the usage metrics.

```tsql

; with UnusedReportsCte 
AS 
( 
SELECT 
Cat.Name,Path,COUNT(ExeLog.TimeStart) AS Cnt 

FROM (SELECT * FROM Catalog 
WHERE type=2 and Hidden=0) AS Cat 
LEFT JOIN 
ExecutionLog AS ExeLog 
ON ExeLog.ReportID = Cat.ItemID 
AND ExeLog.TimeStart>'01/01/2021' 
GROUP BY Cat.Name,Path) 
SELECT * FROM UnusedReportsCte 
ORDER BY Cnt ASC,path 
```
 
> [!NOTE]
> Subreports and linked reports don't appear in the execution log if the parent report is executed.

From here you can decide whether to delete the unused reports right away or replace the report with a message. You can let your users know the report is no longer being used, so they can contact an administrator for support. Then you can develop a cadence to delete them over time.

## See also

[Publish .rdl files to Power BI from Reporting Services](/power-bi/guidance/publish-reporting-services-power-bi-service)
[Migrate SQL Server Reporting Services reports to Power BI](/power-bi/guidance/migrate-ssrs-reports-to-power-bi)
