---
title: "CDC Source | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.designer.cdcsource.f1"
ms.assetid: 99775608-e177-44ed-bb44-aaccb0f4f327
author: janinezhang
ms.author: janinez
manager: craigg
---
# CDC Source
  The CDC source reads a range of change data from [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] change tables and delivers the changes downstream to other SSIS components.  
  
 The range of change data read by the CDC source is called the CDC Processing Range and is determine by the CDC Control task that is executed before the current data flow starts. The CDC Processing Range is derived from the value of a package variable that maintains the state of the CDC processing for a group of tables.  
  
 The CDC source extracts data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using a database table, a view, or an SQL statement.  
  
 The CDC source uses the following configurations:  
  
-   A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ADO.NET connection manager to access the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CDC database. For more information about configuring the CDC source connection, see [CDC Source Editor &#40;Connection Manager Page&#41;](../cdc-source-editor-connection-manager-page.md).  
  
-   A table enabled for CDC.  
  
-   The name of the capture instance of the selected table (if more-than-one exists).  
  
-   The change processing mode.  
  
-   The name of the CDC state package variable based on which the CDC Processing range is determined. The CDC source does not modify that variable.  
  
 The data returned by the CDC Source is the same as that returned by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CDC functions **cdc.fn_cdc_get_all_changes_\<capture-instance-name>** or **cdc.fn_cdc_get_net_changes_\<capture-instance-name>** (when available). The only optional addition is the column, **__$initial_processing** that indicates whether the current processing range can overlap with an initial load of the table. For more information about initial processing, see [CDC Control Task](../control-flow/cdc-control-task.md).  
  
 The CDC source has one regular output and one error output.  
  
## Error Handling  
 The CDC source has an error output. The component error output includes the following output columns:  
  
-   **Error Code**: The value is always -1.  
  
-   **Error Column**: The source column causing the error (for conversion errors).  
  
-   **Error Row Columns**: The record data that causes the error.  
  
 Depending on the error behavior setting, the CDC source supports returning errors (data conversion, truncation) that occur during the extraction process in the error output. For more information, see [CDC Source Editor &#40;Error Output Page&#41;](../cdc-source-editor-error-output-page.md).  
  
## Data Type Support  
 The CDC source component for Microsoft supports all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types, which are mapped to the correct SSIS data types.  
  
## Troubleshooting the CDC Source  
 The following contains information on troubleshooting the CDC source.  
  
### Use this script to isolate problems and reproduce them in SQL Server Management Studio  
 The CDC source operation is governed by the operation of the CDC Control task executed before invoking the CDC source. The CDC Control task prepares the value of the CDC state package variable to contain the start and end LSNs. It performs function equivalent to the following script:  
  
```  
use <cdc-enabled-database-name>  
               declare @start_lsn binary(10), @end_lsn binary(10)  
               set @start_lsn = sys.fn_cdc_increment_lsn(  
                       convert(binary(10),'0x' + '<value-from-state-cs>', 1))  
               set @end_lsn =   
                       convert(binary(10),'0x' + '<value-from-state-ce>', 1)  
               select * from cdc.fn_cdc_get_net_changes_dbo_Table1(@start_lsn,  
@end_lsn, '<mode>')  
```  
  
 where:  
  
-   \<cdc-enabled-database-name> is the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database containing the change tables.  
  
-   \<value-from-state-cs> is the value that appears in the CDC state variable as CS/\<value-from-state-cs>/ (CS stands for Current-processing-range-Start).  
  
-   \<value-from-state-ce> is the value that appears in the CDC state variable as CE/\<value-from-state-cs>/ (CE stands for Current-processing-range-End).  
  
-   \<mode> are the CDC processing modes. The processing modes have one of the following values **All**, **All with Old Values**, **Net**, **Net with Update Mask**, **Net with Merge**.  
  
 This script helps isolate problems by reproducing them in the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], where it is easy to reproduce and identify errors.  
  
#### SQL Server Error Message  
 The following message may be returned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
 **An insufficient number of arguments were supplied for the procedure or function cdc.fn_cdc_get_net_changes_\<..>.**  
  
 This error does not indicate that an argument is missing. It means that the start or end LSN values in the CDC state variable are invalid.  
  
## Configuring the CDC Source  
 You can configure the CDC source programmatically or through the SSIS Designer.  
  
 For more information, see one of the following topics:  
  
-   [CDC Source Editor &#40;Connection Manager Page&#41;](../cdc-source-editor-connection-manager-page.md)  
  
-   [CDC Source Editor &#40;Columns Page&#41;](../cdc-source-editor-columns-page.md)  
  
-   [CDC Source Editor &#40;Error Output Page&#41;](../cdc-source-editor-error-output-page.md)  
  
 The **Advanced Editor** dialog box contains the properties that can be set programmatically.  
  
 To open the **Advanced Editor** dialog box:  
  
-   In the **Data Flow** screen of your [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] project, right click the CDC source and select **Show Advanced Editor**.  
  
 For more information about the properties that you can set in the **Advanced Editor** dialog box, see [CDC Source Custom Properties](cdc-source-custom-properties.md).  
  
## In This Section  
  
-   [CDC Source Editor &#40;Connection Manager Page&#41;](../cdc-source-editor-connection-manager-page.md)  
  
-   [CDC Source Editor &#40;Columns Page&#41;](../cdc-source-editor-columns-page.md)  
  
-   [CDC Source Editor &#40;Error Output Page&#41;](../cdc-source-editor-error-output-page.md)  
  
-   [CDC Source Custom Properties](cdc-source-custom-properties.md)  
  
-   [Extract Change Data Using the CDC Source](cdc-source.md)  
  
## Related Content  
  
-   Blog entry, [Processing Modes for the CDC Source](https://go.microsoft.com/fwlink/?LinkId=242541), on mattmasson.com.  
  
  
