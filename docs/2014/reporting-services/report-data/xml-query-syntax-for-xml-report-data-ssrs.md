---
title: "XML Query Syntax for XML Report Data (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "namespaces [Reporting Services]"
  - "data processing extensions [Reporting Services], data sources"
  - "xmldp [Reporting Services]"
  - "XML [Reporting Services], data retrieval"
ms.assetid: d203886f-faa1-4a02-88f5-dd4c217181ef
author: markingmyname
ms.author: maghan
manager: kfile
---
# XML Query Syntax for XML Report Data (SSRS)
  In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you can create datasets for XML data sources. After you define a data source, you create a query for the dataset. Depending on the type of XML data pointed to by the data source, you create the dataset query by including an XML `Query` or an element path. An XML `Query` starts with a **\<Query>** tag and includes namespaces and XML elements that vary depending on the data source. An element path is namespace-independent and specifies which nodes and node attributes to use from the underlying XML data with an XPath-like syntax. For more information about element paths, see [Element Path Syntax for XML Report Data &#40;SSRS&#41;](report-data-ssrs.md).  
  
 You can create an XML data source for the following types of XML data:  
  
-   Xml documents pointed to by a URL using http protocol  
  
-   Web service endpoints that return XML data  
  
-   Embedded XML data  
  
 How you specify an XML `Query` or an element path depends on the type of XML data.  
  
 For an XML document, the XML `Query` is optional. If it is included, it can contain an optional XML `ElementPath`. The value of the XML `ElementPath` uses the element path syntax. You include the XML `Query` and XML `ElementPath` to process namespaces correctly when it is needed by the XML data from the data source.  
  
 For a Web service endpoint pointed to by a connection string URL, the XML `Query` defines either the Web service method, the SOAP action, or both. The XML data provider creates a Web service request that retrieves XML data to use for the report.  
  
> [!NOTE]  
>  When a Web service namespace includes a forward slash (`/)` character, include both the Web service method and the SOAP action so that the XML data processing extension can derive the namespace correctly.  
  
 For an embedded XML document, the XML `Query` defines the embedded XML data to use, includes optional namespaces, and contains an optional XML `ElementPath`..  
  
## Specifying Query Parameters for XML Data  
 You can specify query parameters for XML documents.  
  
-   For URL requests, the query parameters are included as standard URL parameters.  
  
-   For Web service requests, query parameters are passed to the Web service method. To define a query parameter, use the **Parameters** page of the **Dataset Properties** dialog box. For more information, see [Dataset Properties Dialog Box, Parameters](dataset-properties-dialog-box-parameters.md).  
  
### Example  
 The examples in the following table illustrate how to retrieve data from the Report Server Web service, an XML document, and embedded XML data.  
  
|XML data source|Query example|  
|---------------------|-------------------|  
|Web service XML data from ListChildren method.|`<Query>`<br /><br /> `<Method Name="ListChildren" Namespace="https://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices" />`<br /><br /> `</Query>`|  
|Web service XML data from SoapAction.|`<Query xmlns=namespace>`<br /><br /> `<SoapAction>http://schemas/microsoft.com/sqlserver/2005/03/23/reporting/reportingservices/ListChildren</SoapAction>`<br /><br /> `</Query>`|  
|XML Document or embedded XML data that uses namespaces.<br /><br /> Query element specifying namespaces for an element path.|`<Query xmlns:es="https://schemas.microsoft.com/StandardSchemas/ExtendedSales">`<br /><br /> `<ElementPath>/Customers/Customer/Orders/Order/es:LineItems/es:LineItem</ElementPath>`<br /><br /> `</Query>`|  
|Embedded XML document.|`<Query>`<br /><br /> `<XmlData>`<br /><br /> `<Customers>`<br /><br /> `<Customer ID="1">Bobby</Customer>`<br /><br /> `</Customers>`<br /><br /> `</XmlData>`<br /><br /> `<ElementPath>Customer {@}</ElementPath>`<br /><br /> `</Query>`|  
|XML document that uses default.|*No query*.<br /><br /> The element path is derived from the XML document itself and is namespace-independent.|  
  
> [!NOTE]  
>  The first Web service example lists the contents of the report server that uses the <xref:ReportService2006.ReportingService2006.ListChildren%2A> method. To run this query, you must create a new data source and set the connection string to http://localhost/reportserver/reportservice2006.asmx. The <xref:ReportService2006.ReportingService2006.ListChildren%2A> method takes two parameters: `Item` and `Recursive`. Set the default value for `Item` to `/` and `Recursive` to `1`.  
  
## Specifying Namespaces  
 Use the XML `Query` element to specify the namespaces that are used in the XML data from the data source. The following XML query uses the namespace `sales`. The XML `ElementPath` nodes for `sales:LineItems` and `sales:LineItem` use the namespace `sales`.  
  
```  
<Query xmlns:sales=  
"https://schemas.microsoft.com/StandardSchemas/ExtendedSales">  
   <SoapAction>  
      https://schemas.microsoft.com/SalesWebService/ListOrders   
   </SoapAction>  
   <ElementPath>  
      Customers/Customer/Orders/Order/sales:LineItems/sales:LineItem  
   </ElementPath>  
</Query>  
```  
  
 To specify the data provider namespace so that the default namespace remains empty, use `xmldp`. This is shown in the following example.  
  
### Example  
 The following examples use the XML document DPNamespace.xml, which is provided for illustration after the table. This table shows two examples of XML ElementPath syntax that includes namespace prefixes.  
  
|XML Query Element|Resulting fields in the dataset|  
|-----------------------|-------------------------------------|  
|\<Query/>|Value A: https://schemas.microsoft.com/...<br /><br /> Value B: https://schemas.microsoft.com/...<br /><br /> Value C: https://schemas.microsoft.com/...|  
|\<xmldp:Query xmlns:xmldp="https://schemas.microsoft.com/sqlserver/2005/02/reporting/XmlDPQuery" xmlns:ns="https://schemas.microsoft.com/..."><br /><br /> \<xmldp:ElementPath>Root {}/ns:Element2/Node\</xmldp:ElementPath><br /><br /> \</xmldp:Query>|Value D<br /><br /> Value E<br /><br /> Value F|  
  
#### XML document: DPNamespace.xml  
 You can copy this XML and save it to a URL available for Report Designer to use as an XML data source: for example, http://localhost/DPNamespace.xml.  
  
```  
<Root xmlns:ns="https://schemas.microsoft.com/...">  
   <ns:Element1>  
      <Node>Value A</Node>  
      <Node>Value B</Node>  
      <Node>Value C</Node>  
   </ns:Element1>  
   <ns:Element2>  
      <Node>Value D</Node>  
      <Node>Value E</Node>  
      <Node>Value F</Node>  
   </ns:Element2>  
</Root>  
```  
  
## See Also  
 [XML Connection Type &#40;SSRS&#41;](xml-connection-type-ssrs.md)   
 [Reporting Services Tutorials &#40;SSRS&#41;](../reporting-services-tutorials-ssrs.md)  
  
  
