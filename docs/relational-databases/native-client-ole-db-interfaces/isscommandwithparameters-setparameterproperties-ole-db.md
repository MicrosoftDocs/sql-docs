---
description: "ISSCommandWithParameters::SetParameterProperties in SQL Server Native Client (OLE DB)"
title: "ISSCommandWithParameters::SetParameterProperties (OLE DB)"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apiname: 
  - "ISSCommandWithParameters::SetParameterProperties (OLE DB)"
apitype: "COM"
helpviewer_keywords: 
  - "SetParameterProperties method"
ms.assetid: 4cd0281a-a2a0-43df-8e46-eb478b64cb4b
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ISSCommandWithParameters::SetParameterProperties in SQL Server Native Client (OLE DB)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT]
> [!INCLUDE[snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

  Sets the parameter properties on a per parameter basis by ordinal, or sets bulk parameter properties by specifying an array of SSPARAMPROPS structures.  
  
## Syntax  
  
```cpp
HRESULT SetParameterProperties(  
      DB_UPARAMS cParams,   
      SSPARAMPROPS rgParamProperties[]);  
```  
  
## Arguments  
 *cParams*[in]  
 The number of SSPARAMPROPS structures in the *rgParamProperties* array. If this number is zero, **ISSCommandWithParameters::SetParameterProperties** will delete all properties that might have been specified for any parameters in the command.  
  
 *rgParamProperties*[in]  
 An array of SSPARAMPROPS structures to be set.  
  
## Return Code Values  
 The **ISSCommandWithParameters::SetParameterProperties** method returns the same error codes as the core OLE DB **ICommandProperties::SetProperties** method.  
  
## Remarks  
 Setting parameter properties with this method is allowed on a per parameter basis by ordinal, or with a single **ISSCommandWithParameters::SetParameterProperties** call once the SSPARAMPROPS has been built from the property array.  
  
 The **SetParameterInfo** method must be called before calling the **ISSCommandWithParameters::SetParameterProperties** method. Calling `SetParameterProperties(0, NULL)` clears all specified parameter properties, while calling `SetParameterInfo(0,NULL,NULL)` clears all the parameter info including any properties that might be associated with a parameter.  
  
 Calling **ISSCommandWithParameters::SetParameterProperties** to specify properties for a parameter which is not of type DBTYPE_XML or DBTYPE_UDT returns DB_E_ERRORSOCCURRED or DB_S_ERRORSOCCURRED and marks the *dwStatus* field of all DBPROPs contained in SSPARAMPROPS for that parameter with DBPROPSTATUS_NOTSET. The DBPROP array of each DBPROPSET contained in SSPARAMPROPS should be traversed to detect which parameter the DB_E_ERRORSOCCURRED or DB_S_ERRORSOCCURRED refers to.  
  
 If **ISSCommandWithParameters::SetParameterProperties** is called to specify the properties of parameters whose parameter info have not been set yet with **SetParameterInfo**, the provider returns E_UNEXPECTED with the following error message:  
  
 SetParameterProperties method cannot be called for the specified parameters without first calling SetParameterInfo method. The parameter information must be set before setting the parameter properties.  
  
 If the call to **ISSCommandWithParameters::SetParameterProperties** contains some parameters where the parameter info has been set, and some parameters where the parameter info has not been set, the dwStatus properties in the DBPROPSET of the SSPARAMPROPS property set will return with DBSTATUS_NOTSET.  
  
 The SSPARAMPROPS structure is defined as follows:  

```cpp
struct SSPARAMPROPS {
    DBORDINAL iOrdinal;
    ULONG cPropertySets;
    DBPROPSET *rgPropertySets;
};
```

 Improvements in the database engine starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] allow ISSCommandWithParameters::SetParameterProperties to obtain more accurate descriptions of the expected results. These more accurate results may differ from the values returned by ISSCommandWithParameters::SetParameterProperties in previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Metadata Discovery](../../relational-databases/native-client/features/metadata-discovery.md).  
  
|Member|Description|  
|------------|-----------------|  
|*iOrdinal*|The ordinal of the passed parameter.|  
|*cPropertySets*|The number of DBPROPSET structures in *rgPropertySets*.|  
|*rgPropertySets*|A pointer to memory in which to return an array of DBPROPSET structures.|  

## See Also  
 [ISSCommandWithParameters &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-interfaces/isscommandwithparameters-ole-db.md)  
  
  
