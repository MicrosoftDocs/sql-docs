---
title: "Publishers | Microsoft Docs"
ms.custom: ""
ms.date: "06/30/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.configuredistributionwizard.enablepublishers.f1"
ms.assetid: 116cd6a5-32ac-4273-81a2-d184408e0f07
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Publishers
  You can give permission for other Publishers to use this Distributor. Be aware that enabling a Publisher to use this server as its remote Distributor does not make that server a Publisher. You must connect to the Publisher, configure it for publishing, and choose this server as the Distributor. You can configure the Publisher and choose a Distributor through the New Publication Wizard.  
  
 The servers you select as Publishers will use the distribution database specified on the **Distribution Database** page of this wizard. If you want to use a different distribution database, do not enable the Publisher at this time. Instead, use the **Distributor Properties** dialog box to add Publishers after you complete the Configure Distribution Wizard.  
  
## Options  
 **Publishers**  
 Select the servers that are allowed to use this Distributor. Click the properties button (**...**) next to a Publisher to view and set additional properties.  
  
 **Add**  
 If the server you want to allow is not listed, click **Add** to add a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher or Oracle Publisher to the list of available Publishers.  
  
## See Also  
 [Configure Distribution](configure-distribution.md)   
 [Configure Publishing and Distribution](configure-publishing-and-distribution.md)   
 [View and Modify Distributor and Publisher Properties](view-and-modify-distributor-and-publisher-properties.md)   
 [Create a Publication](publish/create-a-publication.md)  
  
  
