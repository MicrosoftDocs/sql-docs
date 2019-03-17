---
title: "Client-side XML Formatting (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "FOR XML clause, formatting"
  - "middle tier XML formatting [SQLXML]"
  - "client-side XML formatting"
  - "client-side-xml attribute"
ms.assetid: 9630a21d-a93b-4d3b-8a25-c4b32399f993
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Client-side XML Formatting (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  This topic provides information about client-side XML formatting. Client-side formatting refers to the formatting of XML on the middle tier.  
  
> [!NOTE]  
>  This topic provides additional information about using the FOR XML clause on the client side, and assumes you are already familiar with the FOR XML clause. For more information about FOR XML, see [Constructing XML Using FOR XML](../../../relational-databases/xml/for-xml-sql-server.md).  
  
 **Important** To use client-side FOR XML functionality with the new **xml** data type, clients should always use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client (SQLNCLI11) data provider instead of the SQLOLEDB provider. SQLNCLI11 is the latest version of the SQL Server provider and fully understands data types introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. The behavior for client side FOR XML with the SQLOLEDB provider will treat **xml** data types as strings.  
  
## Formatting XML Documents on the Client Side  
 When a client application executes the following query:  
  
```  
SELECT FirstName, LastName  
FROM   Person.Contact  
FOR XML RAW  
```  
  
 ...only this part of the query is sent to the server:  
  
```  
SELECT FirstName, LastName  
FROM   Person.Contact  
```  
  
 The server executes the query and returns a rowset (which contains FirstName and LastNamecolumns) to the client. The middle tier then applies the FOR XML transformation to the rowset and returns XML formatting to the client.  
  
 Similarly, when you execute an XPath query, the server returns the rowset to the client and the FOR XML EXPLICIT transformation is applied to the rowset on the client, generating the desired XML formatting.  
  
 The following table shows the modes you can specify with client-side FOR XML.  
  
|Client-side FOR XML mode|Comment|  
|-------------------------------|-------------|  
|RAW|Produces identical results when specified in client-side or server-side FOR XML.|  
|NESTED|Is similar to FOR XML AUTO mode on the server-side.|  
|EXPLICIT|Is similar to server-side FOR XML EXPLICIT mode.|  
  
> [!NOTE]  
>  If you specify AUTO mode and request client-side XML formatting, the entire query is sent to the server; that is, XML formatting occurs on the server. This is done for convenience, but note that the NESTED mode returns base table names as element names in the XML document that is generated. Some of the applications you write might require base table names. For example, you might execute a stored procedure and load the resulting data in a Dataset (in the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework), and then later generate a DiffGram to update data in the tables. In such a case, you would need the base table information and you would have to use the NESTED mode.  
  
## Benefits of Client-side XML formatting  
 The following are some benefits of formatting XML on the client.  
  
### If you have stored procedures on the server that return a single rowset, you can request client-side FOR XML transformation to generate an XML.  
 For example, consider the following stored procedure. This procedure returns the first and last names of employees from the Person.Contact table in the AdventureWorks database:  
  
```  
IF EXISTS (SELECT name FROM sysobjects  
   WHERE name = 'GetContacts' AND type = 'P')  
   DROP PROCEDURE GetContacts  
GO  
CREATE PROCEDURE GetContacts  
AS  
    SELECT   FirstName, LastName  
    FROM     Person.Contact  
```  
  
 The following sample XML template executes the stored procedure. The FOR XML clause is specified after the stored procedure name.  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <sql:query client-side-xml="1">  
    EXEC GetContacts FOR XML NESTED  
  </sql:query>  
</ROOT>  
```  
  
 Because the **client-side-xml** attribute is set to 1 (true) in the template, the stored procedure is executed on the server and the two-column rowset that is returned by the server is transformed into XML on the middle tier and returned to the client. (Only a partial result is shown here.)  
  
```  
 <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Person.Contact FirstName="Gustavo" LastName="Achong" />   
  <Person.Contact FirstName="Catherine" LastName="Abel" />  
</ROOT>  
```  
  
> [!NOTE]  
>  When you are using the SQLXMLOLEDB Provider or SQLXML Managed Classes, you can use the **ClientSideXml** property to request client-side XML formatting.  
  
### The workload is more balanced.  
 Because the client does the XML formatting, the workload is balanced between the server and client, freeing the server to do other things.  
  
## Supporting Client-side XML Formatting  
 To support the client-side XML formatting functionality, SQLXML provides:  
  
-   SQLXMLOLEDB Provider  
  
-   SQLXML Managed Classes  
  
-   Enhanced XML template support  
  
-   SqlXmlCommand.ClientSideXml property  
  
     You can specify client-side formatting by setting this property of the SQLXML managed classes to true.  
  
## Enhanced XML Template Support  
 Beginning with [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], the XML template in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] has been enhanced with the addition of the **client-side-xml** attribute. If this attribute is set to true, XML is formatted on the client. Note that this template attribute is identical in functionality to the SQLXMLOLEDB Provider-specific ClientSideXML property.  
  
> [!NOTE]  
>  If you execute an XML template in an ADO application that is using the SQLXMLOLEDB Provider, and you specify both the **client-side-xml** attribute in the template and the provider ClientSideXML property, the value specified in the template takes precedence.  
  
## See Also  
 [Architecture of Client-side and Server-side XML Formatting &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml/formatting/architecture-of-client-side-and-server-side-xml-formatting-sqlxml-4-0.md)   
 [FOR XML &#40;SQL Server&#41;](../../../relational-databases/xml/for-xml-sql-server.md)   
 [FOR XML Security Considerations &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/security/for-xml-security-considerations-sqlxml-4-0.md)   
 [xml Data Type Support in SQLXML 4.0](../../../relational-databases/sqlxml/xml-data-type-support-in-sqlxml-4-0.md)   
 [SQLXML Managed Classes](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/sqlxml-4-0-net-framework-support-managed-classes.md)   
 [Client-side vs. Server-side XML Formatting &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml/formatting/client-side-vs-server-side-xml-formatting-sqlxml-4-0.md)   
 [SqlXmlCommand Object &#40;SQLXML Managed Classes&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/sqlxml-managed-classes-sqlxmlcommand-object.md)   
 [XML Data &#40;SQL Server&#41;](../../../relational-databases/xml/xml-data-sql-server.md)  
  
  
