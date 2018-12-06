---
title: "FLWOR Statement and Iteration (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "return clause"
  - "FLWOR statement"
  - "effective Boolean value [XQuery]"
  - "multiple variable binding"
  - "order by clause [XQuery]"
  - "for clause [XQuery]"
  - "where clause [XQuery]"
  - "iterations [XQuery]"
  - "XQuery, FLWOR statement"
  - "EBV"
ms.assetid: d7cd0ec9-334a-4564-bda9-83487b6865cb
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# FLWOR Statement and Iteration (XQuery)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  XQuery defines the FLWOR iteration syntax. FLWOR is the acronym for `for`, `let`, `where`, `order by`, and `return`.  
  
 A FLWOR statement is made up of the following parts:  
  
-   One or more FOR clauses that bind one or more iterator variables to input sequences.  
  
     Input sequences can be other XQuery expressions such as XPath expressions. They are either sequences of nodes or sequences of atomic values. Atomic value sequences can be constructed using literals or constructor functions. Constructed XML nodes are not allowed as input sequences in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
-   An optional `let` clause. This clause assigns a value to the given variable for a specific iteration. The assigned expression can be an XQuery expression such as an XPath expression, and can return either a sequence of nodes or a sequence of atomic values. Atomic value sequences can be constructed by using literals or constructor functions. Constructed XML nodes are not allowed as input sequences in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
-   An iterator variable. This variable can have an optional type assertion by using the `as` keyword.  
  
-   An optional `where` clause. This clause applies a filter predicate on the iteration.  
  
-   An optional `order by` clause.  
  
-   A `return` expression. The expression in the `return` clause constructs the result of the FLWOR statement.  
  
 For example, the following query iterates over the <`Step`> elements at the first manufacturing location and returns the string value of the <`Step`> nodes:  
  
```sql
declare @x xml  
set @x='<ManuInstructions ProductModelID="1" ProductModelName="SomeBike" >  
<Location LocationID="L1" >  
  <Step>Manu step 1 at Loc 1</Step>  
  <Step>Manu step 2 at Loc 1</Step>  
  <Step>Manu step 3 at Loc 1</Step>  
</Location>  
<Location LocationID="L2" >  
  <Step>Manu step 1 at Loc 2</Step>  
  <Step>Manu step 2 at Loc 2</Step>  
  <Step>Manu step 3 at Loc 2</Step>  
</Location>  
</ManuInstructions>'  
SELECT @x.query('  
   for $step in /ManuInstructions/Location[1]/Step  
   return string($step)  
')  
```  
  
 This is the result:  
  
```  
Manu step 1 at Loc 1 Manu step 2 at Loc 1 Manu step 3 at Loc 1  
```  
  
 The following query is similar to the previous one, except that it is specified against the Instructions column, a typed xml column, of the ProductModel table. The query iterates over all the manufacturing steps, <`step`> elements, at the first work center location for a specific product.  
  
```sql
SELECT Instructions.query('  
   declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
for $Step in //AWMI:root/AWMI:Location[1]/AWMI:step  
      return  
           string($Step)   
') as Result  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
 Note the following from the previous query:  
  
-   The `$Step` is the iterator variable.  
  
-   The [path expression](../xquery/path-expressions-xquery.md), `//AWMI:root/AWMI:Location[1]/AWMI:step`, generates the input sequence. This sequence is the sequence of the <`step`> element node children of the first <`Location`> element node.  
  
-   The optional predicate clause, `where`, is not used.  
  
-   The `return` expression returns a string value from the <`step`> element.  
  
 The [string function (XQuery)](../xquery/data-accessor-functions-string-xquery.md) is used to retrieve the string value of the <`step`> node.  
  
 This is the partial result:  
  
```  
Insert aluminum sheet MS-2341 into the T-85A framing tool.   
Attach Trim Jig TJ-26 to the upper and lower right corners of   
the aluminum sheet. ....         
```  
  
 These are examples of additional input sequences that are allowed:  
  
```sql
declare @x xml  
set @x=''  
SELECT @x.query('  
for $a in (1, 2, 3)  
  return $a')  
-- result = 1 2 3   
  
declare @x xml  
set @x=''  
SELECT @x.query('  
for $a in   
   for $b in (1, 2, 3)  
      return $b  
return $a')  
-- result = 1 2 3  
  
declare @x xml  
set @x='<ROOT><a>111</a></ROOT>'  
SELECT @x.query('  
  for $a in (xs:string( "test"), xs:double( "12" ), data(/ROOT/a ))  
  return $a')  
-- result test 12 111  
```  
  
 In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], heterogeneous sequences are not allowed. Specifically, sequences that contain a mixture of atomic values and nodes are not allowed.  
  
 Iteration is frequently used together with the [XML Construction](../xquery/xml-construction-xquery.md) syntax in transforming XML formats, as shown in the next query.  
  
 In the AdventureWorks sample database, the manufacturing instructions stored in the **Instructions** column of the **Production.ProductModel** table have the following form:  
  
```xml
<Location LocationID="10" LaborHours="1.2"   
            SetupHours=".2" MachineHours=".1">  
  <step>describes 1st manu step</step>  
   <step>describes 2nd manu step</step>  
   ...  
</Location>  
...  
```  
  
 The following query constructs new XML that has the <`Location`> elements with the work center location attributes returned as child elements:  
  
```xml
<Location>  
   <LocationID>10</LocationID>  
   <LaborHours>1.2</LaborHours>  
   <SetupHours>.2</SetupHours>  
   <MachineHours>.1</MachineHours>  
</Location>  
...  
```  
  
 This is the query:  
  
```sql
SELECT Instructions.query('  
     declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
        for $WC in /AWMI:root/AWMI:Location  
        return  
          <Location>  
            <LocationID> { data($WC/@LocationID) } </LocationID>  
            <LaborHours>   { data($WC/@LaborHours) }   </LaborHours>  
            <SetupHours>   { data($WC/@SetupHours) }   </SetupHours>  
            <MachineHours> { data($WC/@MachineHours) } </MachineHours>  
          </Location>  
') as Result  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
 Note the following from the previous query:  
  
-   The FLWOR statement retrieves a sequence of <`Location`> elements for a specific product.  
  
-   The [data function (XQuery)](../xquery/data-accessor-functions-data-xquery.md) is used to extract the value of each attribute so they will be added to the resulting XML as text nodes instead of as attributes.  
  
-   The expression in the RETURN clause constructs the XML that you want.  
  
 This is a partial result:  
  
```xml
<Location>  
  <LocationID>10</LocationID>  
  <LaborHours>2.5</LaborHours>  
  <SetupHours>0.5</SetupHours>  
  <MachineHours>3</MachineHours>  
</Location>  
<Location>  
   ...  
<Location>  
...  
```  
  
## Using the let Clause  
 You can use the `let` clause to name repeating expressions that you can refer to by referring to the variable. The expression assigned to a `let` variable is inserted into the query every time the variable is referenced in the query. This means that the statement is executed as many times as the expression gets referenced.  
  
 In the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database, the manufacturing instructions contain information about the tools required and the location where the tools are used. The following query uses the `let` clause to list the tools required to build a production model, as well as the locations where each tool is needed.  
  
```sql
SELECT Instructions.query('  
     declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
        for $T in //AWMI:tool  
            let $L := //AWMI:Location[.//AWMI:tool[.=data($T)]]  
        return  
          <tool desc="{data($T)}" Locations="{data($L/@LocationID)}"/>  
') as Result  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
## Using the where Clause  
 You can use the `where` clause to filter results of an iteration. This is illustrated in this next example.  
  
 In the manufacturing of a bicycle, the manufacturing process goes through a series of work center locations. Each work center location defines a sequence of manufacturing steps. The following query retrieves only those work center locations that manufacture a bicycle model and have less than three manufacturing steps. That is, they have less than three <`step`> elements.  
  
```sql
SELECT Instructions.query('  
     declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
for $WC in /AWMI:root/AWMI:Location  
      where count($WC/AWMI:step) < 3  
      return  
          <Location >  
           { $WC/@LocationID }   
          </Location>  
') as Result  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
 Note the following in the previous query:  
  
-   The `where` keyword uses the **count()** function to count the number of <`step`> child elements in each work center location.  
  
-   The `return` expression constructs the XML that you want from the results of the iteration.  
  
 This is the result:  
  
```  
<Location LocationID="30"/>   
```  
  
 The result of the expression in the `where` clause is converted to a Boolean value by using the following rules, in the order specified. These are the same as the rules for predicates in path expressions, except that integers are not allowed:  
  
1.  If the `where` expression returns an empty sequence, its effective Boolean value is False.  
  
2.  If the `where` expression returns one simple Boolean type value, that value is the effective Boolean value.  
  
3.  If the `where` expression returns a sequence that contains at least one node, the effective Boolean value is True.  
  
4.  Otherwise, it raises a static error.  
  
## Multiple Variable Binding in FLWOR  
 You can have a single FLWOR expression that binds multiple variables to input sequences. In the following example, the query is specified against an untyped xml variable. The FLOWR expression returns the first <`Step`> element child in each <`Location`> element.  
  
```sql
declare @x xml  
set @x='<ManuInstructions ProductModelID="1" ProductModelName="SomeBike" >  
<Location LocationID="L1" >  
  <Step>Manu step 1 at Loc 1</Step>  
  <Step>Manu step 2 at Loc 1</Step>  
  <Step>Manu step 3 at Loc 1</Step>  
</Location>  
<Location LocationID="L2" >  
  <Step>Manu step 1 at Loc 2</Step>  
  <Step>Manu step 2 at Loc 2</Step>  
  <Step>Manu step 3 at Loc 2</Step>  
</Location>  
</ManuInstructions>'  
SELECT @x.query('  
   for $Loc in /ManuInstructions/Location,  
       $FirstStep in $Loc/Step[1]  
   return   
       string($FirstStep)  
')  
```  
  
 Note the following from the previous query:  
  
-   The `for` expression defines `$Loc` and $`FirstStep` variables.  
  
-   The `two` expressions, `/ManuInstructions/Location` and `$FirstStep in $Loc/Step[1]`, are correlated in that the values of `$FirstStep` depend on the values of `$Loc`.  
  
-   The expression associated with `$Loc` generates a sequence of <`Location`> elements. For each <`Location`> element, `$FirstStep` generates a sequence of one <`Step`> element, a singleton.  
  
-   `$Loc` is specified in the expression associated with the `$FirstStep` variable.  
  
 This is the result:  
  
```  
Manu step 1 at Loc 1   
Manu step 1 at Loc 2  
```  
  
 The following query is similar, except that it is specified against the Instructions column, typed **xml** column, of the **ProductModel** table. [XML Construction (XQuery)](../xquery/xml-construction-xquery.md) is used to generate the XML that you want.  
  
```sql
SELECT Instructions.query('  
     declare default element namespace "https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
for $WC in /root/Location,  
            $S  in $WC/step  
      return  
          <Step LocationID= "{$WC/@LocationID }" >  
            { $S/node() }  
          </Step>  
') as Result  
FROM  Production.ProductModel  
WHERE ProductModelID=7  
```  
  
 Note the following in the previous query:  
  
-   The `for` clause defines two variables, `$WC` and `$S`. The expression associated with `$WC` generates a sequence of work center locations in the manufacturing of a bicycle product model. The path expression assigned to the `$S` variable generates a sequence of steps for each work center location sequence in the `$WC`.  
  
-   The return statement constructs XML that has a <`Step`> element that contains the manufacturing step and the **LocationID** as its attribute.  
  
-   The **declare default element namespace** is used in the XQuery prolog so that all the namespace declarations in the resulting XML appear at the top-level element. This makes the result more readable. For more information about default namespaces, see [Handling Namespaces in XQuery](../xquery/handling-namespaces-in-xquery.md).  
  
 This is the partial result:  
  
```xml
<Step xmlns=  
    "https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions"     
  LocationID="10">  
     Insert <material>aluminum sheet MS-2341</material> into the <tool>T-   
     85A framing tool</tool>.   
</Step>  
...  
<Step xmlns=  
      "https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions"     
    LocationID="20">  
        Assemble all frame components following blueprint   
        <blueprint>1299</blueprint>.  
</Step>  
...  
```  
  
## Using the order by Clause  
 Sorting in XQuery is performed by using the `order by` clause in the FLWOR expression. The sorting expressions passed to the `order by` clause must return values whose types are valid for the **gt** operator. Each sorting expression must result in a singleton a sequence with one item. By default, sorting is performed in ascending order. You can optionally specify ascending or descending order for each sorting expression.  
  
> [!NOTE]  
>  Sorting comparisons on string values performed by the XQuery implementation in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] are always performed by using the binary Unicode codepoint collation.  
  
 The following query retrieves all the telephone numbers for a specific customer from the AdditionalContactInfo column. The results are sorted by telephone number.  
  
```sql
USE AdventureWorks2012;  
GO  
SELECT AdditionalContactInfo.query('  
   declare namespace act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes";  
   declare namespace aci="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo";  
   for $a in /aci:AdditionalContactInfo//act:telephoneNumber   
   order by $a/act:number[1] descending  
   return $a  
') As Result  
FROM Person.Person  
WHERE BusinessEntityID=291;  
```  
  
 Note that the [Atomization (XQuery)](../xquery/atomization-xquery.md) process retrieves the atomic value of the <`number`> elements before passing it to `order by`. You can write the expression by using the **data()** function, but that is not required.  
  
```  
order by data($a/act:number[1]) descending  
```  
  
 This is the result:  
  
```xml
<act:telephoneNumber xmlns:act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes">  
  <act:number>333-333-3334</act:number>  
</act:telephoneNumber>  
<act:telephoneNumber xmlns:act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes">  
  <act:number>333-333-3333</act:number>  
</act:telephoneNumber>  
```  
  
 Instead of declaring the namespaces in the query prolog, you can declare them by using WITH XMLNAMESPACES.  
  
```sql
WITH XMLNAMESPACES (  
   'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes' AS act,  
   'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo'  AS aci)  
  
SELECT AdditionalContactInfo.query('  
   for $a in /aci:AdditionalContactInfo//act:telephoneNumber   
   order by $a/act:number[1] descending  
   return $a  
') As Result  
FROM Person.Person  
WHERE BusinessEntityID=291;  
```  
  
 You can also sort by attribute value. For example, the following query retrieves the newly created <`Location`> elements that have the LocationID and LaborHours attributes sorted by the LaborHours attribute in descending order. As a result, the work center locations that have the maximum labor hours are returned first.  
  
```sql
SELECT Instructions.query('  
     declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
for $WC in /AWMI:root/AWMI:Location   
order by $WC/@LaborHours descending  
        return  
          <Location>  
             { $WC/@LocationID }   
             { $WC/@LaborHours }   
          </Location>  
') as Result  
FROM Production.ProductModel  
WHERE ProductModelID=7;  
```  
  
 This is the result:  
  
```  
<Location LocationID="60" LaborHours="4"/>  
<Location LocationID="50" LaborHours="3"/>  
<Location LocationID="10" LaborHours="2.5"/>  
<Location LocationID="20" LaborHours="1.75"/>  
<Location LocationID="30" LaborHours="1"/>  
<Location LocationID="45" LaborHours=".5"/>  
```  
  
 In the following query, the results are sorted by element name. The query retrieves the specifications of a specific product from the product catalog. The specifications are the children of the <`Specifications`> element.  
  
```sql
SELECT CatalogDescription.query('  
     declare namespace  
 pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
      for $a in /pd:ProductDescription/pd:Specifications/*   
     order by local-name($a)  
      return $a  
    ') as Result  
FROM Production.ProductModel  
where ProductModelID=19;  
```  
  
 Note the following from the previous query:  
  
-   The `/p1:ProductDescription/p1:Specifications/*` expression returns element children of <`Specifications`>.  
  
-   The `order by (local-name($a))` expression sorts the sequence by the local part of the element name.  
  
 This is the result:  
  
```xml
<Color>Available in most colors</Color>  
<Material>Almuminum Alloy</Material>  
<ProductLine>Mountain bike</ProductLine>  
<RiderExperience>Advanced to Professional riders</RiderExperience>  
<Style>Unisex</Style>    
```  
  
 Nodes in which the ordering expression returns empty are sorted to the start of the sequence, as shown in the following example:  
  
```sql
declare @x xml  
set @x='<root>  
  <Person Name="A" />  
  <Person />  
  <Person Name="B" />  
</root>  
'  
select @x.query('  
  for $person in //Person  
  order by $person/@Name  
  return   $person  
')  
```  
  
 This is the result:  
  
```xml
<Person />  
<Person Name="A" />  
<Person Name="B" />  
```  
  
 You can specify multiple sorting criteria, as shown in the following example. The query in this example sorts <`Employee`> elements first by Title and then by Administrator attribute values.  
  
```sql
declare @x xml  
set @x='<root>  
  <Employee ID="10" Title="Teacher"        Gender="M" />  
  <Employee ID="15" Title="Teacher"  Gender="F" />  
  <Employee ID="5" Title="Teacher"         Gender="M" />  
  <Employee ID="11" Title="Teacher"        Gender="F" />  
  <Employee ID="8" Title="Administrator"   Gender="M" />  
  <Employee ID="4" Title="Administrator"   Gender="F" />  
  <Employee ID="3" Title="Teacher"         Gender="F" />  
  <Employee ID="125" Title="Administrator" Gender="F" /></root>'  
SELECT @x.query('for $e in /root/Employee  
order by $e/@Title ascending, $e/@Gender descending  
  
  return  
     $e  
')  
```  
  
 This is the result:  
  
```xml
<Employee ID="8" Title="Administrator" Gender="M" />  
<Employee ID="4" Title="Administrator" Gender="F" />  
<Employee ID="125" Title="Administrator" Gender="F" />  
<Employee ID="10" Title="Teacher" Gender="M" />  
<Employee ID="5" Title="Teacher" Gender="M" />  
<Employee ID="11" Title="Teacher" Gender="F" />  
<Employee ID="15" Title="Teacher" Gender="F" />  
<Employee ID="3" Title="Teacher" Gender="F" />  
```  
  
### Implementation Limitations  
 These are the limitations:  
  
-   The sorting expressions must be homogeneously typed. This is statically checked.  
  
-   Sorting empty sequences cannot be controlled.  
  
-   The empty least, empty greatest, and collation keywords on `order by` are not supported  
  
## See Also  
 [XQuery Expressions](../xquery/xquery-expressions.md)  
  
  
