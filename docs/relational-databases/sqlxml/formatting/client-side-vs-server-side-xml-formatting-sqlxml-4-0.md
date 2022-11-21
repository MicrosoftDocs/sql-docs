---
title: "Client-side vs. Server-side XML Formatting (SQLXML)"
description: Learn the general differences between client-side and server-side XML formatting in SQLXML 4.0.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "NESTED mode"
  - "FOR XML clause, formatting"
  - "client-side XML formatting"
  - "server-side XPath"
  - "server-side XML formatting"
  - "AUTO mode"
  - "client-side XPath"
ms.assetid: f807ab7a-c5f8-4e61-9b00-23aebfabc47e
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Client-side vs. Server-side XML Formatting (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  This topic describes the general differences between client-side and server-side XML formatting in SQLXML.  
  
## Multiple Rowset Queries Not Supported in Client-side Formatting  
 Queries that generate multiple rowsets are not supported when you use client-side XML formatting. For example, assume you have a virtual directory in which you have client-side formatting specified. Consider this sample template, which has two SELECT statements in a **\<sql:query>** block:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <sql:query>  
     SELECT FirstName FROM Person.Contact FOR XML Nested;   
     SELECT LastName FROM Person.Contact FOR XML Nested    
  </sql:query>  
</ROOT>  
```  
  
 You can execute this template in application code and an error is returned, because client-side XML formatting does not support formatting of multiple rowsets. If you specify the queries in two separate **\<sql:query>** blocks, you will get the desired results.  
  
## timestamp Maps Differently in Client- vs. Server-side Formatting  
 In server-side XML formatting, the database column of **timestamp** type maps to the i8 XDR type (when the XMLDATA option is specified in the query).  
  
 In client-side XML formatting, the database column of **timestamp** type maps to either the **uri** or the **bin.base64** XDR type (depending on whether the binary base64 option is specified in the query). The **bin.base64** XDR type is useful if you use the updategram and bulkload features, because this type is converted to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **timestamp** type. This way, the insert, update, or delete operation succeeds.  
  
## Deep VARIANTs Are Used in Server-side Formatting  
 In server-side XML formatting, the deep types of a VARIANT type are used. If you use client-side XML formatting, the variants are converted to Unicode string, and the subtypes of VARIANT are not used.  
  
## NESTED Mode vs. AUTO Mode  
 The NESTED mode of the client-side FOR XML is similar to the AUTO mode of server-side FOR XML, with the following exceptions:  
  
### When you query views using AUTO mode on the server-side, the view name is returned as the element name in the resulting XML.  
 For example, assume that the following view is created on the Person.Contact table in the AdventureWorksdatabase:  
  
```  
CREATE VIEW ContactView AS (SELECT ContactID as CID,  
                               FirstName  as FName,  
                               LastName  as LName  
                        FROM Person.Contact)  
```  
  
 The following template specifies a query against the ContactView view, and also specifies server-side XML formatting:  
  
```  
 <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <sql:query client-side-xml="0">  
    SELECT *  
    FROM   ContactView  
    FOR XML AUTO  
  </sql:query>  
</ROOT>  
```  
  
 When you execute the template, the following XML is returned. (Only partial results are shown.) Note that the element names are the names of the views against which the query is executed.  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <ContactView CID="1" FName="Gustavo" LName="Achong" />   
  <ContactView CID="2" FName="Catherine" LName="Abel" />   
...  
</ROOT>  
```  
  
 When you specify client-side XML formatting by using the corresponding NESTED mode, the base table name(s) are returned as the element name(s) in the resulting XML. For example, the following revised template executes the same SELECT statement, but the XML formatting is performed on the client-side (that is, **client-side-xml** is set to true in the template):  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <sql:query client-side-xml="1">  
    SELECT *  
    FROM   ContactView  
    FOR XML NESTED  
  </sql:query>  
</ROOT>  
```  
  
 Executing this template produces the following XML. Note that the element name is the base table name in this case.  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Person.Contact CID="1" FName="Gustavo" LName="Achong" />   
  <Person.Contact CID="2" FName="Catherine" LName="Abel" />   
...  
</ROOT>  
```  
  
### When you use AUTO mode of the server-side FOR XML, the table aliases specified in the query are returned as element names in the resulting XML.  
 For example, consider this template:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <sql:query client-side-xml="0">  
    SELECT FirstName as fname,  
           LastName as lname  
    FROM   Person.Contact C  
    FOR XML AUTO  
  </sql:query>  
</ROOT>  
```  
  
 Executing the template produces the following XML:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <C fname="Gustavo" lname="Achong" />   
  <C fname="Catherine" lname="Abel" />   
...  
</ROOT>   
```  
  
 When you use the NESTED mode of the client-side FOR XML, the table names are returned as element names in the resulting XML. (Table aliases that are specified in the query are not used.) For example, consider this template:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <sql:query client-side-xml="1">  
    SELECT FirstName as fname,  
           LastName as lname  
    FROM   Person.Contact C  
    FOR XML NESTED  
  </sql:query>  
</ROOT>  
```  
  
 Executing the template produces the following XML:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Person.Contact fname="Gustavo" lname="Achong" />   
  <Person.Contact fname="Catherine" lname="Abel" />   
...  
</ROOT>  
```  
  
### If you have a query that returns columns as dbobject queries, you cannot use aliases for these columns.  
 For example, consider the following template, which executes a query that returns an employee ID and a photo.  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
<sql:query client-side-xml="1">  
   SELECT ProductPhotoID, LargePhoto as P  
   FROM   Production.ProductPhoto  
   WHERE  ProductPhotoID=5  
   FOR XML NESTED, elements  
</sql:query>  
</ROOT>  
```  
  
 Executing this template returns the Photo column as a dbobject query. In this dbobject query, `@P` refers to a column name that does not exist.  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Production.ProductPhoto>  
    <ProductPhotoID>5</ProductPhotoID>  
    <LargePhoto>dbobject/Production.ProductPhoto[@ProductPhotoID='5']/@P</LargePhoto>  
  </Production.ProductPhoto>  
</ROOT>  
```  
  
 If the XML formatting is done on the server (**client-side-xml="0"**), you can use the alias for the columns that return dbobject queries in which actual table and column names are returned (even if you have aliases specified). For example, the following template executes a query, and the XML formatting is done on the server (the **client-side-xml** option is not specified and the **Run On Client** option is not selected for the virtual root). The query also specifies AUTO mode (not the client-side NESTED mode).  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
<sql:query   
   SELECT ProductPhotoID, LargePhoto as P  
   FROM   Production.ProductPhoto  
   WHERE  ProductPhotoID=5  
   FOR XML AUTO, elements  
</sql:query>  
</ROOT>  
```  
  
 When this template is executed, the following XML document is returned (note that aliases are not used in the dbobject query for the LargePhoto column):  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Production.ProductPhoto>  
    <ProductPhotoID>5</ProductPhotoID>  
    <LargePhoto>dbobject/Production.ProductPhoto[@ProductPhotoID='5']/@LargePhoto</LargePhoto>  
  </Production.ProductPhoto>  
</ROOT>  
```  
  
### Client-side vs. Server-side XPath  
 Client-side XPath and server-side XPath work the same except for these differences:  
  
-   The data conversions that are applied when you use client-side XPath queries are different from those that are applied when you use server-side XPath queries. Client-side XPath uses CAST instead of CONVERT mode 126.  
  
-   When you specify **client-side-xml="0"** (false) in a template, you are requesting server-side XML formatting. Therefore, you cannot specify FOR XML NESTED because the server does not recognize the NESTED option. This generates an error. You must use the AUTO, RAW, or EXPLICIT modes, which the server does recognize.  
  
-   When you specify **client-side-xml="1"** (true) in a template, you are requesting client-side XML formatting. In this case, you can specify FOR XML NESTED. If you specify FOR XML AUTO, the XML formatting occurs on the server side although **client-side-xml="1"** is specified in the template.  
  
## See Also  
 [FOR XML Security Considerations &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/security/for-xml-security-considerations-sqlxml-4-0.md)   
 [Client-side XML Formatting &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml/formatting/client-side-xml-formatting-sqlxml-4-0.md)   
 [Server-side XML Formatting &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml/formatting/server-side-xml-formatting-sqlxml-4-0.md)  
  
  
