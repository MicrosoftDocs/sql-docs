---
title: "Agent XPs Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "Agent XPs option"
  - "extended stored procedures [SQL Server], SQL Server Agent"
ms.assetid: 2e1c6c64-5ce7-4357-98c7-ac7763a9f9de
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Agent XPs Server Configuration Option
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  Use the **Agent XPs** option to enable the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent extended stored procedures on this server. When this option is not enabled, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent node is not available in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer.  
  
 When you use the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] tool to start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service, these extended stored procedures are enabled automatically. For more information, see [Surface Area Configuration](../../relational-databases/security/surface-area-configuration.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] Object Explorer does not display the contents of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]Agent node unless these extended stored procedures are enabled regardless of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service state.  
  
 The possible values are:  
  
-   **0**, indicating that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent extended stored procedures are not available (the default).  
  
-   **1**, indicating that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent extended stored procedures are available.  
  
 The setting takes effect immediately without a server stop and restart.  
  
## Example
 The following example enables the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent extended stored procedures.  

1. From Microsoft SQL Server Management Studio connect to the Database Engine.

2.  From the Standard bar, click **New Query.**

3.  Copy and paste the following example into the query window and click **Execute**. 
  
```sql 
sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE;  
GO  
sp_configure 'Agent XPs', 1;  
GO  
RECONFIGURE  
GO  
```  
  
## See Also  
 [Automated Administration Tasks &#40;SQL Server Agent&#41;](https://msdn.microsoft.com/library/541ee5ac-2c9f-4b74-b4f0-13b7bd5920b0)   
 [Start, Stop, or Pause the SQL Server Agent Service](https://msdn.microsoft.com/library/c95a9759-dd30-4ab6-9ab0-087bb3bfb97c)  
  
  
