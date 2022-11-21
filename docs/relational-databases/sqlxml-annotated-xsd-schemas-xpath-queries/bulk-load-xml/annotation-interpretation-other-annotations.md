---
title: "Other Annotations (SQLXML)"
description: View a list of SQLXML annotations with a description of how each one is interpreted by XML Bulk Load.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "url-encode annotation"
ms.assetid: f7b4d37b-d6d3-4ac3-b2fd-a0b534a924e4
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Annotation Interpretation - Other Annotations
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
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
  
  
