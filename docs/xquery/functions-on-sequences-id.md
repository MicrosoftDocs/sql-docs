---
title: "id Function (XQuery) | Microsoft Docs"
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
  - "fn:id function"
  - "id function"
ms.assetid: de99fc60-d0ad-4117-a17d-02bdde6512b4
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Functions on Sequences - id
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns the sequence of element nodes with xs:ID values that match the values of one or more of the xs:IDREF values supplied in *$arg*.  
  
## Syntax  
  
```  
  
fn:id($arg as xs:IDREF*) as element()*  
```  
  
## Arguments  
 *$arg*  
 One or more xs:IDREF values.  
  
## Remarks  
 The result of the function is a sequence of elements in the XML instance, in document order, that has an xs:ID value equal to one or more of the xs:IDREFs in the list of candidate xs:IDREFs.  
  
 If the xs:IDREF value does not match any element, the function returns the empty sequence.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database.  
  
### A. Retrieving elements based on the IDREF attribute value  
 The following example uses fn:id to retrieve the <`employee`> elements, based on the IDREF manager attribute. In this example, the manager attribute is an IDREF type attribute and the eid attribute is an ID type attribute.  
  
 For a specific manager attribute value, the **id()** function finds the <`employee`> element whose ID type attribute value matches the input IDREF value. In other words, for a specific employee, the **id()** function returns employee manager.  
  
 This is what happens in the example:  
  
-   An XML schema collection is created.  
  
-   A typed **xml** variable is created by using the XML schema collection.  
  
-   The query retrieves the element that has an ID attribute value referenced by the **manager** IDREF attribute of the <`employee`> element.  
  
```  
-- If exists, drop the XML schema collection (SC).  
-- drop xml schema collection SC  
-- go  
  
create xml schema collection SC as  
'<schema xmlns="http://www.w3.org/2001/XMLSchema" xmlns:e="emp" targetNamespace="emp">  
            <element name="employees" type="e:EmployeesType"/>  
            <complexType name="EmployeesType">  
                 <sequence>  
                      <element name="employee" type="e:EmployeeType" minOccurs="0" maxOccurs="unbounded" />  
                 </sequence>  
            </complexType>    
  
            <complexType name="EmployeeType">  
                        <attribute name="eid" type="ID" />  
                        <attribute name="name" type="string" />  
                        <attribute name="manager" type="IDREF" />  
            </complexType>         
</schema>'  
go  
```  
  
```  
declare @x xml(SC)  
set @x='<e:employees xmlns:e="emp">  
<employee eid="e1" name="Joe" manager="e10" />  
<employee eid="e2" name="Bob" manager="e10" />  
<employee eid="e10" name="Dave" manager="e10" />  
</e:employees>'  
  
select @x.value(' declare namespace e="emp";   
 (fn:id(e:employees/employee[@name="Joe"]/@manager)/@name)[1]', 'varchar(50)')   
Go  
```  
  
 The query returns "Dave" as the value. This indicates that Dave is Joe's manager.  
  
### B. Retrieving elements based on the OrderList IDREFS attribute value  
 In the following example, the OrderList attribute of the <`Customer`> element is an IDREFS type attribute. It lists the order ids for that specific customer. For each order id, there is an <`Order`> element child under the <`Customer`> providing the order value.  
  
 The query expression, `data(CustOrders:Customers/Customer[1]/@OrderList)[1]`, retrieves the first value from the IDRES list for the first customer. This value is then passed to the **id()** function. The function then finds the <`Order`> element whose OrderID attribute value matches the input to the **id()** function.  
  
```  
drop xml schema collection SC  
go  
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
select @x.query('declare namespace CustOrders="Customers";  
  id(data(CustOrders:Customers/Customer[1]/@OrderList)[1])')  
  
-- result  
<Order OrderID="OrderA">  
  <OrderValue>11</OrderValue>  
</Order>  
```  
  
### Implementation Limitations  
 These are the limitations:  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] does not support the two-argument version of **id()**.  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] requires the argument type of **id()** to be a subtype of xs:IDREF*.  
  
## See Also  
 [Functions on Sequences](https://msdn.microsoft.com/library/672d2795-53ab-49c2-bf24-bc81a47ecd3f)  
  
  
