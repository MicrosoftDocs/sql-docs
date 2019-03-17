---
title: "Using Annotations in XSD Schemas (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "annotated XSD schemas, about annotated XSD schemas"
  - "annotations [SQLXML]"
  - "relationships [SQLXML]"
  - "relationships [SQLXML], hierarchical relationships"
  - "hierarchical relationships [SQLXML]"
  - "mapping schema [SQLXML], scenarios for using"
ms.assetid: 78f318a4-7a36-473b-9852-a4bae6940ce5
author: MightyPen
ms.author: douglasl
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Using Annotations in XSD Schemas (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] SQLXML 4.0, the XSD schema language supports annotations in a manner similar to the annotations introduced in the XML-Data Reduced (XDR) schema language. There are additional annotations introduced in XSD that are not supported in XDR.  
  
 These annotations can be used within the XSD schema to specify XML-to-relational mapping. This includes mapping between elements and attributes in the XSD schema to tables (views) and columns in the databases.  
  
 If you do not specify the annotations, default mapping takes place. By default, an XSD element with a complex type maps to a table (view) name in the specified database, and an element or attribute with a simple type maps to the column with the same name as the element or attribute.  
  
 These annotations can also be used to specify the hierarchical relationships in XML-thus representing the relationships in the database, because an XSD schema is simply an XML view of relational data.  
  
 This section provides descriptions of the annotations you can use with XSD schemas and examples of their usage.  
  
> [!NOTE]  
>  All the examples in this section specify simple XPath queries against the annotated XSD schema described in each example. Familiarity with the XPath language is assumed.  
  
## In This Section  
 [XSD Annotations &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/xsd-annotations-sqlxml-4-0.md)  
 Lists the annotations you can use with XSD schemas, their descriptions, and the equivalent annotations for XDR.  
  
 [Default Mapping of XSD Elements and Attributes to Tables and Columns &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/default-mapping-of-xsd-elements-and-attributes-to-tables-and-columns-sqlxml-4-0.md)  
 Explains default mapping and provides examples of tasks related to default mapping.  
  
 [Explicit Mapping of XSD Elements and Attributes to Tables and Columns &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/explicit-mapping-xsd-elements-and-attributes-to-tables-and-columns.md)  
 Explains explicit mapping with the **sql:relation** and **sql:field** annotations, and provides examples.  
  
 [Specifying Relationships Using sql:relationship &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/specifying-relationships-using-sql-relationship-sqlxml-4-0.md)  
 Describes and provides examples of the **sql:relationship** annotation.  
  
 [Specifying the sql:inverse Attribute on sql:relationship &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/specifying-the-sql-inverse-attribute-on-sql-relationship-sqlxml-4-0.md)  
 Describes the **sql:inverse** annotation.  
  
 [Creating Constant Elements Using sql:is-constant &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/creating-constant-elements-using-sql-is-constant-sqlxml-4-0.md)  
 Describes and provides examples of the **sql:is-constant** annotation.  
  
 [Excluding Schema Elements from the Resulting XML Document Using sql:mapped &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/excluding-schema-elements-from-the-xml-document-using-sql-mapped.md)  
 Describes and provides examples of the **sql:mapped** annotation.  
  
 [Filtering Values Using sql:limit-field and sql:limit-value &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/filtering-values-using-sql-limit-field-and-sql-limit-value-sqlxml-4-0.md)  
 Describes and provides examples of the **sql:limit-field** and **sql:limit-value** annotations.  
  
 [Identifying Key Columns Using sql:key-fields &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/identifying-key-columns-using-sql-key-fields-sqlxml-4-0.md)  
 Describes and provides examples of the **sql:key-fields** annotation.  
  
 [Specifying a Target Namespace Using the targetNamespace Attribute &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/specifying-a-target-namespace-using-the-targetnamespace-attribute-sqlxml-4-0.md)  
 Describes and provides examples of the **targetNamespace** attribute.  
  
 [Creating Valid ID, IDREF, and IDREFS Type Attributes Using sql:prefix &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/creating-valid-id-idref-and-idrefs-type-attributes-using-sql-prefix-sqlxml-4-0.md)  
 Describes and provides examples of the **sql:prefix** annotation.  
  
 [Data Type Coercions and the sql:datatype Annotation &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/data-type-coercions-and-the-sql-datatype-annotation-sqlxml-4-0.md)  
 Describes and provides examples of the **sql:datatype** annotation.  
  
 [Mapping XSD Data Types to XPath Data Types &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/mapping-xsd-data-types-to-xpath-data-types-sqlxml-4-0.md)  
 Provides a table that compares XSD, XDR, and XPath datatypes and lists the relevant [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] conversions.  
  
 [Creating CDATA Sections Using sql:use-cdata &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/creating-cdata-sections-using-sql-use-cdata-sqlxml-4-0.md)  
 Describes and provides examples of the **sql:use-data** annotation.  
  
 [Requesting URL References to BLOB Data Using sql:encode &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/requesting-url-references-to-blob-data-using-sql-encode-sqlxml-4-0.md)  
 Describes and provides examples of the **sql:encode** annotation.  
  
 [Retrieving Unconsumed Data Using the sql:overflow-field &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/retrieving-unconsumed-data-using-the-sql-overflow-field-sqlxml-4-0.md)  
 Describes and provides examples of the **sql:overflow-field** annotation.  
  
 [Hiding Elements and Attributes by Using sql:hide](../../relational-databases/sqlxml-annotated-xsd-schemas-using/hiding-elements-and-attributes-by-using-sql-hide.md)  
 Describes and provides examples of the **sql:hide** annotation.  
  
 [Using the sql:identity and sql:guid Annotations](../../relational-databases/sqlxml-annotated-xsd-schemas-using/using-the-sql-identity-and-sql-guid-annotations.md)  
 Describes and provides examples of the **sql:identity** and **sql:guid** annotations.  
  
 [Specifying Depth in Recursive Relationships by Using sql:max-depth](../../relational-databases/sqlxml-annotated-xsd-schemas-using/specifying-depth-in-recursive-relationships-by-using-sql-max-depth.md)  
 Describes and provides examples of the **sql:max-depth** annotation.  
  
## See Also  
 [Annotated Schema Security Considerations &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/security/annotated-schema-security-considerations-sqlxml-4-0.md)  
  
  
