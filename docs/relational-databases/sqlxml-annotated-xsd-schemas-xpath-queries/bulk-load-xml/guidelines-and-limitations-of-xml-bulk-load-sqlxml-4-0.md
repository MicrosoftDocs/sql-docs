---
title: "Guidelines and Limitations of XML Bulk Load (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "XML Bulk Load [SQLXML], about XML Bulk Load"
  - "bulk load [SQLXML], about bulk load"
ms.assetid: c5885d14-c7c1-47b3-a389-455e99a7ece1
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Guidelines and Limitations of XML Bulk Load (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  When you use XML Bulk Load, you should be familiar with the following guidelines and limitations:  
  
-   Inline schemas are not supported.  
  
     If you have an inline schema in the source XML document, XML Bulk Load ignores that schema. You specify the mapping schema for XML Bulk Load external to the XML data. You cannot specify the mapping schema at a node by using the **xmlns="x:schema"** attribute.  
  
-   An XML document is checked for being well-formed, but it is not validated.  
  
     XML Bulk Load checks the XML document to determine whether it is well-formed-that is, to ensure that the XML conforms to the syntax requirements of the World Wide Web Consortium's XML 1.0 recommendation. If the document is not well-formed, XML Bulk Load cancels processing and returns an error. The only exception to this is when the document is a fragment (for example, the document has no single root element), in which case XML Bulk Load will load the document.  
  
     XML Bulk Load does not validate the document with respect to any XML-Data or DTD schema that is defined or referenced within the XML data file. In addition, XML Bulk Load does not validate the XML data file against the mapping schema supplied.  
  
-   Any XML prolog information is ignored.  
  
     XML Bulk Load ignores all the information before and after the \<root> element in the XML document. For example, XML Bulk Load ignores any XML declarations, internal DTD definitions, external DTD references, comments, and so on.  
  
-   If you have a mapping schema that defines a primary key/foreign key relationship between two tables (such as between Customer and CustOrder), the table with the primary key must be described first in the schema. The table with the foreign key column must appear later in the schema. The reason for this is that the order in which the tables are identified in the schema is the order that is used to load them into the database.For example, the following XDR schema will produce an error when it is used in XML Bulk Load because the **\<Order>** element is described before the **\<Customer>** element. The CustomerID column in CustOrder is a foreign key column that refers to the CustomerID primary key column in the Cust table.  
  
    ```  
    <?xml version="1.0" ?>  
    <Schema xmlns="urn:schemas-microsoft-com:xml-data"   
            xmlns:dt="urn:schemas-microsoft-com:xml:datatypes"    
            xmlns:sql="urn:schemas-microsoft-com:xml-sql" >  
  
        <ElementType name="Order" sql:relation="CustOrder" >  
          <AttributeType name="OrderID" />  
          <AttributeType name="CustomerID" />  
          <attribute type="OrderID" />  
          <attribute type="CustomerID" />  
        </ElementType>  
  
       <ElementType name="CustomerID" dt:type="int" />  
       <ElementType name="CompanyName" dt:type="string" />  
       <ElementType name="City" dt:type="string" />  
  
       <ElementType name="root" sql:is-constant="1">  
          <element type="Customers" />  
       </ElementType>  
       <ElementType name="Customers" sql:relation="Cust"   
                         sql:overflow-field="OverflowColumn"  >  
          <element type="CustomerID" sql:field="CustomerID" />  
          <element type="CompanyName" sql:field="CompanyName" />  
          <element type="City" sql:field="City" />  
          <element type="Order" >   
               <sql:relationship  
                   key-relation="Cust"  
                    key="CustomerID"  
                    foreign-key="CustomerID"  
                    foreign-relation="CustOrder" />  
          </element>  
       </ElementType>  
    </Schema>  
    ```  
  
-   If the schema does not specify overflow columns by using the **sql:overflow-field** annotation, XML Bulk Load ignores any data that is present in the XML document but is not described in the mapping schema.  
  
     XML Bulk Load applies the mapping schema that you have specified whenever it encounters known tags in the XML data stream. It ignores data that is present in the XML document but is not described in the schema. For example, assume you have a mapping schema that describes a **\<Customer>** element. The XML data file has an **\<AllCustomers>** root tag (which is not described in the schema) that encloses all the **\<Customer>** elements:  
  
    ```  
    <AllCustomers>  
      <Customer>...</Customer>  
      <Customer>...</Customer>  
       ...  
    </AllCustomers>  
    ```  
  
     In this case, XML Bulk Load ignores the **\<AllCustomers>** element and begins mapping at the **\<Customer>** element. XML Bulk Load ignores the elements that are not described in the schema but are present in the XML document.  
  
     Consider another XML source data file that contains **\<Order>** elements. These elements are not described in the mapping schema:  
  
    ```  
    <AllCustomers>  
      <Customer>...</Customer>  
        <Order> ... </Order>  
        <Order> ... </Order>  
         ...  
      <Customer>...</Customer>  
        <Order> ... </Order>  
        <Order> ... </Order>  
         ...  
      ...  
    </AllCustomers>  
    ```  
  
     XML Bulk Load ignores these **\<Order>** elements. But if you use the **sql:overflow-field**annotation in the schema to identify a column as an overflow column, XML Bulk Load stores all unconsumed data in this column.  
  
-   CDATA sections and entity references are translated to their string equivalents before they are stored in the database.  
  
     In this example, a CDATA section wraps the value for the **\<City>** element. XML Bulk Load extracts the string value ("NY") before it inserts the **\<City>** element into the database.  
  
    ```  
    <City><![CDATA[NY]]> </City>  
    ```  
  
     XML Bulk Load does not preserve entity references.  
  
-   If the mapping schema specifies the default value for an attribute and the XML source data does not contain that attribute, XML Bulk Load uses the default value.  
  
     The following sample XDR schema assigns a default value to the **HireDate** attribute:  
  
    ```  
    <?xml version="1.0" ?>  
    <Schema xmlns="urn:schemas-microsoft-com:xml-data"   
            xmlns:dt="urn:schemas-microsoft-com:xml:datatypes"    
            xmlns:sql="urn:schemas-microsoft-com:xml-sql" >  
       <ElementType name="root" sql:is-constant="1">  
          <element type="Customers" />  
       </ElementType>  
  
       <ElementType name="Customers" sql:relation="Cust3" >  
          <AttributeType name="CustomerID" dt:type="int"  />  
          <AttributeType name="HireDate"  default="2000-01-01" />  
          <AttributeType name="Salary"   />  
  
          <attribute type="CustomerID" sql:field="CustomerID" />  
          <attribute type="HireDate"   sql:field="HireDate"  />  
          <attribute type="Salary"     sql:field="Salary"    />  
       </ElementType>  
    </Schema>  
    ```  
  
     In this XML data, the **HireDate** attribute is missing from the second **\<Customers>** element. When XML Bulk Load inserts the second **\<Customers>** element into the database, it uses the default value that is specified in the schema.  
  
    ```  
    <ROOT>  
      <Customers CustomerID="1" HireDate="1999-01-01" Salary="10000" />  
      <Customers CustomerID="2" Salary="10000" />  
    </ROOT>  
    ```  
  
-   The **sql:url-encode** annotation is not supported:  
  
     You cannot specify a URL in the XML data input and expect Bulk Load to read data from that location.  
  
     The tables that are identified in the mapping schema are created (the database must exist). If one or more of the tables already exists in the database, the SGDropTables property determines whether these preexisting tables are to be dropped and re-created.  
  
-   If you specify the SchemaGen property (for example, SchemaGen = true), the tables that are identified in the mapping schema are created. But SchemaGen does not create any constraints (such as the PRIMARY KEY/FOREIGN KEY constraints) on these tables with one exception: If the XML nodes that constitute the primary key in a relationship are defined as having an XML type of ID (that is, **type="xsd:ID"** for XSD) AND the SGUseID property is set to True for SchemaGen, then not only are primary keys created from the ID typed nodes, but primary key/foreign key relationships are created from mapping schema relationships.  
  
-   SchemaGen does not use XSD schema facets and extensions to generate the relational [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] schema.  
  
-   If you specify the SchemaGen property (for example, SchemaGen = true) on Bulk Load, only tables (and not views of shared name) that are specified are updated.  
  
-   SchemaGen only provides basic functionality for generating the relational schema from annotated XSD. The user should modify the generated tables manually, if needed.  
  
-   Where more than one relationship exists between tables, SchemaGen tries to create a single relationship that includes all the keys involved between the two tables. This limitation might be the cause of a [!INCLUDE[tsql](../../../includes/tsql-md.md)] error.  
  
-   When you are bulk loading XML data into a database, there must be at least one attribute or child element in the mapping schema that is mapped to a database column.  
  
-   If you are inserting date values by using XML Bulk Load, the values must be specified in the (-)CCYY-MM-DD((+-)TZ) format. This is the standard XSD format for the date.  
  
-   Some property flags are not compatible with other property flags. For example, bulk load does not support **Ignoreduplicatekeys=true** together with **Keepidentity=false**. When **Keepidentity=false**, bulk load expects the server to generate the key values. Tables should have an **IDENTITY** constraint on the key. The server will not generate duplicate keys, which means there is no need for **Ignoreduplicatekeys** to be set to **true**. **Ignoreduplicatekeys** should be set to **true** only when uploading primary key values from the incoming data into a table that has rows and there is a potential for conflict of primary key values.  
  
  
