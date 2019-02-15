---
title: "Data Type Mapping in the SQL Server Import and Export Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "01/11/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 669be403-cb17-4b12-bbbf-e7a74003c4b6
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Data Type Mapping in the SQL Server Import and Export Wizard
 In the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard, you can set the name, the data type, and the data type properties of columns in new destination tables and files, but you can't specify custom conversions for column values. As a result, the built-in mapping of data types from source to destination is important.  
  
##  <a name="wizardMapping"></a> How does the wizard map data types between source and destination?
The wizard uses mapping files that are installed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] to map data types from one database system or version to another. For example, it can map from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types to Oracle data types. By default, the mapping files in XML format are installed in the following folders.
-   **C:\Program Files\Microsoft SQL Server\130\DTSMappingFiles\** (for 64-bit)
-   **C:\Program Files (x86)\Microsoft SQL Server\130\DTSMappingFiles\** (for 32-bit).  
  
 If you edit an existing mapping file, or add a new mapping file to the folder, you have to close and reopen the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to load the new or changed mapping file.  
 
## You can change an existing mapping file
If your business requires different mappings between data types, you can update the mapping files to change the mappings used by the wizard. For example, if you want the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **nchar** data type to map to the DB2 **GRAPHIC** data type instead of the DB2 **VARGRAPHIC** data type when you transfer data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to DB2, you can change the **nchar** mapping in the **SqlClientToIBMDB2.xml** mapping file to use **GRAPHIC** instead of **VARGRAPHIC.**  
  
## You can add a new mapping file
[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] installs mappings between many commonly used combinations of source and destination. You can also add new mapping files to the **MappingFiles** directory to support additional sources and destinations. The new mapping files must conform to the published XSD schema and must map between a unique combination of source and destination. The schema for mapping files, **DataTypeMapping.xsd**, is published [here](https://schemas.microsoft.com/sqlserver/2008/07/IntegrationServices/DataTypeMapping/DataTypeMapping.xsd).
 
## Sample mapping file
Here's a portion of the XML mapping file that maps from SQL Server data types (or, more specifically, from the data types used by the .Net Framework Data Provider for SQL Server) to Oracle data types. As one example, you can see that a SQL Server **int** data type maps to an Oracle **INTEGER** data type.
  
```xml  
  
<dtm:DataTypeMappings  
    xmlns:dtm="https://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd"   
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"  
    SourceType="System.Data.SqlClient.SqlConnection"   
    MinSourceVersion="*"   
    MaxSourceVersion="*"   
    DestinationType="MSDAORA;OraOLEDB.Oracle;System.Data.OracleClient.OracleConnection"   
    MinDestinationVersion="08.*"   
    MaxDestinationVersion="*">  
  
    <!-- smallint -->  
    <dtm:DataTypeMapping >  
        <dtm:SourceDataType>  
            <dtm:DataTypeName>smallint</dtm:DataTypeName>  
        </dtm:SourceDataType>  
        <dtm:DestinationDataType>  
            <dtm:SimpleType>  
                <dtm:DataTypeName>INTEGER</dtm:DataTypeName>  
            </dtm:SimpleType>  
        </dtm:DestinationDataType>  
    </dtm:DataTypeMapping>    
  
    <!-- int -->  
    <dtm:DataTypeMapping >  
        <dtm:SourceDataType>  
            <dtm:DataTypeName>int</dtm:DataTypeName>  
        </dtm:SourceDataType>  
        <dtm:DestinationDataType>  
            <dtm:SimpleType>  
                <dtm:DataTypeName>INTEGER</dtm:DataTypeName>  
            </dtm:SimpleType>  
        </dtm:DestinationDataType>  
    </dtm:DataTypeMapping>    
  
        ...  
  
</dtm:DataTypeMappings>  
  
```  

