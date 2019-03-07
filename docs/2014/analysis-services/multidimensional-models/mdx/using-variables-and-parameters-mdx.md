---
title: "Using Variables and Parameters (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "parameters [MDX]"
  - "queries [MDX], variables"
  - "queries [MDX], parameters"
  - "variables [MDX]"
ms.assetid: a4754d16-d9c4-49f6-9be0-392180b912e4
author: minewiskan
ms.author: owend
manager: craigg
---
# Using Variables and Parameters (MDX)
  In [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], you can parameterize a Multidimensional Expressions (MDX) statement. A parameterized statement lets you create generic statements that can be customized at runtime.  
  
 In creating a parameterized statement, you identify the parameter name by prefixing the name with the at sign (@). For example, @Year would be a valid parameter name  
  
 MDX supports only parameters for literal or scalar values. To create a parameter that references a member, set, or tuple, you would have to use a function such as [StrToMember](/sql/mdx/strtomember-mdx) or [StrToSet](/sql/mdx/strtoset-mdx).  
  
 In the following XML for Analysis (XMLA) example, the @CountryName parameter will contain the country for which customer data is retrieved:  
  
```  
<Envelope xmlns="http://schemas.xmlsoap.org/soap/envelope/">  
  <Body>  
    <Execute xmlns="urn:schemas-microsoft-com:xml-analysis">  
      <Command>  
        <Statement>  
select [Measures].members on 0,   
       Filter(Customer.[Customer Geography].Country.members,   
              Customer.[Customer Geography].CurrentMember.Name =  
              @CountryName) on 1  
from [Adventure Works]  
</Statement>  
      </Command>  
      <Properties />  
      <Parameters>  
        <Parameter>  
          <Name>CountryName</Name>  
          <Value>'United Kingdom'</Value>  
        </Parameter>  
      </Parameters>  
    </Execute>  
  </Body>  
</Envelope>  
```  
  
 To use this functionality with OLE DB, you would use the `ICommandWithParameters` interface. To use this functionality with ADOMD.Net, you would use the **AdomdCommand.Parameters** collection.  
  
## See Also  
 [MDX Scripting Fundamentals &#40;Analysis Services&#41;](mdx-scripting-fundamentals-analysis-services.md)  
  
  
