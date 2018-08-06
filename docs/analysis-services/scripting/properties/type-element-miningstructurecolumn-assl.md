---
title: "Type Element (MiningStructureColumn) (ASSL) | Microsoft Docs"
ms.date: 5/8/2018
ms.prod: sql
ms.custom: assl
ms.reviewer: owend
ms.technology: analysis-services
ms.topic: reference
author: minewiskan
ms.author: owend
manager: kfile
---
# Type Element (MiningStructureColumn) (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
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
  
  
