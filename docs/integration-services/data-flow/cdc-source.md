---
description: "CDC Source"
title: "CDC Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.ssis.designer.cdcsource.f1"
  - "sql13.ssis.designer.cdcsource.connection.f1"
  - "sql13.ssis.designer.cdcsource.columns.f1"
  - "sql13.ssis.designer.cdcsource.errorhandling.f1"
ms.assetid: 99775608-e177-44ed-bb44-aaccb0f4f327
author: chugugrace
ms.author: chugu
---
# CDC Source

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The CDC source reads a range of change data from [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] change tables and delivers the changes downstream to other SSIS components.  
  
 The range of change data read by the CDC source is called the CDC Processing Range and is determine by the CDC Control task that is executed before the current data flow starts. The CDC Processing Range is derived from the value of a package variable that maintains the state of the CDC processing for a group of tables.  
  
 The CDC source extracts data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using a database table, a view, or an SQL statement.  
  
 The CDC source uses the following configurations:  
  
-   A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ADO.NET connection manager to access the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CDC database. For more information about configuring the CDC source connection, see [CDC Source Editor &#40;Connection Manager Page&#41;]().  
  
-   A table enabled for CDC.  
  
-   The name of the capture instance of the selected table (if more-than-one exists).  
  
-   The change processing mode.  
  
-   The name of the CDC state package variable based on which the CDC Processing range is determined. The CDC source does not modify that variable.  
  
 The data returned by the CDC Source is the same as that returned by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CDC functions **cdc.fn_cdc_get_all_changes_\<capture-instance-name>** or **cdc.fn_cdc_get_net_changes_\<capture-instance-name>** (when available). The only optional addition is the column, **__$initial_processing** that indicates whether the current processing range can overlap with an initial load of the table. For more information about initial processing, see [CDC Control Task](../../integration-services/control-flow/cdc-control-task.md).  
  
 The CDC source has one regular output and one error output.  
  
## Error Handling  
 The CDC source has an error output. The component error output includes the following output columns:  
  
-   **Error Code**: The value is always -1.  
  
-   **Error Column**: The source column causing the error (for conversion errors).  
  
-   **Error Row Columns**: The record data that causes the error.  
  
 Depending on the error behavior setting, the CDC source supports returning errors (data conversion, truncation) that occur during the extraction process in the error output.  
  
## Data Type Support  
 The CDC source component for Microsoft supports all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types, which are mapped to the correct SSIS data types.  
  
## Troubleshooting the CDC Source  
 The following contains information on troubleshooting the CDC source.  
  
### Use this script to isolate problems and reproduce them in SQL Server Management Studio  
 The CDC source operation is governed by the operation of the CDC Control task executed before invoking the CDC source. The CDC Control task prepares the value of the CDC state package variable to contain the start and end LSNs. It performs function equivalent to the following script:  
  
```sql
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
  
-   [CDC Source Editor &#40;Connection Manager Page&#41;]()  
  
-   [CDC Source Editor &#40;Columns Page&#41;]()  
  
-   [CDC Source Editor &#40;Error Output Page&#41;]()  
  
 The **Advanced Editor** dialog box contains the properties that can be set programmatically.  
  
 To open the **Advanced Editor** dialog box:  
  
-   In the **Data Flow** screen of your [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] project, right click the CDC source and select **Show Advanced Editor**.  
  
 For more information about the properties that you can set in the **Advanced Editor** dialog box, see [CDC Source Custom Properties](../../integration-services/data-flow/cdc-source-custom-properties.md).  
  
## In This Section  
  
-   [CDC Source Custom Properties](../../integration-services/data-flow/cdc-source-custom-properties.md)  
  
-   [Extract Change Data Using the CDC Source](../../integration-services/data-flow/extract-change-data-using-the-cdc-source.md)  
  
## CDC Source Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **CDC Source Editor** dialog box to select the ADO.NET connection manager for the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] database that the CDC source reads change rows from (the CDC database). Once the CDC database is selected you need to select a captured table in the database.  
  
 For more information about the CDC source, see [CDC Source](../../integration-services/data-flow/cdc-source.md).  
  
### Task List  
 **To open the CDC Source Editor Connection Manager Page**  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] package that has the CDC source.  
  
2.  On the **Data Flow** tab, double-click the CDC source.  
  
3.  In the **CDC Source Editor**, click **Connection Manager**.  
  
### Options  
 **ADO.NET connection manager**  
 Select an existing connection manager from the list, or click **New** to create a new connection. The connection must be to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that is enabled for CDC and where the selected change table is located.  
  
 **New**  
 Click **New**. The **Configure ADO.NET Connection Manager Editor** dialog box opens where you can create a new connection manager  
  
 **CDC Table**  
 Select the CDC source table that contains the captured changes that you want read and feed to downstream SSIS components for processing.  
  
 **Capture instance**  
 Select or type in the name of the CDC capture instance with the CDC table to be read.  
  
 A captured source table can have one or two captured instances to handle seamless transitioning of table definition through schema changes. If more than one capture instance is defined for the source table being captured, select the capture instance you want to use here. The default capture instance name for a table [schema].[table] is \<schema>_\<table> but that actual capture instance names in use may be different. The actual table that is read from is the CDC table **cdc .\<capture-instance>_CT**.  
  
 **CDC Processing Mode**  
 Select the processing mode that best handles your processing needs. The possible options are:  
  
-   **All**: Returns the changes in the current CDC range without the **Before Update** values.  
  
-   **All with old values**: Returns the changes in the current CDC processing range including the old values (**Before Update**). For each Update operation, there will be two rows, one with the before-update values and one with the after-update value.  
  
-   **Net**: Returns only one change row per source row modified in the current CDC processing range. If a source row was updated multiple times, the combined change is produced (for example, insert+update is produced as a single update and update+delete is produced as a single delete). When working in Net change processing mode, it is possible to split the changes to Delete, Insert and Update outputs and handle them in parallel because the single source row appears in more than one output.  
  
-   **Net with update mask**: This mode is similar to the regular Net mode but it also adds boolean columns with the name pattern **__$\<column-name>\__Changed** that indicate changed columns in the current change row.  
  
-   **Net with merge**: This mode is similar to the regular Net mode but with Insert and Update operations merged into a single Merge operation (UPSERT).  
  
> [!NOTE]  
>  For all Net change options, the source table must have a primary key or unique index. For tables without a primary key or unique indes, you must yse the **All** option.  
  
 **Variable containing the CDC state**  
 Select the SSIS string package variable that maintains the CDC state for the current CDC context. For more information about the CDC state variable, see [Define a State Variable](../../integration-services/data-flow/define-a-state-variable.md).  
  
 **Include reprocessing indicator column**  
 Select this check box to create a special output column called **__$reprocessing**.  
  
 This column has a value of **true** when the CDC processing range overlaps with the initial processing range (the range of LSNs corresponding to the period of initial load) or when a CDC processing range is reprocessed following an error in a previous run. This indicator column lets the SSIS developer handle errors differently when reprocessing changes (for example, actions such as a delete of a non-existing row and an insert that failed on a duplicate key can be ignored).  
  
 For more information, see [CDC Source Custom Properties](../../integration-services/data-flow/cdc-source-custom-properties.md).  
  
## CDC Source Editor (Columns Page)
  Use the **Columns** page of the **CDC Source Editor** dialog box to map an output column to each external (source) column.  
  
### Task List  
 **To open the CDC Source Editor Columns Page**  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] package that has the CDC source.  
  
2.  On the **Data Flow** tab, double-click the CDC source.  
  
3.  In the **CDC Source Editor**, click **Columns**.  
  
### Options  
 **Available External Columns**  
 A list of available external columns in the data source. You cannot use this table to add or delete columns. Select the columns to use in the source. The selected columns are added to the **External Column** list in the order they are selected.  
  
 **External Column**  
 A view of the external (source) columns in the order that you see them when configuring components that consume data from the CDC source. To change this order, first clear the selected columns in the **Available External Columns** list, and then select external columns from the list in a different order. The selected columns are added to the **External Column** list in the order you select them.  
  
 **Output Column**  
 Enter a unique name for each output column. The default is the name of the selected external (source) column, however you can choose any unique, descriptive name. The name entered is displayed in the SSIS Designer.  
  
## CDC Source Editor (Error Output Page)
  Use the **Error Output** page of the **CDC Source Editor** dialog box to select error handling options.  
  
### Task List  
 **To open the CDC Source Editor Error Output Page**  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] package that has the CDC source.  
  
2.  On the **Data Flow** tab, double-click the CDC source.  
  
3.  In the **CDC Source Editor**, click **Error Output**.  
  
### Options  
 **Input/Output**  
 View the name of the data source.  
  
 **Column**  
 View the external (source) columns that you selected on the **Connection Manager** page of the **CDC Source Editor** dialog box.  
  
 **Error**  
 Select how the CDC source should handle errors in a flow: ignore the failure, redirect the row, or fail the component.  
  
 **Truncation**  
 Select how the CDC source should handle truncation in a flow: ignore the failure, redirect the row, or fail the component.  
  
 **Description**  
 Not used.  
  
 **Set this value to selected cells**  
 Select how the CDC source handles all selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Apply**  
 Apply the error handling options to the selected cells.  
  
### Error Handling Options  
 You use the following options to configure how the CDC source handles errors and truncations.  
  
 **Fail Component**  
 The Data Flow task fails when an error or a truncation occurs. This is the default behavior.  
  
 **Ignore Failure**  
 The error or the truncation is ignored and the data row is directed to the CDC source output.  
  
 **Redirect Flow**  
 The error or the truncation data row is directed to the error output of the CDC source. In this case the CDC source error handling is used. For more information, see [CDC Source](../../integration-services/data-flow/cdc-source.md).  
  
## Related Content  
  
-   Blog entry, [Processing Modes for the CDC Source](https://www.mattmasson.com/2012/01/processing-modes-for-the-cdc-source/), on mattmasson.com.  
  
