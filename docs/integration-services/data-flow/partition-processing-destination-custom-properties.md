---
title: "Partition Processing Destination Custom Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 3eac4413-0c90-4b06-8f7e-d0d72f4d869d
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Partition Processing Destination Custom Properties
  The Partition Processing destination has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Partition Processing destination. All properties are read/write.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|ASConnectionString|String|The connection string to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project or an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
|KeyDuplicate|Integer (enumeration)|When UseDefaultConfiguration is **False**, a value that indicates how to handle duplicate key errors. The possible values are **IgnoreError** (0), **ReportAndContinue** (1), and **ReportAndStop** (2). The default value of this property is **IgnoreError** (0).|  
|KeyErrorAction|Integer (enumeration)|When UseDefaultConfiguration is **False**, a value that indicates how to handle key errors. The possible values are **ConvertToUnknown** (0) and **DiscardRecord** (1). The default value of this property is **ConvertToUnknown** (0).|  
|KeyErrorLimit|Integer|When UseDefaultConfiguration is **False**, the upper limit of key errors that are allowed.|  
|KeyErrorLimitAction|Integer (enumeration)|When UseDefaultConfiguration is **False**, a value that indicates the action to take when **KeyErrorLimit** is reached. The possible values are **StopLogging** (1) and **StopProcessing** (0). The default value of this property is **StopProcessing** (0).|  
|KeyErrorLogFile|String|When UseDefaultConfiguration is **False**, the path and file name of the error log file.|  
|KeyNotFound|Integer (enumeration)|When UseDefaultConfiguration is **False**, a value that indicates how to handle missing key errors. The possible values are **IgnoreError** (0), **ReportAndContinue** (1), and **ReportAndStop** (2). The default value of this property is **ReportAndContinue** (1).|  
|NullKeyConvertedToUnknown|Integer (enumeration)|When UseDefaultConfiguration is **False**, a value that indicates how to handle null keys converted to the Unknown value. The possible values are **IgnoreError** (0), **ReportAndContinue** (1), and **ReportAndStop** (2). The default value of this property is **IgnoreError** (0).|  
|NullKeyNotAllowed|Integer (enumeration)|When UseDefaultConfiguration is **False**, a value that indicates how to handle disallowed nulls. The possible values are **IgnoreError** (0), **ReportAndContinue** (1), and **ReportAndStop** (2). The default value of this property is **ReportAndContinue** (1).|  
|ProcessType|Integer (enumeration)|The type of partition processing the transformation uses. The possible values are **ProcessAdd** (1) (incremental), **ProcessFull** (0), and **ProcessUpdate** (2).|  
|UseDefaultConfiguration|Boolean|A value that specifies whether the transformation uses the default error configuration. If this property is **False**, the transformation uses the values of the error-handling custom properties listed in this table, including KeyDuplicate, KeyErrorAction, and so on.|  
  
 The input and the input columns of the Partition Processing destination have no custom properties.  
  
 For more information, see [Partition Processing Destination](../../integration-services/data-flow/partition-processing-destination.md).  
  
## See Also  
 [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
  
