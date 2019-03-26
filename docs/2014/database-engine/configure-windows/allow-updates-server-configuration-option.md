---
title: "allow updates Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "allow updates option"
ms.assetid: 3967c3ed-280a-4de8-a2ce-393e82745a7b
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# allow updates Server Configuration Option
  This option is still present in the **sp_configure** stored procedure, although its functionality is unavailable in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The setting has no effect. Direct updates to the system tables are not supported.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)]  
  
 Changing the **allow updates** option will cause the RECONFIGURE statement to fail. Changes to the **allow updates** option should be removed from all scripts.  
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)  
  
  
