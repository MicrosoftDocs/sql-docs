---
title: Create analysis reports in Database Experimentation Assistant for SQL Server upgrades
description: Create Analysis Reports in Database Experimentation Assistant
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

# Create analysis reports in Database Experimentation Assistant

After replaying the source trace on both of your target servers, you can generate an analysis report. Analysis reports help you gain insights about the performance implications of proposed changes.

## Create a new analysis report

Open the tool and select the menu icon on the left side of the page. In the expanded vertical menu, select  **Analysis Reports** next to the checklist icon.

![Analysis menu](./media/database-experimentation-assistant-create-report/dea-create-reports-menu.png)

Under **Analysis Reports**, select **New analysis report**.

![New Analysis Report menu](./media/database-experimentation-assistant-create-report/dea-create-reports-new-report.png)

## Enter inputs for creating reports

Enter the following information before you start the **New analysis report** operation.

![New Analysis Report Inputs](./media/database-experimentation-assistant-create-report/dea-create-reports-inputs.png)

- **Report name**: Provide a friendly or recognizable name that you want to call your report. This name is used for both A and B databases, for example, **A (or B)** + **Report name** + **Unique Identifier**. 
- **Server name**: Provide the name of the server that you'd like to include A, B, and Analysis databases.
- **SQL Server instance name**: Provide a SQL Server where you want the report.
- **Trace for source server**: Provide the SQL Server (2008 R2) first trace file (.trc).
- **Trace for target server**: Provide the target SQL Server (2014) first trace file (.trc).

## Begin generating a report

After you enter the required information, select **Start** to begin creating the report. If the inputs are valid, the analysis report is created. Otherwise the fields that have invalid inputs are highlighted with red. Make sure the values are correct and again select **Start**. 

A new analysis report is generated. The analysis database follows the naming scheme **Analysis** + *user-specified report name* + *unique identifier*.

## Frequently asked questions about analysis reports

### What does my analysis report tell me?
    
Using statistical tests, DEA analyzes your workload and determines how each query ran from Target 1 to Target 2, with performance details for each query. Learn more about DEA at [Get started](database-experimentation-assistant-get-started.md).
    
### Can I create a new analysis report while another report is being generated?
    
No.  Currently, only one report can be generated at a time to prevent conflicts. However, you can run more than one capture and replay at the same time.

### I upgraded DEA to version 2.0. Can I still view and use my old reports?
    
Yes. To view previously generated reports, you must update the schema of the report. Learn more [here](https://blogs.msdn.microsoft.com/datamigration/2017/03/24/dea-2-0-updating-db-schema-for-analysis-report-in-the-database-experimentation-assistant/).
    
### Can I generate an analysis report by using the command line?
    
Yes. You can generate an analysis report on the command line. You can then view it using the UI. For more information, see [Run at command prompt](database-experimentation-assistant-run-command-prompt.md).
    
## Troubleshoot analysis reports

###  What security permissions do I need to generate and view an analysis report on my server?
    
The user who is logged in to DEA must have sysadmin rights on the analysis server. If the user is part of a group permission, make sure the group has sysadmin rights.

|Possible errors|Solution|  
|---|---|  
|Unable to connect to the database. Make sure you have sysadmin rights for analyzing and viewing the reports.|You might not have access or sysadmin rights to the server or database. Confirm your login rights and try again.|  
|Unable to generate **Report Name** on the server **Server Name**. For details, check the **Report Name** report.|You might not have sysadmin rights needed to generate a new report. To see detailed errors, select the errored-out report (see #4). You can also check the logs in at %temp%\\DEA.|  
|The current user doesn't have the required permissions to run the operation. Make sure you have sysadmin rights for performing trace and analyzing the reports.|You don't have sysadmin rights needed to generate a new report.|  

### I can't connect to the computer running SQL Server
    
- Confirm that the name of the computer running SQL Server is valid. To confirm, try to connect to the server by using SQL Server Management Studio (SSMS).
- Confirm that your firewall configuration doesn't block connections to the computer running SQL Server.
- Confirm that the user has the required user rights. 

You can see more details in the logs at %temp%\\DEA. If the problem persists, contact the product team.

### I get an error when I generate an analysis report
    
First-time generation of an analysis report after installation of DEA requires internet access to download packages needed for statistical analysis.

If an error occurs while the report is created, the progress page shows the specific step at which analysis generation failed. You can see more details in the logs at %temp%\\DEA. Also, verify that you have a valid connection to the server with the required user rights and retry. If the problem persists, contact the product team.

|Possible errors|Solution|  
|---|---|  
|RInterop hit an error on startup. Check RInterop logs and try again.|DEA requires internet access to download dependent R packages. Check RInterop logs at %temp%\\RInterop and DEA logs at %temp%\\DEA. If RInterop was initialized incorrectly or if it initialized without the correct R packages, you might see an exception "Failed to generate new analysis report" after the InitializeRInterop step in the DEA logs.<br><br>The RInterop logs also might show an error similar to "there's no jsonlite package available". If your machine doesn't have internet access, you can manually download the required jsonlite R package:<br><br><li>Go to the %userprofile%\\DEARPackages folder on the machine's file system. This folder consists of the packages used by R for DEA.</li><br><li>If the jsonlite folder is missing in the list of installed packages, you need a machine with internet access to download the release version of jsonlite\_1.4.zip from [https://cran.r-project.org/web/packages/jsonlite/index.html](https://cran.r-project.org/web/packages/jsonlite/index.html).</li><br><li>Copy the .zip file to the machine where you're running DEA.  Extract the jsonlite folder and copy it to %userprofile%\\DEARPackages. This step automatically installs the jsonlite package in R. The folder should be named **jsonlite** and the contents should be directly inside the folder, not one level below.</li><br><li>Close the DEA app, reopen, and try analysis again.</li><br>You can also use the RGUI. Go to **packages** > **install from zip**. Go to the package you downloaded earlier and install.<br><br>If RInterop was initialized and set up correctly, you should see **Installing dependent R package jsonlite** in the RInterop logs.|  
|Unable to connect to the SQL Server instance, make sure the server name is correct and check for the required access for the user who is logged in.|You might not have access or user rights to the server or the server name might be incorrect.| 
|RInterop process timed out. Check DEA and RInterop logs, stop the RInterop process in Task Manager, and then try again.<br><br>or<br><br>RInterop is in faulted state. Stop the RInterop process in Task Manager, and then try again.|Check logs at %temp%\\RInterop to confirm the error. Remove the RInterop process from Task Manager before trying again. Contact the product team if the problem persists.| 

### The report is generated, but data appears to be missing
    
Check the database on the analysis computer running SQL Server to confirm that data exists. Check that the analysis database exists and check its tables. For example, check these tables: TblBatchesA, TblBatchesB, and TblSummaryStats.

If data doesn't exist, the data might not have copied correctly or the database might be corrupt. If only some data is missing, the trace files created in capture or replay might not have captured your workload accurately. If the data is there, check the log files at %temp%\\DEA to see if any errors were logged. Then, try again to generate the analysis report.

More questions or feedback? Submit feedback through the DEA tool by choosing the smiley icon in the lower-left corner.  

## Next steps

To learn how to view the analysis report, see [View reports](database-experimentation-assistant-view-report.md).

- For a 19-minute introduction to DEA and demonstration, watch the following videoo:

  > [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-the-Database-Experimentation-Assistant/player]
