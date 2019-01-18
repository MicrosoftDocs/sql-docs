---
title: "Data Types in Analysis Services | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: olap
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Data Types in Analysis Services
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  For all <xref:Microsoft.AnalysisServices.DataItem> objects, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] supports the following subset of **System.Data.OleDb.OleDbType**. To set or read the data type, use [DataItem Data Type &#40;ASSL&#41;](https://docs.microsoft.com/bi-reference/assl/data-type/dataitem-data-type-assl).  
  
## Supported Data Types  
  
|||  
|-|-|  
|BigInt|A 64-bit signed integer. The *BigInt* value type represents integers with values ranging from negative 9,223,372,036,854,775,808 to positive 9,223,372,036,854,775,807.|  
|Binary|A stream of binary data of **Byte** type. **Byte** is a value type that represents unsigned integers with values that range from 0 to 255.|  
|Boolean|Instances of this type have values of either **true** or **false**.|  
|Currency|A *currency* value ranging from -922,337,203,685,477.5808 to +922,337,203,685,477.5807 with accuracy to a ten-thousandth of a currency unit (four decimal places).|  
|Date|Date and time data, stored as a double. The whole portion is the number of days since December 30, 1899, and the fractional portion is a fraction of a day or time of the day.|  
|Double|A floating-point number within the range of -1.79769313486232E +308 to 1.79769313486232E +308. A Double value stores number information up to 15 decimal digits of precision.|  
|Integer|A 32-bit signed integer that represents signed integers with values that range from negative 2,147,483,648 through positive 2,147,483,647.|  
|Single|A floating-point number within the range of - 3.4028235E +38 through 3.4028235E +38. A Single value stores number information up to 7 decimal digits of precision.|  
|Smallint|A 16-bit signed integer. The *Smallint* value type represents signed integers with values ranging from negative 32768 to positive 32767.|  
|Tinyint|An 8-bit signed integer. The Tinyint value type represents integers with values ranging from negative 128 to positive 127.|  
|UnsignedBigInt|A 64-bit unsigned integer. The *UnsignedBigInt* value type represents unsigned integers with values ranging from 0 to 18,446,744,073,709,551,615.|  
|UnsignedInt|A 32-bit unsigned integer. The *UnsignedInt* value type represents unsigned integers with values ranging from 0 to 4,294,967,295.|  
|UnsignedSmallInt|A 16-bit unsigned integer. The *UnsignedSmallInt* value type represents unsigned integers with values ranging from 0 to 65535.|  
|UnsignedTinyInt|An 8-bit unsigned integer. The *UnsignedTinyInt* value type represents unsigned integers with values that range from 0 to 255|  
|WChar|A null-terminated stream of Unicode characters. A *WChar* is a sequential collection of Unicode characters that is used to represent text.|  
  
## AMO Validations on Data Types  
 The following table lists the extra validations that Analysis Management Objects (AMO) does for certain bindings:  
  
|Object|Binding|Allowed Data Types|  
|------------|-------------|------------------------|  
|DimensionAttribute|KeyColumns|All but Binary|  
||NameColumn|Only WChar|  
||SkippedLevelsColumn|Only integer types: BigInt, Integer, SmallInt, TinyInt, UnsignedBigInt, UnsignedInt, UnsignedSmallInt, UnsignedTinyInt|  
||CustomRollupColumn|Only WChar|  
||CustomRollupPropertiesColumn|Only WChar|  
||UnaryOperatorColumn|Only WChar|  
||ValueColumn|All|  
|AttributeTranslation|CaptionColumn|Only WChar|  
|ScalarMiningStructureColumn|KeyColumns|All but Binary|  
||NameColumn|Only WChar|  
|TableMiningStructureColumn|ForeignKeyColumns|All but Binary|  
|MeasureGroupAttribute|KeyColumns|All but Binary|  
|Distinct Count Measure|Source|BigInt, Currency, Double, Integer, Single, SmallInt, TinyInt, UnsignedBigInt, UnsignedInt, UnsignedSmallInt, UnsignedTinyInt|  
  
  
