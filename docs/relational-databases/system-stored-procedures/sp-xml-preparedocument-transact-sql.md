---
title: "sp_xml_preparedocument (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_xml_preparedocument_TSQL"
  - "sp_xml_preparedocument"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_xml_preparedocument"
ms.assetid: 95f41cff-c52a-4182-8ac6-bf49369d214c
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# sp_xml_preparedocument (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Reads the XML text provided as input, parses the text by using the MSXML parser (Msxmlsql.dll), and provides the parsed document in a state ready for consumption. This parsed document is a tree representation of the various nodes in the XML document: elements, attributes, text, comments, and so on.  
  
 **sp_xml_preparedocument** returns a handle that can be used to access the newly created internal representation of the XML document. This handle is valid for the duration of the session or until the handle is invalidated by executing **sp_xml_removedocument**.  
  
> [!NOTE]  
>  A parsed document is stored in the internal cache of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The MSXML parser uses one-eighth the total memory available for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To avoid running out of memory, run **sp_xml_removedocument** to free up the memory.  
  
> [!NOTE]  
>  For backwards compatibility, **sp_xml_preparedocument** collapses the CR (char(13)) and LF (char(10)) characters in attributes even if these characters are entitized.  
  
> [!NOTE]  
>  The XML parser invoked by **sp_xml_preparedocument** can parse internal DTDs and entity declarations. Because maliciously constructed DTDs and entity declarations can be used to perform a denial of service attack, we strongly recommend that users not directly pass XML documents from untrusted sources to **sp_xml_preparedocument**.  
>   
>  To mitigate recursive entity expansion attacks, **sp_xml_preparedocument** limits to 10,000 the number of entities that can be expanded underneath a single entity at the top level of a document. The limit does not apply to character or numeric entities. This limit allows documents with many entity references to be stored, but prevents any one entity from being recursively expanded in a chain longer than 10,000 expansions.  
  
> [!NOTE]  
>  **sp_xml_preparedocument** limits the number of elements that can be open at one time to 256.  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_xml_preparedocument  
hdoc   
OUTPUT  
[ , xmltext ]  
[ , xpath_namespaces ]   
```  
  
## Arguments  
 *hdoc*  
 Is the handle to the newly created document. *hdoc* is an integer.  
  
 [ *xmltext* ]  
 Is the original XML document. The MSXML parser parses this XML document. *xmltext* is a text parameter: **char**, **nchar**, **varchar**, **nvarchar**, **text**, **ntext** or **xml**. The default value is NULL, in which case an internal representation of an empty XML document is created.  
  
> [!NOTE]  
>  **sp_xml_preparedocument** can only process text or untyped XML. If an instance value to be used as input is already typed XML, first cast it to a new untyped XML instance or as a string and then pass that value as input. For more information, see [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md).  
  
 [ *xpath_namespaces* ]  
 Specifies the namespace declarations that are used in row and column XPath expressions in OPENXML. *xpath_namespaces* is a text parameter: **char**, **nchar**, **varchar**, **nvarchar**, **text**, **ntext** or **xml**.  
  
 The default value is **\<root xmlns:mp="urn:schemas-microsoft-com:xml-metaprop">**. *xpath_namespaces* provides the namespace URIs for the prefixes used in the XPath expressions in OPENXML by means of a well-formed XML document. *xpath_namespaces* declares the prefix that must be used to refer to the namespace **urn:schemas-microsoft-com:xml-metaprop**; this provides metadata about the parsed XML elements. Although you can redefine the namespace prefix for the metaproperty namespace by using this technique, this namespace is not lost. The prefix **mp** is still valid for **urn:schemas-microsoft-com:xml-metaprop** even if *xpath_namespaces* contains no such declaration.  
  
## Return Code Values  
 0 (success) or >0 (failure)  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
  
### A. Preparing an internal representation for a well-formed XML document  
 The following example returns a handle to the newly created internal representation of the XML document that is provided as input. In the call to `sp_xml_preparedocument`, a default namespace prefix mapping is used.  
  
```  
DECLARE @hdoc int;  
DECLARE @doc varchar(1000);  
SET @doc ='  
<ROOT>  
<Customer CustomerID="VINET" ContactName="Paul Henriot">  
   <Order CustomerID="VINET" EmployeeID="5" OrderDate="1996-07-04T00:00:00">  
      <OrderDetail OrderID="10248" ProductID="11" Quantity="12"/>  
      <OrderDetail OrderID="10248" ProductID="42" Quantity="10"/>  
   </Order>  
</Customer>  
<Customer CustomerID="LILAS" ContactName="Carlos Gonzlez">  
   <Order CustomerID="LILAS" EmployeeID="3" OrderDate="1996-08-16T00:00:00">  
      <OrderDetail OrderID="10283" ProductID="72" Quantity="3"/>  
   </Order>  
</Customer>  
</ROOT>';  
--Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @hdoc OUTPUT, @doc;  
-- Remove the internal representation.  
exec sp_xml_removedocument @hdoc;  
```  
  
### B. Preparing an internal representation for a well-formed XML document with a DTD  
 The following example returns a handle to the newly created internal representation of the XML document that is provided as input. The stored procedure validates the document loaded against the DTD included in the document. In the call to `sp_xml_preparedocument`, a default namespace prefix mapping is used.  
  
```  
DECLARE @hdoc int;  
DECLARE @doc varchar(2000);  
SET @doc = '  
<?xml version="1.0" encoding="UTF-8" ?>   
<!DOCTYPE root   
[<!ELEMENT root (Customers)*>  
<!ELEMENT Customers EMPTY>  
<!ATTLIST Customers CustomerID CDATA #IMPLIED ContactName CDATA #IMPLIED>]>  
<root>  
<Customers CustomerID="ALFKI" ContactName="Maria Anders"/>  
</root>';  
  
EXEC sp_xml_preparedocument @hdoc OUTPUT, @doc;  
```  
  
### C. Specifying a namespace URI  
 The following example returns a handle to the newly created internal representation of the XML document that is provided as input. The call to `sp_xml_preparedocument` preserves the `mp` prefix to the metaproperty namespace mapping and adds the `xyz` mapping prefix to the namespace `urn:MyNamespace`.  
  
```  
DECLARE @hdoc int;  
DECLARE @doc varchar(1000);  
SET @doc ='  
<ROOT>  
<Customer CustomerID="VINET" ContactName="Paul Henriot">  
   <Order CustomerID="VINET" EmployeeID="5"   
           OrderDate="1996-07-04T00:00:00">  
      <OrderDetail OrderID="10248" ProductID="11" Quantity="12"/>  
      <OrderDetail OrderID="10248" ProductID="42" Quantity="10"/>  
   </Order>  
</Customer>  
<Customer CustomerID="LILAS" ContactName="Carlos Gonzlez">  
   <Order CustomerID="LILAS" EmployeeID="3"   
           OrderDate="1996-08-16T00:00:00">  
      <OrderDetail OrderID="10283" ProductID="72" Quantity="3"/>  
   </Order>  
</Customer>  
</ROOT>'  
--Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @hdoc OUTPUT, @doc, '<ROOT xmlns:xyz="urn:MyNamespace"/>';  
```  
  
## See Also  
 <br>[XML Stored Procedures(Transact-SQL)](../../relational-databases/system-stored-procedures/xml-stored-procedures-transact-sql.md)
 <br>[System Stored Procedures(Transact-SQL)](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)
 <br>[OPENXML(Transact-SQL)](../../t-sql/functions/openxml-transact-sql.md)
 <br>[sys.dm_exec_xml_handles (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-xml-handles-transact-sql.md)
 <br>[sp_xml_removedocument (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-xml-removedocument-transact-sql.md)
  
  
