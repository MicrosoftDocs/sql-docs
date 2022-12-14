---
title: "expanded-QName (XQuery) | Microsoft Docs"
description: Learn how to use the expanded-QName() function to return the namespace URI and local name part of a QName.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "expanded-QName function"
  - "fn:expanded-QName function"
ms.assetid: b8377042-95cc-467b-9ada-fe43cebf4bc3
author: "rothja"
ms.author: "jroth"
---
# Functions Related to QNames - expanded-QName
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  Returns a value of the xs:QName type with the namespace URI specified in the *$paramURI* and the local name specified in the *$paramLocal*. If *$paramURI* is the empty string or the empty sequence, it represents no namespace.  
  
## Syntax  
  
```  
fn:expanded-QName($paramURI as xs:string?, $paramLocal as xs:string?) as xs:QName?  
```  
  
## Arguments  
 *$paramURI*  
 Is the namespace URI for the QName.  
  
 *$paramLocal*  
 Is the local name part of the QName.  
  
## Remarks  
 The following applies to the **expanded-QName()** function:  
  
-   If the *$paramLocal* value specified is not in the correct lexical form for xs:NCName type, the empty sequence is returned and represents a dynamic error.  
  
-   Conversion from xs:QName type to any other type is not supported in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Because of this, the **expanded-QName()** function cannot be used in XML construction. For example, when you are constructing a node, such as `<e> expanded-QName(...) </e>`, the value has to be untyped. This would require that you convert the xs:QName type value returned by `expanded-QName()` to xdt:untypedAtomic. However, this is not supported. A solution is provided in an example later in this topic.  
  
-   You can modify or compare the existing QName type values. For example, `/root[1]/e[1] eq expanded-QName("http://nsURI" "myNS")` compares the value of the element, <`e`>, with the QName returned by the **expanded-QName()** function.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database.  
  
### A. Replacing a QName type node value  
 This example illustrates how you can modify the value of an element node of QName type. The example performs the following:  
  
-   Creates an XML schema collection that defines an element of QName type.  
  
-   Creates a table with an **xml** type column by using the XML schema collection.  
  
-   Saves an XML instance in the table.  
  
-   Uses the **modify()** method of the xml data type to modify the value of the QName type element in the instance. The **expanded-QName()** function is used to generate the new QName type value.  
  
```  
-- If XML schema collection (if exists)  
-- drop xml schema collection SC  
-- go  
-- Create XML schema collection  
CREATE XML SCHEMA COLLECTION SC AS N'  
<schema xmlns="http://www.w3.org/2001/XMLSchema"  
    xmlns:xs="http://www.w3.org/2001/XMLSchema"   
    targetNamespace="QNameXSD"   
      xmlns:xqo="QNameXSD" elementFormDefault="qualified">  
      <element name="Root" type="xqo:rootType" />  
      <complexType name="rootType">  
            <sequence minOccurs="1" maxOccurs="1">  
                        <element name="ElemQN" type="xs:QName" />  
            </sequence>  
      </complexType>  
</schema>'  
go  
-- Create table.  
CREATE TABLE T( XmlCol xml(SC) )  
-- Insert sample XML instnace  
INSERT INTO T VALUES ('  
<Root xmlns="QNameXSD" xmlns:ns="https://myURI">  
      <ElemQN>ns:someName</ElemQN>  
</Root>')  
go  
-- Verify the insertion  
SELECT * from T  
go  
-- Result  
<Root xmlns="QNameXSD" xmlns:ns="https://myURI">  
  <ElemQN>ns:someName</ElemQN>  
</Root>   
```  
  
 In the following query, the <`ElemQN`> element value is replaced by using the **modify()** method of the xml data type and the replace value of XML DML, as shown.  
  
```  
-- the value.  
UPDATE T   
SET XmlCol.modify('  
  declare default element namespace "QNameXSD";   
  replace value of /Root[1]/ElemQN   
  with expanded-QName("https://myURI", "myLocalName") ')  
go  
-- Verify the result  
SELECT * from T  
go  
```  
  
 This is the result. Note that the element <`ElemQN`> of QName type now has a new value:  
  
```  
<Root xmlns="QNameXSD" xmlns:ns="urn">  
  <ElemQN xmlns:p1="https://myURI">p1:myLocalName</ElemQN>  
</Root>  
```  
  
 The following statements remove the objects used in the example.  
  
```  
-- Cleanup  
DROP TABLE T  
go  
drop xml schema collection SC  
go  
```  
  
### B. Dealing with the limitations when using the expanded-QName() function  
 The **expanded-QName** function cannot be used in XML construction. The following example illustrates this. To work around this limitation, the example first inserts a node and then modifies the node.  
  
```  
-- if exists drop the table T  
--drop table T  
-- go  
-- Create XML schema collection  
-- DROP XML SCHEMA COLLECTION SC  
-- go  
CREATE XML SCHEMA COLLECTION SC AS '  
<schema xmlns="http://www.w3.org/2001/XMLSchema">  
      <element name="root" type="QName" nillable="true"/>  
</schema>'  
go  
 -- Create table T with a typed xml column (using the XML schema collection)  
CREATE TABLE T (xmlCol XML(SC))  
go  
-- Insert an XML instance.  
insert into T values ('<root xmlns:a="https://someURI">a:b</root>')  
 go  
-- Verify  
SELECT *   
FROM T  
```  
  
 The following attempt adds another <`root`> element but fails, because the expanded-QName() function is not supported in XML construction.  
  
```  
update T SET xmlCol.modify('  
insert <root>{expanded-QName("http://ns","someLocalName")}</root> as last into / ')  
go  
```  
  
 A solution to this is to first insert an instance with a value for the <`root`> element and then modify it. In this example, a nil initial value is used when the <`root`> element is inserted. The XML schema collection in this example allows a nil value for the <`root`> element.  
  
```  
update T SET xmlCol.modify('  
insert <root xsi:nil="true"/> as last into / ')  
go  
-- now replace the nil value with another QName.  
update T SET xmlCol.modify('  
replace value of /root[last()] with expanded-QName("http://ns","someLocalName") ')  
go  
 -- verify   
SELECT * FROM T  
go  
-- result  
<root>b</root>  
```  
  
 `<root xmlns:a="https://someURI">a:b</root>`  
  
 `<root xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:p1="http://ns">p1:someLocalName</root>`  
  
 You can compare the QName value, as shown in the following query. The query returns only the <`root`> elements whose values match the QName type value returned by the **expanded-QName()** function.  
  
```  
SELECT xmlCol.query('  
    for $i in /root  
    return  
       if ($i eq expanded-QName("http://ns","someLocalName") ) then  
          $i  
       else  
          ()')  
FROM T  
```  
  
### Implementation Limitations  
 There is one limitation: The **expanded-QName()** function accepts the empty sequence as the second argument and will return empty instead of raising a run-time error when the second argument is incorrect.  
  
## See Also  
 [Functions Related to QNames &#40;XQuery&#41;]()  
  
