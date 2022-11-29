---
title: "XML Construction (XQuery) | Microsoft Docs"
description: Learn how to construct XML structures in an XQuery using the direct and computed constructors.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "white space [XQuery]"
  - "computed constructor"
  - "construct XML structures [XQuery]"
  - "constructors [XQuery]"
  - "prolog"
  - "direct constructor [SQL Server]"
  - "XML [SQL Server], construction"
  - "XQuery, XML construction"
ms.assetid: a6330b74-4e52-42a4-91ca-3f440b3223cf
author: rothja
ms.author: jroth
---
# XML Construction (XQuery)
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  In XQuery, you can use the **direct** and **computed** constructors to construct XML structures within a query.  
  
> [!NOTE]  
>  There is no difference between the **direct** and **computed** constructors.  
  
## Using Direct Constructors  
 When you use direct constructors, you specify XML-like syntax when you construct the XML. The following examples illustrate XML construction by the direct constructors.  
  
### Constructing Elements  
 In using XML notations, you can construct elements. The following example uses the direct element constructor expression and creates a \<ProductModel> element. The constructed element has three child elements  
  
-   A text node.  
  
-   Two element nodes, \<Summary> and \<Features>.  
  
    -   The \<Summary> element has one text node child whose value is "Some description".  
  
    -   The \<Features> element has three element node children, \<Color>, \<Weight>, and \<Warranty>. Each of these nodes has one text node child and have the values Red, 25, 2 years parts and labor, respectively.  
  
```sql
declare @x xml;  
set @x='';  
select @x.query('<ProductModel ProductModelID="111">;  
This is product model catalog description.  
<Summary>Some description</Summary>  
<Features>  
  <Color>Red</Color>  
  <Weight>25</Weight>  
  <Warranty>2 years parts and labor</Warranty>  
</Features></ProductModel>')  
  
```  
  
 This is the resulting XML:  
  
```xml
<ProductModel ProductModelID="111">  
  This is product model catalog description.  
  <Summary>Some description</Summary>  
  <Features>  
    <Color>Red</Color>  
    <Weight>25</Weight>  
    <Warranty>2 years parts and labor</Warranty>  
  </Features>  
</ProductModel>  
```  
  
 Although constructing elements from constant expressions, as shown in this example, is useful, the true power of this XQuery language feature is the ability to construct XML that dynamically extracts data from a database. You can use curly braces to specify query expressions. In the resulting XML, the expression is replaced by its value. For example, the following query constructs a <`NewRoot`> element with one child element (<`e`>). The value of element <`e`> is computed by specifying a path expression inside curly braces ("{ ... }").  
  
```sql
DECLARE @x xml;  
SET @x='<root>5</root>';  
SELECT @x.query('<NewRoot><e> { /root } </e></NewRoot>');  
```  
  
 The braces act as context-switching tokens and switch the query from XML construction to query evaluation. In this case, the XQuery path expression inside the braces, `/root`, is evaluated and the results are substituted for it.  
  
 This is the result:  
  
```xml
<NewRoot>  
  <e>  
    <root>5</root>  
  </e>  
</NewRoot>  
```  
  
 The following query is similar to the previous one. However, the expression in the curly braces specifies the **data()** function to retrieve the atomic value of the <`root`> element and assigns it to the constructed element, <`e`>.  
  
```sql
DECLARE @x xml;  
SET @x='<root>5</root>';  
DECLARE @y xml;  
SET @y = (SELECT @x.query('  
                           <NewRoot>  
                             <e> { data(/root) } </e>  
                           </NewRoot>' ));  
SELECT @y;  
```  
  
 This is the result:  
  
```xml
<NewRoot>  
  <e>5</e>  
</NewRoot>  
```  
  
 If you want to use the curly braces as part of your text instead of context-switching tokens, you can escape them as "}}" or "{{", as shown in this example:  
  
```sql
DECLARE @x xml;  
SET @x='<root>5</root>';  
DECLARE @y xml;  
SET @y = (SELECT @x.query('  
<NewRoot> Hello, I can use {{ and  }} as part of my text</NewRoot>'));  
SELECT @y;  
```  
  
 This is the result:  
  
```xml
<NewRoot> Hello, I can use { and  } as part of my text</NewRoot>  
```  
  
 The following query is another example of constructing elements by using the direct element constructor. Also, the value of the <`FirstLocation`> element is obtained by executing the expression in the curly braces. The query expression returns the manufacturing steps at the first work center location from the Instructions column of the Production.ProductModel table.  
  
```sql
SELECT Instructions.query('  
    declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
        <FirstLocation>  
           { /AWMI:root/AWMI:Location[1]/AWMI:step }  
        </FirstLocation>   
') as Result   
FROM Production.ProductModel  
WHERE ProductModelID=7;  
```  
  
 This is the result:  
  
```xml
<FirstLocation>  
  <AWMI:step xmlns:AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">  
      Insert <AWMI:material>aluminum sheet MS-2341</AWMI:material> into the <AWMI:tool>T-85A framing tool</AWMI:tool>.   
  </AWMI:step>  
  <AWMI:step xmlns:AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">  
      Attach <AWMI:tool>Trim Jig TJ-26</AWMI:tool> to the upper and lower right corners of the aluminum sheet.   
  </AWMI:step>  
   ...  
</FirstLocation>  
```  
  
#### Element Content in XML Construction  
 The following example illustrates the behavior of the expressions in constructing element content by using the direct element constructor. In the following example, the direct element constructor specifies one expression. For this expression, one text node is created in the resulting XML.  
  
```sql
declare @x xml;  
set @x='  
<root>  
  <step>This is step 1</step>  
  <step>This is step 2</step>  
  <step>This is step 3</step>  
</root>';  
select @x.query('  
<result>  
 { for $i in /root[1]/step  
    return string($i)  
 }  
</result>');  
  
```  
  
 The atomic value sequence resulting from the expression evaluation is added to the text node with a space added between the adjacent atomic values, as shown in the result. The constructed element has one child. This is a text node that contains the value shown in the result.  
  
```xml
<result>This is step 1 This is step 2 This is step 3</result>  
```  
  
 Instead of one expression, if you specify three separate expressions generating three text nodes, the adjacent text nodes are merged into a single text node, by concatenation, in the resulting XML.  
  
```sql
declare @x xml;  
set @x='  
<root>  
  <step>This is step 1</step>  
  <step>This is step 2</step>  
  <step>This is step 3</step>  
</root>';  
select @x.query('  
<result>  
 { string(/root[1]/step[1]) }  
 { string(/root[1]/step[2]) }  
 { string(/root[1]/step[3]) }  
</result>');  
```  
  
 The constructed element node has one child. This is a text node that contains the value shown in the result.  
  
```xml
<result>This is step 1This is step 2This is step 3</result>  
```  
  
### Constructing Attributes  
 When you are constructing elements by using the direct element constructor, you can also specify attributes of the element by using XML-like syntax, as shown in this example:  
  
```sql
declare @x xml;  
set @x='';  
select @x.query('<ProductModel ProductModelID="111">;  
This is product model catalog description.  
<Summary>Some description</Summary>  
</ProductModel>')  
```  
  
 This is the resulting XML:  
  
```xml
<ProductModel ProductModelID="111">  
  This is product model catalog description.  
  <Summary>Some description</Summary>  
</ProductModel>  
```  
  
 The constructed element <`ProductModel`> has a ProductModelID attribute and these child nodes:  
  
-   A text node, `This is product model catalog description.`  
  
-   An element node, <`Summary`>. This node has one text node child, `Some description`.  
  
 When you are constructing an attribute, you can specify its value with an expression in curly braces. In this case, the result of the expression is returned as the attribute value.  
  
 In the following example, the **data()** function is not strictly required. Because you are assigning the expression value to an attribute, **data()** is implicitly applied to retrieve the typed value of the specified expression.  
  
```sql
DECLARE @x xml;  
SET @x='<root>5</root>';  
DECLARE @y xml;  
SET @y = (SELECT @x.query('<NewRoot attr="{ data(/root) }" ></NewRoot>'));  
SELECT @y;  
```  
  
 This is the result:  
  
```xml
<NewRoot attr="5" />  
```  
  
 Following is another example in which expressions are specified for LocationID and SetupHrs attribute construction. These expressions are evaluated against the XML in the Instruction column. The typed valued of the expression is assigned to the attributes.  
  
```sql
SELECT Instructions.query('  
    declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
        <FirstLocation   
         LocationID="{ (/AWMI:root/AWMI:Location[1]/@LocationID)[1] }"  
         SetupHrs = "{ (/AWMI:root/AWMI:Location[1]/@SetupHours)[1] }" >  
           { /AWMI:root/AWMI:Location[1]/AWMI:step }  
        </FirstLocation>   
') as Result   
FROM  Production.ProductModel  
where ProductModelID=7;  
```  
  
 This is the partial result:  
  
```xml
<FirstLocation LocationID="10" SetupHours="0.5" >  
  <AWMI:step ...   
  </AWMI:step>  
  ...  
</FirstLocation>  
```  
  
#### Implementation Limitations  
 These are the limitations:  
  
-   Multiple or mixed (string and XQuery expression) attribute expressions are not supported. For example, as shown in the following query, you construct XML where `Item` is a constant and the value `5` is obtained by evaluating a query expression:  
  
    ```xml
    <a attr="Item 5" />  
    ```  
  
     The following query returns an error, because you are mixing constant string with an expression ({/x}) and this is not supported:  
  
    ```sql
    DECLARE @x xml  
    SET @x ='<x>5</x>'  
    SELECT @x.query( '<a attr="Item {/x}"/>' )   
    ```  
  
     In this case, you have the following options:  
  
    -   Form the attribute value by the concatenation of two atomic values. These atomic values are serialized into the attribute value with a space between the atomic values:  
  
        ```sql
        SELECT @x.query( '<a attr="{''Item'', data(/x)}"/>' )   
        ```  
  
         This is the result:  
  
        ```xml
        <a attr="Item 5" />  
        ```  
  
    -   Use the [concat function](../xquery/functions-on-string-values-concat.md) to concatenate the two string arguments into the resulting attribute value:  
  
        ```sql
        SELECT @x.query( '<a attr="{concat(''Item'', /x[1])}"/>' )   
        ```  
  
         In this case, there is no space added between the two string values. If you want a space between the two values, you must explicitly provide it.  
  
         This is the result:  
  
        ```xml
        <a attr="Item5" />  
        ```  
  
-   Multiple expressions as an attribute value are not supported. For example, the following query returns an error:  
  
    ```sql
    DECLARE @x xml  
    SET @x ='<x>5</x>'  
    SELECT @x.query( '<a attr="{/x}{/x}"/>' )  
    ```  
  
-   Heterogeneous sequences are not supported. Any attempt to assign a heterogeneous sequence as an attribute value will return an error, as shown in the following example. In this example, a heterogeneous sequence, a string "Item" and an element <`x`>, is specified as the attribute value:  
  
    ```sql
    DECLARE @x xml  
    SET @x ='<x>5</x>'  
    select @x.query( '<a attr="{''Item'', /x }" />')  
    ```  
  
     If you apply the **data()** function, the query works because it retrieves the atomic value of the expression, `/x`, which is concatenated with the string. Following is a sequence of atomic values:  
  
    ```sql
    SELECT @x.query( '<a attr="{''Item'', data(/x)}"/>' )   
    ```  
  
     This is the result:  
  
    ```xml
    <a attr="Item 5" />  
    ```  
  
-   Attribute node order is enforced during serialization rather than during static type checking. For example, the following query fails because it attempts to add an attribute after a non-attribute node.  
  
    ```sql
    select convert(xml, '').query('  
    element x { attribute att { "pass" }, element y { "Element text" }, attribute att2 { "fail" } }  
    ')  
    go  
    ```  
  
     The above query returns the following error:  
  
    ```  
    XML well-formedness check: Attribute cannot appear outside of element declaration. Rewrite your XQuery so it returns well-formed XML.  
    ```  
  
### Adding Namespaces  
 When constructing XML by using the direct constructors, the constructed element and attribute names can be qualified by using a namespace prefix. You can bind the prefix to the namespace in the following ways:  
  
-   By using a namespace declaration attribute.  
  
-   By using the WITH XMLNAMESPACES clause.  
  
-   In the XQuery prolog.  
  
#### Using a Namespace Declaration Attribute to Add Namespaces  
 The following example uses a namespace declaration attribute in the construction of element <`a`> to declare a default namespace. The construction of the child element <`b`> undoes the declaration of the default namespace declared in the parent element.  
  
```sql
declare @x xml  
set @x ='<x>5</x>'  
select @x.query( '  
  <a xmlns="a">  
    <b xmlns=""/>  
  </a>' )   
```  
  
 This is the result:  
  
```xml
<a xmlns="a">  
  <b xmlns="" />  
</a>  
```  
  
 You can assign a prefix to the namespace. The prefix is specified in the construction of element <`a`>.  
  
```sql
declare @x xml  
set @x ='<x>5</x>'  
select @x.query( '  
  <x:a xmlns:x="a">  
    <b/>  
  </x:a>' )  
```  
  
 This is the result:  
  
```xml
<x:a xmlns:x="a">  
  <b />  
</x:a>  
```  
  
 You can un-declare a default namespace in the XML construction, but you cannot un-declare a namespace prefix. The following query returns an error, because you cannot un-declare a prefix as specified in the construction of element <`b`>.  
  
```sql
declare @x xml  
set @x ='<x>5</x>'  
select @x.query( '  
  <x:a xmlns:x="a">  
    <b xmlns:x=""/>  
  </x:a>' )  
```  
  
 The newly constructed namespace is available to use inside the query. For example, the following query declares a namespace in constructing the element, <`FirstLocation`>, and specifies the prefix in the expressions for the LocationID and SetupHrs attribute values.  
  
```sql
SELECT Instructions.query('  
        <FirstLocation xmlns:AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions"  
         LocationID="{ (/AWMI:root/AWMI:Location[1]/@LocationID)[1] }"  
         SetupHrs = "{ (/AWMI:root/AWMI:Location[1]/@SetupHours)[1] }" >  
           { /AWMI:root/AWMI:Location[1]/AWMI:step }  
        </FirstLocation>   
') as Result   
FROM  Production.ProductModel  
where ProductModelID=7  
```  
  
 Note that creating a new namespace prefix in this way will override any pre-existing namespace declaration for this prefix. For example, the namespace declaration, `AWMI="https://someURI"`, in the query prolog is overridden by the namespace declaration in the <`FirstLocation`> element.  
  
```sql
SELECT Instructions.query('  
declare namespace AWMI="https://someURI";  
        <FirstLocation xmlns:AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions"  
         LocationID="{ (/AWMI:root/AWMI:Location[1]/@LocationID)[1] }"  
         SetupHrs = "{ (/AWMI:root/AWMI:Location[1]/@SetupHours)[1] }" >  
           { /AWMI:root/AWMI:Location[1]/AWMI:step }  
        </FirstLocation>   
') as Result   
FROM  Production.ProductModel  
where ProductModelID=7  
```  
  
#### Using a Prolog to Add Namespaces  
 This example illustrates how namespaces can be added to the constructed XML. A default namespace is declared in the query prolog.  
  
```sql
declare @x xml  
set @x ='<x>5</x>'  
select @x.query( '  
           declare default element namespace "a";  
            <a><b xmlns=""/></a>' )  
```  
  
 Note that in the construction of element <`b`>, the namespace declaration attribute is specified with an empty string as its value. This un-declares the default namespace that is declared in the parent.  
  

This is the result:  

```xml
<a xmlns="a">  
  <b xmlns="" />  
</a>  
```  
  
### XML Construction and White Space Handling  
 The element content in XML construction can include white-space characters. These characters are handled in the following ways:  
  
-   The white-space characters in namespace URIs are treated as the XSD type **anyURI**. Specifically, this is how they are handled:  
  
    -   Any white-space characters at the start and end are trimmed.  
  
    -   Internal white-space character values are collapsed into a single space  
  
-   The linefeed characters inside the attribute content are replaced by spaces. All other white-space characters remain as they are.  
  
-   The white space inside elements is preserved.  
  
 The following example illustrates white-space handling in XML construction:  
  
```sql
-- line feed is repaced by space.  
declare @x xml  
set @x=''  
select @x.query('  
  
declare namespace myNS="   https://       
 abc/  
xyz  
  
";  
<test attr="    my   
test   attr   
value    " >  
  
<a>  
  
This     is  a  
  
test  
  
</a>  
</test>  
') as XML_Result  
  
```  
  
 This is the result:  
  
```xml
-- result  
<test attr="<test attr="    my test   attr  value    "><a>  
  
This     is  a  
  
test  
  
</a></test>  
"><a>  
  
This     is  a  
  
test  
  
</a></test>  
```  
  
### Other Direct XML Constructors  
 The constructors for processing instructions and XML comments use the same syntax as that of the corresponding XML construct. Computed constructors for text nodes are also supported, but are primarily used in XML DML to construct text nodes.  
  
 **Note** For an example of using an explicit text node constructor, see the specific example in [insert &#40;XML DML&#41;](../t-sql/xml/insert-xml-dml.md).  
  
 In the following query, the constructed XML includes an element, two attributes, a comment, and a processing instruction. Note that a comma is used before the <`FirstLocation`>, because a sequence is being constructed.  
  
```sql
SELECT Instructions.query('  
  declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
   <?myProcessingInstr abc="value" ?>,   
   <FirstLocation   
        WorkCtrID = "{ (/AWMI:root/AWMI:Location[1]/@LocationID)[1] }"  
        SetupHrs = "{ (/AWMI:root/AWMI:Location[1]/@SetupHours)[1] }" >  
       <!-- some comment -->  
       <?myPI some processing instructions ?>  
       { (/AWMI:root/AWMI:Location[1]/AWMI:step) }  
    </FirstLocation>   
') as Result   
FROM Production.ProductModel  
where ProductModelID=7;  
  
```  
  
 This is the partial result:  
  
```xml
<?myProcessingInstr abc="value" ?>  
<FirstLocation WorkCtrID="10" SetupHrs="0.5">  
  <!-- some comment -->  
  <?myPI some processing instructions ?>  
  <AWMI:step xmlns:AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">I  
  nsert <AWMI:material>aluminum sheet MS-2341</AWMI:material> into the <AWMI:tool>T-85A framing tool</AWMI:tool>.   
  </AWMI:step>  
    ...  
</FirstLocation>  
  
```  
  
## Using Computed Constructors  
 . In this case, you specify the keywords that identify the type of node you want to construct. Only the following keywords are supported:  
  
-   element  
  
-   attribute  
  
-   text  
  
 For element and attribute nodes, these keywords are followed by node name and also by the expression, enclosed in braces, that generates the content for that node. In the following example, you are constructing this XML:  
  
```xml
<root>  
  <ProductModel PID="5">Some text <summary>Some Summary</summary></ProductModel>  
</root>  
```  
  
 This is the query that uses computed constructors do generate the XML:  
  
```sql
declare @x xml  
set @x=''  
select @x.query('element root   
               {   
                  element ProductModel  
     {  
attribute PID { 5 },  
text{"Some text "},  
    element summary { "Some Summary" }  
 }  
               } ')  
  
```  
  
 The expression that generates the node content can specify a query expression.  
  
```sql
declare @x xml  
set @x='<a attr="5"><b>some summary</b></a>'  
select @x.query('element root   
               {   
                  element ProductModel  
     {  
attribute PID { /a/@attr },  
text{"Some text "},  
    element summary { /a/b }  
 }  
               } ')  
```  
  
 Note that the computed element and attribute constructors, as defined in the XQuery specification, allow you to compute the node names. When you are using direct constructors in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the node names, such as element and attribute, must be specified as constant literals. Therefore, there is no difference in the direct constructors and computed constructors for elements and attributes.  
  
 In the following example, the content for the constructed nodes is obtained from the XML manufacturing instructions stored in the Instructions column of the **xml** data type in the ProductModel table.  
  
```sql
SELECT Instructions.query('  
  declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
   element FirstLocation   
     {  
        attribute LocationID { (/AWMI:root/AWMI:Location[1]/@LocationID)[1] },  
        element   AllTheSteps { /AWMI:root/AWMI:Location[1]/AWMI:step }  
     }  
') as Result   
FROM  Production.ProductModel  
where ProductModelID=7  
```  
  
 This is the partial result:  
  
```xml
<FirstLocation LocationID="10">  
  <AllTheSteps>  
    <AWMI:step> ... </AWMI:step>  
    <AWMI:step> ... </AWMI:step>  
    ...  
  </AllTheSteps>  
</FirstLocation>    
```  
  
## Additional Implementation Limitations  
 Computed attribute constructors cannot be used to declare a new namespace. Also, the following computed constructors are not supported in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]:  
  
-   Computed document node constructors  
  
-   Computed processing instruction constructors  
  
-   Computed comment constructors  
  
## See Also  
 [XQuery Expressions](../xquery/xquery-expressions.md)  
  
  
