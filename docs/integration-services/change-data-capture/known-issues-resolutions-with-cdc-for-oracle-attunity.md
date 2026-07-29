---
title: "Known errors and resolutions with change data capture for Oracle by Attunity"
description: "Known errors and resolutions with change data capture for Oracle by Attunity"
ms.date: "07/23/2019"
ms.service: sql
ms.topic: reference
ms.custom: sfi-image-nochange
---
# Known errors and resolutions with change data capture for Oracle by Attunity

[!INCLUDE [oracle-cdc-retirement](../includes/attunity-oracle-cdc-retirement.md)]

This topic lists the top issues and known resolutions when viewing a change data capture (CDC) instance in the Oracle CDC Designer configuration tool. This tool is part of the Change Data Capture for Oracle by Attunity that is included starting with SQL Server 2012. 

## Bug fixes
Before spending too much time troubleshooting, it is important to use the latest builds of CDC for Oracle by Attunity to avoid known issues such as these:

### SQL Server 2017

**Version 5.0.0.111** contains these fixes:
- Bug fix - Oracle CDC Designer fails with "Incorrect syntax near the keyword 'KEY'" error when adding an Oracle table. 
- Improvement - Improved support for RAC, this includes better handling when an RAC node is restarted. 
- Bug fix - The CDC is not working with Oracle 10.2 due to requesting NEXT_CHANGE# from the v$log. 


**Version 5.0.0.93** contains these fixes: 
- Microsoft CDC for Oracle by Attunity Designer fails with "Incorrect syntax near the word 'KEY'" error when adding an Oracle table. 

 
### SQL Server 2016

**Version 4.0.107** contains these fixes:
- Bug Fixes – Oracle CDC Designer fails with "Incorrect syntax near the keyword 'KEY'" error when adding an Oracle Table.
- Improvement – Improved support for RAC, this includes better handling when a RAC node is restarted.
- Bug Fixes – The CDC is not working with Oracle 10.2 due to requesting NEXT_CHANGE# from the v$log.

**Version 4.0.0.95** contains these fixes: 
- Bug Fixes – Oracle CDC Designer fails with "Incorrect syntax near the keyword 'KEY'" error when adding an Oracle Table.

**Version 4.0.0.88** contains these fixes:
-  Properties added in the Advanced options of the Attunity CDC instance are removed when a table is added or removed from CDC. 
- Attunity CDC stops working after applying SQL fix that adds __$command_id column

### SQL Server 2014 

**Version 2.0.0.114** contains these fixes:
- Bug Fixes – Oracle CDC Designer fails with "Incorrect syntax near the keyword 'KEY'" error when adding an Oracle Table.
- Improvement – Improved support for RAC, this includes better handling when a RAC node is restarted.
- Bug Fixes – The CDC is not working with Oracle 10.2 due to requesting NEXT_CHANGE# from the v$log.

**Version 2.0.0.92** contains these fixes: 
- Properties added in the Advanced options of the Attunity CDC instance are removed when a table is added or removed from CDC. Attunity CDC stops working after applying SQL fix that adds __$command_id column
- The metadata validation for Oracle table cdc.table_name failed. Column column_name index is out of range.  And this issue: Oracle CDC service shows aborted status when you use CDC for Oracle by Attunity
    - Fixed in _Cumulative Update 1 for SQL Server 2014 RTM_ as described in KB [2894025](https://support.microsoft.com/kb/2894025).
- Some changes are missed and are not replicated to the SQL Server database. This issue occurs when a table contains more than one character large binary object (CLOB) and one of the CLOBs has a large value. 
    - Fixed in _Cumulative Update 1 for SQL Server 2014 SP1_ and _Cumulative Update 8 for SQL Server 2014 RTM_ as described in KB [3029096](https://support.microsoft.com/kb/3029096). 
- Change Data Capture for Oracle by Attunity stops working when Oracle tables have column with Long data type.
    - Fixed in _Cumulative Update 5 for SQL Server 2014 SP1_ and _Cumulative Update 12 for SQL Server 2014 RTM_ as described in KB [KB4017793](https://support.microsoft.com/topic/kb4017793-cumulative-update-12-for-sql-server-2014-sp1-bc24907b-afe2-6703-ccd9-99d6cb76a74d).

## Collect detailed logs 

This section explains how to collect errors and events from the CDC instance. 

### Management console

You can see errors in the **Status** messages filed within the Oracle Change Data Capture Designer management console when a CDC instance is highlighted in the left pane. 

### Query trace table

You can query the trace table in the CDC database within SQL Server to see logged errors. 

### Save output from basic logging 

To capture diagnostics, select **Collect Diagnostics** on the status tab in the Oracle Change Data Capture management console. 

![Screenshot showing the Status tab in the Oracle Change Data Capture management console with the Collect Diagnostics option called out.](media/known-issues-resolutions-with-cdc-for-oracle-attunity/collect-diagnostics.png)

Choose a start time and select a location for the log file. Then select **Create** to start the diagnostics collection. 

![Screenshot of the Collect Diagnostics for testTA dialog box.](media/known-issues-resolutions-with-cdc-for-oracle-attunity/start-diagnostics.png)

### Detailed errors

You can increase the level of tracing collected by the instance and repeat the scenario to gather more detailed logging. To do so, select **Properties** under **Actions** and then add a new property in the **Advanced Settings** grid on the **Advanced** tab. Set the name of the property to `trace` and then set the value to `SOURCE`. 

![Screenshot showing the Properties option under Actions.](media/known-issues-resolutions-with-cdc-for-oracle-attunity/properties.png)

Reproduce the error and then select the **Collect diagnostics** option to gather logs. 

![Another screenshot of the Status tab in the Oracle Change Data Capture management console with the Collect Diagnostics option called out.](media/known-issues-resolutions-with-cdc-for-oracle-attunity/collect-diagnostics.png)

## ORA-00942 Table of view does not exist 

This is a common error displayed in the **Status** message field of the CDC Instance. The instance retries numerous times, so the status icon will change to green momentarily, but then it will fail with the red exclamation and UNEXPECTED status. 

![Screenshot showing the common error displayed in the Status message field of the CDC Instance.](media/known-issues-resolutions-with-cdc-for-oracle-attunity/oracle-error.png)

```
"ERROR","computername","ERROR","UNEXPECTED",
"ORACDC508E:Oracle method OCIStmtExecute failed with error: ORA-00942: table or view does not exist ","source","",""

"ERROR","computername","RUNNING","IDLE",
"ORACDC518E:Failed to verify archive log mode.","source","",""

"ERROR","computername","ERROR","UNEXPECTED",
"ORACDC517E:Oracle Call Interface (OCI) method failed: ORA-00942: table or view does not exist .","source","",""

"ERROR","computername","ERROR","UNEXPECTED",
"ORACDC414E:The Engine component failed with return code 1.","engine","",""
```

This happens when the Oracle account connecting from the CDC instance to the Oracle server does not have permission to see system log views. 

### Resolution

To resolve this error, either grant the currently configured user appropriate permissions within the Oracle database system, or change which account is being used to connect to the Oracle server from the CDC instance. 

The list of all the necessarily permissions is detailed in the help file included in the installation program files folder `C:\Program Files\Change Data Capture for Oracle by Attunity\Attunity.SqlServer.XdbCdcDesigner.chm`.  See the page titled "Connect to an Oracle Source Database" within the .chm file for the complete list.

You can set the user account by selecting the CDCInstance from the left pane and selecting the Properties button in the Actions right-most pane within the **CDC Designer** window. You can change the Oracle log mining authentication account from the properties dialogue page.

![Screenshot showing the Oracle tab of the testTA Properties dialog box.](media/known-issues-resolutions-with-cdc-for-oracle-attunity/oracle-connection.png)


  
## See also  
 [Track Data Changes &#40;SQL Server&#41;](../../relational-databases/track-changes/track-data-changes-sql-server.md)   
 [About Change Data Capture &#40;SQL Server&#41;](../../relational-databases/track-changes/about-change-data-capture-sql-server.md)   
 [Work with Change Data &#40;SQL Server&#41;](../../relational-databases/track-changes/work-with-change-data-sql-server.md)   
 [Administer and Monitor Change Data Capture &#40;SQL Server&#41;](../../relational-databases/track-changes/administer-and-monitor-change-data-capture-sql-server.md)  
  
  
