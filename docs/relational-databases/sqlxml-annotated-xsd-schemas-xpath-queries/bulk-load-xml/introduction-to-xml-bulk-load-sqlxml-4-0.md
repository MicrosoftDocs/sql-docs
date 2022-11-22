---
title: "Introduction to XML Bulk Load (SQLXML)"
description: Learn about the XML Bulk Load utility, a stand-alone COM object in SQLXML 4.0 that allows you to load semistructured XML data into Microsoft SQL Server tables.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "nontransacted XML Bulk Load operations"
  - "XML Bulk Load [SQLXML], about XML Bulk Load"
  - "bulk load [SQLXML], about bulk load"
  - "transacted XML Bulk Load operations"
  - "streaming XML data"
ms.assetid: 38bd3cbd-65ef-4c23-9ef3-e70ecf6bb88a
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Introduction to XML Bulk Load (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  XML Bulk Load is a stand-alone COM object that allows you to load semistructured XML data into Microsoft [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] tables.  
  
 You can insert XML data into a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database by using an INSERT statement and the OPENXML function; however, the Bulk Load utility provides better performance when you need to insert large amounts of XML data.  
  
 The Execute method of the XML Bulk Load object model takes two parameters:  
  
-   An annotated XML Schema Definition (XSD) or XML-Data Reduced (XDR) schema. The XML Bulk Load utility interprets this mapping schema and the annotations that are specified in the schema in identifying the SQL Server tables into which the XML data is to be inserted.  
  
-   An XML document or document fragment (a document fragment is a document without a single top-level element). A file name or a stream from which XML Bulk Load can read can be specified.  
  
 XML Bulk Load interprets the mapping schema and identifies the table(s) into which the XML data is to be inserted.  
  
 It is assumed that you are familiar with the following [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features:  
  
-   Annotated XSD and XDR schemas. For more information about annotated XSD schemas, see [Introduction to Annotated XSD Schemas &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml/annotated-xsd-schemas/introduction-to-annotated-xsd-schemas-sqlxml-4-0.md). For information about annotated XDR schemas, see [Annotated XDR Schemas &#40;Deprecated in SQLXML 4.0&#41;](../../../relational-databases/sqlxml/annotated-xsd-schemas/annotated-xdr-schemas-deprecated-in-sqlxml-4-0.md).  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] bulk insert mechanisms, such as the [!INCLUDE[tsql](../../../includes/tsql-md.md)] BULK INSERT statement and the bcp utility. For more information, see [BULK INSERT &#40;Transact-SQL&#41;](../../../t-sql/statements/bulk-insert-transact-sql.md) and [bcp Utility](../../../tools/bcp-utility.md).  
  
## Streaming of XML Data  
 Because the source XML document can be large, the entire document is not read into memory for bulk load processing. Instead, XML Bulk Load interprets the XML data as a stream and reads it. As the utility reads the data, it identifies the database table(s), generates the appropriate record(s) from the XML data source, and then sends the record(s) to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] for insertion.  
  
 For example, the following source XML document consists of **\<Customer>** elements and **\<Order>** child elements:  
  
```  
<Customer ...>  
    <Order.../>  
    <Order .../>  
     ...  
</Customer>  
...  
```  
  
 As XML Bulk Load reads the **\<Customer>** element, it generates a record for the Customertable. When it reads the **\</Customer>** end tag, XML Bulk Load inserts that record into the table in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. In the same way, when it reads the **\<Order>** element, XML Bulk Load generates a record for the Ordertable, and then inserts that record into the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table upon reading the **\</Order>** end tag.  
  
## Transacted and Nontransacted XML Bulk Load Operations  
 XML Bulk Load can operate in either a transacted or a nontransacted mode. Performance is usually optimal if you are bulk loading in a nontransacted mode: that is, the Transaction property is set to FALSE) and either of the following conditions is true:  
  
-   The tables into which the data is bulk loaded are empty with no indexes.  
  
-   The tables have data and unique indexes.  
  
 The nontransacted approach does not guarantee a rollback if something goes wrong in the bulk load process (although partial rollbacks can happen). The nontransacted bulk load is appropriate when the database is empty. Therefore, if something does go wrong, you can clean the database and start XML Bulk Load again.  
  
> [!NOTE]  
>  In nontransacted mode, XML Bulk Load uses a default internal transaction and commits it. When the Transaction property is set to TRUE, XML Bulk Load does not call commit on this transaction.  
  
 If the Transaction property is set to TRUE, XML Bulk Load creates temporary files, one for each table that is identified in the mapping schema. XML Bulk Load first stores the records from the source XML document in these temporary files. Then, a [!INCLUDE[tsql](../../../includes/tsql-md.md)] BULK INSERT statement retrieves these records from the files and stores them in the corresponding tables. You can specify the location for these temporary files by using the TempFilePath property. You must ensure that the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] account used with XML Bulk Load has access to this path. If the TempFilePath property is not specified, the default file path that is specified in the TEMP environment variable is used to create the temporary files.  
  
 If the Transaction property is set to FALSE (the default setting), XML Bulk Load uses the OLE DB interface IRowsetFastLoad to bulk load the data.  
  
 If the ConnectionString property sets the connection string, and the Transaction property is set to TRUE, XML Bulk Load operates in its own transaction context. (For example, XML Bulk Load starts its own transaction, and commits or rolls back as appropriate.)  
  
 If the ConnectionCommand property sets the connection with an existing connection object and the Transaction property is set to TRUE, XML Bulk Load does not issue a COMMIT or ROLLBACK statement in the case of a success or a failure, respectively. If there is an error, XML Bulk Load returns the appropriate error message. The decision to issue a COMMIT or ROLLBACK statement is left to the client that initiated the bulk load. The connection object that is used for XML Bulk Load should be of type ICommand or be an ADO command object.  
  
 In SQLXML 4.0, a ConnectionObject cannot be used with the Transaction property set to FALSE. The nontransacted mode is not supported with a ConnectionObject because it is impossible to open more than one IRowsetFastLoad interface on a passed-in session.  
  
  
