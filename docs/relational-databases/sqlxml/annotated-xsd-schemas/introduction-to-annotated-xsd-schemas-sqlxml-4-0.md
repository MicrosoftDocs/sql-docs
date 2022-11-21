---
title: "Introduction to Annotated XSD Schemas (SQLXML)"
description: Learn about creating XML views of relational data using the XML Schema Definition (XSD) language (SQLXML 4.0).
author: MikeRayMSFT
ms.author: mikeray
ms.date: 01/11/2019
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "namespaces [SQLXML], annotated XSD schemas"
  - "mapping schema [SQLXML], about mapping schema"
  - "views [SQLXML]"
  - "valid XSD schemas [SQLXML]"
  - "annotations [SQLXML]"
  - "XSD schemas [SQLXML], creating XML views"
  - "annotated XSD schemas, creating XML views"
  - "minimum XSD schema"
  - "annotated XSD schemas, examples"
  - "XML views [SQLXML]"
ms.assetid: 15282db1-65c4-43be-bdb7-e9ef49cb33a2
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Introduction to Annotated XSD Schemas (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  You can create XML views of relational data by using the XML Schema Definition (XSD) language. These views can then be queried by using XML Path language (XPath) queries. This is similar to creating views by using CREATE VIEW statements and then specifying SQL queries against the view.  
  
 An XML schema describes the structure of an XML document and also describes the various constraints on the data in the document. When you specify XPath queries against the schema, the structure of the XML document returned is determined by the schema against which the XPath query is executed.  
  
 In an XSD schema, the **\<xsd:schema>** element encloses the entire schema; all element declarations must be contained within the **\<xsd:schema>** element. You can describe attributes that define the namespace in which the schema resides and the namespaces that are used in the schema as properties of the **\<xsd:schema>** element.  
  
 A valid XSD schema must contain the **\<xsd:schema>** element defined as follows:  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"   
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
<!-- additional schema definitions here -->  
</xsd:schema>  
```  
  
 The **\<xsd:schema>** element is derived from the XML Schema namespace specification at http://www.w3.org/2001/XMLSchema.  
  
## Annotations to the XSD Schema  
 You can use an XSD schema with annotations that describe the mapping to a database, query the database, and return the results in the form of an XML document. Annotations are provided to map an XSD schema to database tables and columns. XPath queries can be specified against the XML view created by the XSD schema to query the database and obtain results as an XML.  
  
> [!NOTE]  
>  In [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQLXML 4.0, the XSD schema language supports the annotations introduced with annotated XML-Data Reduced (XDR) schema language in [!INCLUDE[ssVersion2000](../../../includes/ssversion2000-md.md)]. Annotated XDR is deprecated in SQLXML 4.0.  
  
 In the context of the relational database, it is useful to map the arbitrary XSD schema to a relational store. One way to achieve this is to annotate the XSD schema. An XSD schema with the annotations is referred to as a *mapping schema*, which provides information pertaining to how XML data is to be mapped to the relational store. A mapping schema is, in effect, an XML view of the relational data. These mappings can be used to retrieve relational data as an XML document.  
  
## Namespace for Annotations  
 In an XSD schema, annotations are specified by using the namespace **urn:schemas-microsoft-com:mapping-schema**. As shown in the following example, the easiest way to specify the namespace is to specify it in the **\<xsd:schema>** tag.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"   
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
...  
</xsd:schema>  
```  
  
 The namespace prefix that is used is arbitrary. In this documentation, the **sql** prefix is used to denote the annotation namespace and to distinguish annotations in this namespace from those in other namespaces.  
  
## Example of an Annotated XSD Schema  
 In the following example, the XSD schema consists of an **\<Person.Contact>** element. The **\<Employee>** element has a **ContactID** attribute and **\<FirstName>** and **\<LastName>** child elements:  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema">  
  <xsd:element name="Contact" >  
   <xsd:complexType>  
     <xsd:sequence>  
        <xsd:element name="FName"    
                     type="xsd:string" />   
        <xsd:element name="LName"  
                     type="xsd:string" />  
     </xsd:sequence>  
        <xsd:attribute name="ConID" type="xsd:integer" />  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
 Annotations are added to this XSD schema to map its elements and attributes to the database tables and columns:  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:element name="Contact" sql:relation="Person.Contact" >  
   <xsd:complexType>  
     <xsd:sequence>  
        <xsd:element name="FName"  
                     sql:field="FirstName"   
                     type="xsd:string" />   
        <xsd:element name="LName"    
                     sql:field="LastName"    
                     type="xsd:string" />  
     </xsd:sequence>  
        <xsd:attribute name="ConID"   
                       sql:field="ContactID"   
                       type="xsd:integer" />  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
 In the mapping schema, the **\<Contact>** element is mapped to the Person.Contact table in the sample AdventureWorks database by using the **sql:relation** annotation. The attributes ConID, FName, and LName are mapped to the ContactID, FirstName, and LastName columns in the Person.Contact table by using the **sql:field** annotations.  
  
 This annotated XSD schema provides the XML view of the relational data. This XML view can be queried using the XPath language. An XPath query returns an XML document as a result, instead of the rowset that is returned by SQL queries.  
  
> [!NOTE]  
>  In the mapping schema, case-sensitivity for the specified relational values (such as table name and column name) depends upon if SQL Server is using case-sensitive collation settings. For more information, see [Collation and Unicode Support](../../../relational-databases/collations/collation-and-unicode-support.md).  
  
## Other Resources  
 You can find more information about XML Schema Definition language (XSD), XML Path language (XPath), and Extensible Stylesheet Language Transformations (XSLT) at the following Web sites:  
  
-   XML Schema Part 0: Primer, W3C Recommendation (https://www.w3.org/TR/xmlschema-0/)  
  
-   XML Schema Part 1: Structures, W3C Recommendation (https://www.w3.org/TR/xmlschema-1/)  
  
-   XML Schema Part 2:Datatypes, W3C Recommendation (https://www.w3.org/TR/xmlschema-2/)  
  
-   XML Path Language (XPath) (https://www.w3.org/TR/xpath)  
  
-   XSL Transformations (XSLT) (https://www.w3.org/TR/xslt)  
  
## See Also  
 [Annotated Schema Security Considerations &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/security/annotated-schema-security-considerations-sqlxml-4-0.md)   
 [Annotated XDR Schemas &#40;Deprecated in SQLXML 4.0&#41;](../../../relational-databases/sqlxml/annotated-xsd-schemas/annotated-xdr-schemas-deprecated-in-sqlxml-4-0.md)  
  
  
