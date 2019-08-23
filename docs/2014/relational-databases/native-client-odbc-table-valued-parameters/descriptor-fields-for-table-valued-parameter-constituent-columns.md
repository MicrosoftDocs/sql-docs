---
title: "Descriptor Fields for Table-Valued Parameter Constituent Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "table-valued parameters (ODBC), descriptor fields for constituent columns"
ms.assetid: 944b3968-fd47-4847-98d6-b87e8ef2acdc
author: MightyPen
ms.author: genemi
manager: craigg
---
# Descriptor Fields for Table-Valued Parameter Constituent Columns
  The table-valued parameter descriptor fields described in this section are manipulated by using [SQLSetDescField](../native-client-odbc-api/sqlsetdescfield.md) and [SQLSetDescField](../native-client-odbc-api/sqlsetdescfield.md) with the handle for the implementation parameter descriptor (IPD).  
  
## Remarks  
 SQL_DESC_AUTO_UNIQUE_VALUE is used for table-valued parameters as well as other features.  
  
|Attribute name|Type|Description|  
|--------------------|----------|-----------------|  
|SQL_DESC_AUTO_UNIQUE_VALUE|SQLINTEGER|SQL_TRUE indicates that this column is an identity column.<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can use this information to optimize performance, but applications are not required to set it for identity columns.|  
  
 The following attributes are added to all parameter types in the application parameter descriptor (APD) and implementation parameter descriptor (IPD):  
  
|Attribute name|Type|Description|  
|--------------------|----------|-----------------|  
|SQL_CA_SS_COLUMN_COMPUTED|SQLSMALLINT|SQL_TRUE indicates that this column is computed.<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can use this information to optimize performance, but applications are not required to set it for computed columns.<br /><br /> This attribute is ignored for bindings that are not table-valued parameter columns.|  
|SQL_CA_SS_COLUMN_IN_UNIQUE_KEY|SQLSMALLINT|SQL_TRUE indicates that a table-valued parameter column participates in a unique key. This can result in better query performance. This attribute is ignored for bindings that are not table-valued parameter columns.|  
|SQL_CA_SS_COLUMN_SORT_ORDER|SQLSMALLINT|Indicates the sort order of a table-valued parameter column. This can result in better query performance. This attribute is ignored for bindings that are not table-valued parameter columns. The possible values are the following:<br /><br /> -   SQL_SS_ASCENDING_ORDER<br />-   SQL_SS_DESCENDING_ORDER<br />-   SQL_SS_ORDER_UNSPECIFIED<br /><br /> Values other than SQL_SS_ASCENDING_ORDER and SQL_SS_DESCENDING_ORDER generate an error with SQLSTATE HY024 and message 'Invalid attribute value' and are treated as SQL_SS_ORDER_UNSPECIFIED, which is the default value for this attribute.|  
|SQL_CA_SS_COLUMN_SORT_ORDINAL|SQLSMALLINT|Indicates the ordinal of a table-valued parameter column in the set of columns that define the overall ordering for a table-valued parameter. This can result in better query performance. This attribute is ignored for bindings that are not table-valued parameter columns. Sort ordinals start at 1. A value of 0, the default, indicates that a table-valued parameter column does not have column ordering.|  
|SQL_CA_SS_COLUMN_HAS_DEFAULT_VALUE|SQLSMALLINT|Indicates whether all rows in the table-valued parameter will have the default value for this column. For table-valued parameters, it is not possible to select the default value on a row-by-row basis. A value of SQL_FALSE indicates that rows will have non-default values. This is the default. A value of SQL_TRUE indicates that this column will have default values for all rows.<br /><br /> If set to SQL_TRUE, no data will be sent to the server.<br /><br /> This field can also be used with identity or computed columns if the column values are not required for server processing.|  
  
 These attributes are only valid for table-valued parameter columns. They are ignored for other parameters.  
  
 If SQL_CA_SS_COL_HAS_DEFAULT_VALUE is set for a table-valued parameter column, SQL_DESC_DATA_PTR for that column must be a null pointer. Otherwise, SQLExecute or SQLExecDirect will return SQL_ERROR. A diagnostic record will be generated with SQLSTATE=07S01 and the message "Invalid use of default parameter for parameter \<p>, column \<c>", where \<p> is the parameter ordinal and \<c> is the column ordinal.  
  
## See Also  
 [Table-Valued Parameters &#40;ODBC&#41;](table-valued-parameters-odbc.md)  
  
  
