---
title: "Selective XML Indexes (SXI) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
ms.assetid: 598ecdcd-084b-4032-81b2-eed6ae9f5d44
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Selective XML Indexes (SXI)
  Selective XML indexes are another type of XML index that is available to you in addition to ordinary XML indexes. The goals of the selective XML index feature are the following:  
  
-   To improve the performance of queries over XML data stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   To support faster indexing of large XML data workloads.  
  
-   To improve scalability by reducing the storage costs of XML indexes.  
  
 The main limitation with ordinary XML indexes is that they index the entire XML document. This leads to several significant drawbacks, such as decreased query performance and increased index maintenance cost, mostly related to the storage costs of the index.  
  
 The selective XML index feature lets you promote only certain paths from the XML documents to index. At index creation time, these paths are evaluated, and the nodes that they point to are shredded and stored inside a relational table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This feature uses an efficient mapping algorithm developed by [!INCLUDE[msCoName](../../includes/msconame-md.md)] Research in collaboration with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product team. This algorithm maps the XML nodes to a single relational table, and achieves exceptional performance while requiring only modest storage space.  
  
 The selective XML index feature also supports secondary selective XML indexes over nodes that have been indexed by a selective XML index. These secondary selective indexes are efficient and further improve the performance of queries.  
  
##  <a name="benefits"></a> Benefits of Selective XML Indexes  
 Selective XML indexes provide the following benefits:  
  
1.  Greatly improved query performance over the XML data type for typical query loads.  
  
2.  Reduced storage requirements compared to ordinary XML indexes.  
  
3.  Reduced index maintenance costs compared to ordinary XML indexes.  
  
4.  No need to update applications to benefit from selective XML indexes.  
  

  
##  <a name="compare"></a> Selective XML Indexes and Primary XML Indexes  
  
> [!IMPORTANT]  
>  Create a selective XML index instead of an ordinary XML index in most cases for better performance and more efficient storage.  
  
 However, a selective XML index is not recommended when either of the following conditions is true:  
  
-   You map a large number of node paths.  
  
-   You support queries for unknown elements or elements in an unknown location in the document structure.  
  

  
##  <a name="example"></a> Simple Example of a Selective XML Index  
 Consider the following XML fragment as an XML document in a table of approximately 500,000 rows:  
  
```xml  
<book>  
    <created>2004-03-01</created>   
    <authors>Various</authors>  
    <subjects>  
        <subject>English wit and humor -- Periodicals</subject>  
        <subject>AP</subject>   
    </subjects>  
    <title>Punch, or the London Charivari, Volume 156, April 2, 1919</title>  
    <id>etext11617</id>  
</book>  
```  
  
 Creating a primary XML index over so many rows of this simple schema takes a long time. Querying this data also suffers from the fact that a primary XML index does not support selective indexing.  
  
 If you only need to query this data over the `/book/title` path and the `/book/subjects` path, you can create the following selective XML index:  
  
```sql  
CREATE SELECTIVE XML INDEX SXI_index  
ON Tbl(xmlcol)  
FOR   
(  
    pathTitle = '/book/title/text()' AS XQUERY 'xs:string',   
    pathAuthors = '/book/authors' AS XQUERY 'node()',  
    pathId = '/book/id' AS SQL NVARCHAR(100)  
)  
```  
  
 The preceding statement is a good example of the CREATE syntax that you use when you create a selective XML index. In the CREATE statement, first you provide a name for the index and identify the table and the XML column to index. Then you provide the paths to index. A path has three parts:  
  
1.  A name that uniquely identifies the path.  
  
2.  An XQuery expression that describes the path.  
  
3.  Optional optimization hints.  
  
 For more information about these elements, see [Related Tasks](#reltasks).  
  

  
## Supported Features, Prerequisites, and Limitations  
  
###  <a name="features"></a> Supported XML Features  
 Selective XML indexes support the XQuery supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] inside the exist(), value() and nodes() methods.  
  
-   For the exist(), value() and nodes() methods, selective XML indexes contain enough information to transform the entire expression.  
  
-   For the query() and modify() methods, selective XML indexes may be used for node filtering only.  
  
-   For the query() method, selective XML indexes are not used to retrieve results.  
  
-   For the modify() method, selective XML indexes are not used to update XML documents.  
  

  
###  <a name="unsupported"></a> Unsupported XML Features  
 Selective XML indexes do not support the following features that are supported in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] implementation of XML:  
  
-   Indexing of nodes with complex XS types: union types, sequence types, and list types.  
  
-   Indexing of nodes with binary XS types: for example, base64Binary and hexBinary.  
  
-   Specifying the nodes to index with XPath expressions that contain the wildcard character `*` at the end: For example,  `/a/b/c/*`, `/a//b/*`, or `/a/b/*:c`.  
  
-   Indexing any axis other than child, attribute, or descendant. The `//<step>` case is allowed as a special case.  
  
-   Indexing of XML processing instructions and comments.  
  
-   Specifying and retrieving the identifier for a node by using the id() function.  
  

  
###  <a name="prereq"></a> Prerequisites  
 The following prerequisites must exist before you can create a selective XML index over an XML column in a user table:  
  
-   A clustered index must exist on the primary key of the user table.  
  
-   The primary key of the user table is limited to a size of 128 bytes when used with selective XML indexes.  
  
-   The clustering key of the user table is limited to 15 columns when used with selective XML indexes.  
  

  
###  <a name="limits"></a> Limitations  
 **General requirements and limitations**  
  
-   Each selective XML index can only be created on a single XML column.  
  
-   You cannot create a selective XML index on a non-XML column.  
  
-   Each XML column in a table can have only one selective XML index.  
  
-   Each table can have up to 249 selective XML indexes.  
  
 **Limitations on supported objects**  
  
 You cannot create selective XML indexes on the following objects:  
  
1.  XML columns in a view.  
  
2.  Table-valued variable with XML columns.  
  
3.  XML type variables.  
  
4.  Computed XML columns.  
  
5.  XML columns with a depth of more than 128 nested nodes.  
  
 **Limitations related to storage**  
  
 There is a finite limit on the number of nodes from the XML document that can be added to the index. A selective XML index maps XML documents to a single relational table. Therefore it cannot have more than 1024 non-null columns in any given row of the table. Furthermore, many of the limitations of sparse columns also apply to selective XML indexes, because the indexes use sparse columns for storage.  
  
 The maximum number of non-null columns supported in any given row depends on the size of the data in the columns:  
  
-   In the best case, 1024 non-null columns are supported when all columns are of type **bit**.  
  
-   In the worst case, only 236 non-null columns are supported when all columns are large objects of type **varchar**.  
  
 Selective XML indexes use from one to four columns internally for every node path that is indexed. The total number of nodes that can be indexed ranges from 60 to several hundred nodes, depending on the actual size of the data in the indexed paths.  
  
-   In the worst case, when some or all nodes are mapped using `//` in the node path definition, the maximum number of indexed nodes is 60.  
  
-   In the best case, when nodes are mapped without using `//` in the node path definition, the maximum number of indexed nodes is 200.  
  
 **Selective XML indexes are rebuilt when you CREATE or ALTER the index.**  
  
 When you CREATE or ALTER a selective XML index, it is rebuilt in a single-threaded, offline mode. Frequently ALTER statements negatively affect the performance of queries over the indexed XML documents.  
  
 **Other limitations**  
  
-   Selective XML indexes are not supported in query hints.  
  
-   Selective XML indexes and secondary selective XML indexes are not supported in Database Tuning Advisor.  
  

  
##  <a name="reltasks"></a> Related Tasks  
  
|||  
|-|-|  
|**Task**|**Topic**|  
|Specify the node paths that you want to index and optional optimization hints when you create or alter a selective XML index.|[Specify Paths and Optimization Hints for Selective XML Indexes](specify-paths-and-optimization-hints-for-selective-xml-indexes.md)|  
|Create, alter, or drop a selective XML index.|[Create, Alter, and Drop Selective XML Indexes](create-alter-and-drop-selective-xml-indexes.md)|  
|Create, alter, or drop a secondary selective XML index.|[Create, Alter, and Drop Secondary Selective XML Indexes](create-alter-and-drop-secondary-selective-xml-indexes.md)|  
  

  
  
