---
title: "Teradata source | Microsoft Docs"
ms.custom: ""
ms.date: "11/22/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
author: chugugrace
ms.author: chugu
---
# Teradata source

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]

The Teradata source extracts data from Teradata databases by using:

- A table or view.

- The results of an SQL statement.

The source uses a Teradata connection manager to connect to Teradata source. For more information, see [Teradata connection manager](teradata-connection-manager.md).

## Troubleshooting the Teradata source

You can log the calls that the Teradata source makes to the Teradata Parallel Transporter API (TPT API). You can enable package logging and select the **Diagnostic** event at the package level to log the calls.

You can log the ODBC calls that the Teradada source makes to the Teradata ODBC driver by enabling the ODBC driver manager trace. For more information, see the Microsoft documentation on *How To Generate an ODBC Trace with ODBC the Data Source Administrator*.

## Parallelism

Teradata source supports parallelism that export jobs can access same table or different tables at same time. Database variable called **MaxLoadTasks** sets the limit of the number of Export jobs that can run at the same time. You can define this maximum number with this **MaxLoadTasks** variable.

## Teradata source custom properties

The custom properties of the Teradata source are as below. All properties are read/write.

|Property name|Data Type|Description|
|:-|:-|:-|
|AccessMode|Integer (Enumeration)|The mode used to access the database. The possible values are **Table Name** and **SQL Command**. The default is **Table Name**.|
|BlockSize|Integer|The block size in bytes used when returning data to the client. The default value is 1048576(1MB). The minimum value is 256 bytes. The maximum value is 16775168 bytes.<br> This property is in **Advanced Editor**.|
|BufferMaxSize|Integer|The total maximum size for the data buffer returned by the GetBuffer function. The total maximum size of the data buffer must be large enough to hold at least one row of data including the row header, the actual row of data, and the buffer trailer. The default total maximum size of the data buffer is 16775552. <br>For more information, see Teradata documentation (Export Data from a Teradata Database using GetBuffer).|
|BufferMode|Boolean|Default is **True**. Must be **True** if PutBuffer feature is used. This property is in **Advanced Editor**.|
|DataEncryption|Boolean|Default is **False**. Full security encryption is used if **True**.|
|DefaultCodePage|Integer|The code page to use when data source does not have code page information. <br>**Note**: This property is in **Advanced Editor**.|
|DetailedTracingLevel|Integer (Enumeration)|Select one of the following options for advanced tracing: <br> **Off**: No advanced logging. <br> **General**: Driver-specific activities general tracing is logged. <br> **CLI**: CLIv2-related activities tracing is logged. <br> **Notify Method**: Notify feature-related activities tracing is logged. <br> **Common Library**: opcommon library activities tracing is logged. <br> **All**: All of the above activities tracing is logged. <br> The advanced tracing log file is defined in the **DetailedTracingFile** property. <br> **DetailedTracingFile** property must be set if option is not Off. <br> This property is in **Advanced Editor**.|
|DetailedTracingFile|String|The path of log file that is generated automatically when **DetailedTracingLevel** is not **Off**. This property is in **Advanced Editor**.|
|DiscardLargeRow|Boolean|Default is **False**. Discard large rows (greater than 64K) if **True**|
|ExtendedStringColumnsAllocation|Boolean|**Maximal Transfer Character Allocation Factor** is use if **True**. <br> This value should be set to **True** if the Teradata database **Export Width Table ID** property is set to **Maximal Defaults**. <br> Default is **False**.|
|JobMaxRowSize|Integer|Maximum row size can be supported. This value is needed if **DiscardLargeRow** is **True**.<br>Valid values: <br>64 - (default): 2-byte row lengths can be supported. <br>1024: 4-byte row lengths can be supported|
|MaxSessions|Integer|The maximum number of sessions that are logged on. This value must be greater than one. The default value is one session for each available AMP.|
|MinSessions|Integer|The minimum number of sessions that are logged on. This value must be greater than one. The default value is one session for each available AMP.|
|QueryBandSessInfo|Varchar|A user defined, session-based query band expression in a connection-string format. Charge-back monitoring and governance with this property. This property is in **Advanced Editor**.|
|SpoolMode|Varchar|Valid values are: <br>**Spool**: Default. Use Spool. <br> **NoSpool**: -Do not use Spool. This value is valid only if the DBS supports **NoSpool**. <br>  **NoSpoolOnly**:  Do not use Spool in any case. Job will be terminated with error if the DBS does not support **NoSpool**.|
|SqlCommand|String|The SQL command to be executed when AccessMode is set to SQL Command.|
|TableName|String|The name of the table with the data to be used when AccessMode is set to Table Name.|
|TenacityHours|Integer|The number of hours the TPT driver attempts to log on when the maximum number of load/export operations are already running. The default is 4 hours. This property is in **Advanced Editor**|
|TenacitySleep|Integer|Minutes the TPT driver pauses before attempting to log on when limit is reached. Limit is defined by the **MaxSessions** and **TenacityHours** properties. Default is 6 minutes. This property is in **Advanced Editor**|
|UnicodePassThrough|Boolean|Off (default): Disable the Unicode Pass Through. <br>On: Enable the Unicode Pass Through|

## Configuring Teradata source

You can configure the Teradata source programmatically or through the SSIS Designer.

The Teradata Source Editor is shown in blow picture. It contains Connection Manager Page, Columns Page, and Error Output Page.

For more information, see one of the following sections:

- [Teradata Source Editor (Connection Manager page)](#teradata-source-editor-connection-manager-page)
- [Teradata Source Editor (Columns Page)](#teradata-source-editor-columns-page)
- [Teradata Source Editor (Error Output Page)](#teradata-source-editor-error-output-page)

![source editor](media/teradata-source.png)

The **Advanced Editor** dialog box contains the properties that can be set programmatically.
To open the **Advanced Editor** dialog box:
- In the Data Flow screen of your Integration Services project, right-click the Oracle source and select **Show Advanced Editor**.

For more information about the properties that you can set in the **Advanced Editor** dialog box, see [Teradata source custom properties](#teradata-source-custom-properties).

## Teradata Source Editor (Connection Manager page)

Use the Connection Manager page of the Teradata Source Editor dialog box to select the Teradata connection manager for the source. This page also lets you select a table or view from the database.

**To open the Teradata Source Editor Connection Manager Page**

- In SQL Server Data Tools, open the SQL Server Integration Services (SSIS) package that has the Teradata source.

- On the Data Flow tab, double-click the Teradata source.

### Options

**Connection manager**

Select an existing connection manager from the list or click **New** to create a new Teradata connection manager.

**New**

Click **New**. The **Teradata Connection Manager Editor** dialog box opens where you can create a new connection manager.

**Data Access Mode**

Select the method for selecting data from the source. The options are shown in the following table:

|Option|Description|
|:-|:-|
|Table name- TPT Export|Retrieve data from a table or view in the Teradata data source. When this option is selected, select an available table or view from the list for **Name of the table or the view**.|
|SQL command- TPT Export|Retrieve data from the Teradata data source by using an SQL query. When this option is selected, enter a query in one of the following ways: <br>Enter the text of the SQL query in the **SQL command text** field. <br>Click **Browse** to load the SQL query from a text file. <br>Click **Parse query** to verify the syntax of the query text.|

**Preview**

Click **Preview** to view up to the first 200 rows of the data extracted from the table or view you selected.

## Teradata Source Editor (Columns Page)

On **Columns** page, **Teradata Source Editor** dialog box is used to map an output column to each external (source) column.

**To open the Teradata Source Editor Columns Page**

- In SQL Server Data Tools, open the SQL Server Integration Services (SSIS) package that has the Teradata source.

- On the Data Flow tab, double-click the Teradata source.

- In the Teradata Source Editor, click Columns.

### Options

**Available External Columns**

A list of available external columns that can be selected to add to **External Column** list in the order they are selected. This table cannot be used to add or delete columns.

Select the **Select All** check box to select all the columns.

**External Columns**

The external (source) columns that you selected are listed in order. To change the order, first clear **Available External Column** list, and then select the column(s) with a different order.

**Output Column**

The name of the selected external (source) column is the default output name. While you can input any unique name.
>**Note**
>
>If there are columns with unsupported data types, there will be a warning shown the data types are not supported, and related columns will be removed from mapping columns.

## Teradata Source Editor (Error Output Page)

Use the **Error Output** page of the **Teradata Source Editor** dialog box to select error handling options.

**To open the Teradata Source Editor Error Output Page**

- In SQL Server Data Tools, open the SQL Server Integration Services (SSIS) package that has the Teradata source.

- On the Data Flow tab, double-click the Teradata source.

- In the Teradata Source Editor, click Error Output.

### Options

**Error behavior**

Select how the Teradata source should handle errors in a flow: ignore the failure, redirect the row, or fail the component.
**Related Topics**: [Error Handling in Data](error-handling-in-data.md)

**Truncation**

Select how the Teradata source should handle truncation in a flow: ignore the failure, redirect the row, or fail the component.

## Next steps

- Configure [Teradata destination](teradata-destination.md).
- If you have questions, visit [Tech Community](https://aka.ms/AA6iwdw).
