---
title: "ODBC Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.ssis.designer.odbcdest.f1"
  - "sql13.ssis.designer.odbcdest.connection.f1"
  - "sql13.ssis.designer.odbcdest.columns.f1"
  - "sql13.ssis.designer.odbcdest.errorhandling.f1"
ms.assetid: bffa63e0-c737-4b54-b4ea-495a400ffcf8
author: janinezhang
ms.author: janinez
manager: craigg
---
# ODBC Destination
  The ODBC destination bulk loads data into ODBC-supported database tables. The ODBC destination uses an ODBC connection manager to connect to the data source.  
  
 An ODBC destination includes mappings between input columns and columns in the destination data source. You do not have to map input columns to all destination columns, but depending on the properties of the destination columns, errors may occur if no input columns are mapped to the destination columns. For example, if a destination column does not allow null values, an input column must be mapped to that column. In addition, columns of different types can be mapped, however if the input data is not compatible for the destination column type, an error occurs at runtime. Depending on the error behavior setting, the error will be ignored, cause a failure, or the row is sent to the error output.  
  
 The ODBC destination has one regular output and one error output.  
  
##  <a name="BKMK_odbcdestination_loadoptions"></a> Load Options  
 The ODBC destination can use one of two access load modules. You set the mode in the [ODBC Source Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/odbc-source-editor-connection-manager-page.md). The two modes are:  
  
-   **Batch**: In this mode the ODBC destination attempts to use the most efficient insertion method based on the perceived ODBC provider capabilities. For most modern ODBC providers, this would mean preparing an INSERT statement with parameters and then using a row-wise array parameter binding (where the array size is controlled by the **BatchSize** property). If you select **Batch** and the provider does not support this method, the ODBC destination automatically switches to the **Row-by-row** mode.  
  
-   **Row-by-row**: In this mode, the ODBC destination prepares an INSERT statement with parameters and uses **SQL Execute** to insert rows one at a time.  
  
## Error Handling  
 The ODBC destination has an error output. The component error output includes the following output columns:  
  
-   **Error Code**: The number that corresponds to the current error. See the documentation for your source database for a list of errors. For a list of SSIS error codes, see the SSIS Error Code and Message Reference.  
  
-   **Error Column**: The source column causing the error (for conversion errors).  
  
-   The standard output data columns.  
  
 Depending on the error behavior setting, the ODBC destination supports returning errors (data conversion, truncation) that occur during the extraction process in the error output. For more information, see [ODBC Source Editor &#40;Error Output Page&#41;](../../integration-services/data-flow/odbc-source-editor-error-output-page.md).  
  
## Parallelism  
 There is no limitation on the number of ODBC destination components that can run in parallel against the same table or different tables, on the same machine or on different machines (other than normal global session limits).  
  
 However, limitations of the ODBC provider being used may restrict the number of concurrent connections through the provider. These limitations limit the number of supported parallel instances possible for the ODBC destination. The SSIS developer must be aware of the limitations of any ODBC provider being used and take them into consideration when building SSIS packages.  
  
 You must also be aware that concurrently loading into the same table may reduce performance because of standard record locking. This depends on the data being loaded and on the table organization.  
  
## Troubleshooting the ODBC Destination  
 You can log the calls that the ODBC source makes to external data providers. You can use this logging capability to troubleshoot the saving of data to external data sources that the ODBC destination performs. To log the calls that the ODBC destination makes to external data providers, enable the ODBC driver manager trace. For more information, see the Microsoft documentation on *How To Generate an ODBC Trace with ODBC the Data Source Administrator*.  
  
## Configuring the ODBC Destination  
 You can configure the ODBC destination programatically or through the SSIS Designer  
  
 For more information, see one of the following topics:  
  
-   [ODBC Destination Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/odbc-destination-editor-connection-manager-page.md)  
  
-   [ODBC Destination Editor &#40;Mappings Page&#41;](../../integration-services/data-flow/odbc-destination-editor-mappings-page.md)  
  
-   [ODBC Destination Editor &#40;Error Output Page&#41;](../../integration-services/data-flow/odbc-destination-editor-error-output-page.md)  
  
 The **Advanced Editor** dialog box contains the properties that can be set programmatically.  
  
 To open the **Advanced Editor** dialog box:  
  
-   In the **Data Flow** screen of your [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] project, right click the ODBC destination and select **Show Advanced Editor**.  
  
 For more information about the properties that you can set in the Advanced Editor dialog box, see [ODBC Destination Custom Properties](../../integration-services/data-flow/odbc-destination-custom-properties.md).  
  
## In This Section  
  
-   [Load Data by Using the ODBC Destination](../../integration-services/data-flow/load-data-by-using-the-odbc-destination.md)  
  
-   [ODBC Destination Custom Properties](../../integration-services/data-flow/odbc-destination-custom-properties.md)  
  
## ODBC Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **ODBC Destination Editor** dialog box to select the ODBC connection manager for the destination. This page also lets you select a table or view from the database  
  
 **To open the ODBC Destination Editor Connection Manager Page**  
  
### Task List  
  
-   In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] package that has the ODBC destination.  
  
-   On the **Data Flow** tab, double-click the ODBC destination.  
  
-   In the **ODBC Destination Editor**, click **Connection Manager**.  
  
### Options  
  
#### Connection manager  
 Select an existing ODBC connection manager from the list, or click New to create a new connection. The connection can be to any ODBC-supported database.  
  
#### New  
 Click **New**. The **Configure ODBC Connection Manager Editor** dialog box opens where you can create a new connection manager.  
  
#### Data Access Mode  
 Select the method for loading data to the destination. The options are shown in the following table:  
  
|Option|Description|  
|------------|-----------------|  
|Table Name - Batch|Select this option to configure the ODBC destination to work in batch mode. When you select this option the following options are available:|  
||**Name of the table or the view**: Select an available table or view from the list.<br /><br /> This list contains the first 1000 tables only. If your database contains more than 1000 tables, you can type the beginning of a table name or use the (\*) wild card to enter any part of the name to display the table or tables you want to use.<br /><br /> **Batch size**: Type the size of the batch for bulk loading. This is the number of rows loaded as a batch|  
|Table Name - Row by Row|Select this option to configure the ODBC destination to insert each of the rows into the destination table one at a time. When you select this option the following option is available:|  
||**Name of the table or the view**: Select an available table or view from the database from the list.<br /><br /> This list contains the first 1000 tables only. If your database contains more than 1000 tables, you can type the beginning of a table name or use the (*) wild card to enter any part of the name to display the table or tables you want to use.|  
  
#### Preview  
 Click **Preview** to view up to 200 rows of data for the table that you selected.  
  
## ODBC Destination Editor (Mappings Page)
  Use the **Mappings** page of the **ODBC Destination Editor** dialog box to map input columns to destination columns.  
  
### Options  
  
#### Available Input Columns  
 The list of available input columns. Drag-and-drop an input column to an available destination column to map the columns.  
  
#### Available Destination Columns  
 The list of available destination columns. Drag-and-drop a destination column to an available input column to map the columns.  
  
#### Input Column  
 View the input columns that you selected. You can remove mappings by selecting **\<ignore>** to exclude columns from the output.  
  
#### Destination Column  
 View all available destination columns, both mapped and unmapped.  
  
## ODBC Destination Editor (Error Output Page)
  Use the **Error Output** page of the **ODBC Destination Editor** dialog box to select error handling options.  
  
 **To open the ODBC Destination Editor Error Output Page**  
  
### Task List  
  
-   In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] package that has the ODBC destination.  
  
-   On the **Data Flow** tab, double-click the ODBC destination.  
  
-   In the **ODBC Destination Editor**, click **Error Output**.  
  
### Options  
  
#### Input/Output  
 View the name of the data source.  
  
#### Column  
 Not used.  
  
#### Error  
 Select how the ODBC destination should handle errors in a flow: ignore the failure, redirect the row, or fail the component.  
  
#### Truncation  
 Select how the ODBC destination should handle truncation in a flow: ignore the failure, redirect the row, or fail the component.  
  
#### Description  
 View a description of the error.  
  
#### Set this value to selected cells  
 Select how the ODBC destination handles all selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
#### Apply  
 Apply the error handling options to the selected cells.  
  
### Error Handling Options  
 You use the following options to configure how the ODBC destination handles errors and truncations.  
  
#### Fail Component  
 The Data Flow task fails when an error or a truncation occurs. This is the default behavior.  
  
#### Ignore Failure  
 The error or the truncation is ignored.  
  
#### Redirect Flow  
 The row that is causing the error or the truncation is directed to the error output of the ODBC destination. For more information, see ODBC Destination.  
  
