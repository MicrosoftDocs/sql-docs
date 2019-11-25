---
title: "Use AUTO Mode with FOR XML | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "FOR XML clause, AUTO mode"
  - "ELEMENTS option"
  - "FOR XML AUTO mode"
  - "AUTO FOR XML mode"
ms.assetid: 7140d656-1d42-4f01-a533-5251429f4450
author: MightyPen
ms.author: genemi
manager: craigg
---
# Use AUTO Mode with FOR XML
  As described in [FOR XML &#40;SQL Server&#41;](for-xml-sql-server.md), AUTO mode returns query results as nested XML elements. This does not provide much control over the shape of the XML generated from a query result. The AUTO mode queries are useful if you want to generate simple hierarchies. However, [Use EXPLICIT Mode with FOR XML](use-explicit-mode-with-for-xml.md) and [Use PATH Mode with FOR XML](use-path-mode-with-for-xml.md) provide more control and flexibility in deciding the shape of the XML from a query result.  
  
 Each table in the FROM clause, from which at least one column is listed in the SELECT clause, is represented as an XML element. The columns listed in the SELECT clause are mapped to attributes or subelements, if the optional ELEMENTS option is specified in the FOR XML clause.  
  
 The XML hierarchy, nesting of the elements, in the resulting XML is based on the order of tables identified by the columns specified in the SELECT clause. Therefore, the order in which column names are specified in the SELECT clause is significant. The first, leftmost table that is identified forms the top element in the resulting XML document. The second leftmost table, identified by columns in the SELECT statement, forms a subelement within the top element, and so on.  
  
 If a column name listed in the SELECT clause is from a table that is already identified by a previously specified column in the SELECT clause, the column is added as an attribute of the element already created, instead of opening a new level of hierarchy. If the ELEMENTS option is specified, the column is added as an attribute.  
  
 For example, execute this query:  
  
```  
SELECT Cust.CustomerID,   
       OrderHeader.CustomerID,  
       OrderHeader.SalesOrderID,   
       OrderHeader.Status,  
       Cust.CustomerType  
FROM Sales.Customer Cust, Sales.SalesOrderHeader OrderHeader  
WHERE Cust.CustomerID = OrderHeader.CustomerID  
ORDER BY Cust.CustomerID  
FOR XML AUTO  
```  
  
 This is the partial result:  
  
```  
<Cust CustomerID="1" CustomerType="S">  
  <OrderHeader CustomerID="1" SalesOrderID="43860" Status="5" />  
  <OrderHeader CustomerID="1" SalesOrderID="44501" Status="5" />  
  <OrderHeader CustomerID="1" SalesOrderID="45283" Status="5" />  
  <OrderHeader CustomerID="1" SalesOrderID="46042" Status="5" />  
</Cust>  
...  
```  
  
 Note the following in the SELECT clause:  
  
-   The CustomerID references the Cust table. Therefore, a <`Cust`> element is created and CustomerID is added as its attribute.  
  
-   Next, three columns, OrderHeader.CustomerID, OrderHeader.SaleOrderID, and OrderHeader.Status, reference the OrderHeader table. Therefore, an <`OrderHeader`> element is added as a subelement of the <`Cust`> element and the three columns are added as attributes of <`OrderHeader`>.  
  
-   Next, the Cust.CustomerType column again references the Cust table that was already identified by the Cust.CustomerID column. Therefore, no new element is created. Instead, the CustomerType attribute is added to the <`Cust`> element that was previously created.  
  
-   The query specifies aliases for the table names. These aliases appear as corresponding element names.  
  
-   ORDER BY is required to group all children under one parent.  
  
 This query is similar to the previous one, except the SELECT clause specifies columns in the OrderHeader table before the columns in the Cust table. Therefore, first <`OrderHeader`> element is created and then the <`Cust`> child element is added to it.  
  
```  
select OrderHeader.CustomerID,  
       OrderHeader.SalesOrderID,   
       OrderHeader.Status,  
       Cust.CustomerID,   
       Cust.CustomerType  
from Sales.Customer Cust, Sales.SalesOrderHeader OrderHeader  
where Cust.CustomerID = OrderHeader.CustomerID  
for xml auto  
```  
  
 This is the partial result:  
  
```  
<OrderHeader CustomerID="1" SalesOrderID="43860" Status="5">  
  <Cust CustomerID="1" CustomerType="S" />  
</OrderHeader>  
...  
```  
  
 If the ELEMENTS option is added in the FOR XML clause, element-centric XML is returned.  
  
```  
SELECT Cust.CustomerID,   
       OrderHeader.CustomerID,  
       OrderHeader.SalesOrderID,   
       OrderHeader.Status,  
       Cust.CustomerType  
FROM Sales.Customer Cust, Sales.SalesOrderHeader OrderHeader  
WHERE Cust.CustomerID = OrderHeader.CustomerID  
ORDER BY Cust.CustomerID  
FOR XML AUTO, ELEMENTS  
```  
  
 This is the partial result:  
  
```  
<Cust>  
  <CustomerID>1</CustomerID>  
  <CustomerType>S</CustomerType>  
  <OrderHeader>  
    <CustomerID>1</CustomerID>  
    <SalesOrderID>43860</SalesOrderID>  
    <Status>5</Status>  
  </OrderHeader>  
   ...  
</Cust>  
...  
```  
  
 In this query, the CustomerID values are compared from one row to the next in creating the \<Cust> elements, because CustomerID is the primary key of the table. If CustomerID is not identified as the primary key for the table, all the column values (CustomerID, CustomerType in this query) are compared from one row to the next. If the values differ, a new \<Cust> element is added to the XML.  
  
 When comparing these column values, if any of the columns to be compared are of type **text**, **ntext**, **image**, or **xml**, FOR XML assumes that the values are different and not compared, even though they may be the same. This is because comparing large objects is not supported. Elements are added to the result for each row selected. Note that columns of **(n)varchar(max)** and **varbinary(max)** are compared.  
  
 When a column in the SELECT clause cannot be associated with any of the tables identified in the FROM clause, as in the case of an aggregate column or computed column, the column is added in the XML document in the deepest nesting level in place when it is encountered in the list. If such a column appears as the first column in the SELECT clause, the column is added to the top element.  
  
 If the asterisk (*) wildcard character is specified in the SELECT clause, the nesting is determined in the same way as previously described, based on the order that the rows are returned by the query engine.  
  
## In This Section  
 The following topics provide more information about AUTO mode:  
  
-   [Use the BINARY BASE64 Option](use-the-binary-base64-option.md)  
  
-   [AUTO Mode Heuristics in Shaping Returned XML](auto-mode-heuristics-in-shaping-returned-xml.md)  
  
-   [Examples: Using AUTO Mode](examples-using-auto-mode.md)  
  
## See Also  
 [SELECT &#40;Transact-SQL&#41;](/sql/t-sql/queries/select-transact-sql)   
 [FOR XML &#40;SQL Server&#41;](for-xml-sql-server.md)  
  
  
