---
title: "Examples: Using PATH Mode | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "PATH FOR XML mode, examples"
ms.assetid: 3564e13b-9b97-49ef-8cf9-6a78677b09a3
author: MightyPen
ms.author: genemi
manager: craigg
---
# Examples: Using PATH Mode
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  The following examples illustrate the use of PATH mode in generating XML from a SELECT query. Many of these queries are specified against the bicycle manufacturing instructions XML documents that are stored in the Instructions column of the ProductModel table.  
  
## Specifying a simple PATH mode query  
 This query specifies a FOR XML PATH mode.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT   
       ProductModelID,  
       Name  
FROM Production.ProductModel  
WHERE ProductModelID=122 OR ProductModelID=119  
FOR XML PATH;  
GO  
```  
  
 The following result is element-centric XML where each column value in the resulting rowset is wrapped in an element. Because the `SELECT` clause does not specify any aliases for the column names, the child element names generated are the same as the corresponding column names in the `SELECT` clause. For each row in the rowset a <`row`> tag is added.  
  
 ```
<row>  
  <ProductModelID>122</ProductModelID>  
  <Name>All-Purpose Bike Stand</Name>  
</row>  
<row>  
  <ProductModelID>119</ProductModelID>
  <Name>Bike Wash</Name>
</row>
```
  
 The following result is the same as the `RAW` mode query with the `ELEMENTS` option specified. It returns element-centric XML with a default <`row`> element for each row in the result set.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT ProductModelID,  
       Name  
FROM Production.ProductModel  
WHERE ProductModelID=122 OR ProductModelID=119  
FOR XML RAW, ELEMENTS;  
```  
  
 You can optionally specify the row element name to overwrite the default <`row`>. For example, the following query returns the <`ProductModel`> element for each row in the rowset.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT ProductModelID,  
       Name  
FROM Production.ProductModel  
WHERE ProductModelID=122 or ProductModelID=119  
FOR XML PATH ('ProductModel');  
GO  
```  
  
 The resulting XML will have a specified row element name.  
  
```
<ProductModel>
  <ProductModelID>122</ProductModelID>
  <Name>All-Purpose Bike Stand</Name>
</ProductModel>
<ProductModel>
  <ProductModelID>119</ProductModelID>
  <Name>Bike Wash</Name>
</ProductModel>
```
  
 If you specify a zero-length string, the wrapping element is not produced.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT ProductModelID,  
       Name  
FROM Production.ProductModel  
WHERE ProductModelID=122 OR ProductModelID=119  
FOR XML PATH ('');  
GO  
```  
  
 This is the result:  
  
```
<ProductModelID>122</ProductModelID>
<Name>All-Purpose Bike Stand</Name>
<ProductModelID>119</ProductModelID>
<Name>Bike Wash</Name>
```
  
## Specifying XPath-like column names  
 In the following query the `ProductModelID` column name specified starts with '\@' and does not contain a slash mark ('/'). Therefore, an attribute of the <`row`> element that has the corresponding column value is created in the resulting XML.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT ProductModelID AS "@id",  
       Name  
FROM Production.ProductModel  
WHERE ProductModelID=122 OR ProductModelID=119  
FOR XML PATH ('ProductModelData');  
GO  
```  
  
 This is the result:  
  
```
<ProductModelData id="122">
  <Name>All-Purpose Bike Stand</Name>
</ProductModelData>
<ProductModelData id="119">
  <Name>Bike Wash</Name>
</ProductModelData>
```
  
 You can add a single top-level element by specifying the `root` option in `FOR XML`.  
  
```  
SELECT ProductModelID AS "@id",  
       Name  
FROM Production.ProductModel  
WHERE ProductModelID=122 or ProductModelID=119  
FOR XML PATH ('ProductModelData'), root ('Root');  
GO  
```  
  
 To generate a hierarchy, you can include PATH-like syntax. For example, change the column name for the `Name` column to "SomeChild/ModelName" and you will obtain XML with hierarchy, as shown in this result:  
  
```
<Root>
  <ProductModelData id="122">
    <SomeChild>
      <ModelName>All-Purpose Bike Stand</ModelName>
    </SomeChild>
  </ProductModelData>
  <ProductModelData id="119">
    <SomeChild>
      <ModelName>Bike Wash</ModelName>
    </SomeChild>
  </ProductModelData>
</Root>
```
  
 Besides the product model ID and name, the following query retrieves the manufacturing instruction locations for the product model. Because the Instructions column is of **xml** type, the **query()** method of **xml** data type is specified to retrieve the location.  
  
```  
SELECT ProductModelID AS "@id",  
       Name,  
       Instructions.query('declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
                /MI:root/MI:Location   
              ') AS ManuInstr  
FROM Production.ProductModel  
WHERE ProductModelID = 7  
FOR XML PATH ('ProductModelData'), root ('Root');  
GO  
```  
  
 This is the partial result. Because the query specifies ManuInstr as the column name, the XML returned by the **query()** method is wrapped in a <`ManuInstr`> tag as shown in the following:  

```
<Root>
  <ProductModelData id="7">
    <Name>HL Touring Frame</Name>
    <ManuInstr>
      <MI:Location xmlns:MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions"
        <MI:step>...</MI:step>...
      </MI:Location>
      ...
    </ManuInstr>
  </ProductModelData>
</Root> 
```

In the previous FOR XML query, you may want to include namespaces for the <`Root`> and <`ProductModelData`> elements. You can do this by first defining the prefix to namespace binding by using WITH XMLNAMESPACES and using prefixes in the FOR XML query. For more information, see [Add Namespaces to Queries with WITH XMLNAMESPACES](../../relational-databases/xml/add-namespaces-to-queries-with-with-xmlnamespaces.md).  
  
```  
USE AdventureWorks2012;  
GO  
WITH XMLNAMESPACES (  
   'uri1' AS ns1,    
   'uri2' AS ns2,  
   'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions' as MI)  
SELECT ProductModelID AS "ns1:ProductModelID",  
       Name           AS "ns1:Name",  
       Instructions.query('  
                /MI:root/MI:Location   
              ')   
FROM Production.ProductModel  
WHERE ProductModelID=7  
FOR XML PATH ('ns2:ProductInfo'), root('ns1:root');  
GO  
```  
  
 Note that the `MI` prefix is also defined in the `WITH XMLNAMESPACES`. As a result, the **query()** method of the **xml** type specified does not define the prefix in the query prolog. This is the result:  
  
```
<ns1:root xmlns:MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions" xmlns="uri2" xmlns:ns2="uri2" xmlns:ns1="uri1">
  <ns2:ProductInfo>
    <ns1:ProductModelID>7</ns1:ProductModelID>
    <ns1:Name>HL Touring Frame</ns1:Name>
    <MI:Location xmlns:MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions" LaborHours="2.5" LotSize="100" MachineHours="3" SetupHours="0.5" LocationID="10" xmlns="">
    <MI:step>
      Insert <MI:material>aluminum sheet MS-2341</MI:material> into the <MI:tool>T-85A framing tool</MI:tool>.
    </MI:step>
    ...
    </MI:Location>
    ...
  </ns2:ProductInfo>
</ns1:root>
```
  
## Generating a value list using PATH mode  
 For each product model, this query constructs a value list of product IDs. For each product ID, the query also constructs <`ProductName`> nested elements, as shown in this XML fragment:  

```
<ProductModelData ProductModelID="7" ProductModelName="..." ProductIDs="product id list in the product model">
  <ProductName>...</ProductName>
  <ProductName>...</ProductName>
  ...
</ProductModelData>
```
  
 This is the query that produces the XML you want:  
  
```  
USE AdventureWorks2012;  
GO  
SELECT ProductModelID     AS "@ProductModelID",  
       Name               AS "@ProductModelName",  
      (SELECT ProductID AS "data()"  
       FROM   Production.Product  
       WHERE  Production.Product.ProductModelID =   
              Production.ProductModel.ProductModelID  
       FOR XML PATH ('')) AS "@ProductIDs",  
       (SELECT Name AS "ProductName"  
       FROM   Production.Product  
       WHERE  Production.Product.ProductModelID =   
              Production.ProductModel.ProductModelID  
        FOR XML PATH ('')) AS "ProductNames"  
FROM   Production.ProductModel  
WHERE  ProductModelID= 7 or ProductModelID=9  
FOR XML PATH('ProductModelData');  
```  
  
 Note the following from the previous query:  
  
-   The first nested `SELECT` returns a list of ProductIDs by using `data()` as the column name. Because the query specifies an empty string as the row element name in `FOR XML PATH`, no element is generated. Instead, the value list is assigned to the `ProductID` attribute.  
  
-   The second nested `SELECT` retrieves product names for products in the product model. It generates <`ProductName`> elements that are returned wrapped in the <`ProductNames`> element, because the query specifies `ProductNames` as the column name.  
  
 This is the partial result:  
  
```
<ProductModelData PId="7" ProductModelName="HL Touring Frame" ProductIDs="885 887 ...">
  <ProductNames>
    <ProductName>HL Touring Frame - Yellow, 60</ProductName>
    <ProductName>HL Touring Frame - Yellow, 46</ProductName>
  </ProductNames>
  ...
</ProductModelData>
<ProductModelData PId="9" ProductModelName="LL Road Frame" ProductIDs="722 723 724 ...">
  <ProductNames>
    <ProductName>LL Road Frame - Black, 58</ProductName>
    <ProductName>LL Road Frame - Black, 60</ProductName>
    <ProductName>LL Road Frame - Black, 62</ProductName>
    ...
  </ProductNames>
</ProductModelData>
```
  
 The subquery constructing the product names returns the result as a string that is entitized and then added to the XML. If you add the type directive, `FOR XML PATH (''), type`, the subquery returns the result as **xml** type and no entitization occurs.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT ProductModelID AS "@ProductModelID",  
      Name AS "@ProductModelName",  
      (SELECT ProductID AS "data()"  
       FROM   Production.Product  
       WHERE  Production.Product.ProductModelID =   
              Production.ProductModel.ProductModelID  
       FOR XML PATH ('')  
       ) AS "@ProductIDs",  
       (  
       SELECT Name AS "ProductName"  
       FROM   Production.Product  
       WHERE  Production.Product.ProductModelID =   
              Production.ProductModel.ProductModelID  
       FOR XML PATH (''), type  
       ) AS "ProductNames"  
  
FROM Production.ProductModel  
WHERE ProductModelID= 7 OR ProductModelID=9  
FOR XML PATH('ProductModelData');  
```  
  
## Adding namespaces in the resulting XML  
 As described in [Adding Namespaces Using WITH XMLNAMESPACES](../../relational-databases/xml/add-namespaces-to-queries-with-with-xmlnamespaces.md), you can use WITH XMLNAMESPACES to include namespaces in the PATH mode queries. For example, names specified in the SELECT clause include namespace prefixes. The following `PATH` mode query constructs XML with namespaces.  
  
```  
SELECT 'en'    as "English/@xml:lang",  
       'food'  as "English",  
       'ger'   as "German/@xml:lang",  
       'Essen' as "German"  
FOR XML PATH ('Translation')  
GO  
```  
  
 The `@xml:lang` attribute added to the <`English`> element is defined in the predefined xml namespace.  
  
 This is the result:  

```
<Translation>
  <English xml:lang="en">food</English>
  <German xml:lang="ger">Essen</German>
</Translation>
```
  
 The following query is similar to example C, except that it uses `WITH XMLNAMESPACES` to include namespaces in the XML result. For more information, see [Add Namespaces to Queries with WITH XMLNAMESPACES](../../relational-databases/xml/add-namespaces-to-queries-with-with-xmlnamespaces.md).  
  
```  
USE AdventureWorks2012;  
GO  
WITH XMLNAMESPACES ('uri1' AS ns1,  DEFAULT 'uri2')  
SELECT ProductModelID AS "@ns1:ProductModelID",  
      Name AS "@ns1:ProductModelName",  
      (SELECT ProductID AS "data()"  
       FROM   Production.Product  
       WHERE  Production.Product.ProductModelID =   
              Production.ProductModel.ProductModelID  
       FOR XML PATH ('')  
       ) AS "@ns1:ProductIDs",  
       (  
       SELECT ProductID AS "@ns1:ProductID",   
              Name AS "@ns1:ProductName"  
       FROM   Production.Product  
       WHERE  Production.Product.ProductModelID =   
              Production.ProductModel.ProductModelID  
       FOR XML PATH , type   
       ) AS "ns1:ProductNames"  
FROM Production.ProductModel  
WHERE ProductModelID= 7 OR ProductModelID=9  
FOR XML PATH('ProductModelData'), root('root');  
```  
  
 This is the result:  
  
```
<root xmlns="uri2" 
  xmlns:ns1="uri1">
  <ProductModelData ns1:ProductModelID="7" ns1:ProductModelName="HL Touring Frame" ns1:ProductIDs="885 887 888 889 890 891 892 893">
    <ns1:ProductNames>
      <row xmlns="uri2" xmlns:ns1="uri1" ns1:ProductID="885" ns1:ProductName="HL Touring Frame - Yellow, 60" />
      <row xmlns="uri2" xmlns:ns1="uri1" ns1:ProductID="887" ns1:ProductName="HL Touring Frame - Yellow, 46" />
      ...
    </ns1:ProductNames>
  </ProductModelData>
  <ProductModelData ns1:ProductModelID="9" ns1:ProductModelName="LL Road Frame" ns1:ProductIDs="722 723 724 725 726 727 728 729 730 736 737 738">
    <ns1:ProductNames>
      <row xmlns="uri2" xmlns:ns1="uri1" ns1:ProductID="722" ns1:ProductName="LL Road Frame - Black, 58" />
      ...
    </ns1:ProductNames>
  </ProductModelData>
</root>
```
  
## See Also  
 [Use PATH Mode with FOR XML](../../relational-databases/xml/use-path-mode-with-for-xml.md)  
  
  
