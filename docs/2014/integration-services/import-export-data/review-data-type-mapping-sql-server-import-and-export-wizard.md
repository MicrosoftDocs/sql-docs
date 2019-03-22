---
title: "Review Data Type Mapping (SQL Server Import and Export Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.impexpwizard.reviewissues.f1"
ms.assetid: 0625c4f9-b8ff-4593-b884-39398b9d43af
author: janinezhang
ms.author: janinez
manager: craigg
---
# Review Data Type Mapping (SQL Server Import and Export Wizard)
  Use the **Review Data Type Mapping** page to review detailed information about data type conversions that the wizard has to perform to make the source data compatible with the destination. This information includes visual cues to distinguish conversions that are expected to succeed from conversions that might cause errors or truncations. For each conversion, you can decide whether to accept the conversion that the wizard suggests, and you can specify how to handle any errors that occur.  
  
 To learn more about this wizard, see [SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md). To learn about the options for starting the wizard, and about the permissions required to run the wizard successfully, see [Run the SQL Server Import and Export Wizard](start-the-sql-server-import-and-export-wizard.md).  
  
 The purpose of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard is to copy data from a source to a destination. The wizard can also create a destination database and destination tables for you. However, if you have to copy multiple databases or tables, or other kinds of database objects, you should use the Copy Database Wizard instead. For more information, see [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
## Options  
 The **Review Data Type Mapping** page consists of a **Table** list, a **Data type mapping** list, and error handling options.  
  
### Table List  
 The upper part of the **Review Data Type Issues** page is a **Table** list that lists the tables to be transferred from the source to the destination. The following table describes the columns in this list.  
  
|Column|Description|  
|------------|-----------------|  
|Source icon|Indicates the probability of success for the data type conversions:<br /><br /> A green check mark icon indicates that the wizard expects all data type conversions for this table to succeed.<br /><br /> A yellow warning icon indicates that you should review the individual conversions that the wizard will perform. To review these conversions, select the table, and then review the conversions for individual columns in the **Data type mapping** list.<br /><br /> A red error icon indicates that the wizard is not able to perform some of the conversions for this table reliably.|  
|**Source**|Displays the name of the source table.|  
|Destination icon|Indicates whether the destination already exists or will be created by the wizard:<br /><br /> A table icon indicates that the destination is an existing table.<br /><br /> A table icon with a sunburst indicates that the destination is a new table that the wizard will create.|  
|**Destination**|Displays the name of the destination table.|  
  
 To view conversion information about an individual table, select a table in this **Table** grid. The conversion information for the selected table appears in the columns in the **Data type mapping grid** in the lower part of the page.  
  
### Data type mapping List  
 The lower part of the **Review Data Type Issues** page is the **Data type mapping** list. This grid provides detailed conversion information about the columns in the table that is selected in the **Table** list. The following table describes the columns in this list.  
  
|Column|Description|  
|------------|-----------------|  
|Conversion icon|Indicates the probability of success for the data type conversions:<br /><br /> A green check mark icon indicates that the wizard expects that the data type conversion for this column to succeed.<br /><br /> A yellow warning icon indicates that you should review the conversion that the wizard will perform. To review the conversion, double-click on the column to view the **Column Conversion Details** dialog box.<br /><br /> A red error icon indicates that the wizard is not able to perform the conversion reliably.|  
|**Source Column**|Displays the name of the source column.|  
|**Source Type**|Displays the data type of the source column.|  
|**Destination Column**|Displays the name of the destination column.|  
|**Destination Type**|Displays the data type of the destination column.|  
|**Convert**|Specify whether the planned conversion should continue:<br /><br /> Select the check box to have the wizard continue with the planned conversion.<br /><br /> Clear the check box to cancel the data type conversion.|  
|**On Error**|Specify how the wizard handles errors:<br /><br /> Use the **On Error (global)** setting.<br /><br /> Fail with an error, and stop the import or export process.<br /><br /> Ignore the error.|  
|**On Truncation**|Specify how the wizard handles truncation:<br /><br /> Use the **On Truncation (global)** setting.<br /><br /> Fail with an error, and stop the import or export process<br /><br /> Ignore the truncation.|  
  
 To view detailed information about the conversion of a particular column of data, double-click any row in the list. The **Column Conversion Details** dialog box opens and displays more detailed conversion information for the column.  
  
### Error Handling Options  
 **On Error (global)**  
 Specify how the wizard handles errors:  
  
-   Fail with an error, and stop the import or export process.  
  
-   Ignore the error, and continue the import or export process.  
  
 This setting applies to all conversions that have **Use Global** selected in the **On Error** column of the **Data type mapping** list.  
  
 **On Truncation (global)**  
 Specify how the wizard handles truncation:  
  
-   Fail with an error, and stop the import or export process.  
  
-   Ignore the truncation, and continue the import or export process.  
  
 This setting applies to all conversions that have **Use Global** selected in the **On Truncation** column of the **Data type mapping** list.  
  
  
