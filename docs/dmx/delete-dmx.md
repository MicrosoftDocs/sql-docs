---
title: "DELETE (DMX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DELETE"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "DELETE statement [DMX]"
  - "mining structures [DMX], clearing"
  - "clearing mining models"
  - "deleting mining models"
  - "mining models [Analysis Services], clearing"
  - "deleting mining structures"
ms.assetid: 5a8204c3-a3df-4d97-9c1d-d997d24c70e3
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DELETE (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Clears a mining model, a mining structure, or a mining structure and all its associated mining models, depending on the Data Mining Extensions (DMX) clauses that you use.  
  
## Syntax  
  
```  
  
DELETE FROM [MINING MODEL] <model>[.CONTENT]  
DELETE FROM [MINING STRUCTURE] <structure>[.CONTENT]|[.CASES]  
```  
  
## Arguments  
 *model*  
 A model identifier.  
  
 *structure*  
 A structure identifier.  
  
## Remarks  
 If you do not specify **MINING MODEL** or **MINING STRUCTURE**, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] searches for the object type based on the name, and processes the correct object. If the server contains a mining structure and a mining model that have the same name, an error is returned.  
  
 The following table describes the result of using different forms of the syntax.  
  
|Statement|Result|  
|---------------|------------|  
|DELETE FROM MINING STRUCTURE*\<structure>*<br /><br /> or<br /><br /> DELETE FROM MINING STRUCTURE*\<structure>*.CONTENT|Performs ProcessClear on the mining structure. All the content is cleared from the mining structure and its associated mining models.|  
|DELETE FROM MINING STRUCTURE*\<structure>*.CASES|Performs ProcessClearStructureOnly on the mining structure. All the content is cleared from the mining structure, leaving its associated mining models intact. Drillthrough on the associated mining models will fail after the mining structure has been cleared.|  
|DELETE FROM MINING MODEL*\<model>*<br /><br /> or<br /><br /> DELETE FROM MINING MODEL*\<model>*.CONTENT|Performs ProcessClear on the mining model but leaves the state values intact. State values are the possible states of a column. For example, state values for a gender column would be male and female.|  
  
 For more information about processing types, see [Type Element &#40;XMLA&#41;](../analysis-services/xmla/xml-elements-properties/type-element-xmla.md).  
  
## Examples  
 The following example removes all of the content from the NB_Sample model.  
  
```  
DELETE FROM NB_Sample.CONTENT  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
