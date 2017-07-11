---
title: "DataType Element (ASSL) | Microsoft Docs"
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
  - "DataType Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "DataType"
helpviewer_keywords: 
  - "DataType element"
ms.assetid: efe6f717-8288-4ca2-85ed-9b63d27c02d8
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DataType Element (ASSL)
  Defines the data type of the associated element.  
  
## Syntax  
  
```xml  
  
<DataItem> <!-- or Measure -->  
   ...  
   <DataType>...</DataType>  
   ...  
</DataItem>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DataItem](../../../analysis-services/scripting/data-type/dataitem-data-type-assl.md), [Measure](../../../analysis-services/scripting/objects/measure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The values for **DataType** are defined in the **System.Data.OleDb.OleDbType** enumeration. However, only the enumeration values in the following table are valid in the **DataType** element.  
  
|Value|Description|  
|-----------|-----------------|  
|*BigInt*|A 64-bit signed integer. This data type maps to the **Int64** data type in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework and the DBTYPE_I8 data type in OLE DB.|  
|*Bool*|A Boolean value. This data type maps to the **Boolean** data type in the .NET Framework and the DBTYPE_BOOL data type in OLE DB.|  
|*Currency*|A currency value ranging from -2<sup>63</sup> (or -922,337,203,685,477.5808) to 2<sup>63</sup>-1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a currency unit. This data type maps to the **Decimal** data type in the .NET Framework and the DBTYPE_CY data type in OLE DB.|  
|*Date*|Date data, stored as a double-precision floating point number. The whole portion is the number of days since December 30, 1899, while the fractional portion is a fraction of a day. This data type maps to the **DateTime** data type in the .NET Framework and the DBTYPE_DATE data type in OLE DB.|  
|*Double*|A double-precision floating point number within the range of -1.79E +308 through 1.79E +308. This data type maps to the **Double** data type in the .NET Framework and the DBTYPE_R8 data type in OLE DB.|  
|*Integer*|A 32-bit signed integer. This data type maps to the **Int32** data type in the .NET Framework and the DBTYPE_I4 data type in OLE DB.|  
|*Single*|A single-precision floating point number within the range of -3.40E +38 through 3.40E +38. This data type maps to the **Single** data type in .NET Framework and the DBTYPE_R4 data type in OLE DB.|  
|*SmallInt*|A 16-bit signed integer. This data type maps to the **Int16** data type in the .NET Framework and the DBTYPE_I2 data type in OLE DB.|  
|*TinyInt*|An 8-bit signed integer. This data type maps to the **SByte** data type in the .NET Framework and the DBTYPE_I1 data type in OLE DB.|  
|*UnsignedBigInt*|A 64-bit unsigned integer. This data type maps to the **UInt64** data type in .NET Framework and the DBTYPE_UI8 data type in OLE DB.|  
|*UnsignedInt*|A 32-bit unsigned integer. This data type maps to the **UInt32** data type in the .NET Framework and the DBTYPE_UI4 data type in OLE DB.|  
|*UnsignedSmallInt*|A 16-bit unsigned integer. This data type maps to the **UInt16** data type in the .NET Framework and the DBTYPE_UI2 data type in OLE DB.|  
|*WChar*|A null-terminated stream of Unicode characters. This data type maps to the **String** data type in the .NET Framework and the DBTYPE_WSTR data type in OLE DB.|  
|*Inherited*|The data type of the **DataItem** contained in the [Source](../../../analysis-services/scripting/properties/source-element-measure-assl.md) element of the **Measure** element.<br /><br /> Note: Applicable only to **Measure** elements.|  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  