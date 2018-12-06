---
title: "Alter an Extended Events Session | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xevents
ms.topic: conceptual
ms.assetid: 114ec05b-7eca-4c87-b276-25e37b84be39
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Alter an Extended Events Session
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

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
 [Create an Extended Events Session Using Query Editor](https://msdn.microsoft.com/library/cba0e02b-b201-4863-bf1b-9164e68e5fa8)  
  
  
