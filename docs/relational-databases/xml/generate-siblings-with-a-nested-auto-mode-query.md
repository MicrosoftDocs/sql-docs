---
title: "Generate Siblings with a Nested AUTO Mode Query | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "queries [XML in SQL Server], nested AUTO mode"
  - "nested AUTO mode query"
ms.assetid: 748d9899-589d-4420-8048-1258e9e67c20
author: MightyPen
ms.author: genemi
manager: craigg
---
# Generate Siblings with a Nested AUTO Mode Query
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  The following example shows how to generate siblings by using a nested AUTO mode query. The only other way to generate such XML is to use the EXPLICIT mode. However, this can be cumbersome.  
  
## Example  
 This query constructs XML that provides sales order information. This includes the following:  
  
-   Sales order header information, `SalesOrderID`, `SalesPersonID`, and `OrderDate`. [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] stores this information in the `SalesOrderHeader` table.  
  
-   Sales order detail information. This includes one or more products ordered, the unit price, and the quantity ordered. This information is stored in the `SalesOrderDetail` table.  
  
-   Sales person information. This is the salesperson who took the order. The `SalesPerson` table provides the `SalesPersonID`. For this query, you have to join this table to the `Employee` table to find the name of the sales person.  
  
 The two distinct `SELECT` queries that follow generate XML with a small difference in shape.  
  
 The first query generates XML in which <`SalesPerson`> and <`SalesOrderHeader`> appear as sibling children of <`SalesOrder`>:  
  
```  
SELECT   
      (SELECT top 2 SalesOrderID, SalesPersonID, CustomerID,  
         (select top 3 SalesOrderID, ProductID, OrderQty, UnitPrice  
           from Sales.SalesOrderDetail  
            WHERE  SalesOrderDetail.SalesOrderID =   
                   SalesOrderHeader.SalesOrderID  
            FOR XML AUTO, TYPE)  
        FROM  Sales.SalesOrderHeader  
        WHERE SalesOrderHeader.SalesOrderID = SalesOrder.SalesOrderID  
        for xml auto, type),  
        (SELECT *   
         FROM  (SELECT SalesPersonID, EmployeeID  
              FROM Sales.SalesPerson, HumanResources.Employee  
              WHERE SalesPerson.SalesPersonID = Employee.EmployeeID) As   
                     SalesPerson  
         WHERE  SalesPerson.SalesPersonID = SalesOrder.SalesPersonID  
       FOR XML AUTO, TYPE)  
FROM (SELECT SalesOrderHeader.SalesOrderID, SalesOrderHeader.SalesPersonID  
      FROM Sales.SalesOrderHeader, Sales.SalesPerson  
      WHERE SalesOrderHeader.SalesPersonID = SalesPerson.SalesPersonID  
     ) as SalesOrder  
ORDER BY SalesOrder.SalesOrderID  
FOR XML AUTO, TYPE  
```  
  
 In the previous query, the outermost `SELECT` statement does the following:  
  
-   Queries the rowset, `SalesOrder`, specified in the `FROM` clause. The result is an XML with one or more <`SalesOrder`> elements.  
  
-   Specifies `AUTO` mode and the `TYPE` directive. `AUTO` mode transforms the query result into XML, and the `TYPE` directive returns the result as **xml** type.  
  
-   Includes two nested `SELECT` statements separated by a comma. The first nested `SELECT` retrieves sales order information, header and details, and the second nested `SELECT` statement retrieves salesperson information.  
  
    -   The `SELECT` statement that retrieves `SalesOrderID`, `SalesPersonID`, and `CustomerID` itself includes another nested `SELECT ... FOR XML` statement (with `AUTO` mode and `TYPE` directive) that returns sales order detail information.  
  
 The `SELECT` statement that retrieves the sales person information queries a rowset, `SalesPerson`, created in the `FROM` clause. For `FOR XML` queries to work, you must provide a name for the anonymous rowset generated in the `FROM` clause. In this case, the name provided is `SalesPerson`.  
  
 This is the partial result:  
  
```  
<SalesOrder>  
  <Sales.SalesOrderHeader SalesOrderID="43659" SalesPersonID="279" CustomerID="676">  
    <Sales.SalesOrderDetail SalesOrderID="43659" ProductID="776" OrderQty="1" UnitPrice="2024.9940" />  
    <Sales.SalesOrderDetail SalesOrderID="43659" ProductID="777" OrderQty="3" UnitPrice="2024.9940" />  
    <Sales.SalesOrderDetail SalesOrderID="43659" ProductID="778" OrderQty="1" UnitPrice="2024.9940" />  
  </Sales.SalesOrderHeader>  
  <SalesPerson SalesPersonID="279" EmployeeID="279" />  
</SalesOrder>  
...  
```  
  
 The following query generates the same sales order information, except that in the resulting XML, the <`SalesPerson`> appears as a sibling of <`SalesOrderDetail`>:  
  
```  
<SalesOrder>  
    <SalesOrderHeader ...>  
          <SalesOrderDetail .../>  
          <SalesOrderDetail .../>  
          ...  
          <SalesPerson .../>  
    </SalesOrderHeader>  
  
</SalesOrder>  
<SalesOrder>  
  ...  
</SalesOrder>  
```  
  
 This is the query:  
  
```  
SELECT SalesOrderID, SalesPersonID, CustomerID,  
             (select top 3 SalesOrderID, ProductID, OrderQty, UnitPrice  
              from Sales.SalesOrderDetail  
              WHERE SalesOrderDetail.SalesOrderID = SalesOrderHeader.SalesOrderID  
              FOR XML AUTO, TYPE),  
              (SELECT *   
               FROM  (SELECT SalesPersonID, EmployeeID  
                    FROM Sales.SalesPerson, HumanResources.Employee  
                    WHERE SalesPerson.SalesPersonID = Employee.EmployeeID) As SalesPerson  
               WHERE  SalesPerson.SalesPersonID = SalesOrderHeader.SalesPersonID  
         FOR XML AUTO, TYPE)  
FROM Sales.SalesOrderHeader  
WHERE SalesOrderID=43659 or SalesOrderID=43660  
FOR XML AUTO, TYPE  
```  
  
 This is the result:  
  
```  
<Sales.SalesOrderHeader SalesOrderID="43659" SalesPersonID="279" CustomerID="676">  
  <Sales.SalesOrderDetail SalesOrderID="43659" ProductID="776" OrderQty="1" UnitPrice="2024.9940" />  
  <Sales.SalesOrderDetail SalesOrderID="43659" ProductID="777" OrderQty="3" UnitPrice="2024.9940" />  
  <Sales.SalesOrderDetail SalesOrderID="43659" ProductID="778" OrderQty="1" UnitPrice="2024.9940" />  
  <SalesPerson SalesPersonID="279" EmployeeID="279" />  
</Sales.SalesOrderHeader>  
<Sales.SalesOrderHeader SalesOrderID="43660" SalesPersonID="279" CustomerID="117">  
  <Sales.SalesOrderDetail SalesOrderID="43660" ProductID="762" OrderQty="1" UnitPrice="419.4589" />  
  <Sales.SalesOrderDetail SalesOrderID="43660" ProductID="758" OrderQty="1" UnitPrice="874.7940" />  
  <SalesPerson SalesPersonID="279" EmployeeID="279" />  
</Sales.SalesOrderHeader>  
```  
  
 Because the `TYPE` directive returns a query result as **xml** type, you can query the resulting XML by using various **xml** data type methods. For more information, see [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md). In the following query, note the following:  
  
-   The previous query is added in the `FROM` clause. The query result is returned as a table. Note the `XmlCol` alias that is added.  
  
-   The `SELECT` clause specifies an XQuery against the `XmlCol` returned in the `FROM` clause. The **query()** method of the **xml** data type is used in specifying the XQuery. For more information, see [query&#40;&#41; Method &#40;xml Data Type&#41;](../../t-sql/xml/query-method-xml-data-type.md).  
  
    ```  
    SELECT XmlCol.query('<Root> { /* } </Root>')  
    FROM (  
    SELECT SalesOrderID, SalesPersonID, CustomerID,  
                 (select top 3 SalesOrderID, ProductID, OrderQty, UnitPrice  
                  from Sales.SalesOrderDetail  
                  WHERE SalesOrderDetail.SalesOrderID = SalesOrderHeader.SalesOrderID  
                  FOR XML AUTO, TYPE),  
                  (SELECT *   
                   FROM  (SELECT SalesPersonID, EmployeeID  
                        FROM Sales.SalesPerson, HumanResources.Employee  
                        WHERE SalesPerson.SalesPersonID = Employee.EmployeeID) As SalesPerson  
                   WHERE  SalesPerson.SalesPersonID = SalesOrderHeader.SalesPersonID  
             FOR XML AUTO, TYPE)  
    FROM Sales.SalesOrderHeader  
    WHERE SalesOrderID='43659' or SalesOrderID='43660'  
    FOR XML AUTO, TYPE ) as T(XmlCol)  
    ```  
  
## See Also  
 [Use Nested FOR XML Queries](../../relational-databases/xml/use-nested-for-xml-queries.md)  
  
  
