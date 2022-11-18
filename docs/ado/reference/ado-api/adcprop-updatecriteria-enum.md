---
title: "ADCPROP_UPDATECRITERIA_ENUM"
description: "ADCPROP_UPDATECRITERIA_ENUM"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ADCPROP_UPDATECRITERIA_ENUM"
helpviewer_keywords:
  - "ADCPROP_UPDATECRITERIA_ENUM [ADO]"
apitype: "COM"
---
# ADCPROP_UPDATECRITERIA_ENUM
Specifies which fields can be used to detect conflicts during an optimistic update of a row of the data source with a [Recordset](./recordset-object-ado.md) object.  
  
 Use these constants with the **Recordset** "**Update Criteria**" dynamic property, which is referenced in the [ADO Dynamic Property Index](./ado-dynamic-property-index.md) and documented in the [Microsoft Cursor Service for OLE DB](../../guide/appendixes/microsoft-cursor-service-for-ole-db-ado-service-component.md) documentation.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adCriteriaAllCols**|1|Detects conflicts if any column of the data source row has been changed.|  
|**adCriteriaKey**|0|Detects conflicts if the key column of the data source row has been changed, which means that the row has been deleted.|  
|**adCriteriaTimeStamp**|3|Detects conflicts if the timestamp of the data source row has been changed, which means the row has been accessed after the **Recordset** was obtained.|  
|**adCriteriaUpdCols**|2|Detects conflicts if any of the columns of the data source row that correspond to updated fields of the **Recordset** have been changed.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.AdcPropUpdateCriteria.ALLCOLS|  
|AdoEnums.AdcPropUpdateCriteria.KEY|  
|AdoEnums.AdcPropUpdateCriteria.TIMESTAMP|  
|AdoEnums.AdcPropUpdateCriteria.UPDCOLS|