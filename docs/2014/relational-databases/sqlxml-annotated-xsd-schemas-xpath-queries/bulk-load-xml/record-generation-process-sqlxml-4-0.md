---
title: "Record Generation Process (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "XML Bulk Load [SQLXML], record generation process"
  - "node scopes [SQLXML]"
  - "record subsets [SQLXML]"
  - "scope [SQLXML]"
  - "key ordering rules [SQLXML]"
  - "record generation process for bulk loads [SQLXML]"
  - "entering node scope [SQLXML]"
  - "bulk load [SQLXML], record generation process"
  - "leaving node scope [SQLXML]"
  - "schema mapping [SQLXML]"
ms.assetid: d8885bbe-6f15-4fb9-9684-ca7883cfe9ac
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Record Generation Process (SQLXML 4.0)
  XML Bulk Load processes the XML input data and prepares records for the appropriate tables in Microsoft [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The logic in XML Bulk Load determines when to generate a new record, what child element or attribute values to copy into the fields of the record, and when the record is complete and ready to be sent to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] for insertion.  
  
 XML Bulk Load does not load the entire XML input data into memory, and does not produce complete record sets before sending data to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. This is because XML input data can be a large document and loading the entire document in memory can be expensive. Instead, XML Bulk Load does the following:  
  
1.  Analyzes the mapping schema and prepares the necessary execution plan.  
  
2.  Applies the execution plan to the data in the input stream.  
  
 This sequential processing makes it important to provide the XML input data in a specific way. You must understand how XML Bulk Load analyzes the mapping schema and how the record generation process occurs. With this understanding, you can provide a mapping schema to XML Bulk Load that produces the results you want.  
  
 XML Bulk Load handles common mapping schema annotations, including column and table mappings (specified explicitly by using annotations or implicitly through the default mapping), and join relationships.  
  
> [!NOTE]  
>  It is assumed that you are familiar with annotated XSD or XDR mapping schemas. For more information about schemas, see [Introduction to Annotated XSD Schemas &#40;SQLXML 4.0&#41;](../../sqlxml/annotated-xsd-schemas/introduction-to-annotated-xsd-schemas-sqlxml-4-0.md) or [Annotated XDR Schemas &#40;Deprecated in SQLXML 4.0&#41;](../../sqlxml/annotated-xsd-schemas/annotated-xdr-schemas-deprecated-in-sqlxml-4-0.md).  
  
 Understanding record generation requires understanding the following concepts:  
  
-   Scope of a node  
  
-   Record Generation Rule  
  
-   Record subset and the Key Ordering Rule  
  
-   Exceptions to the Record Generation Rule  
  
## Scope of a Node  
 A node (an element or an attribute) in an XML document enters *into scope* when XML Bulk Load encounters it in the XML input data stream. For an element node, the start tag of the element brings the element in scope. For an attribute node, the attribute name brings the attribute in scope.  
  
 A node leaves scope when there is no more data for it: either at the end tag (in the case of an element node) or at the end of an attribute value (in the case of an attribute node).  
  
## Record Generation Rule  
 When a node (element or attribute) enters into scope, there is a potential for generating a record from that node. The record lives as long as the associated node is in scope. When the node goes out of scope, XML Bulk Load considers the generated record complete (with data) and sends it to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] for insertion.  
  
 For example, consider the following XSD schema fragment:  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:element name="Customer" sql:relation="Customers" >  
   <xsd:complexType>  
     <xsd:attribute name="CustomerID" type="xsd:string" />  
     <xsd:attribute name="CompanyName" type="xsd:string" />  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
 The schema specifies a **\<Customer>** element with **CustomerID** and **CompanyName** attributes. The `sql:relation` annotation maps the **\<Customer>** element to the Customers table.  
  
 Consider this fragment of an XML document:  
  
```  
<Customer CustomerID="1" CompanyName="xyz" />  
<Customer CustomerID="2" CompanyName="abc" />  
...  
```  
  
 When XML Bulk Load is provided with the schema described in the preceding paragraphs and XML data as input, it processes the nodes (elements and attributes) in the source data as follows:  
  
-   The start tag of the first **\<Customer>** element brings that element in scope. This node maps to the Customers table. Therefore, XML Bulk Load generates a record for the Customers table.  
  
-   In the schema, all attributes of the **\<Customer>** element map to columns of the Customers table. As these attributes enter into scope, XML Bulk Load copies their values to the customer record that is already generated by the parent scope.  
  
-   When XML Bulk Load reaches the end tag for the **\<Customer>** element, the element goes out of scope. This causes XML Bulk Load to consider the record complete and send it to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 XML Bulk Load follows this process for each subsequent **\<Customer>** element.  
  
> [!IMPORTANT]  
>  In this model, because a record is inserted when the end tag is reached (or the node is out of scope), you must define all of the data that is associated with the record within the scope of the node.  
  
## Record Subset and the Key Ordering Rule  
 When you specify a mapping schema that uses `<sql:relationship>`, the subset term refers to the set of records that is generated on the foreign side of the relationship. In the following example, the CustOrder records are on the foreign side, `<sql:relationship>`.  
  
 For example, suppose a database contains the following tables:  
  
-   Cust (CustomerID, CompanyName, City)  
  
-   CustOrder (CustomerID, OrderID)  
  
 The CustomerID in the CustOrder table is a foreign key that refers to the CustomerID primary key in the Cust table.  
  
 Now, consider the XML view as specified in the following annotated XSD schema. This schema uses `<sql:relationship>` to specify the relationship between the Cust and CustOrder tables.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
<xsd:annotation>  
  <xsd:appinfo>  
    <sql:relationship name="CustCustOrder"  
          parent="Cust"  
          parent-key="CustomerID"  
          child="CustOrder"  
          child-key="CustomerID" />  
  </xsd:appinfo>  
</xsd:annotation>  
  
  <xsd:element name="Customers" sql:relation="Cust" >  
   <xsd:complexType>  
     <xsd:sequence>  
       <xsd:element name="CustomerID"  type="xsd:integer" />  
       <xsd:element name="CompanyName" type="xsd:string" />  
       <xsd:element name="City"        type="xsd:string" />  
       <xsd:element name="Order"   
                          sql:relation="CustOrder"  
                          sql:relationship="CustCustOrder" >  
         <xsd:complexType>  
          <xsd:attribute name="OrderID" type="xsd:integer" />  
         </xsd:complexType>  
       </xsd:element>  
     </xsd:sequence>  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
 The sample XML data and the steps to create a working sample are given below.  
  
-   When a **\<Customer>** element node in the XML data file enters into scope, XML Bulk Load generates a record for the Cust table. XML Bulk Load then copies the necessary column values (CustomerID, CompanyName, and City) from the **\<CustomerID>**, **\<CompanyName>**, and the **\<City>** child elements as these elements enter into scope.  
  
-   When an **\<Order>** element node enters into scope, XML Bulk Load generates a record for the CustOrder table. XML Bulk Load copies the value of the **OrderID** attribute to this record. The value required for the CustomerID column is obtained from the **\<CustomerID>** child element of the **\<Customer>** element. XML Bulk Load uses the information that is specified in `<sql:relationship>` to obtain the CustomerID foreign key value for this record, unless the **CustomerID** attribute was specified in the **\<Order>** element. The general rule is that if the child element explicitly specifies a value for the foreign key attribute, XML Bulk Load uses that value and does not obtain the value from the parent element by using the specified `<sql:relationship>`. As this **\<Order>** element node goes out of scope, XML Bulk Load sends the record to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and then processes all the subsequent **\<Order>** element nodes in the same manner.  
  
-   Finally, the **\<Customer>** element node goes out of scope. At that time, XML Bulk Load sends the customer record to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. XML Bulk Load follows this process for all the subsequent customers in the XML data stream.  
  
 Here are two observations about the mapping schema:  
  
-   When the schema satisfies the "containment" rule (for example, all data that is associated with the customer and the order is defined within the scope of the associated **\<Customer>** and **\<Order>** element nodes), the bulk load succeeds.  
  
-   In describing the **\<Customer>** element, its child elements are specified in the appropriate order. In this case, the **\<CustomerID>** child element is specified before the **\<Order>** child element. This means that in the input XML data file, the **\<CustomerID>** element value is available as the foreign key value when the **\<Order>** element enters into scope. The key attributes are specified first; this is the "Key Ordering Rule."  
  
     If you specify the **\<CustomerID>** child element after the **\<Order>** child element, the value is not available when the **\<Order>** element enters into scope. When the **\</Order>** end tag is then read, the record for CustOrder table is considered complete and is inserted in the CustOrder table with a NULL value for the CustomerID column, which is not the desired result.  
  
#### To create a working sample  
  
1.  Save the schema that is provided in this example as SampleSchema.xml.  
  
2.  Create these tables:  
  
    ```  
    CREATE TABLE Cust (  
                  CustomerID     int         PRIMARY KEY,  
                  CompanyName    varchar(20) NOT NULL,  
                  City           varchar(20) DEFAULT 'Seattle')  
    GO  
    CREATE TABLE CustOrder (  
                 OrderID        int         PRIMARY KEY,  
                 CustomerID     int         FOREIGN KEY REFERENCES                                          Cust(CustomerID))  
    GO  
    ```  
  
3.  Save the following sample XML input data as SampleXMLData.xml:  
  
    ```  
    <ROOT>  
      <Customers>  
        <CustomerID>1111</CustomerID>  
        <CompanyName>Hanari Carnes</CompanyName>  
        <City>NY</City>   
        <Order OrderID="1" />  
        <Order OrderID="2" />  
      </Customers>  
  
      <Customers>  
        <CustomerID>1112</CustomerID>  
        <CompanyName>Toms Spezialitten</CompanyName>  
        <City>LA</City>  
        <Order OrderID="3" />  
      </Customers>  
      <Customers>  
        <CustomerID>1113</CustomerID>  
        <CompanyName>Victuailles en stock</CompanyName>  
        <Order OrderID="4" />  
    </Customers>  
    </ROOT>  
    ```  
  
4.  To execute XML Bulk Load, save and execute the following [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual Basic Scripting Edition (VBScript) example (BulkLoad.vbs):  
  
    ```  
    set objBL = CreateObject("SQLXMLBulkLoad.SQLXMLBulkload.4.0")  
    objBL.ConnectionString = "provider=SQLOLEDB;data source=localhost;database=tempdb;integrated security=SSPI"  
    objBL.ErrorLogFile = "c:\error.log"  
    objBL.CheckConstraints = True  
    objBL.Execute "c:\SampleSchema.xml", "c:\SampleXMLData.xml"  
    set objBL=Nothing  
    ```  
  
## Exceptions to the Record Generation Rule  
 XML Bulk Load does not generate a record for a node when it enters into scope if that node is either an IDREF or IDREFS type. You must make sure that a complete description of the record occurs at some place in the schema. The `dt:type="nmtokens"` annotations are ignored just as the IDREFS type is ignored.  
  
 For example, consider the following XSD schema that describes **\<Customer>** and **\<Order>** elements. The **\<Customer>** element includes an **OrderList** attribute of the IDREFS type. The `<sql:relationship>` tag specifies the one-to-many relationship between the customer and list of orders.  
  
 This is the schema:  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
<xsd:annotation>  
  <xsd:appinfo>  
    <sql:relationship name="CustCustOrder"  
                 parent="Cust"  
                 parent-key="CustomerID"  
                 child="CustOrder"  
                 child-key="CustomerID" />  
  </xsd:appinfo>  
</xsd:annotation>  
  
  <xsd:element name="Customers" sql:relation="Cust" >  
   <xsd:complexType>  
    <xsd:attribute name="CustomerID" type="xsd:integer" />  
    <xsd:attribute name="CompanyName" type="xsd:string" />  
    <xsd:attribute name="City" type="xsd:string" />  
    <xsd:attribute name="OrderList"   
                       type="xsd:IDREFS"   
                       sql:relation="CustOrder"   
                       sql:field="OrderID"  
                       sql:relationship="CustCustOrder" >  
    </xsd:attribute>  
  </xsd:complexType>  
 </xsd:element>  
  
  <xsd:element name="Order" sql:relation="CustOrder" >  
   <xsd:complexType>  
    <xsd:attribute name="OrderID" type="xsd:string" />  
    <xsd:attribute name="CustomerID" type="xsd:integer" />  
    <xsd:attribute name="OrderDate" type="xsd:date" />  
  </xsd:complexType>  
 </xsd:element>  
</xsd:schema>  
```  
  
 Because Bulk Load ignores the nodes of IDREFS type, there is no record generation when the **OrderList** attribute node enters into scope. Therefore, if you want the order records added to the Orders table, you must describe those orders somewhere in the schema. In this schema, specifying the **\<Order>** element ensures that XML Bulk Load adds the order records to the Orders table. The **\<Order>** element describes all the attributes that are required to fill the record for the CustOrder table.  
  
 You must ensure that the **CustomerID** and **OrderID** values in the **\<Customer>** element match the values in the **\<Order>** element. You are responsible for maintaining referential integrity.  
  
#### To test a working sample  
  
1.  Create these tables:  
  
    ```  
    CREATE TABLE Cust (  
                  CustomerID     int          PRIMARY KEY,  
                  CompanyName    varchar(20)  NOT NULL,  
                  City           varchar(20)  DEFAULT 'Seattle')  
    GO  
    CREATE TABLE CustOrder (  
                  OrderID        varchar(10) PRIMARY KEY,  
                  CustomerID     int         FOREIGN KEY REFERENCES                                          Cust(CustomerID),  
                  OrderDate      datetime DEFAULT '2000-01-01')  
    GO  
    ```  
  
2.  Save the mapping schema provided in this example as SampleSchema.xml.  
  
3.  Save the following sample XML data as SampleXMLData.xml:  
  
    ```  
    <ROOT>  
      <Customers CustomerID="1111" CompanyName="Sean Chai" City="NY"  
                 OrderList="Ord1 Ord2" />  
      <Customers CustomerID="1112" CompanyName="Dont Know" City="LA"  
                 OrderList="Ord3 Ord4" />  
      <Order OrderID="Ord1" CustomerID="1111" OrderDate="1999-01-01" />  
      <Order OrderID="Ord2" CustomerID="1111" OrderDate="1999-02-01" />  
      <Order OrderID="Ord3" CustomerID="1112" OrderDate="1999-03-01" />  
      <Order OrderID="Ord4" CustomerID="1112" OrderDate="1999-04-01" />  
    </ROOT>  
    ```  
  
4.  To execute XML Bulk Load, save and execute this VBScript example (SampleVB.vbs):  
  
    ```  
    set objBL = CreateObject("SQLXMLBulkLoad.SQLXMLBulkload.4.0")  
    objBL.ConnectionString = "provider=SQLOLEDB;data source=localhost;database=tempdb;integrated security=SSPI"  
    objBL.ErrorLogFile = "c:\error.log"  
    objBL.CheckConstraints=True  
    objBL.Execute "c:\SampleSchema.xml", "c:\SampleXMLData.xml"  
    set objBL=Nothing  
    ```  
  
  
