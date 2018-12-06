---
title: "ODBC Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.ssis.designer.odbcsource.f1"
  - "sql13.ssis.designer.odbcsource.connection.f1"
  - "sql13.ssis.designer.odbcsource.columns.f1"
  - "sql13.ssis.designer.odbcsource.errorhandling.f1"
ms.assetid: abcf34eb-9140-4100-82e6-b85bccd22abe
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# ODBC Source
  The ODBC source extracts data from ODBC-supported database by using a database table, a view, or an SQL statement.  
  
 The ODBC source has the following data access modes for extracting data:  
  
-   A table or view.  
  
-   The results of an SQL statement.  
  
 The source uses an ODBC connection manager, which specifies the provider to use.  
  
 An ODBC source includes the source data output columns. When output columns are mapped in the ODBC destination to the destination columns, errors may occur if no output columns are mapped to the destination columns. Columns of different types can be mapped, however if the output data is not compatible for the destination then an error occurs at runtime. Depending on the error behavior, setting the error will be ignored, cause a failure, or the row is sent to the error output.  
  
 The ODBC source has one regular output and one error output.  
  
## Error Handling  
 The ODBC source has an error output. The component error output includes the following output columns:  
  
-   **Error Code**: The number that corresponds to the current error. See the documentation for the ODBC-supported database you are using for a list of errors. For a list of SSIS error codes, see the SSIS Error Code and Message Reference.  
  
-   **Error Column**: The source column causing the error (for conversion errors).  
  
-   The standard output data columns.  
  
 Depending on the error behavior setting, the ODBC source supports returning errors (data conversion, truncation) that occur during the extraction process in the error output. For more information, see [ODBC Destination Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/odbc-destination-editor-connection-manager-page.md).  
  
## Data Type Support  
 For information about the data types supported by the ODBC source, see Connector for Open Database Connectivity (ODBC).  
  
## Extract Options  
 The ODBC source operates in either **Batch** or **Row-by-Row** mode. The mode used is determined by the **FetchMethod** property. The following list describes the modes.  
  
-   **Batch**: The component attempts to use the most efficient fetch method based on the perceived ODBC provider capabilities. For most modern ODBC providers, this is SQLFetchScroll with array binding (where the array size is determined by the **BatchSize** property). If you select **Batch** and the provider does not support this method, the ODBC destination automatically switches to the **Row-by-row** mode.  
  
-   **Row-by Row**: The component uses SQLFetch to retrieve the rows one at a time.  
  
 For more information about the **FetchMethod** property, see [ODBC Source Custom Properties](../../integration-services/data-flow/odbc-source-custom-properties.md).  
  
## Parallelism  
 There is no limitation on the number of ODBC source components that can run in parallel against the same table or different tables, on the same machine or on different machines (other than normal global session limits).  
  
 However, limitations of the ODBC provider being used may restrict the number of concurrent connections through the provider. These limitations limit the number of supported parallel instances possible for the ODBC source. The SSIS developer must be aware of the limitations of any ODBC provider being used and take them into consideration when building SSIS packages.  
  
## Troubleshooting the ODBC Source  
 You can log the calls that the ODBC source makes to external data providers. You can use this logging capability to troubleshoot the loading of data from external data sources that the ODBC source performs. To log the calls that the ODBC source makes to external data providers, enable the ODBC driver manager trace. For more information, see the Microsoft documentation on *How To Generate an ODBC Trace with ODBC the Data Source Administrator.*  
  
## Configuring the ODBC Source  
 You can configure the ODBC source programmatically or through the SSIS Designer.  
  
 The **Advanced Editor** dialog box contains the properties that can be set programmatically.  
  
 To open the **Advanced Editor** dialog box:  
  
-   In the **Data Flow** screen of your [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] project, right click the ODBC source and select **Show Advanced Editor**.  
  
 For more information about the properties that you can set in the Advanced Editor dialog box, see [ODBC Source Custom Properties](../../integration-services/data-flow/odbc-source-custom-properties.md).  
  
## In This Section  
  
-   [Extract Data by Using the ODBC Source](../../integration-services/data-flow/extract-data-by-using-the-odbc-source.md)  
  
-   [ODBC Source Custom Properties](../../integration-services/data-flow/odbc-source-custom-properties.md)  
  
## ODBC Source Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **ODBC Source Editor** dialog box to select the ODBC connection manager for the source. This page also lets you select a table or view from the database.  
  
### Task List  
 **To open the ODBC Source Editor Connection Manager Page**  
  
-   In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] package that has the ODBC source.  
  
-   On the **Data Flow** tab, double-click the ODBC source.  
  
### Options  
  
#### Connection manager  
 Select an existing ODBC connection manager from the list, or click **New** to create a new connection. The connection can be to any ODBC-supported database.  
  
#### New  
 Click **New**. The **Configure ODBC Connection Manager Editor** dialog box opens where you can create a new ODBC connection manager.  
  
#### Data Access Mode  
 Select the method for selecting data from the source. The options are shown in the following table:  
  
|Option|Description|  
|------------|-----------------|  
|Table Name|Retrieve data from a table or view in the ODBC data source. When you select this option, select a value from the list for the following:|  
||**Name of the table or the view**: Select an available table or view from the list or type a regular expression to identify the table.|  
||This list contains the first 1000 tables only. If your database contains more than 1000 tables, you can type the beginning of a table name or use the (*) wild card to enter any part of the name to display the table or tables you want to use.|  
|SQL command|Retrieve data from the ODBC data source by using an SQL query. You should write the query in the syntax of the source database you are working with. When you select this option, enter a query in one of the following ways:|  
||Enter the text of the SQL query in the **SQL command text** field.|  
||Click **Browse** to load the SQL query from a text file.|  
||Click **Parse query** to verify the syntax of the query text.|  
  
#### Preview  
 Click **Preview** to view up to the first 200 rows of the data extracted from the table or view you selected.  
  
## ODBC Source Editor (Columns Page)
  Use the **Columns** page of the **ODBC Source Editor** dialog box to map an output column to each external (source) column.  
  
### Task List  
 **To open the ODBC Source Editor Columns Page**  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] package that has the ODBC source.  
  
2.  On the **Data Flow** tab, double-click the ODBC source.  
  
3.  In the **ODBC Source Editor**, click **Columns**.  
  
### Options  
  
#### Available External Columns  
 A list of available external columns in the data source. You cannot use this table to add or delete columns. Select the columns to use from the source. The selected columns are added to the **External Column** list in the order they are selected.  
  
 Select the **Select All** check box to select all of the columns.  
  
#### External Column  
 A view of the external (source) columns in the order that you see them when configuring components that consume data from the ODBC source.  
  
#### Output Column  
 Enter a unique name for each output column. The default is the name of the selected external (source) column; however, you can choose any unique, descriptive name. The name entered is displayed in the SSIS Designer.  
  
## ODBC Source Editor (Error Output Page)
  Use the **Error Output** page of the **ODBC Source Editor** dialog box to select error handling options.  
  
### Task List  
 **To open the ODBC Source Editor Error Output Page**  
  
-   In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] package that has the ODBC source.  
  
-   On the **Data Flow** tab, double-click the ODBC source.  
  
-   In the **ODBC Source Editor**, click **Error Output**.  
  
### Options  
  
#### Input/Output  
 View the name of the data source.  
  
#### Column  
 Not used.  
  
#### Error  
 Select how the ODBC source should handle errors in a flow: ignore the failure, redirect the row, or fail the component.  
  
#### Truncation  
 Select how the ODBC source should handle truncation in a flow: ignore the failure, redirect the row, or fail the component.  
  
#### Description  
 Not used.  
  
#### Set this value to selected cells  
 Select how the ODBC source handles all selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
#### Apply  
 Apply the error handling options to the selected cells.  
  
### Error Handling Options  
 You use the following options to configure how the ODBC source handles errors and truncations.  
  
#### Fail Component  
 The Data Flow task fails when an error or a truncation occurs. This is the default behavior.  
  
#### Ignore Failure  
 The error or the truncation is ignored.  
  
#### Redirect Flow  
 The row that is causing the error or the truncation is directed to the error output of the ODBC source.  
  
  
