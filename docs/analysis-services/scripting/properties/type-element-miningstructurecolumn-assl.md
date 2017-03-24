---
title: "Type Element (MiningStructureColumn) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Type Element (MiningStructureColumn)"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "TYPE"
helpviewer_keywords: 
  - "Type element"
ms.assetid: ce999716-9487-4348-bc42-270a2026a452
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Type Element (MiningStructureColumn) (ASSL)
  Contains the type of the [MiningStructureColumn](../../../analysis-services/scripting/data-type/miningstructurecolumn-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MiningStructureColumn>  
   ...  
   <Type>...</Type>  
   ...  
</MiningStructureColumn>  
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
|Parent element|[MiningStructureColumn](../../../analysis-services/scripting/data-type/miningstructurecolumn-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Long*|A 64-bit signed integer. This data type maps to the **Int64** data type in the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework and the DBTYPE_I8 data type in OLE DB.|  
|*Boolean*|A Boolean value. This data type maps to the **Boolean** data type in the .NET Framework and the DBTYPE_BOOL data type in OLE DB.|  
|*Text*|A null-terminated stream of Unicode characters. This data type maps to the **String** data type in the .NET Framework and the DBTYPE_WSTR data type in OLE DB.|  
|*Double*|A double-precision floating point number within the range -1.79E +308 through 1.79E +308. This data type maps to the **Double** data type in the .NET Framework and the DBTYPE_R8 data type in OLE DB.|  
|*Date*|Date data, stored as a double-precision floating point number. The whole portion is the number of days since December 30, 1899, while the fractional portion is a fraction of a day. This data type maps to the **DateTime** data type in the .NET Framework and the DBTYPE_DATE data type in OLE DB.|  
|*Table*|A nested table. This data type maps to the DBTYPE_HCHAPTER data type in OLE DB.<br /><br /> Note: Table columns in the .NET Framework do not have an equivalent intrinsic data type, but are instead supported by the **DataReader** class.|  
  
 The enumeration that corresponds to the allowed values for **Type** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructureColumnTypes>.  
  
 The element that corresponds to the parent of **Type** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructureColumn>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  