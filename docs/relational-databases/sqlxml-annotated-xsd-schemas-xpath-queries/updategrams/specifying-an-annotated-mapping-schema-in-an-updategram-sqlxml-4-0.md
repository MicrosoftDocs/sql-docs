---
title: "Specifying an Annotated Mapping Schema in an Updategram (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "annotated XSD schemas, updategrams"
  - "data types [SQLXML], mapping schema in updategrams"
  - "updategrams [SQLXML], annotated mapping schemas"
  - "annotated XDR schemas, updategrams"
  - "inverse attribute"
  - "parent-child relationships [SQLXML]"
  - "mapping-schema attribute"
  - "mapping schema [SQLXML], updategrams"
  - "sql:inverse"
ms.assetid: 2e266ed9-4cfb-434a-af55-d0839f64bb9a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Specifying an Annotated Mapping Schema in an Updategram (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  This topic explains how the mapping schema (XSD or XDR) that is specified in an updategram is used to process the updates. In an updategram, you can provide the name of an annotated mapping schema to use in mapping the elements and attributes in the updategram to tables and columns in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. When a mapping schema is specified in an updategram, the element and attribute names that are specified in the updategram must map to the elements and attributes in the mapping schema.  
  
 To specify a mapping schema, you use the **mapping-schema** attribute of the **\<sync>** element. The following examples show two updategrams: one that uses a simple mapping schema, and one that uses a more complex schema.  
  
> [!NOTE]  
>  This documentation assumes that you are familiar with templates and mapping schema support in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. For more information, see [Introduction to Annotated XSD Schemas &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml/annotated-xsd-schemas/introduction-to-annotated-xsd-schemas-sqlxml-4-0.md). For legacy applications that use XDR, see [Annotated XDR Schemas &#40;Deprecated in SQLXML 4.0&#41;](../../../relational-databases/sqlxml/annotated-xsd-schemas/annotated-xdr-schemas-deprecated-in-sqlxml-4-0.md).  
  
## Dealing with Data Types  
 If the schema specifies the **image**, **binary**, or **varbinary**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type (by using **sql:datatype**) and does not specify an XML data type, the updategram assumes that the XML data type is **binary base 64**. If your data is **bin.base** type, you must explicitly specify the type (**dt:type=bin.base** or **type="xsd:hexBinary"**).  
  
 If the schema specifies the **dateTime**, **date**, or **time** XSD data type, you must also specify the corresponding [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type by using **sql:datatype="dateTime"**.  
  
 When handling parameters of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **money** type, you must explicitly specify **sql:datatype="money"** on the appropriate node in the mapping schema.  
  
## Examples  
 To create working samples using the following examples, you must meet the requirements specified in [Requirements for Running SQLXML Examples](../../../relational-databases/sqlxml/requirements-for-running-sqlxml-examples.md).  
  
### A. Creating an updategram with a simple mapping schema  
 The following XSD schema (SampleSchema.xml) is a mapping schema that maps the **\<Customer>** element to the Sales.Customer table:  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:element name="Customer" sql:relation="Sales.Customer" >  
   <xsd:complexType>  
        <xsd:attribute name="CustID"    
                       sql:field="CustomerID"   
                       type="xsd:string" />  
        <xsd:attribute name="RegionID"    
                       sql:field="TerritoryID"    
                       type="xsd:string" />  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
 The following updategram inserts a record into the Sales.Customer table and relies on the previous mapping schema to properly map this data to the table. Notice that the updategram uses the same element name, **\<Customer>**, as defined in the schema. This is mandatory because the updategram specifies a particular schema.  
  
##### To test the updategram  
  
1.  Copy the schema code above and paste it into a text file. Save the file as SampleUpdateSchema.xml.  
  
2.  Copy the updategram template below and paste it into a text file. Save the file as SampleUpdategram.xml in the same directory where you saved SampleUpdateSchema.xml.  
  
    ```  
    <ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
      <updg:sync mapping-schema="SampleUpdateSchema.xml">  
        <updg:before>  
          <Customer CustID="1" RegionID="1"  />  
        </updg:before>  
        <updg:after>  
          <Customer CustID="1" RegionID="2" />  
        </updg:after>  
      </updg:sync>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (SampleUpdateSchema.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\SqlXmlTest\SampleUpdateSchema.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 This is the equivalent XDR schema:  
  
```  
<?xml version="1.0" ?>  
   <Schema xmlns="urn:schemas-microsoft-com:xml-data"   
         xmlns:dt="urn:schemas-microsoft-com:datatypes"   
         xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
     <ElementType name="Customer" sql:relation="Sales.Customer" >  
       <AttributeType name="CustID" />  
       <AttributeType name="RegionID" />  
  
       <attribute type="CustID" sql:field="CustomerID" />  
       <attribute type="RegionID" sql:field="TerritoryID" />  
     </ElementType>  
   </Schema>   
```  
  
### B. Inserting a record by using the parent-child relationship specified in the mapping schema  
 Schema elements can be related. The **\<sql:relationship>** element specifies the parent-child relationship between the schema elements. This information is used to update corresponding tables that have primary-key/foreign-key relationship.  
  
 The following mapping schema (SampleSchema.xml) consists of two elements, **\<Order>** and **\<OD>**:  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
<xsd:annotation>  
  <xsd:appinfo>  
    <sql:relationship name="OrderOD"  
          parent="Sales.SalesOrderHeader"  
          parent-key="SalesOrderID"  
          child="Sales.SalesOrderDetail"  
          child-key="SalesOrderID" />  
  </xsd:appinfo>  
</xsd:annotation>  
  
  <xsd:element name="Order" sql:relation="Sales.SalesOrderHeader" >  
   <xsd:complexType>  
     <xsd:sequence>  
        <xsd:element name="OD"   
                     sql:relation="Sales.SalesOrderDetail"  
                     sql:relationship="OrderOD" >  
           <xsd:complexType>  
              <xsd:attribute name="SalesOrderID"   type="xsd:integer" />  
              <xsd:attribute name="ProductID" type="xsd:integer" />  
             <xsd:attribute name="UnitPrice"  type="xsd:decimal" />  
             <xsd:attribute name="OrderQty"   type="xsd:integer" />  
             <xsd:attribute name="UnitPriceDiscount"   type="xsd:decimal" />  
  
           </xsd:complexType>  
        </xsd:element>  
     </xsd:sequence>  
        <xsd:attribute name="CustomerID"   type="xsd:string" />   
        <xsd:attribute name="SalesOrderID"  type="xsd:integer" />  
        <xsd:attribute name="OrderDate"  type="xsd:date" />  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
 The following updategram uses this XSD schema to add a new order detail record (an **\<OD>** element in the **\<after>** block) for order 43860. The **mapping-schema** attribute is used to specify the mapping schema in the updategram.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
  <updg:sync mapping-schema="SampleUpdateSchema.xml" >  
    <updg:before>  
       <Order SalesOrderID="43860" />  
    </updg:before>  
    <updg:after>  
      <Order SalesOrderID="43860" >  
           <OD ProductID="753" UnitPrice="$10.00"  
               Quantity="5" Discount="0.0" />  
      </Order>  
    </updg:after>  
  </updg:sync>  
</ROOT>  
```  
  
##### To test the updategram  
  
1.  Copy the schema code above and paste it into a text file. Save the file as SampleUpdateSchema.xml.  
  
2.  Copy the updategram template above and paste it into a text file. Save the file as SampleUpdategram.xml in the same directory where you saved SampleUpdateSchema.xml.  
  
     The directory path specified for the mapping schema (SampleUpdateSchema.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\SqlXmlTest\SampleUpdateSchema.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 This is the equivalent XDR schema:  
  
```  
<?xml version="1.0" ?>  
<Schema xmlns="urn:schemas-microsoft-com:xml-data"  
        xmlns:dt="urn:schemas-microsoft-com:datatypes"  
        xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  
<ElementType name="OD" sql:relation="Sales.SalesOrderDetail" >  
    <AttributeType name="SalesOrderID" />  
    <AttributeType name="ProductID" />  
    <AttributeType name="UnitPrice"  dt:type="fixed.14.4" />  
    <AttributeType name="OrderQty" />  
    <AttributeType name="UnitPriceDiscount" />  
  
    <attribute type="SalesOrderID" />  
    <attribute type="ProductID" />  
    <attribute type="UnitPrice" />  
    <attribute type="OrderQty" />  
    <attribute type="UnitPriceDiscount" />  
</ElementType>  
  
<ElementType name="Order" sql:relation="Sales.SalesOrderHeader" >  
    <AttributeType name="CustomerID" />  
    <AttributeType name="SalesOrderID" />  
    <AttributeType name="OrderDate" />  
  
    <attribute type="CustomerID" />  
    <attribute type="SalesOrderID" />  
    <attribute type="OrderDate" />  
    <element type="OD" >  
             <sql:relationship   
                   key-relation="Sales.SalesOrderHeader"  
                   key="SalesOrderID"  
                   foreign-key="SalesOrderID"  
                   foreign-relation="Sales.SalesOrderDetail" />  
    </element>  
</ElementType>  
</Schema>  
```  
  
### C. Inserting a record by using the parent-child relationship and inverse annotation specified in the XSD schema  
 This example illustrates how the updategram logic uses the parent-child relationship specified in the XSD to process updates, and how the **inverse** annotation is used. For more information about the **inverse** annotation, see [Specifying the sql:inverse Attribute on sql:relationship &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-using/specifying-the-sql-inverse-attribute-on-sql-relationship-sqlxml-4-0.md).  
  
 This example assumes that the following tables are in the **tempdb** database:  
  
-   `Cust (CustomerID, CompanyName)`, where `CustomerID` is the primary key  
  
-   `Ord (OrderID, CustomerID)`, where `CustomerID` is a foreign key that refers to the `CustomerID` primary key in the `Cust` table.  
  
 The updategram uses the following XSD schema to insert records into the Cust and Ord tables :  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
<xsd:annotation>  
  <xsd:appinfo>  
       <sql:relationship name="OrdCust" inverse="true"  
                  parent="Ord"  
                  parent-key="CustomerID"  
                  child-key="CustomerID"  
                  child="Cust"/>  
  </xsd:appinfo>  
</xsd:annotation>  
  
<xsd:element name="Order" sql:relation="Ord">  
  <xsd:complexType>  
    <xsd:sequence>  
      <xsd:element ref="Customer" sql:relationship="OrdCust"/>  
    </xsd:sequence>  
    <xsd:attribute name="OrderID"   type="xsd:int"/>  
    <xsd:attribute name="CustomerID" type="xsd:string"/>  
  </xsd:complexType>  
</xsd:element>  
  
<xsd:element name="Customer" sql:relation="Cust">  
  <xsd:complexType>  
     <xsd:attribute name="CustomerID"  type="xsd:string"/>  
    <xsd:attribute name="CompanyName" type="xsd:string"/>  
  </xsd:complexType>  
</xsd:element>  
  
</xsd:schema>  
```  
  
 The XSD schema in this example has **\<Customer>** and **\<Order>** elements, and it specifies a parent-child relationship between the two elements. It identifies **\<Order>** as the parent element and **\<Customer>** as the child element.  
  
 The updategram processing logic uses the information about the parent-child relationship to determine the order in which records are inserted into tables. In this example, the updategram logic first attempts to insert a record into the Ord table (because **\<Order>** is the parent) and then attempts to insert a record into the Cust table (because **\<Customer>** is the child). However, because of the primary key/foreign key information that is contained in the database table schema, this insert operation causes a foreign key violation in the database and the insert fails.  
  
 To instruct the updategram logic to reverse the parent-child relationship during the update operation, the **inverse** annotation is specified on the **\<relationship>** element. As a result, records are added first in the Cust table and then in the Ord table, and the operation succeeds.  
  
 The following updategram inserts an order (OrderID=2) in the Ord table and a customer (CustomerID='AAAAA') in the Cust table by using the specified XSD schema:  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
  <updg:sync mapping-schema="SampleUpdateSchema.xml" >  
    <updg:before/>  
    <updg:after>  
      <Order OrderID="2" CustomerID="AAAAA" >  
        <Customer CustomerID="AAAAA" CompanyName="AAAAA Company" />  
      </Order>  
    </updg:after>  
  </updg:sync>  
</ROOT>  
```  
  
##### To test the updategram  
  
1.  Create these tables in the **tempdb** database:  
  
    ```  
    USE tempdb  
    CREATE TABLE Cust(CustomerID varchar(5) primary key,   
                      CompanyName varchar(20))  
    GO  
    CREATE TABLE Ord (OrderID int primary key,   
                      CustomerID varchar(5) references Cust(CustomerID))  
    GO  
    ```  
  
2.  Copy the schema code above and paste it into a text file. Save the file as SampleUpdateSchema.xml.  
  
3.  Copy the updategram template above and paste it into a text file. Save the file as SampleUpdategram.xml in the same directory where you saved SampleUpdateSchema.xml.  
  
     The directory path specified for the mapping schema (SampleUpdateSchema.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\SqlXmlTest\SampleUpdateSchema.xml"  
    ```  
  
4.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
## See Also  
 [Updategram Security Considerations &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/security/updategram-security-considerations-sqlxml-4-0.md)  
  
  
