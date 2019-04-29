---
title: "IMDEmbedded Interface | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
ms.assetid: 9dba8c68-4bef-4c2b-815c-c286f1a1939b
author: minewiskan
ms.author: owend
manager: craigg
---
# IMDEmbedded Interface
  The IMDEmbedded interface is a public interface used to manage an embedded PowerPivot database or a tabular model database. The interface inherits from the `IPersistStream` interface. The interface allows for the following operations:  
  
-   Get an identifier to the embedded stream in the container document.  
  
-   Set the URL of the containing document.  
  
-   Set a flag to indicate if the embedding application is in a hosted environment.  
  
-   Set the path to temporary files used by the embedding application.  
  
-   Cancel the current embedded operation.  
  
-   Get the estimated size (in bytes) of the stream to save the embedded object. Inherited from `IPersistStream`.  
  
-   Verify if the embedded database has changed since it was last saved. Inherited from `IPersistStream`.  
  
-   Load the embedded database to the local or in-process engine. Inherited from `IPersistStream`.  
  
-   Save the local or in-process database to the embedded stream in the container document. Inherited from `IPersistStream`.  
  
## Reference  
 The following reference documents the `IMDEmbedded` interface as presented in **msmd.h** header file.  
  
### Source file: PXOEmbeddedData.idl  
  
```  
[  
  local,                            
  object,                           
  uuid(6B6691CF-5453-41c2-ADD9-4F320B7FD421),                       
  pointer_default(unique)           
]  
interface IMDEmbeddedData : IPersistStream  
{  
 [id(1), helpstring("Set flag indicating if the application is in a hosted environment")]   
 HRESULT SetHosted(  
  [in] BOOL in_fIsHosted);  
  
 [id(2), helpstring("Set the URL for the document containing the embedded stream")]   
 HRESULT SetContainerURL(  
  [in] BSTR in_bstrURL);  
  
 [id(3), helpstring("Get identifier used to look up embedded stream in container document")]   
 HRESULT GetStreamIdentifier(  
  [out, retval] BSTR* out_pbstrStreamId);  
  
 [id(4), helpstring("Set the path used by the embedding application for temporary files")]   
 HRESULT SetTempDirPath(  
  [in]  BSTR in_bstrPath);  
  
 [id(5), helpstring("Cancel the current operation")]   
 HRESULT Cancel();  
};  
```  
  
### IMDEmbeddedData::GetStreamIdentifier  
  
```  
HRESULT GetStreamIdentifier (  
    [out, retval] BSTR * out_pbstrStreamId  
    )  
```  
  
#### Description  
 Gets the identifier used by the host application to the embedded stream in the container document.  
  
#### Parameters  
 *out_pbstrStreamId*  
 Specifies the location of the stream identifier.  
  
#### Return Value  
 `S_OK`  
 The stream identifier was successfully returned.  
  
 `S_FALSE`  
 There is no stream identifier.  
  
 `E_FAIL`  
 An error occurred accessing the stream identifier.  
  
#### Remarks  
 To verify if the current connection contains an embedded database, the user should check the value of the DBPROP_MSMD_EMBEDDED_DATA property from the OLE DB connection properties.  
  
 The possible values for DBPROP_MSMD_EMBEDDED_DATA are:  
  
|Name|Value|Definition|  
|----------|-----------|----------------|  
|DBPROPVAL_EMBED_NONE|0x00|No embedded database available|  
|DBPROPVAL_EMBED_EMBEDDED|0x01|The current application contains the embedded database|  
|DBPROPVAL_EMBED_LINKED|0x02|The embedded database is hosted in a remote application (i.e. SharePoint Server)|  
  
#### Source  
  
```  
[id(1), helpstring("Get identifier used to look up embedded stream in container document")]   
 HRESULT GetStreamIdentifier(  
  [out, retval] BSTR* out_pbstrStreamId);  
```  
  
### IMDEmbeddedData::SetContainerURL  
  
```  
HRESULT SetContainerURL (  
    [in] BSTR in_bstrURL  
    )  
```  
  
#### Description  
 Sets the URL for the file containing the embedded stream.  
  
#### Parameters  
 *in_bstrURL*  
 Specifies the URL for the containing document.  
  
#### Return Value  
 `S_OK`  
 The container URL was successfully set.  
  
 `E_FAIL`  
 An error occurred while setting the container URL.  
  
#### Source  
  
```  
[id(2), helpstring("Set the URL for the document containing the embedded stream")]   
 HRESULT SetContainerURL(  
  [in] BSTR in_bstrURL);  
```  
  
### IMDEmbeddedData::SetHosted  
  
```  
HRESULT SetHosted (  
    [in] BOOL in_fIsHosted  
    )  
```  
  
#### Description  
 Set a flag to indicate if the embedding application is in a hosted environment.  
  
#### Parameters  
 *in_ftHosted*  
 TRUE if caller is in a hosted in a service application (like IIS).  
  
#### Return Value  
 `S_OK`  
 The flag was successfully set.  
  
 `E_FAIL`  
 An error occurred while setting the flag.  
  
#### Source  
  
```  
[id(5), helpstring("Set flag indicating if the application is in a hosted environment")]   
 HRESULT SetHosted(  
  [in]  BOOL in_fIsHosted);  
```  
  
### IMDEmbeddedData::SetTempDirPath  
  
```  
HRESULT SetTempDirPath (  
    [in] BSTR in_bstrPath  
    )  
```  
  
#### Description  
 Set the path to temporary files used by the embedding application.  
  
#### Parameters  
 *in_bstrPath*  
 The path used by the host application for temporary files.  
  
#### Return Value  
 `S_OK`  
 The temporary file directory was successfully set.  
  
 `E_FAIL`  
 An error occurred while setting the path.  
  
#### Source  
  
```  
[id(4), helpstring("Set the path used by the host application for temporary files")]   
 HRESULT SetTempDirPath(  
  [in]  BSTR in_bstrPath);  
```  
  
### IMDEmbeddedData::Cancel  
  
```  
HRESULT Cancel ( void )  
```  
  
#### Description  
 Cancels the current embedded database operation  
  
#### Parameters  
 None.  
  
#### Return Value  
 `S_OK`  
 The operation was successfully cancelled.  
  
 `DB_E_CANTCANCEL`  
 No cancellable operation is currently in progress.  
  
 `E_FAIL`  
 An error occurred while cancelling the embedded operation.  
  
#### Source  
  
```  
[id(5), helpstring("Cancel the current operation")]   
 HRESULT Cancel();  
```  
  
### IMDEmbeddedData::GetSizeMax (IPersistStream::GetSizeMax)  
  
```  
HRESULT GetSizeMax (  
    [out] ULARGE_INTEGER * out_pcbSize  
    )  
```  
  
#### Description  
 Gets the estimated size (in bytes) of the stream to save the embedded object. Inherited from `IPersistStream`.  
  
#### Parameters  
 *in_bstrPath*  
 The estimated size (in bytes) of the embedded database image.  
  
#### Return Value  
 `S_OK`  
 The size was successfully obtained.  
  
 `E_FAIL`  
 An error occurred while obtaining the size.  
  
### IMDEmbeddedData::IsDirty (IPersistStream::IsDirty)  
  
```  
HRESULT IsDirty ( void )  
```  
  
#### Description  
 Verifies if the embedded database has changed since it was last saved. Inherited from `IPersistStream`.  
  
#### Parameters  
 none  
  
#### Return Value(s)  
 `S_OK`  
 The database has changed since it was last saved.  
  
 `S_FALSE`  
 The database has not changed since it was last saved.  
  
 `E_FAIL`  
 An error occurred while obtaining the database state.  
  
### IMDEmbeddedData::Load (IPersistStream::Load)  
  
```  
HRESULT Load (   
    [in] IStream * in_pStm   
    )  
```  
  
#### Description  
 Loads the embedded database to the local or in-process engine. Inherited from `IPersistStream`.  
  
#### Parameters  
 *in_pStm*  
 A pointer to a stream interface from where to load the embedded database.  
  
#### Return Value(s)  
 `S_OK`  
 The database was successfully loaded.  
  
 `E_OUTOFMEMORY`  
 Not enough memory to load the database.  
  
 `E_FAIL`  
 An error occurred while loading the database, different than `E_OUTOFMEMORY`.  
  
### IMDEmbeddedData::Save (IPersistStream::Save)  
  
```  
HRESULT Save (   
    [in] IStream * in_pStm,  
    [in] BOOL in_fClearDirty  
    )  
```  
  
#### Description  
 Saves the local or in-process database to the embedded stream in the container document. Inherited from `IPersistStream`.  
  
#### Parameters  
 *in_pStm*  
 A pointer to a stream interface to where to save the embedded database.  
  
 *in_fClearDirty*  
 A flag that indicates whether the dirty flag should be cleared after this operation.  
  
#### Return Value(s)  
 `S_OK`  
 The database was successfully saved.  
  
 `STG_E_CANTSAVE`  
 An error occurred while saving the database, different than `STG_E_MEDIUMFULL`.  
  
 `STG_E_MEDIUMFULL`  
 The database could not be saved because there is no space left on the storage device.  
  
  
