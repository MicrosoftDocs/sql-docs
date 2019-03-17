---
title: "Introduction to Updategrams (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "explicit schema mapping [SQLXML]"
  - "updategrams [SQLXML], mapping schema specifying"
  - "namespaces [SQLXML]"
  - "updategrams [SQLXML], about updategrams"
  - "attribute-centric mapping"
  - "invalid characters [SQLXML]"
  - "element-centric mapping [SQLXML]"
  - "mapping schema [SQLXML], updategrams"
  - "namespaces [SQLXML], updategrams"
  - "executing updategrams [SQLXML]"
  - "implicit schema mapping"
ms.assetid: cfe24e82-a645-4f93-ab16-39c21f90cce6
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Introduction to Updategrams (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  You can modify (insert, update, or delete) a database in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] from an existing XML document by using an updategram or the OPENXML [!INCLUDE[tsql](../../../includes/tsql-md.md)] function.  
  
 The OPENXML function modifies a database by shredding the existing XML document and providing a rowset that can be passed to an INSERT, UPDATE, or DELETE statement. With OPENXML, operations are performed directly against the database tables. Therefore, OPENXML is most appropriate wherever rowset providers, such as a table, can appear as a source.  
  
 Like OPENXML, an updategram allows you to insert, update, or delete data in the database; however, an updategram works against the XML views provided by the annotated XSD (or an XDR) schema; for example, the updates are applied to the XML view provided by the mapping schema. The mapping schema, in turn, has the necessary information to map XML elements and attributes to the corresponding database tables and columns. The updategram uses this mapping information to update the database tables and columns.  
  
> [!NOTE]  
>  This documentation assumes that you are familiar with templates and mapping schema support in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. For more information, see [Introduction to Annotated XSD Schemas &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml/annotated-xsd-schemas/introduction-to-annotated-xsd-schemas-sqlxml-4-0.md). For legacy applications that use XDR, see [Annotated XDR Schemas &#40;Deprecated in SQLXML 4.0&#41;](../../../relational-databases/sqlxml/annotated-xsd-schemas/annotated-xdr-schemas-deprecated-in-sqlxml-4-0.md).  
  
## Required Namespaces in the Updategram  
 The keywords in an updategram, such as **\<sync>**, **\<before>**, and **\<after>**, exist in the **urn:schemas-microsoft-com:xml-updategram** namespace. The namespace prefix that you use is arbitrary. In this documentation, the **updg** prefix denotes the **updategram** namespace.  
  
## Reviewing Syntax  
 An updategram is a template with **\<sync>**, **\<before>**, and **\<after>** blocks that form the syntax of the updategram. The following code shows this syntax in its simplest form:  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
  <updg:sync [mapping-schema= "AnnotatedSchemaFile.xml"] >  
    <updg:before>  
        ...  
    </updg:before>  
    <updg:after>  
        ...  
    </updg:after>  
  </updg:sync>  
</ROOT>  
```  
  
 The following definitions describe the role of each of these blocks:  
  
 **\<before>**  
 Identifies the existing state (also called "the before state") of the record instance.  
  
 **\<after>**  
 Identifies the new state to which data is to be changed.  
  
 **\<sync>**  
 Contains the **\<before>** and **\<after>** blocks. A **\<sync>** block can contain more than one set of **\<before>** and **\<after>** blocks. If there is more than one set of **\<before>** and **\<after>** blocks, these blocks (even if they are empty) must be specified as pairs. Furthermore, an updategram can have more than one **\<sync>** block. Each **\<sync>** block is one unit of transaction (which means that either everything in the **\<sync>** block is done or nothing is done). If you specify multiple **\<sync>** blocks in an updategram, the failure of one **\<sync>** block does not affect the other **\<sync>** blocks.  
  
 Whether an updategram deletes, inserts, or updates a record instance depends on the contents of the **\<before>** and **\<after>** blocks:  
  
-   If a record instance appears only in the **\<before>** block with no corresponding instance in the **\<after>** block, the updategram performs a delete operation.  
  
-   If a record instance appears only in the **\<after>** block with no corresponding instance in the **\<before>** block, it is an insert operation.  
  
-   If a record instance appears in the **\<before>** block and has a corresponding instance in the **\<after>** block, it is an update operation. In this case, the updategram updates the record instance to the values that are specified in the **\<after>** block.  
  
## Specifying a Mapping Schema in the Updategram  
 In an updategram, the XML abstraction that is provided by a mapping schema (both XSD and XDR schemas are supported) can be implicit or explicit (that is, an updategram can work with or without a specified mapping schema). If you do not specify a mapping schema, the updategram assumes an implicit mapping (the default mapping), where each element in the **\<before>** block or **\<after>** block maps to a table and each element's child element or attribute maps to a column in the database. If you explicitly specify a mapping schema, the elements and attributes in the updategram must match the elements and attributes in the mapping schema.  
  
### Implicit (default) Mapping  
 In most cases, an updategram that performs simple updates might not require a mapping schema. In this case, the updategram relies on the default mapping schema.  
  
 The following updategram demonstrates implicit mapping. In this example, the updategram inserts a new customer in the Sales.Customer table. Because this updategram uses implicit mapping, the \<Sales.Customer> element maps to the Sales.Customer table, and the CustomerID and SalesPersonID attributes map to the corresponding columns in the Sales.Customer table.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
<updg:sync >  
<updg:before>  
</updg:before>  
<updg:after>  
    <Sales.Customer CustomerID="1" SalesPersonID="277" />  
    </updg:after>  
</updg:sync>  
</ROOT>  
```  
  
### Explicit Mapping  
 If you specify a mapping schema (either XSD or XDR), the updategram uses the schema to determine the database tables and columns that are to be updated.  
  
 If the updategram performs a complex update (for example, inserting records in multiple tables on the basis of the parent-child relationship that is specified in the mapping schema), you must explicitly provide the mapping schema by using the **mapping-schema** attribute against which the updategram executes.  
  
 Because an updategram is a template, the path specified for the mapping schema in the updategram is relative to the location of the template file (relative to where the updategram is stored). For more information, see [Specifying an Annotated Mapping Schema in an Updategram &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/specifying-an-annotated-mapping-schema-in-an-updategram-sqlxml-4-0.md).  
  
## Element-centric and Attribute-centric Mapping in Updategrams  
 With default mapping (when the mapping schema is not specified in the updategram), the updategram elements map to tables and the  child elements (in the case of element-centric mapping) and the attributes (in the case of attribute-centric mapping) map to columns.  
  
### Element-centric Mapping  
 In an element-centric updategram, an element contains child elements that denote the properties of the element. As an example, refer to the following updategram. The **\<Person.Contact>** element contains the **\<FirstName>**and **\<LastName>** child elements. These child elements are properties of the **\<Person.Contact>** element.  
  
 Because this updategram does not specify a mapping schema, the updategram uses implicit mapping, where the **\<Person.Contact>** element maps to the Person.Contact table and its child elements map to the FirstName and LastName columns.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
<updg:sync >  
  <updg:after>  
    <Person.Contact>  
       <FirstName>Catherine</FirstName>  
       <LastName>Abel</LastName>  
    </Person.Contact>  
  </updg:after>  
</updg:sync>  
</ROOT>  
```  
  
### Attribute-centric Mapping  
 In an attribute-centric mapping, the elements have attributes. The following updategram uses attribute-centric mapping. In this example, the **\<Person.Contact>** element consists of the **FirstName** and **LastName** attributes. These attributes are the properties of the **\<Person.Contact>** element. As in the previous example, this updategram specifies no mapping schema, so it relies on implicit mapping to map the **\<Person.Contact>** element to the Person.Contact table and the element's attributes to the respective columns in the table.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
<updg:sync >  
  <updg:before>  
  </updg:before>  
  <updg:after>  
    <Person.Contact FirstName="Catherine" LastName="Abel" />  
  </updg:after>  
</updg:sync>  
</ROOT>  
```  
  
### Using Both Element-centric and Attribute-centric Mapping  
 You can specify a mix of element-centric and attribute-centric mapping, as shown in the following updategram. Notice that the **\<Person.Contact>** element contains both an attribute and a child element. Also, this updategram relies on implicit mapping. Thus, the **FirstName** attribute and the **\<LastName>** child element map to corresponding columns in the Person.Contact table.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
<updg:sync >  
  <updg:before>  
  </updg:before>  
  <updg:after>  
    <Person.Contact FirstName="Catherine" >  
       <LastName>Abel</LastName>  
    </Person.Contact>  
  </updg:after>  
</updg:sync>  
</ROOT>  
```  
  
## Working with Characters Valid in SQL Server but Not Valid in XML  
 In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], table names can include a space. However, this type of table name is not valid in XML.  
  
 To encode characters that are valid [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] identifiers but are not valid XML identifiers, use '__xHHHH\_\_' as the encoding value, where HHHH stands for the four-digit hexadecimal UCS-2 code for the character in the most significant bit-first order. Using this encoding scheme, a space character gets replaced with x0020 (the four-digit hexadecimal code for a space character); thus, the table name [Order Details] in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] becomes _x005B_Order_x0020_Details_x005D\_ in XML.  
  
 Similarly, you might need to specify three-part element names, such as \<[database].[owner].[table]>. Because the bracket characters ([ and ]) are not valid in XML, you must specify this as \<_x005B_database_x005D\_._x005B_owner_x005D\_._x005B_table_x005D\_>, where _x005B\_ is the encoding for the left bracket ([) and _x005D\_ is the encoding for the right bracket (]).  
  
## Executing Updategrams  
 Because an updategram is a template, all the processing mechanisms of a template apply to the updategram. For SQLXML 4.0, you can execute an updategram in either of the following ways:  
  
-   By submitting it in an ADO command.  
  
-   By submitting it as an OLE DB command.  
  
## See Also  
 [Updategram Security Considerations &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/security/updategram-security-considerations-sqlxml-4-0.md)  
  
  
