---
title: "IRowsetFastLoad::Commit (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
api_name: 
  - "IRowsetFastLoad::Commit (OLE DB)"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Commit method"
ms.assetid: 19de9128-b91a-4626-847f-af721edaa24e
author: MightyPen
ms.author: genemi
manager: craigg
---
# IRowsetFastLoad::Commit (OLE DB)
  Marks the end of a batch of inserted rows and writes the rows to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. For samples, see [Bulk Copy Data Using IRowsetFastLoad &#40;OLE DB&#41;](irowsetfastload-ole-db.md) and [Send BLOB Data to SQL SERVER Using IROWSETFASTLOAD and ISEQUENTIALSTREAM &#40;OLE DB&#41;](../native-client-ole-db-how-to/send-blob-data-to-sql-server-using-irowsetfastload-and-isequentialstream-ole-db.md).  
  
## Syntax  
  
```  
  
HRESULT Commit(  
BOOL   
fDone  
);  
  
```  
  
## Arguments  
 *fDone*[in]  
 If FALSE, the rowset maintains validity and can be used by the consumer for additional row insertion. If TRUE, the rowset loses validity and no further insertion can be done by the consumer.  
  
## Return Code Values  
 S_OK  
 The method succeeded and all inserted data has been written to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
 E_FAIL  
 A provider-specific error occurred. Retrieve error information for the specific error text from the provider.  
  
 E_UNEXPECTED  
 The method was called on a bulk copy rowset previously invalidated by the **IRowsetFastLoad::Commit** method.  
  
## Remarks  
 A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider bulk copy rowset behaves as a delayed-update mode rowset. As the user inserts row data through the rowset, inserted rows are treated in the same fashion as pending inserts on a rowset supporting **IRowsetUpdate**.  
  
 The consumer must call the **Commit** method on the bulk copy rowset to write inserted rows to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table in the same way as the **IRowsetUpdate::Update** method is used to submit pending rows to an instance of SQL Server.  
  
 If the consumer releases its reference on the bulk copy rowset without calling the **Commit** method, all inserted rows not previously written are lost.  
  
 The consumer can batch inserted rows by calling the **Commit** method with the *fDone* argument set to FALSE. When *fDone*is set to TRUE, the rowset becomes invalid. An invalid bulk copy rowset supports only the **ISupportErrorInfo** interface and **IRowsetFastLoad::Release** method.  
  
## See Also  
 [IRowsetFastLoad &#40;OLE DB&#41;](irowsetfastload-ole-db.md)  
  
  
