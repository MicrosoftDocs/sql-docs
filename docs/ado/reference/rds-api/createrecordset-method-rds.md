---
title: "CreateRecordset Method (RDS) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "DataControl::CreateRecordset"
  - "RDS.DataControl::CreateRecordset"
  - "CreateRecordset"
  - "RDSServer.DataFactory::CreateRecordset"
  - "DataFactory::CreateRecordset"
helpviewer_keywords: 
  - "CreateRecordset method [RDS]"
ms.assetid: 6840b1e5-c04d-4d3e-9dcc-42128c83492f
author: MightyPen
ms.author: genemi
manager: craigg
---
# CreateRecordset Method (RDS)
Creates an empty, disconnected [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
object.CreateRecordset(ColumnInfos)  
```  
  
#### Parameters  
 *Object*  
 An object variable that represents an [RDSServer.DataFactory](../../../ado/reference/rds-api/datafactory-object-rdsserver.md) or [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object.  
  
 *ColumnsInfos*  
 A **Variant** array of attributes that defines each column in the **Recordset** created. Each column definition contains an array of four required attributes and one optional attribute.  
  
|Attribute|Description|  
|---------------|-----------------|  
|Name|Name of the column header.|  
|Type|Integer of the data type.|  
|Size|Integer of the width in characters, regardless of data type.|  
|Nullability|Boolean value.|  
|Scale (Optional)|This optional attribute defines the scale for numeric fields. If this value is not specified, numeric values will be truncated to a scale of three. Precision is not affected, but the number of digits following the decimal point will be truncated to three.|  
  
 The set of column arrays is then grouped into an array, which defines the **Recordset**.  
  
## Remarks  
 The server-side business object can populate the resulting **Recordset** with data from a non-OLE DB data provider, such as an operating system file containing stock quotes.  
  
 The following table lists the [DataTypeEnum](../../../ado/reference/ado-api/datatypeenum.md) values supported by the **CreateRecordset** method. The number listed is the reference number used to define fields.  
  
 Each of the data types is either fixed length or variable length. Fixed-length types should be defined with a size of -1, because the size is predetermined and a size definition is still required. Variable-length data types allow a size from 1 to 32767.  
  
 For some of the variable data types, the type can be coerced to the type noted in the Substitution column. You will not see the substitutions until after the **Recordset** is created and filled. Then you can check for the actual data type, if necessary.  
  
|Length|Constant|Number|Substitution|  
|------------|--------------|------------|------------------|  
|Fixed|**adTinyInt**|16||  
|Fixed|**adSmallInt**|2||  
|Fixed|**adInteger**|3||  
|Fixed|**adBigInt**|20||  
|Fixed|**adUnsignedTinyInt**|17||  
|Fixed|**adUnsignedSmallInt**|18||  
|Fixed|**adUnsignedInt**|19||  
|Fixed|**adUnsignedBigInt**|21||  
|Fixed|**adSingle**|4||  
|Fixed|**adDouble**|5||  
|Fixed|**adCurrency**|6||  
|Fixed|**adDecimal**|14||  
|Fixed|**adNumeric**|131||  
|Fixed|**adBoolean**|11||  
|Fixed|**adError**|10||  
|Fixed|**adGuid**|72||  
|Fixed|**adDate**|7||  
|Fixed|**adDBDate**|133||  
|Fixed|**adDBTime**|134||  
|Fixed|**adDBTimestamp**|135|7|  
|Variable|**adBSTR**|8|130|  
|Variable|**adChar**|129|200|  
|Variable|**adVarChar**|200||  
|Variable|**adLongVarChar**|201|200|  
|Variable|**adWChar**|130||  
|Variable|**adVarWChar**|202|130|  
|Variable|**adLongVarWChar**|203|130|  
|Variable|**adBinary**|128||  
|Variable|**adVarBinary**|204||  
|Variable|**adLongVarBinary**|205|204|  
  
## Applies To  
  
|||  
|-|-|  
|[DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)|[DataFactory Object (RDSServer)](../../../ado/reference/rds-api/datafactory-object-rdsserver.md)|  
  
## See Also  
 [CreateRecordset Method Example (VB)](../../../ado/reference/ado-api/createrecordset-method-example-vb.md)   
 [CreateRecordset Method Example (VBScript)](../../../ado/reference/rds-api/createrecordset-method-example-vbscript.md)   
 [CreateObject Method (RDS)](../../../ado/reference/rds-api/createobject-method-rds.md)



