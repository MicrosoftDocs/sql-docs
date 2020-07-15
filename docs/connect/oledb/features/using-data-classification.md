---
title: "Using Data Classification with Microsoft OLE DB Driver for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "07/25/2020"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "driver"
author: "bazizi"
ms.author: v-beaziz
manager: kenvh
---
# Using data classification
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

## Overview
Sensitivity classification allows the client to provide database columns with sensitivity metadata. As a result, applications can handle different types of sensitive data (such as health, financial, and so on) based on data protection policies.

For more information on how to assign classification to columns, see [SQL Data Discovery and Classification](https://docs.microsoft.com/sql/relational-databases/security/sql-data-discovery-and-classification).

Microsoft OLE DB Driver 18.5 allows the retrieval of this metadata via the [ISSDataClassification](../ole-db-interfaces/issdataclassification-ole-db.md) interface.

## Code sample

```cpp
#include <string>
#include <iostream>
#include <oledb.h>
#include "msoledbsql.h"

HRESULT Connect(IDBInitialize *&pIDBInitialize, const wchar_t *server, const wchar_t *database);
HRESULT GetISSDataClassification(IDBInitialize *pIDBInitialize, ISSDataClassification *&pISSDataClassification, const wchar_t *query);
HRESULT PrintSensitivityClassificationInfo(ISSDataClassification *pISSDataClassification);

int main()
{
    const wchar_t server[] = L"myserver";
    const wchar_t database[] = L"mydb";
    const wchar_t query[] = L"SELECT a, b, c, a + c FROM mytable";
    IDBInitialize *pIDBInitialize = nullptr;
    ISSDataClassification *pISSDataClassification = nullptr;
    int retVal = 0;

    CoInitialize(nullptr);

    // Connect to data source
    if (FAILED(Connect(pIDBInitialize, server, database)))
    {
        std::wcout << L"Failed to establish connection." << std::endl;
        retVal = -1;
        goto Cleanup;
    }

    // Obtain DataClassification object
    if (FAILED(GetISSDataClassification(pIDBInitialize, pISSDataClassification, query)))
    {
        std::wcout << L"Failed to get data classification object." << std::endl;
        retVal = -1;
        goto Cleanup;
    }

    // Print data classification data
    if (FAILED(PrintSensitivityClassificationInfo(pISSDataClassification)))
    {
        std::wcout << L"Failed to get data classification info." << std::endl;
        retVal = -1;
        goto Cleanup;
    }

Cleanup:
    if (pISSDataClassification)
    {
        pISSDataClassification->Release();
        pISSDataClassification = nullptr;
    }

    if (pIDBInitialize)
    {
        pIDBInitialize->Uninitialize();
        pIDBInitialize->Release();
        pIDBInitialize = nullptr;
    }

    CoUninitialize();
    return retVal;
}

HRESULT PrintSensitivityClassificationInfo(ISSDataClassification *pISSDataClassification)
{
    SENSITIVITYCLASSIFICATION *pSensitivityClassification = nullptr;

    IMalloc *pIMalloc = nullptr;
    HRESULT hr = CoGetMalloc(1, &pIMalloc);
    if (FAILED(hr))
    {
        std::wcout << L"Failed to get Malloc object." << std::endl;
        goto Cleanup;
    }

    hr = pISSDataClassification->GetSensitivityClassification(&pSensitivityClassification);

    if (FAILED(hr))
    {
        goto Cleanup;
    }

    if (pSensitivityClassification)
    {
        std::wcout << L"Query sensitivity rank: " << pSensitivityClassification->eQuerySensitivityRank << std::endl
                   << std::endl;

        for (int indColumnSensitivityMetadata = 0; indColumnSensitivityMetadata < pSensitivityClassification->cColumnSensitivityMetadata; ++indColumnSensitivityMetadata)
        {
            std::wcout << L"                Column #" << indColumnSensitivityMetadata << std::endl;
            for (int indSensitivityProperties = 0; indSensitivityProperties < pSensitivityClassification->rgColumnSensitivityMetadata[indColumnSensitivityMetadata].cSensitivityProperties; ++indSensitivityProperties)
            {
                std::wcout << L"Prop #" << indSensitivityProperties << std::endl;
                std::wcout << L"Column sensitivity rank: " << pSensitivityClassification->rgColumnSensitivityMetadata[indColumnSensitivityMetadata].rgSensitivityProperties[indSensitivityProperties].eSensitivityRank << std::endl;
                if (pSensitivityClassification->rgColumnSensitivityMetadata[indColumnSensitivityMetadata].rgSensitivityProperties[indSensitivityProperties].pInformationType)
                {
                    std::wcout << L"Info type #" << pSensitivityClassification->rgColumnSensitivityMetadata[indColumnSensitivityMetadata].rgSensitivityProperties[indSensitivityProperties].pInformationType->pwszId << std::endl;
                    std::wcout << L"Info type name: " << pSensitivityClassification->rgColumnSensitivityMetadata[indColumnSensitivityMetadata].rgSensitivityProperties[indSensitivityProperties].pInformationType->pwszName << std::endl;
                }
                else
                {
                    std::wcout << L"Info type N/A\n";
                }

                if (pSensitivityClassification->rgColumnSensitivityMetadata[indColumnSensitivityMetadata].rgSensitivityProperties[indSensitivityProperties].pSensitivityLabel)
                {
                    std::wcout << L"Label #" << pSensitivityClassification->rgColumnSensitivityMetadata[indColumnSensitivityMetadata].rgSensitivityProperties[indSensitivityProperties].pSensitivityLabel->pwszId << std::endl;
                    std::wcout << L"Label name: " << pSensitivityClassification->rgColumnSensitivityMetadata[indColumnSensitivityMetadata].rgSensitivityProperties[indSensitivityProperties].pSensitivityLabel->pwszName << std::endl;
                }
                else
                {
                    std::wcout << L"Label N/A\n";
                }
                std::wcout << std::endl;
            }
        }
    }

Cleanup:
    if (pSensitivityClassification)
    {
        pIMalloc->Free(pSensitivityClassification);
        pSensitivityClassification = nullptr;
        pIMalloc->Release();
        pIMalloc = nullptr;
    }

    return hr;
}

HRESULT Connect(IDBInitialize *&pIDBInitialize, const wchar_t *server, const wchar_t *database)
{
    // Construct the connection string.
    std::wstring connString = L"Server=" + std::wstring(server) + L";Database=" +
                              std::wstring(database) + L";Authentication=ActiveDirectoryIntegrated;Encrypt=yes;";

    HRESULT hr = CoCreateInstance(CLSID_MSOLEDBSQL, nullptr, CLSCTX_INPROC_SERVER, IID_IDBInitialize, reinterpret_cast<LPVOID *>(&pIDBInitialize));
    if (FAILED(hr))
    {
        std::wcout << L"Failed to create an IDBInitialize instance." << std::endl;
        goto Cleanup;
    }

    {
        IDBProperties *pIDBProperties = nullptr;
        hr = pIDBInitialize->QueryInterface<IDBProperties>(&pIDBProperties);

        if (FAILED(hr))
        {
            std::wcout << L"Failed to get IDBProperties object." << std::endl;
            goto Cleanup;
        }

        DBPROP prop = {};
        prop.dwPropertyID = DBPROP_INIT_PROVIDERSTRING;
        prop.vValue.vt = VT_BSTR;
        prop.vValue.bstrVal = SysAllocString(connString.c_str());

        DBPROPSET propSet = {};
        propSet.cProperties = 1;
        propSet.guidPropertySet = DBPROPSET_DBINIT;
        propSet.rgProperties = &prop;

        hr = pIDBProperties->SetProperties(1, &propSet);

        pIDBProperties->Release();
        pIDBProperties = nullptr;

        VariantClear(&prop.vValue);

        if (FAILED(hr))
        {
            std::wcout << L"Failed to set properties." << std::endl;
            goto Cleanup;
        }

        hr = pIDBInitialize->Initialize();
        if (FAILED(hr))
        {
            goto Cleanup;
        }
    }
Cleanup:
    return hr;
}

HRESULT GetISSDataClassification(IDBInitialize *pIDBInitialize, ISSDataClassification *&pISSDataClassification, const wchar_t *query)
{
    IDBCreateCommand *pIDBCreateCommand = nullptr;
    IDBCreateSession *pIDBCreateSession = nullptr;
    ICommandText *pICommandText = nullptr;

    HRESULT hr = pIDBInitialize->QueryInterface<IDBCreateSession>(&pIDBCreateSession);
    if (FAILED(hr))
    {
        std::wcout << L"Failed to get session object." << std::endl;
        goto Cleanup;
    }

    hr = pIDBCreateSession->CreateSession(nullptr, IID_IDBCreateCommand, reinterpret_cast<IUnknown **>(&pIDBCreateCommand));
    pIDBCreateSession->Release();
    pIDBCreateSession = nullptr;

    if (FAILED(hr))
    {
        std::wcout << L"Failed to create a session." << std::endl;
        goto Cleanup;
    }

    hr = pIDBCreateCommand->CreateCommand(nullptr, IID_ICommandText, reinterpret_cast<IUnknown **>(&pICommandText));

    pIDBCreateCommand->Release();
    pIDBCreateCommand = nullptr;

    if (FAILED(hr))
    {
        std::wcout << L"Failed to get command object." << std::endl;
        goto Cleanup;
    }

    hr = pICommandText->SetCommandText(DBGUID_DBSQL, query);
    if (FAILED(hr))
    {
        std::wcout << L"Failed to set command text." << std::endl;
        goto Cleanup;
    }

    hr = pICommandText->Execute(nullptr, IID_ISSDataClassification, nullptr, nullptr, reinterpret_cast<IUnknown **>(&pISSDataClassification));
    if (FAILED(hr))
    {
        std::wcout << L"Failed to execute command." << std::endl;
        goto Cleanup;
    }

Cleanup:
    if (pICommandText)
    {
        pICommandText->Release();
        pICommandText = nullptr;
    }
    return hr;
}
```


