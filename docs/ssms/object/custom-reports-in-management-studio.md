---
description: "Custom Reports in Management Studio"
title: "Custom Reports in Management Studio"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.summary.new.custom.report.f1"
  - "sql13.swb.summary.render.custom.report.f1"
helpviewer_keywords: 
  - "SQL Server Management Studio [SQL Server], custom reports"
ms.assetid: 1ba3f758-f39b-4f5f-91ca-516cedc78979
author: "markingmyname"
ms.author: "maghan"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
---

# Custom Reports in Management Studio
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], many Object Explorer nodes display a set of standard reports that are created by [!INCLUDE[msCoName](../../includes/msconame-md.md)]. These reports summarize typically requested server information. Starting with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 2, administrators can run custom reports that were created in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] from [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
## Implementation  
Custom reports are stored as report definition (.rdl) files and are created by using Report Definition Language (RDL). RDL contains data retrieval and layout information for a report in an XML format. RDL is an open schema. Developers can extend RDL with additional attributes and elements. Reports can execute any valid [!INCLUDE[tsql](../../includes/tsql-md.md)] statement within the report.  
  
If Object Explorer is connected to a server, custom reports can execute in the context of the current Object Explorer selection if the reports reference report parameters of that node. This enables a report to use the current context, such as the current database; or a consistent context, such as specifying a designated database as part of the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that is contained in the custom report.  
  
## Running a Custom Report  
You can run a custom report in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] in the following ways:  
  
-   Right-click a node in Object Explorer, point to **Reports** and left-click **Custom Reports**. In the **Open File** dialog box, locate a folder that contains .rdl files, and then open the appropriate report file.  
  
-   Right-click a node in Object Explorer, point to **Reports**, point to **Custom Reports**, and then select a custom report from the most recently used file list.  
  
## Limitations  
When you work with custom reports, consider the following limitations:  
  
-   To prevent the unintended execution of malicious code, [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] cannot be configured to automatically run a report, even if the file system is configured to associate .rdl files with [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. Reports cannot be programmatically executed in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and cannot run from the command line through [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
-   You can run custom reports in a context that does not produce the expected values. For example, you can run a report about replication in the context of a database that is not involved in replication, or run a report as a user who does not have permission to access information that is required to generate an accurate report. The creator of the custom report is responsible for the validity of the report structure and its context.  
  
-   You cannot add a custom report to the list of standard reports.  
  
-   The code processed by the report might affect server performance.  
  
-   Custom reports will not support subreports.  
  
-   The command text for each query within the report must not be defined through an expression.  
  
-   Any query parameter that is used in a command (query) can only reference a single report parameter and cannot use any expression operators.  
  
-   Only Text and Stored Procedure command types are supported for report commands (queries).  
  
-   The report framework does not provide any parameter escaping for the queries. Query authors must make sure that their queries are free from SQL injection attacks.  
  
## Managing Custom Reports  
We recommend that users who have many custom reports organize them by using file system folders that have appropriate NTFS file system permissions.  
  
## Permissions  
Custom reports run by using the permissions of the current user. To prevent a malicious user from changing the queries run by the report, permissions on the file system folder that contains the report files should be set to restrict access.  
  
Both the user and the account that is used by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service require read access to the file system folder that contains the report files.  
  
Any valid [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] command can be embedded in a report, but the command will not be executed.  
  
> [!CAUTION]  
> Any valid [!INCLUDE[tsql](../../includes/tsql-md.md)] statement can be embedded in and executed from a report. Running a report under a high-privileged user account makes it possible for any of these embedded instructions to execute without challenge.  
  

  
## See Also  
[Add a Custom Report to Management Studio](../../ssms/object/add-a-custom-report-to-management-studio.md)  
[Unsuppress Run Custom Report Warnings](../../ssms/object/unsuppress-run-custom-report-warnings.md)  
[Use Custom Reports with Object Explorer Node Properties](../../ssms/object/use-custom-reports-with-object-explorer-node-properties.md)  
  
