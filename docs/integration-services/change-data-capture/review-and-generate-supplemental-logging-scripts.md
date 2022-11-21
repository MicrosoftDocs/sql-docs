---
description: "Review and Generate Supplemental Logging Scripts"
title: "Review and Generate Supplemental Logging Scripts | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "scripts"
ms.assetid: 5c858ae2-37d6-42e8-a252-7f6ed4e628a7
author: chugugrace
ms.author: chugu
---
# Review and Generate Supplemental Logging Scripts

  Use the **Scripts** tab to run or re-run a script on the Oracle source database that sets up supplemental logging.  
  
 Before you run the scripts select one of the following:  
  
 **Include changes in this session**  
 Select this to run the script on any new table that was added to the CDC instance or on tables that were changed in any way using the Properties editor.  
  
> [!NOTE]  
>  If no changes were made to the tables in the CDC instance, the Oracle supplemental logging script area will be empty.  
  
 **Include all tables/capture instances**  
 Select this to run the script on all of the tables and capture instances in the CDC instance.  
  
 After you select one of these options, run the supplemental logging script.  
  
### To run the supplemental logging scripts  
  
1.  Click **Run Script** to run the supplemental logging script on the tables defined for the CDC instance. This script instructs the Oracle database to write all columns of interest to its transaction logs when logging UPDATE operations to captured tables. This script is normally examined and executed by an Oracle system administrator.  
  
2.  When you run the supplemental logging scripts, the Oracle Credentials for Running Script dialog box opens where you provide a valid Oracle user name and password. For information on how to provide the proper Oracle credentials, see [Oracle Credentials for Running Script](../../integration-services/change-data-capture/oracle-credentials-for-running-script.md).  
  
 You can also run the scripts manually using SQL\*Plus, if necessary.  
  
### To run the scripts manually  
  
1.  Click **Copy** to paste the script to the clipboard. Open SQL* Plus and go to the directory with your Oracle source database. Paste the script into SQL\*Plus to run it.  
  
### To save the supplemental logging script in a text file  
  
1.  Click **Save as** and browse to the location where you want to save the file.  
  
2.  Give the file a name and click **Save** to save the file.  
  
## See Also  
 [How to Edit the CDC Instance Properties](../../integration-services/change-data-capture/how-to-edit-the-cdc-instance-properties.md)   
 [Oracle Credentials for Running Script](../../integration-services/change-data-capture/oracle-credentials-for-running-script.md)  
  
  
