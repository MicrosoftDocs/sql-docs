---
title: "DataControl Error Codes | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords:
  - "errors [ADO], DataControl"
  - "DataControl errors [ADO]"
ms.assetid: 293df9d5-e1a2-406d-9107-07bf7cdc6f96
author: MightyPen
ms.author: genemi
manager: craigg
---
# DataControl Object Error Codes
The following table lists the [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object error codes. The positive decimal translation of the low two bytes, the negative decimal translation of the full error code, and the hexadecimal values are shown.

|RDS.DataControl error codes|Number|Description|
|---------------------------------|------------|-----------------|
|**IDS_AsyncPending**|4107 -2146824175 0x800A1011|Operation cannot be performed while async operation is pending.|
|**IDS_BadInlineTablegram**|4105 -2146824183 0x800A1009|Bad inline tablegram.|
|**IDS_CantConnect**|4099 -2146824189 0x800A1003|Cannot connect to server.|
|**IDS_CantCreateObject**|4100 -2146824188 0x800A1004|Business object cannot be created.|
|**IDS_CantFindDataspace**|4102 -2146824186 0x800A1006|Dataspace property is not valid.|
|**IDS_CantInvokeMethod**|4101 -2146824187 0x800A1005|Method cannot be invoked on business object.|
|**IDS_CrossDomainWarning**|4112 -2146824170 0x800A1016|This page accesses data on another domain. Do you want to allow this? To avoid this message in Internet Explorer, you can add a secure Web site to your Trusted Sites zone on the **Security** tab of the **Internet Options** dialog box.|
|**IDS_InvalidADCClientVersion**|4106 -2146824176 0x800A1010|Invalid RDS Client Version - Client is newer than server.|
|**IDS_INVALIDARG**|5376 -2147019520 0x80071500|One or more arguments are invalid.|
|**IDS_InvalidBindings**|4097 -2146824191 0x800A1001|Error in bindings property.|
|**IDS_InvalidParam**|4110 -2146824172 0x800A1014|One or more arguments are invalid.|
|**IDS_NOINTERFACE**|5377 -2147019519 0x80071501|No such interface is supported.|
|**IDS_NotReentrant**|4111 -2146824171 0x800A1015|Request cannot be executed while the event handler is still processing.|
|**IDS_ObjectNotSafe**|4103 -2146824185 0x800A1007|Safety settings on this computer prohibit creation of business object.|
|**IDS_RecordsetNotOpen**|4109 -2146824173 0x800A1013|**Recordset** is not open.|
|**IDS_ResetInvalidField**|4108 -2146824174 0x800A1012|Column specified in **SortColumn** or **FilterColumn** does not exist.|
|**IDS_RowsetNotUpdateable**|4104 -2146824184 0x800A1008|Rowset not updateable.|
|**IDS_UnexpectedError**|4351 -2146823937 0x800A10FF|Unexpected error.|
|**IDS_UpdatesFailed**|4098 -2146824190 0x800A1002|Unable to update database.|
|**IDS_URLMONNotFound**|4119 -2146824169 0x800A1017|DataControl **URL** property requires the system file Urlmon.dll, which cannot be found.|

## See Also
 [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)
