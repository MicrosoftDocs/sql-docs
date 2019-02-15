---
title: "Oracle Publisher | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.newpubwizard.selectoraclepublisher.f1"
ms.assetid: 019b7c49-dcca-445d-8969-5982a8ccbc1a
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Oracle Publisher
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Beginning with [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows you to publish data from an Oracle database using snapshot and transactional replication. For more information, see [Oracle Publishing Overview](../../relational-databases/replication/non-sql/oracle-publishing-overview.md).  
  
 The Oracle Publisher must use a remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributor; this wizard must be run on that server after the necessary Oracle networking software has been installed and tested. For more information, see [Configure an Oracle Publisher](../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md).  
  
> [!IMPORTANT]  
>  If another administrator configured the Oracle database as a Publisher, after clicking **Next** you will be prompted to enter the password for the replication login used to connect to the Oracle database. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will then create a mapping between your login and the linked server connection to the Oracle database. You will not be required to enter a password for subsequent connections to the Oracle database.  
  
## Options  
 **Oracle Publishers**  
 Select an Oracle Publisher from the list. This list contains Oracle Publishers that have previously been configured to use the server against which the wizard is running as their Distributor. If the list is empty, or the Oracle Publisher you want to use is not in the list, click **Add Oracle Publisher**.  
  
 **Add Oracle Publisher**  
 Click to launch the **Distributor Properties** dialog box. In this dialog box, click **Add**, and then click **Add Oracle Publisher**. In the **Connect to Server** dialog box, specify the Oracle server name, and the login and password for the replication administrative user schema. For more information, see [Connect to Server &#40;Oracle&#41;, Login](../../relational-databases/replication/connect-to-server-oracle-login.md).  
  
> [!NOTE]  
>  If the server against which the wizard is running has not yet been configured as a Distributor, you are prompted to configure it now.  
  
## See Also  
 [Create a Publication from an Oracle Database](../../relational-databases/replication/publish/create-a-publication-from-an-oracle-database.md)   

  
  
