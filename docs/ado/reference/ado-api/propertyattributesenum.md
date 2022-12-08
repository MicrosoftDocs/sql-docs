---
title: "PropertyAttributesEnum"
description: "PropertyAttributesEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "PropertyAttributesEnum"
helpviewer_keywords:
  - "PropertyAttributesEnum enumeration [ADO]"
apitype: "COM"
---
# PropertyAttributesEnum
Specifies the attributes of a [Property](./property-object-ado.md) object.  
  
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
 [Attributes Property (ADO)](./attributes-property-ado.md)