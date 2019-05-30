---
title: "Guidelines and Limitations of XML Updategrams (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "updategrams [SQLXML], about updategrams"
ms.assetid: b5231859-14e2-4276-bc17-db2817b6f235
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Guidelines and Limitations of XML Updategrams (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Remember the following when using XML updategrams:  
  
-   If you are using an updategram for an insert operation with only a single pair of **\<before>** and **\<after>** blocks, the **\<before>** block can be omitted. Conversely, in case of a delete operation, the **\<after>** block can be omitted.  
  
-   If you are using an updategram with multiple **\<before>** and **\<after>** blocks in the **\<sync>** tag, both **\<before>** blocks and **\<after>** blocks must be specified to form **\<before>** and **\<after>** pairs.  
  
-   The updates in an updategram are applied to the XML view that is provided by the XML schema. Therefore, for the default mapping to succeed either you must specify the schema file name in the updategram or, if the file name is not provided, the element and attribute names must match the table and column names in the database.  
  
-   SQLXML 4.0 requires that all column values in an updategram must be explicitly mapped in the schema (either XDR or XSD) provided to compose the XML view for its child elements. This behavior differs from earlier versions of SQLXML, which allowed a value for a column not mapped in the schema if it was implied as part of the foreign Key in a **sql:relationship** annotation. (Note that this change does not affect propagation of primary key values to child elements, which still occurs for SQLXML 4.0 if no value is explicitly specified for the child element.  
  
-   If you are using an updategram to modify data in a binary column (such as the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **image** data type), you must provide a mapping schema in which the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type (for example, **sql:datatype="image"**) and the XML data type (for example, **dt:type="binhex"** or **dt:type="binbase64**) must be specified. The data for the binary column must be specified in the updategram; the **sql:url-encode** annotation that is specified in the mapping schema is ignored by the updategram.  
  
-   When you are writing an XSD schema, if the value you specify for the **sql:relation** or **sql:field** annotation includes a special character, such as a space character (for example, in the "Order Details" table name), this value must be enclosed in brackets (for example, "[Order Details]").  
  
-   When using updategrams, chain relationships are not supported. For example, if tables A and C are related through a chain relationship that uses table B, the following error will occur when attempting to run and execute the updategram:  
  
    ```  
    There is an inconsistency in the schema provided.  
    ```  
  
     Even if both schema and updategram are otherwise correct and validly formed, this error will occur if a chain relationship is present.  
  
-   Updategrams do not permit the passing of **image** type data as parameters during updates.  
  
-   Binary large object (BLOB) types like **text/ntext** and images should not be used in the **\<before>** block in when working with updategrams, because this will include them for use in concurrency control. This can cause problems with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] because of the limitations on comparison for BLOB types. For example, the LIKE keyword is used in the WHERE clause for comparing between columns of the **text** data type; however, comparisons will fail in the case of BLOB types where the size of the data is greater than 8K.  
  
-   Special characters in **ntext** data can cause problems with SQLXML 4.0 because of the limitations on comparison for BLOB types. For example, the use of "[Serializable]" in the **\<before>** block of an updategrams when used in concurrency checking of a column of **ntext** type will fail with the following SQLOLEDB error description:  
  
    ```  
    Empty update, no updatable rows found   Transaction aborted  
    ```  
  
## See Also  
 [Updategram Security Considerations &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/security/updategram-security-considerations-sqlxml-4-0.md)  
  
  
