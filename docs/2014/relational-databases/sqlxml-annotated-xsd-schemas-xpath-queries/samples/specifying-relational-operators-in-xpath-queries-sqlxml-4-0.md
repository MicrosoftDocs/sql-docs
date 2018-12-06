---
title: "Specifying Relational Operators in XPath Queries (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "XPath queries [SQLXML], relational operators"
  - "relational operators [SQLXML]"
  - "XPath operators [SQLXML]"
  - "operators [SQLXML]"
ms.assetid: 177a0eb2-11ef-4459-a317-485a433ee769
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Specifying Relational Operators in XPath Queries (SQLXML 4.0)
  The following examples show how relational operators are specified in XPath queries. The XPath queries in these examples are specified against the mapping schema contained in SampleSchema1.xml. For information about this sample schema, see [Sample Annotated XSD Schema for XPath Examples &#40;SQLXML 4.0&#41;](sample-annotated-xsd-schema-for-xpath-examples-sqlxml-4-0.md).  
  
## Examples  
  
### A. Specify relational operator  
 This XPath query returns the child elements of the **\<Customer>** element where the **CustomerID** attribute value is "1" and where any child **\<Order>** elements contain an **\<OrderDetail>** child with a **OrderQty** attribute with a value greater than 3:  
  
```  
/child::Customer[@CustomerID="1"]/Order/OrderDetail[@OrderQty > 3]  
```  
  
 The predicate specified in the brackets filters the **\<Customer>** elements. Only the **\<Customer>** elements that have at least one **\<OrderDetail>** grandchild with a OrderQty attribute value greater than 3 are returned.  
  
 The `child` axis is the default. Therefore, the query can be specified as:  
  
```  
/Customer[@CustomerID="1"]/Order/OrderDetail[@OrderQty > 3]  
```  
  
##### To test the XPath query against the mapping schema  
  
1.  Copy the [sample schema code](sample-annotated-xsd-schema-for-xpath-examples-sqlxml-4-0.md) and paste it into a text file. Save the file as SampleSchema1.xml.  
  
2.  Create the following template (SpecifyRelationalA.xml) and save it in the directory where SampleSchema1.xml is saved.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
      <sql:xpath-query mapping-schema="SampleSchema1.xml">  
        /Customer[@CustomerID="1"]/Order/OrderDetail[@OrderQty > 3]  
      </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (SampleSchema1.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\MyDir\SampleSchema1.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 Here is the result set of the template execution:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <OrderDetail ProductID="Prod-760" UnitPrice="503.3507" OrderQty="4" UnitPriceDiscount="0" />   
  <OrderDetail ProductID="Prod-763" UnitPrice="503.3507" OrderQty="4" UnitPriceDiscount="0" />   
  <OrderDetail ProductID="Prod-766" UnitPrice="503.3507" OrderQty="4" UnitPriceDiscount="0" />   
  <OrderDetail ProductID="Prod-732" UnitPrice="440.1742" OrderQty="4" UnitPriceDiscount="0" />   
  <OrderDetail ProductID="Prod-757" UnitPrice="1049.7528" OrderQty="4" UnitPriceDiscount="0" />   
</ROOT>  
```  
  
### B. Specify relational operator in the XPath query and use Boolean function to compare the result  
 This query returns all the **\<Order>** element children of the context node that have an **SalesPersonID** attribute value that is less than 270:  
  
```  
/child::Customer/child::Order[(attribute::SalesPersonID < 270)=true()]  
```  
  
 A shortcut to the `attribute` axis (@) can be specified, and because the `child` axis is the default, it can be omitted from the query:  
  
```  
/Customer/Order[(@SalesPersonID < 270)=true()]  
```  
  
> [!NOTE]  
>  When this query is specified in a template, the < character must be entity encoded because the < character has special meaning in an XML document. In a template, use `<` to specify the < character.  
  
##### To test the XPath query against the mapping schema  
  
1.  Copy the [sample schema code](sample-annotated-xsd-schema-for-xpath-examples-sqlxml-4-0.md) and paste it into a text file. Save the file as SampleSchema1.xml.  
  
2.  Create the following template (SpecifyRelationalB.xml) and save it in the directory where SampleSchema1.xml is saved.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
        <sql:xpath-query mapping-schema="SampleSchema1.xml">  
            /Customer/Order[(@SalesPersonID<270)=true()]  
        </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (SampleSchema1.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\MyDir\SampleSchema1.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 Here is the partial result set of the template execution:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Order SalesOrderID="Ord-46613" SalesPersonID="268"   
         OrderDate="2002-07-01T00:00:00"   
         DueDate="2002-07-13T00:00:00"   
         ShipDate="2002-07-08T00:00:00">  
    <OrderDetail ProductID="Prod-739" UnitPrice="917.9363"   
                 OrderQty="2" UnitPriceDiscount="0" />   
    <OrderDetail ProductID="Prod-779" UnitPrice="1491.4221"   
                 OrderQty="1" UnitPriceDiscount="0" />   
    <OrderDetail ProductID="Prod-825" UnitPrice="242.1391"   
                 OrderQty="1" UnitPriceDiscount="0" />   
  </Order>  
  <Order SalesOrderID="Ord-71919" SalesPersonID="268"  
         OrderDate="2004-06-01T00:00:00"   
         DueDate="2004-06-13T00:00:00"   
         ShipDate="2004-06-08T00:00:00">  
    <OrderDetail ProductID="Prod-961" UnitPrice="534.492"   
                 OrderQty="1" UnitPriceDiscount="0" />   
    <OrderDetail ProductID="Prod-965" UnitPrice="534.492"   
                 OrderQty="1" UnitPriceDiscount="0" />   
    <OrderDetail ProductID="Prod-966" UnitPrice="1716.5304"   
                 OrderQty="1" UnitPriceDiscount="0" />   
  </Order>  
  ...  
</ROOT>  
```  
  
  
