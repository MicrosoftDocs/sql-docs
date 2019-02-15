---
title: "delete (XML DML) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "XML DML [SQL Server]"
  - "delete keyword"
  - "delete statement [XML DML]"
  - "deleting nodes"
ms.assetid: b22c93a4-b84d-4356-af4c-6013322a4b71
author: MightyPen
ms.author: genemi
manager: "craigg"
---
# delete (XML DML)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Deletes nodes from an XML instance.  
  
## Syntax  
  
```  
  
delete Expression  
```  
  
## Arguments  
 *Expression*  
 Is an XQuery expression identifying the nodes to be deleted. All the nodes selected by the expression, and also all the nodes or values that are contained within the selected nodes, are deleted. As described in [insert (XML DML)](../../t-sql/xml/insert-xml-dml.md), this must be a reference to an existing node in the document. It cannot be a constructed node. The expression cannot be the root (/) node. If the expression returns an empty sequence, no deletion occurs and no errors are returned.  
  
## Examples  
  
### A. Deleting nodes from a document stored in an untyped xml variable  
 The following example illustrates how to delete various nodes from a document. First, an XML instance is assigned to variable of **xml** type. Then, subsequent delete XML DML statements delete various nodes from the document.  
  
```  
DECLARE @myDoc xml  
SET @myDoc = '<?Instructions for=TheWC.exe ?>   
<Root>  
 <!-- instructions for the 1st work center -->  
<Location LocationID="10"   
            LaborHours="1.1"  
            MachineHours=".2" >Some text 1  
<step>Manufacturing step 1 at this work center</step>  
<step>Manufacturing step 2 at this work center</step>  
</Location>  
</Root>'  
SELECT @myDoc  
  
-- delete an attribute  
SET @myDoc.modify('  
  delete /Root/Location/@MachineHours  
')  
SELECT @myDoc  
  
-- delete an element  
SET @myDoc.modify('  
  delete /Root/Location/step[2]  
')  
SELECT @myDoc  
  
-- delete text node (in <Location>  
SET @myDoc.modify('  
  delete /Root/Location/text()  
')  
SELECT @myDoc  
  
-- delete all processing instructions  
SET @myDoc.modify('  
  delete //processing-instruction()  
')  
SELECT @myDoc  
```  
  
### B. Deleting nodes from a document stored in an untyped xml column  
 In the following example, a **delete** XML DML statement removes the second child element of <`Features`> from the document stored in the column.  
  
```  
CREATE TABLE T (i int, x xml)  
go  
INSERT INTO T VALUES(1,'<Root>  
<ProductDescription ProductID="1" ProductName="Road Bike">  
<Features>  
  <Warranty>1 year parts and labor</Warranty>  
  <Maintenance>3 year parts and labor extended maintenance is available</Maintenance>  
</Features>  
</ProductDescription>  
</Root>')  
go  
-- verify the contents before delete  
SELECT x.query(' //ProductDescription/Features')  
FROM T  
-- delete the second feature  
UPDATE T  
SET x.modify('delete /Root/ProductDescription/Features/*[2]')  
-- verify the deletion  
SELECT x.query(' //ProductDescription/Features')  
FROM T  
```  
  
 Note the following from the previous query:  
  
-   The [modify() Method (xml Data Type)](../../t-sql/xml/modify-method-xml-data-type.md) is used to specify the **delete** XML DML keyword.  
  
-   The [query() Method (xml Data Type)](../../t-sql/xml/query-method-xml-data-type.md) is used to query the document.  
  
### C. Deleting nodes from a typed xml column  
 This example deletes nodes from a manufacturing instructions XML document stored in a typed **xml** column.  
  
 In the example, you first create a table (T) with a typed **xml** column in the AdventureWorks database. You then copy a manufacturing instructions XML instance from the Instructions column in the ProductModel table into table T and delete one or more nodes from the document.  
  
```  
use AdventureWorks  
go  
drop table T  
go  
create table T(ProductModelID int primary key,   
Instructions xml (Production.ManuInstructionsSchemaCollection))  
go  
insert  T   
select ProductModelID, Instructions  
from Production.ProductModel  
where ProductModelID=7  
go  
select Instructions  
from T  
--1) insert <Location 1000/>. Note: <Root> must be singleton in the query  
update T  
set Instructions.modify('  
  declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
  insert <MI:Location LocationID="1000"  LaborHours="1000" >  
           These are manu steps at location 1000.   
           <MI:step>New step1 instructions</MI:step>  
           Instructions for step 2 are here  
           <MI:step>New step 2 instructions</MI:step>  
         </MI:Location>  
  as first  
  into   (/MI:root)[1]  
')  
go  
select Instructions  
from T  
  
-- delete an attribute  
update T  
set Instructions.modify('  
  declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
  delete(/MI:root/MI:Location[@LocationID=1000]/@LaborHours)   
')  
go  
select Instructions  
from T  
-- delete text in <location>  
update T  
set Instructions.modify('  
  declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
  delete(/MI:root/MI:Location[@LocationID=1000]/text())   
')  
go  
select Instructions  
from T  
-- delete 2nd manu step at location 1000  
update T  
set Instructions.modify('  
  declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
  delete(/MI:root/MI:Location[@LocationID=1000]/MI:step[2])   
')  
go  
select Instructions  
from T  
-- cleanup  
drop table T  
go  
```  
  
## See Also  
 [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md)   
 [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)   
 [XML Data Modification Language &#40;XML DML&#41;](../../t-sql/xml/xml-data-modification-language-xml-dml.md)  
  
  
