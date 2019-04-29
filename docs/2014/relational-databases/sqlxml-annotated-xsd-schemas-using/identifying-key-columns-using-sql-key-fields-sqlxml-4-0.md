---
title: "Identifying Key Columns Using sql:key-fields (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "nesting XML results"
  - "proper nesting in results [SQLXML]"
  - "sql:key-fields"
  - "XSD schemas [SQLXML], key columns"
  - "identifying key columns [SQLXML]"
  - "annotated XSD schemas, key columns"
  - "key columns [SQLXML]"
  - "relationships [SQLXML], key columns"
  - "hierarchical relationships [SQLXML]"
  - "key-fields annotation"
ms.assetid: 1a5ad868-8602-45c4-913d-6fbb837eebb0
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Identifying Key Columns Using sql:key-fields (SQLXML 4.0)
  When an XPath query is specified against an XSD schema, key information is required in most cases to obtain proper nesting in the result. Specifying the `sql:key-fields` annotation is a way of ensuring that the appropriate hierarchy is generated.  
  
> [!NOTE]  
>  To ensure proper nesting, it is recommended that you specify `sql:key-fields` for elements that map to tables. The XML produced is sensitive to the ordering of the underlying result set. If `sql:key-fields` is not specified, the XML generated might not be formed properly.  
  
 The value of `sql:key-fields` identifies the column(s) that uniquely identify the rows in the relation. If more than one column is required to uniquely identify a row, the column values are delimited by spaces.  
  
 You must use the `sql:key-fields` annotation when an element contains a **\<sql:relationship>** that is defined between the element and a child element but does not provide the primary key of the table that is specified in the parent element.  
  
## Examples  
 To create working samples using the following examples, you must meet certain requirements. For more information, see [Requirements for Running SQLXML Examples](../sqlxml/requirements-for-running-sqlxml-examples.md).  
  
### A. Producing the appropriate nesting when \<sql:relationship> does not provide sufficient information  
 This example shows where `sql:key-fields` must be specified.  
  
 Consider the following schema. The schema specifies a hierarchy between the **\<Order>** and **\<Customer>** elements in which the **\<Order>** element is the parent and the **\<Customer>** element is a child.  
  
 The **\<sql:relationship>** tag is used to specify the parent-child relationship. It identifies CustomerID in the Sales.SalesOrderHeader table as the parent key that refers to the CustomerID child key in the Sales.Customer table. The information provided in **\<sql:relationship>** is not sufficient to uniquely identify rows in the parent table (Sales.SalesOrderHeader). Therefore, without the `sql:key-fields` annotation, the hierarchy that is generated is inaccurate.  
  
 With `sql:key-fields` specified on **\<Order>**, the annotation uniquely identifies the rows in the parent (Sales.SalesOrderHeader table), and its child elements appear below its parent.  
  
 This is the schema:  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
<xsd:annotation>  
  <xsd:appinfo>  
    <sql:relationship name="OrdCust"  
        parent="Sales.SalesOrderHeader"  
        parent-key="CustomerID"  
        child="Sales.Customer"  
        child-key="CustomerID" />  
  </xsd:appinfo>  
</xsd:annotation>  
  <xsd:element name="Order" sql:relation="Sales.SalesOrderHeader"   
               sql:key-fields="SalesOrderID">  
   <xsd:complexType>  
     <xsd:sequence>  
     <xsd:element name="Customer" sql:relation="Sales.Customer"   
                       sql:relationship="OrdCust"  >  
       <xsd:complexType>  
         <xsd:attribute name="CustID" sql:field="CustomerID" />  
         <xsd:attribute name="SoldBy" sql:field="SalesPersonID" />  
       </xsd:complexType>  
     </xsd:element>  
     </xsd:sequence>  
     <xsd:attribute name="SalesOrderID" type="xsd:integer" />  
     <xsd:attribute name= "CustomerID" type="xsd:string" />  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
##### To create a working sample of this schema  
  
1.  Copy the schema code above and paste it into a text file. Save the file as KeyFields1.xml.  
  
2.  Copy the following template and paste it into a text file. Save the file as KeyFields1T.xml in the same directory where you saved KeyFields1.xml. The XPath query in the template returns all the **\<Order>** elements with a CustomerID of less than 3.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
        <sql:xpath-query mapping-schema="KeyFields1.xml">  
            /Order[@CustomerID < 3]  
        </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (KeyFields1.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\MyDir\KeyFields1.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML Queries](../sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 This is the partial result set:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
    <Order SalesOrderID="43860" CustomerID="1">  
       <Customer CustID="1" SoldBy="280"/>  
    </Order>  
    <Order SalesOrderID="44501" CustomerID="1">  
       <Customer CustID="1" SoldBy="280"/>  
    </Order>  
    <Order SalesOrderID="45283" CustomerID="1">  
       <Customer CustID="1" SoldBy="280"/>  
    </Order>  
    .....  
</ROOT>  
```  
  
### B. Specifying sql:key-fields to produce proper nesting in the result  
 In the following schema, there is no hierarchy specified using **\<sql:relationship>**. The schema still requires specifying the `sql:key-fields` annotation to uniquely identify employees in the HumanResources.Employee table.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:element name="HumanResources.Employee" sql:key-fields="EmployeeID" >  
   <xsd:complexType>  
     <xsd:sequence>  
        <xsd:element name="Title">  
          <xsd:complexType>  
            <xsd:simpleContent>  
              <xsd:extension base="xsd:string">  
                 <xsd:attribute name="EmployeeID" type="xsd:integer" />  
              </xsd:extension>  
            </xsd:simpleContent>  
          </xsd:complexType>  
        </xsd:element>  
     </xsd:sequence>  
   </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
##### To create a working sample of this schema  
  
1.  Copy the schema code above and paste it into a text file. Save the file as KeyFields2.xml.  
  
2.  Copy the following template and paste it into a text file. Save the file as KeyFields2T.xml in the same directory where you saved KeyFields2.xml. The XPath query in the template returns all the **\<HumanResources.Employee>** elements:  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
      <sql:xpath-query mapping-schema="KeyFields2.xml">  
        /HumanResources.Employee  
      </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (KeyFields2.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\MyDir\KeyFields2.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML Queries](../sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 This is the result:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <HumanResources.Employee>  
    <Title EmployeeID="1">Production Technician - WC60</Title>   
  </HumanResources.Employee>  
  <HumanResources.Employee>  
    <Title EmployeeID="2">Marketing Assistant</Title>   
  </HumanResources.Employee>  
  <HumanResources.Employee>  
    <Title EmployeeID="3">Engineering Manager</Title>   
  </HumanResources.Employee>  
  ...  
</ROOT>  
```  
  
  
