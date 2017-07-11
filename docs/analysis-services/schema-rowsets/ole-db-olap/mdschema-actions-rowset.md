---
title: "MDSCHEMA_ACTIONS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "MDSCHEMA_ACTIONS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "MDSCHEMA_ACTIONS rowset"
ms.assetid: f73081f8-ac51-4286-b46e-2b34e792c3e0
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDSCHEMA_ACTIONS Rowset
  Describes the actions that may be available to the client application.  
  
## Rowset Columns  
 The **MDSCHEMA_ACTIONS** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**||The name of the database.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**||Not supported. Always contains **VT_NULL**.|  
|**CUBE_NAME**|**DBTYPE_WSTR**||The name of the cube to which this action belongs.|  
|**ACTION_NAME**|**DBTYPE_WSTR**||The name of this action.|  
|**ACTION_TYPE**|**DBTYPE_I4**||A bitmap that is used to specify the action's triggering method. The Msmd.h file defines the following bit value constants for this bitmap:<br /><br /> **MDACTION_TYPE_URL** (**0x01**)<br /><br /> **MDACTION_TYPE_HTML** (**0x02**)<br /><br /> **MDACTION_TYPE_STATEMENT** (**0x04**)<br /><br /> **MDACTION_TYPE_DATASET** (**0x08**)<br /><br /> **MDACTION_TYPE_ROWSET** (**0x10**)<br /><br /> **MDACTION_TYPE_COMMANDLINE** (**0x20**)<br /><br /> **MDACTION_TYPE_PROPRIETARY** (**0x40**)<br /><br /> **MDACTION_TYPE_REPORT** (**0x80**)<br /><br /> **MDACTION_TYPE_DRILLTHROUGH** (**0x100**)|  
|**COORDINATE**|**DBTYPE_WSTR**||A Multidimensional Expressions (MDX) expression that specifies an object or a coordinate in the multidimensional space in which the action is performed. It is the responsibility of the client application to provide the value of this restriction column.<br /><br /> The **CORDINATE** must resolve to the object specified in **COORDINATE_TYPE**.|  
|**COORDINATE_TYPE**|**DBTYPE_I4**||A bitmap that specifies how the **COORDINATE** restriction column is interpreted. The Msmd.h file defines the following bit value constants for this bitmap:<br /><br /> **MDACTION_COORDINATE_CUBE** (**1**)<br /><br /> **MDACTION_COORDINATE_DIMENSION** (**2**): refers to the dimensions hierarchies.<br /><br /> **MDACTION_COORDINATE_LEVEL** (**3**)<br /><br /> **MDACTION_COORDINATE_MEMBER** (**4**)<br /><br /> **MDACTION_COORDINATE_SET** (**5**)<br /><br /> **MDACTION_COORDINATE_CELL** (**6**)|  
|**ACTION_CAPTION**|**DBTYPE_WSTR**||The action name if no caption was specified and no translations were specified in the DDL.<br /><br /> If a caption or translations were specified, and **CaptionIsMDX** is false, one of the following strings:<br /><br /> -The translation for the appropriate language.<br /><br /> -The specified caption if no translation was found for the specified language.<br /><br /> -The action name if no translation was found and the caption was not specified in DDL.<br /><br /> If a caption or translations were specified, and **CaptionIsMDX** is true, the string resulting from finding the appropriate translation for the specified language or the specified translation in the DDL caption, and calculating the formula to create the string.<br /><br /> If the action was specified in MDX Script, there are no translations and the caption is always treated as MDX expression.|  
|**DESCRIPTION**|**DBTYPE_WSTR**||A user-friendly description of the action.|  
|**CONTENT**|**DBTYPE_WSTR**||The expression or content of the action that is to be run.|  
|**APPLICATION**|**DBTYPE_WSTR**||The name of the application that is to be used to run the action.|  
|**INVOCATION**|**DBTYPE_I4**||Information about how the action should be invoked:<br /><br /> **MDACTION_INVOCATION_INTERACTIVE** (**1**) indicates a regular action used during normal operations. This is the default value for this column.<br /><br /> **MDACTION_INVOCATION_ON_OPEN** (**2**) indicates that the action should be performed when the cube is first opened.<br /><br /> **MDACTION_INVOCATION_BATCH** (**4**) indicates that the action is performed as part of a batch operation or [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] task.<br /><br /> <br /><br /> Note that these enumeration values are defined in the file, Msmd.h.|  
  
 The rowset is sorted on **CATALOG_NAME**, **SCHEMA_NAME**, **CUBE_NAME**, **ACTION_NAME**.  
  
> [!NOTE]  
>  Actions of **MDACTION_TYPE_PROPRIETARY** type must provide a value for the **APPLICATION** column.  
  
## Restriction Columns  
 The **MDSCHEMA_ACTIONS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|Optional|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Optional|  
|**CUBE_NAME**|**DBTYPE_WSTR**|Mandatory|  
|**ACTION_NAME**|**DBTYPE_WSTR**|Optional|  
|**ACTION_TYPE**|**DBTYPE_I4**|Optional|  
|**COORDINATE**|**DBTYPE_WSTR**|Mandatory|  
|**COORDINATE_TYPE**|**DBTYPE_I4**|Mandatory|  
|**INVOCATION**|**DBTYPE_I4**|(Optional) The **INVOCATION** restriction column defaults to the value of **MDACTION_INVOCATION_INTERACTIVE**. To retrieve all actions, use the **MDACTION_INVOCATION_ALL** value in the **INVOCATION** restriction column.|  
|**CUBE_SOURCE**|**DBTYPE_UI2**|(Optional) Default restriction is a value of 1. A bitmap with one of the following valid values:<br /><br /> 1 CUBE<br /><br /> 2 DIMENSION|  
  
> [!IMPORTANT]  
>  The **INVOCATION** restriction column has a default value of **MDACTION_INVOCATION_INTERACTIVE**. Any schema rowset that does not explicitly specify a value for this column contains only rows with this value. If you want the rowset to contain the entire set of actions, use the **MDACTION_INVOCATION_ALL** constant in the **INVOCATION** restriction column.  
  
 Client applications can define more than one **ACTION_TYPE** by using the OR operator.  
  
## Remarks  
 The following table lists the valid **COORDINATE** and **COORDINATE_TYPE** combinations.  
  
|COORDINATE object type|COORDINATE_TYPE|  
|----------------------------|----------------------|  
|**Cube**|**MDACTION_COORDINATE_CUBE**|  
|**Dimension**|**MDACTION_COORDINATE_DIMENSION**<br /><br /> **MDACTION_COORDINATE_LEVEL**<br /><br /> **MDACTION_COORDINATE_MEMBER**<br /><br /> **MDACTION_COORDINATE_SET**<br /><br /> **MDACTION_COORDINATE_CELL**|  
|**Hierarchy**|**MDACTION_COORDINATE_DIMENSION**|  
|**Level**|**MDACTION_COORDINATE_LEVEL**|  
|**Member**|**MDACTION_COORDINATE_MEMBER**|  
|**Set**|**MDACTION_COORDINATE_SET**|  
|**cell**|**MDACTION_COORDINATE_CELL**|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  