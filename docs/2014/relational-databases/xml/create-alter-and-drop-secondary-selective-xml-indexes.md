---
title: "Create, Alter, and Drop Secondary Selective XML Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
ms.assetid: 45128105-833b-40a9-9cc9-1ae03ac0b52b
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Create, Alter, and Drop Secondary Selective XML Indexes
  Describes how to create a new secondary selective XML index, or alter or drop an existing secondary selective XML index.  
  
##  <a name="create"></a> Creating a Secondary Selective XML Index  
  
### How to: Create a Secondary Selective XML Index  
 **Create a Secondary Selective XML Index by Using Transact-SQL**  
 Create a secondary selective XML index by calling the CREATE XML INDEX statement. For more information, see [CREATE XML INDEX &#40;Selective XML Indexes&#41;](~/t-sql/statements/create-xml-index-selective-xml-indexes.  
  
 **Example**  
  
 The following example creates a secondary selective XML index on the path `'pathabc'`. The path to index is identified by the name that was given to it when it was created with the CREATE SELECTIVE XML INDEX statement. For more information, see [CREATE SELECTIVE XML INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-selective-xml-index-transact-sql).  
  
```sql  
CREATE XML INDEX filt_sxi_index_c  
ON Tbl(xmlcol)  
USING XML INDEX sxi_index  
FOR  
(  
    pathabc  
)  
```  
  
  
##  <a name="alter"></a> Altering a Secondary Selective XML Index  
 The ALTER statement is not supported for secondary selective XML indexes. To change a secondary selective XML index, drop the existing index and recreate it.  
  
### How to: Alter a Secondary Selective XML Index  
 **Alter a Secondary Selective XML Index by Using Transact-SQL**  
 1.  Drop the existing secondary selective XML index by calling the DROP INDEX statement. For more information, see [DROP INDEX &#40;Selective XML Indexes&#41;](../indexes/indexes.md).  
  
2.  Recreate the index with the desired options by calling the CREATE XML INDEX statement. For more information, see [CREATE XML INDEX &#40;Selective XML Indexes&#41;](~/t-sql/statements/create-xml-index-selective-xml-indexes.  
  
 **Example**  
  
 The following example changes a secondary selective XML index by dropping it and recreating it.  
  
```sql  
DROP INDEX filt_sxi_index_c  
  
CREATE XML INDEX filt_sxi_index_c  
ON Tbl(xmlcol)  
USING XML INDEX sxi_index  
FOR  
(  
    pathabc  
)  
```  
  
  
##  <a name="drop"></a> Dropping a Secondary Selective XML Index  
  
### How to: Drop a Secondary Selective XML Index  
 **Drop a Secondary Selective XML Index by Using Transact-SQL**  
 Drop a secondary selective XML index by calling the DROP INDEX statement. For more information, see [DROP INDEX &#40;Selective XML Indexes&#41;](../indexes/indexes.md).  
  
 **Example**  
  
 The following example shows a DROP INDEX statement.  
  
```sql  
DROP INDEX ssxi_index  
ON tbl  
```  
  
  
## See Also  
 [Selective XML Indexes &#40;SXI&#41;](selective-xml-indexes-sxi.md)  
  
  
