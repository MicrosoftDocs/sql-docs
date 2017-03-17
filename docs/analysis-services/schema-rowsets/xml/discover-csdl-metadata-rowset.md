---
title: "DISCOVER_CSDL_METADATA Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
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
ms.assetid: a2d3cffd-a2c4-411c-b244-9e41ebe30939
caps.latest.revision: 22
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_CSDL_METADATA Rowset
  Returns information about an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] data model (either tabular or multidimensional), providing the definition of the model in the CSDLBI format (Conceptual Schema Definition Language with BI annotations). CSDLBI is based on CSDL, an XML schema used by the Entity Data Framework that is used for communication between an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] server and the [!INCLUDE[ssCrescent](../../../includes/sscrescent-md.md)] client. The Business Intelligence (BI) annotations provide additional metadata about tabular models and the objects in them. For more information about tabular data models, see [CSDL Annotations for Business Intelligence &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/csdl-annotations-for-business-intelligence-csdlbi.md).  
  
 The security context of the command affects the rowset that is returned. Read permissions on the Analysis Services instance are required to obtain the CSDL definition from the server.  
  
 The language identifier of the client that issues the rowset request is included in the connection string for the command, and affects the language displayed in several properties that are returned as part of the rowset.  For information about properties and description that may be affected by the language identifier, see the Remarks section.  
  
## Rowset Columns  
 The **DISCOVER_CSDL_METADATA** rowset contains the following columns.  
  
|**Column name**|**Type indicator**|**Restriction**|**Description**|  
|---------------------|------------------------|---------------------|---------------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|Yes|Specifies the name of the database for which the CSDLBI description is requested. If omitted, the current database is used.<br /><br /> This restriction is required for all model types.|  
|**PERSPECTIVE_ID**|**DBTYPE_WSTR**|Yes|Specifies the ID of a perspective that has been defined on the model specified by CATALOG_NAME.<br /><br /> An optional restriction. Applies to all model types.|  
|**PERSPECTIVE_NAME**|**DBTYPE_WSTR**|Yes|Specifies the name of a perspective that has been defined on the model specified by CATALOG_NAME.<br /><br /> This restriction is required when the tabular model includes perspectives or when a multidimensional solution includes multiple cube or perspectives.|  
|**METADATA**|**DBTYPE_WSTR**|No|A string containing the XML definition of a data source and its properties, according to the CSDLBI schema.|  
|**CUBE_ID**|**DBTYPE_WSTR**|Yes|A string identifier.<br /><br /> This restriction is optional for multidimensional databases. If multiple cubes are available and the restriction is omitted, the default cube is returned.|  
  
## Remarks  
 DISCOVER_CSDL_METADATA has the following requirements:  
  
-   The DISCOVER request will fail if a database is not specified by using the CATALOG_NAME restriction.  
  
-   For a tabular model, a perspective ID is required if there is more than one perspective in the model.  
  
-   For multidimensional models, the catalog name and cube (perspective) name are both required.  
  
-   If a perspective is provided as a restriction, the same CSDL rowset is returned as for the model. However, any objects that are present in the model but which are not included in the specified perspective are marked as **Hidden** = True.  
  
-   For tables and columns, the DISCOVER request always outputs a value from the cube dimension. If the cube dimension property is not set, the request returns the value from the dimension.  
  
-   The DISCOVER request cannot return any measure or calculated columns that contain a semantic error.  
  
-   The DISCOVER request will not return any information for objects that do not have property values. The DISCOVER request will also not return values for attributes that use the default value.  
  
 The XML string that is returned in the rowset may include the following language-specific properties or values. For example, if you issue the rowset request from a client that has the LCID of 0403 (Catalan Spanish), the property will return the following values as appropriate for Catalan Spanish. If translations are not available on the server, the string for the default language of the server is returned.  
  
-   Caption  
  
-   Qualifier  
  
-   SortDirection  
  
-   IsRightToLeft  
  
## Example  
 **Tabular**  
  
 The following XMLA query returns the CSDL representation of the AdventureWorks 2012 tabular model sample. Each tabular solution can contain only one model, so the PERSPECTIVE_NAME restriction can be left blank. However, this model contains several perspectives.  
  
```  
  
<Discover  xmlns="urn:schemas-microsoft-com:xml-analysis">  
     <RequestType>DISCOVER_CSDL_METADATA</RequestType>  
         <Restrictions>  
            <RestrictionList>  
                <CATALOG_NAME>AdventureWorks2012Tabular</CATALOG_NAME>  
                <PERSPECTIVE_NAME>EMPLOYEE</PERSPECTIVE_NAME>  
                <VERSION>2.0</VERSION>  
            </RestrictionList>  
         </Restrictions>  
         <Properties>  
                <PropertyList>  
                </PropertyList>  
         </Properties>  
</Discover>  
  
```  
  
## Example  
 **Multidimensional**  
  
 The following XMLA query returns the CSDLBI representations of the Contoso Operations cube. The VERSION restriction is required to query a multidimensional database. The Contoso Retail database contains two cubes, so the Operations cube is referenced by using the PERSPECTIVE_NAME restriction.  
  
```  
  
<Discover  xmlns="urn:schemas-microsoft-com:xml-analysis">  
     <RequestType>DISCOVER_CSDL_METADATA</RequestType>  
         <Restrictions>  
            <RestrictionList>  
                <CATALOG_NAME>Contoso_Retail</CATALOG_NAME>  
                <PERSPECTIVE_NAME>Operation</PERSPECTIVE_NAME>  
                <VERSION>2.0</VERSION>  
            </RestrictionList>  
         </Restrictions>  
         <Properties>  
                <PropertyList>  
                </PropertyList>  
         </Properties>  
 </Discover>  
  
```  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-working-with-schema-rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|3444B255-171E-4cb9-AD98-19E57888A75F|  
|ADOMDNAME|Csdl|  
  
## See Also  
 [Analysis Services Schema Rowsets](../../../analysis-services/schema-rowsets/analysis-services-schema-rowsets.md)   
 [CSDL Annotations for Business Intelligence &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/csdl-annotations-for-business-intelligence-csdlbi.md)  
  
  