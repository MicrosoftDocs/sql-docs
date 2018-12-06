---
title: "Field-Related Error Information | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "field-related errors [ADO]"
  - "errors [ADO], field-related"
ms.assetid: 5e7b1af4-996b-47c5-9161-c5575ad4fec9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Field-Related Error Information
If an error is directly related to a field - for example, if the data is missing or if it is the wrong type for the field - you can retrieve more information about the cause of the problem by examining the **Field** object's **Status** property. This property has been enhanced to provide specific information about the problem. So, for example, when a call to **UpdateBatch** fails, the cause of the problem can be determined by examining the **Status** property of the **Fields** in each of the effected records. The property will contain one of the values in the **FieldStatusEnum** constant. The following table includes those values that are of particular interest when an error occurs.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adFieldCantConvertValue**|2|Indicates that the field cannot be retrieved or stored without loss of data.|  
|**adFieldDataOverflow**|6|Indicates that the data returned from the provider overflowed the data type of the field.|  
|**adFieldDefault**|13|Indicates that the default value for the field was used when setting data.|  
|**adFieldIgnore**|15|Indicates that this field was skipped when setting data values in the source. No value was set by the provider.|  
|**adFieldIntegrityViolation**|10|Indicates that the field cannot be modified because it is a calculated or derived entity.|  
|**adFieldIsNull**|3|Indicates that the provider returned a null value.|  
|**adFieldOutOfSpace**|22|Indicates that the provider is unable to obtain enough storage space to complete a move or copy operation.|
