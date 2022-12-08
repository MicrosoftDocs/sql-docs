---
title: "Annotated Schema Security Considerations (SQLXML)"
description: Learn about security guidelines for using annotated schemas in SQLXML 4.0.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "mapping schema [SQLXML], security"
  - "annotated XDR schemas, security"
  - "XDR schemas [SQLXML], security"
  - "annotations [SQLXML]"
  - "annotated XSD schemas, security"
  - "SQLXML, annotated XSD schemas"
  - "SQLXML, annotated XDR schemas"
  - "security [SQLXML], annotated schemas"
  - "XSD schemas [SQLXML], security"
ms.assetid: 7d7e44dc-b6d3-4e0f-95c7-8f99930c94f2
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Annotated Schema Security Considerations (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  The following are security guidelines for using annotated schemas:  
  
-   Avoid using default mapping in the mapping schemas. The default mapping exposes the database information (table and column names) in the resulting XML document because, by default, the element names map to table names and attribute names map to column names. Therefore, any user who sees the XML document has access to the table and column information in the database, presenting a potential security risk. To avoid this risk, specify arbitrary element and attribute names in the schema and use annotations to explicitly map them to the tables and columns. For more information about using default mapping when you create XSD schemas, see [Default Mapping of XSD Elements and Attributes to Tables and Columns &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-using/default-mapping-of-xsd-elements-and-attributes-to-tables-and-columns-sqlxml-4-0.md).  
  
-   The explicit mapping specified using the annotations exposes the database information (such as table names and column names). Therefore, you may not want to make these schemas available publicly.  
  
-   Certain queries such as those specified against mapping schema with recursion (specified using **max-depth** annotation set to a higher value) may take longer to execute. You can optionally specify a time-out limit by setting the Command Time Out property (in seconds). For example:  
  
    ```  
    cn.Open "Provider=SQLOLEDB;Server=localhost;Database=tempdb;Integrated Security=SSPI;Command Properties='Command Time Out=50';"  
    ```  
  
## See Also  
 [Annotated XSD Schemas in SQLXML 4.0](../../../relational-databases/sqlxml/annotated-xsd-schemas/annotated-xsd-schemas-in-sqlxml-4-0.md)  
  
  
