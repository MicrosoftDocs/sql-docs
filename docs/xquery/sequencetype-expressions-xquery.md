---
title: "SequenceType Expressions (XQuery) | Microsoft Docs"
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
  - "SequenceType expressions"
  - "instance of operator [XQuery]"
  - "expressions [XQuery], SequenceType"
  - "cast as operator"
ms.assetid: ad3573da-d820-4d1c-81c4-a83c4640ce22
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# SequenceType Expressions (XQuery)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  In XQuery, a value is always a sequence. The type of the value is referred to as a sequence type. The sequence type can be used in an **instance of** XQuery expression. The SequenceType syntax described in the XQuery specification is used when you need to refer to a type in an XQuery expression.  
  
 The atomic type name can also be used in the **cast as** XQuery expression. In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the **instance of** and **cast as** XQuery expressions on SequenceTypes are partially supported.  
  
## instance of Operator  
 The **instance of** operator can be used to determine the dynamic, or run-time, type of the value of the specified expression. For example:  
  
```  
  
Expression instance of SequenceType[Occurrence indicator]  
```  
  
 Note that the `instance of` operator, the `Occurrence indicator`, specifies the cardinality, number of items in the resulting sequence. If this is not specified, the cardinality is assumed to be 1. In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], only the question mark (**?)** occurrence indicator is supported. The **?** occurrence indicator indicates that `Expression` can return zero or one item. If the **?** occurrence indicator is specified, `instance of` returns True when the `Expression` type matches the specified `SequenceType`, regardless of whether `Expression` returns a singleton or an empty sequence.  
  
 If the **?** occurrence indicator is not specified, `sequence of` returns True only when the `Expression` type matches the `Type` specified and `Expression` returns a singleton.  
  
 **Note** The plus symbol (**+**) and the asterisk (**&#42;**) occurrence indicators are not supported in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 The following examples illustrate the use of the**instance of** XQuery operator.  
  
### Example A  
 The following example creates an **xml** type variable and specifies a query against it. The query expression specifies an `instance of` operator to determine whether the dynamic type of the value returned by the first operand matches the type specified in the second operand.  
  
 The following query returns True, because the 125 value is an instance of the specified type, **xs:integer**:  
  
```  
declare @x xml  
set @x=''  
select @x.query('125 instance of xs:integer')  
go  
```  
  
 The following query returns True, because the value returned by the expression, /a[1], in the first operand is an element:  
  
```  
declare @x xml  
set @x='<a>1</a>'  
select @x.query('/a[1] instance of element()')  
go  
```  
  
 Similarly, `instance of` returns True in the following query, because the value type of the expression in the first expression is an attribute:  
  
```  
declare @x xml  
set @x='<a attr1="x">1</a>'  
select @x.query('/a[1]/@attr1 instance of attribute()')  
go  
```  
  
 In the following example, the expression, `data(/a[1]`, returns an atomic value that is typed as xdt:untypedAtomic. Therefore, the `instance of` returns True.  
  
```  
declare @x xml  
set @x='<a>1</a>'  
select @x.query('data(/a[1]) instance of xdt:untypedAtomic')  
go  
```  
  
 In the following query, the expression, `data(/a[1]/@attrA`, returns an untyped atomic value. Therefore, the `instance of` returns True.  
  
```  
declare @x xml  
set @x='<a attrA="X">1</a>'  
select @x.query('data(/a[1]/@attrA) instance of xdt:untypedAtomic')  
go  
```  
  
### Example B  
 In this example, you are querying a typed XML column in the AdventureWorks sample database. The XML schema collection associated with the column that is being queried provides the typing information.  
  
 In the expression, **data()** returns the typed value of the ProductModelID attribute whose type is xs:string according to the schema associated with the column. Therefore, the `instance of` returns True.  
  
```  
SELECT CatalogDescription.query('  
   declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
   data(/PD:ProductDescription[1]/@ProductModelID) instance of xs:string  
') as Result  
FROM Production.ProductModel  
WHERE ProductModelID = 19  
```  
  
 For more information, see [Compare Typed XML to Untyped XML](../relational-databases/xml/compare-typed-xml-to-untyped-xml.md).  
  
 The following queries usetheBoolean `instance of` expression to determine whether the LocationID attribute is of xs:integer type:  
  
```  
SELECT Instructions.query('  
   declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
   /AWMI:root[1]/AWMI:Location[1]/@LocationID instance of attribute(LocationID,xs:integer)  
') as Result  
FROM Production.ProductModel  
WHERE ProductModelID=7  
```  
  
 The following query is specified against the CatalogDescription typed XML column. The XML schema collection associated with this column provides typing information.  
  
 The query uses the `element(ElementName, ElementType?)` test in the `instance of` expression to verify that the `/PD:ProductDescription[1]` returns an element node of a specific name and type.  
  
```  
SELECT CatalogDescription.query('  
     declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
     /PD:ProductDescription[1] instance of element(PD:ProductDescription, PD:ProductDescription?)  
    ') as Result  
FROM  Production.ProductModel  
where ProductModelID=19  
```  
  
 The query returns True.  
  
### Example C  
 When using union types, the `instance of` expression in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has a limitation: Specifically, when the type of an element or attribute is a union type, `instance of` might not determine the exact type. Consequently, a query will return False, unless the atomic types used in the SequenceType is the highest parent of the actual type of the expression in the simpleType hierarchy. That is, the atomic types specified in the SequenceType must be a direct child of anySimpleType. For information about the type hierarchy, see [Type Casting Rules in XQuery](../xquery/type-casting-rules-in-xquery.md).  
  
 The next query example performs the following:  
  
-   Create an XML schema collection with a union type, such as an integer or string type, defined in it.  
  
-   Declare a typed **xml** variable by using the XML schema collection.  
  
-   Assign a sample XML instance to the variable.  
  
-   Query the variable to illustrate the `instance of` behavior when dealing with a union type.  
  
 This is the query:  
  
```  
CREATE XML SCHEMA COLLECTION MyTestSchema AS '  
<schema xmlns="http://www.w3.org/2001/XMLSchema" targetNamespace="http://ns" xmlns:ns="http://ns">  
<simpleType name="MyUnionType">  
<union memberTypes="integer string"/>  
</simpleType>  
<element name="TestElement" type="ns:MyUnionType"/>  
</schema>'  
Go  
```  
  
 The following query returns False, because the SequenceType specified in the `instance of` expression is not the highest parent of the actual type of the specified expression. That is, the value of the <`TestElement`> is an integer type. The highest parent is xs:decimal. However, it is not specified as the second operand to the `instance of` operator.  
  
```  
SET QUOTED_IDENTIFIER ON  
DECLARE @var XML(MyTestSchema)  
  
SET @var = '<TestElement xmlns="http://ns">123</TestElement>'  
  
SELECT @var.query('declare namespace ns="http://ns"   
   data(/ns:TestElement[1]) instance of xs:integer')  
go  
```  
  
 Because the highest parent of xs:integer is xs:decimal, the query will return True if you modify the query and specify xs:decimal as the SequenceType in the query.  
  
```  
SET QUOTED_IDENTIFIER ON  
DECLARE @var XML(MyTestSchema)  
SET @var = '<TestElement xmlns="http://ns">123</TestElement>'  
SELECT @var.query('declare namespace ns="http://ns"     
   data(/ns:TestElement[1]) instance of xs:decimal')  
go  
```  
  
### Example D  
 In this example, you first create an XML schema collection and use it to type an **xml** variable. The typed **xml** variable is then queried to illustrate the `instance of` functionality.  
  
 The following XML schema collection defines a simple type, myType, and an element, <`root`>, of type myType:  
  
```  
drop xml schema collection SC  
go  
CREATE XML SCHEMA COLLECTION SC AS '  
<schema xmlns="http://www.w3.org/2001/XMLSchema" targetNamespace="myNS" xmlns:ns="myNS"  
xmlns:s="https://schemas.microsoft.com/sqlserver/2004/sqltypes">  
      <import namespace="https://schemas.microsoft.com/sqlserver/2004/sqltypes"/>  
      <simpleType name="myType">  
           <restriction base="s:varchar">  
                  <maxLength value="20"/>  
            </restriction>  
      </simpleType>  
      <element name="root" type="ns:myType"/>  
</schema>'  
Go  
```  
  
 Now create a typed **xml** variable and query it:  
  
```  
DECLARE @var XML(SC)  
SET @var = '<root xmlns="myNS">My data</root>'  
SELECT @var.query('declare namespace sqltypes = "https://schemas.microsoft.com/sqlserver/2004/sqltypes";  
declare namespace ns="myNS";   
   data(/ns:root[1]) instance of ns:myType')  
go  
```  
  
 Because myType type derives by restriction from a varchar type that is defined in the sqltypes schema, `instance of` will also return True.  
  
```  
DECLARE @var XML(SC)  
SET @var = '<root xmlns="myNS">My data</root>'  
SELECT @var.query('declare namespace sqltypes = "https://schemas.microsoft.com/sqlserver/2004/sqltypes";  
declare namespace ns="myNS";   
data(/ns:root[1]) instance of sqltypes:varchar?')  
go  
```  
  
### Example E  
 In the following example, the expression retrieves one of the values of the IDREFS attribute and uses `instance of` to determine whether the value is of IDREF type. The example performs the following:  
  
-   Creates an XML schema collection in which the <`Customer`> element has an **OrderList** IDREFS type attribute, and the <`Order`> element has an **OrderID** ID type attribute.  
  
-   Creates a typed **xml** variable and assigns a sample XML instance to it.  
  
-   Specifies a query against the variable. The query expression retrieves the first order ID value from the OrderList IDRERS type attribute of the first <`Customer`>. The value retrieved is IDREF type. Therefore, `instance of` returns True.  
  
```  
create xml schema collection SC as  
'<schema xmlns="http://www.w3.org/2001/XMLSchema" xmlns:Customers="Customers" targetNamespace="Customers">  
            <element name="Customers" type="Customers:CustomersType"/>  
            <complexType name="CustomersType">  
                        <sequence>  
                            <element name="Customer" type="Customers:CustomerType" minOccurs="0" maxOccurs="unbounded" />  
                        </sequence>  
            </complexType>  
             <complexType name="OrderType">  
                <sequence minOccurs="0" maxOccurs="unbounded">  
                            <choice>  
                                <element name="OrderValue" type="integer" minOccurs="0" maxOccurs="unbounded"/>  
                            </choice>  
                </sequence>                                             
                <attribute name="OrderID" type="ID" />  
            </complexType>  
  
            <complexType name="CustomerType">  
                <sequence minOccurs="0" maxOccurs="unbounded">  
                            <choice>  
                                <element name="spouse" type="string" minOccurs="0" maxOccurs="unbounded"/>  
                                <element name="Order" type="Customers:OrderType" minOccurs="0" maxOccurs="unbounded"/>  
                            </choice>  
                </sequence>                                             
                <attribute name="CustomerID" type="string" />  
                <attribute name="OrderList" type="IDREFS" />  
            </complexType>  
 </schema>'  
go  
declare @x xml(SC)  
set @x='<CustOrders:Customers xmlns:CustOrders="Customers">  
                <Customer CustomerID="C1" OrderList="OrderA OrderB"  >  
                              <spouse>Jenny</spouse>  
                                <Order OrderID="OrderA"><OrderValue>11</OrderValue></Order>  
                                <Order OrderID="OrderB"><OrderValue>22</OrderValue></Order>  
  
                </Customer>  
                <Customer CustomerID="C2" OrderList="OrderC OrderD" >  
                                <spouse>John</spouse>  
                                <Order OrderID="OrderC"><OrderValue>33</OrderValue></Order>  
                                <Order OrderID="OrderD"><OrderValue>44</OrderValue></Order>  
  
                        </Customer>  
                <Customer CustomerID="C3"  OrderList="OrderE OrderF" >  
                                <spouse>Jane</spouse>  
                                <Order OrderID="OrderE"><OrderValue>55</OrderValue></Order>  
                                <Order OrderID="OrderF"><OrderValue>55</OrderValue></Order>  
                </Customer>  
                <Customer CustomerID="C4"  OrderList="OrderG"  >  
                                <spouse>Tim</spouse>  
                                <Order OrderID="OrderG"><OrderValue>66</OrderValue></Order>  
                        </Customer>  
                <Customer CustomerID="C5"  >  
                </Customer>  
                <Customer CustomerID="C6" >  
                </Customer>  
                <Customer CustomerID="C7"  >  
                </Customer>  
</CustOrders:Customers>'  
  
select @x.query(' declare namespace CustOrders="Customers";   
 data(CustOrders:Customers/Customer[1]/@OrderList)[1] instance of xs:IDREF ? ') as XML_result  
```  
  
### Implementation Limitations  
 These are the limitations:  
  
-   The **schema-element()** and **schema-attribute()** sequence types are not supported for comparison to the `instance of` operator.  
  
-   Full sequences, for example, `(1,2) instance of xs:integer*`, are not supported.  
  
-   When you are using a form of the **element()** sequence type that specifies a type name, such as `element(ElementName, TypeName)`, the type must be qualified with a question mark (?). For example, `element(Title, xs:string?)` indicates that the element might be null. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] does not support run-time detection of the **xsi:nil** property by using `instance of`.  
  
-   If the value in `Expression` comes from an element or attribute typed as a union, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can only identify the primitive, not derived, type from which the value's type was derived. For example, if <`e1`> is defined to have a static type of (xs:integer | xs:string), the following will return False.  
  
    ```  
    data(<e1>123</e1>) instance of xs:integer  
    ```  
  
     However, `data(<e1>123</e1>) instance of xs:decimal` will return True.  
  
-   For the **processing-instruction()** and **document-node()** sequence types, only forms without arguments are allowed. For example, `processing-instruction()` is allowed, but `processing-instruction('abc')` is not allowed.  
  
## cast as Operator  
 The **cast as** expression can be used to convert a value to a specific data type. For example:  
  
```  
  
Expression cast as  AtomicType?  
```  
  
 In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the question mark (?) is required after the `AtomicType`. For example, as shown in the following query, `"2" cast as xs:integer?` converts the string value to an integer:  
  
```  
declare @x xml  
set @x=''  
select @x.query('"2" cast as xs:integer?')  
```  
  
 In the following query, **data()** returns the typed value of the ProductModelID attribute, a string type. The `cast as`operator converts the value to xs:integer.  
  
```  
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS PD)  
SELECT CatalogDescription.query('  
   data(/PD:ProductDescription[1]/@ProductModelID) cast as xs:integer?  
') as Result  
FROM Production.ProductModel  
WHERE ProductModelID = 19  
```  
  
 The explicit use of **data()** is not required in this query. The `cast as` expression performs implicit atomization on the input expression.  
  
### Constructor Functions  
 You can use the atomic type constructor functions. For example, instead of using the `cast as` operator, `"2" cast as xs:integer?`, you can use the **xs:integer()** constructor function, as in the following example:  
  
```  
declare @x xml  
set @x=''  
select @x.query('xs:integer("2")')  
```  
  
 The following example returns an xs:date value equal to 2000-01-01Z.  
  
```  
declare @x xml  
set @x=''  
select @x.query('xs:date("2000-01-01Z")')  
```  
  
 You can also use constructors for the user-defined atomic types. For example, if the XML schema collection associated with the XML data type defines a simple type, a **myType()** constructor can be used to return a value of that type.  
  
#### Implementation Limitations  
  
-   The XQuery expressions **typeswitch**, **castable**, and **treat** are not supported.  
  
-   **cast as** requires a question mark (?)after the atomic type.  
  
-   **xs:QName** is not supported as a type for casting. Use **expanded-QName** instead.  
  
-   **xs:date**, **xs:time**, and **xs:datetime** require a time zone, which is indicated by a Z.  
  
     The following query fails, because time zone is not specified.  
  
    ```  
    DECLARE @var XML  
    SET @var = ''  
    SELECT @var.query(' <a>{xs:date("2002-05-25")}</a>')  
    go  
    ```  
  
     By adding the Z time zone indicator to the value, the query works.  
  
    ```  
    DECLARE @var XML  
    SET @var = ''  
    SELECT @var.query(' <a>{xs:date("2002-05-25Z")}</a>')  
    go  
    ```  
  
     This is the result:  
  
    ```  
    <a>2002-05-25Z</a>  
    ```  
  
## See Also  
 [XQuery Expressions](../xquery/xquery-expressions.md)   
 [Type System &#40;XQuery&#41;](../xquery/type-system-xquery.md)  
  
  
