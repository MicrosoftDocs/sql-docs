---
title: "Other Annotations (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "url-encode annotation"
  - "sql:key-fields"
  - "use-cdata annotation"
  - "sql:is-mapping-schema"
  - "sql:url-encode"
  - "sql:id-prefix"
  - "sql:use-cdata"
  - "key-fields annotation"
  - "id-prefix annotation [SQLXML]"
  - "is-mapping-schema annotation"
ms.assetid: f7b4d37b-d6d3-4ac3-b2fd-a0b534a924e4
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Annotation Interpretation - Other Annotations
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  In addition to the annotations discussed in the previous topics in this section, XML Bulk Load interprets these other annotations, as follows:  
  
 **sql:id-prefix**  
 If the schema specifies prefixes to the XML data, XML Bulk Load removes the prefixes before sending the data to Microsoft [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 **sql:use-cdata**  
 XML Bulk Load reads the text that is stored in the CDATA sections and sends it to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 **sql:url-encode**  
 XML Bulk Load does not support this annotation. For example, you cannot specify a URL in the XML data input and expect XML Bulk Load to read data from that location to store it in the database.  
  
 **sql:is-mapping-schema**  
 XML Bulk Load does not support this annotation, nor does it support **sql:id**.  
  
> [!NOTE]  
>  XML Bulk Load does not support inline mapping schemas.  
  
 **sql:key-fields**  
 XML Bulk Load always ignores this annotation.  
  
## See Also  
 [Annotation Interpretation &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/annotation-interpretation-sqlxml-4-0.md)  
  
  
