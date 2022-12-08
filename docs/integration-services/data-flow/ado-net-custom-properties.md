---
description: "ADO NET Custom Properties"
title: "ADO NET Custom Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
ms.assetid: e062a9ab-1e6b-4061-845a-4f8a0552b09d
author: chugugrace
ms.author: chugu
---
# ADO NET Custom Properties

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  **Source Custom Properties**  
  
 The ADO NET source has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the ADO NET source. All properties are read/write.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|CommandTimeout|String|A value that specifies the number of seconds before the SQL command times out. A value of 0 indicates that the command never times out.|  
|SqlCommand|String|The SQL statement that the ADO NET source uses to extract data.<br /><br /> When the package loads, you can dynamically update this property with the SQL statement that the ADO NET source will use. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../integration-services/expressions/integration-services-ssis-expressions.md) and [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md).|  
|AllowImplicitStringConversion|Boolean|A value that indicates whether the following occurs:<br /><br /> -No generation of a validation error if there is a mismatch between external metadata types and output column types that are strings (DT_WSTR or DT_NTEXT).<br /><br /> -Implicit conversion of external metadata types to the string data type that the output column uses.<br /><br /> <br /><br /> The default value is TRUE.<br /><br /> For more information, see [ADO NET Source](../../integration-services/data-flow/ado-net-source.md).|  
  
 The output and the output columns of the ADO NET source have no custom properties.  
  
 For more information, see [ADO NET Source](../../integration-services/data-flow/ado-net-source.md).  
  
 **Destination Custom Properties**  
  
 The [!INCLUDE[vstecado](../../includes/vstecado-md.md)] destination has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] destination. All properties are read/write. These properties are not available in the **ADO NET Destination Editor**, but can be set by using the **Advanced Editor**.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|BatchSize|Integer|The number of rows in a batch that is sent to the server. A value of **0** indicates that the batch size matches the internal buffer size. The default value of this property is **0**.|  
|CommandTimeOut|Integer|The maximum number of seconds that the SQL command can run before timing out. A value of **0** indicates an infinite time. The default value of this property is **0**.|  
|TableOrViewName|String|The name of the destination table or view.|  
  
 For more information, see [ADO NET Destination](../../integration-services/data-flow/ado-net-destination.md).  
  
## See Also  
 [Common Properties](./set-the-properties-of-a-data-flow-component.md)  
  
