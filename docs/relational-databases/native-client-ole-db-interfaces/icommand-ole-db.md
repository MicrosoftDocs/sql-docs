---
title: "ICommand (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "ICommand [SQL Server Native Client]"
ms.assetid: 5e24b3a0-0658-44fc-b653-f4c52f9eb328
caps.latest.revision: 7
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# ICommand (OLE DB)
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  This topic discusses OLE DB behavior that is specific to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
## ICommand::Execute  
 Inserting data that is greater than the size of a column typically results in an error. However, there are situations where S_OK will be returned but the *dwStatus* will be set to DBSTATUS_S_TRUNCATED. This generally occurs when inserting data with parameters, where the column is not large enough to hold the data, and **ICommandWithParameters::SetParameterInfo** has not been called.  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](http://msdn.microsoft.com/library/34c33364-8538-45db-ae41-5654481cda93)  
  
  