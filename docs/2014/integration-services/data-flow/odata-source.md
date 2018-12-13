---
title: "OData Source | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "SQL12.DTS.DESIGNER.ODATASOURCE.F1"
ms.assetid: cc9003c9-638e-432b-867e-e949d50cec90
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# OData Source
  You use the OData Source component in an SSIS package to consume data from Open Data Protocol (OData) services. The component supports the OData v2 and v3 protocols, as well as the ATOM and JSON data formats.  
  
> [!NOTE]  
>  The OData Source can be used to read from SharePoint lists. To see all lists on your SharePoint server, use the following URL: http://\<server>/_vti_bin/ListData.svc. For more information about SharePoint URL conventions, see [SharePoint Foundation REST Interface](https://msdn.microsoft.com/library/ff521587.aspx).  
  
## OData Format  
 Most OData services return results in multiple formats. You can specify the format of the result set by using the $format query option. Formats such as JSON and JSON Light are more efficient than ATOM/XML, and may give you better performance when transferring large amounts of data. The following table provides results from sample tests. As you can see, there was a 30-53% performance gain when switching from ATOM to JSON and 67% performance gain when switching from ATOM to the new JSON light format (available in WCF Data Services 5.1).  
  
|||||  
|-|-|-|-|  
|Rows|ATOM|JSON|JSON (Light)|  
|10000|113 seconds|74 seconds|68 seconds|  
|1000000|1110 seconds|853 seconds|665 seconds|  
  
> [!NOTE]  
>  The SSIS OData Source uses ODataLib 5.5.0 to parse OData feeds and it can read all formats supported by this library.  
  
## In This Section  
  
-   [Install and Uninstall OData Source Component](../install-and-uninstall-odata-source-component.md)  
  
-   [Tutorial: Using the OData Source &#91;SSIS&#93;](tutorial-using-the-odata-source.md)  
  
-   [Modify OData Source Query at Runtime](modify-odata-source-query-at-runtime.md)  
  
-   [OData Source Editor &#40;Connection Page&#41;](../odata-source-editor-connection-page.md)  
  
-   [OData Source Editor &#40;Columns Page&#41;](../odata-source-editor-columns-page.md)  
  
-   [OData Source Editor &#40;Error Output Page&#41;](../odata-source-editor-error-output-page.md)  
  
-   [OData Source Properties](odata-source-properties.md)  
  
## See Also  
 [OData Connection Manager](../connection-manager/odata-connection-manager.md)  
  
  
