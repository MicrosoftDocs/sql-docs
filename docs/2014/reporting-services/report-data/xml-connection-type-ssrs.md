---
title: "XML Connection Type (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 5b55fff2-1b15-4156-83ef-15ad9cf9f509
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# XML Connection Type (SSRS)
  To include data from an XML data source in your report, you must have a dataset that is based on a report data source of type XML. This built-in data source type is based on the XML data extension. Use this data source type to connect to and retrieve data from XML documents, Web services, or XML that is embedded in the query.  
  
 This data extension supports parameters and credentials managed separately from the connection string.  
  
 Use the information in this topic to build a data source. For step-by-step instructions, see [Add and Verify a Data Connection or Data Source &#40;Report Builder and SSRS&#41;](add-and-verify-a-data-connection-report-builder-and-ssrs.md).  
  
##  <a name="Connection"></a> Connection String  
 The connection string must be a URL that points to the Web service, Web-based application, or XML document available through HTTP. XML documents must have the XML extension. You can also use an empty connection string for XML data embedded in the dataset query.  
  
 The following examples illustrate the connection string syntax for a Web service and XML document, respectively. The `file://` protocol is not supported.  
  
|XML document type|Connection String Example|  
|-----------------------|-------------------------------|  
|Web service|`http://adventure-works.com/results.aspx`|  
|XML document|`http://localhost/XML/Customers.xml`|  
|Embedded XML document|*Empty*|  
  
 For more connection string examples, see [Data Connections, Data Sources, and Connection Strings in Report Builder](../data-connections-data-sources-and-connection-strings-in-report-builder.md).  
  
##  <a name="Credentials"></a> Credentials  
 Credentials are required to run queries, to preview the report locally, and to preview the report from the report server.  
  
 After you publish your report, you may need to change the credentials for the data source so that when the report runs on the report server, the permissions to retrieve the data are valid.  
  
 From a report authoring client, the following options are available to specify credentials:  
  
-   Current Windows user (also known as integrated security).  
  
-   No credentials are required. If you select no credentials, Anonymous access is used. Make sure that you have defined the unattended execution account for the report server to connect to an external data source. The XML data processing extension does not pass credentials to the target URL or the Web service; the connection will be unsuccessful unless you have defined the unattended execution account. For more information, see [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](../install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md) in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312) on msdn.microsoft.com.  
  
 Stored and prompted credentials are not supported. Remember that if you disable Windows integrated security, you cannot use it to retrieve data. If you specify stored or prompted credentials, an error will occur at run time.  
  
 For more information, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](../data-connections-data-sources-and-connection-strings-in-reporting-services.md) or [Specify Credentials in Report Builder](../specify-credentials-in-report-builder.md).  
  
##  <a name="Query"></a> Queries  
 A query specifies which data to retrieve for a report dataset. The columns in the result set for a query populate the field collection for a dataset. A report processes only the first result set retrieved by a query.  
  
 You must use the text-based query designer to create the query. The query must return XML data.  
  
 For more information about the text-based query designer, see [Text-based Query Designer User Interface &#40;Report Builder&#41;](text-based-query-designer-user-interface-report-builder.md).  
  
 The possible values for a dataset query for a data source that is type XML are shown below.  
  
-   *Empty*: Use an empty query to create a default result set. The default query is created by reading the data source and traversing the XML node hierarchy to the first leaf collection. The result set includes all nodes with text values and all node attributes along that path. Columns in the result set are mapped to fields for the dataset.  
  
-   An element path: Specifies the sequence of nodes to use when retrieving XML data from the data source.  
  
-   An XML Query element: An XML query specification with the following optional elements:  
  
    -   **For a Web service:**  
  
         Required XML Elements:  
  
         `<Method Namespace=` *"namespace"*  `Name="MethodName" />`  
  
         `-- or --`  
  
         `<SoapAction>` *soap action* `</SoapAction>`  
  
         Optional XML Elements:  
  
         `<ElementPath>`  *element path*  `</ElementPath>`  
  
         `<Method Namespace=` *"namespace"*  `Name="MethodName" />`  
  
         `-- or --`  
  
         `<SoapAction>` *soap action* `</SoapAction>`  
  
    -   **For an XML document:**  
  
         Optional XML Elements:  
  
         `<ElementPath>`  *element path*  `</ElementPath>`  
  
    -   **For an embedded XML document:**  
  
         Required XML Elements:  
  
         `<XmlData>` inner XML `</XmlData>`  
  
         Optional XML Elements:  
  
         `<ElementPath>`  *element path*  `</ElementPath>`  
  
         `-- or --`  
  
         `<ElementPath IgnoreNamespaces="true">`  *element path*  `</ElementPath>`  
  
 For more information about query syntax, see [XML Query Syntax for XML Report Data &#40;SSRS&#41;](report-data-ssrs.md) in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312) on msdn.microsoft.com.  
  
 For examples, see [Reporting Services: Using XML and Web Service Data Sources](https://go.microsoft.com/fwlink/?LinkId=81654).  
  
### Requirements for Retrieving XML Web Service Data  
 The XML data processing extension does not detect the schema for you. Therefore, you must have some way of discovering which SOAP methods will retrieve the data that you want. You must also understand the addressing scheme or namespace that the Web service uses for its data.  
  
 For a Web service, you can provide a <`Query`> element that specifies a method to call or SOAP action. You can leave the query empty and use the default query if the XML data source has a hierarchical structure that produces the data that you want to use for your report. XML element node values and attributes retrieved when the query runs map to the dataset fields you use in your report.  
  
### Requirements for Retrieving XML Document Data  
 Using the http protocol, the server must return XML data or the XML data must be embedded in the XML `Query` element. If you refer to an XML document directly using the http protocol, the extension must be .xml.  
  
 You must know how to create an XML query that retrieves all the data you need. If you do not specify an element path, the default behavior for parsing an XML document is to select the first available path to a leaf-node collection in the XML document. If the XML document includes additional paths to other sibling leaf-node collections, those nodes will be ignored unless you specify a path in your query.  
  
 You can provide an element path using XML syntax similar to XQuery.  
  
 For more information, see [Element Path Syntax for XML Report Data &#40;SSRS&#41;](element-path-syntax-for-xml-report-data-ssrs.md) in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312) on msdn.microsoft.com.  
  
##  <a name="Parameters"></a> Parameters  
 The query is not analyzed to identify parameters.  
  
 To add parameters, you must create them manually through the **Parameter** page on the [Dataset Properties](../dataset-properties-dialog-box-parameters-report-builder.md) dialog box.  
  
##  <a name="Remarks"></a> Remarks  
 The XML data extension supports reporting from XML data that is tabular and not hierarchical. For more information, see [Add Data from External Data Sources &#40;SSRS&#41;](add-data-from-external-data-sources-ssrs.md).  
  
 There is no built-in support for retrieving XML documents from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
##  <a name="HowTo"></a> How-To Topics  
 This section contains step-by-step instructions for working with data connections, data sources, and datasets.  
  
 [Add and Verify a Data Connection or Data Source &#40;Report Builder and SSRS&#41;](add-and-verify-a-data-connection-report-builder-and-ssrs.md)  
  
 [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md)  
  
 [Add a Filter to a Dataset &#40;Report Builder and SSRS&#41;](add-a-filter-to-a-dataset-report-builder-and-ssrs.md)  
  
##  <a name="Related"></a> Related Sections  
 These sections of the documentation provide in-depth conceptual information about report data, as well as procedural information about how to define, customize, and use parts of a report that are related to data.  
  
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-datasets-ssrs.md)  
 Provides an overview of accessing data for your report.  
  
 [Data Connections, Data Sources, and Connection Strings in Report Builder](../data-connections-data-sources-and-connection-strings-in-report-builder.md)  
 Provides information about data connections and data sources.  
  
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)  
 Provides information about embedded and shared datasets.  
  
 [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](dataset-fields-collection-report-builder-and-ssrs.md)  
 Provides information about the dataset field collection generated by the query.  
  
 [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../create-deploy-and-manage-mobile-and-paginated-reports.md) in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312).  
 Provides in-depth information about platform and version support for each data extension.  
  
## See Also  
 [Report Parameters &#40;Report Builder and Report Designer&#41;](../report-design/report-parameters-report-builder-and-report-designer.md)   
 [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)   
 [Expressions &#40;Report Builder and SSRS&#41;](../report-design/expressions-report-builder-and-ssrs.md)  
  
  
