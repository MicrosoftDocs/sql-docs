---
title: "replace value of (XML DML) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "XML DML [SQL Server]"
  - "update keyword"
  - "replacement values [XML DML]"
  - "updating node values"
  - "replace value of XML DML statement"
ms.assetid: c310f6df-7adf-493b-b56b-8e3143b13ae7
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# replace value of (XML DML)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Updates the value of a node in the document.  
  
## Syntax  
  
```  
  
replace value of Expression1   
with Expression2  
```  
  
## Arguments  
 *Expression1*  
 Identifies a node whose value is to be updated. It must identify only a single node. That is, *Expression1* must be a static singleton. If the XML is typed, the type of the node must be a simple type. If multiple nodes are selected, an error is raised. If *Expression1* returns an empty sequence, no value replacement occurs and no errors are returned. *Expression1* must return a single element that has simply typed content (list or atomic types), a text node, or an attribute node. *Expression1* cannot be a union type, a complex type, a processing instruction, a document node, or a comment node. If it is, an error is returned.  
  
 *Expression2*  
 Identifies the new value of the node. This can be an expression that returns a simply typed node, because **data()** will be used implicitly. If the value is a list of values, the **update** statement replaces the old value with the list. In modifying a typed XML instance, *Expression2* must be the same type or a subtype of *Expression*1. Otherwise, an error is returned. In modifying an untyped XML instance, *Expression2* must be an expression that can be atomized. Otherwise, an error is returned.  
  
## Examples  
 The following examples of the **replace value of** XML DML statement illustrates how to update nodes in an XML document.  
  
### A. Replacing values in an XML instance  
 In the following example, a document instance is first assigned to a variable of **xml** type. Then, **replace value of** XML DML statements update values in the document.  
  
```  
DECLARE @myDoc xml;  
SET @myDoc = '<Root>  
<Location LocationID="10"   
            LaborHours="1.1"  
            MachineHours=".2" >Manufacturing steps are described here.  
<step>Manufacturing step 1 at this work center</step>  
<step>Manufacturing step 2 at this work center</step>  
</Location>  
</Root>';  
SELECT @myDoc;  
  
-- update text in the first manufacturing step  
SET @myDoc.modify('  
  replace value of (/Root/Location/step[1]/text())[1]  
  with     "new text describing the manu step"  
');  
SELECT @myDoc;  
-- update attribute value  
SET @myDoc.modify('  
  replace value of (/Root/Location/@LaborHours)[1]  
  with     "100.0"  
');  
SELECT @myDoc;  
```  
  
 Note that the target being updated must be, at most, one node that is explicitly specified in the path expression by adding a "[1]" at the end of the expression.  
  
### B. Using the if expression to determine replacement value  
 You can specify the **if** expression in Expression2 of the **replace value of XML DML** statement, as shown in the following example. Expression1 identifies that the LaborHours attribute from the first work center is to be updated. Expression2 uses an **if** expression to determine the new value of the LaborHours attribute.  
  
```  
DECLARE @myDoc xml  
SET @myDoc = '<Root>  
<Location LocationID="10"   
            LaborHours=".1"  
            MachineHours=".2" >Manu steps are described here.  
<step>Manufacturing step 1 at this work center</step>  
<step>Manufacturing step 2 at this work center</step>  
</Location>  
</Root>'  
--SELECT @myDoc  
  
SET @myDoc.modify('  
  replace value of (/Root/Location[1]/@LaborHours)[1]  
  with (  
       if (count(/Root/Location[1]/step) > 3) then  
         "3.0"  
       else  
          "1.0"  
      )  
')  
SELECT @myDoc  
```  
  
### C. Updating XML stored in an untyped XML column  
 The following example updates XML stored in a column:  
  
```  
drop table T  
go  
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
-- verify the current <ProductDescription> element  
SELECT x.query(' /Root/ProductDescription')  
FROM T  
-- update the ProductName attribute value  
UPDATE T  
SET x.modify('  
  replace value of (/Root/ProductDescription/@ProductName)[1]  
  with "New Road Bike" ')  
-- verify the update  
SELECT x.query(' /Root/ProductDescription')  
FROM T  
```  
  
### D. Updating XML stored in a typed XML column  
 This example replaces values in a manufacturing instructions document stored in a typed XML column.  
  
 In the example, you first create a table (T) with a typed XML column in the AdventureWorks database. You then copy a manufacturing instructions XML instance from the Instructions column in the ProductModel table into table T. Insertions are then applied to XML in table T.  
  
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
--insert a new location - <Location 1000/>.   
update T  
set Instructions.modify('  
  declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
insert <MI:Location LocationID="1000"  LaborHours="1000"  LotSize="1000" >  
           <MI:step>Do something using <MI:tool>hammer</MI:tool></MI:step>  
         </MI:Location>  
  as first  
  into   (/MI:root)[1]  
')  
go  
select Instructions  
from T  
go  
-- Now replace manu. tool in location 1000  
update T  
set Instructions.modify('  
  declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
  replace value of (/MI:root/MI:Location/MI:step/MI:tool)[1]   
  with   "screwdriver"  
')  
go  
select Instructions  
from T  
-- Now replace value of lot size  
update T  
set Instructions.modify('  
  declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
  replace value of (/MI:root/MI:Location/@LotSize)[1]   
  with   500 cast as xs:decimal ?  
')  
go  
select Instructions  
from T  
```  
  
 Note the use of **cast** when replacing LotSize value. This is required when the value must be of a specific type. In this example, if 500 were the value, explicit casting would not be necessary.  
  
## See Also  
 [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md)   
 [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)   
 [XML Data Modification Language &#40;XML DML&#41;](../../t-sql/xml/xml-data-modification-language-xml-dml.md)  
  
  
