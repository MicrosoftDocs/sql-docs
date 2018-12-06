---
title: "Generate and Run the Supplemental Logging Script | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "supLog"
ms.assetid: 6e940d93-12c6-4cda-9333-5489b245f0e4
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Generate and Run the Supplemental Logging Script
  Use the Set up Tables for Capturing Changes page to run a script on the Oracle source database that will set up supplemental logging.  
  
 **To run the supplemental logging scripts**  
  
 Click **Run Script** to run the supplemental logging script on the tables defined for the CDC instance. This script instructs the Oracle database to write all columns of interest to its transaction logs when logging UPDATE operations to captured tables. This script is normally examined and executed by an Oracle system administrator.  
  
 You can also run the scripts manually using SQL * Plus.  
  
 **To run the scripts manually**  
  
 Click **Copy** to paste the script to the clipboard. Open SQL* Plus and go to the directory with your Oracle source database. Paste the script into SQL\*Plus to run it.  
  
 To save the supplemental logging script in a text file, click **Save as** and browse to the location where you want to save the file. Give the file a name and click **Save** to save the file.  
  
 Click **Next** to [Generate Mirror Tables and CDC Capture Instances](generate-mirror-tables-and-cdc-capture-instances.md).  
  
## See Also  
 [How to Create the SQL Server Change Database Instance](how-to-create-the-sql-server-change-database-instance.md)   
 [Review and Generate Supplemental Logging Scripts](review-and-generate-supplemental-logging-scripts.md)  
  
  
