---
title: "Sequence Type Matching | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "sequence type matching [XQuery]"
  - "XQuery, sequence type matching"
ms.assetid: 8c56fb69-ca04-4aba-b55a-64ae216c492d
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Type System - Sequence Type Matching
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  An XQuery expression value is always a sequence of zero or more items. An item can be either an atomic value or a node. The sequence type refers to the ability to match the sequence type returned by a query expression with a specific type. For example:  
  
-   If the expression value is atomic, you may want to know if it is an integer, decimal, or string type.  
  
-   If the expression value is an XML node, you may want to know if it is a comment node, a processing instruction node, or a text node.  
  
-   You may want to know if the expression returns an XML element or an attribute node of a specific name and type.  
  
 You can use the `instance of` Boolean operator in sequence type matching. For more information about the `instance of` expression, see [SequenceType Expressions &#40;XQuery&#41;](../xquery/sequencetype-expressions-xquery.md).  
  
## Comparing the Atomic Value Type Returned by an Expression  
 If an expression returns a sequence of atomic values, you may have to find the type of the value in the sequence. The following examples illustrate how sequence type syntax can be used to evaluate the atomic value type returned by an expression.  
  
### Example: Determining whether a sequence is empty  
 The **empty()** sequence type can be used in a sequence type expression to determine whether the sequence returned by the specified expression is an empty sequence.  
  
 In the following example, the XML schema allows the <`root`> element to be nilled:  
  
```  
CREATE XML SCHEMA COLLECTION SC AS N'  
<schema xmlns="http://www.w3.org/2001/XMLSchema">  
      <element name="root" nillable="true" type="byte"/>  
</schema>'  
GO  
```  
  
 Now, if a typed XML instance specifies a value for the <`root`> element, `instance of empty()` returns False.  
  
```  
DECLARE @var XML(SC1)  
SET @var = '<root>1</root>'  
-- The following returns False  
SELECT @var.query('data(/root[1]) instance of  empty() ')  
GO  
```  
  
 If the <`root`> element is nilled in the instance, its value is an empty sequence and the `instance of empty()` returns True.  
  
```  
DECLARE @var XML(SC)  
SET @var = '<root xsi:nil="true" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" />'  
SELECT @var.query('data(/root[1]) instance of  empty() ')  
GO  
```  
  
### Example: Determining the type of an attribute value  
 Sometimes, you may want to evaluate the sequence type returned by an expression before processing. For example, you may have an XML schema in which a node is defined as a union type. In the following example, the XML schema in the collection defines the attribute `a` as a union type whose value can be of decimal or string type.  
  
```  
-- Drop schema collection if it exists.  
-- DROP XML SCHEMA COLLECTION SC.  
-- GO  
CREATE XML SCHEMA COLLECTION SC AS N'  
<schema xmlns="http://www.w3.org/2001/XMLSchema">  
  <element name="root">  
    <complexType>  
       <sequence/>  
         <attribute name="a">  
            <simpleType>  
               <union memberTypes="decimal string"/>  
            </simpleType>  
         </attribute>  
     </complexType>  
  </element>  
</schema>'  
GO  
```  
  
 Before processing a typed XML instance, you may want to know the type of the attribute `a` value. In the following example, the attribute `a` value is a decimal type. Therefore`, instance of xs:decimal` returns True.  
  
```  
DECLARE @var XML(SC)  
SET @var = '<root a="2.5"/>'  
SELECT @var.query('data((/root/@a)[1]) instance of xs:decimal')  
GO  
```  
  
 Now, change the attribute `a` value to a string type. The `instance of xs:string` will return True.  
  
```  
DECLARE @var XML(SC)  
SET @var = '<root a="Hello"/>'  
SELECT @var.query('data((/root/@a)[1]) instance of xs:string')  
GO  
```  
  
### Example: Cardinality in sequence expressions  
 This example illustrates the effect of cardinality in a sequence expression. The following XML schema defines a <`root`> element that is of byte type and is nillable.  
  
```  
CREATE XML SCHEMA COLLECTION SC AS N'  
<schema xmlns="http://www.w3.org/2001/XMLSchema">  
      <element name="root" nillable="true" type="byte"/>  
</schema>'  
GO  
```  
  
 In the following query, because the expression returns a singleton of byte type, `instance of` returns True.  
  
```  
DECLARE @var XML(SC)  
SET @var = '<root>111</root>'  
SELECT @var.query('data(/root[1]) instance of  xs:byte ')   
GO  
```  
  
 If you make the <`root`> element nil, its value is an empty sequence. That is, the expression, `/root[1]`, returns an empty sequence. Therefore, `instance of xs:byte` returns False. Note that the default cardinality in this case is 1.  
  
```  
DECLARE @var XML(SC)  
SET @var = '<root xsi:nil="true" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"></root>'  
SELECT @var.query('data(/root[1]) instance of  xs:byte ')   
GO  
-- result = false  
```  
  
 If you specify cardinality by adding the occurrence indicator (`?`), the sequence expression returns True.  
  
```  
DECLARE @var XML(SC)  
SET @var = '<root xsi:nil="true" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"></root>'  
SELECT @var.query('data(/root[1]) instance of  xs:byte? ')   
GO  
-- result = true  
```  
  
 Note that the testing in a sequence type expression is completed in two stages:  
  
1.  First, the testing determines whether the expression type matches the specified type.  
  
2.  If it does, the testing then determines whether the number of items returned by the expression matches the occurrence indicator specified.  
  
 If both are true, the `instance of` expression returns True.  
  
### Example: Querying against an xml type column  
 In the following example, a query is specified against an Instructions column of **xml** type in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database. It is a typed XML column because it has a schema associated with it. The XML schema defines the `LocationID` attribute of the integer type. Therefore, in the sequence expression, the `instance of xs:integer?` returns True.  
  
```  
SELECT Instructions.query('   
declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";   
data(/AWMI:root[1]/AWMI:Location[1]/@LocationID) instance of xs:integer?') as Result   
FROM Production.ProductModel   
WHERE ProductModelID = 7  
```  
  
## Comparing the Node Type Returned by an Expression  
 If an expression returns a sequence of nodes, you may have to find the type of the node in the sequence. The following examples illustrate how sequence type syntax can be used to evaluate the node type returned by an expression. You can use the following sequence types:  
  
-   **item()** - Matches any item in the sequence.  
  
-   **node()** - Determines whether the sequence is a node.  
  
-   **processing-instruction()** - Determines whether the expression returns a processing instruction.  
  
-   **comment()** - Determines whether the expression returns a comment.  
  
-   **document-node()** - Determines whether the expression returns a document node.  
  
 The following example illustrates these sequence types.  
  
### Example: Using sequence types  
 In this example, several queries are executed against an untyped XML variable. These queries illustrate the use of sequence types.  
  
```  
DECLARE @var XML  
SET @var = '<?xml-stylesheet href="someValue" type="text/xsl" ?>  
<root>text node  
  <!-- comment 1 -->   
  <a>Data a</a>  
  <!-- comment  2 -->  
</root>'  
```  
  
 In the first query, the expression returns the typed value of element <`a`>. In the second query, the expression returns element <`a`>. Both are items. Therefore, both queries return True.  
  
```  
SELECT @var.query('data(/root[1]/a[1]) instance of item()')  
SELECT @var.query('/root[1]/a[1] instance of item()')  
```  
  
 All the XQuery expressions in the following three queries return the element node child of the <`root`> element. Therefore, the sequence type expression, `instance of node()`, returns True, and the other two expressions, `instance of text()` and `instance of document-node()`, return False.  
  
```  
SELECT @var.query('(/root/*)[1] instance of node()')  
SELECT @var.query('(/root/*)[1] instance of text()')  
SELECT @var.query('(/root/*)[1] instance of document-node()')   
```  
  
 In the following query, the `instance of document-node()` expression returns True, because the parent of the <`root`> element is a document node.  
  
```  
SELECT @var.query('(/root/..)[1] instance of document-node()') -- true  
```  
  
 In the following query, the expression retrieves the first node from the XML instance. Because it is a processing instruction node, the `instance of processing-instruction()` expression returns True.  
  
```  
SELECT @var.query('(/node())[1] instance of processing-instruction()')  
```  
  
### Implementation Limitations  
 These are the specific limitations:  
  
-   **document-node()** with content type syntax is not supported.  
  
-   **processing-instruction(name)** syntax is not supported.  
  
## Element Tests  
 An element test is used to match the element node returned by an expression to an element node with a specific name and type. You can use these element tests:  
  
```  
element ()  
element(ElementName)  
element(ElementName, ElementType?)   
element(*, ElementType?)  
```  
  
## Attribute Tests  
 The attribute test determines whether the attribute returned by an expression is an attribute node. You can use these attribute tests.  
  
 `attribute()`  
  
 `attribute(AttributeName)`  
  
 `attribute(AttributeName, AttributeType)`  
  
## Test Examples  
 The following examples illustrate scenarios in which element and attribute tests are useful.  
  
### Example A  
 The following XML schema defines the `CustomerType` complex type where <`firstName`> and <`lastName`> elements are optional. For a specified XML instance, you may have to determine whether the first name exists for a particular customer.  
  
```  
CREATE XML SCHEMA COLLECTION SC AS N'  
<schema xmlns="http://www.w3.org/2001/XMLSchema"  
targetNamespace="myNS" xmlns:ns="myNS">  
  <complexType name="CustomerType">  
     <sequence>  
        <element name="firstName" type="string" minOccurs="0"   
                  nillable="true" />  
        <element name="lastName" type="string" minOccurs="0"/>  
     </sequence>  
  </complexType>  
  <element name="customer" type="ns:CustomerType"/>  
</schema>  
'  
GO  
DECLARE @var XML(SC)  
SET @var = '<x:customer xmlns:x="myNS">  
<firstName>SomeFirstName</firstName>  
<lastName>SomeLastName</lastName>  
</x:customer>'  
```  
  
 The following query uses an `instance of element (firstName)` expression to determine whether the first child element of <`customer`> is an element whose name is <`firstName`>. In this case, it returns True.  
  
```  
SELECT @var.query('declare namespace x="myNS";   
     (/x:customer/*)[1] instance of element (firstName)')  
GO  
```  
  
 If you remove the <`firstName`> element from the instance, the query will return False.  
  
 You can also use the following:  
  
-   The `element(ElementName, ElementType?)` sequence type syntax, as shown in the following query. It matches a nilled or non-nilled element node whose name is `firstName` and whose type is `xs:string`.  
  
    ```  
    SELECT @var.query('declare namespace x="myNS";   
    (/x:customer/*)[1] instance of element (firstName, xs:string?)')  
    ```  
  
-   The `element(*, type?)` sequence type syntax, as shown in the following query. It matches the element node if its type is `xs:string`, regardless of its name.  
  
    ```  
    SELECT @var.query('declare namespace x="myNS"; (/x:customer/*)[1] instance of element (*, xs:string?)')  
    GO  
    ```  
  
### Example B  
 The following example illustrates how to determine whether the node returned by an expression is an element node with a specific name. It uses the **element()** test.  
  
 In the following example, the two <`Customer`> elements in the XML instance that are being queried are of two different types, `CustomerType` and `SpecialCustomerType`. Assume that you want to know the type of the <`Customer`> element returned by the expression. The following XML schema collection defines the `CustomerType` and `SpecialCustomerType` types.  
  
```  
CREATE XML SCHEMA COLLECTION SC AS N'  
<schema xmlns="http://www.w3.org/2001/XMLSchema"  
          targetNamespace="myNS"  xmlns:ns="myNS">  
  <complexType name="CustomerType">  
    <sequence>  
      <element name="firstName" type="string"/>  
      <element name="lastName" type="string"/>  
    </sequence>  
  </complexType>  
  <complexType name="SpecialCustomerType">  
     <complexContent>  
       <extension base="ns:CustomerType">  
        <sequence>  
            <element name="Age" type="int"/>  
        </sequence>  
       </extension>  
     </complexContent>  
    </complexType>  
   <element name="customer" type="ns:CustomerType"/>  
</schema>  
'  
GO  
```  
  
 This XML schema collection is used to create a typed **xml** variable. The XML instance assigned to this variable has two <`customer`> elements of two different types. The first element is of `CustomerType` and the second element is of `SpecialCustomerType` type.  
  
```  
DECLARE @var XML(SC)  
SET @var = '  
<x:customer xmlns:x="myNS">  
   <firstName>FirstName1</firstName>  
   <lastName>LastName1</lastName>  
</x:customer>  
<x:customer xsi:type="x:SpecialCustomerType" xmlns:x="myNS" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
   <firstName> FirstName2</firstName>  
   <lastName> LastName2</lastName>  
   <Age>21</Age>  
</x:customer>'  
```  
  
 In the following query, the `instance of element (*, x:SpecialCustomerType ?)` expression returns False, because the expression returns the first customer element that is not of `SpecialCustomerType` type.  
  
```  
SELECT @var.query('declare namespace x="myNS";   
    (/x:customer)[1] instance of element (*, x:SpecialCustomerType ?)')  
```  
  
 If you change the expression of the previous query and retrieve the second <`customer`> element (`/x:customer)[2]`), the `instance of` will return True.  
  
### Example C  
 This example uses the attribute test. The following XML schema defines the CustomerType complex type with CustomerID and Age attributes. The Age attribute is optional. For a specific XML instance, you may want to determine whether the Age attribute is present in the <`customer`> element.  
  
```  
CREATE XML SCHEMA COLLECTION SC AS N'  
<schema xmlns="http://www.w3.org/2001/XMLSchema"  
       targetNamespace="myNS" xmlns:ns="myNS">  
<complexType name="CustomerType">  
  <sequence>  
     <element name="firstName" type="string" minOccurs="0"   
               nillable="true" />  
     <element name="lastName" type="string" minOccurs="0"/>  
  </sequence>  
  <attribute name="CustomerID" type="integer" use="required" />  
  <attribute name="Age" type="integer" use="optional" />  
 </complexType>  
 <element name="customer" type="ns:CustomerType"/>  
</schema>  
'  
GO  
```  
  
 The following query returns True, because there is an attribute node whose name is `Age` in the XML instance that is being queried. The `attribute(Age)` attribute test is used in this expression. Because attributes have no order, the query uses the FLWOR expression to retrieve all the attributes and then test each attribute by using the `instance of` expression. The example first creates an XML schema collection to create a typed **xml** variable.  
  
```  
DECLARE @var XML(SC)  
SET @var = '<x:customer xmlns:x="myNS" CustomerID="1" Age="22" >  
<firstName>SomeFName</firstName>  
<lastName>SomeLName</lastName>  
</x:customer>'  
SELECT @var.query('declare namespace x="myNS";   
FOR $i in /x:customer/@*  
RETURN  
    IF ($i instance of attribute (Age)) THEN  
        "true"  
        ELSE  
        ()')     
GO  
  
```  
  
 If you remove the optional `Age` attribute from the instance, the previous query will return False.  
  
 You can specify attribute name and type (`attribute(name,type)`) in the attribute test.  
  
```  
SELECT @var.query('declare namespace x="myNS";   
FOR $i in /x:customer/@*  
RETURN  
    IF ($i instance of attribute (Age, xs:integer)) THEN  
        "true"  
        ELSE  
        ()')  
```  
  
 Alternatively, you can specify the `attribute(*, type)` sequence type syntax. This matches the attribute node if the attribute type matches the specified type, regardless of the name.  
  
### Implementation Limitations  
 These are the specific limitations:  
  
-   In the element test, the type name must be followed by the occurrence indicator (**?**).  
  
-   **element(ElementName, TypeName)** is not supported.  
  
-   **element(\*, TypeName)** is not supported.  
  
-   **schema-element()** is not supported.  
  
-   **schema-attribute(AttributeName)** is not supported.  
  
-   Explicitly querying for **xsi:type** or **xsi:nil** is not supported.  
  
## See Also  
 [Type System &#40;XQuery&#41;](../xquery/type-system-xquery.md)  
  
  
