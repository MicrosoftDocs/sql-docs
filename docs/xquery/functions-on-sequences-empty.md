---
title: "empty Function (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "empty function"
  - "fn:empty function"
ms.assetid: 46da89a8-0cd9-4913-8521-4087589a04ba
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Functions on Sequences - empty
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns True if the value of *$arg* is an empty sequence. Otherwise, the function returns False.  
  
## Syntax  
  
```  
  
fn:empty($arg as item()*) as xs:boolean  
```  
  
## Arguments  
 *$arg*  
 A sequence of items. If the sequence is empty, the function returns True. Otherwise, the function returns False.  
  
## Remarks  
 The **fn:exists()** function is not supported. As an alternative, the **not()** function can be used.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Using the empty() XQuery function to determine if an attribute is present  
 In the manufacturing process for Product Model 7, this query returns all the work center locations that do not have a **MachineHours** attribute.  
  
```  
SELECT ProductModelID, Instructions.query('  
declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
     for $i in /AWMI:root/AWMI:Location[empty(@MachineHours)]  
     return  
       <Location  
            LocationID="{ ($i/@LocationID) }"  
            LaborHrs="{ ($i/@LaborHours) }" >  
            {   
              $i/@MachineHours  
            }    
       </Location>  
') as Result  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
 This is the result:  
  
```  
ProductModelID      Result          
-------------- ------------------------------------------  
7              <Location LocationID="30" LaborHrs="1"/>  
               <Location LocationID="50" LaborHrs="3"/>  
               <Location LocationID="60" LaborHrs="4"/>  
```  
  
 The following, slightly modified, query returns "NotFound" if the **MachineHour** attribute is not present:  
  
```  
SELECT ProductModelID, Instructions.query('  
declare namespace p14="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
     for $i in /p14:root/p14:Location  
     return  
       <Location  
            LocationID="{ ($i/@LocationID) }"  
            LaborHrs="{ ($i/@LaborHours) }" >  
            {   
                 if (empty($i/@MachineHours)) then  
                    attribute MachineHours { "NotFound" }  
                 else  
                    attribute MachineHours { data($i/@MachineHours) }  
            }    
       </Location>  
') as Result  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
 This is the result:  
  
```  
ProductModelID Result                         
-------------- -----------------------------------  
7                
  <Location LocationID="10" LaborHrs="2.5" MachineHours="3"/>  
  <Location LocationID="20" LaborHrs="1.75" MachineHours="2"/>  
  <Location LocationID="30" LaborHrs="1" MachineHours="NotFound"/>  
  <Location LocationID="45" LaborHrs="0.5" MachineHours="0.65"/>  
  <Location LocationID="50" LaborHrs="3" MachineHours="NotFound"/>  
  <Location LocationID="60" LaborHrs="4" MachineHours="NotFound"/>  
```  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)   
 [exist&#40;&#41; Method &#40;xml Data Type&#41;](../t-sql/xml/exist-method-xml-data-type.md)  
  
  
