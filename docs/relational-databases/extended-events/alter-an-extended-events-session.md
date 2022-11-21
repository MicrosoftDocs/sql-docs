---
title: "Alter an Extended Events Session"
description: Use the SQL Server Extended Events Wizard to alter an Extended Events session. Changes you can make depend on whether the session is active and inactive.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "05/24/2019"
ms.service: sql
ms.subservice: xevents
ms.topic: tutorial
ms.assetid: 114ec05b-7eca-4c87-b276-25e37b84be39
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Alter an Extended Events Session

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  After you create an Extended Events session, you can alter it according to your needs using the **SQL Server Extended Events Wizard**.  
  
## Before you Begin  
 You cannot alter a target for active and inactive sessions, and you cannot change the advanced properties configurations for an active session.  
  
 You can make the following alterations to both active and inactive event sessions:  
  
-   Add or remove events from the session, and alter the event configurations such as event fields, filters and actions.  
  
-   Add or remove a target from the event session.  
  
-   Enable the **Start the event session on server startup** option.  
  
 You can make the following additional alterations to an inactive event session:  
  
-   Enable the **Track the relationship between events** option.  
  
-   Change the advanced properties configuration.  
  
> [!NOTE]  
>  The **SQL Server Extended Events Wizard** does not support event session modification.  
  
## How to alter an Extended Events session using the SQL Server Extended Events Wizard  
  
-   In Object Explorer, expand **Management**, expand **Extended Events**, and then expand **Sessions**.  
  
-   Right-click the session you want to alter, and then click **Properties**.  
  
-   In the **Properties** dialog box, make the appropriate changes, and then click **OK**.  
  
## See Also  
 [ALTER EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-event-session-transact-sql.md)   
 [Create an Extended Events Session Using Query Editor](quick-start-extended-events-in-sql-server.md)  
  
  
