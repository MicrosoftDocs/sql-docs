---
title: "DBSCHEMA_PROVIDER_TYPES Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DBSCHEMA_PROVIDER_TYPES"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DBSCHEMA_PROVIDER_TYPES rowset"
ms.assetid: 255e01ba-53a9-478d-9b86-45faba76710e
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DBSCHEMA_PROVIDER_TYPES Rowset
  Identifies the (base) data types supported by the data provider.  
  
## Rowset Columns  
 The **DBSCHEMA_PROVIDER_TYPES** rowset contains the following columns.  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|**TYPE_NAME**|**DBTYPE_WSTR**|The provider-specific data type name.|  
|**DATA_TYPE**|**DBTYPE_UI2**|The indicator of the data type.|  
|**COLUMN_SIZE**|**DBTYPE_UI4**|The length of a non-numeric column or parameter that refers to either the maximum or the length defined for this type by the provider. For character data, this is the maximum or defined length in characters. For DateTime data types, this is the length of the string representation (assuming the maximum allowed precision of the fractional seconds component).<br /><br /> If the data type is numeric, this is the upper bound on the maximum precision of the data type.|  
|**LITERAL_PREFIX**|**DBTYPE_WSTR**|The character or characters used to prefix a literal of this type in a text command.|  
|**LITERAL_SUFFIX**|**DBTYPE_WSTR**|The character or characters used to suffix a literal of this type in a text command.|  
|**CREATE_PARAMS**|**DBTYPE_WSTR**|The creation parameters specified by the consumer when creating a column of this data type. For example, the SQL data type, **DECIMAL,** needs a precision and a scale. In this case, the creation parameters might be the string "precision,scale". In a text command to create a **DECIMAL** column with a precision of 10 and a scale of 2, the value of the **TYPE_NAME** column might be **DECIMAL()** and the complete type specification would be **DECIMAL(10,2)**.<br /><br /> The creation parameters appear as a comma-separated list of values, in the order they are to be supplied and with no surrounding parentheses. If a creation parameter is length, maximum length, precision, scale, seed, or increment, use "length", "max length", "precision", "scale", "seed", and "increment", respectively. If the creation parameter is some other value, the provider determines what text is to be used to describe the creation parameter.<br /><br /> If the data type requires creation parameters, "()" usually appears in the type name. This indicates the position at which to insert the creation parameters. If the type name does not include "()", the creation parameters are enclosed in parentheses and appended to the data type name.|  
|**IS_NULLABLE**|**DBTYPE_BOOL**|A Boolean that indicates whether the data type is nullable.<br /><br /> **VARIANT_TRUE** indicates that the data type is nullable.<br /><br /> **VARIANT_FALSE** indicates that the data type is not nullable.<br /><br /> **NULL**— indicates that it is not known whether the data type is nullable.|  
|**CASE_SENSITIVE**|**DBTYPE_BOOL**|A Boolean that indicates whether the data type is a characters type and case-sensitive.<br /><br /> **VARIANT_TRUE** indicates that the data type is a character type and is case-sensitive.<br /><br /> **VARIANT_FALSE** indicates that the data type is not a character type or is not case-sensitive.|  
|**SEARCHABLE**|**DBTYPE_UI4**|An integer indicating how the data type can be used in searches if the provider supports **ICommandText**; otherwise, **NULL**. This column can have the following values:<br /><br /> **DB_UNSEARCHABLE** indicates that the data type cannot be used in a **WHERE** clause.<br /><br /> **DB_LIKE_ONLY** indicates that the data type can be used in a **WHERE** clause only with the **LIKE** predicate.<br /><br /> **DB_ALL_EXCEPT_LIKE** indicates that the data type can be used in a **WHERE** clause with all comparison operators except **LIKE**.<br /><br /> **DB_SEARCHABLE** indicates that the data type can be used in a **WHERE** clause with any comparison operator.|  
|**UNSIGNED_ATTRIBUTE**|**DBTYPE_BOOL**|A Boolean that indicates whether the data type is unsigned.<br /><br /> **VARIANT_TRUE** indicates that the data type is unsigned.<br /><br /> **VARIANT_FALSE** indicates that the data type is signed.<br /><br /> **NULL** indicates that this is not applicable to the data type.|  
|**FIXED_PREC_SCALE**|**DBTYPE_BOOL**|A Boolean that indicates whether the data type has a fixed precision and scale.<br /><br /> **VARIANT_TRUE** indicates that the data type has a fixed precision and scale.<br /><br /> **VARIANT_FALSE** indicates that the data type does not have a fixed precision and scale.|  
|**AUTO_UNIQUE_VALUE**|**DBTYPE_BOOL**|A Boolean that indicates whether the data type is autoincrementing.<br /><br /> **VARIANT_TRUE** indicates that values of this type can be autoincrementing.<br /><br /> **VARIANT_FALSE** indicates that values of this type cannot be autoincrementing.<br /><br /> If this value is **VARIANT_TRUE**, whether or not a column of this type is always autoincrementing depends on the provider's **DBPROP_COL_AUTOINCREMENT** column property. If the **DBPROP_COL_AUTOINCREMENT** property is read/write, whether or not a column of this type is autoincrementing depends on the setting of the **DBPROP_COL_AUTOINCREMENT** property. If **DBPROP_COL_AUTOINCREMENT** is a read-only property, either all or none of the columns of this type are autoincrementing.|  
|**LOCAL_TYPE_NAME**|**DBTYPE_WSTR**|The localized version of **TYPE_NAME**. **NULL** is returned if a localized name is not supported by the data provider.|  
|**MINIMUM_SCALE**|**DBTYPE_I2**|If the type indicator is **DBTYPE_VARNUMERIC**, **DBTYPE_DECIMAL**, or **DBTYPE_NUMERIC**, the minimum number of digits allowed to the right of the decimal point. Otherwise, **NULL**.|  
|**MAXIMUM_SCALE**|**DBTYPE_I2**|The maximum number of digits allowed to the right of the decimal point if the type indicator is **DBTYPE_VARNUMERIC**, **DBTYPE_DECIMAL**, or **DBTYPE_NUMERIC**; otherwise, N**U**LL.|  
|**GUID**|**DBTYPE_GUID**|(Intended for future use) The **GUID** of the type, if the type is described in a type library. Otherwise, **NULL**.|  
|**TYPELIB**|**DBTYPE_WSTR**|(Intended for future use) The type library containing the description of the type, if the type is described in a type library. Otherwise, NULL.|  
|**VERSION**|**DBTYPE_WSTR**|(Intended for future use) The version of the type definition. Providers might want to version type definitions. Different providers might use different versioning schemes, such as a timestamp or number (integer or float). **NULL** if not supported.|  
|**IS_LONG**|**DBTYPE_BOOL**|A Boolean that indicates whether the data type is a binary large object (BLOB) and has very long data.<br /><br /> **VARIANT_TRUE** indicates that the data type is a **BLOB** that contains very long data; the definition of very long data is provider-specific.<br /><br /> **VARIANT_FALSE** indicates that the data type is a **BLOB** that does not contain very long data or is not a **BLOB**.<br /><br /> This value determines the setting of the **DBCOLUMNFLAGS_ISLONG** flag returned by **GetColumnInfo** in **IColumnsInfo** and **GetParameterInfo** in **ICommandWithParameters**.|  
|**BEST_MATCH**|**DBTYPE_BOOL**|A Boolean that indicates whether the data type is a best match.<br /><br /> **VARIANT_TRUE** indicates that the data type is the best match between all data types in the data store and the OLE DB data type indicated by the value in the **DATA_TYPE** column.<br /><br /> **VARIANT_FALSE** indicates that the data type is not the best match.<br /><br /> For each set of rows in which the value of the **DATA_TYPE** column is the same, the **BEST_MATCH** column is set to **VARIANT_TRUE** in only one row.|  
|**IS_FIXEDLENGTH**|**DBTYPE_BOOL**|A Boolean that indicates whether the column is fixed in length.<br /><br /> **VARIANT_TRUE** indicates that columns of this type created by the data definition language (DDL) will be of fixed length.<br /><br /> **VARIANT_FALSE** indicates that columns of this type created by the DDL will be of variable length.<br /><br /> If the field is **NULL**, it is not known whether the provider will map this field with a fixed-length or variable-length column.|  
  
 The rowset is sorted on **DATA_TYPE**.  
  
## Restriction Columns  
 The **DBSCHEMA_PROVIDER_TYPES** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|  
|-----------------|--------------------|  
|**DATA_TYPE**|**DBTYPE_UI2**|  
|**BEST_MATCH**|**DBTYPE_BOOL**|  
  
## See Also  
 [OLE DB Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db/ole-db-schema-rowsets.md)  
  
  