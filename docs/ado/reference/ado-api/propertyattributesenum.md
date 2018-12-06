---
title: "PropertyAttributesEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "PropertyAttributesEnum"
helpviewer_keywords: 
  - "PropertyAttributesEnum enumeration [ADO]"
ms.assetid: 96a01955-a6b4-4cbf-9c73-52bcd1e9fb25
author: MightyPen
ms.author: genemi
manager: craigg
---
# PropertyAttributesEnum
Specifies the attributes of a [Property](../../../ado/reference/ado-api/property-object-ado.md) object.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adPropNotSupported**|0|Indicates that the property is not supported by the provider.|  
|**adPropRequired**|1|Indicates that the user must specify a value for this property before the data source is initialized.|  
|**adPropOptional**|2|Indicates that the user does not need to specify a value for this property before the data source is initialized.|  
|**adPropRead**|512|Indicates that the user can read the property.|  
|**adPropWrite**|1024|Indicates that the user can set the property.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.PropertyAttributes.NOTSUPPORTED|  
|AdoEnums.PropertyAttributes.REQUIRED|  
|AdoEnums.PropertyAttributes.OPTIONAL|  
|AdoEnums.PropertyAttributes.READ|  
|AdoEnums.PropertyAttributes.WRITE|  
  
## Applies To  
 [Attributes Property (ADO)](../../../ado/reference/ado-api/attributes-property-ado.md)
