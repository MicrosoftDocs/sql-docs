---
title: "ISSDataClassification::GetSensitivityClassification | Microsoft Docs"
description: "ISSDataClassification::GetSensitivityClassification"
ms.custom: ""
ms.date: "07/25/2020"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
apiname: 
  - "ISSDataClassification::GetSensitivityClassification"
apitype: "COM"
helpviewer_keywords: 
  - "GetSensitivityClassification method"
author: bazizi
ms.author: v-beaziz
---
# ISSDataClassification::GetSensitivityClassification
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

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
The OLE DB Driver for SQL Server allocates a block of memory to hold the SENSITIVITYCLASSIFICATION structure and the data referenced by this structure. When the consumer no longer requires access to the classification data, it must deallocate this memory by calling the [IMalloc::Free](https://docs.microsoft.com/windows/win32/api/objidl/nf-objidl-imalloc-free) method.  
  
 The SENSITIVITYCLASSIFICATION structure is defined as follows:
  
```cpp
typedef struct tagSensitivityClassification
{
    USHORT cSensitivityLabels;
    SENSITIVITYLABEL* rgSensitivityLabels;
    USHORT cInformationTypes;
    INFORMATIONTYPE* rgInformationTypes;
    USHORT cColumnSensitivityMetadata;
    COLUMNSENSITIVITYMETADATA* rgColumnSensitivityMetadata;
    SENSITIVITYRANKENUM eQuerySensitivityRank;
} SENSITIVITYCLASSIFICATION;
```  

|Member|Description|  
|------------|-----------------|  
|*cSensitivityLabels*|Number of SENSITIVITYLABEL structures in *rgSensitivityLabels*.|  
|*rgSensitivityLabels*|A pointer to memory in which to return an array of SENSITIVITYLABEL structures.|  
|*cInformationTypes*|Number of INFORMATIONTYPE structures in *rgInformationTypes*.|  
|*rgInformationTypes*|A pointer to memory in which to return an array of INFORMATIONTYPE structures.|  
|*cColumnSensitivityMetadata*|Number of COLUMNSENSITIVITYMETADATA structures in *rgColumnSensitivityMetadata*|  
|*rgColumnSensitivityMetadata*|A pointer to memory in which to return an array of COLUMNSENSITIVITYMETADATA structures.|  
|*eQuerySensitivityRank*|An enum representing the sensitivity rank for the query that was executed to obtain the rowset.|  

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
|*pwszName*|The name for the sensitivity label.|  
|*pwszId*|The identifier for the sensitivity label.|  

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
|*pwszName*|The name for the information type.|  
|*pwszId*|The identifier for the information type.|  

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
|*cSensitivityProperties*|Number of SENSITIVITYPROPERTY structures in *rgSensitivityProperties*.|  
|*rgSensitivityProperties*|A pointer to memory in which to return an array of SENSITIVITYPROPERTY structures.|  

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

|Member|Description|  
|------------|-----------------|  
|*SENSITIVITYRANK_NOT_DEFINED*|Sensitivity ranking is undefined.|  
|*SENSITIVITYRANK_NONE*|No sensitivity rankings present.|  
|*SENSITIVITYRANK_LOW*|Low sensitivity ranking.|  
|*SENSITIVITYRANK_MEDIUM*|Medium sensitivity ranking.|  
|*SENSITIVITYRANK_HIGH*|High sensitivity ranking.|  
|*SENSITIVITYRANK_CRITICAL*|Critical sensitivity ranking.|  

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
|*eSensitivityRank*|An enum representing the sensitivity rank for this sensitivity property.|  

## See Also  
 [ISSDataClassification](../../oledb/ole-db-interfaces/issdataclassification-ole-db.md)  
 [Rowsets](../ole-db-rowsets/rowsets.md)  
  
