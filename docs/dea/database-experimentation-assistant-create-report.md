---
title: Create Analysis Reports with Database Experimentation Assistant for SQL Server upgrades
description: Create Analysis Reports with Database Experimentation Assistant
ms.custom: ""
ms.date: 10/12/2018
ms.prod: sql
ms.prod_service: dea
ms.suite: sql
ms.technology: dea
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: HJToland3
ms.author: jtoland
ms.reviewer: douglasl
manager: craigg
---

# Create Analysis Reports with Database Experimentation Assistant
After replaying the source trace on both of your target servers, you can generate an analysis report. Analysis reports help you gain insights about the performance implications of proposed changes.

## Open a new analysis report  
Open the tool and select the menu icon on the left side of the screen. This opens the left side-bar menu. Choose **Analysis Reports** next to the checklist icon to open the Analysis Reports section.

![Analysis Menu](./media/database-experimentation-assistant-create-report/dea-create-reports-menu.png)

From the **Analysis Reports** window, select **New analysis report**.

![New Analysis Report Menu](./media/database-experimentation-assistant-create-report/dea-create-reports-new-report.png)

The New analysis report window opens.

## Enter inputs for creating reports

Enter the following information in the input fields before starting the New analysis report operation.

![New Analysis Report Inputs](./media/database-experimentation-assistant-create-report/dea-create-reports-inputs.png)

- **Report name**: Provide a friendly or recognizable name that you want to call your report. This is the name used for both A and B databases, for example, **A (or B)** + **Report name** + **Unique Identifier**. 
- **Server name**: Provide the name of the server that you'd like to contain A, B, and Analysis databases.
- **SQL Server instance name**: Provide a SQL server where you want the report.
- **Trace for source server**: Provide the SQL server (2008 R2) first trace file (.trc).
- **Trace for target server**: Provide the target SQL server (2014) first trace file (.trc).

## Begin generating a report

After you enter all input information, select **Start** to begin creating the report. If the inputs are valid, the analysis report is created. Otherwise the fields that have invalid inputs are highlighted with red. Make sure the values are correct and again select **Start**. 

A new analysis report is generated. The analysis database follows the naming scheme **Analysis** + **User specified report name** + **unique identifier**.

## Frequently asked questions about analysis reports

### What does my analysis report tell me?
    
Using statistical tests, DEA analyzes your workload and determines how each query performed from Target 1 to Target 2, with performance details for each query. Learn more about DEA at [Get started](database-experimentation-assistant-get-started.md).
    
### Can I create a new analysis report while another report is being generated?
    
No.  Currently, only one report can be generated at a time to prevent conflicts. However, you can run more than one capture and replay at the same time.

### I upgraded DEA to version 2.0. Can I still view and use my old reports?
    
Yes. To view previously generated reports, you must update the schema of the report. Learn more [here](https://blogs.msdn.microsoft.com/datamigration/2017/03/24/dea-2-0-updating-db-schema-for-analysis-report-in-the-database-experimentation-assistant/).
    
### Can I generate an analysis report using command line?
    
Yes. You can generate an analysis report on the command line. You can then view it using the UI. Learn more [here](https://blogs.msdn.microsoft.com/datamigration/2016/10/25/database-experimentation-assistant-command-line/).
    
## Troubleshooting analysis reports
###  What security permissions do I need to generate and view an analysis report on my server?
    
The current logged in user in DEA should have sysadmin privilege to the analysis server. If the user is part of a group permission, make sure the group has sysadmin privileges as well.

|Possible Errors|Solution|  
|---|---|  
|Unable to connect to the database. Please ensure you have sysadmin privileges for analyzing and viewing the reports.|You might not have access or sysadmin permissions to the server and/or database.  Confirm your login permissions and privileges and try again|  
|Unable to generate Report Name on server Server Name. For details, check the Report Name report.|You might not have sysadmin privileges needed to generate a new report. Detailed errors can be found by clicking on the errored out report (see #4) and by checking the logs found at %temp%\\DEA.|  
|The current user doesn't have the required permissions to perform the operation. Please ensure you have sysadmin privileges for performing trace and analyzing the reports.|You don't have sysadmin privileges needed to generate a new report.|  

### I am unable to connect to the SQL Server.
    
- Confirm the SQL Server name is valid.  To confirm, try connecting to the server using SSMS.
- Confirm your firewall configuration isn't blocking connections to SQL Server.
- Confirm the user has the required permissions. 

Further details can be found in the logs at %temp%\\DEA. If the problem persists, contact the product team.

### I get an error when generating an analysis report.
    
First-time generation of an analysis report after installation of DEA requires internet access to download packages needed for statistical analysis.

If an error occurs during creation of the report, the progress page shows the specific step at which analysis generation failed. Further details can be found in the logs at %temp%\\DEA. Also check if you have a valid connection to the server with the appropriate permissions and retry. If the problem persists, contact the product team.

|Possible Errors|Solution|  
|---|---|  
|RInterop hit an error on startup. Please check RInterop logs and try again.|DEA requires internet access to download dependent R packages. Check RInterop logs at %temp%\\RInterop and DEA logs at %temp%\\DEA.  If RInterop was initialized incorrectly and/or without the correct R packages, you might see an exception "Failed to generate new analysis report." after the InitializeRInterop step in the DEA logs.<br><br>The RInterop logs can also show an error similar to "there is no jsonlite package available". If your machine doesn't have internet access, you can manually download the needed jsonlite R package with the following steps:<br><br><li>Navigate to the %userprofile%\\DEARPackages folder on the machine's filesystem.  This folder consists of the packages used by R for DEA.</li><br><li>If the jsonlite folder is missing in the list of installed packages, you'll need a machine with internet access to download the release version of jsonlite\_1.4.zip from [https://cran.r-project.org/web/packages/jsonlite/index.html](https://cran.r-project.org/web/packages/jsonlite/index.html).</li><br><li>Copy the zip to the machine where you are running DEA.  Extract the jsonlite folder and copy it over to %userprofile%\\DEARPackages.  This step automatically installs the jsonlite package in R. Note that the folder should be named 'jsonlite' and the contents should be directly inside the folder, not one level below.</li><br><li>Close the DEA app, reopen, and try analysis again.</li><br>Alternatively, you can use the RGUI. Navigate to packages -> install from zip.  Navigate to package you downloaded earlier and install.<br><br>If RInterop was initialized and set up correctly, you should see "Installing dependent R package "jsonlite"" in the RInterop logs.|  
|Unable to connect to the SQL Server instance, please ensure the server name is correct and have required access for the current logged in user.|You might not have access/privileges to server or the server name might be incorrect.|  
|RInterop process timed out. Check DEA and RInterop logs, kill RInterop process from task manager and try again.<br><br>or<br><br>RInterop is in faulted state. Kill RInterop from task manager and try again.|Check logs at %temp%\\RInterop to confirm the error and remove RInterop process from the Task Manager before trying again.  If problem persists, contact the product team.| 

### The report is generated, but data appears to be missing.
    
Check the database on the analysis SQL Server to confirm that data exists. Check that the analysis database exists and check its tables, for example, TblBatchesA, TblBatchesB, and TblSummaryStats.

If data doesn't exist, the data might not have copied over correctly or the database might be corrupt. If only some data is missing, the trace files created in capture or replay might not have captured your workload accurately. If the data is there, then further check the log files at %temp%\\DEA to see if any errors were logged. Then, try regenerating the analysis report.

Further questions or feedback? Submit feedback through the DEA tool by choosing the 'smiley' in the lower-left corner.  

## Next steps
For a 19-minute introduction and demonstration of this feature, watch the following video:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-the-Database-Experimentation-Assistant/player]
