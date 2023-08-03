---
title: "FieldStatusEnum"
description: "FieldStatusEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "FieldStatusEnum"
helpviewer_keywords:
  - "FieldStatusEnum enumeration [ADO]"
apitype: "COM"
---
# FieldStatusEnum
Specifies the [status](./status-property-ado-field.md) of a [Field Object](./field-object.md).  
  
 The **adFieldPending\*** values indicate the operation that caused the status to be set, and may be combined with other status values.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adFieldAlreadyExists**|26|Indicates that the specified field already exists.|  
|**adFieldBadStatus**|12|Indicates that an invalid status value was sent from ADO to the OLE DB provider. Possible causes include an OLE DB 1.0 or 1.1 provider, or an improper combination of [Value](./value-property-ado.md) and [Status](./status-property-ado-field.md).|  
|**adFieldCannotComplete**|20|Indicates that the server of the URL specified by [Source](./source-property-ado-record.md) could not complete the operation.|  
|**adFieldCannotDeleteSource**|23|Indicates that during a move operation, a tree or subtree was moved to a new location, but the source could not be deleted.|  
|**adFieldCantConvertValue**|2|Indicates that the field cannot be retrieved or stored without loss of data.|  
|**adFieldCantCreate**|7|Indicates that the field could not be added because the provider exceeded a limitation (such as the number of fields allowed).|  
|**adFieldDataOverflow**|6|Indicates that the data returned from the provider overflowed the data type of the field.|  
|**adFieldDefault**|13|Indicates that the default value for the field was used when setting data.|  
|**adFieldDoesNotExist**|16|Indicates that the field specified does not exist.|  
|**adFieldIgnore**|15|Indicates that this field was skipped when setting data values in the source. The provider set no value.|  
|**adFieldIntegrityViolation**|10|Indicates that the field cannot be modified because it is a calculated or derived entity.|  
|**adFieldInvalidURL**|17|Indicates that the data source URL contains invalid characters.|  
|**adFieldIsNull**|3|Indicates that the provider returned a VARIANT value of type VT_NULL and that the field is not empty.|  
|**adFieldOK**|0|Default. Indicates that the field was successfully added or deleted.|  
|**adFieldOutOfSpace**|22|Indicates that the provider is unable to obtain enough storage space to complete a move or copy operation.|  
|**adFieldPendingChange**|0x40000|Indicates either that the field has been deleted and then re-added, perhaps with a different data type, or that the value of the field which previously had a status of **adFieldOK** has changed. The final form of the field will modify the [Fields](./fields-collection-ado.md) collection after the [Update](./update-method.md) method is called.|  
|**adFieldPendingDelete**|0x20000|Indicates that the **Delete** operation caused the status to be set. The field has been marked for deletion from the **Fields** collection after the **Update** method is called.|  
|**adFieldPendingInsert**|0x10000|Indicates that the **Append** operation caused the status to be set. The **Field** has been marked to be added to the **Fields** collection after the **Update** method is called.|  
|**adFieldPendingUnknown**|0x80000|Indicates that the provider cannot determine what operation caused field status to be set.|  
|**adFieldPendingUnknownDelete**|0x100000|Indicates that the provider cannot determine what operation caused field status to be set, and that the field will be deleted from the **Fields** collection after the **Update** method is called.|  
|**adFieldPermissionDenied**|9|Indicates that the field cannot be modified because it is defined as read-only.|  
|**adFieldReadOnly**|24|Indicates that the field in the data source is defined as read-only.|  
|**adFieldResourceExists**|19|Indicates that the provider was unable to perform the operation because an object already exists at the destination URL and it is not able to overwrite the object.|  
|**adFieldResourceLocked**|18|Indicates that the provider was unable to perform the operation because the data source is locked by one or more other application or process.|  
|**adFieldResourceOutOfScope**|25|Indicates that a source or destination URL is outside the scope of the current record.|  
|**adFieldSchemaViolation**|11|Indicates that the value violated the data source schema constraint for the field.|  
|**adFieldSignMismatch**|5|Indicates that data value returned by the provider was signed but the data type of the ADO field value was unsigned.|  
|**adFieldTruncated**|4|Indicates that variable-length data was truncated when reading from the data source.|  
|**adFieldUnavailable**|8|Indicates that the provider could not determine the value when reading from the data source. For example, the row was just created, the default value for the column was not available, and a new value had not yet been specified.|  
|**adFieldVolumeNotFound**|21|Indicates that the provider is unable to locate the storage volume indicated by the URL.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [Status Property (ADO Field)](./status-property-ado-field.md)