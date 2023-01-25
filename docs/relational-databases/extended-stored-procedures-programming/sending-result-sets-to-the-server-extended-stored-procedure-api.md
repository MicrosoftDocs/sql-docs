---
title: "Sending Result Sets to the Server (Extended Stored Procedure API)"
description: Sending Result Sets to the Server (Extended Stored Procedure API)
author: VanMSFT
ms.author: vanto
ms.date: "03/16/2017"
ms.service: sql
ms.topic: "reference"
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "extended stored procedures [SQL Server], sending result sets"
  - "result sets [SQL Server], extended stored procedures"
ms.assetid: 9d54673d-ea9d-4ac6-825a-f216ad8b0e34
---
# Sending Result Sets to the Server (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR Integration instead.  
  
 When sending a result set to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the extended stored procedure should call the appropriate API as follows:  
  
-   The **srv_sendmsg** function may be called in any order before or after all rows (if any) have been sent with **srv_sendrow**. All messages must be sent to the client before the completion status is sent with **srv_senddone**.  
  
-   The **srv_sendrow** function is called once for each row sent to the client. All rows must be sent to the client before any messages, status values, or completion statuses are sent with **srv_sendmsg**, the **srv_status** argument of **srv_pfield**, or **srv_senddone**.  
  
-   Sending a row that has not had all its columns defined with **srv_describe** causes the application to raise an informational error message and return FAIL to the client. In this case, the row is not sent.  
  
## See Also  
 [Creating Extended Stored Procedures](../../relational-databases/extended-stored-procedures-programming/creating-extended-stored-procedures.md)  
  
  
