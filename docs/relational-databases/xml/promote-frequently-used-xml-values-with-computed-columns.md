---
title: "Promote Frequently Used XML Values with Computed Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "promoting properties [XML in SQL Server]"
  - "property promotion [XML in SQL Server]"
ms.assetid: f5111896-c2fd-4209-b500-f2baa45489ad
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Promote Frequently Used XML Values with Computed Columns
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  If queries are made principally on a small number of element and attribute values, you may want to promote those quantities into relational columns. This is helpful when queries are issued on a small part of the XML data while the whole XML instance is retrieved. Creating an XML index on the XML column is not required. Instead, the promoted column can be indexed. Queries must be written to use the promoted column. That is, the query optimizer does not target again the queries on the XML column to the promoted column.  
  
 The promoted column can be a computed column in the same table or it can be a separate, user-maintained column in a table. This is sufficient when singleton values are promoted from each XML instance. However, for multi-valued properties, you have to create a separate table for the property, as described in the following section.  
  
## Computed Column Based on the xml Data Type  
 A computed column can be created by using a user-defined function that invokes **xml** data type methods. The type of the computed column can be any SQL type, including XML. This is illustrated in the following example.  
  
### Example: Computed Column Based on the xml Data Type Method  
 Create the user-defined function for a book ISBN number:  
  
```  
CREATE FUNCTION udf_get_book_ISBN (@xData xml)  
RETURNS varchar(20)  
BEGIN  
   DECLARE @ISBN   varchar(20)  
   SELECT @ISBN = @xData.value('/book[1]/@ISBN', 'varchar(20)')  
   RETURN @ISBN   
END  
```  
  
 Add a computed column to the table for the ISBN:  
  
```  
ALTER TABLE      T  
ADD   ISBN AS dbo.udf_get_book_ISBN(xCol)  
```  
  
 The computed column can be indexed in the usual way.  
  
### Example: Queries on a Computed Column Based on xml Data Type Methods  
 To obtain the <`book`> whose ISBN is 0-7356-1588-2:  
  
```  
SELECT xCol  
FROM   T  
WHERE  xCol.exist('/book/@ISBN[. = "0-7356-1588-2"]') = 1  
```  
  
 The query on the XML column can be rewritten to use the computed column as follows:  
  
```  
SELECT xCol  
FROM   T  
WHERE  ISBN = '0-7356-1588-2'  
```  
  
 You can create a user-defined function to return the **xml** data type and a computed column by using the user-defined function. However, you cannot create an XML index on the computed, XML column.  
  
## Creating Property Tables  
 You may want to promote some of the multivalued properties from your XML data into one or more tables, create indexes on those tables, and target again your queries to use them. A typical scenario is one in which a small number of properties covers most of your query workload. You can do the following:  
  
-   Create one or more tables to hold the multivalued properties. You may find it convenient to store one property per table and duplicate the primary key of the base table in the property tables for back joining with the base table.  
  
-   If you want to maintain the relative order of the properties, you have to introduce a separate column for the relative order.  
  
-   Create triggers on the XML column to maintain the property tables. Within the triggers, do one of the following:  
  
    -   Use **xml** data type methods, such as **nodes()** and **value()**, to insert and delete rows of the property tables.  
  
    -   Create streaming table-valued functions in the common language runtime (CLR) to insert and delete rows of the property tables.  
  
    -   Write queries for SQL access to the property tables and for XML access to the XML column in the base table, with joins between the tables by using their primary key.  
  
### Example: Create a Property Table  
 For illustration, assume that you want to promote the first name of the authors. Books have one or more authors, so that first name is a multivalued property. Each first name is stored in a separate row of a property table. The primary key of the base table is duplicated in the property table for back join.  
  
```  
create table tblPropAuthor (propPK int, propAuthor varchar(max))  
```  
  
### Example: Create a User-defined Function to Generate a Rowset from an XML Instance  
 The following table-valued function, udf_XML2Table, accepts a primary key value and an XML instance. It retrieves the first name of all authors of the <`book`> elements and returns a rowset of primary key, first name pairs.  
  
```  
create function udf_XML2Table (@pk int, @xCol xml)  
returns @ret_Table table (propPK int, propAuthor varchar(max))  
with schemabinding  
as  
begin  
      insert into @ret_Table   
      select @pk, nref.value('.', 'varchar(max)')  
      from   @xCol.nodes('/book/author/first-name') R(nref)  
      return  
end  
```  
  
### Example: Create Triggers to Populate a Property Table  
 The insert trigger inserts rows into the property table:  
  
```  
create trigger trg_docs_INS on T for insert  
as  
      declare @wantedXML xml  
      declare @FK int  
      select @wantedXML = xCol from inserted  
      select @FK = PK from inserted  
  
   insert into tblPropAuthor  
   select * from dbo.udf_XML2Table(@FK, @wantedXML)  
```  
  
 The delete trigger deletes the rows from the property table based on the primary key value of the deleted rows:  
  
```  
create trigger trg_docs_DEL on T for delete  
as  
   declare @FK int  
   select @FK = PK from deleted  
   delete tblPropAuthor where propPK = @FK  
```  
  
 The update trigger deletes the existing rows in the property table corresponding to the updated XML instance and inserts new rows into the property table:  
  
```  
create trigger trg_docs_UPD  
on T  
for update  
as  
if update(xCol) or update(pk)  
begin  
      declare @FK int  
      declare @wantedXML xml  
      select @FK = PK from deleted  
      delete tblPropAuthor where propPK = @FK  
  
   select @wantedXML = xCol from inserted  
   select @FK = pk from inserted  
  
   insert into tblPropAuthor   
      select * from dbo.udf_XML2Table(@FK, @wantedXML)  
end  
```  
  
### Example: Find XML Instances Whose Authors Have the Same First Name  
 The query can be formed on the XML column. Alternatively, it can search the property table for first name "David" and perform a back join with the base table to return the XML instance. For example:  
  
```  
SELECT xCol   
FROM     T JOIN tblPropAuthor ON T.pk = tblPropAuthor.propPK  
WHERE    tblPropAuthor.propAuthor = 'David'  
```  
  
### Example: Solution Using the CLR Streaming Table-valued Function  
 This solution is made up of the following steps:  
  
1.  Define a CLR class, SqlReaderBase, that implements ISqlReader and generates a streaming, table-valued output by applying a path expression on an XML instance.  
  
2.  Create an assembly and a Transact-SQL user-defined function to start the CLR class.  
  
3.  Define the insert, update, and delete triggers by using the user-defined function to maintain a property tables.  
  
 To do this, you first create the streaming CLR function. The **xml** data type is exposed as a managed class SqlXml in ADO.NET and supports the **CreateReader()** method that returns an XmlReader.  
  
> [!NOTE]  
>  The example code in this section uses XPathDocument and XPathNavigator. These force you to load all the XML documents into memory. If you are using similar code in your application to process several large XML documents, this code is not scalable. Instead, keep memory allocations small and use streaming interfaces whenever possible. For more information about performance, see [Architecture of CLR Integration](https://msdn.microsoft.com/library/05e4b872-3d21-46de-b4d5-739b5f2a0cf9).  
  
```  
public class c_streaming_xml_tvf {  
   public static ISqlReader streaming_xml_tvf   
(SqlXml xmlDoc, string pathExpression) {  
      return (new TestSqlReaderBase (xmlDoc, pathExpression));  
   }  
}  
  
// Class that implements ISqlReader  
public class TestSqlReaderBase : ISqlReader {  
XPathNodeIterator m_iterator;           
   public SqlChars FirstName;  
// Metadata for current resultset  
private SqlMetaData[] m_rgSqlMetaData;        
  
   public TestSqlReaderBase (SqlXml xmlDoc, string pathExpression) {     
      // Variables for XPath navigation  
      XPathDocument xDoc;  
      XPathNavigator xNav;  
      XPathExpression xPath;  
  
      // Set sql metadata  
      m_rgSqlMetaData = new SqlMetaData[1];  
      m_rgSqlMetaData[0] = new SqlMetaData ("FirstName",    
SqlDbType.NVarChar,50);     
  
      //Set up the Navigator  
      if (!xmlDoc.IsNull)  
          xDoc = new XPathDocument (xmlDoc.CreateReader());  
      else  
          xDoc = new XPathDocument ();  
      xNav = xDoc.CreateNavigator();  
      xPath = xNav.Compile (pathExpression);  
      m_iterator = xNav.Select(xPath);  
   }  
   public bool Read() {  
      bool moreRows = true;  
      if (moreRows = m_iterator.MoveNext())  
         FirstName = new SqlChars (m_iterator.Current.Value);  
      return moreRows;  
   }  
}  
```  
  
 Next, create an assembly and a [!INCLUDE[tsql](../../includes/tsql-md.md)] user-defined function, SQL_streaming_xml_tvf (not shown), that corresponds to the CLR function, streaming_xml_tvf. The user-defined function is used to define the table-valued function, CLR_udf_XML2Table, for rowset generation:  
  
```  
create function CLR_udf_XML2Table (@pk int, @xCol xml)  
returns @ret_Table table (FK int, FirstName varchar(max))  
with schemabinding  
as  
begin  
      insert into @ret_Table   
   select @pk, FirstName   
   FROM   SQL_streaming_xml_tvf (@xCol, '/book/author/first-name')  
      return  
end  
```  
  
 Finally, define triggers as shown in the example, "Create triggers to populate a property table", but replace udf_XML2Table with the CLR_udf_XML2Table function. The insert trigger is shown in the following example:  
  
```  
create trigger CLR_trg_docs_INS on T for insert  
as  
   declare @wantedXML xml  
   declare @FK int  
   select @wantedXML = xCol from inserted  
   select @FK = PK from inserted  
  
   insert into tblPropAuthor  
      select *  
   from    dbo.CLR_udf_XML2Table(@FK, @wantedXML)  
```  
  
 The delete trigger is identical to the non-CLR version. However, the update trigger just replaces the function udf_XML2Table() with CLR_udf_XML2Table().  
  
## See Also  
 [Use XML in Computed Columns](../../relational-databases/xml/use-xml-in-computed-columns.md)  
  
  
