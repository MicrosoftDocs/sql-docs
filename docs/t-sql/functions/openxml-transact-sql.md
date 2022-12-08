---
title: "OPENXML (Transact-SQL)"
description: "OPENXML (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/20/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "OPENXML_TSQL"
  - "OPENXML"
helpviewer_keywords:
  - "OPENXML statement"
  - "rowsets [SQL Server], XML documents"
  - "XML [SQL Server], rowset views"
dev_langs:
  - "TSQL"
---
# OPENXML (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  OPENXML provides a rowset view over an XML document. Because OPENXML is a rowset provider, OPENXML can be used in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements in which rowset providers such as a table, view, or the OPENROWSET function can appear.  
  
 ![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
OPENXML( idoc int [ in] , rowpattern nvarchar [ in ] , [ flags byte [ in ] ] )   
[ WITH ( SchemaDeclaration | TableName ) ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *idoc*  
 Is the document handle of the internal representation of an XML document. The internal representation of an XML document is created by calling **sp_xml_preparedocument**.  
  
 *rowpattern*  
 Is the XPath pattern used to identify the nodes to be processed as rows. The nodes come from the XML document whose handle is passed in the *idoc* parameter.
  
 *flags*  
 Indicates the mapping used between the XML data and the relational rowset, and how the spill-over column is filled. *flags* is an optional input parameter, and can be one of the following values.  
  
|Byte value|Description|  
|----------------|-----------------|  
|**0**|Defaults to **attribute-centric** mapping.|  
|**1**|Use the **attribute-centric** mapping. Can be combined with XML_ELEMENTS. In this case, **attribute-centric** mapping is applied first. Next, **element-centric** mapping is applied for any remaining columns.|  
|**2**|Use the **element-centric** mapping. Can be combined with XML_ATTRIBUTES. In this case, **attribute-centric** mapping is applied first. Next, **element-centric** mapping is applied for any remaining columns.|  
|**8**|Can be combined (logical OR) with XML_ATTRIBUTES or XML_ELEMENTS. In the context of retrieval, this flag indicates that the consumed data shouldn't be copied to the overflow property **\@mp:xmltext**.|  
  
 _SchemaDeclaration_  
 Is the schema definition of the form: _ColName_*ColType* [_ColPattern_ | _MetaProperty_] [**,**_ColNameColType_ [_ColPattern_ | _MetaProperty_]...]  
  
 _ColName_  
 Is the column name in the rowset.  
  
 *ColType*  
 Is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type of the column in the rowset. If the column types differ from the underlying **xml** data type of the attribute, type coercion occurs.  
  
 *ColPattern*  
 Is an optional, general XPath pattern that describes how the XML nodes should be mapped to the columns. If *ColPattern* isn't specified, the default mapping (**attribute-centric** or **element-centric** mapping as specified by *flags*) takes place.  
  
 The XPath pattern specified as *ColPattern* is used to specify the special nature of the mapping (for **attribute-centric** and **element-centric** mapping) that overwrites or enhances the default mapping indicated by *flags*.  
  
 The general XPath pattern specified as *ColPattern* also supports the metaproperties.  
  
 *MetaProperty*  
 Is one of the metaproperties provided by OPENXML. If *MetaProperty* is specified, the column contains information provided by the metaproperty. The metaproperties allow you to extract information (such as relative position and namespace information) about XML nodes. These metaproperties provide more information than is visible in the textual representation.  
  
 *TableName*  
 Is the table name that can be given (instead of *SchemaDeclaration*) if a table with the desired schema already exists and no column patterns are required.  
  
## Remarks  
 The WITH clause provides a rowset format (and additional mapping information as required) by using either *SchemaDeclaration* or specifying an existing *TableName*. If the optional WITH clause isn't specified, the results are returned in an **edge** table format. Edge tables represent the fine-grained XML document structure (such as element/attribute names, the document hierarchy, the namespaces, PIs, and son on) in a single table.  
  
 The following table describes the structure of the **edge** table.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**bigint**|Is the unique ID of the document node.<br /><br /> The root element has an ID value 0. The negative ID values are reserved.|  
|**parentid**|**bigint**|Identifies the parent of the node. The parent identified by this ID isn't necessarily the parent element, but it depends on the NodeType of the node whose parent is identified by this ID. For example, if the node is a text node, the parent of it may be an attribute node.<br /><br /> If the node is at the top level in the XML document, its **ParentID** is NULL.|  
|**nodetype**|**int**|Identifies the node type. Is an integer that corresponds to the XML DOM node type numbering.<br /><br /> The node types are:<br /><br /> 1 = Element node<br /><br /> 2 = Attribute node<br /><br /> 3 = Text node|  
|**localname**|**nvarchar**|Gives the local name of the element or attribute. Is NULL if the DOM object doesn't have a name.|  
|**prefix**|**nvarchar**|Is the namespace prefix of the node name.|  
|**namespaceuri**|**nvarchar**|Is the namespace URI of the node. If the value is NULL, no namespace is present.|  
|**datatype**|**nvarchar**|Is the actual data type of the element or attribute row, otherwise is NULL. The data type is inferred from the inline DTD or from the inline schema.|  
|**prev**|**bigint**|Is the XML ID of the previous sibling element. Is NULL if there is no direct previous sibling.|  
|**text**|**ntext**|Contains the attribute value or the element content in text form (or is NULL if the **edge** table entry doesn't require a value).|  
  
## Examples  
  
### A. Using a simple SELECT statement with OPENXML  
 The following example creates an internal representation of the XML image by using `sp_xml_preparedocument`. A `SELECT` statement that uses an `OPENXML` rowset provider is then executed against the internal representation of the XML document.  
  
 The *flag* value is set to `1`. This value indicates **attribute-centric** mapping. Therefore, the XML attributes map to the columns in the rowset. The *rowpattern* specified as `/ROOT/Customer` identifies the `<Customers>` nodes to be processed.  
  
 The optional *ColPattern* (column pattern) parameter isn't specified because the column name matches the XML attribute names.  
  
 The `OPENXML` rowset provider creates a two-column rowset (`CustomerID` and `ContactName`) from which the `SELECT` statement retrieves the necessary columns (in this case, all the columns).  
  
```sql  
DECLARE @idoc INT, @doc VARCHAR(1000);  
SET @doc ='  
<ROOT>  
<Customer CustomerID="VINET" ContactName="Paul Henriot">  
   <Order CustomerID="VINET" EmployeeID="5" OrderDate="1996-07-04T00:00:00">  
      <OrderDetail OrderID="10248" ProductID="11" Quantity="12"/>  
      <OrderDetail OrderID="10248" ProductID="42" Quantity="10"/>  
   </Order>  
</Customer>  
<Customer CustomerID="LILAS" ContactName="Carlos Gonzlez">  
   <Order CustomerID="LILAS" EmployeeID="3" OrderDate="1996-08-16T00:00:00">  
      <OrderDetail OrderID="10283" ProductID="72" Quantity="3"/>  
   </Order>  
</Customer>  
</ROOT>';  
--Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc;  
-- Execute a SELECT statement that uses the OPENXML rowset provider.  
SELECT    *  
FROM       OPENXML (@idoc, '/ROOT/Customer',1)  
            WITH (CustomerID  VARCHAR(10),  
                  ContactName VARCHAR(20));  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
CustomerID ContactName            
---------- --------------------   
VINET      Paul Henriot  
LILAS      Carlos Gonzlez  
```  
  
 If the same `SELECT` statement is executed with *flags* set to `2`, indicating **element-centric** mapping, the values of `CustomerID` and `ContactName` for both of the customers in the XML document are returned as NULL, because there aren't any elements named `CustomerID` or `ContactName` in the XML document.  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
CustomerID ContactName  
---------- -----------  
NULL       NULL  
NULL       NULL  
```  
  
### B. Specifying ColPattern for mapping between columns and the XML attributes  
 The following query returns customer ID, order date, product ID, and quantity attributes from the XML document. The *rowpattern* identifies the `<OrderDetails>` elements. `ProductID` and `Quantity` are the attributes of the `<OrderDetails>` element. However, `OrderID`, `CustomerID`, and `OrderDate` are the attributes of the parent element (`<Orders>`).  
  
 The optional *ColPattern* is specified for the following mappings:  
  
-   The `OrderID`, `CustomerID`, and `OrderDate` in the rowset map to the attributes of the parent of the nodes identified by *rowpattern* in the XML document.  
  
-   The `ProdID` column in the rowset maps to the `ProductID` attribute, and the `Qty` column in the rowset maps to the `Quantity` attribute of the nodes identified in *rowpattern*.  
  
 Although the **element-centric** mapping is specified by the *flags* parameter, the mapping specified in *ColPattern* overwrites this mapping.  
  
```sql  
DECLARE @idoc INT, @doc VARCHAR(1000);   
SET @doc ='  
<ROOT>  
<Customer CustomerID="VINET" ContactName="Paul Henriot">  
   <Order OrderID="10248" CustomerID="VINET" EmployeeID="5"   
           OrderDate="1996-07-04T00:00:00">  
      <OrderDetail ProductID="11" Quantity="12"/>  
      <OrderDetail ProductID="42" Quantity="10"/>  
   </Order>  
</Customer>  
<Customer CustomerID="LILAS" ContactName="Carlos Gonzlez">v  
   <Order OrderID="10283" CustomerID="LILAS" EmployeeID="3"   
           OrderDate="1996-08-16T00:00:00">  
      <OrderDetail ProductID="72" Quantity="3"/>  
   </Order>  
</Customer>  
</ROOT>';   
  
--Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc;   
  
-- SELECT stmt using OPENXML rowset provider  
SELECT *  
FROM   OPENXML (@idoc, '/ROOT/Customer/Order/OrderDetail',2)   
         WITH (OrderID       int         '../@OrderID',
               CustomerID  varchar(10) '../@CustomerID',
               OrderDate   datetime    '../@OrderDate',
               ProdID      int         '@ProductID',
               Qty         int         '@Quantity');
  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
OrderID CustomerID           OrderDate                 ProdID    Qty  
------------------------------------------------------------------------  
10248      VINET       1996-07-04 00:00:00.000   11      12  
10248      VINET       1996-07-04 00:00:00.000   42      10  
10283      LILAS       1996-08-16 00:00:00.000   72      3  
```  
  
### C. Obtaining results in an edge table format  
 The sample XML document in the following example consists of `<Customers>`, `<Orders>`, and `<Order_0020_Details>` elements. First, **sp_xml_preparedocument** is called to obtain a document handle. This document handle is passed to `OPENXML`.  
  
 In the `OPENXML` statement, the *rowpattern* (`/ROOT/Customers`) identifies the `<Customers>` nodes to process. Because the WITH clause isn't provided, `OPENXML` returns the rowset in an **edge** table format.  
  
 Finally the `SELECT` statement retrieves all the columns in the **edge** table.  
  
```sql  
DECLARE @idoc INT, @doc VARCHAR(1000);   
SET @doc ='  
<ROOT>  
<Customers CustomerID="VINET" ContactName="Paul Henriot">  
   <Orders CustomerID="VINET" EmployeeID="5" OrderDate=  
           "1996-07-04T00:00:00">  
      <Order_x0020_Details OrderID="10248" ProductID="11" Quantity="12"/>  
      <Order_x0020_Details OrderID="10248" ProductID="42" Quantity="10"/>  
   </Orders>  
</Customers>  
<Customers CustomerID="LILAS" ContactName="Carlos Gonzlez">  
   <Orders CustomerID="LILAS" EmployeeID="3" OrderDate=  
           "1996-08-16T00:00:00">  
      <Order_x0020_Details OrderID="10283" ProductID="72" Quantity="3"/>  
   </Orders>  
</Customers>  
</ROOT>';  
  
--Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc;   
  
-- SELECT statement that uses the OPENXML rowset provider.  
SELECT    *  
FROM       OPENXML (@idoc, '/ROOT/Customers')   
EXEC sp_xml_removedocument @idoc;   
```  
  
## See Also  
 [Examples: Using OPENXML](../../relational-databases/xml/examples-using-openxml.md)  
  
