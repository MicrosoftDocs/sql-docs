---
title: "NS$&lt;service name&gt; Properties (Log On Tab)"
description: Learn about the Log On tab of the Notification Services Properties dialog box in SQL Server. See how to specify an account and start or stop the service.
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
monikerRange: ">=sql-server-2016"
---
# NS$&lt;service name&gt; Properties (Log On Tab)
[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
  Use the **Log On** tab of the **Notification Services Properties** dialog box to specify the account used by the [!INCLUDE[ssNS](../../includes/ssns-md.md)] service, and to start and stop the service.  
  
## Options  
 **Local System account**  
 Specify a local system account, which does not require a password. However, the local system account may restrict the service from interacting with other servers, depending on the privileges granted to the account.  
  
 **This account**  
 Specify a local or domain user account that uses [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends using a domain user account with minimal rights for services. For information about selecting an account, search Books Online for the topic, "Setting Up Windows Service Accounts."  
  
 **Account Name**  
 Specify the local or domain user account name.  
  
 **Password**  
 Type the password of the account.  
  
 **Confirm password**  
 Type the password of the account again.  
  
 **Start**  
 Start the service.  
  
 **Stop**  
 Stop the service.  
  
 **Pause**  
 Pause the service.  
  
 **Resume**  
 Resume a paused service.  
  
  
