---
title: "ODBC Destination | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.designer.odbcdest.f1"
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
 The ODBC destination can use one of two access load modules. You set the mode in the [ODBC Source Editor &#40;Connection Manager Page&#41;](../odbc-source-editor-connection-manager-page.md). The two modes are:  
  
-   **Batch**: In this mode the ODBC destination attempts to use the most efficient insertion method based on the perceived ODBC provider capabilities. For most modern ODBC providers, this would mean preparing an INSERT statement with parameters and then using a row-wise array parameter binding (where the array size is controlled by the **BatchSize** property). If you select **Batch** and the provider does not support this method, the ODBC destination automatically switches to the **Row-by-row** mode.  
  
-   **Row-by-row**: In this mode, the ODBC destination prepares an INSERT statement with parameters and uses **SQL Execute** to insert rows one at a time.  
  
## Error Handling  
 The ODBC destination has an error output. The component error output includes the following output columns:  
  
-   **Error Code**: The number that corresponds to the current error. See the documentation for your source database for a list of errors. For a list of SSIS error codes, see the SSIS Error Code and Message Reference.  
  
-   **Error Column**: The source column causing the error (for conversion errors).  
  
-   The standard output data columns.  
  
 Depending on the error behavior setting, the ODBC destination supports returning errors (data conversion, truncation) that occur during the extraction process in the error output. For more information, see [ODBC Source Editor &#40;Error Output Page&#41;](../odbc-source-editor-error-output-page.md).  
  
## Parallelism  
 There is no limitation on the number of ODBC destination components that can run in parallel against the same table or different tables, on the same machine or on different machines (other than normal global session limits).  
  
 However, limitations of the ODBC provider being used may restrict the number of concurrent connections through the provider. These limitations limit the number of supported parallel instances possible for the ODBC destination. The SSIS developer must be aware of the limitations of any ODBC provider being used and take them into consideration when building SSIS packages.  
  
 You must also be aware that concurrently loading into the same table may reduce performance because of standard record locking. This depends on the data being loaded and on the table organization.  
  
## Troubleshooting the ODBC Destination  
 You can log the calls that the ODBC source makes to external data providers. You can use this logging capability to troubleshoot the saving of data to external data sources that the ODBC destination performs. To log the calls that the ODBC destination makes to external data providers, enable the ODBC driver manager trace. For more information, see the Microsoft documentation on *How To Generate an ODBC Trace with ODBC the Data Source Administrator*.  
  
## Configuring the ODBC Destination  
 You can configure the ODBC destination programatically or through the SSIS Designer  
  
 For more information, see one of the following topics:  
  
-   [ODBC Destination Editor &#40;Connection Manager Page&#41;](../odbc-destination-editor-connection-manager-page.md)  
  
-   [ODBC Destination Editor &#40;Mappings Page&#41;](../odbc-destination-editor-mappings-page.md)  
  
-   [ODBC Destination Editor &#40;Error Output Page&#41;](../odbc-destination-editor-error-output-page.md)  
  
 The **Advanced Editor** dialog box contains the properties that can be set programmatically.  
  
 To open the **Advanced Editor** dialog box:  
  
-   In the **Data Flow** screen of your [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] project, right click the ODBC destination and select **Show Advanced Editor**.  
  
 For more information about the properties that you can set in the Advanced Editor dialog box, see [ODBC Destination Custom Properties](odbc-destination-custom-properties.md).  
  
## In This Section  
  
-   [ODBC Destination Editor &#40;Error Output Page&#41;](../odbc-destination-editor-error-output-page.md)  
  
-   [ODBC Destination Editor &#40;Mappings Page&#41;](../odbc-destination-editor-mappings-page.md)  
  
-   [ODBC Destination Editor &#40;Connection Manager Page&#41;](../odbc-destination-editor-connection-manager-page.md)  
  
-   [Load Data by Using the ODBC Destination](odbc-destination.md)  
  
-   [ODBC Destination Custom Properties](odbc-destination-custom-properties.md)  
  
  
