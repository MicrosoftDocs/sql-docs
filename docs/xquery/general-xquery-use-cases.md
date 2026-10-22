---
title: "General XQuery Use Cases"
description: View several examples of general XQuery use including searching, retrieving, and listing specific data from a product catalog.
author: uc-msft
ms.author: umajay
ms.reviewer: vanto
ms.date: 09/26/2025
ms.service: sql
ms.subservice: xml
ms.topic: reference
helpviewer_keywords:
  - "XQuery, general usage cases"
dev_langs:
  - "XML"
---
# General XQuery use cases

[!INCLUDE [SQL Server Azure SQL Database](../includes/applies-to-version/sqlserver.md)]

This article provides general examples of XQuery use.

## Examples

### A. Query catalog descriptions to find products and weights

The following query returns the product model IDs and weights, if they exist, from the product catalog description. The query constructs XML that has the following form:

```xml
<Product ProductModelID="...">
  <Weight>...</Weight>
</Product>
```

Here's the query:

```sql
SELECT CatalogDescription.query('
declare namespace p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";
  <Product  ProductModelID="{ (/p1:ProductDescription/@ProductModelID)[1] }">
     {
       /p1:ProductDescription/p1:Specifications/Weight
     }
  </Product>
') AS Result
FROM Production.ProductModel
WHERE CatalogDescription IS NOT NULL;
```

Note the following considerations from the previous query:

- The `namespace` keyword in the XQuery prolog defines a namespace prefix that is used in the query body.

- The query body constructs the required XML.

- In the `WHERE` clause, the `exist()` method is used to find only rows that contain product catalog descriptions. That is, the XML that contains the `<ProductDescription>` element.

Here's the result:

```xml
<Product ProductModelID="19"/>
<Product ProductModelID="23"/>
<Product ProductModelID="25"/>
<Product ProductModelID="28"><Weight>Varies with size.</Weight></Product>
<Product ProductModelID="34"/>
<Product ProductModelID="35"/>
```

The following query retrieves the same information, but only for those product models whose catalog description includes the weight, the `<Weight>` element, in the specifications, the `<Specifications>` element. This example uses `WITH XMLNAMESPACES` to declare the `pd` prefix and its namespace binding. In this way, the binding isn't described in both the `query()` method and in the `exist()` method.

```sql
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS pd)
SELECT CatalogDescription.query('
          <Product  ProductModelID="{ (/pd:ProductDescription/@ProductModelID)[1] }">
                 {
                      /pd:ProductDescription/pd:Specifications/Weight
                 }
          </Product>
') AS x
FROM Production.ProductModel
WHERE CatalogDescription.exist('/pd:ProductDescription/pd:Specifications//Weight ') = 1;
```

In the previous query, the `exist()` method of the **xml** data type in the `WHERE` clause checks to see if there's a `<Weight>` element in the `<Specifications>` element.

### B. Find product model IDs for product models whose catalog descriptions include front-angle and small size pictures

The XML product catalog description includes the product pictures, the `<Picture>` element. Each picture has several properties, including the picture angle (the `<Angle>` element), and the size (the `<Size>` element).

For product models whose catalog descriptions include front-angle and small-size pictures, the query constructs XML that has the following form:

```xml
< Product ProductModelID="...">
  <Picture>
    <Angle>front</Angle>
    <Size>small</Size>
  </Picture>
</Product>
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS pd)
SELECT CatalogDescription.query('
   <pd:Product  ProductModelID="{ (/pd:ProductDescription/@ProductModelID)[1] }">
      <Picture>
         {  /pd:ProductDescription/pd:Picture/pd:Angle }
         {  /pd:ProductDescription/pd:Picture/pd:Size }
      </Picture>
   </pd:Product>
') as Result
FROM  Production.ProductModel
WHERE CatalogDescription.exist('/pd:ProductDescription/pd:Picture') = 1
AND   CatalogDescription.value('(/pd:ProductDescription/pd:Picture/pd:Angle)[1]', 'varchar(20)')  = 'front'
AND   CatalogDescription.value('(/pd:ProductDescription/pd:Picture/pd:Size)[1]', 'varchar(20)')  = 'small'
```

Note the following considerations from the previous query:

- In the `WHERE` clause, the `exist()` method is used to retrieve only rows that have product catalog descriptions with the `<Picture>` element.

- The `WHERE` clause uses the `value()` method two times to compare the values of the `<Size>` and `<Angle>` elements.

Here's a partial result:

```xml
<p1:Product
  xmlns:p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription"
  ProductModelID="19">
  <Picture>
    <p1:Angle>front</p1:Angle>
    <p1:Size>small</p1:Size>
  </Picture>
</p1:Product>
...
```

### C. Create a flat list of the product model name and feature pairs, with each pair enclosed in the \<Features> element

In the product model catalog description, the XML includes several product features. All these features are included in the `<Features>` element. The query uses [XML construction (XQuery)](xml-construction-xquery.md) to construct the required XML. The expression in the curly braces is replaced by the result.

```sql
SELECT CatalogDescription.query('
declare namespace p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";
  for $pd in /p1:ProductDescription,
   $f in $pd/p1:Features/*
  return
   <Feature>
     <ProductModelName> { data($pd/@ProductModelName) } </ProductModelName>
     { $f }
  </Feature>
') AS x
FROM Production.ProductModel
WHERE ProductModelID = 19;
```

Note the following considerations from the previous query:

- `$pd/p1:Features/*` returns only the element node children of `<Features>`, but `$pd/p1:Features/node()` returns all the nodes. This includes the element nodes, text nodes, processing instructions, and comments.

- The two `FOR` loops generate a Cartesian product from which the product name and the individual feature are returned.

- The `ProductName` is an attribute. The XML construction in this query returns it as an element.

Here's a partial result:

```xml
<Feature>
 <ProductModelName>Mountain 100</ProductModelName>
 <ProductModelID>19</ProductModelID>
 <p1:Warranty
   xmlns:p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain">
    <p1:WarrantyPeriod>3 year</p1:WarrantyPeriod>
    <p1:Description>parts and labor</p1:Description>
 </p1:Warranty>
</Feature>
<Feature>
 <ProductModelName>Mountain 100</ProductModelName>
 <ProductModelID>19</ProductModelID>
 <p2:Maintenance xmlns:p2="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain">
    <p2:NoOfYears>10</p2:NoOfYears>
    <p2:Description>maintenance contact available through your dealer
           or any AdventureWorks retail store.</p2:Description>
    </p2:Maintenance>
</Feature>
...
...
```

### D. From the catalog description of a product model, list the product model name, model ID, and features grouped inside a \<Product> element

Using the information stored in the catalog description of the product model, the following query lists the product model name, model ID, and features grouped inside a `<Product>` element.

```sql
SELECT ProductModelID,
       CatalogDescription.query('
     declare namespace pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";
     <Product>
         <ProductModelName>
           { data(/pd:ProductDescription/@ProductModelName) }
         </ProductModelName>
         <ProductModelID>
           { data(/pd:ProductDescription/@ProductModelID) }
         </ProductModelID>
         { /pd:ProductDescription/pd:Features/* }
     </Product>
') AS x
FROM Production.ProductModel
WHERE ProductModelID = 19;
```

Here's a partial result:

```xml
<Product>
  <ProductModelName>Mountain 100</ProductModelName>
  <ProductModelID>19</ProductModelID>
  <p1:Warranty>... </p1:Warranty>
  <p2:Maintenance>...  </p2:Maintenance>
  <p3:wheel xmlns:p3="https://www.adventure-works.com/schemas/OtherFeatures">High performance wheels.</p3:wheel>
  <p4:saddle xmlns:p4="https://www.adventure-works.com/schemas/OtherFeatures">
    <p5:i xmlns:p5="http://www.w3.org/1999/xhtml">Anatomic design</p5:i> and made from durable leather for a full-day of riding in comfort.</p4:saddle>
  <p6:pedal xmlns:p6="https://www.adventure-works.com/schemas/OtherFeatures">
    <p7:b xmlns:p7="http://www.w3.org/1999/xhtml">Top-of-the-line</p7:b> clipless pedals with adjustable tension.</p6:pedal>
   ...
```

### E. Retrieve product model feature descriptions

The following query constructs XML that includes a `<Product>` element that has `ProductModelID`, `ProductModelName` attributes, and the first two product features. Specifically, the first two product features are the first two child elements of the `<Features>` element. If there are more features, it returns an empty `<There-is-more/>` element.

```sql
SELECT CatalogDescription.query('
declare namespace pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";
     <Product>
          { /pd:ProductDescription/@ProductModelID }
          { /pd:ProductDescription/@ProductModelName }
          {
            for $f in /pd:ProductDescription/pd:Features/*[position()<=2]
            return
            $f
          }
          {
            if (count(/pd:ProductDescription/pd:Features/*) > 2)
            then <there-is-more/>
            else ()
          }
     </Product>
') AS Result
FROM Production.ProductModel
WHERE CatalogDescription IS NOT NULL;
```

Note the following considerations from the previous query:

- The `FOR` ... `RETURN` loop structure retrieves the first two product features. The `position()` function is used to find the position of the elements in the sequence.

### F. Find element names from the product catalog description that end with `ons`

The following query searches the catalog descriptions and returns all the elements in the `<ProductDescription>` element whose name ends with `ons`.

```sql
SELECT ProductModelID,
       CatalogDescription.query('
     declare namespace p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";
      for $pd in /p1:ProductDescription/*[substring(local-name(.),string-length(local-name(.))-2,3)="ons"]
      return
          <Root>
             { $pd }
          </Root>
') AS Result
FROM Production.ProductModel
WHERE CatalogDescription IS NOT NULL;
```

Here's a partial result:

```xml
ProductModelID   Result
-----------------------------------------
         19        <Root>
                     <p1:Specifications xmlns:p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription">
                          ...
                     </p1:Specifications>
                   </Root>
```

### G. Find summary descriptions that contain the word "Aerodynamic"

The following query retrieves product models whose catalog descriptions contain the word "Aerodynamic" in the summary description:

```sql
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS pd)
SELECT ProductModelID,
       CatalogDescription.query('
          <Prod >
             { /pd:ProductDescription/@ProductModelID }
             { /pd:ProductDescription/pd:Summary }
          </Prod>
 ') AS Result
FROM Production.ProductModel
WHERE CatalogDescription.value('
     contains( string( (/pd:ProductDescription/pd:Summary)[1] ),"Aerodynamic")', 'bit') = 1;
```

The `SELECT` query specifies `query()` and `value()` methods of the **xml** data type. Therefore, instead of repeating the namespaces declaration two times in two difference query prologs, the prefix `pd` is used in the query and is defined only once by using `WITH XMLNAMESPACES`.

Note the following considerations from the previous query:

- The `WHERE` clause is used to retrieve only the rows where the catalog description contains the word "Aerodynamic" in the `<Summary>` element.

- The `contains()` function is used to see if the word is included in the text.

- The `value()` method of the **xml** data type compares the value returned by `contains()` to 1.

Here's the result:

```xml
ProductModelID Result
-------------- ------------------------------------------
28     <Prod ProductModelID="28">
        <pd:Summary xmlns:pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription">
       <p1:p xmlns:p1="http://www.w3.org/1999/xhtml">
         A TRUE multi-sport bike that offers streamlined riding and a
         revolutionary design. Aerodynamic design lets you ride with the
         pros, and the gearing will conquer hilly roads.</p1:p>
       </pd:Summary>
      </Prod>
```

### H. Find product models whose catalog descriptions don't include product model pictures

The following query retrieves ProductModelIDs for product models whose catalog descriptions do no include a `<Picture>` element.

```sql
SELECT ProductModelID
FROM Production.ProductModel
WHERE CatalogDescription IS NOT NULL
      AND CatalogDescription.exist('declare namespace p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";
     /p1:ProductDescription/p1:Picture
') = 0;
```

Note the following considerations from the previous query:

- If the `exist()` method in the `WHERE` clause returns False (0), the product model ID is returned. Otherwise, it isn't returned.

- Because all the product descriptions include a `<Picture>` element, the result set is empty in this case.

## Related content

- [XQueries Involving Hierarchy](xqueries-involving-hierarchy.md)
- [XQueries Involving Order](xqueries-involving-order.md)
- [XQueries Handling Relational Data](xqueries-handling-relational-data.md)
- [String Search in XQuery](string-search-in-xquery.md)
- [Handling Namespaces in XQuery](handling-namespaces-in-xquery.md)
- [Add namespaces to queries using WITH XMLNAMESPACES](../relational-databases/xml/add-namespaces-to-queries-with-with-xmlnamespaces.md)
- [XML data (SQL Server)](../relational-databases/xml/xml-data-sql-server.md)
- [XQuery Language Reference (SQL Server)](xquery-language-reference-sql-server.md)
