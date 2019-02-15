---
title: "Objects Supported by the Generate Scripts Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 071eb2cb-f073-41ca-9f4d-11d3b8803495
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Objects Supported by the Generate Scripts Wizard
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  The Generate and Publish Scripts wizard supports a subset of the objects supported by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
## Supported Objects  
 The following table lists the objects that can be published supported by the Generate and Publish Scripts Wizard.  
  
||||||  
|-|-|-|-|-|  
|Application role|Database role|Schema|User-defined aggregate|View*|  
|Assembly|DEFAULT constraint|Stored procedure*|User-defined data type|XML schema collection|  
|CHECK constraint|Full-text catalog|Synonym|User-defined function||  
|CLR (common language runtime) stored procedure*|Index|Table|User-defined table||  
|CLR user-defined function|Rule|User**|User-defined type||  
  
 *Published without encryption.  
  
 **Any non-system users that exist in the database are published as Roles.  
  
  
