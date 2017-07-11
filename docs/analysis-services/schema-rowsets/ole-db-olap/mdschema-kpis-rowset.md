---
title: "MDSCHEMA_KPIS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "MDSCHEMA_KPIS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "MDSCHEMA_KPIS rowset"
ms.assetid: 40fb5112-6a90-4455-82b3-8b6322490222
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDSCHEMA_KPIS Rowset
  Describes the key performance indicators (KPIs) within a database.  
  
## Rowset Columns  
 The **MDSCHEMA_KPIS** rowset contains the following columns.  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|The source database.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Not supported.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|The parent cube for the KPI.|  
|**MEASUREGROUP_NAME**|**DBTYPE_WSTR**|The associated measure group for the KPI.<br /><br /> You can use this column to determine the dimensionality of the KPI. If "**\<NULL>**", the KPI will be dimensioned by all measure groups.<br /><br /> The default value is "**\<NULL>**".|  
|**KPI_NAME**|**DBTYPE_WSTR**|The name of the KPI.|  
|**KPI_CAPTION**|**DBTYPE_WSTR**|A label or caption associated with the KPI. Used primarily for display purposes. If a caption does not exist, **KPI_NAME** is returned.|  
|**KPI_DESCRIPTION**|**DBTYPE_WSTR**|A human-readable description of the KPI.|  
|**KPI_DISPLAY_FOLDER**|**DBTYPE_WSTR**|A string that identifies the path of the display folder that the client application uses to show the member. The folder level separator is defined by the client application. For the tools and clients supplied by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], the backslash (\\) is the level separator. To provide multiple display folders, use a semicolon (;) to separate the folders.|  
|**KPI_VALUE**|**DBTYPE_WSTR**|The unique name of the member in the measures dimension for the KPI Value.|  
|**KPI_GOAL**|**DBTYPE_WSTR**|The unique name of the member in the measures dimension for the KPI Goal.<br /><br /> Returns **NULL** if there is no Goal defined.|  
|**KPI_STATUS**|**DBTYPE_WSTR**|The unique name of the member in the measures dimension for the KPI Status.<br /><br /> Returns **NULL** if there is no Status defined.|  
|**KPI_TREND**|**DBTYPE_WSTR**|The unique name of the member in the measures dimension for the KPI Trend.<br /><br /> Returns **NULL** if there is no Trend defined.|  
|**KPI_STATUS_GRAPHIC**|**DBTYPE_WSTR**|The default graphical representation of the KPI.|  
|**KPI_TREND_GRAPHIC**|**DBTYPE_WSTR**|The default graphical representation of the KPI.|  
|**KPI_WEIGHT**|**DBTYPE_WSTR**|The unique name of the member in the measures dimension for the KPI Weight.|  
|**KPI_CURRENT_TIME_MEMBER**|**DBTYPE_WSTR**|The unique name of the member in the time dimension that defines the temporal context of the KPI.<br /><br /> Returns **NULL** if there is no Time member defined.|  
|**KPI_PARENT_KPI_NAME**|**DBTYPE_WSTR**|The name of the parent KPI.|  
|**SCOPE**|**DBTYPE_I4**|The scope of the KPI. The KPI can be a session KPI or global KPI.<br /><br /> This column can have one of the following values:<br /><br /> MDKPI_SCOPE_GLOBAL=1<br /><br /> MDKPI_SCOPE_SESSION=2|  
|**ANNOTATIONS**|**DBTYPE_WSTR**|(Optional) A set of notes, in XML format.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **MDSCHEMA_KPIS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|Optional.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Optional.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**KPI_NAME**|**DBTYPE_WSTR**|Optional.|  
|**CUBE_SOURCE**|**DBTYPE_UI2**|(Optional) Default restriction is a value of **1**. A bitmap with one of the following valid values:<br /><br /> **1** CUBE<br /><br /> **2** DIMENSION|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  