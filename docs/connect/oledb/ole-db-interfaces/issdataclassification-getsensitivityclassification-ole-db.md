---
title: "ISSDataClassification::GetSensitivityClassification"
description: "ISSDataClassification::GetSensitivityClassification"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-davidengel
ms.date: "09/30/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "GetSensitivityClassification method"
apiname: "ISSDataClassification::GetSensitivityClassification"
apitype: "COM"
---
# ISSDataClassification::GetSensitivityClassification
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../../includes/applies-to-version/sql-asdb-asa.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Retrieves sensitivity classification data for the active rowset. For more information and code sample, see [Using data classification](../features/using-data-classification.md).  
  
## Syntax  
  
```  
HRESULT GetSensitivityClassification(
    SENSITIVITYCLASSIFICATION** ppSensitivityClassification);
```  
  
## Arguments  
  *ppSensitivityClassification*[out]  
 A pointer to a SENSITIVITYCLASSIFICATION structure pointer. If the method fails or there is no data classification information available, the provider doesn't allocate any memory, and ensures that the ppSensitivityClassification argument is a null pointer on output.  
  
## Return Code Values  
 S_OK  
 The method succeeded.    
  
 E_INVALIDARG  
 The *ppSensitivityClassification* argument was NULL.  
  
 E_OUTOFMEMORY  
 The OLE DB Driver for SQL Server couldn't allocate enough memory to complete the request.  

  
## Remarks  
The OLE DB Driver for SQL Server allocates a block of memory to hold the SENSITIVITYCLASSIFICATION structure and the data referenced by this structure. When the consumer no longer requires access to the classification data, it must deallocate this memory by calling the [IMalloc::Free](/windows/win32/api/objidl/nf-objidl-imalloc-free) method.  
  
 The SENSITIVITYCLASSIFICATION structure is defined as follows:
  
```cpp
typedef struct tagSensitivityClassification
{
    USHORT                     cSensitivityLabels;
    SENSITIVITYLABEL          *rgSensitivityLabels;
    USHORT                     cInformationTypes;
    INFORMATIONTYPE           *rgInformationTypes;
    USHORT                     cColumnSensitivityMetadata;
    COLUMNSENSITIVITYMETADATA *rgColumnSensitivityMetadata;
    SENSITIVITYRANKENUM        eQuerySensitivityRank;
} SENSITIVITYCLASSIFICATION;
```  

|Member|Description|  
|------------|-----------------|  
|*cSensitivityLabels*|The number of SENSITIVITYLABEL structures in *rgSensitivityLabels*.|  
|*rgSensitivityLabels*|An array of SENSITIVITYLABEL structures.|  
|*cInformationTypes*|The number of INFORMATIONTYPE structures in *rgInformationTypes*.|  
|*rgInformationTypes*|An array of INFORMATIONTYPE structures.|  
|*cColumnSensitivityMetadata*|The number of COLUMNSENSITIVITYMETADATA structures in *rgColumnSensitivityMetadata*.|  
|*rgColumnSensitivityMetadata*|An array of COLUMNSENSITIVITYMETADATA structures.|  
|*eQuerySensitivityRank*|A relative ranking of the sensitivity of a query that was executed to obtain the rowset.|  

The SENSITIVITYLABEL structure is defined as follows:
```cpp
typedef struct tagSENSITIVITYLABEL
{
    LPOLESTR pwszName;
    LPOLESTR pwszId;
} SENSITIVITYLABEL;
```

|Member|Description|  
|------------|-----------------|  
|*pwszName*|The name for a sensitivity label.|  
|*pwszId*|The identifier for a sensitivity label.|  

The INFORMATIONTYPE structure is defined as follows:
```cpp
typedef struct tagINFORMATIONTYPE
{
    LPOLESTR pwszName;
    LPOLESTR pwszId;
} INFORMATIONTYPE;
```

|Member|Description|  
|------------|-----------------|  
|*pwszName*|The name for an information type.|  
|*pwszId*|The identifier for an information type.|  

The COLUMNSENSITIVITYMETADATA structure is defined as follows:
```cpp
typedef struct tagCOLUMNSENSITIVITYMETADATA
{
    SENSITIVITYPROPERTY* rgSensitivityProperties;
    USHORT cSensitivityProperties;
} COLUMNSENSITIVITYMETADATA;
```

|Member|Description|  
|------------|-----------------|  
|*cSensitivityProperties*|The number of SENSITIVITYPROPERTY structures in *rgSensitivityProperties*.|  
|*rgSensitivityProperties*|An array of SENSITIVITYPROPERTY structures.|  

The SENSITIVITYRANKENUM enum is defined as follows:
```cpp
typedef enum tagSENSITIVITYRANKENUM
{
    SENSITIVITYRANK_NOT_DEFINED = -1,
    SENSITIVITYRANK_NONE = 0,
    SENSITIVITYRANK_LOW = 10,
    SENSITIVITYRANK_MEDIUM = 20,
    SENSITIVITYRANK_HIGH = 30,
    SENSITIVITYRANK_CRITICAL = 40
} SENSITIVITYRANKENUM;
```

The SENSITIVITYPROPERTY structure is defined as follows:
```cpp
typedef struct tagSENSITIVITYPROPERTY
{
    SENSITIVITYLABEL* pSensitivityLabel;
    INFORMATIONTYPE* pInformationType;
    SENSITIVITYRANKENUM eSensitivityRank;
} SENSITIVITYPROPERTY;
```

|Member|Description|  
|------------|-----------------|  
|*pSensitivityLabel*|A pointer to a SENSITIVITYLABEL structure.|  
|*pInformationType*|A pointer to an INFORMATIONTYPE structure.|  
|*eSensitivityRank*|A relative ranking of the sensitivity of a column that is part of per-column data.|  

## See Also  
 [ISSDataClassification](../../oledb/ole-db-interfaces/issdataclassification-ole-db.md)  
 [Rowsets](../ole-db-rowsets/rowsets.md)  
