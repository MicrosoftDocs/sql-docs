---
title: "Atomization (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "08/01/2016"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "XQuery, atomization"
  - "atomization [XQuery]"
ms.assetid: e3d7cf2f-c6fb-43c2-8538-4470a6375af5
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Atomization (XQuery)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Atomization is the process of extracting the typed value of an item. This process is implied under certain circumstances. Some of the XQuery operators, such as arithmetic and comparison operators, depend on this process. For example, when you apply arithmetic operators directly to nodes, the typed value of a node is first retrieved by implicitly invoking the [data function](../xquery/data-accessor-functions-data-xquery.md). This passes the atomic value as an operand to the arithmetic operator.  
  
 For example, the following query returns the total of the LaborHours attributes. In this case, **data()** is implicitly applied to the attribute nodes.  
  
```  
declare @x xml  
set @x='<ROOT><Location LID="1" SetupTime="1.1" LaborHours="3.3" />  
<Location LID="2" SetupTime="1.0" LaborHours="5" />  
<Location LID="3" SetupTime="2.1" LaborHours="4" />  
</ROOT>'  
-- data() implicitly applied to the attribute node sequence.  
SELECT @x.query('sum(/ROOT/Location/@LaborHours)')  
```  
  
 Although not required, you can also explicitly specify the **data()** function:  
  
```  
SELECT @x.query('sum(data(ROOT/Location/@LaborHours))')  
```  
  
 Another example of implicit atomization is when you use arithmetic operators. The **+** operator requires atomic values, and **data()** is implicitly applied to retrieve the atomic value of the LaborHours attribute. The query is specified against the Instructions column of the **xml** type in the ProductModel table. The following query returns the LaborHours attribute three times. In the query, note the following:  
  
-   In constructing the OrignialLaborHours attribute, atomization is implicitly applied to the singleton sequence returned by (`$WC/@LaborHours`). The typed value of the LaborHours attribute is assigned to OrignialLaborHours.  
  
-   In constructing the UpdatedLaborHoursV1 attribute, the arithmetic operator requires atomic values. Therefore, **data()** is implicitly applied to the LaborHours attribute that is returned by (`$WC/@LaborHours`). The atomic value 1 is then added to it. The construction of attribute UpdatedLaborHoursV2 shows the explicit application of **data()**, but is not required.  
  
```  
SELECT Instructions.query('  
     declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
for $WC in /AWMI:root/AWMI:Location[1]  
        return  
            <WC OriginalLaborHours = "{ $WC/@LaborHours }"  
                UpdatedLaborHoursV1 = "{ $WC/@LaborHours + 1 }"   
                UpdatedLaborHoursV2 = "{ data($WC/@LaborHours) + 1 }" >  
            </WC>') as Result  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
 This is the result:  
  
```  
<WC OriginalLaborHours="2.5"   
    UpdatedLaborHoursV1="3.5"   
    UpdatedLaborHoursV2="3.5" />  
```  
  
 The atomization results in an instance of a simple type, an empty set, or a static type error.  
  
 Atomization also occurs in comparison expression parameters passed to functions, values returned by functions, **cast()** expressions, and ordering expressions passed in the order by clause.  
  
## See Also  
 [XQuery Basics](../xquery/xquery-basics.md)   
 [Comparison Expressions &#40;XQuery&#41;](../xquery/comparison-expressions-xquery.md)   
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
