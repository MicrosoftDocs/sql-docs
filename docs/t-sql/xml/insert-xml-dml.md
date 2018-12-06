---
title: "insert (XML DML) | Microsoft Docs"
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
  - "inserting nodes"
  - "insert keyword [XML DML]"
  - "insert XML DML statement"
ms.assetid: 0c95c2b3-5cc2-4c38-9e25-86493096c442
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# insert (XML DML)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Inserts one or more nodes identified by *Expression1* as child nodes or siblings of the node identified by *Expression2*.  
  
## Syntax  
  
```  
  
insert   
      Expression1 (  
                 {as first | as last} into | after | before  
                                    Expression2  
                )  
```  
  
## Arguments  
 *Expression1*  
 Identifies one or more nodes to insert. This can be a constant XML instance; a reference to a typed XML data type instance of the same XML Schema collection on which the modify method is being applied; an untyped XML data type instance using a stand-alone **sql:column()**/**sql:variable()** function; or an XQuery expression. The expression can result in a node, and also a text node, or in an ordered sequence of nodes. It cannot resolve to the root (/) node. If the expression results in a value or a sequence of values, the values are inserted as a single text node with a space separating each value in the sequence. If you specify multiple nodes as constant, the nodes are included in parentheses and are separated by commas. You cannot insert heterogeneous sequences such as a sequence of elements, attributes, or values. If *Expression1* resolves to an empty sequence, no insertion occurs and no errors are returned.  
  
 into  
 Nodes identified by *Expression1* are inserted as direct descendents (child nodes) of the node identified by *Expression2*. If the node in *Expression2* already has one or more child nodes, you must use either **as first** or **as last** to specify where you want the new node added. For example, at the start or at the end of the child list, respectively. The **as first** and **as last** keywords are ignored when attributes are inserted.  
  
 after  
 Nodes identified by *Expression1* are inserted as siblings directly after the node identified by *Expression2*. The **after** keyword cannot be used to insert attributes. For example, it cannot be used to insert an attribute constructor or to return an attribute from an XQuery.  
  
 before  
 Nodes identified by *Expression1* are inserted as siblings directly before the node identified by *Expression2*. The **before** keyword cannot be used when attributes are being inserted. For example, it cannot be used to insert an attribute constructor or to return an attribute from an XQuery.  
  
 *Expression2*  
 Identifies a node. The nodes identified in *Expression1* are inserted relative to the node identified by *Expression2*. This can be an XQuery expression that returns a reference to a node that exists in the currently referenced document. If more than one node is returned, the insert fails. If *Expression2* returns an empty sequence, no insertion occurs and no errors are returned. If *Expression2* is statically not a singleton, a static error is returned. *Expression2* cannot be a processing instruction, comment, or attribute. Note that *Expression2* must be a reference to an existing node in the document and not a constructed node.  
  
## Examples  
  
### A. Inserting element nodes into the document  
 The following example illustrates how to insert elements into a document. First, an XML document is assigned to a variable of **xml** type. Then, through several **insert** XML DML statements, the example illustrates how element nodes are inserted in the document. After each insert, the SELECT statement displays the result.  
  
```  
USE AdventureWorks;  
GO  
DECLARE @myDoc xml;         
SET @myDoc = '<Root>         
    <ProductDescription ProductID="1" ProductName="Road Bike">         
        <Features>         
        </Features>         
    </ProductDescription>         
</Root>'  ;       
SELECT @myDoc;     
-- insert first feature child (no need to specify as first or as last)         
SET @myDoc.modify('         
insert <Maintenance>3 year parts and labor extended maintenance is available</Maintenance>   
into (/Root/ProductDescription/Features)[1]') ;  
SELECT @myDoc ;        
-- insert second feature. We want this to be the first in sequence so use 'as first'         
set @myDoc.modify('         
insert <Warranty>1 year parts and labor</Warranty>          
as first         
into (/Root/ProductDescription/Features)[1]         
')  ;       
SELECT @myDoc  ;       
-- insert third feature child. This one is the last child of <Features> so use 'as last'         
SELECT @myDoc         
SET @myDoc.modify('         
insert <Material>Aluminium</Material>          
as last         
into (/Root/ProductDescription/Features)[1]         
')         
SELECT @myDoc ;        
-- Add fourth feature - this time as a sibling (and not a child)         
-- 'after' keyword is used (instead of as first or as last child)         
SELECT @myDoc  ;       
set @myDoc.modify('         
insert <BikeFrame>Strong long lasting</BikeFrame>   
after (/Root/ProductDescription/Features/Material)[1]         
')  ;       
SELECT @myDoc;  
GO  
```  
  
 Note that various path expressions in this example specify "[1]" as a per-static typing requirement. This ensures a single target node.  
  
### B. Inserting multiple elements into the document  
 In the following example, a document is first assigned to a variable of **xml** type. Then, a sequence of two elements, representing product features, is assigned to a second variable of **xml** type. This sequence is then inserted into the first variable.  
  
```  
USE AdventureWorks;  
GO  
DECLARE @myDoc xml;  
SET @myDoc = N'<Root>             
<ProductDescription ProductID="1" ProductName="Road Bike">             
    <Features> </Features>             
</ProductDescription>             
</Root>';  
DECLARE @newFeatures xml;  
SET @newFeatures = N'<Warranty>1 year parts and labor</Warranty>            
<Maintenance>3 year parts and labor extended maintenance is available</Maintenance>';           
-- insert new features from specified variable            
SET @myDoc.modify('             
insert sql:variable("@newFeatures")             
into (/Root/ProductDescription/Features)[1] ')             
SELECT @myDoc;  
GO  
```  
  
### C. Inserting attributes into a document  
 The following example illustrates how attributes are inserted in a document. First, a document is assigned to an **xml** type variable. Then, a series of **insert** XML DML statements is used to insert attributes into the document. After each attribute insertion, the SELECT statement displays the result.  
  
```  
USE AdventureWorks;  
GO  
DECLARE @myDoc xml ;            
SET @myDoc =   
'<Root>             
    <Location LocationID="10" >             
        <step>Manufacturing step 1 at this work center</step>             
        <step>Manufacturing step 2 at this work center</step>             
    </Location>             
</Root>' ;  
SELECT @myDoc  ;          
-- insert LaborHours attribute             
SET @myDoc.modify('             
insert attribute LaborHours {".5" }             
into (/Root/Location[@LocationID=10])[1] ') ;           
SELECT @myDoc  ;          
-- insert MachineHours attribute but its value is retrived from a sql variable @Hrs             
DECLARE @Hrs float ;            
SET @Hrs =.2   ;          
SET @myDoc.modify('             
insert attribute MachineHours {sql:variable("@Hrs") }             
into   (/Root/Location[@LocationID=10])[1] ');            
SELECT @myDoc;             
-- insert sequence of attribute nodes (note the use of ',' and ()              
-- around the attributes.             
SET @myDoc.modify('             
insert (              
           attribute SetupHours {".5" },             
           attribute SomeOtherAtt {".2"}             
        )             
into (/Root/Location[@LocationID=10])[1] ');             
SELECT @myDoc;  
GO  
```  
  
### D. Inserting a comment node  
 In this query, an XML document is first assigned to a variable of **xml** type. Then, XML DML is used to insert a comment node after the first <`step`> element.  
  
```  
USE AdventureWorks;  
GO  
DECLARE @myDoc xml;             
SET @myDoc =   
'<Root>             
    <Location LocationID="10" >             
        <step>Manufacturing step 1 at this work center</step>             
        <step>Manufacturing step 2 at this work center</step>             
    </Location>             
</Root>' ;           
SELECT @myDoc;             
SET @myDoc.modify('             
insert <!-- some comment -->             
after (/Root/Location[@LocationID=10]/step[1])[1] ');            
SELECT @myDoc;  
GO  
```  
  
### E. Inserting a processing instruction  
 In the following query, an XML document is first assigned to a variable of **xml** type. Then, the XML DML keyword is used to insert a processing instruction at the start of the document.  
  
```  
USE AdventureWorks;  
GO  
DECLARE @myDoc xml;  
SET @myDoc =   
'<Root>   
    <Location LocationID="10" >   
        <step>Manufacturing step 1 at this work center</step>   
        <step>Manufacturing step 2 at this work center</step>   
    </Location>   
</Root>' ;  
SELECT @myDoc ;  
SET @myDoc.modify('   
insert <?Program = "Instructions.exe" ?>   
before (/Root)[1] ') ;  
SELECT @myDoc ;  
GO  
```  
  
### F. Inserting data using a CDATA section  
 When you insert text that includes characters that are not valid in XML, such as < or >, you can use CDATA sections to insert the data as shown in the following query. The query specifies a CDATA section, but it is added as a text node with any invalid characters converted to entities. For example, '<' is saved as &lt;.  
  
```  
USE AdventureWorks;  
GO  
DECLARE @myDoc xml;             
SET @myDoc =   
'<Root>             
    <ProductDescription ProductID="1" ProductName="Road Bike">             
        <Features> </Features>             
    </ProductDescription>             
</Root>' ;            
SELECT @myDoc ;            
SET @myDoc.modify('             
insert <![CDATA[ <notxml> as text </notxml> or cdata ]]>   
into  (/Root/ProductDescription/Features)[1] ') ;   
SELECT @myDoc ;  
GO  
```  
  
 The query inserts a text node into the <`Features`> element:  
  
```  
<Root>  
<ProductDescription ProductID="1" ProductName="Road Bike">  
<Features> <notxml> as text </notxml> or cdata </Features>  
</ProductDescription>  
</Root>       
```  
  
### G. Inserting text node  
 In this query, an XML document is first assigned to a variable of **xml** type. Then, XML DML is used to insert a text node as the first child of the <`Root`> element. The text constructor is used to specify the text.  
  
```  
USE AdventureWorks;  
GO  
DECLARE @myDoc xml;  
SET @myDoc = '<Root>  
<ProductDescription ProductID="1" ProductName="Road Bike">  
<Features>  
  
</Features>  
</ProductDescription>  
</Root>'  
SELECT @myDoc;  
set @myDoc.modify('  
 insert text{"Product Catalog Description"}   
 as first into (/Root)[1]  
');  
SELECT @myDoc;  
```  
  
### H. Inserting a new element into an untyped xml column  
 The following example applies XML DML to update an XML instance stored in an **xml** type column:  
  
```  
USE AdventureWorks;  
GO  
CREATE TABLE T (i int, x xml);  
go  
INSERT INTO T VALUES(1,'<Root>  
    <ProductDescription ProductID="1" ProductName="Road Bike">  
        <Features>  
            <Warranty>1 year parts and labor</Warranty>  
            <Maintenance>3 year parts and labor extended maintenance is available</Maintenance>  
        </Features>  
    </ProductDescription>  
</Root>');  
go  
-- insert a new element  
UPDATE T  
SET x.modify('insert <Material>Aluminium</Material> as first  
  into   (/Root/ProductDescription/Features)[1]  
');  
GO  
```  
  
 Again, when the <`Material`> element node is inserted, the path expression must return a single target. This is explicitly specified by adding a [1] at the end of the expression.  
  
```  
-- check the update  
SELECT x.query(' //ProductDescription/Features')  
FROM T;  
GO  
```  
  
### I. Inserting based on an if condition statement  
 In the following example, an IF condition is specified as part of Expression1 in the **insert** XML DML statement. If the condition is True, an attribute is added to the <`WorkCenter`> element.  
  
```  
USE AdventureWorks;  
GO  
DECLARE @myDoc xml;  
SET @myDoc =   
'<Root>  
    <Location LocationID="10" LaborHours="1.2" >  
        <step>Manufacturing step 1 at this work center</step>  
    <step>Manufacturing step 2 at this work center</step>  
    </Location>  
</Root>';  
SELECT @myDoc  
SET @myDoc.modify('  
insert  
if (/Root/Location[@LocationID=10])  
then attribute MachineHours {".5"}  
else ()  
    as first into   (/Root/Location[@LocationID=10])[1] ');  
SELECT @myDoc;  
GO  
```  
  
 The following example is similar, except that the **insert** XML DML statement inserts an element in the document if the condition is True. That is, if the <`WorkCenter`> element has less than or is equal to two <`step`> child elements.  
  
```  
USE AdventureWorks;  
GO  
DECLARE @myDoc xml;  
SET @myDoc =   
'<Root>  
    <Location LocationID="10" LaborHours="1.2" >  
        <step>Manufacturing step 1 at this work center</step>  
        <step>Manufacturing step 2 at this work center</step>  
    </Location>  
</Root>';  
SELECT @myDoc;  
SET @myDoc.modify('  
insert  
if (count(/Root/Location/step) <= 2)  
then element step { "This is a new step" }  
else ()  
    as last into   (/Root/Location[@LocationID=10])[1] ');  
SELECT @myDoc;  
GO  
```  
  
 This is the result:  
  
```  
<Root>  
 <WorkCenter WorkCenterID="10" LaborHours="1.2">  
  <step>Manufacturing step 1 at this work center</step>  
  <step>Manufacturing step 2 at this work center</step>  
  <step>This is a new step</step>  
 </WorkCenter>  
```  
  
### J. Inserting nodes in a typed xml column  
 This example inserts an element and an attribute into a manufacturing instructions XML stored in a typed **xml** column.  
  
 In the example, you first create a table (T) with a typed **xml** column, in the AdventureWorks database. You then copy a manufacturing instructions XML instance from the Instructions column in the ProductModel table into table T. Insertions are then applied to XML in table T.  
  
```  
USE AdventureWorks;  
GO            
DROP TABLE T;  
GO             
CREATE TABLE T(ProductModelID int primary key,    
Instructions xml (Production.ManuInstructionsSchemaCollection));  
GO  
INSERT T              
    SELECT ProductModelID, Instructions             
    FROM Production.ProductModel             
    WHERE ProductModelID=7;  
GO             
SELECT Instructions             
FROM T;  
-- now insertion begins             
--1) insert a new manu. Location. The <Root> specified as              
-- expression 2 in the insert() must be singleton.      
UPDATE T   
set Instructions.modify('   
declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";   
insert <MI:Location LocationID="1000" >   
           <MI:step>New instructions go here</MI:step>   
         </MI:Location>   
as first   
into   (/MI:root)[1]   
') ;  
  
SELECT Instructions             
FROM T ;  
-- 2) insert attributes in the new <Location>             
UPDATE T             
SET Instructions.modify('             
declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";             
insert attribute LaborHours { "1000" }             
into (/MI:root/MI:Location[@LocationID=1000])[1] ');   
GO             
SELECT Instructions             
FROM T ;  
GO             
--cleanup             
DROP TABLE T ;  
GO             
```  
  
## See Also  
 [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md)   
 [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)   
 [XML Data Modification Language &#40;XML DML&#41;](../../t-sql/xml/xml-data-modification-language-xml-dml.md)  
  
  
