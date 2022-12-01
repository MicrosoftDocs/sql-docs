---
title: "sql:column() Function (XQuery) | Microsoft Docs"
description: Learn how the XQuery function sql:column() can be used to bind non-XML relational data inside XML and bring relational and XML data together.
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "sql:column function"
  - "sql:column() function"
ms.assetid: e8f67bdf-b489-49a9-9d0f-2069c1750467
author: "rothja"
ms.author: "jroth"
---
# XQuery Extension Functions - sql:column()
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  As described in the topic, [Binding Relational Data Inside XML](../t-sql/xml/binding-relational-data-inside-xml-data.md), you can use the **sql:column(()** function when you use [XML Data Type Methods](../t-sql/xml/xml-data-type-methods.md) to expose a relational value inside XQuery.  
  
 For example, the [query() method (XML data type)](../t-sql/xml/query-method-xml-data-type.md) is used to specify a query against an XML instance that is stored in a variable or column of **xml** type. Sometimes, you may also want your query to use values from another, non-XML column, to bring relational and XML data together. To do this, you use the **sql:column()** function.  
  
 The SQL value will be mapped to a corresponding XQuery value and its type  will be an XQuery base type that is equivalent to the corresponding SQL type.  
  
## Syntax  
  
```  
  
sql:column("columnName")  
```  
  
## Remarks  
 Note that reference to a column specified in the **sql:column()** function inside an XQuery refers to a column in the row that is being processed.  
  
 In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you can only refer to an **xml** instance in the context of the source expression of an XML-DML insert statement; otherwise, you cannot refer to columns that are of type **xml** or a CLR user-defined type.  
  
 The **sql:column()** function is not supported in JOIN operations. The APPLY operation can be used instead.  
  
## Examples  
  
### A. Using sql:column() to retrieve the relational value inside XML  
 In constructing XML, the following example illustrates how you can retrieve values from a non-XML relational column  to bind XML and relational data.  
  
 The query constructs XML that has the following form:  
  
```xml
<Product ProductID="771" ProductName="Mountain-100 Silver, 38" ProductPrice="3399.99" ProductModelID="19"   
  ProductModelName="Mountain 100" />  
```  
  
 Note the following in the constructed XML:  
  
-   The **ProductID**, **ProductName**,and **ProductPrice** attribute values are obtained from the **Product** table.  
  
-   The **ProductModelID** attribute value is retrieved from the **ProductModel** table.  
  
-   To make the query more interesting, the **ProductModelName** attribute value is obtained from the **CatalogDescription** column of **xml type**. Because the XML product model catalog information is not stored for all the product models, the `if` statement is used to retrieve the value only if it exists.  
  
    ```sql
    SELECT P.ProductID, CatalogDescription.query('  
    declare namespace pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
           <Product   
               ProductID=       "{ sql:column("P.ProductID") }"  
               ProductName=     "{ sql:column("P.Name") }"  
               ProductPrice=    "{ sql:column("P.ListPrice") }"  
               ProductModelID= "{ sql:column("PM.ProductModelID") }" >  
               { if (not(empty(/pd:ProductDescription))) then  
                 attribute ProductModelName { /pd:ProductDescription[1]/@ProductModelName }  
                else   
                   ()  
    }  
            </Product>  
    ') as Result  
    FROM Production.ProductModel PM, Production.Product P  
    WHERE PM.ProductModelID = P.ProductModelID  
    AND   CatalogDescription is not NULL  
    ORDER By PM.ProductModelID  
    ```  
  
 Note the following from the previous query:  
  
-   Because the values are retrieved from two different tables, the FROM clause specifies two tables. The condition in the WHERE clause filters the result and retrieves only products whose product models have catalog descriptions.  
  
-   The **namespace** keyword in the [XQuery Prolog](../xquery/modules-and-prologs-xquery-prolog.md) defines the XML namespace prefix, "pd", that is used in the query body. Note that the table aliases, "P" and "PM", are defined in the FROM clause of the query itself.  
  
-   The **sql:column()** function is used to bring non-XML values inside XML.  
  
 This is the partial result:  
  
```  
ProductID               Result  
-----------------------------------------------------------------  
771         <Product ProductID="771"                   ProductName="Mountain-100 Silver, 38"   
                  ProductPrice="3399.99" ProductModelID="19"   
                  ProductModelName="Mountain 100" />  
...  
```  
  
 The following query constructs XML that contains product-specific information. This information includes the ProductID, ProductName, ProductPrice, and, if available, the ProductModelName for all products that belong to a specific product model, ProductModelID=19. The XML is then assigned to the @x variable of **xml** type.  
  
```sql
declare @x xml  
SELECT @x = CatalogDescription.query('  
declare namespace pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
       <Product   
           ProductID=       "{ sql:column("P.ProductID") }"  
           ProductName=     "{ sql:column("P.Name") }"  
           ProductPrice=    "{ sql:column("P.ListPrice") }"  
           ProductModelID= "{ sql:column("PM.ProductModelID") }" >  
           { if (not(empty(/pd:ProductDescription))) then  
             attribute ProductModelName { /pd:ProductDescription[1]/@ProductModelName }  
            else   
               ()  
}  
        </Product>  
')   
FROM Production.ProductModel PM, Production.Product P  
WHERE PM.ProductModelID = P.ProductModelID  
And P.ProductModelID = 19  
select @x  
```  
  
## See Also  
 [SQL Server XQuery Extension Functions]()   
 [Compare Typed XML to Untyped XML](../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [XML Data &#40;SQL Server&#41;](../relational-databases/xml/xml-data-sql-server.md)   
 [Create Instances of XML Data](../relational-databases/xml/create-instances-of-xml-data.md)   
 [xml Data Type Methods](../t-sql/xml/xml-data-type-methods.md)   
 [XML Data Modification Language &#40;XML DML&#41;](../t-sql/xml/xml-data-modification-language-xml-dml.md)  
  
