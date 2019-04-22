---
title: "Add Namespaces to Queries with WITH XMLNAMESPACES | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "ELEMENTS XSINIL directive"
  - "adding namespaces"
  - "XSINIL directive"
  - "default namespaces"
  - "queries [XML in SQL Server], WITH XMLNAMESPACES clause"
  - "predefined namespaces [XML in SQL Server]"
  - "FOR XML clause, WITH XMLNAMESPACES clause"
  - "namespaces [XML in SQL Server]"
  - "xml data type [SQL Server], WITH XMLNAMESPACES clause"
  - "WITH XMLNAMESPACES clause"
ms.assetid: 2189cb5e-4460-46c5-a254-20c833ebbfec
author: MightyPen
ms.author: genemi
manager: craigg
---
# Add Namespaces to Queries with WITH XMLNAMESPACES
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  [WITH XMLNAMESPACES (Transact-SQL)](../../t-sql/xml/with-xmlnamespaces.md) provides namespace URI support in the following way:  
  
-   It makes the namespace prefix to URI mapping available when [Constructing XML Using FOR XML](../../relational-databases/xml/for-xml-sql-server.md) queries.  
  
-   It makes the namespace to URI mapping available to the static namespace context of the [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md).  
  
## Using WITH XMLNAMESPACES in the FOR XML Queries  
 WITH XMLNAMESPACES lets you include XML namespaces in FOR XML queries. For example, consider the following FOR XML query:  
  
```  
SELECT ProductID, Name, Color  
FROM   Production.Product  
WHERE  ProductID=316 or ProductID=317  
FOR XML RAW  
```  
  
 This is the result:  
  
```  
<row ProductID="316" Name="Blade" />  
<row ProductID="317" Name="LL Crankarm" Color="Black" />  
  
```  
  
 To add namespaces to the XML constructed by the FOR XML query, first specify the namespace prefix to URI mappings by using the WITH NAMESPACES clause. Then, use the namespace prefixes in specifying the names in the query as shown in the following modified query. Note that the WITH XMLNAMESPACES clause specifies the namespace prefix (`ns1`) to URI (`uri`) mapping. The `ns1` prefix is then used in specifying the element and attribute names to be constructed by the FOR XML query.  
  
```  
WITH XMLNAMESPACES ('uri' as ns1)  
SELECT ProductID as 'ns1:ProductID',  
       Name      as 'ns1:Name',   
       Color     as 'ns1:Color'  
FROM Production.Product  
WHERE ProductID=316 or ProductID=317  
FOR XML RAW ('ns1:Prod'), ELEMENTS  
  
```  
  
 The XML result includes the namespace prefixes:  
  
```  
<ns1:Prod xmlns:ns1="uri">  
  <ns1:ProductID>316</ns1:ProductID>  
  <ns1:Name>Blade</ns1:Name>  
</ns1:Prod>  
<ns1:Prod xmlns:ns1="uri">  
  <ns1:ProductID>317</ns1:ProductID>  
  <ns1:Name>LL Crankarm</ns1:Name>  
  <ns1:Color>Black</ns1:Color>  
</ns1:Prod>  
  
```  
  
 The following applies to the WITH XMLNAMESPACES clause:  
  
-   It is supported only on the RAW, AUTO, and PATH modes of the FOR XML queries. The EXPLICIT mode is not supported.  
  
-   It only affects the namespace prefixes of FOR XML queries and the **xml** data type methods, but not the XML parser. For example, the following query returns an error, because the XML document has no namespace declaration for the myNS prefix.  
  
-   The FOR XML directives, XMLSCHEMA and XMLDATA cannot be used when a WITH XMLNAMESPACES clause is being used.  
  
    ```  
    CREATE TABLE T (x xml)  
    go  
    WITH XMLNAMESPACES ('https://abc' as myNS )  
    INSERT INTO T VALUES('<myNS:root/>')  
    ```  
  
## Using the XSINIL Directive  
 You cannot define the xsi prefix in the WITH XMLNAMESPACES clause if you are using the ELEMENTS XSINIL directive. Instead, it is added automatically when you use ELEMENTS XSINIL. The following query uses ELEMENTS XSINIL that generates element-centric XML where null values are mapped to elements that have the **xsi:nil** attribute set to True.  
  
```  
WITH XMLNAMESPACES ('uri' as ns1)  
SELECT ProductID as 'ns1:ProductID',  
       Name      as 'ns1:Name',   
       Color     as 'ns1:Color'  
FROM Production.Product  
WHERE ProductID=316   
FOR XML RAW, ELEMENTS XSINIL  
```  
  
 This is the result:  
  
```  
<row xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:ns1="uri">  
  <ns1:ProductID>316</ns1:ProductID>  
  <ns1:Name>Blade</ns1:Name>  
  <ns1:Color xsi:nil="true" />  
</row>  
```  
  
## Specifying Default Namespaces  
 Instead of declaring a namespace prefix, you can declare a default namespace by using a DEFAULT keyword. In the FOR XML query, it will bind the default namespace to XML nodes in the resulting XML. In the following example, the WITH XMLNAMESPACES defines two namespace prefixes that are defined together with a default namespace.  
  
```  
WITH XMLNAMESPACES ('uri1' as ns1,   
                    'uri2' as ns2,  
                    DEFAULT 'uri2')  
SELECT ProductID,   
      Name,  
      Color  
FROM Production.Product   
WHERE ProductID=316 or ProductID=317  
FOR XML RAW ('ns1:Product'), ROOT('ns2:root'), ELEMENTS  
```  
  
 The FOR XML query generates element-centric XML. Note that the query uses both the namespace prefixes in naming nodes. In the SELECT clause, the ProductID, Name, and Color do not specify a name with any prefix. Therefore, the corresponding elements in the resulting XML belong to the default namespace.  
  
```  
<ns2:root xmlns="uri2" xmlns:ns2="uri2" xmlns:ns1="uri1">  
  <ns1:Product>  
    <ProductID>316</ProductID>  
    <Name>Blade</Name>  
  </ns1:Product>  
  <ns1:Product>  
    <ProductID>317</ProductID>  
    <Name>LL Crankarm</Name>  
    <Color>Black</Color>  
  </ns1:Product>  
</ns2:root>  
```  
  
 The following query is similar to the previous one, except that the FOR XML AUTO mode is specified.  
  
```  
WITH XMLNAMESPACES ('uri1' as ns1,  'uri2' as ns2,DEFAULT 'uri2')  
SELECT ProductID,   
      Name,  
      Color  
FROM Production.Product as "ns1:Product"  
WHERE ProductID=316 or ProductID=317  
FOR XML AUTO, ROOT('ns2:root'), ELEMENTS  
```  
  
## Using Predefined Namespaces  
 When you use predefined namespaces, except the xml namespace and the xsi namespace when ELEMENTS XSINIL is used, you must explicitly specify the namespace binding by using WITH XMLNAMESPACES. The following query explicitly defines the namespace prefix to URI binding for the predefined namespace (`urn:schemas-microsoft-com:xml-sql`).  
  
```  
WITH XMLNAMESPACES ('urn:schemas-microsoft-com:xml-sql' as sql)  
SELECT 'SELECT * FROM Customers FOR XML AUTO, ROOT("a")' AS "sql:query"  
FOR XML PATH('sql:root')  
```  
  
 This is the result. SQLXML users are familiar with this XML template. For more information, see [SQLXML 4.0 Programming Concepts](../../relational-databases/sqlxml/sqlxml-4-0-programming-concepts.md).  
  
```  
<sql:root xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <sql:query>SELECT * FROM Customers FOR XML AUTO, ROOT("a")</sql:query>  
</sql:root>  
```  
  
 Only the xml namespace prefix can be used without explicitly defining it in WITH XMLNAMESPACES, as shown in the following PATH mode query. Also, if the prefix is declared, it has to be bound to the namespace http://www.w3.org/XML/1998/namespace. The names specified in the SELECT clause refer to the xml namespace prefix that is not explicitly defined by using WITH XMLNAMESPACES.  
  
```  
SELECT 'en'    as "English/@xml:lang",  
       'food'  as "English",  
       'ger'   as "German/@xml:lang",  
       'Essen' as "German"  
FOR XML PATH ('Translation')  
go  
```  
  
 The @xml:lang attributes use the predefined xml namespace. Because XML version 1.0 does not require the explicit declaration of the xml namespace binding, the result will not include an explicit declaration of the namespace binding.  
  
 This is the result:  
  
```  
<Translation>  
  <English xml:lang="en">food</English>  
  <German xml:lang="ger">Essen</German>  
</Translation>  
```  
  
## Using WITH XMLNAMESPACES with the xml Data Type Methods  
 The [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md) specified in a SELECT query, or in UPDATE when it is the **modify()** method, all have to repeat the namespace declaration in their prolog. This can be time-consuming. For example, the following query retrieves product model IDs whose catalog descriptions do include specification. That is, the <`Specifications`> element exists.  
  
```  
SELECT ProductModelID, CatalogDescription.query('  
declare namespace pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
    <Product   
        ProductModelID= "{ sql:column("ProductModelID") }"   
        />  
') AS Result  
FROM Production.ProductModel  
WHERE CatalogDescription.exist('  
    declare namespace  pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
     /pd:ProductDescription[(pd:Specifications)]'  
    ) = 1  
```  
  
 In the previous query, both the **query()** and **exist()** methods declare the same namespace in their prolog. For example:  
  
```  
declare namespace pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
```  
  
 Alternatively, you can declare WITH XMLNAMESPACES first and use the namespace prefixes in the query. In this case, the **query()** and **exist()** methods  do not have to include namespace declarations in their prolog.  
  
```  
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' as pd)  
SELECT ProductModelID, CatalogDescription.query('  
    <Product   
        ProductModelID= "{ sql:column("ProductModelID") }"   
        />  
') AS Result  
FROM Production.ProductModel  
WHERE CatalogDescription.exist('  
     /pd:ProductDescription[(pd:Specifications)]'  
    ) = 1  
Go  
```  
  
 Note that an explicit declaration in the XQuery prolog overrides the namespace prefix and the default element namespace that are defined in the WITH clause.  
  
## See Also  
 [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)   
 [XQuery Language Reference &#40;SQL Server&#41;](../../xquery/xquery-language-reference-sql-server.md)   
 [WITH XMLNAMESPACES &#40;Transact-SQL&#41;](../../t-sql/xml/with-xmlnamespaces.md)   
 [FOR XML &#40;SQL Server&#41;](../../relational-databases/xml/for-xml-sql-server.md)  
  
  
