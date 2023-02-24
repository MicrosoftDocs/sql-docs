---
title: "Introduction to the SQLXMLOLEDB Provider (SQLXML)"
description: Learn about the SQLXMLOLEDB Provider, an OLE DB provider that exposes SQLXML functionality through ActiveX Data Objects (ADO).
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "SQLXMLOLEDB Provider, properties"
  - "adExecuteStream flag"
  - "SQLXMLOLEDB Provider, about SQLXMLOLEDB Provider"
ms.assetid: 2e3f3817-4209-4bf4-9f46-248c95bc6f1b
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Introduction to the SQLXMLOLEDB Provider (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  The SQLXMLOLEDB Provider is an OLE DB provider that exposes [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQLXML functionality through ActiveX Data Objects (ADO). However, the provider can execute commands only in the "write to an output stream" mode of ADO. The SQLXMLOLEDB Provider is not a rowset provider. When you execute a command, you must specify the adExecuteStream flag, which directs ADO to use the output stream that you have specified.  
  
 The following example shows the syntax for the Execute command in which the adExecuteStream flag is specified:  
  
```  
Dim oTestCommand As New ADODB.Command  
...  
oTestCommand.Properties("Output Stream").Value = oTestStream  
oTestCommand.Execute , , adExecuteStream  
...  
```  
  
## SQLXMLOLEDB Provider-specific Properties  
 The SQLXMLOLEDB Provider exposes the following provider-specific connection property.  
  
|Connection<br /><br /> property|Default<br /><br /> (if any)|Description|  
|-----------------------------|----------------------------|-----------------|  
|Data Provider||Provides the PROGID of the OLE DB provider through which SQLXMLOLEDB executes the commands. Beginning in SQLXML 4.0 and [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], this provider is contained within the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client; therefore, this property value is restricted to "SQLNCLI11". For more information, see [SQL Server Native Client Programming](../../../relational-databases/native-client/sql-server-native-client-programming.md).|  
  
 The SQLXMLOLEDB Provider exposes the following provider-specific command properties.  
  
|Command<br /><br /> property|Default<br /><br /> (if any)|Description|  
|--------------------------|----------------------------|-----------------|  
|Base Path|""|Specifies the base file path. The base file path is used to specify the location of the XML Stylesheet Language (XSL) or mapping schema files. The base file path is also used to resolve the relative paths of XSL or mapping schema files that have been specified in the XSL or Mapping Schema properties.<br /><br /> For an example in which this property is used, see [Executing XPath Queries &#40;SQLXMLOLEDB Provider&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/data-access-components-provider/executing-xpath-queries-sqlxmloledb-provider.md).|  
|ClientSideXML|False|Set this property to True if you want the process of converting the rowset to XML to occur on the client instead of on the server. This is useful when you want to move the performance load to the middle tier.<br /><br /> For an example in which this property is used, see [Executing SQL Queries &#40;SQLXMLOLEDB Provider&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/data-access-components-provider/executing-sql-queries-sqlxmloledb-provider.md) or [Executing Templates That Contain SQL Queries &#40;SQLXMLOLEDB Provider&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/data-access-components-provider/executing-templates-that-contain-sql-queries-sqlxmloledb-provider.md).|  
|Content Type||Returns the output content type. This is a READ ONLY property.<br /><br /> This property provides information to the browser about the content type (such as TEXT/XML, TEXT/HTML, image/jpeg, and so on). The value of this property becomes the **content-type** field that is sent to the browser as part of the HTTP header, which contains the MIME-type (Multipurpose Internet Mail Extensions) of the document being sent as the body.|  
|Mapping Schema|NULL|If a client application executes an XPath query against a mapping schema (XDR or XSD), this property is used to specify the name of the mapping schema.<br /><br /> The path that is specified can be relative (xyz/abc/MySchema.xml) or absolute (C:\MyFolder\abc\MySchema.xml).<br /><br /> If a relative path is specified, the base path that is specified by the Base Path property is used to resolve the relative path. If no path has been specified in the Base Path property, the relative path is relative to the current directory.<br /><br /> In specifying a value for the Mapping Schema property, you can specify a local directory path or a URL (https://...). If you specify a URL, you must configure WinHTTP to access HTTP and HTTPS servers through a proxy server. You can do this by executing the Proxycfg.exe utility. For more information, see "Using the WinHTTP Proxy Configuration Utility" in the MSDN Library.<br /><br /> For an example in which this property is used, see [Executing XPath Queries &#40;SQLXMLOLEDB Provider&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/data-access-components-provider/executing-xpath-queries-sqlxmloledb-provider.md).|  
|namespaces||This property enables the execution of XPath queries that use namespaces. For an example in which this property is used, see [Executing XPath Queries with Namespaces &#40;SQLXMLOLEDB Provider&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/data-access-components-provider/executing-xpath-queries-with-namespaces-sqlxmloledb-provider.md).|  
|ss Stream Flags||This property is used to specify particular types of security restrictions. For example, you might not want to allow URL references to files or absolute paths to files (such as external sites). Or you might not want to allow queries in the templates.<br /><br /> The property can be assigned these values:<br /><br /> 1 = STREAM_FLAGS_DISALLOW_URL 2 = STREAM_FLAGS_DISALLOW_ABSOLUTE_PATH 4 = STREAM_FLAGS_DISALLOW_QUERY 8 = STREAM_FLAGS_       DONTCACHEMAPPINGSCHEMA 16 = STREAM_FLAGS_DONTCACHETEMPLATE 32 = STREAM_FLAGS_DONTCACHEXSL<br /><br /> Additional information about these values is provided in the next table.|  
|xml root||This property is used to define a root tag for the resulting XML. For example, if you execute SQL queries against the database and the resulting XML document has no single root element, the value of the property is used to add a single root element to the document.<br /><br /> For an example in which this property is used, see [Executing SQL Queries &#40;SQLXMLOLEDB Provider&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/data-access-components-provider/executing-sql-queries-sqlxmloledb-provider.md).|  
|xsl||This property is used to specify the XSL file name when you want to apply XSL transformation to the XML document returned by the query.<br /><br /> The path that is specified can be relative (xyz/abc/MyXSL.xsl) or absolute (C:\MyFolder\abc\MyXSL.xsl).<br /><br /> If a relative path is specified, the base path that is specified by the Base Path property is used to resolve the relative path. If no path has been specified in the Base Path property, the relative path is relative to the current directory.<br /><br /> For an example in which this property is used, see Applying an XSL Transformation (SQLXMLOLEDB Provider).|  
  
 The following table contains descriptions of the ss Stream Flags property values.  
  
|Property value|Description|  
|--------------------|-----------------|  
|STREAM_FLAGS_DISALLOW_URL|URLs are not accepted for mapping schemas or XSL.|  
|STREAM_FLAGS_DISALLOW_ABSOLTE_PATH|A path that is specified for a mapping schema or for XSL must be relative to the base path of the template itself.|  
|STREAM_FLAGS_DISALLOW_QUERY|Queries are not allowed in a template.|  
|STREAM_FLAGS_DONTCACHEMAPPINGSCHEMA|The mapping schema is not cached. This property value is useful during the database development phase, when database schemas are subject to alteration.|  
|STREAM_FLAGS_DONTCACHETEMPLATE|Templates are not cached.|  
|STREAM_FLAGS_DONTCACHEXSL|XSL is not cached.|  
  
  
