---
title: "Objects Supported by the Generate Scripts Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 071eb2cb-f073-41ca-9f4d-11d3b8803495
author: MightyPen
ms.author: genemi
manager: craigg
---
# Objects Supported by the Generate Scripts Wizard
  The Generate and Publish Scripts wizard supports a subset of the objects supported by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
## Supported Objects  
 The following table lists the objects that can be published supported by the Generate and Publish Scripts Wizard.  
  
||||||  
|-|-|-|-|-|  
|Application role|Database role|Schema|User-defined aggregate|View<sup>1</sup>|  
|Assembly|DEFAULT constraint|Stored procedure<sup>1</sup>|User-defined data type|XML schema collection|  
|CHECK constraint|Full-text catalog|Synonym|User-defined function||  
|CLR (common language runtime) stored procedure<sup>1</sup>|Index|Table|User-defined table||  
|CLR user-defined function|Rule|User<sup>2</sup>|User-defined type||  
  
 <sup>1</sup> Published without encryption.  
  
 <sup>2</sup> Any non-system users that exist in the database are published as Roles.  
  
  
