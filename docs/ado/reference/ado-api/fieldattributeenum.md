---
title: "FieldAttributeEnum"
description: "FieldAttributeEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "FieldAttributeEnum"
helpviewer_keywords:
  - "FieldAttributeEnum enumeration [ADO]"
apitype: "COM"
---
# FieldAttributeEnum
Specifies one or more attributes of a [Field](../../../ado/reference/ado-api/field-object.md) object.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adFldCacheDeferred**|0x1000|Indicates that the provider caches field values and that subsequent reads are done from the cache.|  
|**adFldFixed**|0x10|Indicates that the field contains fixed-length data.|  
|**adFldIsChapter**|0x2000|Indicates that the field contains a chapter value, which specifies a specific child recordset related to this parent field. Typically chapter fields are used with data shaping or filters.|  
|**adFldIsCollection**|0x40000|Indicates that the field specifies that the resource represented by the record is a collection of other resources, such as a folder, rather than a simple resource, such as a text file.|  
|**adFldKeyColumn**|0x8000|Indicates that the field specifies all or part of the column's primary key.|  
|**adFldIsDefaultStream**|0x20000|Indicates that the field contains the default stream for the resource represented by the record. For example, the default stream can be the HTML content of a root folder on a Web site, which is automatically served when the root URL is specified.|  
|**adFldIsNullable**|0x20|Indicates that the field accepts null values.|  
|**adFldIsRowURL**|0x10000|Indicates that the field contains the URL that names the resource from the data store represented by the record.|  
|**adFldLong**|0x80|Indicates that the field is a long binary field. Also indicates that you can use the [AppendChunk](../../../ado/reference/ado-api/appendchunk-method-ado.md) and [GetChunk](../../../ado/reference/ado-api/getchunk-method-ado.md) methods.|  
|**adFldMayBeNull**|0x40|Indicates that you can read null values from the field.|  
|**adFldMayDefer**|0x2|Indicates that the field is deferred-that is, the field values are not retrieved from the data source with the whole record, but only when you explicitly access them.|  
|**adFldNegativeScale**|0x4000|Indicates that the field represents a numeric value from a column that supports negative scale values. The scale is specified by the [NumericScale](../../../ado/reference/ado-api/numericscale-property-ado.md) property.|  
|**adFldRowID**|0x100|Indicates that the field contains a persistent row identifier that cannot be written to and has no meaningful value except to identify the row (such as a record number, unique identifier, and so forth).|  
|**adFldRowVersion**|0x200|Indicates that the field contains some kind of time or date stamp used to track updates.|  
|**adFldUnknownUpdatable**|0x8|Indicates that the provider cannot determine if you can write to the field.|  
|**adFldUnspecified**|-1 0xFFFFFFFF|Indicates that the provider does not specify the field attributes.|  
|**adFldUpdatable**|0x4|Indicates that you can write to the field.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.FieldAttribute.CACHEDEFERRED|  
|AdoEnums.FieldAttribute.FIXED|  
|AdoEnums.FieldAttribute.ISNULLABLE|  
|AdoEnums.FieldAttribute.LONG|  
|AdoEnums.FieldAttribute.MAYBENULL|  
|AdoEnums.FieldAttribute.MAYDEFER|  
|AdoEnums.FieldAttribute.NEGATIVESCALE|  
|AdoEnums.FieldAttribute.ROWID|  
|AdoEnums.FieldAttribute.ROWVERSION|  
|AdoEnums.FieldAttribute.UNKNOWNUPDATABLE|  
|AdoEnums.FieldAttribute.UNSPECIFIED|  
|AdoEnums.FieldAttribute.UPDATABLE|  
  
## Applies To  

:::row:::
    :::column:::
        [Append Method (ADO)](../../../ado/reference/ado-api/append-method-ado.md)  
    :::column-end:::
    :::column:::
        [Attributes Property (ADO)](../../../ado/reference/ado-api/attributes-property-ado.md)  
    :::column-end:::
:::row-end:::
