---
title: "OLE DB for OLAP Schema Rowsets | Microsoft Docs"
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
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "schema rowsets [Analysis Services], OLE DB for OLAP"
  - "OLE DB for OLAP schema rowsets"
  - "schema rowsets [OLE DB for OLAP]"
  - "rowsets [Analysis Services], OLE DB for OLAP"
ms.assetid: 5fad3ecc-407c-4148-862e-ea6119cc7480
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# OLE DB for OLAP Schema Rowsets
  The [!INCLUDE[msCoName](../../../includes/msconame-md.md)] XML for Analysis (XMLA) provider supports the following OLE DB for OLAP schema rowsets.  
  
> [!NOTE]  
>  To check whether a particular data source provider supports a rowset, use the **DISCOVER_ENUMERATIONS** rowset with the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method.  
  
 You can also find detailed information about these rowsets by searching for the topic, "OLAP Schema Rowsets," in the MSDN Library at this [Microsoft Web site](http://go.microsoft.com/fwlink/?LinkId=15426).  
  
## In This Section  
  
|Schema Rowset<sup>1</sup>|Description|  
|-------------------------------|-----------------|  
|[DISCOVER_INSTANCES Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/discover-instances-rowset.md)|Describes the instances on the server.|  
|[DISCOVER_KEYWORDS Rowset &#40;OLE DB for OLAP&#41;](../../../analysis-services/schema-rowsets/ole-db-olap/discover-keywords-rowset-ole-db-for-olap.md)|Enumerates a list of words reserved by the provider.|  
|[MDSCHEMA_ACTIONS Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-actions-rowset.md)|Describes the actions that may be available to the client application.|  
|[MDSCHEMA_CUBES Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-cubes-rowset.md)|Describes the structure of cubes within a database.|  
|[MDSCHEMA_DIMENSIONS Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-dimensions-rowset.md)|Describes the shared and private dimensions within a database.|  
|[MDSCHEMA_FUNCTIONS Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-functions-rowset.md)|Describes the functions that are available to client applications connected to the database.|  
|[MDSCHEMA_HIERARCHIES Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-hierarchies-rowset.md)|Describes each hierarchy that is contained in a particular dimension.|  
|[MDSCHEMA_INPUT_DATASOURCES Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-input-datasources-rowset.md)|Describes the data sources defined within the database.|  
|[MDSCHEMA_KPIS Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-kpis-rowset.md)|Describes the key performance indicators (KPIs) within a database.|  
|[MDSCHEMA_LEVELS Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-levels-rowset.md)|Describes each level within a particular hierarchy.|  
|[MDSCHEMA_MEASUREGROUP_DIMENSIONS Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-measuregroup-dimensions-rowset.md)|Enumerates the dimensions of measure groups.|  
|[MDSCHEMA_MEASUREGROUPS Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-measuregroups-rowset.md)|Describes the measure groups within a database.|  
|[MDSCHEMA_MEASURES Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-measures-rowset.md)|Describes each measure within in a cube.|  
|[MDSCHEMA_MEMBERS Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-members-rowset.md)|Describes the members within a database.|  
|[MDSCHEMA_PROPERTIES Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-properties-rowset.md)|Describes the properties of members within in a database.|  
|[MDSCHEMA_SETS Rowset](../../../analysis-services/schema-rowsets/ole-db-olap/mdschema-sets-rowset.md)|Describes any sets that are currently defined in a database, including session-scoped sets.|  
  
 <sup>1</sup> All the schema rowsets listed here are supported by the MSOLAP data source provider for the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] XMLA provider.  
  
## See Also  
 [DISCOVER_ENUMERATORS Rowset](../../../analysis-services/schema-rowsets/xml/discover-enumerators-rowset.md)   
 [Analysis Services Schema Rowsets](../../../analysis-services/schema-rowsets/analysis-services-schema-rowsets.md)  
  
  