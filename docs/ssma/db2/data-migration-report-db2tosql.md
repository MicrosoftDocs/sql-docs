---
title: "Data Migration Report (DB2ToSQL) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-ssma"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "Azure SQL Database"
  - "SQL Server"
ms.assetid: 46ebada7-db36-4ae9-b7ae-baa4b854b237
caps.latest.revision: 4
author: "sabotta"
ms.author: "carlasab"
manager: "lonnyb"
---
# Data Migration Report (DB2ToSQL)
The **Data Migration Report** dialog box appears after you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)].  
  
## Options  
**Status**  
Shows the status of the data migration from the source to the target database.  
  
**From**  
The source table.  
  
**To**  
The target table.  
  
**Total Number of Rows**  
The number of rows of data in the source table.  
  
**Number of Successfully Migrated Rows**  
The number of rows of data successfully migrated to the target table.  
  
**Ratio**  
The percentage of rows successfully migrated.  
  
**Details**  
If any data migration failed, click to display migration details for the selected row in the report. SSMA will display the reason for the failure.  
  
**Save Report**  
Saves the report to a .CSV, (comma-separated values) file, which can be examined using Microsoft Excel.  
  
