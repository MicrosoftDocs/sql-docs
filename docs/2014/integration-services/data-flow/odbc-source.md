---
title: "ODBC Source | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.designer.odbcsource.f1"
ms.assetid: abcf34eb-9140-4100-82e6-b85bccd22abe
author: douglaslMS
ms.author: douglasl
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
  
 Depending on the error behavior setting, the ODBC source supports returning errors (data conversion, truncation) that occur during the extraction process in the error output. For more information, see [ODBC Destination Editor &#40;Connection Manager Page&#41;](../odbc-destination-editor-connection-manager-page.md).  
  
## Data Type Support  
 For information about the data types supported by the ODBC source, see Connector for Open Database Connectivity (ODBC) by Attunity.  
  
## Extract Options  
 The ODBC source operates in either **Batch** or **Row-by-Row** mode. The mode used is determined by the **FetchMethod** property. The following list describes the modes.  
  
-   **Batch**: The component attempts to use the most efficient fetch method based on the perceived ODBC provider capabilities. For most modern ODBC providers, this is SQLFetchScroll with array binding (where the array size is determined by the **BatchSize** property). If you select **Batch** and the provider does not support this method, the ODBC destination automatically switches to the **Row-by-row** mode.  
  
-   **Row-by Row**: The component uses SQLFetch to retrieve the rows one at a time.  
  
 For more information about the **FetchMethod** property, see [ODBC Source Custom Properties](odbc-source-custom-properties.md).  
  
## Parallelism  
 There is no limitation on the number of ODBC source components that can run in parallel against the same table or different tables, on the same machine or on different machines (other than normal global session limits).  
  
 However, limitations of the ODBC provider being used may restrict the number of concurrent connections through the provider. These limitations limit the number of supported parallel instances possible for the ODBC source. The SSIS developer must be aware of the limitations of any ODBC provider being used and take them into consideration when building SSIS packages.  
  
## Troubleshooting the ODBC Source  
 You can log the calls that the ODBC source makes to external data providers. You can use this logging capability to troubleshoot the loading of data from external data sources that the ODBC source performs. To log the calls that the ODBC source makes to external data providers, enable the ODBC driver manager trace. For more information, see the Microsoft documentation on *How To Generate an ODBC Trace with ODBC the Data Source Administrator.*  
  
## Configuring the ODBC Source  
 You can configure the ODBC source programmatically or through the SSIS Designer.  
  
 For more information, see one of the following topics:  
  
-   [ODBC Source Editor &#40;Connection Manager Page&#41;](../odbc-source-editor-connection-manager-page.md)  
  
-   [ODBC Source Editor &#40;Columns Page&#41;](../odbc-source-editor-columns-page.md)  
  
-   [ODBC Source Editor &#40;Error Output Page&#41;](../odbc-source-editor-error-output-page.md)  
  
 The **Advanced Editor** dialog box contains the properties that can be set programmatically.  
  
 To open the **Advanced Editor** dialog box:  
  
-   In the **Data Flow** screen of your [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] project, right click the ODBC source and select **Show Advanced Editor**.  
  
 For more information about the properties that you can set in the Advanced Editor dialog box, see [ODBC Source Custom Properties](odbc-source-custom-properties.md).  
  
## In This Section  
  
-   [ODBC Source Editor &#40;Error Output Page&#41;](../odbc-source-editor-error-output-page.md)  
  
-   [ODBC Source Editor &#40;Columns Page&#41;](../odbc-source-editor-columns-page.md)  
  
-   [ODBC Source Editor &#40;Connection Manager Page&#41;](../odbc-source-editor-connection-manager-page.md)  
  
-   [Extract Data by Using the ODBC Source](odbc-source.md)  
  
-   [ODBC Source Custom Properties](odbc-source-custom-properties.md)  
  
  
