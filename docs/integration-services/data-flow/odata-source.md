---
title: "OData Source | Microsoft Docs"
ms.date: "09/17/2018"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.custom: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.DTS.DESIGNER.ODATASOURCE.F1"
  - "sql13.dts.designer.odatasource.connection.f1"
  - "sql13.dts.designer.odatasource.columns.f1"
  - "sql13.dts.designer.odatasource.erroroutput.f1"
ms.assetid: cc9003c9-638e-432b-867e-e949d50cec90
author: janinezhang
ms.author: janinez
manager: craigg
---
# OData Source

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


Use the OData Source component in an SSIS package to consume data from an Open Data Protocol (OData) service.

## Supported protocols and data formats

The component supports the OData v3 and v4 protocols.  
  
-   For OData V3 protocol, the component supports the ATOM and JSON data formats.  
  
-   For OData V4 protocol, the component supports the JSON data format.  

## Supported data sources

The OData source includes support for the following data sources:
-   Microsoft Dynamics AX Online and Microsoft Dynamics CRM Online
-   SharePoint lists. To see all the lists on a SharePoint server, use the following URL: https://\<server>/_vti_bin/ListData.svc. For more information about SharePoint URL conventions, see [SharePoint Foundation REST Interface](https://msdn.microsoft.com/library/ff521587.aspx).

## Supported data types

The OData source supports the following simple data types: int, byte[], bool, byte, DateTime, DateTimeOffset, decimal, double, Guid, Int16, Int32, Int64, sbyte, float, string, and TimeSpan.

To discover the data types of columns in your data source, check the `https://<OData feed endpoint>/$metadata` page.

> [!IMPORTANT]
> The OData Source component does not support complex types, such as multiple-choice items, in SharePoint lists.

## OData Format and Performance
 Most OData services can return results in multiple formats. You can specify the format of the result set by using the `$format` query option. Formats such as JSON and JSON Light are more efficient than ATOM or XML, and may give you better performance when transferring large amounts of data. The following table provides results from sample tests. As you can see, there was a 30-53% performance gain when switching from ATOM to JSON and a 67% performance gain when switching from ATOM to the new JSON light format (available in WCF Data Services 5.1).  
  
|Rows|ATOM|JSON|JSON (Light)|  
|-|-|-|-|  
|10000|113 seconds|74 seconds|68 seconds|  
|1000000|1110 seconds|853 seconds|665 seconds|  
  
## Related Topics in This Section  
  
-   [Tutorial: Using the OData Source](../../integration-services/data-flow/tutorial-using-the-odata-source.md)  
  
-   [Modify OData Source Query at Runtime](../../integration-services/data-flow/modify-odata-source-query-at-runtime.md)  
  
-   [OData Source Properties](../../integration-services/data-flow/odata-source-properties.md)  
  
## OData Source Editor (Connection Page)
  Use the **Connection** page of the **OData Source Editor** dialog box to select the OData connection manager for the OData source. This page also lets you specify a collection or a resource path and any query options to indicate what data needs to be retrieved from the OData source. 
  
### Static Options  
 **OData connection manager**  
 Select an existing connection manager from the list, or create a new connection by clicking **New**.  
  
 After you select or create a connection manager, the dialog box displays the OData protocol version that the connection manager is using.  
  
 **New**  
 Create a new connection manager by using the **OData Connection Manager Editor** dialog box.  
  
 **Use collection or resource path**  
 Specify the method for selecting data from the source.  
  
|Option|Description|  
|------------|-----------------|  
|Collection|Retrieve data from the OData source by using a collection name.|  
|Resource Path|Retrieve data from the OData source by using a resource path.|  
  
 **Query options**  
 Specify options for the query. For example: `$top=5` 
  
 **Feed url**  
 Displays the read-only feed URL based on options you selected on this dialog box.  
  
 **Preview**  
 Preview results by using the **Preview** dialog box. **Preview** can display up to 20 rows.  
  
### Dynamic Options  
  
#### Use collection or resource path = Collection  
 **Collection**  
 Select a collection from the drop-down list.  
  
#### Use collection or resource path = Resource Path  
 **Resource path**  
 Type a resource path. For example: Employees  
  
## OData Source Editor (Columns Page)
  Use the **Columns** page of the **OData Source Editor** dialog box to select external (source) columns to be included in the output and map them to output columns.  
  
### Options  
 **Available External Columns**  
 View the list of available source columns in the data source. Use check boxes in the list to add to or remove columns to the table at the bottom of the page. The selected columns are added to the output.  
  
 **External Column**  
 View source columns that you chose to be included in the output.  
  
 **Output Column**  
 Provide a unique name for each output column. The default is the name of the selected external (source) column; however, you can choose any unique, descriptive name.  
  
## OData Source Editor (Error Output Page)
  Use the **Error Output** page of the **OData Source Editor** dialog box to select error handling options and to set properties on error output columns.  
  
### Options  
 **Input/Output**  
 View the name of the data source.  
  
 **Column**  
 View the external (source) columns that you selected on the **Connection Manager** page of the **OData Source Editor** dialog box.  
  
 **Error**  
 Specify what should happen when an error occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Related Topics:** [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md)  
  
 **Truncation**  
 Specify what should happen when a truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Description**  
 View the description of the error.  
  
 **Set this value to selected cells**  
 Specify what should happen to all the selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Apply**  
 Apply the error handling option to the selected cells.  
  
## See Also  
 [OData Connection Manager](../../integration-services/connection-manager/odata-connection-manager.md)  
  
  
