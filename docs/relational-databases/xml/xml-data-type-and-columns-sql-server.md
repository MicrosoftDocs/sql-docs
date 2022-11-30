---
title: "xml data type and columns (SQL Server)"
description: Learn about the advantages and limitations of the xml data type for storing XML data in SQL Server.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
---
# xml data type and columns (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article discusses the advantages and the limitations of the **xml** data type in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and helps you to choose how to store XML data.

## Relational or XML data model

If your data is highly structured with a known schema, the relational model is likely to work best for data storage. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the required functionality and tools you may need. On the other hand, if the structure is semi-structured or unstructured, or unknown, you have to give consideration to modeling such data.

XML is a good choice if you want a platform-independent model in order to ensure portability of the data by using structural and semantic markup. Additionally, it is an appropriate option if some of the following properties are satisfied:

- Your data is sparse or you don't know the structure of the data, or the structure of your data may change significantly in the future.

- Your data represents containment hierarchy, instead of references among entities, and may be recursive.

- Order is inherent in your data.

- You want to query into the data or update parts of it, based on its structure.

If none of these conditions is met, you should use the relational data model. For example, if your data is in XML format but your application just uses the database to store and retrieve the data, an **[n]varchar(max)** column is all you require. Storing the data in an XML column has additional benefits. This includes having the engine determine that the data is well formed or valid, and also includes support for fine-grained query and updates into the XML data.

## Reasons for storing XML data in SQL Server

Following are some of the reasons to use native XML features in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instead of managing your XML data in the file system:

- You want to share, query, and modify your XML data in an efficient and transacted way. Fine-grained data access is important to your application. For example, you may want to extract some of the sections within an XML document, or you may want to insert a new section without replacing your whole document.

- You have relational data and XML data and you want interoperability between both relational and XML data within your application.

- You need language support for query and data modification for cross-domain applications.

- You want the server to guarantee that the data is well formed and also optionally validate your data according to XML schemas.

- You want indexing of XML data for efficient query processing and good scalability, and the use of a first-rate query optimizer.

- You want SOAP, ADO.NET, and OLE DB access to XML data.

- You want to use administrative functionality of the database server for managing your XML data. For example, this would be backup, recovery, and replication.

If none of these conditions is satisfied, it may be better to store your data as a non-XML, large object type, such as **[n]varchar(max)** or **varbinary(max)**.

## XML storage options

The storage options for XML in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] include the following:

- Native storage as **xml** data type

  The data is stored in an internal representation that preserves the XML content of the data. This internal representation includes information about the containment hierarchy, document order, and element and attribute values. Specifically, the InfoSet content of the XML data is preserved. For more information about InfoSet, visit [http://www.w3.org/TR/xml-infoset](https://go.microsoft.com/fwlink/?LinkId=48843). The InfoSet content may not be an identical copy of the text XML, because the following information isn't retained: insignificant white spaces, order of attributes, namespace prefixes, and XML declaration.

  For typed **xml** data type, an **xml** data type bound to XML schemas, the post-schema validation InfoSet (PSVI) adds type information to the InfoSet and is encoded in the internal representation. This improves parsing speed significantly. For more information, see the W3C XML Schema specifications at [http://www.w3.org/TR/xmlschema-1](https://go.microsoft.com/fwlink/?LinkId=48881) and [http://www.w3.org/TR/xmlschema-2](https://go.microsoft.com/fwlink/?LinkId=4871).

- Mapping between XML and relational storage

  By using an annotated schema (AXSD), the XML is decomposed into columns in one or more tables. This preserves fidelity of the data at the relational level. As a result, the hierarchical structure is preserved although order among elements is ignored. The schema can't be recursive.

- Large object storage, **[n]varchar(max)** and **varbinary(max)**

  An identical copy of the data is stored. This is useful for special-purpose applications such as legal documents. Most applications don't require an exact copy and are satisfied with the XML content (InfoSet fidelity).

Generally, you may have to use a combination of these approaches. For example, you may want to store your XML data in an **xml** data type column and promote properties from it into relational columns. Or, you may want to use mapping technology to store nonrecursive parts in non-XML columns and only the recursive parts in **xml** data type columns.

### Choice of XML technology

The choice of XML technology, native XML versus XML view, generally depends upon the following factors:

- Storage options

  Your XML data may be more appropriate for large object storage (for example, a product manual), or more amenable to storage in relational columns (for example, a line item converted to XML). Each storage option preserves document fidelity to a different extent.

- Query capabilities

  You may find one storage option more appropriate than another, based on the nature of your queries and on the extent to which you query your XML data. Fine-grained query of your XML data, for example, predicate evaluation on XML nodes, is supported to varying degrees in the two storage options.

- Indexing XML data

  You may want to index the XML data to speed up XML query performance. Indexing options vary with the storage options; you have to make the appropriate choice to optimize your workload.

- Data modification capabilities

  Some workloads involve fine-grained modification of XML data. For example, this can include adding a new section within a document, while other workloads, such as Web content, don't. Data modification language support may be important for your application.

- Schema support

  Your XML data may be described by a schema that may or may not be an XML schema document. The support for schema-bound XML depends upon the XML technology.

Different choices also have different performance characteristics.

### Native XML storage

You can store your XML data in an **xml** data type column at the server. This is an appropriate choice if the following applies:

- You want a straightforward way to store your XML data at the server and, at the same time, preserve document order and document structure.

- You may or may not have a schema for your XML data.

- You want to query and modify your XML data.

- You want to index the XML data for faster query processing.

- Your application needs system catalog views to administer your XML data and XML schemas.

Native XML storage is useful when you have XML documents that have a range of structures, or you have XML documents that conform to different or complex schemas that are too hard to map to relational structures.

#### Example: Model XML data using the xml data type

Consider a product manual in XML format that is made up of a separate chapter for each topic and that has multiple sections within each chapter. A section can contain subsections. As a result, `<section>` is a recursive element. Product manuals contain a large amount of mixed content, diagrams, and technical material; the data is semi-structured. Users may want to perform a contextual search for topics of interest such as searching for the section on "clustered index" within the chapter on "indexing", and query technical quantities.

An appropriate storage model for your XML documents is an **xml** data type column. This preserves the InfoSet content of your XML data. Indexing the XML column benefits query performance.

#### Example: Retain exact copies of XML data

For illustration, assume that government regulations require you to retain exact textual copies of your XML documents. For example, these could include signed documents, legal documents, or stock transaction orders. You may want to store your documents in a **[n]varchar(max)** column.

For querying, convert the data to **xml** data type at run time and execute XQuery on it. The run-time conversion may be costly, especially when the document is large. If you query frequently, you can redundantly store the documents in an **xml** data type column and index it while you return exact document copies from the **[n]varchar(max)** column.

The XML column may be a computed column, based on the **[n]varchar(max)** column. However, you can't create an XML index on a computed, XML column, nor can an XML index be built on **[n]varchar(max)** or **varbinary(max)** columns.

### XML view technology

By defining a mapping between your XML schemas and the tables in a database, you create an *XML view* of your persistent data. XML bulk load can be used to populate the underlying tables by using the XML view. You can query the XML view by using XPath version 1.0; the query is translated to SQL queries on the tables. Similarly, updates are also propagated to those tables.

This technology is useful in the following situations:

- You want to have an XML-centric programming model using XML views over your existing relational data.

- You have a schema (XSD, XDR) for your XML data that an external partner may have provided.

- Order isn't important in your data, or your query table data isn't recursive, or the maximal recursion depth is known in advance.

- You want to query and modify the data through the XML view by using XPath version 1.0.

- You want to bulk load XML data and decompose them into the underlying tables by using the XML view.

Examples include relational data exposed as XML for data exchange and Web services, and XML data with fixed schema. For more information.

#### Example: Model data using an annotated XML schema (AXSD)

For illustration, assume that you have existing relational data, such as customers, orders, and line items, that you want to handle as XML. Define an XML view by using AXSD over the relational data. The XML view allows you to bulk load XML data into your tables and query and update the relational data by using the XML view. This model is useful if you have to exchange data that contains XML markup with other applications while your SQL applications work uninterrupted.

### Hybrid model

Frequently, a combination of relational and **xml** data type columns is appropriate for data modeling. Some of the values from your XML data can be stored in relational columns, and the rest, or the whole XML value stored in an XML column. This may yield better performance in that you have more control over the indexes created on the relational columns and locking characteristics.

The values to store in relational columns depend on your workload. For example, if you retrieve all the XML values based on the path expression, `/Customer/@CustId`, promoting the value of the `CustId` attribute into a relational column and indexing it may yield faster query performance. On the other hand, if your XML data is extensively and nonredundantly decomposed into relational columns, the reassembly cost may be significant.

For highly structured XML data, for example, the content of a table has been converted into XML; you can map all values to relational columns, and possibly use XML view technology.

## Granularity of XML data

The granularity of the XML data stored in an XML column is important for locking and, to a lesser degree, it is also important for updates. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the same locking mechanism for both XML and non-XML data. Therefore, row-level locking causes all XML instances in the row to be locked. When the granularity is large, locking large XML instances for updates causes throughput to decline in a multiuser scenario. On the other hand, severe decomposition loses object encapsulation and increases reassembly cost.

A balance between data modeling requirements and locking and update characteristics is important for good design. However, in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the size of actual stored XML instances isn't as critical.

For example, updates to an XML instance are performed by using new support for partial binary large object (BLOB) and partial index updates in which the existing stored XML instance is compared to its updated version. Partial binary large object (BLOB) update performs a differential comparison between the two XML instances and updates only the differences. Partial index updates modify only those rows that must be changed in the XML index.

## Limitations of the xml data type

Note the following general limitations that apply to the **xml** data type:

- The stored representation of **xml** data type instances can't exceed 2 GB.

- It can't be used as a subtype of a **sql_variant** instance.

- It doesn't support casting or converting to either **text** or **ntext**. Use **varchar(max)** or **nvarchar(max)** instead.

- It can't be compared or sorted. This means an **xml** data type can't be used in a GROUP BY statement.

- It can't be used as a parameter to any scalar, built-in functions other than ISNULL, COALESCE, and DATALENGTH.

- It can't be used as a key column in an index. However, it can be included as data in a clustered index or explicitly added to a nonclustered index by using the INCLUDE keyword when the nonclustered index is created.

- XML elements can be nested up to 128 levels.

## See also

- [Examples of Bulk Import and Export of XML Documents &#40;SQL Server&#41;](../../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)
