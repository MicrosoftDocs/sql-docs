---
title: "Examples: Using OPENXML | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "ColPattern [XML in SQL Server]"
  - "XML [SQL Server], mapping data"
  - "OPENXML statement, about OPENXML statement"
  - "overflow in XML document [SQL Server]"
  - "mapping XML data [SQL Server]"
  - "combining attribute-centric and element centric mapping"
  - "unconsumed data"
  - "attribute-centric mapping"
  - "column patterns [XML in SQL Server]"
  - "XML [SQL Server], overflow handling"
  - "row patterns [XML in SQL Server]"
  - "rowpattern [XML in SQL Server]"
  - "flags parameter"
  - "element-centric mapping [SQL Server]"
  - "edge tables"
ms.assetid: 689297f3-adb0-4d8d-bf62-cfda26210164
author: MightyPen
ms.author: genemi
manager: craigg
---
# Examples: Using OPENXML
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  The examples in this topic show how OPENXML is used to create a rowset view of an XML document. For information about the syntax of OPENXML, see [OPENXML &#40;Transact-SQL&#41;](../../t-sql/functions/openxml-transact-sql.md). The examples show all aspects of OPENXML, but do not specify metaproperties in OPENXML. For more information about how to specify metaproperties in OPENXML, see [Specify Metaproperties in OPENXML](../../relational-databases/xml/specify-metaproperties-in-openxml.md).  
  
## Examples  
 In retrieving the data, *rowpattern* is used to identify the nodes in the XML document that determine the rows. Additionally, *rowpattern* is expressed in the XPath pattern language that is used in the MSXML XPath implementation. For example, if the pattern ends in an element or an attribute, a row is created for each element or attribute node that is selected by *rowpattern*.  
  
 The *flags* value provides default mapping. If no *ColPattern* is specified in the *SchemaDeclaration*, the mapping specified in *flags* is assumed. The *flags* value is ignored if *ColPattern* is specified in *SchemaDeclaration*. The specified *ColPattern* determines the mapping, attribute-centric or element-centric, and also the behavior in dealing with overflow and unconsumed data.  
  
### A. Executing a simple SELECT statement with OPENXML  
 The XML document in this example is made up of the <`Customer`>, <`Order`>, and <`OrderDetail`> elements. The OPENXML statement retrieves customer information in a two-column rowset, **CustomerID** and **ContactName**, from the XML document.  
  
 First, the **sp_xml_preparedocument** stored procedure is called to obtain a document handle. This document handle is passed to OPENXML.  
  
 The OPENXML statement illustrates the following:  
  
-   *rowpattern* (/ROOT/Customer) identifies the <`Customer`> nodes to process.  
  
-   The *flags* parameter value is set to **1** and indicates attribute-centric mapping. As a result, the XML attributes map to the columns in the rowset defined in *SchemaDeclaration*.  
  
-   In *SchemaDeclaration*, in the WITH clause, the specified *ColName* values match the corresponding XML attribute names. Therefore, the *ColPattern* parameter is not specified in *SchemaDeclaration*.  
  
 The SELECT statement then retrieves all the columns in the rowset provided by OPENXML.  
  
```  
DECLARE @DocHandle int  
DECLARE @XmlDocument nvarchar(1000)  
SET @XmlDocument = N'<ROOT>  
<Customer CustomerID="VINET" ContactName="Paul Henriot">  
   <Order OrderID="10248" CustomerID="VINET" EmployeeID="5"   
          OrderDate="1996-07-04T00:00:00">  
      <OrderDetail ProductID="11" Quantity="12"/>  
      <OrderDetail ProductID="42" Quantity="10"/>  
   </Order>  
</Customer>  
<Customer CustomerID="LILAS" ContactName="Carlos Gonzlez">  
   <Order OrderID="10283" CustomerID="LILAS" EmployeeID="3"   
          OrderDate="1996-08-16T00:00:00">  
      <OrderDetail ProductID="72" Quantity="3"/>  
   </Order>  
</Customer>  
</ROOT>'  
-- Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @DocHandle OUTPUT, @XmlDocument  
-- Execute a SELECT statement using OPENXML rowset provider.  
SELECT *  
FROM OPENXML (@DocHandle, '/ROOT/Customer',1)  
      WITH (CustomerID  varchar(10),  
            ContactName varchar(20))  
EXEC sp_xml_removedocument @DocHandle  
```  
  
 This is the result:  
  
```  
CustomerID ContactName            
---------- --------------------   
VINET      Paul Henriot  
LILAS      Carlos Gonzlez  
```  
  
 Because the <`Customer`> elements do not have any subelements, if the same SELECT statement is executed with *flags* set to **2** to indicate element-centric mapping, the values of **CustomerID** and **ContactName** for both the customers are returned as NULL.  
  
 The \@xmlDocument can also be of **xml** type or of **(n)varchar(max)** type.  
  
 If <`CustomerID`> and <`ContactName`> in the XML document are subelements, the element-centric mapping retrieves the values.  
  
```  
DECLARE @XmlDocumentHandle int  
DECLARE @XmlDocument nvarchar(1000)  
SET @XmlDocument = N'<ROOT>  
<Customer>  
   <CustomerID>VINET</CustomerID>  
   <ContactName>Paul Henriot</ContactName>  
   <Order OrderID="10248" CustomerID="VINET" EmployeeID="5" OrderDate="1996-07-04T00:00:00">  
      <OrderDetail ProductID="11" Quantity="12"/>  
      <OrderDetail ProductID="42" Quantity="10"/>  
   </Order>  
</Customer>  
<Customer>     
   <CustomerID>LILAS</CustomerID>  
   <ContactName>Carlos Gonzlez</ContactName>  
   <Order OrderID="10283" CustomerID="LILAS" EmployeeID="3" OrderDate="1996-08-16T00:00:00">  
      <OrderDetail ProductID="72" Quantity="3"/>  
   </Order>  
</Customer>  
</ROOT>'  
-- Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @XmlDocumentHandle OUTPUT, @XmlDocument  
-- Execute a SELECT statement using OPENXML rowset provider.  
SELECT    *  
FROM      OPENXML (@XmlDocumentHandle, '/ROOT/Customer',2)  
           WITH (CustomerID  varchar(10),  
                 ContactName varchar(20))  
EXEC sp_xml_removedocument @XmlDocumentHandle  
```  
  
 This is the result:  
  
```  
CustomerID ContactName            
---------- --------------------   
VINET      Paul Henriot  
LILAS      Carlos Gonzlez  
```  
  
 Note that the document handle returned by **sp_xml_preparedocument** is valid for the duration of the batch and not the session.  
  
### B. Specifying ColPattern for mapping between rowset columns and the XML attributes and elements  
 This example shows how the XPath pattern is specified in the optional *ColPattern* parameter to provide mapping between rowset columns and the XML attributes and elements.  
  
 The XML document in this example is made up of the <`Customer`>, <`Order`>, and <`OrderDetail`> elements. The OPENXML statement retrieves customer and order information as a rowset (**CustomerID**, **OrderDate**, **ProdID**, and **Qty**) from the XML document.  
  
 First, the **sp_xml_preparedocument** stored procedure is called to obtain a document handle. This document handle is passed to OPENXML.  
  
 The OPENXML statement illustrates the following:  
  
-   *rowpattern* (/ROOT/Customer/Order/OrderDetail) identifies the <`OrderDetail`> nodes to process.  
  
 For illustration, the *flags* parameter value is set to **2** and indicates element-centric mapping. However, the mapping specified in *ColPattern* overwrites this mapping. That is, the XPath pattern specified in *ColPattern* maps the columns in the rowset to attributes. This results in attribute-centric mapping.  
  
 In *SchemaDeclaration*, in the WITH clause, *ColPattern* is also specified with the *ColName* and *ColType* parameters. The optional *ColPattern* is the XPath pattern specified and indicates the following:  
  
-   The **OrderID**, **CustomerID**, and **OrderDate** columns in the rowset map to the attributes of the parent of the nodes identified by *rowpattern*, and *rowpattern* identifies the <`OrderDetail`> nodes. Therefore, the **CustomerID** and **OrderDate** columns map to **CustomerID** and **OrderDate** attributes of the <`Order`> element.  
  
-   The **ProdID** and **Qty** columns in the rowset map to the **ProductID** and **Quantity** attributes of the nodes identified in *rowpattern*.  
  
 The SELECT statement then retrieves all the columns in the rowset provided by OPENXML.  
  
```  
DECLARE @XmlDocumentHandle int  
DECLARE @XmlDocument nvarchar(1000)  
SET @XmlDocument = N'<ROOT>  
<Customer CustomerID="VINET" ContactName="Paul Henriot">  
   <Order OrderID="10248" CustomerID="VINET" EmployeeID="5"   
           OrderDate="1996-07-04T00:00:00">  
      <OrderDetail ProductID="11" Quantity="12"/>  
      <OrderDetail ProductID="42" Quantity="10"/>  
   </Order>  
</Customer>  
<Customer CustomerID="LILAS" ContactName="Carlos Gonzlez">  
   <Order OrderID="10283" CustomerID="LILAS" EmployeeID="3"   
           OrderDate="1996-08-16T00:00:00">  
      <OrderDetail ProductID="72" Quantity="3"/>  
   </Order>  
</Customer>  
</ROOT>'  
-- Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @XmlDocumentHandle OUTPUT, @XmlDocument  
-- Execute a SELECT stmt using OPENXML rowset provider.  
SELECT *  
FROM OPENXML (@XmlDocumentHandle, '/ROOT/Customer/Order/OrderDetail',2)  
WITH (OrderID     int         '../@OrderID',  
      CustomerID  varchar(10) '../@CustomerID',  
      OrderDate   datetime    '../@OrderDate',  
      ProdID      int         '@ProductID',  
      Qty         int         '@Quantity')  
EXEC sp_xml_removedocument @XmlDocumentHandle  
```  
  
 This is the result:  
  
```  
OrderID CustomerID        OrderDate          ProdID    Qty  
-------------------------------------------------------------  
10248    VINET     1996-07-04 00:00:00.000     11       12  
10248    VINET     1996-07-04 00:00:00.000     42       10  
10283    LILAS     1996-08-16 00:00:00.000     72        3  
```  
  
 The XPath pattern specified as *ColPattern* can also be specified to map the XML elements to the rowset columns. This results in element-centric mapping. In the following example, the XML document <`CustomerID`> and <`OrderDate`> are subelements of the <`Orders`> element. Because *ColPattern* overwrites the mapping specified in the *flags* parameter, the *flags* parameter is not specified in OPENXML.  
  
```  
DECLARE @docHandle int  
DECLARE @XmlDocument nvarchar(1000)  
SET @XmlDocument = N'<ROOT>  
<Customer CustomerID="VINET" ContactName="Paul Henriot">  
   <Order EmployeeID="5" >  
      <OrderID>10248</OrderID>  
      <CustomerID>VINET</CustomerID>  
      <OrderDate>1996-07-04T00:00:00</OrderDate>  
      <OrderDetail ProductID="11" Quantity="12"/>  
      <OrderDetail ProductID="42" Quantity="10"/>  
   </Order>  
</Customer>  
<Customer CustomerID="LILAS" ContactName="Carlos Gonzlez">  
   <Order  EmployeeID="3" >  
      <OrderID>10283</OrderID>  
      <CustomerID>LILAS</CustomerID>  
      <OrderDate>1996-08-16T00:00:00</OrderDate>  
      <OrderDetail ProductID="72" Quantity="3"/>  
   </Order>  
</Customer>  
</ROOT>'  
-- Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @docHandle OUTPUT, @XmlDocument  
-- Execute a SELECT statement using OPENXML rowset provider.  
SELECT *  
FROM OPENXML (@docHandle, '/ROOT/Customer/Order/OrderDetail')  
WITH (CustomerID  varchar(10)   '../CustomerID',  
      OrderDate   datetime      '../OrderDate',  
      ProdID      int           '@ProductID',  
      Qty         int           '@Quantity')  
EXEC sp_xml_removedocument @docHandle  
```  
  
### C. Combining attribute-centric and element-centric mapping  
 In this example, the *flags* parameter is set to **3** and indicates that both attribute-centric and element-centric mapping will be applied. In this case, the attribute-centric mapping is applied first, and then element-centric mapping is applied for all the columns not yet dealt with.  
  
```  
DECLARE @docHandle int  
DECLARE @XmlDocument nvarchar(1000)  
SET @XmlDocument =N'<ROOT>  
<Customer CustomerID="VINET"  >  
     <ContactName>Paul Henriot</ContactName>  
   <Order OrderID="10248" CustomerID="VINET" EmployeeID="5"   
          OrderDate="1996-07-04T00:00:00">  
      <OrderDetail ProductID="11" Quantity="12"/>  
      <OrderDetail ProductID="42" Quantity="10"/>  
   </Order>  
</Customer>  
<Customer CustomerID="LILAS" >   
     <ContactName>Carlos Gonzlez</ContactName>  
   <Order OrderID="10283" CustomerID="LILAS" EmployeeID="3"   
          OrderDate="1996-08-16T00:00:00">  
      <OrderDetail ProductID="72" Quantity="3"/>  
   </Order>  
</Customer>  
</ROOT>'  
-- Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @docHandle OUTPUT, @XmlDocument  
  
-- Execute a SELECT statement using OPENXML rowset provider.  
SELECT *  
FROM OPENXML (@docHandle, '/ROOT/Customer',3)  
      WITH (CustomerID  varchar(10),  
            ContactName varchar(20))  
EXEC sp_xml_removedocument @docHandle  
```  
  
 This is the result  
  
```  
CustomerID ContactName            
---------- --------------------   
VINET      Paul Henriot  
LILAS      Carlos Gonzlez  
```  
  
 The attribute-centric mapping is applied for **CustomerID**. There is no **ContactName** attribute in the <`Customer`> element. Therefore, element-centric mapping is applied.  
  
### D. Specifying the text() XPath function as ColPattern  
 The XML document in this example is made up of the <`Customer`> and <`Order`> elements. The OPENXML statement retrieves a rowset that is made up of the **oid** attribute from the <`Order`> element, the ID of the parent of the node identified by *rowpattern*, and the leaf-value string of the element content.  
  
 First, the **sp_xml_preparedocument** stored procedure is called to obtain a document handle. This document handle is passed to OPENXML.  
  
 The OPENXML statement illustrates the following:  
  
-   *rowpattern* (/root/Customer/Order) identifies the <`Order`> nodes to process.  
  
-   The *flags* parameter value is set to **1** and indicates attribute-centric mapping. As a result, the XML attributes map to the rowset columns defined in *SchemaDeclaration*.  
  
-   In *SchemaDeclaration* in the WITH clause, the **oid** and **amount** rowset column names match the corresponding XML attribute names. Therefore, the *ColPattern* parameter is not specified. For the **comment** column in the rowset, the XPath function, **text()**, is specified as *ColPattern*. This overwrites the attribute-centric mapping specified in *flags*, and the column contains the leaf-value string of the element content.  
  
 The SELECT statement then retrieves all the columns in the rowset provided by OPENXML.  
  
```  
DECLARE @docHandle int  
DECLARE @xmlDocument nvarchar(1000)  
--sample XML document  
SET @xmlDocument =N'<root>  
  <Customer cid= "C1" name="Janine" city="Issaquah">  
      <Order oid="O1" date="1/20/1996" amount="3.5" />  
      <Order oid="O2" date="4/30/1997" amount="13.4">Customer was very satisfied  
      </Order>  
   </Customer>  
   <Customer cid="C2" name="Ursula" city="Oelde" >  
      <Order oid="O3" date="7/14/1999" amount="100" note="Wrap it blue   
             white red">  
            <Urgency>Important</Urgency>  
            Happy Customer.  
      </Order>  
      <Order oid="O4" date="1/20/1996" amount="10000"/>  
   </Customer>  
</root>'  
-- Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @docHandle OUTPUT, @xmlDocument  
  
-- Execute a SELECT statement using OPENXML rowset provider.  
SELECT *  
FROM OPENXML (@docHandle, '/root/Customer/Order', 1)  
     WITH (oid     char(5),   
           amount  float,   
           comment ntext 'text()')  
EXEC sp_xml_removedocument @docHandle  
```  
  
 This is the result:  
  
```  
oid   amount        comment  
----- -----------   -----------------------------  
O1    3.5           NULL  
O2    13.4          Customer was very satisfied  
O3    100.0         Happy Customer.  
O4    10000.0       NULL  
```  
  
### E. Specifying TableName in the WITH clause  
 This example specifies *TableName* in the WITH clause instead of *SchemaDeclaration*. This is useful if you have a table that has the structure you want and no column patterns, *ColPattern* parameter, are required.  
  
 The XML document in this example is made up of the <`Customer`> and <`Order`> elements. The OPENXML statement retrieves order information in a three-column rowset (**oid**, **date**, and **amount**) from the XML document.  
  
 First, the **sp_xml_preparedocument** stored procedure is called to obtain a document handle. This document handle is passed to OPENXML.  
  
 The OPENXML statement illustrates the following:  
  
-   *rowpattern* (/root/Customer/Order) identifies the <`Order`> nodes to process.  
  
-   There is no *SchemaDeclaration* in the WITH clause. Instead, a table name is specified. Therefore, the table schema is used as the rowset schema.  
  
-   The *flags* parameter value is set to **1** and indicates attribute-centric mapping. Therefore, attributes of the elements, identified by *rowpattern*, map to the rowset columns with the same name.  
  
 The SELECT statement then retrieves all the columns in the rowset provided by OPENXML.  
  
```  
-- Create a test table. This table schema is used by OPENXML as the  
-- rowset schema.  
CREATE TABLE T1(oid char(5), date datetime, amount float)  
GO  
DECLARE @docHandle int  
DECLARE @xmlDocument nvarchar(1000)  
-- Sample XML document  
SET @xmlDocument =N'<root>  
  <Customer cid= "C1" name="Janine" city="Issaquah">  
      <Order oid="O1" date="1/20/1996" amount="3.5" />  
      <Order oid="O2" date="4/30/1997" amount="13.4">Customer was very   
             satisfied</Order>  
   </Customer>  
   <Customer cid="C2" name="Ursula" city="Oelde" >  
      <Order oid="O3" date="7/14/1999" amount="100" note="Wrap it blue   
             white red">  
          <Urgency>Important</Urgency>  
      </Order>  
      <Order oid="O4" date="1/20/1996" amount="10000"/>  
   </Customer>  
</root>'  
--Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @docHandle OUTPUT, @xmlDocument  
  
-- Execute a SELECT statement using OPENXML rowset provider.  
SELECT *  
FROM OPENXML (@docHandle, '/root/Customer/Order', 1)  
     WITH T1  
EXEC sp_xml_removedocument @docHandle  
```  
  
 This is the result:  
  
```  
oid   date                        amount  
----- --------------------------- ----------  
O1    1996-01-20 00:00:00.000     3.5  
O2    1997-04-30 00:00:00.000     13.4  
O3    1999-07-14 00:00:00.000     100.0  
O4    1996-01-20 00:00:00.000     10000.0  
```  
  
### F. Obtaining the result in an edge table format  
 In this example, the WITH clause is not specified in the OPENXML statement. As a result, the rowset generated by OPENXML has an edge table format. The SELECT statement returns all the columns in the edge table.  
  
 The sample XML document in the example is made up of the <`Customer`>, <`Order`>, and <`OrderDetail`> elements.  
  
 First, the **sp_xml_preparedocument** stored procedure is called to obtain a document handle. This document handle is passed to OPENXML.  
  
 The OPENXML statement illustrates the following:  
  
-   *rowpattern* (/ROOT/Customer) identifies the <`Customer`> nodes to process.  
  
-   The WITH clause is not provided. Therefore, OPENXML returns the rowset in an edge table format.  
  
 The SELECT statement then retrieves all the columns in the edge table.  
  
```  
DECLARE @docHandle int  
DECLARE @xmlDocument nvarchar(1000)  
SET @xmlDocument = N'<ROOT>  
<Customer CustomerID="VINET" ContactName="Paul Henriot">  
   <Order CustomerID="VINET" EmployeeID="5" OrderDate=  
           "1996-07-04T00:00:00">  
      <OrderDetail OrderID="10248" ProductID="11" Quantity="12"/>  
      <OrderDetail OrderID="10248" ProductID="42" Quantity="10"/>  
   </Order>  
</Customer>  
<Customer CustomerID="LILAS" ContactName="Carlos Gonzlez">  
   <Order CustomerID="LILAS" EmployeeID="3" OrderDate=  
           "1996-08-16T00:00:00">  
      <OrderDetail OrderID="10283" ProductID="72" Quantity="3"/>  
   </Order>  
</Customer>  
</ROOT>'  
--Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @docHandle OUTPUT, @xmlDocument  
-- Execute a SELECT statement using OPENXML rowset provider.  
SELECT *  
FROM OPENXML (@docHandle, '/ROOT/Customer')  
  
EXEC sp_xml_removedocument @docHandle  
```  
  
 The result is returned as an edge table. You can write queries against the edge table to obtain information. For example:  
  
-   The following query returns the number of **Customer** nodes in the document. Because the WITH clause is not specified, OPENXML returns an edge table. The SELECT statement queries the edge table.  
  
    ```  
    SELECT count(*)  
    FROM OPENXML(@docHandle, '/')  
    WHERE localname = 'Customer'  
    ```  
  
-   The following query returns the local names of XML nodes of element type.  
  
    ```  
    SELECT distinct localname   
    FROM OPENXML(@docHandle, '/')   
    WHERE nodetype = 1   
    ORDER BY localname  
    ```  
  
### G. Specifying rowpattern ending with an attribute  
 The XML document in this example is made up of the <`Customer`>, <`Order`>, and <`OrderDetail`> elements. The OPENXML statement retrieves information about the order details in a three-column rowset (**ProductID**, **Quantity**, and **OrderID**) from the XML document.  
  
 First, the **sp_xml_preparedocument** is called to obtain a document handle. This document handle is passed to OPENXML.  
  
 The OPENXML statement illustrates the following:  
  
-   *rowpattern* (/ROOT/Customer/Order/OrderDetail/\@ProductID) ends with an XML attribute, **ProductID**. In the resulting rowset, a row is created for each attribute node selected in the XML document.  
  
-   In this example, the *flags* parameter is not specified. Instead, the mappings are specified by the *ColPattern* parameter.  
  
 In *SchemaDeclaration* in the WITH clause, *ColPattern* is also specified with the *ColName* and *ColType* parameters. The optional *ColPattern* is the XPath pattern specified to indicate the following:  
  
-   The XPath pattern (**.**) specified as *ColPattern* for the **ProdID** column in the rowset identifies the context node, current node. As per the *rowpattern* specified, it is the **ProductID** attribute of the <`OrderDetail`> element.  
  
-   The *ColPattern*, **../\@Quantity**, specified for the **Qty** column in the rowset identifies the **Quantity** attribute of the parent, <`OrderDetail`>, node of the context node, \<ProductID>.  
  
-   Similarly, the *ColPattern*, **../../\@OrderID**, specified for the **OID** column in the rowset identifies the **OrderID** attribute of the parent, <`Order`>, of the parent node of the context node. The parent node is <`OrderDetail`>, and the context node is <`ProductID`>.  
  
 The SELECT statement then retrieves all the columns in the rowset provided by OPENXML.  
  
```  
DECLARE @docHandle int  
DECLARE @xmlDocument nvarchar(1000)  
--Sample XML document  
SET @xmlDocument =N'<ROOT>  
<Customer CustomerID="VINET" ContactName="Paul Henriot">  
   <Order OrderID="10248" CustomerID="VINET" EmployeeID="5" OrderDate=  
           "1996-07-04T00:00:00">  
      <OrderDetail ProductID="11" Quantity="12"/>  
      <OrderDetail ProductID="42" Quantity="10"/>  
   </Order>  
</Customer>  
<Customer CustomerID="LILAS" ContactName="Carlos Gonzlez">  
   <Order OrderID="10283" CustomerID="LILAS" EmployeeID="3" OrderDate=  
           "1996-08-16T00:00:00">  
      <OrderDetail ProductID="72" Quantity="3"/>  
   </Order>  
</Customer>  
</ROOT>'  
-- Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @docHandle OUTPUT, @xmlDocument  
-- Execute a SELECT statement using OPENXML rowset provider.  
SELECT *  
FROM OPENXML (@docHandle, '/ROOT/Customer/Order/OrderDetail/@ProductID')  
       WITH ( ProdID  int '.',  
              Qty     int '../@Quantity',  
              OID     int '../../@OrderID')  
EXEC sp_xml_removedocument @docHandle  
```  
  
 This is the result:  
  
```  
ProdID      Qty         OID  
----------- ----------- -------   
11          12          10248  
42          10          10248  
72          3           10283  
```  
  
### H. Specifying an XML document that has multiple text nodes  
 If you have multiple text nodes in an XML document, a SELECT statement with a *ColPattern*, **text()**, returns only the first text node, instead of all of them. For example:  
  
```  
DECLARE @h int  
EXEC sp_xml_preparedocument @h OUTPUT,  
         N'<root xmlns:a="urn:1">  
           <a:Elem abar="asdf">  
             T<a>a</a>U  
           </a:Elem>  
         </root>',  
         '<ns xmlns:b="urn:1" />'  
  
SELECT * FROM openxml(@h, '/root/b:Elem')  
      WITH (Col1 varchar(20) 'text()')  
EXEC sp_xml_removedocument @h  
```  
  
 The SELECT statement returns **T** as the result, and not **TaU**.  
  
### I. Specifying the xml data type in the WITH clause  
 In the WITH clause, a column pattern that is mapped to the **xml** data type column, whether typed or untyped, must return either an empty sequence or a sequence of elements, processing instructions, text nodes, and comments. The data is cast to an **xml** data type.  
  
 In the following example, the table schema declaration in the WITH clause includes **xml** type columns.  
  
```  
DECLARE @h int  
DECLARE @x xml  
set @x = '<Root>  
  <row id="1"><lname>Duffy</lname>  
   <Address>  
            <Street>111 Maple</Street>  
            <City>Seattle</City>  
   </Address>  
  </row>  
  <row id="2"><lname>Wang</lname>  
   <Address>  
            <Street>222 Pine</Street>  
            <City>Bothell</City>  
   </Address>  
  </row>  
</Root>'  
  
EXEC sp_xml_preparedocument @h output, @x  
SELECT *  
FROM   OPENXML (@h, '/Root/row', 10)  
      WITH (id int '@id',  
  
            lname    varchar(30),  
            xmlname  xml 'lname',  
            OverFlow xml '@mp:xmltext')  
EXEC sp_xml_removedocument @h  
```  
  
 Specifically, you are passing an **xml** type variable (\@x) to the **sp_xml_preparedocument()** function.  
  
 This is the result:  
  
```  
id  lname   xmlname                   OverFlow  
--- ------- ------------------------------ -------------------------------  
1   Duffy   <lname>Duffy</lname>  <row><Address>  
                                   <Street>111 Maple</Street>  
                                   <City>Seattle</City>  
                                  </Address></row>  
2   Wang    <lname>Wang</lname>   <row><Address>  
                                    <Street>222 Pine</Street>  
                                    <City>Bothell</City>  
                                   </Address></row>  
```  
  
 Note the following from the result:  
  
-   For the **lname** column of **varchar(30)** type, its value is retrieved from the corresponding <`lname`> element.  
  
-   For the **xmlname** column of **xml** type, the same name element is returned as its value.  
  
-   The flag is set to 10. The 10 means 2 + 8, where 2 indicates element-centric mapping and 8 indicates that only unconsumed XML data should be added to the OverFlow column defined in the WITH clause. If you set the flag to 2, the whole XML document is copied to the OverFlow column that is specified in the WITH clause.  
  
-   In case the column in the WITH clause is a typed XML column and the XML instance does not confirm to the schema, an error is returned.  
  
### J. Retrieving individual values from multivalued attributes  
 An XML document can have attributes that are multivalued. For example, the **IDREFS** attribute can be multivalued. In an XML document, the multivalued attribute values are specified as a string, with the values separated by a space. In the following XML document, the **attends** attribute of the \<Student> element and the **attendedBy** attribute of \<Class> are multivalued. Retrieving individual values from a multivalued XML attribute and storing each value in a separate row in the database requires additional work. This example shows the process.  
  
 This sample XML document is made up of the following elements:  
  
-   \<Student>  
  
     The **id** (student ID), **name**, and **attends** attributes. The **attends** attribute is a multivalued attribute.  
  
-   \<Class>  
  
     The **id** (class ID), **name**, and **attendedBy** attributes. The **attendedBy** attribute is a multivalued attribute.  
  
 The **attends** attribute in \<Student> and the **attendedBy** attribute in \<Class> represent a **m:n** relationship between the Student and Class tables. A student can take many classes and a class can have many students.  
  
 Assume that you want to shred this document and save it in the database as shown in the following:  
  
-   Save the \<Student> data in the Students table.  
  
-   Save the \<Class> data in the Courses table.  
  
-   Save the **m:n** relationship data, between Student and Class, in the CourseAttendence table. Additional work is required to extract the values. To retrieve this information and store it in the table, use these stored procedures:  
  
    -   **Insert_Idrefs_Values**  
  
         Inserts the values of course ID and student ID in the CourseAttendence table.  
  
    -   **Extract_idrefs_values**  
  
         Extracts the individual student IDs from each \<Course> element. An edge table is used to retrieve these values.  
  
 Here are the steps:  
  
```  
-- Create these tables:  
DROP TABLE CourseAttendance  
DROP TABLE Students  
DROP TABLE Courses  
GO  
CREATE TABLE Students(  
                id   varchar(5) primary key,  
                name varchar(30)  
                )  
GO  
CREATE TABLE Courses(  
               id       varchar(5) primary key,  
               name     varchar(30),  
               taughtBy varchar(5)  
)  
GO  
CREATE TABLE CourseAttendance(  
             id         varchar(5) references Courses(id),  
             attendedBy varchar(5) references Students(id),  
             constraint CourseAttendance_PK primary key (id, attendedBy)  
)  
go  
-- Create these stored procedures:  
DROP PROCEDURE f_idrefs  
GO  
CREATE PROCEDURE f_idrefs  
    @t      varchar(500),  
    @idtab  varchar(50),  
    @id     varchar(5)  
AS  
DECLARE @sp int  
DECLARE @att varchar(5)  
SET @sp = 0  
WHILE (LEN(@t) > 0)  
BEGIN   
    SET @sp = CHARINDEX(' ', @t+ ' ')  
    SET @att = LEFT(@t, @sp-1)  
    EXEC('INSERT INTO '+@idtab+' VALUES ('''+@id+''', '''+@att+''')')  
    SET @t = SUBSTRING(@t+ ' ', @sp+1, LEN(@t)+1-@sp)  
END  
Go  
  
DROP PROCEDURE fill_idrefs  
GO  
CREATE PROCEDURE fill_idrefs   
    @xmldoc     int,  
    @xpath      varchar(100),  
    @from       varchar(50),  
    @to         varchar(50),  
    @idtable    varchar(100)  
AS  
DECLARE @t varchar(500)  
DECLARE @id varchar(5)  
  
/* Temporary Edge table */  
SELECT *   
INTO #TempEdge   
FROM OPENXML(@xmldoc, @xpath)  
  
DECLARE fillidrefs_cursor CURSOR FOR  
    SELECT CAST(iv.text AS nvarchar(200)) AS id,  
           CAST(av.text AS nvarchar(4000)) AS refs  
    FROM   #TempEdge c, #TempEdge i,  
           #TempEdge iv, #TempEdge a, #TempEdge av  
    WHERE  c.id = i.parentid  
    AND    UPPER(i.localname) = UPPER(@from)  
    AND    i.id = iv.parentid  
    AND    c.id = a.parentid  
    AND    UPPER(a.localname) = UPPER(@to)  
    AND    a.id = av.parentid  
  
OPEN fillidrefs_cursor  
FETCH NEXT FROM fillidrefs_cursor INTO @id, @t  
WHILE (@@FETCH_STATUS <> -1)  
BEGIN  
    IF (@@FETCH_STATUS <> -2)  
    BEGIN  
        execute f_idrefs @t, @idtable, @id  
    END  
    FETCH NEXT FROM fillidrefs_cursor INTO @id, @t  
END  
CLOSE fillidrefs_cursor  
DEALLOCATE fillidrefs_cursor  
Go  
-- This is the sample document that is shredded and the data is stored in the preceding tables.  
DECLARE @h int  
EXECUTE sp_xml_preparedocument @h OUTPUT, N'<Data>  
  <Student id = "s1" name = "Student1"  attends = "c1 c3 c6"  />  
  <Student id = "s2" name = "Student2"  attends = "c2 c4" />  
  <Student id = "s3" name = "Student3"  attends = "c2 c4 c6" />  
  <Student id = "s4" name = "Student4"  attends = "c1 c3 c5" />  
  <Student id = "s5" name = "Student5"  attends = "c1 c3 c5 c6" />  
  <Student id = "s6" name = "Student6" />  
  
  <Class id = "c1" name = "Intro to Programming"   
         attendedBy = "s1 s4 s5" />  
  <Class id = "c2" name = "Databases"   
         attendedBy = "s2 s3" />  
  <Class id = "c3" name = "Operating Systems"   
         attendedBy = "s1 s4 s5" />  
  <Class id = "c4" name = "Networks" attendedBy = "s2 s3" />  
  <Class id = "c5" name = "Algorithms and Graphs"   
         attendedBy =  "s4 s5"/>  
  <Class id = "c6" name = "Power and Pragmatism"   
         attendedBy = "s1 s3 s5" />  
</Data>'  
  
INSERT INTO Students SELECT * FROM OPENXML(@h, '//Student') WITH Students  
  
INSERT INTO Courses SELECT * FROM OPENXML(@h, '//Class') WITH Courses  
/* Using the edge table */  
EXECUTE fill_idrefs @h, '//Class', 'id', 'attendedby', 'CourseAttendance'  
  
SELECT * FROM Students  
SELECT * FROM Courses  
SELECT * FROM CourseAttendance  
  
EXECUTE sp_xml_removedocument @h  
```  
  
### K. Retrieving binary from base64 encoded data in XML  
 Binary data is frequently included in XML using base64 encoding. When you shred this XML by using OPENXML, you receive the base64 encoded data. This example shows how you can convert the base64 encoded data back to binary.  
  
-   Create a table with sample binary data.  
  
-   Use a FOR XML query and the BINARY BASE64 option to construct XML that has the binary data encoded as base64.  
  
-   Shred the XML by using OPENXML. The data returned by OPENXML will be base64 encoded data. Next, call the .value function to convert it back to binary.  
  
```  
CREATE TABLE T (Col1 int primary key, Col2 varbinary(100))  
go  
-- Insert sample binary data  
INSERT T VALUES(1, 0x1234567890)   
go  
 -- Create test XML document that has base64 encoded binary data (use FOR XML query and specify BINARY BASE64 option)  
SELECT * FROM T  
FOR XML AUTO, BINARY BASE64  
go  
-- result  
-- <T Col1="1" Col2="EjRWeJA="/>  
  
-- Now shredd the sample XML using OPENXML.   
-- Call the .value function to convert   
-- the base64 encoded data returned by OPENXML to binary.  
DECLARE @h int ;  
EXEC sp_xml_preparedocument @h OUTPUT, '<T Col1="1" Col2="EjRWeJA="/>' ;  
SELECT Col1,   
CAST('<binary>' + Col2 + '</binary>' AS XML).value('.', 'varbinary(max)') AS BinaryCol   
FROM openxml(@h, '/T')   
WITH (Col1 integer, Col2 varchar(max)) ;  
EXEC sp_xml_removedocument @h ;  
GO  
```  
  
 This is the result. The binary data returned is the original binary data in table T.  
  
```  
Col1        BinaryCol  
----------- ---------------------  
1           0x1234567890  
```  
  
## See Also  
 [sp_xml_preparedocument &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-xml-preparedocument-transact-sql.md)   
 [sp_xml_removedocument &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-xml-removedocument-transact-sql.md)   
 [OPENXML &#40;Transact-SQL&#41;](../../t-sql/functions/openxml-transact-sql.md)   
 [OPENXML &#40;SQL Server&#41;](../../relational-databases/xml/openxml-sql-server.md)  
  
  
