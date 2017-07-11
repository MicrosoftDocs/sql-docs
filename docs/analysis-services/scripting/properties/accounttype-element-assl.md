---
title: "AccountType Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "AccountType Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "AccountType"
helpviewer_keywords: 
  - "AccountType element"
ms.assetid: 4fdf17d3-cd84-4bf6-9baf-21e15d4bf71e
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AccountType Element (ASSL)
  Contains the name of an account type defined in a [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Account>  
   ...  
   <AccountType>...</AccountType>  
   ...  
</Account>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Account](../../../analysis-services/scripting/objects/account-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Income*|The account is an income account.|  
|*Expense*|The account is an expense account.|  
|*Flow*|The account is a cash flow account.|  
|*Balance*|The account is a balance account.|  
|*Asset*|The account is an asset account.|  
|*Liability*|The account is a liability account.|  
|*Statistical*|The account is a statistical account.|  
  
 The enumeration corresponding to the allowed values for **AccountType** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AccountTypes>.  
  
## See Also  
 [Accounts Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/accounts-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  