---
title: "Specifying Arithmetic Operators in XPath Queries (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "arithmetic operators"
  - "XPath operators [SQLXML]"
  - "XPath queries [SQLXML], arithmetic operators"
  - "operators [SQLXML]"
ms.assetid: fdfbc87d-759f-4abc-acf5-a21de01f78d3
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Specifying Arithmetic Operators in XPath Queries (SQLXML 4.0)
  The following example shows how arithmetic operators are specified in XPath queries. The XPath query in this example is specified against the mapping schema contained in SampleSchema1.xml. For information about this sample schema, see [Sample Annotated XSD Schema for XPath Examples &#40;SQLXML 4.0&#41;](sample-annotated-xsd-schema-for-xpath-examples-sqlxml-4-0.md).  
  
## Examples  
  
### A. Specify the * arithmetic operator  
 This XPath query returns **\<OrderDetail>** elements that satisfy the predicate specified:  
  
```  
/child::OrderDetail[@UnitPrice * @Quantity = 12.350]  
```  
  
 In the query, `child` is the axis and `OrderDetail` is the node test (TRUE if **OrderDetail** is an **\<element node>**, because the **\<element>** node is the primary node for the `child` axis). For all the **\<OrderDetail>** element nodes, the test in the predicate is applied, and only those nodes that satisfy the condition are returned.  
  
> [!NOTE]  
>  The numbers in XPath are double-precision floating-point numbers, and comparing floating-point numbers as in the example causes rounding.  
  
##### To test the XPath query against the mapping schema  
  
1.  Copy the [sample schema code](sample-annotated-xsd-schema-for-xpath-examples-sqlxml-4-0.md) and paste it into a text file. Save the file as SampleSchema1.xml.  
  
2.  Create the following template (ArithmeticOperatorA.xml) and save it in the directory where SampleSchema1.xml is saved.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
      <sql:xpath-query mapping-schema="SampleSchema1.xml">  
        /OrderDetail[@UnitPrice * @OrderQty = 12.350]  
      </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (SampleSchema1.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\MyDir\SampleSchema1.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
```  
Here is the partial result set of the template execution:    
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <OrderDetail ProductID="Prod-709" UnitPrice="6.175" OrderQty="2" UnitPriceDiscount="0" />   
  <OrderDetail ProductID="Prod-709" UnitPrice="6.175" OrderQty="2" UnitPriceDiscount="0" />   
  <OrderDetail ProductID="Prod-709" UnitPrice="6.175" OrderQty="2" UnitPriceDiscount="0" />   
  <OrderDetail ProductID="Prod-709" UnitPrice="6.175" OrderQty="2" UnitPriceDiscount="0" />   
  <OrderDetail ProductID="Prod-709" UnitPrice="6.175" OrderQty="2" UnitPriceDiscount="0" />   
  <OrderDetail ProductID="Prod-709" UnitPrice="6.175" OrderQty="2" UnitPriceDiscount="0" />   
  <OrderDetail ProductID="Prod-709" UnitPrice="6.175" OrderQty="2" UnitPriceDiscount="0" />   
  <OrderDetail ProductID="Prod-710" UnitPrice="6.175" OrderQty="2" UnitPriceDiscount="0" />   
   ...  
</ROOT>  
```  
  
  
