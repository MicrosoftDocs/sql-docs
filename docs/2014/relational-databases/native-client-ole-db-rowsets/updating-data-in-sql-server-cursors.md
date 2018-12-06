---
title: "Updating Data in SQL Server Cursors | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "updating data [SQL Server]"
  - "isolation levels [SQL Server]"
  - "delayed update mode [OLE DB]"
  - "immediate update mode [OLE DB]"
  - "cursors [OLE DB]"
  - "data updates [SQL Server], OLE DB"
ms.assetid: 732dafee-f2d5-4aef-aad7-3a8bf3b1e876
author: MightyPen
ms.author: genemi
manager: craigg
---
# Updating Data in SQL Server Cursors
  When fetching and updating data through [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cursors, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider consumer application is bound by the same considerations and constraints that apply to any other client application.  
  
 Only rows in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cursors participate in concurrent data-access control. When the consumer requests a modifiable rowset, the concurrency control is controlled by DBPROP_LOCKMODE. To modify the level of concurrent access control, the consumer sets the DBPROP_LOCKMODE property before opening the rowset.  
  
 Transaction isolation levels can cause significant lags in row positioning if client application design lets transactions remain open for long periods of time. By default, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider uses the read-committed isolation level specified by DBPROPVAL_TI_READCOMMITTED. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider supports dirty read isolation when the rowset concurrency is read-only. Therefore, the consumer can request a higher level of isolation in a modifiable rowset but cannot request any lower level successfully.  
  
## Immediate and Delayed Update Modes  
 In immediate update mode, each call to **IRowsetChange::SetData** causes a round trip to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the consumer makes multiple changes to a single row, it is more efficient to submit all changes with a single **SetData** call.  
  
 In delayed update mode, a roundtrip is made to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for each row indicated in the *cRows* and *rghRows* parameters of **IRowsetUpdate::Update**.  
  
 In either mode, a roundtrip represents a distinct transaction when no transaction object is open for the rowset.  
  
 When you are using **IRowsetUpdate::Update**, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider tries to process each indicated row. An error occurring because of invalid data, length, or status values for any row does not stop [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider processing. All or none of the other rows participating in the update may be modified. The consumer must examine the returned *prgRowStatus* array to determine failure for any specific row when the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider returns DB_S_ERRORSOCCURRED.  
  
 A consumer should not assume that rows are processed in any specific order. If a consumer requires ordered processing of data modification over more than a single row, the consumer should establish that order in the application logic and open a transaction to enclose the process.  
  
## See Also  
 [Updating Data in Rowsets](updating-data-in-rowsets.md)  
  
  
