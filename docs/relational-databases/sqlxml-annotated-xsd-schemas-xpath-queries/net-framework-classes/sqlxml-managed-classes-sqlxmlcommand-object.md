---
title: "SqlXmlCommand Object (SQLXML)"
description: Learn about the methods and properties of the SqlXmlCommand object.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "void ExecuteNonQuery() method"
  - "namespaces property"
  - "Stream ExecuteStream() method"
  - "CommandType property"
  - "RootTag property"
  - "OutputEncoding property"
  - "XmlReader ExecuteXmlReader() method"
  - "Managed Classes [SQLXML], SqlXmlCommand object"
  - "SQLXML Managed Classes, SqlXmlCommand object"
  - "Base Path property"
  - "void ClearParameters() method"
  - "public void ExecuteToStream(Stream outputStream) method"
  - "SqlXmlCommand object"
  - "CommandText property"
  - "XslPath property"
  - "SchemaPath property"
  - "SqlXmlParameter CreateParameter() method"
  - "ClientSideXML property"
  - "CommandStream property"
ms.assetid: c1f9e0bb-a89d-4d6a-a96e-289ef516a3a6
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLXML Managed Classes - SqlXmlCommand Object
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  This is the constructor for the SqlXmlCommand object:  
  
```  
public SqlXmlCommand(string cnString)  
```  
  
 Where `cnString` is the ADO or OLEDB connection string that identifies the server, database, and the login information-for example, `Provider=SQLOLEDB; Server=(local); database=AdventureWorks; Integrated Security=SSPI"`.  
  
 In the connection string, the `Provider` must be SQLOLEDB and the `Data Provider` should not be included in the provider string).  
  
 For a working sample, see [Executing SQL Queries &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-sql-queries-sqlxml-managed-classes.md).  
  
## Methods  
 TheSqlXmlCommand object supports several methods, including the following methods for executing a command:  
  
 void ExecuteNonQuery()  
 Executes the command, but does not return anything. This method is useful if you want to execute a nonquery command (that is, a command that does not return anything). An example is executing an updategram or a DiffGram that updates records but returns nothing.  
  
 Stream ExecuteStream()  
 Returns a new Stream object. This method is useful when you want the query results returned to you in a new stream. For a working sample, see [Executing SQL Queries &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-sql-queries-sqlxml-managed-classes.md).  
  
 public void ExecuteToStream(Stream outputStream)  
 Writes the query results to an existing stream. This method is useful when you have a stream to which you need the results appended (for example, to have the query results written to the System.Web.HttpResponse.OutputStream). For a working sample, see [Executing SQL Queries &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-sql-queries-sqlxml-managed-classes.md).  
  
 XmlReader ExecuteXmlReader()  
 Returns an XmlReader object. You can use this method to either manipulate data in the XmlReader object directly or plug in the chainable architecture of System.Xml. For more information, see the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework documentation. For a working sample, see [Executing SQL Queries by Using the ExecuteXMLReader Method](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-sql-queries-by-using-the-executexmlreader-method.md).  
  
 TheSqlXmlCommand object also supports these additional methods:  
  
 SqlXmlParameter CreateParameter()  
 Creates an SqlXmlParameter object. You can set values for the *Name* and *Value* parameters of this object. This method is useful if you want to pass parameters to a command. For a working sample, see [Executing SQL Queries &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-sql-queries-sqlxml-managed-classes.md).  
  
 void ClearParameters()  
 Clears parameter(s) that were created for a given command object. This method is useful if you want to execute multiple queries on the same command object.  
  
## Properties  
 The SqlXmlCommand object also supports these properties:  
  
 ClientSideXml  
 When set to True, specifies that conversion of the rowset to XML is to occur on the client instead of on the server. This property is useful when you want to move the performance load to the middle tier. The property also allows you to wrap the existing stored procedures with FOR XML to get XML output.  
  
 SchemaPath  
 The name of the mapping schema along with the directory path (for example, C:\x\y\MySchema.xml). This property is useful for specifying a mapping schema for XPath queries. The path that is specified can be absolute or relative. If the path is relative, the base path that is specified in Base Path is used to resolve the relative path. If no base path is specified, the relative path is relative to the current directory. For a working sample, see [Accessing SQLXML Functionality in the .NET Environment](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/accessing-sqlxml-functionality-in-the-net-environment.md).  
  
 XslPath  
 The name of the XSL file along with the directory path. The path that is specified can be absolute or relative. If the path is relative, the base path that is specified in Base Path is used to resolve the relative path. If no base path is specified, the relative path is relative to the current directory. For a working sample, see [Applying an XSL Transformation &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/applying-an-xsl-transformation-sqlxml-managed-classes.md).  
  
 Base Path  
 The base path (a directory path). This property is useful for resolving a relative path that is specified for an XSL file (by using the XslPath property), a mapping schema file (by using the SchemaPath property), or an external schema reference in an XML template (specified by using the **mapping-schema** attribute).  
  
 OutputEncoding  
 Specifies the encoding for the stream that is returned when the command executes. This property is useful for requesting a specific encoding for the stream that is returned. Some commonly used encodings are UTF-8, ANSI, and Unicode. UTF-8 is the default encoding.  
  
 Namespaces  
 Enables the execution of XPath queries that use namespaces. For more information about XPath queries with namespaces, see [Executing XPath Queries with Namespaces &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-xpath-queries-with-namespaces-sqlxml-managed-classes.md). For a working sample, see [Executing XPath Queries &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-xpath-queries-sqlxml-managed-classes.md).  
  
 RootTag  
 Provides the single root element for XML generated by command execution. A valid XML document requires a single root-level tag. If the command executed generates an XML fragment (without a single top-level element) you can specify a root element for the returning XML. For a working sample, see [Applying an XSL Transformation &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/applying-an-xsl-transformation-sqlxml-managed-classes.md).  
  
 CommandText  
 The text of the command. This property is used for specifying the text of the command you want to execute. For a working sample, see [Executing SQL Queries &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-sql-queries-sqlxml-managed-classes.md).  
  
 CommandStream  
 The command stream. This property is useful if you want to execute a command from a file (for example, an XML template). When you are using CommandStream, only **"Template"**, **"UpdateGram"** and **"DiffGram"CommandType** values are supported. For a working sample, see [Executing Template Files by Using the CommandStream Property](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/executing-template-files-by-using-the-commandstream-property.md).  
  
 CommandType  
 Identifies the type of command. This property is used for specifying the type of command you want to execute. The values in the following table determine the type of the command. For a working sample, see [Accessing SQLXML Functionality in the .NET Environment](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/accessing-sqlxml-functionality-in-the-net-environment.md).  
  
|Value|Description|  
|-----------|-----------------|  
|SqlXmlCommandType.Sql|Executes an SQL command (for example, `SELECT * FROM Employees FOR XML AUTO`).|  
|SqlXmlCommandType.XPath|Executes an XPath command (for example, `Employees[@EmployeeID=1]`).|  
|SqlXmlCommandType.Template|Executes an XML template.|  
|SqlXmlCommandType.TemplateFile|Executes a template file at the specified path.|  
|SqlXmlCommandType.UpdateGram|Executes an updategram.|  
|SqlXmlCommandType.Diffgram|Executes a DiffGram.|  
  
## See Also  
 [SqlXmlParameter Object &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/sqlxml-managed-classes-sqlxmlparameter-object.md)   
 [SqlXmlAdapter Object &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/sqlxml-managed-classes-sqlxmladapter-object.md)  
  
  
